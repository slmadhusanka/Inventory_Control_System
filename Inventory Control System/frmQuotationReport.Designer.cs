namespace Inventory_Control_System
{
    partial class frmQuotationReport
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
            this.ViwerQuotationReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViwerQuotationReport
            // 
            this.ViwerQuotationReport.ActiveViewIndex = -1;
            this.ViwerQuotationReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViwerQuotationReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViwerQuotationReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.ViwerQuotationReport.Location = new System.Drawing.Point(12, 88);
            this.ViwerQuotationReport.Name = "ViwerQuotationReport";
            this.ViwerQuotationReport.Size = new System.Drawing.Size(1007, 433);
            this.ViwerQuotationReport.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel5.Controls.Add(this.label15);
            this.panel5.Location = new System.Drawing.Point(-1, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1036, 75);
            this.panel5.TabIndex = 216;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(412, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(170, 25);
            this.label15.TabIndex = 2;
            this.label15.Text = "Quotation Report";
            // 
            // frmQuotationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 533);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.ViwerQuotationReport);
            this.Name = "frmQuotationReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quotation Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmQuotationReport_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ViwerQuotationReport;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
    }
}