namespace Inventory_Control_System
{
    partial class frmCustomerCreditPayment
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
            this.ViwerCustomerCreditPayment = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ViwerCustomerCreditPayment
            // 
            this.ViwerCustomerCreditPayment.ActiveViewIndex = -1;
            this.ViwerCustomerCreditPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViwerCustomerCreditPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViwerCustomerCreditPayment.Cursor = System.Windows.Forms.Cursors.Default;
            this.ViwerCustomerCreditPayment.Location = new System.Drawing.Point(265, 82);
            this.ViwerCustomerCreditPayment.Name = "ViwerCustomerCreditPayment";
            this.ViwerCustomerCreditPayment.Size = new System.Drawing.Size(653, 512);
            this.ViwerCustomerCreditPayment.TabIndex = 0;
            // 
            // frmCustomerCreditPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 606);
            this.Controls.Add(this.ViwerCustomerCreditPayment);
            this.Name = "frmCustomerCreditPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Credit Payment Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCustomerCreditPayment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ViwerCustomerCreditPayment;
    }
}