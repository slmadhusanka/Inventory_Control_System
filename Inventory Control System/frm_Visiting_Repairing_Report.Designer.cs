namespace Inventory_Control_System
{
    partial class frm_Visiting_Repairing_Report
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.viwer_Visiting_Repairing_Report = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 92);
            this.panel1.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Inventory_Control_System.Properties.Resources.Inventory1;
            this.pictureBox1.Location = new System.Drawing.Point(8, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Image = global::Inventory_Control_System.Properties.Resources.Vender;
            this.label17.Location = new System.Drawing.Point(27, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(0, 24);
            this.label17.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(112, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(237, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "Visiting Repairing Report";
            // 
            // viwer_Visiting_Repairing_Report
            // 
            this.viwer_Visiting_Repairing_Report.ActiveViewIndex = -1;
            this.viwer_Visiting_Repairing_Report.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viwer_Visiting_Repairing_Report.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viwer_Visiting_Repairing_Report.Cursor = System.Windows.Forms.Cursors.Default;
            this.viwer_Visiting_Repairing_Report.Location = new System.Drawing.Point(12, 98);
            this.viwer_Visiting_Repairing_Report.Name = "viwer_Visiting_Repairing_Report";
            this.viwer_Visiting_Repairing_Report.Size = new System.Drawing.Size(1022, 446);
            this.viwer_Visiting_Repairing_Report.TabIndex = 11;
            // 
            // frm_Visiting_Repairing_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 556);
            this.Controls.Add(this.viwer_Visiting_Repairing_Report);
            this.Controls.Add(this.panel1);
            this.Name = "frm_Visiting_Repairing_Report";
            this.Text = "Visiting Repairing Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Visiting_Repairing_Report_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer viwer_Visiting_Repairing_Report;
    }
}