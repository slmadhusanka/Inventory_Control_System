namespace Inventory_Control_System
{
    partial class FrmCurrentStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCurrentStock));
            this.viwerWholsaleStockReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.rbtWholeSaleCurrentStock = new System.Windows.Forms.RadioButton();
            this.rbtItemsCurStock = new System.Windows.Forms.RadioButton();
            this.rbtCatogery = new System.Windows.Forms.RadioButton();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbItemWise = new System.Windows.Forms.ComboBox();
            this.rbtAllCategory = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IblItemID = new System.Windows.Forms.Label();
            this.rbtbyItems = new System.Windows.Forms.RadioButton();
            this.rbtAllItems = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.LgDisplayName = new System.Windows.Forms.Label();
            this.LgUser = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // viwerWholsaleStockReport
            // 
            this.viwerWholsaleStockReport.ActiveViewIndex = -1;
            this.viwerWholsaleStockReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viwerWholsaleStockReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viwerWholsaleStockReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.viwerWholsaleStockReport.Location = new System.Drawing.Point(315, 115);
            this.viwerWholsaleStockReport.Name = "viwerWholsaleStockReport";
            this.viwerWholsaleStockReport.Size = new System.Drawing.Size(700, 471);
            this.viwerWholsaleStockReport.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(197, 545);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(103, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Load The Report";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // rbtWholeSaleCurrentStock
            // 
            this.rbtWholeSaleCurrentStock.AutoSize = true;
            this.rbtWholeSaleCurrentStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtWholeSaleCurrentStock.Location = new System.Drawing.Point(15, 27);
            this.rbtWholeSaleCurrentStock.Name = "rbtWholeSaleCurrentStock";
            this.rbtWholeSaleCurrentStock.Size = new System.Drawing.Size(146, 19);
            this.rbtWholeSaleCurrentStock.TabIndex = 2;
            this.rbtWholeSaleCurrentStock.TabStop = true;
            this.rbtWholeSaleCurrentStock.Text = "Total Avarage balance";
            this.rbtWholeSaleCurrentStock.UseVisualStyleBackColor = true;
            // 
            // rbtItemsCurStock
            // 
            this.rbtItemsCurStock.AutoSize = true;
            this.rbtItemsCurStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtItemsCurStock.Location = new System.Drawing.Point(14, 62);
            this.rbtItemsCurStock.Name = "rbtItemsCurStock";
            this.rbtItemsCurStock.Size = new System.Drawing.Size(208, 19);
            this.rbtItemsCurStock.TabIndex = 3;
            this.rbtItemsCurStock.TabStop = true;
            this.rbtItemsCurStock.Text = "Items Current Stock (with Serial)\r\n";
            this.rbtItemsCurStock.UseVisualStyleBackColor = true;
            // 
            // rbtCatogery
            // 
            this.rbtCatogery.AutoSize = true;
            this.rbtCatogery.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCatogery.Location = new System.Drawing.Point(6, 59);
            this.rbtCatogery.Name = "rbtCatogery";
            this.rbtCatogery.Size = new System.Drawing.Size(95, 19);
            this.rbtCatogery.TabIndex = 4;
            this.rbtCatogery.TabStop = true;
            this.rbtCatogery.Text = " By Category";
            this.rbtCatogery.UseVisualStyleBackColor = true;
            this.rbtCatogery.CheckedChanged += new System.EventHandler(this.rbtCatogery_CheckedChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Enabled = false;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(107, 59);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(163, 23);
            this.cmbCategory.TabIndex = 5;
            // 
            // cmbItemWise
            // 
            this.cmbItemWise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemWise.Enabled = false;
            this.cmbItemWise.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbItemWise.FormattingEnabled = true;
            this.cmbItemWise.Location = new System.Drawing.Point(107, 52);
            this.cmbItemWise.Name = "cmbItemWise";
            this.cmbItemWise.Size = new System.Drawing.Size(163, 23);
            this.cmbItemWise.TabIndex = 6;
            this.cmbItemWise.SelectedIndexChanged += new System.EventHandler(this.cmbItemWise_SelectedIndexChanged);
            // 
            // rbtAllCategory
            // 
            this.rbtAllCategory.AutoSize = true;
            this.rbtAllCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAllCategory.Location = new System.Drawing.Point(6, 24);
            this.rbtAllCategory.Name = "rbtAllCategory";
            this.rbtAllCategory.Size = new System.Drawing.Size(95, 19);
            this.rbtAllCategory.TabIndex = 7;
            this.rbtAllCategory.TabStop = true;
            this.rbtAllCategory.Text = " All Category";
            this.rbtAllCategory.UseVisualStyleBackColor = true;
            this.rbtAllCategory.CheckedChanged += new System.EventHandler(this.rbtAllCategory_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtAllCategory);
            this.groupBox1.Controls.Add(this.rbtCatogery);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbCategory);
            this.groupBox1.Location = new System.Drawing.Point(9, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(4, -2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Category Wise...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.IblItemID);
            this.groupBox2.Controls.Add(this.rbtbyItems);
            this.groupBox2.Controls.Add(this.rbtAllItems);
            this.groupBox2.Controls.Add(this.cmbItemWise);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 101);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(6, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Items Wise...";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Item ";
            // 
            // IblItemID
            // 
            this.IblItemID.AutoSize = true;
            this.IblItemID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.IblItemID.Location = new System.Drawing.Point(104, 83);
            this.IblItemID.Name = "IblItemID";
            this.IblItemID.Size = new System.Drawing.Size(17, 15);
            this.IblItemID.TabIndex = 10;
            this.IblItemID.Text = "--";
            // 
            // rbtbyItems
            // 
            this.rbtbyItems.AutoSize = true;
            this.rbtbyItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtbyItems.Location = new System.Drawing.Point(6, 52);
            this.rbtbyItems.Name = "rbtbyItems";
            this.rbtbyItems.Size = new System.Drawing.Size(74, 19);
            this.rbtbyItems.TabIndex = 9;
            this.rbtbyItems.TabStop = true;
            this.rbtbyItems.Text = "By Items";
            this.rbtbyItems.UseVisualStyleBackColor = true;
            this.rbtbyItems.CheckedChanged += new System.EventHandler(this.rbtbyItems_CheckedChanged);
            // 
            // rbtAllItems
            // 
            this.rbtAllItems.AutoSize = true;
            this.rbtAllItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAllItems.Location = new System.Drawing.Point(6, 18);
            this.rbtAllItems.Name = "rbtAllItems";
            this.rbtAllItems.Size = new System.Drawing.Size(74, 19);
            this.rbtAllItems.TabIndex = 8;
            this.rbtAllItems.TabStop = true;
            this.rbtAllItems.Text = "All Items";
            this.rbtAllItems.UseVisualStyleBackColor = true;
            this.rbtAllItems.CheckedChanged += new System.EventHandler(this.rbtAllItems_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rbtItemsCurStock);
            this.groupBox3.Controls.Add(this.rbtWholeSaleCurrentStock);
            this.groupBox3.Location = new System.Drawing.Point(12, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(297, 138);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(14, 97);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(165, 19);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "WholeSale Current Stock";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(6, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Stock Wise...";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Location = new System.Drawing.Point(12, 271);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(297, 251);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(6, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Filter By...";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.LgDisplayName);
            this.panel5.Controls.Add(this.LgUser);
            this.panel5.Controls.Add(this.pictureBox7);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Location = new System.Drawing.Point(-1, -1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1026, 85);
            this.panel5.TabIndex = 154;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(97, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(482, 37);
            this.label6.TabIndex = 35;
            this.label6.Text = "Current Stock (Wholesale OR Items )";
            // 
            // LgDisplayName
            // 
            this.LgDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgDisplayName.AutoSize = true;
            this.LgDisplayName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgDisplayName.ForeColor = System.Drawing.Color.Black;
            this.LgDisplayName.Location = new System.Drawing.Point(906, 41);
            this.LgDisplayName.Name = "LgDisplayName";
            this.LgDisplayName.Size = new System.Drawing.Size(105, 21);
            this.LgDisplayName.TabIndex = 34;
            this.LgDisplayName.Text = "DispalyName";
            // 
            // LgUser
            // 
            this.LgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgUser.AutoSize = true;
            this.LgUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgUser.ForeColor = System.Drawing.Color.Black;
            this.LgUser.Location = new System.Drawing.Point(910, 16);
            this.LgUser.Name = "LgUser";
            this.LgUser.Size = new System.Drawing.Size(94, 21);
            this.LgUser.TabIndex = 33;
            this.LgUser.Text = "HideUserID";
            this.LgUser.Visible = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(791, 38);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(26, 26);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 31;
            this.pictureBox7.TabStop = false;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(816, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(96, 21);
            this.label28.TabIndex = 32;
            this.label28.Text = "Login User :";
            // 
            // FrmCurrentStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 598);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.viwerWholsaleStockReport);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmCurrentStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Current  Stock";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCurrentStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer viwerWholsaleStockReport;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RadioButton rbtWholeSaleCurrentStock;
        private System.Windows.Forms.RadioButton rbtItemsCurStock;
        private System.Windows.Forms.RadioButton rbtCatogery;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbItemWise;
        private System.Windows.Forms.RadioButton rbtAllCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtbyItems;
        private System.Windows.Forms.RadioButton rbtAllItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label IblItemID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label LgDisplayName;
        public System.Windows.Forms.Label LgUser;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}