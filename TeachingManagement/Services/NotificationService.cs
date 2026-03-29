using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace TeachingManagement.Services
{
    public class NotificationService
    {
        private string connStr;

        public NotificationService(string connectionString)
        {
            connStr = connectionString;
        }

        public class NotificationItem
        {
            public string Loai { get; set; }
            public string NoiDung { get; set; }
            public DateTime ThoiGian { get; set; }
        }

        public List<NotificationItem> GetNotifications(int giangVienId)
        {
            List<NotificationItem> list = new List<NotificationItem>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                list.AddRange(GetUpcomingDeadlines(conn, giangVienId));
                list.AddRange(GetExpiredAssignments(conn, giangVienId));
                list.AddRange(GetInactiveClasses(conn, giangVienId));
            }


            // Sắp xếp mới nhất lên đầu
            list.Sort((a, b) => b.ThoiGian.CompareTo(a.ThoiGian));

            return list;
        }

        public int GetNotificationCount(int giangVienId)
        {
            int count = 0;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                count += GetUpcomingDeadlines(conn, giangVienId).Count;
                count += GetExpiredAssignments(conn, giangVienId).Count;
                count += GetInactiveClasses(conn, giangVienId).Count;
            }


            return count;
        }

        // ==============================
        // 1. Bài tập sắp hết hạn
        // ==============================

        private List<NotificationItem> GetUpcomingDeadlines(MySqlConnection conn, int giangVienId)
        {
            List<NotificationItem> list = new List<NotificationItem>();

            string query = @"
                SELECT bt.TieuDe, bt.HanNop, lh.TenLop
                FROM BaiTap bt
                JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId
                WHERE lh.GiangVienId = @GiangVienId
                AND bt.HanNop BETWEEN NOW() AND DATE_ADD(NOW(), INTERVAL 3 DAY)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@GiangVienId", giangVienId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new NotificationItem
                    {
                        Loai = "⏰ Sắp hết hạn",
                        NoiDung = $"Bài tập \"{reader["TieuDe"]}\" của lớp \"{reader["TenLop"]}\" sắp hết hạn. (\"{reader["HanNop"]}\")",
                        ThoiGian = DateTime.Now
                    });
                }
            }

            return list;
        }

        // ==============================
        // 2. Bài tập đã hết hạn
        // ==============================

        private List<NotificationItem> GetExpiredAssignments(MySqlConnection conn, int giangVienId)
        {
            List<NotificationItem> list = new List<NotificationItem>();

            string query = @"
                SELECT bt.TieuDe, lh.TenLop, bt.HanNop, 
                COUNT(lsv.SinhVienId) - COUNT(bn.BaiNopId) AS SoSVChuaNop
                FROM BaiTap bt
                JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId
                JOIN LopHoc_SinhVien lsv ON lh.LopHocId = lsv.LopHocId
                LEFT JOIN BaiNop bn 
                    ON bn.BaiTapId = bt.BaiTapId
                    AND bn.SinhVienId = lsv.SinhVienId
                WHERE bt.HanNop < NOW()
                AND lh.GiangVienId = @GiangVienId
                GROUP BY bt.BaiTapId
                HAVING SoSVChuaNop > 0";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@GiangVienId", giangVienId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string title = reader.GetString("TieuDe");
                    string lop = reader.GetString("TenLop");
                    int soSV = reader.GetInt32("SoSVChuaNop");
                    DateTime thoiGian = Convert.ToDateTime(reader["HanNop"]);

                    list.Add(new NotificationItem
                    {
                        Loai = "❗ Hết hạn",
                        NoiDung = $"Bài tập \"{title}\" lớp {lop} còn {soSV} sinh viên chưa nộp",
                        ThoiGian = thoiGian
                    });
                }
            }

            return list;
        }

        // ==============================
        // 3. Lớp lâu không hoạt động
        // ==============================

        private List<NotificationItem> GetInactiveClasses(MySqlConnection conn, int giangVienId)
        {
            List<NotificationItem> list = new List<NotificationItem>();

            string query = @"
                SELECT lh.TenLop, MAX(bt.NgayTao) AS BaiTapGanNhat
                FROM LopHoc lh
                LEFT JOIN BaiTap bt ON lh.LopHocId = bt.LopHocId
                WHERE lh.GiangVienId = @GiangVienId
                    AND lh.TrangThai = 1
                GROUP BY lh.LopHocId
                HAVING BaiTapGanNhat < DATE_SUB(NOW(), INTERVAL 7 DAY)
                   OR BaiTapGanNhat IS NULL";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@GiangVienId", giangVienId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string lop = reader.GetString("TenLop");

                    list.Add(new NotificationItem
                    {
                        Loai = "⚠ Không hoạt động",
                        NoiDung = $"Lớp {lop} đã lâu chưa giao bài tập mới",
                        ThoiGian = Convert.ToDateTime(reader["BaiTapGanNhat"])
                    });
                }
            }

            return list;
        }
    }
}