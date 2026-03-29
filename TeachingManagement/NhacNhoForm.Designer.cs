namespace TeachingManagement
{
    partial class NhacNhoForm
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.lvThongBao = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhắc nhở";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(26, 53);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(37, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Text...";
            // 
            // lvThongBao
            // 
            this.lvThongBao.FullRowSelect = true;
            this.lvThongBao.GridLines = true;
            this.lvThongBao.HideSelection = false;
            this.lvThongBao.Location = new System.Drawing.Point(29, 99);
            this.lvThongBao.Name = "lvThongBao";
            this.lvThongBao.OwnerDraw = true;
            this.lvThongBao.Size = new System.Drawing.Size(748, 430);
            this.lvThongBao.TabIndex = 0;
            this.lvThongBao.UseCompatibleStateImageBehavior = false;
            this.lvThongBao.View = System.Windows.Forms.View.Details;
            this.lvThongBao.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvThongBao_DrawColumnHeader);
            this.lvThongBao.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvThongBao_DrawItem);
            this.lvThongBao.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvThongBao_DrawSubItem);
            // 
            // NhacNhoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 541);
            this.Controls.Add(this.lvThongBao);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.label1);
            this.Name = "NhacNhoForm";
            this.Text = "ThongBaoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NhacNhoForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NhacNhoForm_FormClosed);
            this.Load += new System.EventHandler(this.ThongBaoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ListView lvThongBao;
    }
}