namespace TeachingManagement
{
    partial class BaiTapForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnDongBoBaiTap = new System.Windows.Forms.Button();
            this.btnThemBaiTapMoi = new System.Windows.Forms.Button();
            this.cbxLopHoc = new System.Windows.Forms.ComboBox();
            this.dgvBaiTap = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblHanNop = new System.Windows.Forms.Label();
            this.lblDuongDan = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvBaiNop = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblHuongDan2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiTap)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiNop)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn lớp:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.btnDongBoBaiTap);
            this.groupBox1.Controls.Add(this.btnThemBaiTapMoi);
            this.groupBox1.Controls.Add(this.cbxLopHoc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thao tác";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(51)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.Image = global::TeachingManagement.Properties.Resources.New_Folder__Streamline_Core;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(643, 18);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(133, 32);
            this.button4.TabIndex = 2;
            this.button4.Text = "   Mở thư mục";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnDongBoBaiTap
            // 
            this.btnDongBoBaiTap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnDongBoBaiTap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDongBoBaiTap.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDongBoBaiTap.Image = global::TeachingManagement.Properties.Resources.Arrow_Reload_Vertical_1__Streamline_Core;
            this.btnDongBoBaiTap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDongBoBaiTap.Location = new System.Drawing.Point(504, 18);
            this.btnDongBoBaiTap.Name = "btnDongBoBaiTap";
            this.btnDongBoBaiTap.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDongBoBaiTap.Size = new System.Drawing.Size(133, 32);
            this.btnDongBoBaiTap.TabIndex = 2;
            this.btnDongBoBaiTap.Text = "   Đồng bộ bài tập";
            this.btnDongBoBaiTap.UseVisualStyleBackColor = false;
            this.btnDongBoBaiTap.Click += new System.EventHandler(this.btnDongBoBaiTap_Click);
            // 
            // btnThemBaiTapMoi
            // 
            this.btnThemBaiTapMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(87)))), ((int)(((byte)(224)))));
            this.btnThemBaiTapMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemBaiTapMoi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnThemBaiTapMoi.Image = global::TeachingManagement.Properties.Resources.Add_1__Streamline_Core;
            this.btnThemBaiTapMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThemBaiTapMoi.Location = new System.Drawing.Point(365, 19);
            this.btnThemBaiTapMoi.Name = "btnThemBaiTapMoi";
            this.btnThemBaiTapMoi.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnThemBaiTapMoi.Size = new System.Drawing.Size(133, 32);
            this.btnThemBaiTapMoi.TabIndex = 2;
            this.btnThemBaiTapMoi.Text = "    Thêm bài tập mới";
            this.btnThemBaiTapMoi.UseVisualStyleBackColor = false;
            this.btnThemBaiTapMoi.Click += new System.EventHandler(this.btnThemBaiTapMoi_Click);
            // 
            // cbxLopHoc
            // 
            this.cbxLopHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLopHoc.FormattingEnabled = true;
            this.cbxLopHoc.Location = new System.Drawing.Point(78, 25);
            this.cbxLopHoc.Name = "cbxLopHoc";
            this.cbxLopHoc.Size = new System.Drawing.Size(121, 21);
            this.cbxLopHoc.TabIndex = 1;
            this.cbxLopHoc.SelectedIndexChanged += new System.EventHandler(this.cbxLopHoc_SelectedIndexChanged);
            // 
            // dgvBaiTap
            // 
            this.dgvBaiTap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaiTap.Location = new System.Drawing.Point(7, 19);
            this.dgvBaiTap.Name = "dgvBaiTap";
            this.dgvBaiTap.Size = new System.Drawing.Size(525, 203);
            this.dgvBaiTap.TabIndex = 1;
            this.dgvBaiTap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaiTap_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblMoTa);
            this.groupBox2.Controls.Add(this.lblHanNop);
            this.groupBox2.Controls.Add(this.lblDuongDan);
            this.groupBox2.Controls.Add(this.lblTieuDe);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(555, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 190);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin bài tập";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Đường dẫn:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Hạn nộp:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mô tả:";
            // 
            // lblMoTa
            // 
            this.lblMoTa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoTa.Location = new System.Drawing.Point(90, 63);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(137, 31);
            this.lblMoTa.TabIndex = 0;
            this.lblMoTa.Text = "SAMPLE";
            // 
            // lblHanNop
            // 
            this.lblHanNop.AutoSize = true;
            this.lblHanNop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHanNop.Location = new System.Drawing.Point(90, 94);
            this.lblHanNop.Name = "lblHanNop";
            this.lblHanNop.Size = new System.Drawing.Size(56, 13);
            this.lblHanNop.TabIndex = 0;
            this.lblHanNop.Text = "SAMPLE";
            // 
            // lblDuongDan
            // 
            this.lblDuongDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuongDan.Location = new System.Drawing.Point(90, 125);
            this.lblDuongDan.Name = "lblDuongDan";
            this.lblDuongDan.Size = new System.Drawing.Size(143, 51);
            this.lblDuongDan.TabIndex = 0;
            this.lblDuongDan.Text = "SAMPLE";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(90, 34);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(56, 13);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "SAMPLE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tiêu đề:";
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.AutoSize = true;
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuongDan.Location = new System.Drawing.Point(303, 95);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(181, 21);
            this.lblHuongDan.TabIndex = 0;
            this.lblHuongDan.Text = "Tạm thời chưa có dữ liệu";
            this.lblHuongDan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHuongDan.Click += new System.EventHandler(this.label6_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblHuongDan);
            this.groupBox3.Controls.Add(this.dgvBaiNop);
            this.groupBox3.Location = new System.Drawing.Point(12, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 214);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Danh sách sinh viên đã nộp bài tập";
            // 
            // dgvBaiNop
            // 
            this.dgvBaiNop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaiNop.Location = new System.Drawing.Point(6, 19);
            this.dgvBaiNop.Name = "dgvBaiNop";
            this.dgvBaiNop.Size = new System.Drawing.Size(764, 189);
            this.dgvBaiNop.TabIndex = 1;
            this.dgvBaiNop.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaiNop_CellContentClick);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(54)))), ((int)(((byte)(88)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnXoa.Image = global::TeachingManagement.Properties.Resources.Recycle_Bin_2__Streamline_Core;
            this.btnXoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXoa.Location = new System.Drawing.Point(694, 280);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnXoa.Size = new System.Drawing.Size(94, 32);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblHuongDan2);
            this.groupBox4.Controls.Add(this.dgvBaiTap);
            this.groupBox4.Location = new System.Drawing.Point(11, 84);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(538, 228);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách bài tập";
            // 
            // lblHuongDan2
            // 
            this.lblHuongDan2.AutoSize = true;
            this.lblHuongDan2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuongDan2.Location = new System.Drawing.Point(177, 105);
            this.lblHuongDan2.Name = "lblHuongDan2";
            this.lblHuongDan2.Size = new System.Drawing.Size(181, 21);
            this.lblHuongDan2.TabIndex = 0;
            this.lblHuongDan2.Text = "Tạm thời chưa có dữ liệu";
            this.lblHuongDan2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHuongDan2.Click += new System.EventHandler(this.label6_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Image = global::TeachingManagement.Properties.Resources.Pen_Tool__Streamline_Core;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(555, 280);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(94, 32);
            this.button2.TabIndex = 4;
            this.button2.Text = "Sửa";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BaiTapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 541);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BaiTapForm";
            this.Text = "BaiTapForm";
            this.Load += new System.EventHandler(this.BaiTapForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiTap)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaiNop)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxLopHoc;
        private System.Windows.Forms.Button btnDongBoBaiTap;
        private System.Windows.Forms.Button btnThemBaiTapMoi;
        private System.Windows.Forms.DataGridView dgvBaiTap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblHanNop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDuongDan;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dgvBaiNop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblHuongDan2;
    }
}