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

namespace TeachingManagement
{
    public partial class FrmLopHocDetail : Form
    {
        private int _lopHocId;
        private string connStr = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";

        public FrmLopHocDetail(int lopHocId)
        {
            InitializeComponent();

            _lopHocId = lopHocId;

            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }
        public FrmLopHocDetail()
        {
            InitializeComponent();
        }

        private void FrmLopHocDetail_Load(object sender, EventArgs e)
        {
            loadData();
            LoadDanhSachSinhVien();
        }

        private void loadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        lh.TenLop,
                        lh.MoTa,
                        lh.HocKy,
                        lh.NamHoc,

                        (
                            SELECT COUNT(*)
                            FROM LopHoc_SinhVien lsv
                            JOIN SinhVien sv ON lsv.SinhVienId = sv.SinhVienId
                            WHERE lsv.LopHocId = lh.LopHocId
                            AND sv.TrangThai = 1
                        ) AS SoLuongSinhVien,

                        (
                            SELECT COUNT(*)
                            FROM BaiTap bt
                            WHERE bt.LopHocId = lh.LopHocId
                        ) AS TongBaiTap,

                        (
                            SELECT COUNT(*)
                            FROM BaiTap bt
                            WHERE bt.LopHocId = lh.LopHocId
                            AND bt.HanNop >= NOW()
                        ) AS BaiTapDangMo,

                        (
                            SELECT COUNT(*)
                            FROM BaiTap bt
                            WHERE bt.LopHocId = lh.LopHocId
                            AND bt.HanNop < NOW()
                        ) AS BaiTapDaDong

                    FROM LopHoc lh
                    WHERE lh.LopHocId = @Id
                    AND lh.GiangVienId = @GiangVienId;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", _lopHocId);
                cmd.Parameters.AddWithValue("@GiangVienId", Session.GiangVienId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtTenLop.Text = reader["TenLop"].ToString();
                        txtMoTa.Text = reader["MoTa"].ToString();
                        txtHocKy.Text = reader["HocKy"].ToString();
                        txtNamHoc.Text = reader["NamHoc"].ToString();

                        lblTongSinhVien.Text = reader["SoLuongSinhVien"].ToString();

                        lblTongCacBaiTap.Text = reader["TongBaiTap"].ToString();
                        lblBaiTapDangMo.Text = reader["BaiTapDangMo"].ToString();
                        lblBaiTapDaDong.Text = reader["BaiTapDaDong"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lớp hoặc bạn không có quyền truy cập.");
                        this.Close();
                    }
                }
            }
        }

        private void LoadDanhSachSinhVien()
        {
            string query = @"
                SELECT 
                    sv.MaSinhVien AS 'Mã SV',
                    sv.HoTen AS 'Họ và tên',
                    sv.Email AS 'Email',

                    CAST(CONCAT(
                        IFNULL(COUNT(DISTINCT bn.BaiTapId),0),
                        '/',
                        (SELECT COUNT(*) FROM BaiTap WHERE LopHocId = @LopHocId)
                    ) AS CHAR) AS 'Bài tập đã nộp',

                    IFNULL(COUNT(DISTINCT bn.BaiTapId),0) AS SoBaiDaNop,
                    (SELECT COUNT(*) FROM BaiTap WHERE LopHocId = @LopHocId) AS TongBaiTap

                FROM SinhVien sv
                JOIN LopHoc_SinhVien lsv 
                    ON sv.SinhVienId = lsv.SinhVienId

                LEFT JOIN BaiNop bn 
                    ON bn.SinhVienId = sv.SinhVienId
                    AND bn.BaiTapId IN (
                        SELECT BaiTapId 
                        FROM BaiTap 
                        WHERE LopHocId = @LopHocId
                    )

                WHERE lsv.LopHocId = @LopHocId
                AND sv.TrangThai = 1

                GROUP BY sv.SinhVienId
                ORDER BY SUBSTRING_INDEX(sv.HoTen, ' ', -1);";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    

                    dgvSinhVien.DataSource = dt;

                    // Ẩn cột dùng nội bộ
                    dgvSinhVien.Columns["Bài tập đã nộp"].ValueType = typeof(string);
                    dgvSinhVien.Columns["SoBaiDaNop"].Visible = false;
                    dgvSinhVien.Columns["TongBaiTap"].Visible = false;

                    ToMauTrangThaiSinhVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sinh viên: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm) ) {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }
            string query = @"
                SELECT 
                    sv.MaSinhVien AS 'Mã SV',
                    sv.HoTen AS 'Họ và tên',
                    sv.Email AS 'Email',

                    CAST(CONCAT(
                        IFNULL(COUNT(DISTINCT bn.BaiTapId),0),
                        '/',
                        (SELECT COUNT(*) FROM BaiTap WHERE LopHocId = @LopHocId)
                    ) AS CHAR) AS 'Bài tập đã nộp',

                    IFNULL(COUNT(DISTINCT bn.BaiTapId),0) AS SoBaiDaNop,
                    (SELECT COUNT(*) FROM BaiTap WHERE LopHocId = @LopHocId) AS TongBaiTap

                FROM SinhVien sv
                JOIN LopHoc_SinhVien lsv 
                    ON sv.SinhVienId = lsv.SinhVienId

                LEFT JOIN BaiNop bn 
                    ON bn.SinhVienId = sv.SinhVienId
                    AND bn.BaiTapId IN (
                        SELECT BaiTapId 
                        FROM BaiTap 
                        WHERE LopHocId = @LopHocId
                    )

                WHERE lsv.LopHocId = @LopHocId
                AND sv.TrangThai = 1
                AND (sv.HoTen LIKE @SearchTerm OR sv.MaSinhVien LIKE @SearchTerm)   

                GROUP BY sv.SinhVienId
                ORDER BY SUBSTRING_INDEX(sv.HoTen, ' ', -1);";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LopHocId", _lopHocId);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSinhVien.DataSource = dt;
                    // Ẩn cột dùng nội bộ
                    dgvSinhVien.Columns["Bài tập đã nộp"].ValueType = typeof(string);
                    dgvSinhVien.Columns["SoBaiDaNop"].Visible = false;
                    dgvSinhVien.Columns["TongBaiTap"].Visible = false;
                    ToMauTrangThaiSinhVien();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sinh viên: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDanhSachSinhVien();
            txtSearch.Clear();
        }

        private void ToMauTrangThaiSinhVien()
        {
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.IsNewRow) continue;

                int daNop = Convert.ToInt32(row.Cells["SoBaiDaNop"].Value);
                int tong = Convert.ToInt32(row.Cells["TongBaiTap"].Value);

                if (tong == 0)
                    continue;

                if (daNop == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose; // đỏ rất nhạt
                }
                else if (daNop < tong)
                {
                    row.DefaultCellStyle.BackColor = Color.LemonChiffon; // vàng nhạt
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Honeydew; // xanh rất nhạt
                }
            }
        }
    }
}
