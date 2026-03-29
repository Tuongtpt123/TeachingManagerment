using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeachingManagement
{
    public partial class FrmGiangVienEdit : Form
    {
        private int _giangVienId;
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";


        public FrmGiangVienEdit()
        {
            InitializeComponent();
        }

        public FrmGiangVienEdit(int GiangVienId)
        {
            _giangVienId = GiangVienId;
            InitializeComponent();
        }

        private void FrmGiangVienEdit_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            string sql = @"SELECT TenDangNhap, HoTen, Email
                FROM GiangVien
                WHERE GiangVienId = @GiangVienId;";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GiangVienId", _giangVienId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTenDangNhap.Text = reader.GetString("TenDangNhap");
                                txtHoTen.Text = reader.GetString("HoTen");
                                txtEmail.Text = reader.GetString("Email");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (String.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Focus();
                return;
            }
            if (String.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen.Focus();
                return;
            }
            if (String.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            // Kiểm tra định dạng email đơn giản
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            // Kiểm tra trùng tên đăng nhập
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    string checkSql = @"SELECT COUNT(*) FROM GiangVien WHERE TenDangNhap = @TenDangNhap AND GiangVienId != @GiangVienId;";
                    using (var checkCmd = new MySql.Data.MySqlClient.MySqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        checkCmd.Parameters.AddWithValue("@GiangVienId", _giangVienId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTenDangNhap.Focus();
                            return;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra tên đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DialogResult rs = MessageBox.Show("Lưu thay đổi?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs != DialogResult.Yes) {
                return;
            }

            string updateSql = @"UPDATE GiangVien
                SET TenDangNhap = @TenDangNhap, HoTen = @HoTen, Email = @Email
                WHERE GiangVienId = @GiangVienId;";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@GiangVienId", _giangVienId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không có thay đổi nào được thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
