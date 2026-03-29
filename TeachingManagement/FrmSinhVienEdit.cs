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
    public partial class FrmSinhVienEdit : Form
    {
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
        private string _maSinhVien;
        public FrmSinhVienEdit()
        {
            InitializeComponent();
        }

        public FrmSinhVienEdit(string MaSinhVien)
        {
            InitializeComponent();

            _maSinhVien = MaSinhVien;
        }

        private void FrmSinhVienEdit_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            string query = @"
                SELECT 
                    MaSinhVien,
                    HoTen,
                    Email
                FROM SinhVien
                WHERE MaSinhVien = @MaSinhVien";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSinhVien", _maSinhVien);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblMaSinhVien.Text = reader["MaSinhVien"].ToString();
                                txtHoTen.Text = reader["HoTen"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sinh viên.");
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống.", "Cảnh báo!");
                txtHoTen.Focus();
                return;
            }
            DialogResult result = MessageBox.Show(
                    "Xác nhận cập nhật thông tin sinh viên?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
            if (result != DialogResult.Yes) {
                return;
            }
            string query = @"
                UPDATE SinhVien
                SET HoTen = @HoTen, Email = @Email
                WHERE MaSinhVien = @MaSinhVien";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@MaSinhVien", _maSinhVien);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin sinh viên thành công.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không có thay đổi nào được thực hiện.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }
    }
}
