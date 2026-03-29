using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TeachingManagement
{
    public partial class BaiTapForm : Form
    {
        private int _currentBaiTapId = -1;
        private string _currentLinkDrive = "";
        private int _currentLopHocId = -1;

        string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public BaiTapForm()
        {
            InitializeComponent();
        }

        private void BaiTapForm_Load(object sender, EventArgs e)
        {
            // Ẩn bảng ngay từ đầu
            dgvBaiTap.Visible = false;
            dgvBaiNop.Visible = false;

            LoadDanhSachLop();
        }

        private void LoadDanhSachLop()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                    SELECT LopHocId, TenLop
                    FROM LopHoc
                    WHERE GiangVienId = @GiangVienId
                        AND TrangThai = 1
                    ORDER BY NgayTao DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cbxLopHoc.DataSource = dt;
                        cbxLopHoc.DisplayMember = "TenLop";
                        cbxLopHoc.ValueMember = "LopHocId";
                        cbxLopHoc.SelectedIndex = -1; // không auto chọn
                    }
                }
            }
        }

        private void cbxLopHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLopHoc.SelectedValue == null)
                return;

            if (cbxLopHoc.SelectedValue is int lopHocId)
            {
                _currentLopHocId = lopHocId;
                // Reset lại thông tin bài tập và bài nộp khi đổi lớp
                LoadDanhSachBaiTap(_currentLopHocId);

                dgvBaiTap.Visible = true;

                dgvBaiNop.Visible = false;
                lblHuongDan.Visible = true;
                lblHuongDan2.Visible = false;
            }

        }

        private void LoadDanhSachBaiTap(int lopHocId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"SELECT 
                        BaiTapId,
                        TieuDe,
                        HanNop,
                        NgayTao,
                        LinkDrive
                    FROM BaiTap
                    WHERE LopHocId = @LopHocId
                    ORDER BY NgayTao DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LopHocId", lopHocId);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvBaiTap.DataSource = dt;

                    }
                }
            }

            FormatDgvBaiTap();
        }

        private void FormatDgvBaiTap()
        {
            if (dgvBaiTap.Columns.Count == 0)
                return;

            dgvBaiTap.Columns["BaiTapId"].HeaderText = "Mã";
            dgvBaiTap.Columns["TieuDe"].HeaderText = "Tiêu đề";
            dgvBaiTap.Columns["HanNop"].HeaderText = "Hạn nộp";
            dgvBaiTap.Columns["NgayTao"].HeaderText = "Ngày tạo";

            dgvBaiTap.Columns["BaiTapId"].Visible = false;
            dgvBaiTap.Columns["LinkDrive"].Visible = false;

            dgvBaiTap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaiTap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaiTap.MultiSelect = false;
            dgvBaiTap.ReadOnly = true;
            dgvBaiTap.AllowUserToAddRows = false;
        }

        private void dgvBaiTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvBaiTap.Rows[e.RowIndex];

            _currentBaiTapId = Convert.ToInt32(row.Cells["BaiTapId"].Value);
            _currentLinkDrive = row.Cells["LinkDrive"].Value?.ToString();

            HienThiThongTinBaiTap(_currentBaiTapId);
            LoadDanhSachBaiNop(_currentBaiTapId);

            // Hiện lại dgvBaiNop
            dgvBaiNop.Visible = true;
            lblHuongDan.Visible = false;
        }

        private void HienThiThongTinBaiTap(int baiTapId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                    SELECT TieuDe, MoTa, HanNop, LinkDrive
                    FROM BaiTap
                    WHERE BaiTapId = @BaiTapId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTieuDe.Text = reader["TieuDe"].ToString().ToUpper();
                            lblMoTa.Text = reader["MoTa"].ToString().ToUpper();
                            lblHanNop.Text = Convert.ToDateTime(reader["HanNop"])
                                                .ToString("dd/MM/yyyy HH:mm");
                            lblDuongDan.Text = reader["LinkDrive"].ToString();
                        }
                    }
                }
            }
        }

        private void FormatDgvBaiNop()
        {
            if (dgvBaiNop.Columns.Count == 0) return;

            dgvBaiNop.Columns["BaiNopId"].Visible = false;

            dgvBaiNop.Columns["MaSinhVien"].HeaderText = "Mã SV";
            dgvBaiNop.Columns["HoTen"].HeaderText = "Họ tên";
            dgvBaiNop.Columns["TenFile"].HeaderText = "Tên file";
            dgvBaiNop.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvBaiNop.Columns["ThoiGianNop"].HeaderText = "Thời gian";

            if (!dgvBaiNop.Columns.Contains("Action"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Action";
                btn.HeaderText = "";
                btn.Text = "Xem";
                btn.UseColumnTextForButtonValue = true;

                dgvBaiNop.Columns.Add(btn);
            }

            dgvBaiNop.Columns["DuongDanThuMuc"].Visible = false;

            dgvBaiNop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaiNop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaiNop.MultiSelect = false;
            dgvBaiNop.ReadOnly = true;
            dgvBaiNop.AllowUserToAddRows = false;
        }

        private void LoadDanhSachBaiNop(int baiTapId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        bn.BaiNopId,
                        sv.MaSinhVien,
                        sv.HoTen,
                        bn.TenFile,
                        bn.ThoiGianNop,
                        CASE 
                            WHEN bn.TrangThai = 0 THEN 'Chưa nộp'
                            WHEN bn.TrangThai = 1 THEN 'Đã nộp'
                            WHEN bn.TrangThai = 2 THEN 'Đã xem'
                            ELSE 'Không xác định'
                        END AS TrangThai,
                        bn.DuongDanThuMuc
                    FROM BaiNop bn
                    JOIN SinhVien sv ON bn.SinhVienId = sv.SinhVienId
                    WHERE bn.BaiTapId = @BaiTapId
                    ORDER BY sv.MaSinhVien";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBaiNop.DataSource = dt;
                    }
                }
            }

            FormatDgvBaiNop();
        }

        private void btnDongBoBaiTap_Click(object sender, EventArgs e)
        {
            if (_currentBaiTapId == -1)
            {
                MessageBox.Show("Chưa chọn bài tập.", "Thông báo");
                return;
            }

            if (string.IsNullOrEmpty(_currentLinkDrive) || !Directory.Exists(_currentLinkDrive))
            {
                MessageBox.Show("LinkDrive không hợp lệ.");
                return;
            }

            DongBoBaiTap(_currentBaiTapId, _currentLinkDrive);
        }

        private void DongBoBaiTap(int baiTapId, string folderPath)
        {
            int themMoi = 0;
            int capNhat = 0;
            int boQua = 0;
            int tongNop = 0;

            StringBuilder log = new StringBuilder();
            log.AppendLine("===== KẾT QUẢ ĐỒNG BỘ =====");
            log.AppendLine("Thời gian: " + DateTime.Now);

            string[] studentFolders = Directory.GetDirectories(folderPath);

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                int lopHocId = GetLopHocId(conn, baiTapId);

                foreach (string folderFullPath in studentFolders)
                {
                    string folderName = Path.GetFileName(folderFullPath);

                    // Lấy MSSV từ tên folder: SV001_NguyenVanA hoặc SV001-NguyenVanA => lấy SV001 hoặc không có chuỗi phụ thì lấy nguyên folderName
                    string maSinhVien = folderName.Split('_', '-')
                              .FirstOrDefault() ?? folderName;

                    int? sinhVienId = GetSinhVienId(conn, maSinhVien);



                    if (sinhVienId == null)
                    {
                        boQua++;
                        log.AppendLine($"BỎ QUA: Không tìm thấy SV {maSinhVien}");
                        continue;
                    }

                    // 🔥 Kiểm tra thuộc lớp
                    if (!KiemTraSinhVienThuocLop(conn, sinhVienId.Value, lopHocId))
                    {
                        boQua++;
                        log.AppendLine($"BỎ QUA: SV {maSinhVien} không thuộc lớp của bài tập");
                        continue;
                    }

                    if (KiemTraBaiNopTonTai(conn, baiTapId, sinhVienId.Value))
                    {
                        CapNhatBaiNop(conn, baiTapId, sinhVienId.Value, folderName, folderFullPath);
                        capNhat++;
                        log.AppendLine($"CẬP NHẬT: {folderName}");
                    }
                    else
                    {
                        ThemBaiNop(conn, baiTapId, sinhVienId.Value, folderName, folderFullPath);
                        themMoi++;
                        log.AppendLine($"THÊM MỚI: {folderName}");
                    }

                    tongNop++;
                }
            }

            log.AppendLine("===== KẾT THÚC ĐỒNG BỘ =====");

            string logPath = LuuFileLog(log.ToString());

            DialogResult result = MessageBox.Show(
                $"Đồng bộ hoàn tất.\n\n" +
                $"Thêm mới: {themMoi}\n" +
                $"Cập nhật: {capNhat}\n" +
                $"Bỏ qua: {boQua}\n" +
                $"Tổng SV đã nộp: {tongNop}\n\n" +
                $"Mở file log?",
                "Kết quả đồng bộ",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("explorer.exe", logPath);
            }

            string sql = @"
                UPDATE GiangVien
                SET LastSyncTime = NOW()
                WHERE GiangVienId = @GiangVienId;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xẩy ra lỗi khi thực hiện cập nhật thời gian đồng bộ: " + ex.Message);
            }

            LoadDanhSachBaiNop(baiTapId);
        }

        private string LuuFileLog(string content)
        {
            string logFolder = Path.Combine(Application.StartupPath, "logs");

            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            string fileName = "KetQuaDongBo_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            string fullPath = Path.Combine(logFolder, fileName);

            File.AppendAllText(fullPath, content + Environment.NewLine);

            return fullPath;
        }

        private bool KiemTraBaiNopTonTai(MySqlConnection conn, int baiTapId, int sinhVienId)
        {
            string query = @"SELECT COUNT(*) 
                     FROM BaiNop 
                     WHERE BaiTapId=@BaiTapId 
                     AND SinhVienId=@SinhVienId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);
                cmd.Parameters.AddWithValue("@SinhVienId", sinhVienId);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void ThemBaiNop(MySqlConnection conn,
                        int baiTapId,
                        int sinhVienId,
                        string folderName,
                        string fullPath)
        {
            string query = @"
                INSERT INTO BaiNop
                (BaiTapId, SinhVienId, TenFile, DuongDanThuMuc, ThoiGianNop, TrangThai)
                VALUES
                (@BaiTapId, @SinhVienId, @TenFile, @DuongDanThuMuc, @ThoiGianNop, 1)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);
                cmd.Parameters.AddWithValue("@SinhVienId", sinhVienId);
                cmd.Parameters.AddWithValue("@TenFile", folderName);
                cmd.Parameters.AddWithValue("@DuongDanThuMuc", fullPath);
                cmd.Parameters.AddWithValue("@ThoiGianNop", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }

        private int? GetSinhVienId(MySqlConnection conn, string maSinhVien)
        {
            string query = @"SELECT SinhVienId 
                     FROM SinhVien 
                     WHERE MaSinhVien = @MaSinhVien
                        AND TrangThai = 1
                     LIMIT 1";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToInt32(result);
            }
        }

        private void CapNhatBaiNop(MySqlConnection conn,
                           int baiTapId,
                           int sinhVienId,
                           string folderName,
                           string fullPath)
        {
            string query = @"
                UPDATE BaiNop
                SET 
                    TenFile = @TenFile,
                    DuongDanThuMuc = @DuongDanThuMuc,
                    ThoiGianNop = @ThoiGianNop,
                    TrangThai = 1
                WHERE 
                    BaiTapId = @BaiTapId 
                    AND SinhVienId = @SinhVienId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TenFile", folderName);
                cmd.Parameters.AddWithValue("@DuongDanThuMuc", fullPath);
                cmd.Parameters.AddWithValue("@ThoiGianNop", DateTime.Now);
                cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);
                cmd.Parameters.AddWithValue("@SinhVienId", sinhVienId);

                cmd.ExecuteNonQuery();
            }
        }
        private int GetLopHocId(MySqlConnection conn, int baiTapId)
        {
            string query = @"SELECT LopHocId 
                     FROM BaiTap 
                     WHERE BaiTapId = @BaiTapId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private bool KiemTraSinhVienThuocLop(MySqlConnection conn,
                                     int sinhVienId,
                                     int lopHocId)
        {
            string query = @"
                SELECT COUNT(*)
                FROM LopHoc_SinhVien
                WHERE SinhVienId = @SinhVienId
                AND LopHocId = @LopHocId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@SinhVienId", sinhVienId);
                cmd.Parameters.AddWithValue("@LopHocId", lopHocId);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void dgvBaiNop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Kiểm tra có phải cột Action không
            if (dgvBaiNop.Columns[e.ColumnIndex].Name == "Action")
            {
                string path = dgvBaiNop.Rows[e.RowIndex]
                                       .Cells["DuongDanThuMuc"]
                                       .Value?.ToString();

                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("Không có đường dẫn.");
                    return;
                }

                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Thư mục không tồn tại.");
                    return;
                }

                try
                {
                    // Mở thư mục bằng Windows Explorer
                    System.Diagnostics.Process.Start("explorer.exe", path);

                    // 👉 Nếu muốn khi xem thì cập nhật trạng thái = 2 (Đã xem)
                    CapNhatTrangThaiDaXem(e.RowIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở thư mục: " + ex.Message);
                }
            }
        }

        private void CapNhatTrangThaiDaXem(int rowIndex)
        {
            int baiNopId = Convert.ToInt32(
                dgvBaiNop.Rows[rowIndex].Cells["BaiNopId"].Value);

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"UPDATE BaiNop 
                         SET TrangThai = 2
                         WHERE BaiNopId = @BaiNopId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BaiNopId", baiNopId);
                    cmd.ExecuteNonQuery();
                }
            }

            // Reload lại grid để cập nhật hiển thị
            LoadDanhSachBaiNop(_currentBaiTapId);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MoThuMucBaiTap();
        }

        private void MoThuMucBaiTap()
        {
            try
            {
                // 1. Kiểm tra có dòng được chọn không
                if (dgvBaiTap.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một bài tập.", "Thông báo");
                    return;
                }

                // 2. Kiểm tra cột LinkDrive có tồn tại không
                if (!dgvBaiTap.Columns.Contains("LinkDrive"))
                {
                    MessageBox.Show("Không tìm thấy cột LinkDrive.", "Lỗi");
                    return;
                }

                // 3. Lấy đường dẫn
                string path = dgvBaiTap.CurrentRow.Cells["LinkDrive"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(path))
                {
                    MessageBox.Show("Bài tập này chưa có đường dẫn thư mục. Vui lòng thêm đường dẫn vào bài tập này trước!", "Thông báo");
                    return;
                }

                // 4. Kiểm tra thư mục tồn tại
                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Thư mục không tồn tại:\n" + path, "Lỗi");
                    return;
                }

                // 5. Mở thư mục
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở thư mục.\nChi tiết: " + ex.Message, "Lỗi");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvBaiTap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một bài tập.", "Thông báo");
                return;
            }
            FrmBaiTapEdit frmEdit = new FrmBaiTapEdit(
                _currentBaiTapId,
                _currentLopHocId
            );
            frmEdit.ShowDialog();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvBaiNop.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một bài tập để xóa.", "Thông báo");
                return;
            }
            deleteBaiTap();
        }

        private void deleteBaiTap()
        {
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa bài tập này? Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
            {
                return;
            }
            bool result = XoaBaiTap(_currentBaiTapId, Session.GiangVienId);

            if (result)
            {
                MessageBox.Show("Đã xóa bài tập thành công");
            }
        }

        public bool XoaBaiTap(int baiTapId, int giangVienId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa bài nộp
                        string deleteBaiNop = @"
                            DELETE bn
                            FROM BaiNop bn
                            JOIN BaiTap bt ON bn.BaiTapId = bt.BaiTapId
                            JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId
                            WHERE bt.BaiTapId = @BaiTapId
                            AND lh.GiangVienId = @GiangVienId";

                        using (var cmd = new MySqlCommand(deleteBaiNop, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);
                            cmd.Parameters.AddWithValue("@GiangVienId", giangVienId);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa bài tập
                        string deleteBaiTap = @"
                            DELETE bt
                            FROM BaiTap bt
                            JOIN LopHoc lh ON bt.LopHocId = lh.LopHocId
                            WHERE bt.BaiTapId = @BaiTapId
                            AND lh.GiangVienId = @GiangVienId";

                        using (var cmd = new MySqlCommand(deleteBaiTap, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@BaiTapId", baiTapId);
                            cmd.Parameters.AddWithValue("@GiangVienId", giangVienId);

                            int affected = cmd.ExecuteNonQuery();
                            if (affected == 0)
                                throw new Exception("Không tìm thấy bài tập hoặc bạn không có quyền xóa.");
                        }

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        private void btnThemBaiTapMoi_Click(object sender, EventArgs e)
        {
            if (_currentLopHocId == -1)
            {
                MessageBox.Show("Vui lòng chọn lớp học trước khi thêm bài tập.", "Thông báo");
                return;
            }
            FrmBaiTapAdd frmAdd = new FrmBaiTapAdd(_currentLopHocId);
            frmAdd.ShowDialog();
        }
    }
}
