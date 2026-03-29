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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Diagnostics;

namespace TeachingManagement
{
    public partial class LopHocForm : Form
    {
        private Form newForm;

        public LopHocForm()
        {
            InitializeComponent();

            dgvClasses.MultiSelect = false;
            dgvClasses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClasses.ReadOnly = true;
            dgvClasses.AllowUserToAddRows = false;
            dgvClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LopHocForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            loadData();
            AddActionColumns();
        }

        private void loadData() {
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            string query = @"
                SELECT 
                    lh.LopHocId AS 'Mã lớp',
                    lh.TenLop AS 'Tên lớp',
                    lh.MoTa AS 'Mô tả',
                    lh.HocKy AS 'Học kỳ',
                    lh.NamHoc AS 'Năm học',
                    COUNT(lsv.SinhVienId) AS 'Số SV'
                FROM LopHoc lh
                LEFT JOIN LopHoc_SinhVien lsv
                    ON lh.LopHocId = lsv.LopHocId
                JOIN SinhVien sv ON lsv.SinhVienId = sv.SinhVienId AND sv.TrangThai = 1
                WHERE lh.GiangVienId = @GiangVienId
                AND lh.TrangThai = 1
                GROUP BY 
                    lh.LopHocId,
                    lh.TenLop,
                    lh.MoTa,
                    lh.HocKy,
                    lh.NamHoc
                ORDER BY lh.LopHocId DESC;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvClasses.DataSource = dt;

                dgvClasses.AutoGenerateColumns = false;

                dgvClasses.Columns["Mã lớp"].Visible = false;
                //dgvClasses.Columns["Tên lớp"].FillWeight = 60;
                dgvClasses.Columns["Mô tả"].FillWeight = 150;
                dgvClasses.Columns["Học kỳ"].FillWeight = 60;
                dgvClasses.Columns["Năm học"].FillWeight = 70;
                dgvClasses.Columns["Số SV"].FillWeight = 60;

            }
        }

        private void dgvClasses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dgvClasses.Columns[e.ColumnIndex].Name;
            int lopHocId = Convert.ToInt32(dgvClasses.CurrentRow.Cells["Mã lớp"].Value);

