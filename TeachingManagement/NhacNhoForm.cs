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
using TeachingManagement.Services;
using static TeachingManagement.Services.NotificationService;

namespace TeachingManagement
{
    public partial class NhacNhoForm : Form
    {
        NotificationService notificationService;
        string connectionString = "Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;";
        private int _newNotiCount = 0;

        public NhacNhoForm()
        {
            InitializeComponent();

            notificationService = new NotificationService(connectionString);


            lvThongBao.DrawColumnHeader += lvThongBao_DrawColumnHeader;
            lvThongBao.DrawItem += lvThongBao_DrawItem;
            lvThongBao.DrawSubItem += lvThongBao_DrawSubItem;
        }

        private void ThongBaoForm_Load(object sender, EventArgs e)
        {

            lvThongBao.Columns.Add("Loại", 140);
            lvThongBao.Columns.Add("Nội dung", 450);
            lvThongBao.Columns.Add("Thời gian", 160);

            LoadNotifications();

            CheckNotificationCount();

            
        }

        private void LoadNotifications()
        {
            var list =
                notificationService.GetNotifications(Session.GiangVienId);

            DisplayNotifications(list);
        }

        void DisplayNotifications(List<NotificationItem> list)
        {
            lvThongBao.Items.Clear();

            DateTime lastReadTime = Properties.Settings.Default.LastActionTime;

            if (list == null || list.Count == 0)
            {
                ListViewItem empty = new ListViewItem("");
                empty.SubItems.Add("Không có nhắc nhở nào");
                empty.SubItems.Add("");
                empty.ForeColor = Color.Gray;

                lvThongBao.Items.Add(empty);
                return;
            }

            // Tách 2 nhóm
            var chuaXem = list.Where(x => x.ThoiGian > lastReadTime)
                              .OrderByDescending(x => x.ThoiGian)
                              .ToList();

            var daXem = list.Where(x => x.ThoiGian <= lastReadTime)
                            .OrderByDescending(x => x.ThoiGian)
                            .ToList();

            // ===== NHÓM CHƯA XEM =====
            if (chuaXem.Count > 0)
            {
                ListViewItem header = new ListViewItem("(Chưa xem)");
                header.ForeColor = Color.Blue;
                header.Font = new Font(lvThongBao.Font, FontStyle.Bold);

                lvThongBao.Items.Add(header);

                foreach (var tb in chuaXem)
                {
                    ListViewItem item = new ListViewItem(tb.Loai.ToString());
                    item.SubItems.Add(tb.NoiDung);
                    item.SubItems.Add(tb.ThoiGian.ToString("dd/MM/yyyy HH:mm"));

                    item.Tag = tb.Loai; // thêm lại dòng này
                    item.Font = new Font(lvThongBao.Font, FontStyle.Bold);

                    lvThongBao.Items.Add(item);

                    _newNotiCount+=1;
                }
            }

            // ===== NHÓM ĐÃ XEM =====
            if (daXem.Count > 0)
            {
                ListViewItem header = new ListViewItem("(Đã xem)");
                header.ForeColor = Color.Gray;
                header.Font = new Font(lvThongBao.Font, FontStyle.Bold);

                lvThongBao.Items.Add(header);

                foreach (var tb in daXem)
                {
                    ListViewItem item = new ListViewItem(tb.Loai.ToString());
                    item.SubItems.Add(tb.NoiDung);
                    item.SubItems.Add(tb.ThoiGian.ToString("dd/MM/yyyy HH:mm"));

                    item.Tag = tb.Loai; // thêm lại dòng này
                    item.ForeColor = Color.Gray;

                    lvThongBao.Items.Add(item);
                }
            }
        }

        void CheckNotificationCount()
        {
            int count = _newNotiCount;
            MessageBox.Show("count: " + count);
            if (count > 0)
                lblDescription.Text = $"Bạn có ({count}) nhắc nhở cần kiểm tra";
            else
                lblDescription.Text = "Hiện chưa có nhắc nhở mới";
        }

        private void lvThongBao_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvThongBao_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawBackground();
        }

        private void lvThongBao_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {

            Color back = Color.White;
            Color text = Color.Black;

            if (e.ColumnIndex == 0 && e.Item.Tag != null) // cột Loại
            {
                string type = e.SubItem.Text;

                if (type.Contains("Sắp hết hạn"))
                {
                    back = Color.FromArgb(255, 243, 205);
                    text = Color.DarkOrange;
                }
                else if (type.Contains("Hết hạn"))
                {
                    back = Color.FromArgb(248, 215, 218);
                    text = Color.DarkRed;
                }
                else if (type.Contains("Không hoạt động"))
                {
                    back = Color.FromArgb(255, 244, 229);
                    text = Color.DarkGoldenrod;
                }
            }

            // vẽ background
            using (SolidBrush bg = new SolidBrush(back))
                e.Graphics.FillRectangle(bg, e.Bounds);

            // vẽ text
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                lvThongBao.Font,
                e.Bounds,
                text,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );
        }

        private void NhacNhoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastActionTime = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        private void NhacNhoForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
