namespace Inventory_Control_System
{
    partial class rptItemWiseSaleReport
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
            this.ViwerItemWiseSaleReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.RBtAllDate = new System.Windows.Forms.RadioButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.rbtFromDate = new System.Windows.Forms.RadioButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtAllItems = new System.Windows.Forms.RadioButton();
            this.rbtItemWise = new System.Windows.Forms.RadioButton();
            this.RbtAlllCategory = new System.Windows.Forms.RadioButton();
            this.rbtByCategory = new System.Windows.Forms.RadioButton();
            this.cmbItemWise = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.IblItemID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LgDisplayName = new System.Windows.Forms.Label();
            this.LgUser = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViwerItemWiseSaleReport
            // 
            this.ViwerItemWiseSaleReport.ActiveViewIndex = -1;
            this.ViwerItemWiseSaleReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViwerItemWiseSaleReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViwerItemWiseSaleReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.ViwerItemWiseSaleReport.Location = new System.Drawing.Point(346, 101);
            this.ViwerItemWiseSaleReport.Name = "ViwerItemWiseSaleReport";
            this.ViwerItemWiseSaleReport.Size = new System.Drawing.Size(728, 475);
            this.ViwerItemWiseSaleReport.TabIndex = 0;
            // 
            // RBtAllDate
            // 
            this.RBtAllDate.AutoSize = true;
            this.RBtAllDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.RBtAllDate.Location = new System.Drawing.Point(12, 25);
            this.RBtAllDate.Name = "RBtAllDate";
            this.RBtAllDate.Size = new System.Drawing.Size(74, 19);
            this.RBtAllDate.TabIndex = 1;
            this.RBtAllDate.TabStop = true;
            this.RBtAllDate.Text = "All Dates";
            this.RBtAllDate.UseVisualStyleBackColor = true;
            this.RBtAllDate.CheckedChanged += new System.EventHandler(this.RBtAllItem_CheckedChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(190, 477);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(147, 37);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Load The Report";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // rbtFromDate
            // 
            this.rbtFromDate.AutoSize = true;
            this.rbtFromDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rbtFromDate.Location = new System.Drawing.Point(12, 60);
            this.rbtFromDate.Name = "rbtFromDate";
            this.rbtFromDate.Size = new System.Drawing.Size(60, 19);
            this.rbtFromDate.TabIndex = 3;
            this.rbtFromDate.TabStop = true;
            this.rbtFromDate.Text = "From :";
            this.rbtFromDate.UseVisualStyleBackColor = true;
            this.rbtFromDate.CheckedChanged += new System.EventHandler(this.rbtFromDate_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(72, 57);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(104, 23);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(218, 56);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(91, 23);
            this.dateTimePicker2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "To";
            // 
            // rbtAllItems
            // 
            this.rbtAllItems.AutoSize = true;
            this.rbtAllItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rbtAllItems.Location = new System.Drawing.Point(10, 29);
            this.rbtAllItems.Name = "rbtAllItems";
            this.rbtAllItems.Size = new System.Drawing.Size(74, 19);
            this.rbtAllItems.TabIndex = 8;
            this.rbtAllItems.TabStop = true;
            this.rbtAllItems.Text = "All Items";
            this.rbtAllItems.UseVisualStyleBackColor = true;
            this.rbtAllItems.CheckedChanged += new System.EventHandler(this.rbtAllItems_CheckedChanged);
            // 
            // rbtItemWise
            // 
            this.rbtItemWise.AutoSize = true;
            this.rbtItemWise.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rbtItemWise.Location = new System.Drawing.Point(10, 52);
            this.rbtItemWise.Name = "rbtItemWise";
            this.rbtItemWise.Size = new System.Drawing.Size(87, 19);
            this.rbtItemWise.TabIndex = 9;
            this.rbtItemWise.TabStop = true;
            this.rbtItemWise.Text = "Items Wise";
            this.rbtItemWise.UseVisualStyleBackColor = true;
            this.rbtItemWise.CheckedChanged += new System.EventHandler(this.rbtItemWise_CheckedChanged);
            // 
            // RbtAlllCategory
            // 
            this.RbtAlllCategory.AutoSize = true;
            this.RbtAlllCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.RbtAlllCategory.Location = new System.Drawing.Point(12, 22);
            this.RbtAlllCategory.Name = "RbtAlllCategory";
            this.RbtAlllCategory.Size = new System.Drawing.Size(92, 19);
            this.RbtAlllCategory.TabIndex = 10;
            this.RbtAlllCategory.TabStop = true;
            this.RbtAlllCategory.Text = "All Category";
            this.RbtAlllCategory.UseVisualStyleBackColor = true;
            this.RbtAlllCategory.CheckedChanged += new System.EventHandler(this.RbtAlllCategory_CheckedChanged);
            // 
            // rbtByCategory
            // 
            this.rbtByCategory.AutoSize = true;
            this.rbtByCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rbtByCategory.Location = new System.Drawing.Point(10, 47);
            this.rbtByCategory.Name = "rbtByCategory";
            this.rbtByCategory.Size = new System.Drawing.Size(92, 19);
            this.rbtByCategory.TabIndex = 11;
            this.rbtByCategory.TabStop = true;
            this.rbtByCategory.Text = "By Category";
            this.rbtByCategory.UseVisualStyleBackColor = true;
            this.rbtByCategory.CheckedChanged += new System.EventHandler(this.rbtByCategory_CheckedChanged);
            // 
            // cmbItemWise
            // 
            this.cmbItemWise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemWise.Enabled = false;
            this.cmbItemWise.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmbItemWise.FormattingEnabled = true;
            this.cmbItemWise.Location = new System.Drawing.Point(108, 51);
            this.cmbItemWise.Name = "cmbItemWise";
            this.cmbItemWise.Size = new System.Drawing.Size(196, 23);
            this.cmbItemWise.TabIndex = 12;
            this.cmbItemWise.SelectedIndexChanged += new System.EventHandler(this.cmbItemWise_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.RBtAllDate);
            this.groupBox1.Controls.Add(this.rbtFromDate);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(7, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Date Wise";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.IblItemID);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbItemWise);
            this.groupBox2.Controls.Add(this.rbtAllItems);
            this.groupBox2.Controls.Add(this.rbtItemWise);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(6, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(9, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Items Wise...";
            // 
            // IblItemID
            // 
            this.IblItemID.AutoSize = true;
            this.IblItemID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.IblItemID.Location = new System.Drawing.Point(100, 78);
            this.IblItemID.Name = "IblItemID";
            this.IblItemID.Size = new System.Drawing.Size(17, 15);
            this.IblItemID.TabIndex = 15;
            this.IblItemID.Text = "--";
            this.IblItemID.Click += new System.EventHandler(this.IblItemID_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(31, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Item ID :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbCategory);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.rbtByCategory);
            this.groupBox3.Controls.Add(this.RbtAlllCategory);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(6, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 78);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Enabled = false;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(108, 46);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(196, 23);
            this.cmbCategory.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(9, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Category Wise...";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(11, 208);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(326, 250);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filter By.....";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.LgDisplayName);
            this.panel1.Controls.Add(this.LgUser);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 75);
            this.panel1.TabIndex = 85;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // LgDisplayName
            // 
            this.LgDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgDisplayName.AutoSize = true;
            this.LgDisplayName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgDisplayName.ForeColor = System.Drawing.Color.Black;
            this.LgDisplayName.Location = new System.Drawing.Point(962, 37);
            this.LgDisplayName.Name = "LgDisplayName";
            this.LgDisplayName.Size = new System.Drawing.Size(87, 17);
            this.LgDisplayName.TabIndex = 36;
            this.LgDisplayName.Text = "DispalyName";
            // 
            // LgUser
            // 
            this.LgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgUser.AutoSize = true;
            this.LgUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgUser.ForeColor = System.Drawing.Color.Black;
            this.LgUser.Location = new System.Drawing.Point(993, 14);
            this.LgUser.Name = "LgUser";
            this.LgUser.Size = new System.Drawing.Size(76, 17);
            this.LgUser.TabIndex = 35;
            this.LgUser.Text = "HideUserID";
            this.LgUser.Visible = false;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(888, 37);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(79, 17);
            this.label28.TabIndex = 34;
            this.label28.Text = "Login User :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(11, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(0, 24);
            this.label17.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(439, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(206, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "Item Wise Sale  Report";
            // 
            // rptItemWiseSaleReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 588);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ViwerItemWiseSaleReport);
            this.Name = "rptItemWiseSaleReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Wise Sale";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.rptItemWiseSaleReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ViwerItemWiseSaleReport;
        private System.Windows.Forms.RadioButton RBtAllDate;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RadioButton rbtFromDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtAllItems;
        private System.Windows.Forms.RadioButton rbtItemWise;
        private System.Windows.Forms.RadioButton RbtAlllCategory;
        private System.Windows.Forms.RadioButton rbtByCategory;
        private System.Windows.Forms.ComboBox cmbItemWise;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label IblItemID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label LgDisplayName;
        public System.Windows.Forms.Label LgUser;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
    }
}