using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

// Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;
// W, H = 816, 580
namespace TeachingManagement
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();

            
            dgvClasses.MultiSelect = false;
            dgvClasses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClasses.ReadOnly = true;
            dgvClasses.AllowUserToAddRows = false;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            LoadClasses();
            loadTongLopHoc();
            loadTongSinhVien();
            loadBaiTapDangMo();

            loadThoiGianDongBoGanNhat();
            loadBaiTapSapHetHan();
            loadBaiCoSinhVienChuaNop();
            loadDaNopNhungChuaXem();
        }


        private void LoadClasses()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

            string query = @"
        SELECT 
            lh.TenLop,

            (SELECT COUNT(*) 
             FROM LopHoc_SinhVien lsv 
             WHERE lsv.LopHocId = lh.LopHocId) AS SoSinhVien,

            (SELECT COUNT(*) 
             FROM BaiTap bt 
             WHERE bt.LopHocId = lh.LopHocId) AS SoBaiTap,

            CASE
                WHEN NOT EXISTS (
                    SELECT 1 FROM BaiTap bt 
                    WHERE bt.LopHocId = lh.LopHocId
                ) THEN 'Chưa có bài tập'

                WHEN (
                    SELECT COUNT(*) 
                    FROM LopHoc_SinhVien lsv
                    WHERE lsv.LopHocId = lh.LopHocId
                ) = (
                    SELECT COUNT(DISTINCT bn.SinhVienId)
                    FROM BaiTap bt
                    LEFT JOIN BaiNop bn ON bt.BaiTapId = bn.BaiTapId
                    WHERE bt.LopHocId = lh.LopHocId
                    AND bt.BaiTapId = (
                        SELECT MAX(BaiTapId)
                        FROM BaiTap
                        WHERE LopHocId = lh.LopHocId
                    )
                ) THEN 'Đã nộp đủ'

                ELSE 'Còn sinh viên chưa nộp'
            END AS TrangThai

        FROM LopHoc lh
        WHERE lh.GiangVienId = @GiangVienId
            AND lh.TrangThai = 1;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvClasses.DataSource = dt;
                }

                // Đặt lại tên cột cho đẹp
                dgvClasses.Columns["TenLop"].HeaderText = "Tên lớp";
                dgvClasses.Columns["SoSinhVien"].HeaderText = "Số sinh viên";
                dgvClasses.Columns["SoBaiTap"].HeaderText = "Số bài tập";
                dgvClasses.Columns["TrangThai"].HeaderText = "Trạng thái gần nhất";
                dgvClasses.Columns["TrangThai"].Width = 150;


                foreach (DataGridViewRow row in dgvClasses.Rows)
                {
                    string status = row.Cells["TrangThai"].Value?.ToString();

                    if (status == "Đã nộp đủ")
                        row.Cells["TrangThai"].Style.ForeColor = Color.Green;

                    else if (status == "Còn sinh viên chưa nộp")
                        row.Cells["TrangThai"].Style.ForeColor = Color.Red;

                    else
                        row.Cells["TrangThai"].Style.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void loadTongLopHoc()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string query = @"
                    SELECT COUNT(*)
                    FROM LopHoc
                    WHERE GiangVienId = @GiangVienId 
                    AND TrangThai = 1;
                ";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                int totalClasses = Convert.ToInt32(cmd.ExecuteScalar());
                lblTongSoLop.Text = $"" + totalClasses;
            }
        }

        private void loadTongSinhVien()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string query = 
                "SELECT COUNT(DISTINCT lsv.SinhVienId)\r\n" +
                "FROM LopHoc_SinhVien lsv\r\n" +
                "JOIN LopHoc lh ON lh.LopHocId = lsv.LopHocId\r\n" +
                "JOIN SinhVien sv ON sv.SinhVienId = lsv.SinhVienId\r\n" +
                "WHERE lh.GiangVienId = @GiangVienId AND sv.TrangThai = 1;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                int totalStudents = Convert.ToInt32(cmd.ExecuteScalar());
                lblTongSoSinhVien.Text = $"" + totalStudents;
            }
        }

        private void loadBaiTapDangMo()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string query =
                "SELECT COUNT(DISTINCT lsv.SinhVienId)\r\n" +
                "FROM LopHoc_SinhVien lsv\r\n" +
                "JOIN LopHoc lh ON lh.LopHocId = lsv.LopHocId\r\n" +
                "WHERE lh.GiangVienId = @GiangVienId;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                int openAssignments = Convert.ToInt32(cmd.ExecuteScalar());
                lblBaiTapDangMo.Text = $"" + openAssignments;
            }
        }

        private void loadBaiTapSapHetHan()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string querry =
                "SELECT COUNT(*)\r\n" +
                "FROM BaiTap bt\r\n" +
                "JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId\r\n" +
                "WHERE lh.GiangVienId = @GiangVienId\r\n" +
                "AND bt.HanNop BETWEEN NOW() AND DATE_ADD(NOW(), INTERVAL 3 DAY);";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                int expiringAssignments = Convert.ToInt32(cmd.ExecuteScalar());
                lblBaiTapSapHetHan.Text = $"" + expiringAssignments;
            }
        }
        
        private void loadBaiCoSinhVienChuaNop()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string querry =
                "SELECT COUNT(DISTINCT bt.BaiTapId)\r\n" +
                "FROM BaiTap bt\r\n" +
                "JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId\r\n" +
                "WHERE lh.GiangVienId = @GiangVienId\r\n" +
                "AND EXISTS (\r\n" +
                "    SELECT 1\r\n" +
                "    FROM LopHoc_SinhVien lsv\r\n" +
                "    WHERE lsv.LopHocId = bt.LopHocId\r\n" +
                "    AND NOT EXISTS (\r\n" +
                "        SELECT 1\r\n" +
                "        FROM BaiNop bn\r\n" +
                "        WHERE bn.BaiTapId = bt.BaiTapId\r\n" +
                "        AND bn.SinhVienId = lsv.SinhVienId\r\n" +
                "    )\r\n" +
                ");";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                int studentsWithoutSubmission = Convert.ToInt32(cmd.ExecuteScalar());
                lblBaiCoSinhVienChuaNop.Text = $"" + studentsWithoutSubmission;
            }
        }

        private void loadDaNopNhungChuaXem()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string querry = @"
                SELECT COUNT(*)
                FROM BaiNop bn
                JOIN BaiTap bt ON bn.BaiTapId = bt.BaiTapId
                JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId
                WHERE lh.GiangVienId = @GiangVienId
                    AND bn.TrangThai != 2";
                
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                int ungradedSubmissions = Convert.ToInt32(cmd.ExecuteScalar());
                lblDaNopNhungChuaCham.Text = $"" + ungradedSubmissions;
            }
        }

        private void loadThoiGianDongBoGanNhat()
        {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string querry = @"
                SELECT LastSyncTime FROM GiangVien WHERE GiangVienId = @GiangVienId";
                
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                if (cmd.ExecuteScalar() != DBNull.Value)
                {
                    DateTime lastSyncTime = Convert.ToDateTime(cmd.ExecuteScalar());
                    HienThiLastSync(lastSyncTime);
                }
                else
                {
                    lblDongBoLanCuoi.Text = "Chưa đồng bộ";
                }

            }
        }

        public void HienThiLastSync(DateTime lastSync)
        {
            string timeAgo = GetTimeAgo(lastSync);

            lblDongBoLanCuoi.Text =
                $"{lastSync:dd/MM/yyyy HH:mm} \n({timeAgo})";
        }

        public static string GetTimeAgo(DateTime time)
        {
            TimeSpan diff = DateTime.Now - time;

            if (diff.TotalSeconds < 60)
                return $"{(int)diff.TotalSeconds} giây trước";

            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} phút trước";

            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} giờ trước";

            if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} ngày trước";

            if (diff.TotalDays >=7 && diff.TotalDays < 30)
                return $"{(int)(diff.TotalDays / 7)} tuần trước";

            if (diff.TotalDays >= 30 && diff.TotalDays < 365)
                return $"{(int)(diff.TotalDays / 30)} tháng trước";

            return time.ToString("dd/MM/yyyy HH:mm");
        }

        private void lblTongSoLop_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThemLopMoi_Click(object sender, EventArgs e)
        {
            FrmLopHocEdit frm = new FrmLopHocEdit(0);
            frm.ShowDialog();
        }
    }
}
