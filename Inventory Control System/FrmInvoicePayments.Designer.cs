namespace Inventory_Control_System
{
    partial class FrmInvoicePayments
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
            this.InvoicePaymentsViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.RptInvoicePayments1 = new Inventory_Control_System.RptInvoicePayments();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PickerDateFrom = new System.Windows.Forms.DateTimePicker();
            this.PickerDateTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InvoicePaymentsViewer
            // 
            this.InvoicePaymentsViewer.ActiveViewIndex = 0;
            this.InvoicePaymentsViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InvoicePaymentsViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.InvoicePaymentsViewer.Location = new System.Drawing.Point(-1, 109);
            this.InvoicePaymentsViewer.Name = "InvoicePaymentsViewer";
            this.InvoicePaymentsViewer.ReportSource = this.RptInvoicePayments1;
            this.InvoicePaymentsViewer.Size = new System.Drawing.Size(1302, 638);
            this.InvoicePaymentsViewer.TabIndex = 0;
            this.InvoicePaymentsViewer.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(470, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load The Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(292, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(92, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(263, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "From";
            // 
            // PickerDateFrom
            // 
            this.PickerDateFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerDateFrom.Location = new System.Drawing.Point(109, 67);
            this.PickerDateFrom.Name = "PickerDateFrom";
            this.PickerDateFrom.Size = new System.Drawing.Size(105, 20);
            this.PickerDateFrom.TabIndex = 9;
            // 
            // PickerDateTo
            // 
            this.PickerDateTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerDateTo.Location = new System.Drawing.Point(309, 64);
            this.PickerDateTo.Name = "PickerDateTo";
            this.PickerDateTo.Size = new System.Drawing.Size(105, 20);
            this.PickerDateTo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-4, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1559, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "_________________________________________________________________________________" +
    "________________________________________________________________________________" +
    "_________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(419, 363);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(462, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Please Select the Date range and submit it";
            // 
            // FrmInvoicePayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 749);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PickerDateTo);
            this.Controls.Add(this.PickerDateFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InvoicePaymentsViewer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInvoicePayments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Payments Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmInvoicePayments_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer InvoicePaymentsViewer;
        private RptInvoicePayments RptInvoicePayments1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker PickerDateFrom;
        private System.Windows.Forms.DateTimePicker PickerDateTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}