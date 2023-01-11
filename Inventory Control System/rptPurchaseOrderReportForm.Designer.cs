namespace Inventory_Control_System
{
    partial class rptPurchaseOrderReportForm
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
            this.viewPurchaseOrderReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // viewPurchaseOrderReport
            // 
            this.viewPurchaseOrderReport.ActiveViewIndex = -1;
            this.viewPurchaseOrderReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewPurchaseOrderReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewPurchaseOrderReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.viewPurchaseOrderReport.Location = new System.Drawing.Point(0, 0);
            this.viewPurchaseOrderReport.Name = "viewPurchaseOrderReport";
            this.viewPurchaseOrderReport.Size = new System.Drawing.Size(1037, 478);
            this.viewPurchaseOrderReport.TabIndex = 0;
            // 
            // rptPurchaseOrderReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 478);
            this.Controls.Add(this.viewPurchaseOrderReport);
            this.Name = "rptPurchaseOrderReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rptPurchaseOrderReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer viewPurchaseOrderReport;
    }
}