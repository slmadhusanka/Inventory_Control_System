using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace Inventory_Control_System
{
    public partial class RepairNote : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public RepairNote(string GogP)
        {
            

            InitializeComponent();

            LgUser.Text = GogP;

        }

        public void ClearTxt()
        {
            #region clear text as default
            //PaymentDetails.Text="";
             //JobStatus.Text="";
            
             ReCusID.Text="";
            CusFirstName.Text="";
            ReCusName.Text = "";
            CusPersonalAddress.Text="";
            CusTelNUmber.Text="";

            // ReWarranty.Text="";
            WarrantyNo.Checked = false;
            WarrantyYes.Checked = false;

             PerchesDate.Text="";
            //clear check boes
             checkBox1.Checked = false;
             checkBox2.Checked = false;
             checkBox3.Checked = false;
             checkBox4.Checked = false;
             checkBox5.Checked = false;
             checkBox6.Checked = false;
             checkBox7.Checked = false;
             checkBox8.Checked = false;
             checkBox9.Checked = false;
             checkBox10.Checked = false;

            FaultOther.Text="";
            ReProcrssor.Text="";
            ReMemory.Text="";
             ReChassis.Text="";
            ReGraphics.Text="";
            ReMouse.Text="";
            RePowerCbl.Text="";
            RePowerBx.Text="";
            ReMBoard.Text="";
            ReHDD.Text="";
            ReRom.Text="";
            ReExtCard.Text="";
            ReKeyB.Text="";
            ReUSB.Text="";
            
            ReSystemOther.Text="";
            ReCompletingDate.Text="";
            ReCompletingTime.Text="";
           
            ReEngNote.Text="";
            ReCusNote.Text = "";

            TroSh1.Checked = false;
            TroSh2.Checked = false;
            TroSh3.Checked = false;
            TroSh4.Checked = false;
            TroSh5.Checked = false;

            checkBox13.Checked = false;

            groupBox10.Enabled = false;

            //UpdateJOB.Enabled = false;
            //NewJOB.Checked = true;
            //NewJOB.Enabled = true;
            //UpdateJOB.Checked = false;

            TroSh1.Checked = true;

            #endregion

        }


        private void button4_Click(object sender, EventArgs e)
        {

        }

        public void GenerateJOBNumbe()
        {
            #region New JOB Number...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT ReJobNumber FROM RepairNotes";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    ReJobNumber.Text = "REJ1001";
                    // PassInvoiceNumber.Text = "INV1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 ReJobNumber FROM RepairNotes order by ReJobNumber DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        ReJobNumber.Text = "REJ" + no;
                        // PassInvoiceNumber.Text = "INV" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }



        string tot;

        public void FaultSelect()
        {
            try
            {
                #region faultselection Binary order =tot

                string a, b, c, d, f, g, h, i, j, k;

                if (checkBox1.Checked == true)
                {
                    a = "1";
                }
                else
                {
                    a = "0";
                }

                if (checkBox2.Checked == true)
                {
                    b = "1";
                }
                else
                {
                    b = "0";
                }

                if (checkBox3.Checked == true)
                {
                    c = "1";
                }
                else
                {
                    c = "0";
                }
                if (checkBox4.Checked == true)
                {
                    d = "1";
                }
                else
                {
                    d = "0";
                }
                if (checkBox5.Checked == true)
                {
                    f = "1";
                }
                else
                {
                    f = "0";
                }
                if (checkBox6.Checked == true)
                {
                    g = "1";
                }
                else
                {
                    g = "0";
                }
                if (checkBox7.Checked == true)
                {
                    h = "1";
                }
                else
                {
                    h = "0";
                }
                if (checkBox8.Checked == true)
                {
                    i = "1";
                }
                else
                {
                    i = "0";

                }
                if (checkBox9.Checked == true)
                {
                    j = "1";
                }
                else
                {
                    j = "0";
                }
                if (checkBox10.Checked == true)
                {
                    k = "1";
                }
                else
                {
                    k = "0";
                }

                tot = a + b + c + d + f + g + h + i + j + k;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        string TrobleShoot;

        public void TrobleshhotingInfo()
        {
            try
            {
                #region troubleshooting Information

                if (TroSh1.Checked == true)
                {
                    TrobleShoot = "1";
                }

                if (TroSh2.Checked == true)
                {
                    TrobleShoot = "2";
                }

                if (TroSh3.Checked == true)
                {
                    TrobleShoot = "3";
                }

                if (TroSh4.Checked == true)
                {
                    TrobleShoot = "4";
                }

                if (TroSh5.Checked == true)
                {
                    TrobleShoot = "5";
                }


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        string Warnt;
        public void WarrentyStatus()
        {
            if (WarrantyYes.Checked == true)
            {
                Warnt = "1";
            }

            if (WarrantyNo.Checked == true)
            {
                Warnt = "2";
            }
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            passJobNumber.Text = ReJobNumber.Text;

            if (CusFirstName.Text == "" || CusPersonalAddress.Text == "" || CusTelNUmber.Text == "")
            {
                MessageBox.Show("Please complete customer Details", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PnlAddress.Visible = true;
                CusFirstName.Focus();
                return;
            }



            try
            {
                 DialogResult result = MessageBox.Show("Are you whether you need to complete the invoice?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    if (NewJOB.Checked == true)
                    {

                        #region insert Data to database

                        FaultSelect();

                        TrobleshhotingInfo();

                        WarrentyStatus();

                        GenerateJOBNumbe();
                       

                        SqlConnection con1 = new SqlConnection(IMS);
                        con1.Open();
                        string CusIntert = @"INSERT INTO RepairNotes (ReJobNumber, PaymentDetails, JobStatus,LgUser, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, ReWarranty, PerchesDate, FaultStatus, 
                      FaultOther, ReProcrssor, ReMemory, ReChassis, ReGraphics, ReMouse, RePowerCbl, RePowerBx, ReMBoard, ReHDD, ReRom, ReExtCard, ReKeyB, ReUSB, 
                      ReSystemOther, ReCompletingDate, ReCompletingTime, TroblShootInfor, ReEngNote, ReCusNote,TimeStamp) VALUES ('" + ReJobNumber.Text + "','Pending','1','" + LgUser.Text + "','" + ReCusID.Text + "','" + CusFirstName.Text + "','" + CusPersonalAddress.Text + "','" + CusTelNUmber.Text + "','" + Warnt + "','" + PerchesDate.Text + "','" + tot + "','" + FaultOther.Text + "','" + ReProcrssor.Text + "','" + ReMemory.Text + "','" + ReChassis.Text + "','" + ReGraphics.Text + "','" + ReMouse.Text + "','" + RePowerCbl.Text + "','" + RePowerBx.Text + "','" + ReMBoard.Text + "','" + ReHDD.Text + "','" + ReRom.Text + "','" + ReExtCard.Text + "','" + ReKeyB.Text + "','" + ReUSB.Text + "','" + ReSystemOther.Text + "','" + ReCompletingDate.Text + "','" + ReCompletingTime.Text + "','" + TrobleShoot + "','" + ReEngNote.Text + "','" + ReCusNote.Text + "','" + System.DateTime.Now.ToString() + "')";

                        SqlCommand cmd1 = new SqlCommand(CusIntert, con1);
                        cmd1.ExecuteNonQuery();


                        MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FrmRepairNote rpn = new FrmRepairNote(passJobNumber.Text);
                        rpn.Show();


                        ClearTxt();

                        GenerateJOBNumbe();

                        //passJobNumber.Text = ReJobNumber.Text;

                    }

                    #endregion
                }

                if (UpdateJOB.Checked == true)
                {
                    #region Update Repair JOB details

                    FaultSelect();

                    TrobleshhotingInfo();

                    WarrentyStatus();


                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

                    //string CusUpdate = "UPDATE CustomerDetails SET CusFirstName='" + CusFirstName.Text + "',CusLastName='" + CusLastName.Text + "',CusActiveDeactive='" + 1 + "',CusCompanyName='" + CusCompanyName.Text + "',CusPersonalAddress='" + CusPersonalAddress.Text + "',CusTelNUmber='" + CusTelNUmber.Text + "',CusMobileNumber='" + CusMobileNumber.Text + "',CusFaxNumber='" + CusFaxNumber.Text + "',CusEmailAddress='" + CusEmailAddress.Text + "',CusCountry='" + CusCountry.Text + "',CusTaxGroup='" + CusTaxGroup.Text + "',CusPriceLevel='" + CusPriceLevel.Text + "',CusCreditLimit='" + CusCreditLimit.Text + "',CusRemarks='" + CusRemarks.Text + "' WHERE CusID='" + CusID.Text + "'";

                    string CusUpdate = @"UPDATE RepairNotes SET ReJobNumber='" + ReJobNumber.Text + "', ReCusID='" + ReCusID.Text + "', CusFirstName='" + CusFirstName.Text + "', CusPersonalAddress='" + CusPersonalAddress.Text + "', CusTelNUmber='" + CusTelNUmber.Text + "', ReWarranty='" + Warnt + "', PerchesDate='" + PerchesDate.Text + "', FaultStatus='" + tot + "', FaultOther='" + FaultOther.Text + "', ReProcrssor='" + ReProcrssor.Text + "', ReMemory='" + ReMemory.Text + "', ReChassis='" + ReChassis.Text + "', ReGraphics='" + ReGraphics.Text + "', ReMouse='" + ReMouse.Text + "', RePowerCbl='" + RePowerCbl.Text + "', RePowerBx='" + RePowerBx.Text + "', ReMBoard='" + ReMBoard.Text + "', ReHDD='" + ReHDD.Text + "', ReRom='" + ReRom.Text + "', ReExtCard='" + ReExtCard.Text + "', ReKeyB='" + ReKeyB.Text + "', ReUSB='" + ReUSB.Text + "', ReSystemOther='" + ReSystemOther.Text + "', ReCompletingDate='" + ReCompletingDate.Text + "', ReCompletingTime='" + ReCompletingTime.Text + "', TroblShootInfor='" + TrobleShoot + "', ReEngNote='" + ReEngNote.Text + "', ReCusNote='" + ReCusNote.Text + "' WHERE ReJobNumber='" + ReJobNumber.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(CusUpdate, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated the JOB Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearTxt();

                    GenerateJOBNumbe();

                    groupBox2.Enabled = true;
                    groupBox4.Enabled = true;
                    //groupBox10.Enabled = true;
                    groupBox1.Enabled = true;

                    checkBox13.Checked = false;
                    checkBox13.Enabled = true;

                    PerchesDate.Enabled = true;

                    groupBox6.Enabled = true;

                    UpdateJOB.Enabled = false;

                    BtnSave.Text = "Save And Print";

                    NewJOB.Checked = true;

                #endregion

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void BtnBillTo_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;

            ReCusName.Text= CusFirstName.Text;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked == true)
            {
                groupBox10.Enabled = true;
            }

            if (checkBox13.Checked == false)
            {
                groupBox10.Enabled = false;
            }
        }

        private void RepairNote_Load(object sender, EventArgs e)
        {

            //generate the JOB Number
            GenerateJOBNumbe();

            //passJobNumber.Text = ReJobNumber.Text;

            NewJOB.Checked = true;

            TroSh1.Checked = true;

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                checkBox12.Checked = false;

            PnlCustomerSerch.Visible = true;

            #region select the customer when select the button

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "'";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridView1.Rows.Clear();

            while (dr.Read() == true)
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);

            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }

            #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = false;
            textBox18.Text = "";
            textBox19.Text = "";
        }

        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

               // PnlCustomerSerch.Visible = true;

                textBox18.Text = "";

                #region select the customer in the textbox by ID

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "' AND CusID LIKE '%" + textBox19.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox18_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

               // PnlCustomerSerch.Visible = true;

                textBox19.Text = "";

                #region select the customer in the textbox by ID

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "' AND CusFirstName LIKE '%" + textBox18.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {


                DataGridViewRow dr = dataGridView1.SelectedRows[0];

                CusPreCreditLimit.Text = "00.00";

                String Name = dr.Cells[1].Value.ToString() + " " + dr.Cells[2].Value.ToString();

                ReCusID.Text = dr.Cells[0].Value.ToString();
                ReCusName.Text = Name;
                CusFirstName.Text = Name;
                CusPersonalAddress.Text = dr.Cells[4].Value.ToString();
                CusTelNUmber.Text = dr.Cells[5].Value.ToString();

                ReCusCreditLimit.Text = dr.Cells[7].Value.ToString();

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusCreditBalane = "SELECT CusID,CreditBalance FROM RegCusCredBalance WHERE CusID='" + dr.Cells[0].Value.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView1.Rows.Clear();

                textBox18.Text = "";
                textBox19.Text = "";

                if (dr2.Read() == true)
                {
                    ReCusPreCreditLimit.Text = dr2[1].ToString();

                }

                //calculate the Current credit balence that Customer can get 
                ReCusBalCreditLimit.Text = Convert.ToString(Convert.ToDouble(ReCusCreditLimit.Text) - Convert.ToDouble(ReCusPreCreditLimit.Text));


                PnlCustomerSerch.Visible = false;


                textBox18.Text = "";
                textBox19.Text = "";

                //BtnSave.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void WarrantyYes_CheckedChanged(object sender, EventArgs e)
        {
            if (WarrantyYes.Checked == true)
            {
                PerchesDate.Enabled = true;
            }

            if (WarrantyYes.Checked == false)
            {
                PerchesDate.Enabled = false;
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked == true)
            {
                ReCusID.Text = "WK_Customer";
                ReCusCreditLimit.Text = "00.00";
                ReCusPreCreditLimit.Text = "00.00";
                ReCusBalCreditLimit.Text = "00.00";

                CusFirstName.Text = "";
                CusPersonalAddress.Text = "";
                CusTelNUmber.Text = "";
                ReCusName.Text = "";

                button5.Enabled = false;

            }

            if (checkBox12.Checked == false)
            {
                ReCusID.Text = "";
                ReCusCreditLimit.Text = "00.00";
                ReCusPreCreditLimit.Text = "00.00";
                ReCusBalCreditLimit.Text = "00.00";

                CusFirstName.Text = "";
                CusPersonalAddress.Text = "";
                CusTelNUmber.Text = "";
                ReCusName.Text = "";

                button5.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PnlJOBSerch.Visible = true;

            NewJOB.Checked = false;
            UpdateJOB.Checked = false;


            try
            {

            #region select the JOB when select the button

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = @"SELECT      ReJobNumber, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, ReWarranty, PerchesDate, FaultStatus, FaultOther, ReProcrssor, ReMemory, 
                         ReChassis, ReGraphics, ReMouse, RePowerCbl, RePowerBx, ReMBoard, ReHDD, ReRom, ReExtCard, ReKeyB, ReUSB, ReSystemOther, ReCompletingDate, 
                        ReCompletingTime, TroblShootInfor, ReEngNote, ReCusNote
                        FROM         RepairNotes
                        WHERE     (PaymentDetails = 'Pending') AND (JobStatus = '1')";

            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridView2.Rows.Clear();

            while (dr.Read() == true)
            {
                dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7],dr[8],dr[9],dr[10],dr[11],dr[12],dr[13],dr[14],dr[15],dr[16],dr[17],dr[18],dr[19],dr[20],dr[21],dr[22],dr[23],dr[24],dr[25],dr[26],dr[27]);

            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }

            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PnlJOBSerch.Visible = false;
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            textBox2.Text = "";

            try
            {

                #region select the JOB when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT      ReJobNumber, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, ReWarranty, PerchesDate, FaultStatus, FaultOther, ReProcrssor, ReMemory, 
                         ReChassis, ReGraphics, ReMouse, RePowerCbl, RePowerBx, ReMBoard, ReHDD, ReRom, ReExtCard, ReKeyB, ReUSB, ReSystemOther, ReCompletingDate, 
                        ReCompletingTime, TroblShootInfor, ReEngNote, ReCusNote
                        FROM         RepairNotes
                        WHERE     (PaymentDetails = 'Pending') AND (JobStatus = '1') AND ReJobNumber LIKE '%" + textBox3.Text + "%'";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22], dr[23], dr[24], dr[26], dr[27]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            textBox3.Text = "";

            try
            {

                #region select the Customer Name when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT      ReJobNumber, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, ReWarranty, PerchesDate, FaultStatus, FaultOther, ReProcrssor, ReMemory, 
                         ReChassis, ReGraphics, ReMouse, RePowerCbl, RePowerBx, ReMBoard, ReHDD, ReRom, ReExtCard, ReKeyB, ReUSB, ReSystemOther, ReCompletingDate, 
                        ReCompletingTime, TroblShootInfor, ReEngNote, ReCusNote
                        FROM         RepairNotes
                        WHERE     (PaymentDetails = 'Pending') AND (JobStatus = '1') AND CusFirstName LIKE '%" + textBox2.Text + "%'";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22], dr[23], dr[24], dr[26], dr[27]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

            DataGridViewRow dr = dataGridView2.SelectedRows[0];

                  ReJobNumber.Text=dr.Cells[0].Value.ToString();
                      ReCusID.Text=dr.Cells[1].Value.ToString();
                      CusFirstName.Text = ReCusName.Text=dr.Cells[2].Value.ToString();
                
                      CusPersonalAddress.Text=dr.Cells[3].Value.ToString();
                      CusTelNUmber.Text=dr.Cells[4].Value.ToString();

                 //watenty radio button
                     string ReWarranty=dr.Cells[5].Value.ToString();

                     if (ReWarranty == "1")
                     {
                         WarrantyYes.Checked = true;
                         WarrantyNo.Checked = false;
                     }

                     if (ReWarranty == "0")
                     {
                         WarrantyNo.Checked = true;
                         WarrantyYes.Checked = false;
                     }


                      PerchesDate.Text=dr.Cells[6].Value.ToString();


                      string FaultStatus = dr.Cells[7].Value.ToString();

                      #region fault CheckBox select

                      string Num01 = FaultStatus.Substring(0, 1);
                      string Num02 = FaultStatus.Substring(1, 1);
                      string Num03 = FaultStatus.Substring(2, 1);
                      string Num04 = FaultStatus.Substring(3, 1);
                      string Num05 = FaultStatus.Substring(4, 1);
                      string Num06 = FaultStatus.Substring(5, 1);
                      string Num07 = FaultStatus.Substring(6, 1);
                      string Num08 = FaultStatus.Substring(7, 1);
                      string Num09 = FaultStatus.Substring(8, 1);
                      string Num10 = FaultStatus.Substring(9, 1);

                      //For the check box 01
                      if (Num01 == "1")
                      {
                          checkBox1.Checked = true;
                      }
                      if (Num01 == "0")
                      {
                          checkBox1.Checked = false;
                      }
                      //For the check box 02
                      if (Num02 == "1")
                      {
                          checkBox2.Checked = true;
                      }
                      if (Num02 == "0")
                      {
                          checkBox2.Checked = false;
                      }
                      //For the check box 03
                      if (Num03 == "1")
                      {
                          checkBox3.Checked = true;
                      }
                      if (Num03 == "0")
                      {
                          checkBox3.Checked = false;
                      }
                      //For the check box 04
                      if (Num04 == "1")
                      {
                          checkBox4.Checked = true;
                      }
                      if (Num04 == "0")
                      {
                          checkBox4.Checked = false;
                      }
                      //For the check box 05
                      if (Num05 == "1")
                      {
                          checkBox1.Checked = true;
                      }
                      if (Num05 == "0")
                      {
                          checkBox5.Checked = false;
                      }
                      //For the check box 06
                      if (Num06 == "1")
                      {
                          checkBox6.Checked = true;
                      }
                      if (Num06 == "0")
                      {
                          checkBox6.Checked = false;
                      }
                      //For the check box 07
                      if (Num07 == "1")
                      {
                          checkBox1.Checked = true;
                      }
                      if (Num07 == "0")
                      {
                          checkBox7.Checked = false;
                      }
                      //For the check box 08
                      if (Num08 == "1")
                      {
                          checkBox8.Checked = true;
                      }
                      if (Num08 == "0")
                      {
                          checkBox8.Checked = false;

                      }
                      //For the check box 09
                      if (Num09 == "1")
                      {
                          checkBox9.Checked = true;
                      }
                      if (Num09 == "0")
                      {
                          checkBox9.Checked = false;
                      }
                      //For the check box 10
                      if (Num10 == "1")
                      {
                          checkBox10.Checked = true;
                      }
                      if (Num10 == "0")
                      {
                          checkBox10.Checked = false;
                      }

                      #endregion


                      FaultOther.Text=dr.Cells[8].Value.ToString();
                      ReProcrssor.Text=dr.Cells[9].Value.ToString();
                      ReMemory.Text=dr.Cells[10].Value.ToString();
                      ReChassis.Text=dr.Cells[11].Value.ToString();
                      ReGraphics.Text=dr.Cells[12].Value.ToString();
                      ReMouse.Text=dr.Cells[13].Value.ToString();
                      RePowerCbl.Text=dr.Cells[14].Value.ToString();
                      RePowerBx.Text=dr.Cells[15].Value.ToString();
                      ReMBoard.Text=dr.Cells[16].Value.ToString();
                      ReHDD.Text=dr.Cells[17].Value.ToString();
                      ReRom.Text=dr.Cells[18].Value.ToString();
                      ReExtCard.Text=dr.Cells[19].Value.ToString();
                      ReKeyB.Text=dr.Cells[20].Value.ToString();
                      ReUSB.Text=dr.Cells[21].Value.ToString();
                      
                      ReSystemOther.Text=dr.Cells[22].Value.ToString();
                      ReCompletingDate.Text=dr.Cells[23].Value.ToString();
                       ReCompletingTime.Text=dr.Cells[24].Value.ToString();

                       string TroblShootInfor = dr.Cells[25].Value.ToString();

                       #region TroblShoot radio button

                       if (TroblShootInfor == "1")
                       {
                           TroSh1.Checked = true;
                           TroSh2.Checked = false;
                           TroSh3.Checked = false;
                           TroSh4.Checked = false;
                           TroSh5.Checked = false;
                       }

                       if (TroblShootInfor == "2")
                       {
                           TroSh1.Checked = false;
                           TroSh2.Checked = true;
                           TroSh3.Checked = false;
                           TroSh4.Checked = false;
                           TroSh5.Checked = false;
                       }

                       if (TroblShootInfor == "3")
                       {
                           TroSh1.Checked = false;
                           TroSh2.Checked = false;
                           TroSh3.Checked = true;
                           TroSh4.Checked = false;
                           TroSh5.Checked = false;
                       }

                       if (TroblShootInfor == "4")
                       {
                           TroSh1.Checked = false;
                           TroSh2.Checked = false;
                           TroSh3.Checked = false;
                           TroSh4.Checked = true;
                           TroSh5.Checked = false;
                       }

                       if (TroblShootInfor == "5")
                       {
                           TroSh1.Checked = false;
                           TroSh2.Checked = false;
                           TroSh3.Checked = false;
                           TroSh4.Checked = false;
                           TroSh5.Checked = true;
                       }

                    #endregion

                      ReEngNote.Text=dr.Cells[26].Value.ToString();
                      ReCusNote.Text=dr.Cells[27].Value.ToString();

                      PnlJOBSerch.Visible = false;

                      groupBox2.Enabled = false;
                      groupBox4.Enabled = false;
                      groupBox10.Enabled = false;
                      groupBox1.Enabled = false;

                      checkBox13.Checked = false;
                      checkBox13.Enabled = false;

                      PerchesDate.Enabled = false;

                      NewJOB.Enabled = true;
                      NewJOB.Checked = false;

                      UpdateJOB.Checked = false;
                      UpdateJOB.Enabled = true;

                      groupBox6.Enabled = false;

                dataGridView2.Rows.Clear();

            }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {


           

            this.Close();

            
        }

        private void UpdateJOB_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox4.Enabled = true;
            
            groupBox1.Enabled = true;

            checkBox13.Checked = false;
            checkBox13.Enabled = true;

            PerchesDate.Enabled = true;

            groupBox6.Enabled = true;

            BtnSave.Text = "Update JOB";

            //NewJOB.Enabled = true;
           // NewJOB.Checked = false;

           // UpdateJOB.Checked = false;
            //UpdateJOB.Enabled = true;
        }

        private void NewJOB_CheckedChanged(object sender, EventArgs e)
        {
            ClearTxt();

            GenerateJOBNumbe();

            passJobNumber.Text = ReJobNumber.Text;

            groupBox2.Enabled = true;
            groupBox4.Enabled = true;
            //groupBox10.Enabled = true;
            groupBox1.Enabled = true;

            checkBox13.Checked = false;
            checkBox13.Enabled = true;

            PerchesDate.Enabled = true;

            groupBox6.Enabled = true;

            UpdateJOB.Enabled = false;

            BtnSave.Text = "Save And Print";
        }
    }
}
