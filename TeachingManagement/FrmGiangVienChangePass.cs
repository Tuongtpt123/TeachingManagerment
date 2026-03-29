using Org.BouncyCastle.Crypto.Engines;
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
    public partial class FrmGiangVienChangePass : Form
    {
        private int _giangVienId;
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public FrmGiangVienChangePass()
        {
            InitializeComponent();
        }

        public FrmGiangVienChangePass(int giangVienId)
        {
            _giangVienId = giangVienId;
            InitializeComponent();
        }

        private void FrmGiangVienChangePass_Load(object sender, EventArgs e)
        {

        }

        private void lblHide_Click(object sender, EventArgs e)
        {
            txtMatKhauHienTai.UseSystemPasswordChar = !txtMatKhauHienTai.UseSystemPasswordChar;
            txtMatKhauMoi.UseSystemPasswordChar = !txtMatKhauMoi.UseSystemPasswordChar;
            txtMatKhauConfirm.UseSystemPasswordChar = !txtMatKhauConfirm.UseSystemPasswordChar;
            lblHide.Text = txtMatKhauHienTai.UseSystemPasswordChar ? "Hiện" : "Ẩn";
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatKhauHienTai.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhauConfirm.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMatKhauMoi.Text == txtMatKhauHienTai.Text)
            {
                MessageBox.Show("Mật khẩu mới phải khác mật khẩu hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtMatKhauMoi.Text != txtMatKhauConfirm.Text)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (txtMatKhauMoi.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult rs = MessageBox.Show("Xác nhận đổi mật khẩu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs != DialogResult.Yes) { return;  }

            // Kiểm tra mật khẩu hiện tại
            string sqlCheck = @"SELECT MatKhauHash FROM GiangVien WHERE GiangVienId = @GiangVienId";
             try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlCheck, conn))
                    {
                        cmd.Parameters.AddWithValue("@GiangVienId", _giangVienId);
                        string currentHash = (string)cmd.ExecuteScalar();
                        if (!BCrypt.Net.BCrypt.Verify(txtMatKhauHienTai.Text, currentHash))
                        {
                            MessageBox.Show("Mật khẩu hiện tại không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Cập nhật mật khẩu mới

            string sql = @"UPDATE GiangVien
                SET MatKhauHash = @MatKhauHash
                WHERE GiangVienId = @GiangVienId";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        string newHash = BCrypt.Net.BCrypt.HashPassword(txtMatKhauMoi.Text);
                        cmd.Parameters.AddWithValue("@MatKhauHash", newHash);
                        cmd.Parameters.AddWithValue("@GiangVienId", _giangVienId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đổi mật khẩu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
