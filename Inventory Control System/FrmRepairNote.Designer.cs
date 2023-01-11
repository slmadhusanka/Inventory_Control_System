namespace Inventory_Control_System
{
    partial class FrmRepairNote
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
            this.RepairItmViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.RptRepairNote1 = new Inventory_Control_System.RptRepairNote();
            this.SuspendLayout();
            // 
            // RepairItmViewer
            // 
            this.RepairItmViewer.ActiveViewIndex = 0;
            this.RepairItmViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RepairItmViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.RepairItmViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RepairItmViewer.Location = new System.Drawing.Point(0, 0);
            this.RepairItmViewer.Name = "RepairItmViewer";
            this.RepairItmViewer.ReportSource = this.RptRepairNote1;
            this.RepairItmViewer.Size = new System.Drawing.Size(1120, 606);
            this.RepairItmViewer.TabIndex = 0;
            // 
            // FrmRepairNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 606);
            this.Controls.Add(this.RepairItmViewer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRepairNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repair Note";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmRepairNote_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer RepairItmViewer;
        private RptRepairNote RptRepairNote1;
    }
}