namespace Inventory_Control_System
{
    partial class Frm_Photocopy_Print_Invoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Photocopy_Print_Invoice));
            this.label9 = new System.Windows.Forms.Label();
            this.CrystalReVie_Print_Invoice = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LgDisplayName = new System.Windows.Forms.Label();
            this.LgUser = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(427, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 25);
            this.label9.TabIndex = 177;
            this.label9.Text = "Document Invoice";
            // 
            // CrystalReVie_Print_Invoice
            // 
            this.CrystalReVie_Print_Invoice.ActiveViewIndex = -1;
            this.CrystalReVie_Print_Invoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrystalReVie_Print_Invoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrystalReVie_Print_Invoice.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrystalReVie_Print_Invoice.Location = new System.Drawing.Point(12, 130);
            this.CrystalReVie_Print_Invoice.Name = "CrystalReVie_Print_Invoice";
            this.CrystalReVie_Print_Invoice.Size = new System.Drawing.Size(1027, 418);
            this.CrystalReVie_Print_Invoice.TabIndex = 179;
            this.CrystalReVie_Print_Invoice.ToolPanelWidth = 233;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel5.Controls.Add(this.LgDisplayName);
            this.panel5.Controls.Add(this.LgUser);
            this.panel5.Controls.Add(this.pictureBox7);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1051, 75);
            this.panel5.TabIndex = 206;
            // 
            // LgDisplayName
            // 
            this.LgDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgDisplayName.AutoSize = true;
            this.LgDisplayName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgDisplayName.ForeColor = System.Drawing.Color.Black;
            this.LgDisplayName.Location = new System.Drawing.Point(926, 41);
            this.LgDisplayName.Name = "LgDisplayName";
            this.LgDisplayName.Size = new System.Drawing.Size(105, 21);
            this.LgDisplayName.TabIndex = 34;
            this.LgDisplayName.Text = "DispalyName";
            this.LgDisplayName.Visible = false;
            // 
            // LgUser
            // 
            this.LgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgUser.AutoSize = true;
            this.LgUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgUser.ForeColor = System.Drawing.Color.Black;
            this.LgUser.Location = new System.Drawing.Point(930, 16);
            this.LgUser.Name = "LgUser";
            this.LgUser.Size = new System.Drawing.Size(94, 21);
            this.LgUser.TabIndex = 33;
            this.LgUser.Text = "HideUserID";
            this.LgUser.Visible = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(811, 38);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(26, 26);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 31;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Visible = false;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(836, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(96, 21);
            this.label28.TabIndex = 32;
            this.label28.Text = "Login User :";
            this.label28.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(31, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(178, 25);
            this.label15.TabIndex = 2;
            this.label15.Text = "Photocopy Invoice";
            // 
            // Frm_Photocopy_Print_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 560);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CrystalReVie_Print_Invoice);
            this.Name = "Frm_Photocopy_Print_Invoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Document details Invoice";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Photocopy_Print_Invoice_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReVie_Print_Invoice;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label LgDisplayName;
        public System.Windows.Forms.Label LgUser;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label28;
    }
}