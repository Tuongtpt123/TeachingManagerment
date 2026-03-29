using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachingManagement
{
    public static class Session
    {
        private static int _giangVienId;
        private static string _hoTen;
        private static string _email;
        private static bool _isLoggedIn = false;
        //TenDangNhap, Pass = admin, admin123

        // ====== GET / SET ======

        public static int GiangVienId
        {
            get { return _giangVienId; }
            set { _giangVienId = value; }
        }

        public static string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }

        public static string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public static bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { _isLoggedIn = value; }
        }

        // ====== Hàm khởi tạo session sau login ======

        public static void SetSession(int giangVienId, string hoTen, string email)
        {
            _giangVienId = giangVienId;
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string sql = "SELECT HoTen, Email FROM GiangVien WHERE GiangVienId = @giangVienId";
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@giangVienId", giangVienId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _hoTen = reader.GetString("HoTen");
                            _email = reader.GetString("Email");
                            
                        }
                    }
                }
            }
            _isLoggedIn = true;
        }

        // ====== Logout ======

        public static void ClearSession()
        {
            _giangVienId = 0;
            _hoTen = null;
            _email = null;
            _isLoggedIn = false;
        }
    }
}
