using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeachingManagement
{
    public partial class SinhVienForm : Form
    {
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public SinhVienForm()
        {
            InitializeComponent();

            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SinhVienForm_Load(object sender, EventArgs e)
        {
            loadData();
            addActionColumns();
        }

        private void loadData()
        {
            string query = @"
                SELECT DISTINCT
                    sv.MaSinhVien AS 'Mã SV',
                    sv.HoTen AS 'Họ tên',
                    sv.Email AS 'Email'
                FROM SinhVien sv
                JOIN LopHoc_SinhVien lsv ON sv.SinhVienId = lsv.SinhVienId
                JOIN LopHoc lh ON lh.LopHocId = lsv.LopHocId
                WHERE lh.GiangVienId = @GiangVienId
                AND sv.TrangThai = 1
                ORDER BY SUBSTRING_INDEX(sv.HoTen, ' ', -1), sv.HoTen;";
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                    using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvSinhVien.DataSource = dt;

                        dgvSinhVien.AutoGenerateColumns = false;
                        dgvSinhVien.Columns["Mã SV"].FillWeight = 60;
                        dgvSinhVien.Columns["Họ tên"].FillWeight = 180;
                        dgvSinhVien.Columns["Email"].FillWeight = 150;
                    }
                }
            }
        }

        private void addActionColumns()
        {
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.Name = "btnEdit";
            btnEdit.HeaderText = "";
            btnEdit.Text = "Sửa";
            btnEdit.UseColumnTextForButtonValue = true;
            btnEdit.FillWeight = 60;
            dgvSinhVien.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "btnDelete";
            btnDelete.HeaderText = "";
            btnDelete.Text = "Xóa";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.FillWeight = 60;
            dgvSinhVien.Columns.Add(btnDelete);
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dgvSinhVien.Columns[e.ColumnIndex].Name;
            string MaSinhVien = dgvSinhVien.Rows[e.RowIndex].Cells["Mã SV"].Value.ToString();
            if (columnName == "btnEdit")
            {
                OpenEditForm(MaSinhVien);
            }
            else if (columnName == "btnDelete")
            {
                DialogResult rs = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sinh viên {MaSinhVien} không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (rs != DialogResult.Yes) {
                    return;
                }
                
                deleteSinhVienTrongLopHoc(MaSinhVien);
                deleteMaSinhVien(MaSinhVien);
                
            }
        }

        private void OpenEditForm(string MaSinhVien)
        {
            FrmSinhVienEdit frm = new FrmSinhVienEdit(MaSinhVien);
            frm.ShowDialog();
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string filePath = ofd.FileName;
            ImportSinhVienFromExcel(filePath);
            loadData();
        }

        private void deleteMaSinhVien(string MaSinhVien)
        {
            string query = "UPDATE SinhVien SET TrangThai = 0 WHERE MaSinhVien = @MaSinhVien";
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSinhVien", MaSinhVien);
                    cmd.ExecuteNonQuery();
                }
            }
            loadData();
        }

        private void deleteSinhVienTrongLopHoc(string MaSinhVien)
        {
            string query = @" DELETE lsv
                FROM LopHoc_SinhVien lsv
                INNER JOIN SinhVien sv ON lsv.SinhVienId = sv.SinhVienId
                WHERE sv.MaSinhVien = @MaSinhVien;";
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSinhVien", MaSinhVien);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Đã xóa sinh viên khỏi lớp học.");
                            loadData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên hoặc đã xóa trước đó.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sinh viên khỏi lớp học: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSinhVienAdd frm = new FrmSinhVienAdd();
            frm.ShowDialog();
            loadData();
        }

        public void ImportSinhVienFromExcel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file Excel.");
                return;
            }

            int successCount = 0;
            int skipCount = 0;
            int errorCount = 0;

            StringBuilder logBuilder = new StringBuilder();

            void WriteLog(string status, string message)
            {
                logBuilder.AppendLine(
                    $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}: [{status}] {message}");
            }

            try
            {
                // EPPlus 8+ license
                ExcelPackage.License.SetNonCommercialPersonal("LMS Import Tool");

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var ws = package.Workbook.Worksheets[0];
                    int rowCount = ws.Dimension.Rows;

                    using (var conn = new MySqlConnection(connStr))
                    {
                        conn.Open();
                        using (var tran = conn.BeginTransaction())
                        {
                            try
                            {
                                WriteLog("Info", $"Bắt đầu import file {Path.GetFileName(filePath)}");

                                for (int row = 2; row <= rowCount; row++)
                                {
                                    try
                                    {
                                        string maSV = ws.Cells[row, 2].Text.Trim();
                                        string hoDem = ws.Cells[row, 3].Text.Trim();
                                        string ten = ws.Cells[row, 4].Text.Trim();
                                        string email = ws.Cells[row, 5].Text.Trim();
                                        string lop = ws.Cells[row, 6].Text.Trim();

                                        if (string.IsNullOrEmpty(maSV))
                                        {
                                            WriteLog("Skipped", $"Dòng {row} Mã sinh viên rỗng -> Bỏ qua");
                                            skipCount++;
                                            continue;
                                        }

                                        string hoTen = hoDem + " " + ten;

                                        string checkSql = "SELECT COUNT(*) FROM SinhVien WHERE MaSinhVien = @MaSV";
                                        using (var checkCmd = new MySqlCommand(checkSql, conn, tran))
                                        {
                                            checkCmd.Parameters.AddWithValue("@MaSV", maSV);
                                            int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                                            if (exists > 0)
                                            {
                                                WriteLog("Skipped", $"{maSV} Tồn tại -> Bỏ qua");
                                                skipCount++;
                                                continue;
                                            }
                                        }

                                        string insertSql = @"INSERT INTO SinhVien
                                                     (MaSinhVien, HoTen, Email, TenLop)
                                                     VALUES (@MaSV, @HoTen, @Email, @Lop)";

                                        using (var cmd = new MySqlCommand(insertSql, conn, tran))
                                        {
                                            cmd.Parameters.AddWithValue("@MaSV", maSV);
                                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                            cmd.Parameters.AddWithValue("@Email", email);
                                            cmd.Parameters.AddWithValue("@Lop", lop);

                                            cmd.ExecuteNonQuery();
                                        }

                                        WriteLog("Success", $"{maSV} {hoTen} -> Thêm thành công");
                                        successCount++;
                                    }
                                    catch (Exception exRow)
                                    {
                                        WriteLog("Error", $"Dòng {row} -> {exRow.Message}");
                                        errorCount++;
                                    }
                                }

                                tran.Commit();
                                WriteLog("Info", "Commit transaction thành công");
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                WriteLog("Error", "Rollback transaction -> " + ex.Message);
                                errorCount++;
                            }
                        }
                    }
                }

                // ===== GHI FILE LOG =====
                string logFolder = Path.Combine(Application.StartupPath, "log");
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                string logFileName = $"studentAddResult_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string fullPath = Path.Combine(logFolder, logFileName);

                File.WriteAllText(fullPath, logBuilder.ToString(), Encoding.UTF8);

                // ===== MESSAGEBOX =====
                var result = MessageBox.Show(
                    $"Import hoàn tất.\n\n" +
                    $"Thêm thành công: {successCount}\n" +
                    $"Bỏ qua: {skipCount}\n" +
                    $"Lỗi: {errorCount}\n\n" +
                    $"Nhấn YES để mở file log.",
                    "Kết quả import",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", fullPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }
            string sql = @"SELECT MaSinhVien AS 'Mã SV', HoTen AS 'Họ tên', Email 
                FROM SinhVien 
                WHERE TrangThai = 1
                AND (HoTen LIKE @Search OR MaSinhVien LIKE @Search OR Email LIKE @Search)";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvSinhVien.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
    }
}
