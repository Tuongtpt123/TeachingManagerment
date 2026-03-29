namespace TeachingManagement
{
    partial class FrmLopHocEdit
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
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxHocKy = new System.Windows.Forms.ComboBox();
            this.lblTongSinhVien = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamHoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenLop = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstSuggestSV = new System.Windows.Forms.ListBox();
            this.btnLoadSinhVienTuExcel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSinhVienSearch = new System.Windows.Forms.TextBox();
            this.btnConfirmChange = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tìm sinh viên:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSinhVien);
            this.groupBox2.Location = new System.Drawing.Point(12, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 419);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách sinh viên";
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Location = new System.Drawing.Point(6, 19);
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.Size = new System.Drawing.Size(567, 394);
            this.dgvSinhVien.TabIndex = 0;
            this.dgvSinhVien.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSinhVien_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxHocKy);
            this.groupBox1.Controls.Add(this.lblTongSinhVien);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNamHoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTenLop);
            this.groupBox1.Controls.Add(this.txtMoTa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(890, 54);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin lớp tổng quan";
            // 
            // cbxHocKy
            // 
            this.cbxHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxHocKy.FormattingEnabled = true;
            this.cbxHocKy.Items.AddRange(new object[] {
            "Học kỳ",
            "1",
            "2",
            "3"});
            this.cbxHocKy.Location = new System.Drawing.Point(515, 21);
            this.cbxHocKy.Name = "cbxHocKy";
            this.cbxHocKy.Size = new System.Drawing.Size(64, 21);
            this.cbxHocKy.TabIndex = 2;
            // 
            // lblTongSinhVien
            // 
            this.lblTongSinhVien.AutoSize = true;
            this.lblTongSinhVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSinhVien.Location = new System.Drawing.Point(836, 23);
            this.lblTongSinhVien.Name = "lblTongSinhVien";
            this.lblTongSinhVien.Size = new System.Drawing.Size(15, 15);
            this.lblTongSinhVien.TabIndex = 0;
            this.lblTongSinhVien.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(750, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tổng sinh viên:";
            // 
            // txtNamHoc
            // 
            this.txtNamHoc.Location = new System.Drawing.Point(658, 22);
            this.txtNamHoc.Name = "txtNamHoc";
            this.txtNamHoc.Size = new System.Drawing.Size(74, 20);
            this.txtNamHoc.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(599, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Năm học:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(465, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Học kỳ:";
            // 
            // txtTenLop
            // 
            this.txtTenLop.Location = new System.Drawing.Point(58, 22);
            this.txtTenLop.Name = "txtTenLop";
            this.txtTenLop.Size = new System.Drawing.Size(100, 20);
            this.txtTenLop.TabIndex = 1;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(242, 22);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(199, 20);
            this.txtMoTa.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mô tả:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên lớp:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefresh.Location = new System.Drawing.Point(512, 94);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 20);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(87)))), ((int)(((byte)(224)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(433, 94);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 20);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(21, 94);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(404, 20);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(223, 32);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 26);
            this.btnCheck.TabIndex = 11;
            this.btnCheck.Text = "Thêm SV";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstSuggestSV);
            this.groupBox3.Controls.Add(this.btnLoadSinhVienTuExcel);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnCheck);
            this.groupBox3.Controls.Add(this.txtSinhVienSearch);
            this.groupBox3.Location = new System.Drawing.Point(597, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 158);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thêm sinh viên vào lớp học";
            // 
            // lstSuggestSV
            // 
            this.lstSuggestSV.FormattingEnabled = true;
            this.lstSuggestSV.IntegralHeight = false;
            this.lstSuggestSV.Location = new System.Drawing.Point(55, 62);
            this.lstSuggestSV.Name = "lstSuggestSV";
            this.lstSuggestSV.Size = new System.Drawing.Size(209, 90);
            this.lstSuggestSV.TabIndex = 14;
            this.lstSuggestSV.Visible = false;
            this.lstSuggestSV.Click += new System.EventHandler(this.lstSuggestSV_Click);
            // 
            // btnLoadSinhVienTuExcel
            // 
            this.btnLoadSinhVienTuExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnLoadSinhVienTuExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadSinhVienTuExcel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLoadSinhVienTuExcel.Location = new System.Drawing.Point(16, 104);
            this.btnLoadSinhVienTuExcel.Name = "btnLoadSinhVienTuExcel";
            this.btnLoadSinhVienTuExcel.Size = new System.Drawing.Size(144, 41);
            this.btnLoadSinhVienTuExcel.TabIndex = 13;
            this.btnLoadSinhVienTuExcel.Text = "Tải danh sách từ Excel";
            this.btnLoadSinhVienTuExcel.UseVisualStyleBackColor = false;
            this.btnLoadSinhVienTuExcel.Click += new System.EventHandler(this.btnLoadSinhVienTuExcel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Hoặc:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Mã sinh viên:";
            // 
            // txtSinhVienSearch
            // 
            this.txtSinhVienSearch.Location = new System.Drawing.Point(89, 36);
            this.txtSinhVienSearch.Name = "txtSinhVienSearch";
            this.txtSinhVienSearch.Size = new System.Drawing.Size(128, 20);
            this.txtSinhVienSearch.TabIndex = 2;
            this.txtSinhVienSearch.Click += new System.EventHandler(this.txtSinhVienSearch_Click);
            this.txtSinhVienSearch.TextChanged += new System.EventHandler(this.txtMaSinhVien_TextChanged);
            this.txtSinhVienSearch.Enter += new System.EventHandler(this.txtSinhVienSearch_Enter);
            this.txtSinhVienSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSinhVienSearch_KeyDown);
            this.txtSinhVienSearch.Leave += new System.EventHandler(this.txtSinhVienSearch_Leave);
            // 
            // btnConfirmChange
            // 
            this.btnConfirmChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(87)))), ((int)(((byte)(224)))));
            this.btnConfirmChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmChange.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConfirmChange.Location = new System.Drawing.Point(598, 275);
            this.btnConfirmChange.Name = "btnConfirmChange";
            this.btnConfirmChange.Size = new System.Drawing.Size(122, 41);
            this.btnConfirmChange.TabIndex = 14;
            this.btnConfirmChange.Text = "Xác nhận thay đổi";
            this.btnConfirmChange.UseVisualStyleBackColor = false;
            this.btnConfirmChange.Click += new System.EventHandler(this.btnConfirmChange_Click);
            // 
            // label9
            // 
            this.label9.AllowDrop = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(597, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(197, 26);
            this.label9.TabIndex = 15;
            this.label9.Text = "*Bạn không cần bấm nút này nếu đang \r\n  thao tác với dữ liệu sinh viên";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(598, 322);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(304, 224);
            this.rtbLog.TabIndex = 16;
            this.rtbLog.Text = "";
            // 
            // FrmLopHocEdit
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 561);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnConfirmChange);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Name = "FrmLopHocEdit";
            this.Text = "Thông tin lớp học";
            this.Load += new System.EventHandler(this.FrmLopHocEdit_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxHocKy;
        private System.Windows.Forms.Label lblTongSinhVien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamHoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenLop;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLoadSinhVienTuExcel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSinhVienSearch;
        private System.Windows.Forms.Button btnConfirmChange;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ListBox lstSuggestSV;
    }
}