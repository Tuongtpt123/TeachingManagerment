using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

namespace TeachingManagement
{
    public partial class FrmLopHocEdit : Form
    {
        private int _lopHocId;
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public FrmLopHocEdit()
        {
            InitializeComponent();

            txtSinhVienSearch.KeyDown += txtSinhVienSearch_KeyDown;
        }

        public FrmLopHocEdit(int lopHocId)
        {
            InitializeComponent();

            _lopHocId = lopHocId;

            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FrmLopHocEdit_Load(object sender, EventArgs e)
        {
            if (_lopHocId > 0)
            {
                loadData();
            }
            loadSinhVienData();
            addActionColumns();
        }

        private void loadData()
        {
            string query = @"
                SELECT 
                    lh.TenLop,
                    lh.MoTa,
                    lh.HocKy,
                    lh.NamHoc,
                    COUNT(sv.SinhVienId) AS SoLuongSinhVien
                FROM LopHoc lh
                LEFT JOIN LopHoc_SinhVien lsv 
                    ON lh.LopHocId = lsv.LopHocId
                LEFT JOIN SinhVien sv
                    ON lsv.SinhVienId = sv.SinhVienId
                    AND sv.TrangThai = 1
                WHERE lh.LopHocId = @Id
                AND lh.GiangVienId = @GiangVienId
                GROUP BY lh.LopHocId;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", _lopHocId);
                    cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTenLop.Text = reader["TenLop"].ToString();
                            txtMoTa.Text = reader["MoTa"].ToString();
                            int hocKy = reader["HocKy"] != DBNull.Value ? Convert.ToInt32(reader["HocKy"]) : 0;
                            cbxHocKy.SelectedIndex = hocKy >= 0 && hocKy < cbxHocKy.Items.Count
                                ? hocKy
                                : 0;
                            txtNamHoc.Text = reader["NamHoc"].ToString();
                            lblTongSinhVien.Text = reader["SoLuongSinhVien"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy lớp học hoặc bạn không có quyền truy cập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadSinhVienData()
        {
            string sql = @"SELECT sv.MaSinhVien AS 'Mã SV', sv.HoTen AS 'Họ và tên', sv.Email AS 'Email'
                FROM SinhVien sv
                JOIN LopHoc_SinhVien lsv ON sv.SinhVienId = lsv.SinhVienId
                WHERE lsv.LopHocId = @LopHocId
                AND sv.TrangThai = 1
                ORDER BY SUBSTRING_INDEX(sv.HoTen, ' ', -1), sv.HoTen;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        
                        dgvSinhVien.DataSource = dt;
                        

                    }
                    dgvSinhVien.AutoGenerateColumns = false;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Lỗi khi tải danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchSinhVien();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
            loadSinhVienData();
        }

        private void addActionColumns()
        {
            if (!dgvSinhVien.Columns.Contains("Remove"))
            {
                DataGridViewButtonColumn removeBtn = new DataGridViewButtonColumn();
                removeBtn.Name = "Remove";
                removeBtn.HeaderText = "Hành động";
                removeBtn.Text = "Xóa";
                removeBtn.UseColumnTextForButtonValue = true;
                dgvSinhVien.Columns.Add(removeBtn);
            }
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maSinhVien = dgvSinhVien.Rows[e.RowIndex].Cells["Mã SV"].Value.ToString();
            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "Remove")
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa sinh viên {maSinhVien} khỏi lớp học?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string sql = @"DELETE FROM LopHoc_SinhVien 
                        WHERE LopHocId = @LopHocId 
                        AND SinhVienId = (SELECT SinhVienId FROM SinhVien WHERE MaSinhVien = @MaSinhVien);";
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connStr))
                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                            cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Đã xóa sinh viên khỏi lớp học.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData();
                                loadSinhVienData();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sinh viên hoặc đã xảy ra lỗi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnConfirmChange_Click(object sender, EventArgs e)
        {
            if (_lopHocId > 0) {
                confirmChange_Update();
            }
            else if (_lopHocId == 0) 
            {
                confirmChange_AddNew();
            }
        }

        private void confirmChange_AddNew()
        {
            string tenLop = txtTenLop.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            int hocKy = cbxHocKy.SelectedIndex;
            string namHoc = txtNamHoc.Text.Trim();

            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Tên lớp không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus();
                return;
            }

