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
    public partial class FrmBaiTapAdd : Form
    {
        private int _lopHocId;
        private string _tenLop;
        string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public FrmBaiTapAdd()
        {
            InitializeComponent();
        }

        public FrmBaiTapAdd(int lopHocId)
        {
            InitializeComponent();
            _lopHocId = lopHocId;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            checkValidate();

            DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thêm bài tập này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );
            if (result != DialogResult.Yes) { return; }
            
            string tieuDe = txtTieuDe.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            string path = txtDirectory.Text.Trim();
            DateTime hanNop = dtpHanNop.Value;

            string sql = "INSERT INTO BaiTap (LopHocId, TieuDe, MoTa, HanNop, LinkDrive) " +
                         "VALUES (@LopHocId, @TieuDe, @MoTa, @HanNop, @LinkDrive)";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                        cmd.Parameters.AddWithValue("@TieuDe", tieuDe);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@HanNop", hanNop);
                        cmd.Parameters.AddWithValue("@LinkDrive", path);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm bài tập thành công!\nĐể lấy link drive, mở đường dẫn vừa tạo -> click chuột phải vào folder và chọn Copy link.", "Thông báo!");
                            
                        }
                        else
                        {
                            MessageBox.Show("Thêm bài tập thất bại.", "Cảnh báo");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm bài tập: " + ex.Message, "Lỗi");
            }
        }

        private void FrmBaiTapAdd_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
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

                                _tenLop = reader["TenLop"].ToString();
                                lblTenLop.Text = _tenLop;
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

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string basePath = @"G:\My Drive\LMS_Submissions";
            string path = Path.Combine(basePath, _tenLop);
            fbd.SelectedPath = path;
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtDirectory.Text = fbd.SelectedPath;
            }
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
