namespace Inventory_Control_System
{
    partial class RptRepairJobFinal
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
            this.RptNewRepairJobFinalBill1 = new Inventory_Control_System.RptNewRepairJobFinalBill();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RptRepairJobAdding1 = new Inventory_Control_System.RptRepairJobAdding();
            this.RptRepairJobFinalBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.RptRepairJobFinalBill2 = new Inventory_Control_System.RptRepairJobFinalBill();
            this.RptRepairJobFinalBill1 = new Inventory_Control_System.RptRepairJobFinalBill();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(37, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 46);
            this.button2.TabIndex = 178;
            this.button2.Text = "Original Copy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(192, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 46);
            this.button1.TabIndex = 178;
            this.button1.Text = "Customer Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(409, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 40);
            this.label1.TabIndex = 179;
            this.label1.Text = "Print the JOB Invoice";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RptRepairJobFinalBill
            // 
            this.RptRepairJobFinalBill.ActiveViewIndex = 0;
            this.RptRepairJobFinalBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RptRepairJobFinalBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.RptRepairJobFinalBill.Location = new System.Drawing.Point(1, 71);
            this.RptRepairJobFinalBill.Name = "RptRepairJobFinalBill";
            this.RptRepairJobFinalBill.ReportSource = this.RptRepairJobFinalBill2;
            this.RptRepairJobFinalBill.Size = new System.Drawing.Size(1163, 725);
            this.RptRepairJobFinalBill.TabIndex = 0;
            this.RptRepairJobFinalBill.Load += new System.EventHandler(this.RptRepairJobFinalBill_Load);
            // 
            // RptRepairJobFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 749);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.RptRepairJobFinalBill);
            this.Name = "RptRepairJobFinal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repair Job Invoice";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RptRepairJobFinal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer RptRepairJobFinalBill;
        private RptRepairJobFinalBill RptRepairJobFinalBill1;
        private RptNewRepairJobFinalBill RptNewRepairJobFinalBill1;
        private RptRepairJobFinalBill RptRepairJobFinalBill2;
        private RptRepairJobAdding RptRepairJobAdding1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}