            if (hocKy <= 0)
            {
                MessageBox.Show("Vui lòng chọn học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxHocKy.Focus();
                return;
            }

            if (string.IsNullOrEmpty(namHoc))
            {
                MessageBox.Show("Năm học không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamHoc.Focus();
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // 1️⃣ Kiểm tra lớp đã tồn tại chưa
                    string checkQuery = @"SELECT LopHocId, TrangThai 
                                  FROM LopHoc 
                                  WHERE TenLop = @TenLop 
                                  AND HocKy = @HocKy 
                                  AND NamHoc = @NamHoc
                                  AND GiangVienId = @GiangVienId
                                  LIMIT 1";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenLop", tenLop);
                        checkCmd.Parameters.AddWithValue("@HocKy", hocKy);
                        checkCmd.Parameters.AddWithValue("@NamHoc", namHoc);
                        checkCmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                        using (MySqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int lopHocId = reader.GetInt32("LopHocId");
                                int trangThai = reader.GetInt32("TrangThai");

                                reader.Close();

                                // Lớp đang hoạt động
                                if (trangThai == 1)
                                {
                                    MessageBox.Show("Lớp học này đã tồn tại.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                // Lớp đã bị "xóa mềm"
                                else
                                {
                                    DialogResult rs = MessageBox.Show(
                                        "Lớp học này đã tồn tại nhưng đang bị vô hiệu hóa.\nBạn có muốn kích hoạt lại lớp này cùng các thông tin mới?",
                                        "Khôi phục lớp học",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

                                    if (rs == DialogResult.Yes)
                                    {
                                        string updateQuery = @"UPDATE LopHoc 
                                                       SET MoTa = @MoTa,
                                                           TrangThai = 1
                                                       WHERE LopHocId = @LopHocId";

                                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                        {
                                            updateCmd.Parameters.AddWithValue("@MoTa", moTa);
                                            updateCmd.Parameters.AddWithValue("@LopHocId", lopHocId);
                                            updateCmd.ExecuteNonQuery();
                                        }

                                        MessageBox.Show("Đã kích hoạt lại lớp học.", "Thành công",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        _lopHocId = lopHocId;
                                        loadData();
                                        loadSinhVienData();
                                    }

                                    return;
                                }
                            }
                        }
                    }

                    // 2️⃣ Nếu chưa tồn tại → tạo lớp mới
                    DialogResult result = MessageBox.Show(
                        "Bạn có chắc tạo lớp học này?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                        return;

                    string insertQuery = @"INSERT INTO LopHoc 
                                   (TenLop, MoTa, HocKy, NamHoc, GiangVienId) 
                                   VALUES (@TenLop, @MoTa, @HocKy, @NamHoc, @GiangVienId);
                                   SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenLop", tenLop);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@HocKy", hocKy);
                        cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                        cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                        object resultObj = cmd.ExecuteScalar();

                        if (resultObj != null)
                        {
                            int newLopHocId = Convert.ToInt32(resultObj);

                            _lopHocId = newLopHocId;

                            // 🔹 Tạo folder lớp học
                            CreateClassFolder(tenLop);

                            MessageBox.Show("Lớp học đã được tạo thành công.",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            loadData();
                            loadSinhVienData();
                        }
                        else
                        {
                            MessageBox.Show("Không thể tạo lớp học mới.",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo lớp học: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void confirmChange_Update()
        {
            string tenLop = txtTenLop.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            int hocKy = cbxHocKy.SelectedIndex;
            string namHoc = txtNamHoc.Text.Trim();

            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Tên lớp không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus();
                return;
            }

            if (hocKy <= 0)
            {
                MessageBox.Show("Vui lòng chọn học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxHocKy.Focus();
                return;
            }

            if (string.IsNullOrEmpty(namHoc))
            {
                MessageBox.Show("Năm học không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamHoc.Focus();
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn lưu thay đổi?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // 1️⃣ Lấy tên lớp cũ
                    string oldTenLop = "";

                    string getOldNameSql = @"SELECT TenLop FROM LopHoc 
                                     WHERE LopHocId = @LopHocId 
                                     AND GiangVienId = @GiangVienId";

                    using (MySqlCommand getCmd = new MySqlCommand(getOldNameSql, conn))
                    {
                        getCmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                        getCmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                        object obj = getCmd.ExecuteScalar();

                        if (obj != null)
                            oldTenLop = obj.ToString();
                        else
                        {
                            MessageBox.Show("Không tìm thấy lớp học.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 2️⃣ Update DB
                    string sql = @"UPDATE LopHoc 
                           SET TenLop = @TenLop, MoTa = @MoTa, HocKy = @HocKy, NamHoc = @NamHoc
                           WHERE LopHocId = @LopHocId AND GiangVienId = @GiangVienId;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenLop", tenLop);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@HocKy", hocKy);
                        cmd.Parameters.AddWithValue("@NamHoc", namHoc);
                        cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                        cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // 3️⃣ Nếu tên lớp thay đổi → rename folder
                            if (!oldTenLop.Equals(tenLop, StringComparison.OrdinalIgnoreCase))
                            {
                                RenameClassFolder(oldTenLop, tenLop);
                            }

                            MessageBox.Show(
                                "Lưu thay đổi thành công.",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            loadData();
                            loadSinhVienData();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Không tìm thấy lớp học hoặc bạn không có quyền chỉnh sửa.",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            loadData();
                            loadSinhVienData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi lưu thay đổi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CreateClassFolder(string tenLop)
        {
            try
            {
                string basePath = @"G:\My Drive\LMS_Submissions";
                string folderPath = Path.Combine(basePath, tenLop);

                // Kiểm tra thư mục gốc tồn tại
                if (!Directory.Exists(basePath))
                {
                    throw new Exception("Không tìm thấy thư mục LMS_Submissions trên Google Drive.");
                }

                // Kiểm tra trùng tên folder
                if (Directory.Exists(folderPath))
                {
                    MessageBox.Show(
                        $"Folder '{tenLop}' đã tồn tại trong LMS_Submissions.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                Directory.CreateDirectory(folderPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tạo folder lớp học trên Google Drive.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void RenameClassFolder(string oldName, string newName)
        {
            try
            {
                string basePath = @"G:\My Drive\LMS_Submissions";

                string oldPath = Path.Combine(basePath, oldName);
                string newPath = Path.Combine(basePath, newName);

                if (!Directory.Exists(basePath))
                    throw new Exception("Không tìm thấy thư mục LMS_Submissions.");

                // Nếu folder cũ không tồn tại
                if (!Directory.Exists(oldPath))
                {
                    MessageBox.Show(
                        $"Không tìm thấy folder lớp '{oldName}' để đổi tên.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Nếu folder mới đã tồn tại
                if (Directory.Exists(newPath))
                {
                    MessageBox.Show(
                        $"Đã tồn tại folder '{newName}'. Không thể đổi tên.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                Directory.Move(oldPath, newPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể đổi tên folder lớp học.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void searchSinhVien()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }
            string sql = @"SELECT 
                                sv.MaSinhVien AS 'Mã SV', 
                                sv.HoTen AS 'Họ và Tên', 
                                sv.Email
                            FROM SinhVien sv
                            JOIN LopHoc_SinhVien lsv ON sv.SinhVienId = lsv.SinhVienId
                            WHERE lsv.LopHocId = @LopHocId
                            AND sv.TrangThai = 1
                            AND (sv.HoTen LIKE @Search OR sv.MaSinhVien LIKE @Search OR sv.Email LIKE @Search)
                            ORDER BY SUBSTRING_INDEX(sv.HoTen, ' ', -1), sv.HoTen;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                    cmd.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvSinhVien.AutoGenerateColumns = true;
                        dgvSinhVien.DataSource = dt;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (_lopHocId <= 0)
            {
                MessageBox.Show("Vui lòng tạo lớp học trước khi thêm sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            checkSinhVien_Update();
        }

        private void checkSinhVien_Update()
        {
            WriteLog("Bắt đầu kiểm tra mã sinh viên", LogStatus.Info);

            string text = txtSinhVienSearch.Text.Trim();
            string maSinhVien = text.Split(' ')[0];

            if (string.IsNullOrEmpty(maSinhVien))
            {
                WriteLog("Mã sinh viên rỗng", LogStatus.Error);
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSinhVienSearch.Focus();
                return;
            }

            WriteLog($"Mã sinh viên nhập vào: {maSinhVien}", LogStatus.Info);

            string sql = @"SELECT COUNT(*) FROM SinhVien 
                   WHERE MaSinhVien = @MaSinhVien AND TrangThai = 1;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                try
                {
                    WriteLog("Mở kết nối database để kiểm tra sinh viên", LogStatus.Info);
                    conn.Open();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    WriteLog($"Kết quả kiểm tra COUNT = {count}", LogStatus.Info);

                    if (count <= 0)
                    {
                        WriteLog("Mã sinh viên không tồn tại hoặc đã bị vô hiệu hóa", LogStatus.Error);
                        MessageBox.Show("Mã sinh viên không tồn tại hoặc đã bị vô hiệu hóa.", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    WriteLog("Mã sinh viên hợp lệ", LogStatus.Success);

                    if (isSinhVienExistClass(maSinhVien))
                    {
                        WriteLog("Sinh viên đã tồn tại trong lớp.", LogStatus.Info);
                        MessageBox.Show("Sinh viên đã tồn tại trong lớp.", "Thông báo");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("Lỗi khi kiểm tra mã sinh viên: " + ex.Message, LogStatus.Error);
                    MessageBox.Show("Lỗi khi kiểm tra mã sinh viên: " + ex.Message,
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult result = MessageBox.Show(
                "Mã sinh viên hợp lệ. Bạn có muốn thêm sinh viên này vào lớp học?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                WriteLog("Người dùng hủy thao tác thêm sinh viên", LogStatus.Info);
                return;
            }

            WriteLog("Bạn xác nhận thêm sinh viên", LogStatus.Info);

            string sqlInsert = @"INSERT INTO LopHoc_SinhVien (LopHocId, SinhVienId) 
                                    VALUES (@LopHocId, 
                                           (SELECT SinhVienId FROM SinhVien WHERE MaSinhVien = @MaSinhVien));";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(sqlInsert, conn))
                {
                    cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                    WriteLog("Mở kết nối database để thêm sinh viên vào lớp", LogStatus.Info);
                    conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    WriteLog($"Kết quả INSERT rowsAffected = {rowsAffected}", LogStatus.Info);

                    if (rowsAffected > 0)
                    {
                        WriteLog("Thêm sinh viên vào lớp thành công", LogStatus.Success);

                        MessageBox.Show("Đã thêm sinh viên vào lớp học.", "Thành công",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSinhVienSearch.Clear();
                        loadData();
                        loadSinhVienData();
                    }
                    else
                    {
                        WriteLog("INSERT không thành công: Sinh viên đã tồn tại trong lớp!", LogStatus.Error);
                        MessageBox.Show("Đã xảy ra lỗi.", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("Lỗi khi thêm sinh viên: " + ex.Message, LogStatus.Error);
                MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            WriteLog("Kết thúc quá trình thêm sinh viên", LogStatus.Info);
        }

        private bool isSinhVienExistClass(string maSinhVien)
        {
            string sqlCheckExist = @"SELECT COUNT(*) 
                         FROM LopHoc_SinhVien lhs
                         JOIN SinhVien sv ON sv.SinhVienId = lhs.SinhVienId
                         WHERE lhs.LopHocId = @LopHocId
                         AND sv.MaSinhVien = @MaSinhVien;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                using (MySqlCommand cmd = new MySqlCommand(sqlCheckExist, conn))
                {
                    cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                    conn.Open();

                    int exist = Convert.ToInt32(cmd.ExecuteScalar());

                    if (exist > 0)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch
            {
                throw new Exception("Lỗi kết nối cơ sở dữ liệu khi cố kiểm tra sinh viên tồn tại trong lớp.");
            }
        }

        private void WriteLog(string message, LogStatus status)
        {
            string time = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            string logLine = $"[{time}] [{status.ToString().ToUpper()}] {message}\n";

            Color logColor = Color.White;

            switch (status)
            {
                case LogStatus.Info:
                    logColor = Color.DeepSkyBlue;
                    break;
                case LogStatus.Success:
                    logColor = Color.LimeGreen;
                    break;
                case LogStatus.Error:
                    logColor = Color.Red;
                    break;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;

            rtbLog.SelectionColor = logColor;
            rtbLog.AppendText(logLine);

            rtbLog.SelectionColor = rtbLog.ForeColor;
            rtbLog.ScrollToCaret();
        }

        private void btnLoadSinhVienTuExcel_Click(object sender, EventArgs e)
        {
            if (_lopHocId <= 0)
            {
                MessageBox.Show("Vui lòng tạo lớp học trước khi import sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string filePath = ofd.FileName;

            ImportExcel(filePath);
        }

        public void ImportExcel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                WriteLog("Không tìm thấy file Excel.", LogStatus.Error);
                return;
            }

            WriteLog("Bắt đầu import file: " + Path.GetFileName(filePath), LogStatus.Info);

            try
            {
                // EPPlus 8+ license
                ExcelPackage.License.SetNonCommercialPersonal("LMS Import Tool");

                int addedToClass = 0;
                int createdStudent = 0;
                int existedInClass = 0;
                int invalidRow = 0;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var ws = package.Workbook.Worksheets[0];
                    if (ws == null)
                    {
                        WriteLog("Không tìm thấy worksheet.", LogStatus.Error);
                        return;
                    }

                    if (ws.Dimension == null)
                    {
                        WriteLog("File không có dữ liệu.", LogStatus.Error);
                        return;
                    }

                    int rowCount = ws.Dimension.Rows;

                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        conn.Open();

                        using (MySqlTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                for (int row = 2; row <= rowCount; row++)
                                {
                                    string maSV = ws.Cells[row, 2].Text.Trim();
                                    string ho = ws.Cells[row, 3].Text.Trim();
                                    string ten = ws.Cells[row, 4].Text.Trim();
                                    string email = ws.Cells[row, 5].Text.Trim();

                                    string hoTen = (ho + " " + ten).Trim();

                                    if (string.IsNullOrWhiteSpace(maSV) ||
                                        string.IsNullOrWhiteSpace(hoTen))
                                    {
                                        invalidRow++;
                                        WriteLog($"Dòng {row}: Thiếu Mã SV hoặc Họ tên.", LogStatus.Error);
                                        continue;
                                    }

                                    int sinhVienId;

                                    // 1️⃣ Kiểm tra tồn tại
                                    int? existingId = GetSinhVienId(conn, trans, maSV);

                                    if (existingId == null)
                                    {
                                        sinhVienId = InsertSinhVien(conn, trans, maSV, hoTen, email);
                                        createdStudent++;
                                        WriteLog($"Tạo mới sinh viên {maSV} - {hoTen}.", LogStatus.Success);
                                    }
                                    else
                                    {
                                        sinhVienId = existingId.Value;
                                        WriteLog($"Sinh viên {maSV} đã tồn tại.", LogStatus.Info);
                                    }

                                    // 2️⃣ Kiểm tra đã thuộc lớp chưa
                                    if (IsStudentInClass(conn, trans, _lopHocId, sinhVienId))
                                    {
                                        existedInClass++;
                                        WriteLog($"Sinh viên {maSV} đã thuộc lớp.", LogStatus.Info);
                                        continue;
                                    }

                                    // 3️⃣ Thêm vào lớp
                                    InsertStudentToClass(conn, trans, _lopHocId, sinhVienId);
                                    addedToClass++;
                                    WriteLog($"Thêm {maSV} vào lớp thành công.", LogStatus.Success);
                                }

                                trans.Commit();
                                WriteLog("Import hoàn tất và đã commit dữ liệu.", LogStatus.Success);
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                WriteLog("Lỗi khi import: " + ex.Message, LogStatus.Error);
                                return;
                            }
                        }

                        WriteLog(
                            $"Tổng kết: Thêm vào lớp = {addedToClass}, " +
                            $"Tạo mới = {createdStudent}, " +
                            $"Đã thuộc lớp = {existedInClass}, " +
                            $"Dòng lỗi = {invalidRow}",
                            LogStatus.Info
                        );

                        loadSinhVienData();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("Lỗi hệ thống: " + ex.Message, LogStatus.Error);
            }
        }

        private int? GetSinhVienId(MySqlConnection conn, MySqlTransaction trans, string maSV)
        {
            string query = @"SELECT SinhVienId 
                            FROM SinhVien 
                            WHERE MaSinhVien = @MaSinhVien
                            AND TrangThai = 1
                            LIMIT 1;";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaSinhVien", maSV);
                object result = cmd.ExecuteScalar();

                if (result == null)
                    return null;

                return Convert.ToInt32(result);
            }
        }

        private int InsertSinhVien(MySqlConnection conn, MySqlTransaction trans,
                           string maSV, string hoTen, string email)
        {
            string query = @"INSERT INTO SinhVien (MaSinhVien, HoTen, Email)
                     VALUES (@MaSinhVien, @HoTen, @Email);
                     SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSV);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@Email", email);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                WriteLog("Lỗi khi tạo sinh viên mới: " + ex.Message, LogStatus.Error);
                throw; // Rethrow để rollback transaction
            }
        }

        private bool IsStudentInClass(MySqlConnection conn, MySqlTransaction trans,
                              int lopHocId, int sinhVienId)
        {
            string query = @"SELECT COUNT(*)
                     FROM LopHoc_SinhVien
                     WHERE LopHocId = @LopHocId
                     AND SinhVienId = @SinhVienId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@LopHocId", lopHocId);
                cmd.Parameters.AddWithValue("@SinhVienId", sinhVienId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void InsertStudentToClass(MySqlConnection conn, MySqlTransaction trans,
                                  int lopHocId, int sinhVienId)
        {
            string query = @"INSERT INTO LopHoc_SinhVien (LopHocId, SinhVienId)
                     VALUES (@LopHocId, @SinhVienId)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@LopHocId", lopHocId);
                cmd.Parameters.AddWithValue("@SinhVienId", sinhVienId);

                cmd.ExecuteNonQuery();
            }
        }

        private void txtMaSinhVien_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSinhVienSearch.Text.Trim();

            if (keyword.Length < 1)
            {
                lstSuggestSV.Visible = false;
                return;
            }

            var data = SearchSinhVien(keyword);

            lstSuggestSV.Items.Clear();

            foreach (var item in data)
            {
                lstSuggestSV.Items.Add(item);
            }

            lstSuggestSV.Visible = data.Count > 0;

            if (lstSuggestSV.Items.Count > 0)
            {
                lstSuggestSV.SelectedIndex = 0;
            }
        }

        private List<string> SearchSinhVien(string keyword)
        {
            List<string> result = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string sql = @"SELECT MaSinhVien, HoTen
                       FROM SinhVien
                       WHERE TrangThai = 1
                       AND (MaSinhVien LIKE @key OR HoTen LIKE @key)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item =
                                reader["MaSinhVien"].ToString() +
                                " - " +
                                reader["HoTen"].ToString();

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        private void lstSuggestSV_Click(object sender, EventArgs e)
        {
            if (lstSuggestSV.SelectedItem != null)
            {
                txtSinhVienSearch.Text = lstSuggestSV.SelectedItem.ToString();
                lstSuggestSV.Visible = false;
            }
        }

        private void txtSinhVienSearch_Leave(object sender, EventArgs e)
        {
            if (!lstSuggestSV.Focused)
            {
                lstSuggestSV.Visible = false;
            }
        }

        private void txtSinhVienSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lstSuggestSV.Visible || lstSuggestSV.Items.Count == 0)
                return;

            // Nhấn mũi tên xuống
            if (e.KeyCode == Keys.Down)
            {
                if (lstSuggestSV.SelectedIndex < lstSuggestSV.Items.Count - 1)
                {
                    lstSuggestSV.SelectedIndex++;
                }
                e.Handled = true;
            }

            // Nhấn mũi tên lên
            else if (e.KeyCode == Keys.Up)
            {
                if (lstSuggestSV.SelectedIndex > 0)
                {
                    lstSuggestSV.SelectedIndex--;
                }
                e.Handled = true;
            }

            // Nhấn Enter để chọn
            else if (e.KeyCode == Keys.Enter)
            {
                if (lstSuggestSV.SelectedItem != null)
                {
                    txtSinhVienSearch.Text = lstSuggestSV.SelectedItem.ToString();
                    lstSuggestSV.Visible = false;
                }

                e.SuppressKeyPress = true; // tránh tiếng beep
            }

            // Nhấn ESC để đóng
            else if (e.KeyCode == Keys.Escape)
            {
                lstSuggestSV.Visible = false;
            }
        }

        private void txtSinhVienSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSinhVienSearch_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnCheck;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnSearch;
        }
    }
}
