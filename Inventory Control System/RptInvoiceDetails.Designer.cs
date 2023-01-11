namespace Inventory_Control_System
{
    partial class RptInvoiceDetails
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
            this.button1 = new System.Windows.Forms.Button();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RptInvoicesVeivew = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.InvoiceSold1 = new Inventory_Control_System.InvoiceSold();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(453, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load the Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateFrom
            // 
            this.dateFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFrom.Location = new System.Drawing.Point(113, 45);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(103, 25);
            this.dateFrom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";
            // 
            // DateTo
            // 
            this.DateTo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTo.Location = new System.Drawing.Point(313, 45);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(103, 25);
            this.DateTo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(268, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(297, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-5, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1559, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "_________________________________________________________________________________" +
    "________________________________________________________________________________" +
    "_________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(340, 348);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(462, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "Please Select the Date range and submit it";
            // 
            // RptInvoicesVeivew
            // 
            this.RptInvoicesVeivew.ActiveViewIndex = 0;
            this.RptInvoicesVeivew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RptInvoicesVeivew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RptInvoicesVeivew.Cursor = System.Windows.Forms.Cursors.Default;
            this.RptInvoicesVeivew.Location = new System.Drawing.Point(12, 106);
            this.RptInvoicesVeivew.Name = "RptInvoicesVeivew";
            this.RptInvoicesVeivew.ReportSource = this.InvoiceSold1;
            this.RptInvoicesVeivew.Size = new System.Drawing.Size(1174, 577);
            this.RptInvoicesVeivew.TabIndex = 0;
            this.RptInvoicesVeivew.Visible = false;
            // 
            // InvoiceSold1
            // 
            this.InvoiceSold1.InitReport += new System.EventHandler(this.InvoiceSold1_InitReport);
            // 
            // RptInvoiceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 682);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RptInvoicesVeivew);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RptInvoiceDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Details Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RptInvoiceDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer RptInvoicesVeivew;
        private InvoiceSold InvoiceSold1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}