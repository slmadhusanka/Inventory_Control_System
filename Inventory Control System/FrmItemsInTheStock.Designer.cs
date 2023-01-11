namespace Inventory_Control_System
{
    partial class FrmItemsInTheStock
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
            this.ReportItems = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.StockItem1 = new Inventory_Control_System.StockItem();
            this.SuspendLayout();
            // 
            // ReportItems
            // 
            this.ReportItems.ActiveViewIndex = 0;
            this.ReportItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportItems.Location = new System.Drawing.Point(0, 0);
            this.ReportItems.Name = "ReportItems";
            this.ReportItems.ReportSource = this.StockItem1;
            this.ReportItems.Size = new System.Drawing.Size(1250, 658);
            this.ReportItems.TabIndex = 0;
            // 
            // FrmItemsInTheStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 658);
            this.Controls.Add(this.ReportItems);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmItemsInTheStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Items In The Stock Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmItemsInTheStock_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReportItems;
        private StockItem StockItem1;
       // private Inventory_Control_System.RptItemsInTheStock RptItemsInTheStock1;
    }
}