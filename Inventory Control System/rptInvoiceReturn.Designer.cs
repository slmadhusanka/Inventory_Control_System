namespace Inventory_Control_System
{
    partial class rptInvoiceReturn
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
            this.ReportViewInvoiceReturn = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.RPTinvoiceReturnNote1 = new Inventory_Control_System.RPTinvoiceReturnNote();
            this.SuspendLayout();
            // 
            // ReportViewInvoiceReturn
            // 
            this.ReportViewInvoiceReturn.ActiveViewIndex = 0;
            this.ReportViewInvoiceReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportViewInvoiceReturn.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportViewInvoiceReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewInvoiceReturn.Location = new System.Drawing.Point(0, 0);
            this.ReportViewInvoiceReturn.Name = "ReportViewInvoiceReturn";
            this.ReportViewInvoiceReturn.ReportSource = this.RPTinvoiceReturnNote1;
            this.ReportViewInvoiceReturn.Size = new System.Drawing.Size(1007, 506);
            this.ReportViewInvoiceReturn.TabIndex = 0;
            // 
            // RPTinvoiceReturnNote1
            // 
            this.RPTinvoiceReturnNote1.InitReport += new System.EventHandler(this.RPTinvoiceReturnNote1_InitReport);
            // 
            // rptInvoiceReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 506);
            this.Controls.Add(this.ReportViewInvoiceReturn);
            this.Name = "rptInvoiceReturn";
            this.Text = "Invoice Return";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rptInvoiceReturn_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportViewInvoiceReturn;
        private RPTinvoiceReturnNote RPTinvoiceReturnNote1;
    }
}