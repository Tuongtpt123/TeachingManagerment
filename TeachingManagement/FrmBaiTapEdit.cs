using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeachingManagement
{
    public partial class FrmBaiTapEdit : Form
    {
        private int _baiTapId;
        private int _lopHocId;
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public FrmBaiTapEdit()
        {
            InitializeComponent();
        }

        public FrmBaiTapEdit(int baiTapId, int lopHocId)
        {
            _baiTapId = baiTapId;
            _lopHocId = lopHocId;
            InitializeComponent();
        }
        private void FrmBaiTapEdit_Load(object sender, EventArgs e)
        {
            loadLopHoc();
            loadBaiTap();
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtDirectory.Text = fbd.SelectedPath;
            }
        }

        private void loadLopHoc()
        {
            string sql = "SELECT TenLop, MoTa FROM LopHoc WHERE LopHocId = @LopHocId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblTenLop.Text = reader["TenLop"].ToString();
                                lblMoTa_LopHoc.Text = reader["MoTa"].ToString();
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

        private void loadBaiTap()
        {
            string sql = @"
                SELECT 
                    TieuDe, 
                    MoTa, 
                    HanNop, 
                    LinkDrive 
                FROM BaiTap 
                WHERE BaiTapId = @BaiTapId
                    AND LopHocId = @LopHocId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@BaiTapId", _baiTapId);
                        cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTieuDe.Text = reader["TieuDe"].ToString();
                                txtMoTa.Text = reader["MoTa"].ToString();
                                dtpHanNop.Value = Convert.ToDateTime(reader["HanNop"]);
                                txtDirectory.Text = reader["LinkDrive"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu bài tập: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            checkValidate();

            DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn cập nhật bài tập này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
            if (result != DialogResult.Yes) { return; }

            string tieuDe = txtTieuDe.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            string path = txtDirectory.Text.Trim();
            DateTime hanNop = dtpHanNop.Value;

            string sql = @"
                UPDATE BaiTap 
                SET TieuDe = @TieuDe, 
                    MoTa = @MoTa, 
                    HanNop = @HanNop, 
                    LinkDrive = @LinkDrive
                WHERE BaiTapId = @BaiTapId
                    AND LopHocId = @LopHocId";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TieuDe", tieuDe);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@HanNop", hanNop);
                        cmd.Parameters.AddWithValue("@LinkDrive", path);
                        cmd.Parameters.AddWithValue("@BaiTapId", _baiTapId);
                        cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật bài tập thành công!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật bài tập thất bại.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật bài tập: " + ex.Message);
            }
        }

        private bool ValidateHanNop()
        {
            DateTime now = DateTime.Now;
            DateTime hanNop = dtpHanNop.Value;

            // Không cho nhỏ hơn hiện tại
            if (hanNop <= now)
            {
                MessageBox.Show("Hạn nộp phải lớn hơn thời điểm hiện tại.");
                return false;
            }

            // Không cho quá sát (ít nhất 10 phút)
            if ((hanNop - now).TotalMinutes < 10)
            {
                MessageBox.Show("Hạn nộp phải cách thời điểm hiện tại ít nhất 10 phút.");
                return false;
            }

            // Không cho quá xa (ví dụ tối đa 1 năm)
            if ((hanNop - now).TotalDays > 365)
            {
                MessageBox.Show("Hạn nộp không được vượt quá 1 năm.");
                return false;
            }

            return true;
        }

        public bool ValidateFolderPath(string path, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(path))
            {
                message = "Đường dẫn không được để trống.";
                return false;
            }

            try
            {
                Path.GetFullPath(path);
            }
            catch
            {
                message = "Đường dẫn không đúng định dạng.";
                return false;
            }

            if (!Directory.Exists(path))
            {
                message = "Thư mục không tồn tại.";
                return false;
            }

            try
            {
                Directory.GetFiles(path);
            }
            catch
            {
                message = "Không có quyền truy cập thư mục.";
                return false;
            }

            return true;
        }

        private void checkValidate()
        {
            string tieuDe = txtTieuDe.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            string path = txtDirectory.Text.Trim();
            if (string.IsNullOrEmpty(tieuDe))
            {
                MessageBox.Show("Tiêu đề bài tập không được để trống.");
                txtTieuDe.Focus();
                return;
            }
            if (string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Mô tả bài tập không được để trống.");
                txtMoTa.Focus();
                return;
            }
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Đường dẫn không được để trống.");
                txtDirectory.Focus();
                return;
            }
            if (!ValidateFolderPath(path, out string error))
            {
                MessageBox.Show(error);
                txtDirectory.Focus();
                return;
            }
            if (!ValidateHanNop())
            {
                dtpHanNop.Focus();
                return;
            }
        }
    }
}
