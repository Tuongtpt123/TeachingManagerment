using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeachingManagement;
using TeachingManagement.Services;

// Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;

namespace TeachingManagement
{
    public partial class MainForm : Form
    {
        NotificationService notificationService;

        public MainForm()
        {
            InitializeComponent();

            notificationService = new NotificationService("Server=localhost;Database=db_doan2;Uid=root;Pwd=;Port=3306;");

        }

        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            controlPanel.Controls.Add(childForm);
            controlPanel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void filesToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Đăng xuất và thoát?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TaiKhoanForm());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenChildForm(new HomeForm());
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HomeForm());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void classMnBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LopHocForm());
        }

        private void courseMnBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BaiTapForm());
        }

        private void qlSinhVienBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SinhVienForm());
        }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhacNhoForm());
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //checkLogin();
        }

        private void checkLogin()
        {
            
        }
    }
}
