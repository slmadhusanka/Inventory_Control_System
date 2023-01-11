namespace Inventory_Control_System
{
    partial class Frm_Photocopy_Report
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.LgDisplayName = new System.Windows.Forms.Label();
            this.LgUser = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PickerDateTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.PickerDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CrystalReVie_Stationary = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtItemIDWise = new System.Windows.Forms.RadioButton();
            this.CmbItemID = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblItemID = new System.Windows.Forms.Label();
            this.rbtinvoice = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbInvoiceID = new System.Windows.Forms.ComboBox();
            this.rbtByinvoice = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtItemId = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LgDisplayName);
            this.panel2.Controls.Add(this.LgUser);
            this.panel2.Controls.Add(this.pictureBox7);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Location = new System.Drawing.Point(-1, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1153, 71);
            this.panel2.TabIndex = 173;
            // 
            // LgDisplayName
            // 
            this.LgDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgDisplayName.AutoSize = true;
            this.LgDisplayName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgDisplayName.ForeColor = System.Drawing.Color.Black;
            this.LgDisplayName.Location = new System.Drawing.Point(980, 38);
            this.LgDisplayName.Name = "LgDisplayName";
            this.LgDisplayName.Size = new System.Drawing.Size(105, 21);
            this.LgDisplayName.TabIndex = 32;
            this.LgDisplayName.Text = "DispalyName";
            // 
            // LgUser
            // 
            this.LgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgUser.AutoSize = true;
            this.LgUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgUser.ForeColor = System.Drawing.Color.Black;
            this.LgUser.Location = new System.Drawing.Point(980, 8);
            this.LgUser.Name = "LgUser";
            this.LgUser.Size = new System.Drawing.Size(94, 21);
            this.LgUser.TabIndex = 31;
            this.LgUser.Text = "HideUserID";
            this.LgUser.Visible = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.Image = global::Inventory_Control_System.Properties.Resources.Account_and_Control1;
            this.pictureBox7.Location = new System.Drawing.Point(842, 31);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(35, 30);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 23;
            this.pictureBox7.TabStop = false;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(877, 38);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(96, 21);
            this.label28.TabIndex = 24;
            this.label28.Text = "Login User :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Image = global::Inventory_Control_System.Properties.Resources.Vender;
            this.label17.Location = new System.Drawing.Point(15, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(0, 24);
            this.label17.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(85, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(231, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "Stationery Issue details...";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label16.Location = new System.Drawing.Point(161, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 17);
            this.label16.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(647, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 25);
            this.label9.TabIndex = 175;
            this.label9.Text = "Stationery Issue details...";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PickerDateTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.PickerDateFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 291);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 71);
            this.groupBox1.TabIndex = 176;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Range...";
            // 
            // PickerDateTo
            // 
            this.PickerDateTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateTo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerDateTo.Location = new System.Drawing.Point(206, 26);
            this.PickerDateTo.Name = "PickerDateTo";
            this.PickerDateTo.Size = new System.Drawing.Size(97, 22);
            this.PickerDateTo.TabIndex = 169;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 166;
            this.label4.Text = ":";
            // 
            // PickerDateFrom
            // 
            this.PickerDateFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateFrom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerDateFrom.Location = new System.Drawing.Point(55, 26);
            this.PickerDateFrom.Name = "PickerDateFrom";
            this.PickerDateFrom.Size = new System.Drawing.Size(97, 22);
            this.PickerDateFrom.TabIndex = 168;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(159, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 164;
            this.label3.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 165;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 167;
            this.label2.Text = ":";
            // 
            // CrystalReVie_Stationary
            // 
            this.CrystalReVie_Stationary.ActiveViewIndex = -1;
            this.CrystalReVie_Stationary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrystalReVie_Stationary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrystalReVie_Stationary.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrystalReVie_Stationary.Location = new System.Drawing.Point(363, 176);
            this.CrystalReVie_Stationary.Name = "CrystalReVie_Stationary";
            this.CrystalReVie_Stationary.Size = new System.Drawing.Size(778, 478);
            this.CrystalReVie_Stationary.TabIndex = 177;
            this.CrystalReVie_Stationary.ToolPanelWidth = 233;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(208, 612);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 30);
            this.button1.TabIndex = 178;
            this.button1.Text = "Load The Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Enabled = false;
            this.rbtAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAll.Location = new System.Drawing.Point(6, 24);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(39, 19);
            this.rbtAll.TabIndex = 179;
            this.rbtAll.TabStop = true;
            this.rbtAll.Text = "All";
            this.rbtAll.UseVisualStyleBackColor = true;
            this.rbtAll.CheckedChanged += new System.EventHandler(this.rbtAll_CheckedChanged);
            // 
            // rbtItemIDWise
            // 
            this.rbtItemIDWise.AutoSize = true;
            this.rbtItemIDWise.Enabled = false;
            this.rbtItemIDWise.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtItemIDWise.Location = new System.Drawing.Point(6, 47);
            this.rbtItemIDWise.Name = "rbtItemIDWise";
            this.rbtItemIDWise.Size = new System.Drawing.Size(98, 19);
            this.rbtItemIDWise.TabIndex = 180;
            this.rbtItemIDWise.TabStop = true;
            this.rbtItemIDWise.Text = "Item ID Wise";
            this.rbtItemIDWise.UseVisualStyleBackColor = true;
            this.rbtItemIDWise.CheckedChanged += new System.EventHandler(this.rbtItemIDWise_CheckedChanged);
            // 
            // CmbItemID
            // 
            this.CmbItemID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbItemID.Enabled = false;
            this.CmbItemID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbItemID.FormattingEnabled = true;
            this.CmbItemID.Location = new System.Drawing.Point(120, 47);
            this.CmbItemID.Name = "CmbItemID";
            this.CmbItemID.Size = new System.Drawing.Size(182, 23);
            this.CmbItemID.TabIndex = 181;
            this.CmbItemID.SelectedIndexChanged += new System.EventHandler(this.CmbItemID_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 170;
            this.label5.Text = "Item ID :";
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemID.ForeColor = System.Drawing.Color.Maroon;
            this.lblItemID.Location = new System.Drawing.Point(116, 82);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(17, 15);
            this.lblItemID.TabIndex = 182;
            this.lblItemID.Text = "--";
            // 
            // rbtinvoice
            // 
            this.rbtinvoice.AutoSize = true;
            this.rbtinvoice.Enabled = false;
            this.rbtinvoice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtinvoice.Location = new System.Drawing.Point(8, 27);
            this.rbtinvoice.Name = "rbtinvoice";
            this.rbtinvoice.Size = new System.Drawing.Size(66, 19);
            this.rbtinvoice.TabIndex = 183;
            this.rbtinvoice.TabStop = true;
            this.rbtinvoice.Text = "Invoice";
            this.rbtinvoice.UseVisualStyleBackColor = true;
            this.rbtinvoice.CheckedChanged += new System.EventHandler(this.rbtinvoice_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.rbtAll);
            this.groupBox2.Controls.Add(this.rbtItemIDWise);
            this.groupBox2.Controls.Add(this.lblItemID);
            this.groupBox2.Controls.Add(this.CmbItemID);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(14, 477);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 115);
            this.groupBox2.TabIndex = 184;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(6, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 15);
            this.label6.TabIndex = 183;
            this.label6.Text = "Item ID Wise ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbInvoiceID);
            this.groupBox3.Controls.Add(this.rbtByinvoice);
            this.groupBox3.Controls.Add(this.rbtinvoice);
            this.groupBox3.Location = new System.Drawing.Point(12, 371);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(321, 100);
            this.groupBox3.TabIndex = 183;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "..";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(5, -2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 15);
            this.label7.TabIndex = 184;
            this.label7.Text = "Invoice ID Wise";
            // 
            // cmbInvoiceID
            // 
            this.cmbInvoiceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceID.Enabled = false;
            this.cmbInvoiceID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInvoiceID.FormattingEnabled = true;
            this.cmbInvoiceID.Location = new System.Drawing.Point(121, 63);
            this.cmbInvoiceID.Name = "cmbInvoiceID";
            this.cmbInvoiceID.Size = new System.Drawing.Size(182, 23);
            this.cmbInvoiceID.TabIndex = 183;
            // 
            // rbtByinvoice
            // 
            this.rbtByinvoice.AutoSize = true;
            this.rbtByinvoice.Enabled = false;
            this.rbtByinvoice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtByinvoice.Location = new System.Drawing.Point(8, 63);
            this.rbtByinvoice.Name = "rbtByinvoice";
            this.rbtByinvoice.Size = new System.Drawing.Size(83, 19);
            this.rbtByinvoice.TabIndex = 184;
            this.rbtByinvoice.TabStop = true;
            this.rbtByinvoice.Text = "By Invoice";
            this.rbtByinvoice.UseVisualStyleBackColor = true;
            this.rbtByinvoice.CheckedChanged += new System.EventHandler(this.rbtByinvoice_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(10, 31);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(96, 19);
            this.radioButton1.TabIndex = 185;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Invoice Wise";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.rbtItemId);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Location = new System.Drawing.Point(12, 176);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(321, 100);
            this.groupBox4.TabIndex = 186;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 15);
            this.label8.TabIndex = 185;
            this.label8.Text = "Report Category";
            // 
            // rbtItemId
            // 
            this.rbtItemId.AutoSize = true;
            this.rbtItemId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtItemId.Location = new System.Drawing.Point(10, 64);
            this.rbtItemId.Name = "rbtItemId";
            this.rbtItemId.Size = new System.Drawing.Size(98, 19);
            this.rbtItemId.TabIndex = 186;
            this.rbtItemId.TabStop = true;
            this.rbtItemId.Text = "Item ID Wise";
            this.rbtItemId.UseVisualStyleBackColor = true;
            this.rbtItemId.CheckedChanged += new System.EventHandler(this.rbtItemId_CheckedChanged);
            // 
            // Frm_Photocopy_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 666);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CrystalReVie_Stationary);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Name = "Frm_Photocopy_Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photocopy Report Detail Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Photocopy_Report_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label LgDisplayName;
        public System.Windows.Forms.Label LgUser;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker PickerDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker PickerDateFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReVie_Stationary;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtItemIDWise;
        private System.Windows.Forms.ComboBox CmbItemID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.RadioButton rbtinvoice;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbInvoiceID;
        private System.Windows.Forms.RadioButton rbtByinvoice;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtItemId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}