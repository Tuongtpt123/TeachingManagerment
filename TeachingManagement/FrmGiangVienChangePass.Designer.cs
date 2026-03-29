namespace TeachingManagement
{
    partial class FrmGiangVienChangePass
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtMatKhauMoi2 = new System.Windows.Forms.Label();
            this.lblHide = new System.Windows.Forms.Label();
            this.txtMatKhauHienTai = new System.Windows.Forms.TextBox();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.txtMatKhauConfirm = new System.Windows.Forms.TextBox();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mật khẩu hiện tại:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mật khẩu mới:";
            // 
            // txtMatKhauMoi2
            // 
            this.txtMatKhauMoi2.AutoSize = true;
            this.txtMatKhauMoi2.Location = new System.Drawing.Point(12, 109);
            this.txtMatKhauMoi2.Name = "txtMatKhauMoi2";
            this.txtMatKhauMoi2.Size = new System.Drawing.Size(122, 13);
            this.txtMatKhauMoi2.TabIndex = 7;
            this.txtMatKhauMoi2.Text = "Xác nhận mật khẩu mới:";
            // 
            // lblHide
            // 
            this.lblHide.AutoSize = true;
            this.lblHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.ForeColor = System.Drawing.Color.Blue;
            this.lblHide.Location = new System.Drawing.Point(208, 84);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(33, 13);
            this.lblHide.TabIndex = 4;
            this.lblHide.Text = "Hiện";
            this.lblHide.Click += new System.EventHandler(this.lblHide_Click);
            // 
            // txtMatKhauHienTai
            // 
            this.txtMatKhauHienTai.Location = new System.Drawing.Point(15, 35);
            this.txtMatKhauHienTai.Name = "txtMatKhauHienTai";
            this.txtMatKhauHienTai.Size = new System.Drawing.Size(177, 20);
            this.txtMatKhauHienTai.TabIndex = 0;
            this.txtMatKhauHienTai.UseSystemPasswordChar = true;
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Location = new System.Drawing.Point(15, 81);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(177, 20);
            this.txtMatKhauMoi.TabIndex = 1;
            this.txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // txtMatKhauConfirm
            // 
            this.txtMatKhauConfirm.Location = new System.Drawing.Point(15, 125);
            this.txtMatKhauConfirm.Name = "txtMatKhauConfirm";
            this.txtMatKhauConfirm.Size = new System.Drawing.Size(177, 20);
            this.txtMatKhauConfirm.TabIndex = 2;
            this.txtMatKhauConfirm.UseSystemPasswordChar = true;
            // 
            // btnChangePass
            // 
            this.btnChangePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(87)))), ((int)(((byte)(224)))));
            this.btnChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePass.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnChangePass.Location = new System.Drawing.Point(158, 164);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(102, 28);
            this.btnChangePass.TabIndex = 3;
            this.btnChangePass.Text = "Đổi mật khẩu";
            this.btnChangePass.UseVisualStyleBackColor = false;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // FrmGiangVienChangePass
            // 
            this.AcceptButton = this.btnChangePass;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 204);
            this.Controls.Add(this.btnChangePass);
            this.Controls.Add(this.txtMatKhauConfirm);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.txtMatKhauHienTai);
            this.Controls.Add(this.lblHide);
            this.Controls.Add(this.txtMatKhauMoi2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmGiangVienChangePass";
            this.Text = "Đổi mật khẩu người dùng";
            this.Load += new System.EventHandler(this.FrmGiangVienChangePass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtMatKhauMoi2;
        private System.Windows.Forms.Label lblHide;
        private System.Windows.Forms.TextBox txtMatKhauHienTai;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.TextBox txtMatKhauConfirm;
        private System.Windows.Forms.Button btnChangePass;
    }
}