using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TeachingManagement
{
    public partial class FrmAuth : Form
    {
        public FrmAuth()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTenReg.Text.Trim();
            string email = txtEmailReg.Text.Trim();
            string matKhau = txtPassReg.Text.Trim();
            string tenDangNhap = txtTenDangNhapReg.Text.Trim();
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng ký.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                MessageBox.Show("Email không hợp lệ.");
                txtEmailReg.Focus();
                return;
            }

            string connectionString = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // 4. Kiểm tra username đã tồn tại
                    string checkQuery = "SELECT COUNT(*) FROM GiangVien WHERE TenDangNhap = @TenDangNhap";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Tên đang nhập đã tồn tại.");
                            return;
                        }
                    }

                    // 5. Hash password bằng BCrypt
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(matKhau);

                    // 6. Insert tài khoản mới
                    string insertQuery = @"INSERT INTO GiangVien (TenDangNhap, HoTen, Email, MatKhauHash)
                                   VALUES (@TenDangNhap, @HoTen, @Email, @PasswordHash)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Đăng ký thành công. Hãy đăng nhập.");

                            // Xóa dữ liệu form
                            txtHoTenReg.Clear();
                            txtEmailReg.Clear();
                            txtPassReg.Clear();
                            txtTenDangNhapReg.Clear();

                            // Chuyển về panel đăng nhập
                            tabControl1.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("Đăng ký thất bại.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống:\n" + ex.Message);
            }
        }

        private void lblShowPass_Click(object sender, EventArgs e)
        {
            lblShowPass.Text = lblShowPass.Text == "Hiện" ? "Ẩn" : "Hiện";
            txtPassReg.UseSystemPasswordChar = lblShowPass.Text == "Hiện" ? true : false;
            txtPassReg.Focus();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
            lblShowLog.Text = lblShowLog.Text == "Hiện" ? "Ẩn" : "Hiện";
            txtPassLog.UseSystemPasswordChar = lblShowLog.Text == "Hiện" ? false : true;
            txtPassLog.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhapLog.Text.Trim();
            string matKhau = txtPassLog.Text.Trim();
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT GiangVienId, HoTen, Email, MatKhauHash
                             FROM GiangVien
                             WHERE TenDangNhap = @TenDangNhap
                             LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 2. Kiểm tra email tồn tại
                            if (!reader.Read())
                            {
                                MessageBox.Show("Tên người dùng hoặc mật khẩu không khớp.", "Lỗi đăng nhập");
                                return;
                            }

                            int giangVienId = reader.GetInt32("GiangVienId");
                            string hoTen = reader.GetString("HoTen");
                            string dbEmail = reader.GetString("Email");
                            string passwordHash = reader.GetString("MatKhauHash");

                            // 3. Kiểm tra mật khẩu bằng BCrypt
                            bool isValid = BCrypt.Net.BCrypt.Verify(matKhau, passwordHash);

                            if (!isValid)
                            {
                                MessageBox.Show("Tên người dùng hoặc mật khẩu không khớp.", "Lỗi đăng nhập");
                                return;
                            }

                            // 4. Lưu session
                            Session.GiangVienId = giangVienId;
                            Session.HoTen = hoTen;
                            Session.Email = dbEmail;
                            Session.IsLoggedIn = true;

                            // 5. Trả kết quả cho Program.cs
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Lỗi kết nối database:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống:\n" + ex.Message);
            }
        }

        private void FrmAuth_Load(object sender, EventArgs e)
        {
            txtTenDangNhapLog.Focus();
            txtTenDangNhapLog.Text = "admin"; // Mặc định cho dễ test
            txtPassLog.Text = "admin123";
        }

        private void lblToReg_Click(object sender, EventArgs e)
        {
            // Chuyển sang tabPage đăng ký
            tabControl1.SelectedIndex = 1;
        }

        private void lblToLogin_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AcceptButton = tabControl1.SelectedIndex == 0 ? btnLogin : btnReg;
        }
    }
}
