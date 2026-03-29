namespace TeachingManagement
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuPanel = new System.Windows.Forms.Panel();
            this.btnNhacNho = new System.Windows.Forms.Button();
            this.baiTapMnBtn = new System.Windows.Forms.Button();
            this.qlSinhVienBtn = new System.Windows.Forms.Button();
            this.classMnBtn = new System.Windows.Forms.Button();
            this.homeBtn = new System.Windows.Forms.Button();
            this.accountBtn = new System.Windows.Forms.Button();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuPanel.Controls.Add(this.btnNhacNho);
            this.menuPanel.Controls.Add(this.baiTapMnBtn);
            this.menuPanel.Controls.Add(this.qlSinhVienBtn);
            this.menuPanel.Controls.Add(this.classMnBtn);
            this.menuPanel.Controls.Add(this.homeBtn);
            this.menuPanel.Controls.Add(this.accountBtn);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(106, 571);
            this.menuPanel.TabIndex = 2;
            // 
            // btnNhacNho
            // 
            this.btnNhacNho.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNhacNho.FlatAppearance.BorderSize = 0;
            this.btnNhacNho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhacNho.Location = new System.Drawing.Point(0, 203);
            this.btnNhacNho.Name = "btnNhacNho";
            this.btnNhacNho.Size = new System.Drawing.Size(106, 34);
            this.btnNhacNho.TabIndex = 6;
            this.btnNhacNho.Text = "Nhắc nhở";
            this.btnNhacNho.UseVisualStyleBackColor = true;
            this.btnNhacNho.Click += new System.EventHandler(this.btnThongBao_Click);
            // 
            // baiTapMnBtn
            // 
            this.baiTapMnBtn.FlatAppearance.BorderSize = 0;
            this.baiTapMnBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.baiTapMnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.baiTapMnBtn.Location = new System.Drawing.Point(0, 123);
            this.baiTapMnBtn.Name = "baiTapMnBtn";
            this.baiTapMnBtn.Size = new System.Drawing.Size(106, 34);
            this.baiTapMnBtn.TabIndex = 5;
            this.baiTapMnBtn.Text = "Bài tập";
            this.baiTapMnBtn.UseVisualStyleBackColor = true;
            this.baiTapMnBtn.Click += new System.EventHandler(this.courseMnBtn_Click);
            // 
            // qlSinhVienBtn
            // 
            this.qlSinhVienBtn.FlatAppearance.BorderSize = 0;
            this.qlSinhVienBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.qlSinhVienBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.qlSinhVienBtn.Location = new System.Drawing.Point(0, 163);
            this.qlSinhVienBtn.Name = "qlSinhVienBtn";
            this.qlSinhVienBtn.Size = new System.Drawing.Size(106, 34);
            this.qlSinhVienBtn.TabIndex = 4;
            this.qlSinhVienBtn.Text = "Sinh viên";
            this.qlSinhVienBtn.UseVisualStyleBackColor = true;
            this.qlSinhVienBtn.Click += new System.EventHandler(this.qlSinhVienBtn_Click);
            // 
            // classMnBtn
            // 
            this.classMnBtn.FlatAppearance.BorderSize = 0;
            this.classMnBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.classMnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.classMnBtn.Location = new System.Drawing.Point(0, 83);
            this.classMnBtn.Name = "classMnBtn";
            this.classMnBtn.Size = new System.Drawing.Size(106, 34);
            this.classMnBtn.TabIndex = 2;
            this.classMnBtn.Text = "Lớp học";
            this.classMnBtn.UseVisualStyleBackColor = true;
            this.classMnBtn.Click += new System.EventHandler(this.classMnBtn_Click);
            // 
            // homeBtn
            // 
            this.homeBtn.FlatAppearance.BorderSize = 0;
            this.homeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.homeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeBtn.Location = new System.Drawing.Point(0, 43);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(106, 34);
            this.homeBtn.TabIndex = 1;
            this.homeBtn.Text = "Trang chủ";
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // accountBtn
            // 
            this.accountBtn.FlatAppearance.BorderSize = 0;
            this.accountBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.accountBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accountBtn.Location = new System.Drawing.Point(0, 3);
            this.accountBtn.Name = "accountBtn";
            this.accountBtn.Size = new System.Drawing.Size(106, 34);
            this.accountBtn.TabIndex = 0;
            this.accountBtn.Text = "Tài khoản";
            this.accountBtn.UseVisualStyleBackColor = true;
            this.accountBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.SystemColors.Control;
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(106, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(808, 571);
            this.controlPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 571);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.menuPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Quản lý giảng dạy";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button accountBtn;
        private System.Windows.Forms.Button classMnBtn;
        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.Button qlSinhVienBtn;
        private System.Windows.Forms.Button baiTapMnBtn;
        private System.Windows.Forms.Button btnNhacNho;
    }
}

