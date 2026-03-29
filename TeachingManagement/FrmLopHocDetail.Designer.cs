namespace TeachingManagement
{
    partial class FrmLopHocDetail
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBaiTapDaDong = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblBaiTapDangMo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTongCacBaiTap = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTongSinhVien = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamHoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHocKy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenLop = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(16, 226);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(650, 22);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(87)))), ((int)(((byte)(224)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(676, 224);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 27);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(793, 224);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(107, 27);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBaiTapDaDong);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblBaiTapDangMo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblTongCacBaiTap);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblTongSinhVien);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNamHoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHocKy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTenLop);
            this.groupBox1.Controls.Add(this.txtMoTa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox1.Size = new System.Drawing.Size(884, 175);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin lớp tổng quan";
            // 
            // lblBaiTapDaDong
            // 
            this.lblBaiTapDaDong.AutoSize = true;
            this.lblBaiTapDaDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaiTapDaDong.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblBaiTapDaDong.Location = new System.Drawing.Point(768, 126);
            this.lblBaiTapDaDong.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBaiTapDaDong.Name = "lblBaiTapDaDong";
            this.lblBaiTapDaDong.Size = new System.Drawing.Size(67, 20);
            this.lblBaiTapDaDong.TabIndex = 14;
            this.lblBaiTapDaDong.Text = "label12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(769, 105);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Đã đóng";
            // 
            // lblBaiTapDangMo
            // 
            this.lblBaiTapDangMo.AutoSize = true;
            this.lblBaiTapDangMo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaiTapDangMo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblBaiTapDangMo.Location = new System.Drawing.Point(608, 126);
            this.lblBaiTapDangMo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBaiTapDangMo.Name = "lblBaiTapDangMo";
            this.lblBaiTapDangMo.Size = new System.Drawing.Size(67, 20);
            this.lblBaiTapDangMo.TabIndex = 12;
            this.lblBaiTapDangMo.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(609, 105);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Đang mở";
            // 
            // lblTongCacBaiTap
            // 
            this.lblTongCacBaiTap.AutoSize = true;
            this.lblTongCacBaiTap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongCacBaiTap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(87)))), ((int)(((byte)(224)))));
            this.lblTongCacBaiTap.Location = new System.Drawing.Point(435, 126);
            this.lblTongCacBaiTap.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTongCacBaiTap.Name = "lblTongCacBaiTap";
            this.lblTongCacBaiTap.Size = new System.Drawing.Size(57, 20);
            this.lblTongCacBaiTap.TabIndex = 6;
            this.lblTongCacBaiTap.Text = "label8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(436, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tổng các bài tập";
            // 
            // lblTongSinhVien
            // 
            this.lblTongSinhVien.AutoSize = true;
            this.lblTongSinhVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSinhVien.Location = new System.Drawing.Point(563, 57);
            this.lblTongSinhVien.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTongSinhVien.Name = "lblTongSinhVien";
            this.lblTongSinhVien.Size = new System.Drawing.Size(15, 16);
            this.lblTongSinhVien.TabIndex = 9;
            this.lblTongSinhVien.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(563, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tổng sinh viên";
            // 
            // txtNamHoc
            // 
            this.txtNamHoc.Location = new System.Drawing.Point(246, 126);
            this.txtNamHoc.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtNamHoc.Name = "txtNamHoc";
            this.txtNamHoc.ReadOnly = true;
            this.txtNamHoc.Size = new System.Drawing.Size(132, 22);
            this.txtNamHoc.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Năm học";
            // 
            // txtHocKy
            // 
            this.txtHocKy.Location = new System.Drawing.Point(54, 126);
            this.txtHocKy.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtHocKy.Name = "txtHocKy";
            this.txtHocKy.ReadOnly = true;
            this.txtHocKy.Size = new System.Drawing.Size(132, 22);
            this.txtHocKy.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Học kỳ";
            // 
            // txtTenLop
            // 
            this.txtTenLop.Location = new System.Drawing.Point(54, 55);
            this.txtTenLop.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtTenLop.Name = "txtTenLop";
            this.txtTenLop.ReadOnly = true;
            this.txtTenLop.Size = new System.Drawing.Size(132, 22);
            this.txtTenLop.TabIndex = 0;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(247, 54);
            this.txtMoTa.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ReadOnly = true;
            this.txtMoTa.Size = new System.Drawing.Size(263, 22);
            this.txtMoTa.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mô tả";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tên lớp";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSinhVien);
            this.groupBox2.Location = new System.Drawing.Point(16, 258);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox2.Size = new System.Drawing.Size(884, 291);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách sinh viên";
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Location = new System.Drawing.Point(10, 23);
            this.dgvSinhVien.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.Size = new System.Drawing.Size(864, 262);
            this.dgvSinhVien.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 207);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(211, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Tìm sinh viên theo tên hoặc mã số:";
            // 
            // FrmLopHocDetail
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 561);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "FrmLopHocDetail";
            this.Text = "Thông tin lớp học";
            this.Load += new System.EventHandler(this.FrmLopHocDetail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTongSinhVien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamHoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHocKy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenLop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.Label lblBaiTapDaDong;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblBaiTapDangMo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTongCacBaiTap;
        private System.Windows.Forms.Label label6;
    }
}