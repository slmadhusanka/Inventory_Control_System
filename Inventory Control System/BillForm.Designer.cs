namespace Inventory_Control_System
{
    partial class BillForm
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
            this.BillCrystalReportViwer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CustomerInvoice2 = new Inventory_Control_System.CustomerInvoice();
            this.CustomerInvoice1 = new Inventory_Control_System.CustomerInvoice();
            this.SuspendLayout();
            // 
            // BillCrystalReportViwer
            // 
            this.BillCrystalReportViwer.ActiveViewIndex = -1;
            this.BillCrystalReportViwer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BillCrystalReportViwer.Cursor = System.Windows.Forms.Cursors.Default;
            this.BillCrystalReportViwer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BillCrystalReportViwer.Location = new System.Drawing.Point(0, 0);
            this.BillCrystalReportViwer.Name = "BillCrystalReportViwer";
            this.BillCrystalReportViwer.ShowCloseButton = false;
            this.BillCrystalReportViwer.ShowLogo = false;
            this.BillCrystalReportViwer.Size = new System.Drawing.Size(1205, 741);
            this.BillCrystalReportViwer.TabIndex = 0;
            // 
            // BillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 741);
            this.Controls.Add(this.BillCrystalReportViwer);
            this.Name = "BillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Customer Invoice";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BillForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomerInvoice CustomerInvoice1;
        private CustomerInvoice CustomerInvoice2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer BillCrystalReportViwer;
    }
}