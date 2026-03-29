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

namespace TeachingManagement
{
    public partial class FrmSinhVienAdd : Form
    {
        private string connStr = "server=localhost;database=db_doan2;uid=root;pwd=;port=3306;";

        public FrmSinhVienAdd()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSinhVien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }


            // Kiểm tra xem mã sinh viên đã tồn tại chưa
            string checkQuery = "SELECT TrangThai FROM SinhVien WHERE MaSinhVien = @MaSinhVien";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                        object result = cmd.ExecuteScalar();

                        if (result != null) // Sinh viên đã tồn tại trong hệ thống
                        {
                            int trangThai = Convert.ToInt32(result);

                            // Trường hợp sinh viên đang hoạt động
                            if (trangThai == 1)
                            {
                                MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng kiểm tra lại.",
                                                "Thông báo",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);

                                txtMaSinhVien.Focus();
                                return;
                            }
                            // Trường hợp sinh viên đã bị "xóa mềm"
                            else if (trangThai == 0)
                            {
                                DialogResult rs = MessageBox.Show(
                                    "Sinh viên này đã tồn tại nhưng đang bị vô hiệu hóa.\n" +
                                    "Bạn có muốn kích hoạt lại sinh viên này với thông tin mới không?",
                                    "Khôi phục sinh viên",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                                if (rs == DialogResult.Yes)
                                {
                                    string updateQuery = @"UPDATE SinhVien 
                                               SET HoTen = @HoTen,
                                                   Email = @Email,
                                                   TrangThai = 1
                                               WHERE MaSinhVien = @MaSinhVien";

                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@HoTen", hoTen);
                                        updateCmd.Parameters.AddWithValue("@Email", email);
                                        updateCmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                                        updateCmd.ExecuteNonQuery();
                                    }

                                    MessageBox.Show("Đã kích hoạt lại sinh viên thành công.",
                                                    "Thông báo",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                                }

                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra sinh viên: " + ex.Message,
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            DialogResult chon = MessageBox.Show(
                    $"Xác nhận thêm sinh viên mới?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
            if (chon != DialogResult.Yes)
            {
                return;
            }

            string query = @"INSERT INTO sinhvien (MaSinhVien, HoTen, Email, TrangThai) 
                VALUES (@MaSinhVien, @HoTen, @Email, 1);";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm sinh viên thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
