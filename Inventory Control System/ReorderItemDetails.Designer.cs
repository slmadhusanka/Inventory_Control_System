namespace Inventory_Control_System
{
    partial class ReorderItemDetails
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
            this.crystalReportReOrder = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.ReOrderItems1 = new Inventory_Control_System.ReOrderItems();
            this.SuspendLayout();
            // 
            // crystalReportReOrder
            // 
            this.crystalReportReOrder.ActiveViewIndex = 0;
            this.crystalReportReOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportReOrder.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportReOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportReOrder.Location = new System.Drawing.Point(0, 0);
            this.crystalReportReOrder.Name = "crystalReportReOrder";
            this.crystalReportReOrder.ReportSource = this.ReOrderItems1;
            this.crystalReportReOrder.Size = new System.Drawing.Size(1289, 609);
            this.crystalReportReOrder.TabIndex = 0;
            // 
            // ReOrderItems1
            // 
            this.ReOrderItems1.InitReport += new System.EventHandler(this.ReOrderItems1_InitReport);
            // 
            // ReorderItemDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 609);
            this.Controls.Add(this.crystalReportReOrder);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReorderItemDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reorder Item Details";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReorderItemDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportReOrder;
        private ReOrderItems ReOrderItems1;
    }
}