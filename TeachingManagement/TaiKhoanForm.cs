using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeachingManagement
{
    public partial class TaiKhoanForm : Form
    {
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public TaiKhoanForm()
        {
            InitializeComponent();
        }



        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void TaiKhoanForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData() {
            string sql = @"SELECT TenDangNhap, HoTen, Email, NgayTao
                FROM GiangVien
                WHERE GiangVienId = @GiangVienId;";
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblTenDangNhap.Text = reader.GetString("TenDangNhap");
                                lblHoTen.Text = reader.GetString("HoTen");
                                lblHoTen2.Text = reader.GetString("HoTen");
                                lblEmail.Text = reader.GetString("Email");
                                lblNgayTao.Text = reader.GetDateTime("NgayTao").ToString("dd/MM/yyyy");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FrmGiangVienEdit editForm = new FrmGiangVienEdit(Session.GiangVienId);
            editForm.ShowDialog();
            loadData();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            FrmGiangVienChangePass frmGiangVienChangePass = new FrmGiangVienChangePass(Session.GiangVienId);
            frmGiangVienChangePass.ShowDialog();
        }
    }
}