            if (columnName == "View")
            {
                OpenDetailForm(lopHocId);
            }
            else if (columnName == "Edit")
            {
                OpenEditForm(lopHocId);
            }
            else if (columnName == "Delete")
            {
                DeleteLopHoc(lopHocId);
            }
            else if (columnName == "Export")
            {
                ExportToExcel(lopHocId);
            }
            else if (columnName == "AddBaiTap")
            {
                FrmBaiTapAdd frm = new FrmBaiTapAdd(lopHocId);
                frm.ShowDialog();
                loadData();
            }
        }

        private void AddActionColumns()
        {
            DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
            btnView.Name = "View";
            btnView.HeaderText = "";
            btnView.Text = "Xem";
            btnView.UseColumnTextForButtonValue = true;
            btnView.FillWeight = 40;
            dgvClasses.Columns.Add(btnView);

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.Name = "Edit";
            btnEdit.HeaderText = "";
            btnEdit.Text = "Sửa";
            btnEdit.UseColumnTextForButtonValue = true;
            btnEdit.FillWeight = 40; 
            dgvClasses.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = "";
            btnDelete.Text = "Xóa";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.FillWeight = 40;
            dgvClasses.Columns.Add(btnDelete);

            DataGridViewButtonColumn btnExport = new DataGridViewButtonColumn();
            btnExport.Name = "Export";
            btnExport.HeaderText = "";
            btnExport.Text = "Xuất";
            btnExport.UseColumnTextForButtonValue = true;
            btnExport.FillWeight = 40;
            dgvClasses.Columns.Add(btnExport);

            DataGridViewButtonColumn btnAddBaiTap = new DataGridViewButtonColumn();
            btnAddBaiTap.Name = "AddBaiTap";
            btnAddBaiTap.HeaderText = "";
            btnAddBaiTap.Text = "Thêm BT";
            btnAddBaiTap.UseColumnTextForButtonValue = true;
            btnAddBaiTap.FillWeight = 60;
            dgvClasses.Columns.Add(btnAddBaiTap);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmLopHocEdit frm = new FrmLopHocEdit(0);
            frm.ShowDialog();
            loadData();
        }

        private void OpenDetailForm(int lopHocId)
        {
            FrmLopHocDetail frm = new FrmLopHocDetail(lopHocId);
            frm.ShowDialog();   // mở dạng modal
            loadData(); // reload lại danh sách
        }

        private void OpenEditForm(int lopHocId)
        {
            FrmLopHocEdit frm = new FrmLopHocEdit(lopHocId);
            frm.ShowDialog();   // mở dạng modal
            loadData(); // reload lại danh sách
        }

        private void DeleteLopHoc(int lopHocId)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
                string query = "UPDATE LopHoc SET TrangThai = 0 WHERE LopHocId = @Id AND GiangVienId = @GiangVienId";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", lopHocId);
                        cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa lớp thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy lớp hoặc bạn không có quyền xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            loadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToExcel(int lopHocId)
        {
            string query = @"
                SELECT sv.MaSinhVien, sv.HoTen, sv.Email, lh.TenLop
                FROM LopHoc lh
                JOIN LopHoc_SinhVien lsv ON lh.LopHocId = lsv.LopHocId
                JOIN SinhVien sv ON sv.SinhVienId = lsv.SinhVienId
                WHERE lh.LopHocId = @LopHocId
                AND lh.GiangVienId = @GiangVienId
                AND sv.TrangThai = 1
                ORDER BY sv.HoTen";

            DataTable dt = new DataTable();

            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LopHocId", lopHocId);
                    cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Lớp chưa có sinh viên!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Đặt filter để chỉ cho phép lưu file .xlsx
            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
            string tenLop = dgvClasses.CurrentRow.Cells["Tên lớp"].Value.ToString();
            saveFileDialog.FileName = tenLop + ".xlsx";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            // ✅ API mới của EPPlus 8+
            ExcelPackage.License.SetNonCommercialPersonal("LMS Giang Vien");

            using (ExcelPackage package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("DanhSach");

                // Header
                ws.Cells["A1"].Value = "STT";
                ws.Cells["B1"].Value = "Mã SV";
                ws.Cells["C1"].Value = "Họ";
                ws.Cells["D1"].Value = "Tên";
                ws.Cells["E1"].Value = "Email";
                ws.Cells["F1"].Value = "Tên lớp";

                using (var header = ws.Cells["A1:F1"])
                {
                    header.Style.Font.Bold = true;
                    header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    header.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string maSV = dt.Rows[i]["MaSinhVien"].ToString();
                    string hoTen = dt.Rows[i]["HoTen"].ToString().Trim();
                    string email = dt.Rows[i]["Email"].ToString();
                    tenLop = dt.Rows[i]["TenLop"].ToString();

                    string[] parts = hoTen.Split(' ');
                    string ten = parts.Last();
                    string ho = parts.Length > 1
                                ? string.Join(" ", parts.Take(parts.Length - 1))
                                : "";

                    ws.Cells[i + 2, 1].Value = i + 1;
                    ws.Cells[i + 2, 2].Value = maSV;
                    ws.Cells[i + 2, 3].Value = ho;
                    ws.Cells[i + 2, 4].Value = ten;
                    ws.Cells[i + 2, 5].Value = email;
                    ws.Cells[i + 2, 6].Value = tenLop;
                }

                ws.Cells.AutoFitColumns();
                
                File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                try
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = saveFileDialog.FileName,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Xảy ra lỗi khi mở file Excel!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            loadData();
            txtSearch.Clear();
            //AddActionColumns();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // Tìm theo tên lớp & mô tả
            string searchTerm = txtSearch.Text.Trim();
            string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }
            string query = @"
                SELECT 
                    lh.LopHocId AS 'Mã lớp',
                    lh.TenLop AS 'Tên lớp',
                    lh.MoTa AS 'Mô tả',
                    lh.HocKy AS 'Học kỳ',
                    lh.NamHoc AS 'Năm học',
                    COUNT(lsv.SinhVienId) AS 'Số SV'
                FROM LopHoc lh
                LEFT JOIN LopHoc_SinhVien lsv
                    ON lh.LopHocId = lsv.LopHocId
                WHERE lh.GiangVienId = @GiangVienId
                AND lh.TrangThai = 1
                AND (lh.TenLop LIKE @SearchTerm OR lh.MoTa LIKE @SearchTerm)
                GROUP BY 
                    lh.LopHocId,
                    lh.TenLop,
                    lh.MoTa,
                    lh.HocKy,
                    lh.NamHoc
                ORDER BY lh.LopHocId DESC";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                    cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvClasses.DataSource = dt;
                    dgvClasses.AutoGenerateColumns = false;
                    dgvClasses.Columns["Mã lớp"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
