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
    public partial class RepairJOBPayments : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public RepairJOBPayments()
        {
            InitializeComponent();
            Select_Bank();
        }

        public void Select_Bank()
        {
            try
            {
                #region select the bank---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankName,BankID FROM Bank_Category";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                CkBank.Items.Clear();

                while (dr.Read() == true)
                {
                    CkBank.Items.Add(dr[0].ToString());
                }

                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        public void Customer_Credit_Balance_Update()
        {
            try
            {
                #region Customer_Credit_Balance_Update--------------------------

                double LastBalance = 0;
                double New_Bal = 0;
                string Deb_Bal = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusCreditBalane = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + txtCustID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView1.Rows.Clear();

                if (dr2.Read() == true)
                {
                    LastBalance = Convert.ToDouble(dr2[0].ToString());
                    Deb_Bal = dr2[1].ToString();
                }
                //check the balesce >0 or <0-----------------------------------

                #region New Customer Previos Remainder is Possitive Value----------------------------

                //balance calc-----------------------------
                New_Bal = LastBalance + Convert.ToDouble(PayBalance.Text);

                SqlConnection con1x = new SqlConnection(IMS);
                con1x.Open();

                string Cus_DebitPaymet = @"INSERT INTO RegCusCredBalance( CusID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) 
                                                VALUES  ('" + txtCustID.Text + "','" + ReJobNumber.Text + "','" + PayBalance.Text + "','0','" + Deb_Bal + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

                SqlCommand cmd21 = new SqlCommand(Cus_DebitPaymet, con1x);
                cmd21.ExecuteNonQuery();

                if (con1x.State == ConnectionState.Open)
                {
                    con1x.Close();
                }

                #endregion


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        public void getCreatebANKcode()
        {
            #region New getCreateReasoncode...........................................
            try
            {
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select BankID from Bank_Category";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                label79.Text = "BNK1001";

                cmd.Dispose();
                dr.Close();

            }

            else
            {

                cmd.Dispose();
                dr.Close();

                string sql1 = " SELECT TOP 1 BankID FROM Bank_Category order by BankID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string OrderNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                    label79.Text = "BNK" + no;

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
        void mouseEnter(Panel pnl, Label lblText)
        {
            pnl.BackColor = Color.FromArgb(201, 35, 35);
            lblText.ForeColor = Color.WhiteSmoke;
        }

        void mouseLeave(Panel pnl, Label lblText)
        {
            pnl.BackColor = Color.WhiteSmoke;
            lblText.ForeColor = Color.FromArgb(0, 0, 0);
        }

        public void HidePanelLeft()
        {
            PnlAddedItemDetails.Visible = false;
            PnlFault.Visible = false;
            pnlTechnicalSolution.Visible = false;
        }

        public void PayMethod()
        {
            double Cash = Convert.ToDouble(PayCash.Text);
            double Check = Convert.ToDouble(PayCheck.Text);
            double CreCard = Convert.ToDouble(PayCrditCard.Text);
            double Debit = Convert.ToDouble(PayDebitCard.Text);
           // double Credits = Convert.ToDouble(PAyCredits.Text);

           // double GndTotal = Cash + Check + CreCard + Debit + Credits;
            double GndTotal = Cash + Check + CreCard + Debit ;


            PayAmount.Text = Convert.ToString(GndTotal);

        }

        public void commonColorSelectItemsLeft()
        {

            //Item Details
            BtnItenDetals.BackColor = Color.WhiteSmoke;
            lblItemDetails.ForeColor = Color.FromArgb(0, 0, 0);

            //Fault Details
            btnaultDetails.BackColor = Color.WhiteSmoke;
            LblFaultDetails.ForeColor = Color.FromArgb(0, 0, 0);

            //technical Details
            btnTechnical.BackColor = Color.WhiteSmoke;
            lblTechnical.ForeColor = Color.FromArgb(0, 0, 0);
        }

        public void GetTotalAmount()
        {
            try
            {
                decimal gtotal = 0;
                foreach (ListViewItem lstItem in ListVPAySumm.Items)
                {
                    if (lstItem.SubItems[1].Text != "Free")
                    {
                        gtotal += Math.Round(decimal.Parse(lstItem.SubItems[1].Text), 2);
                    }
                }
                LblBillTotalAmount.Text = txtSubTotal.Text = Convert.ToString(gtotal);

                //------------------------------------
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }

                txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                txtTotal.Text = PayAmountInvoiced.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();

                if (ListVPAySumm.Items.Count == 0)
                {
                    button5.Enabled = false;
                    txtTaxPer.Enabled = false;
                    button14.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void InsertInToListVeiw()
        {
            try
            {

                #region check the data is in list========================================================

                for (int i = 0; i <= ListVPAySumm.Items.Count - 1; i++)
                {
                    if (ListVPAySumm.Items[i].SubItems[2].Text == AutoID.Text)
                    {
                        MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (comboBox5.Items.Count > 0)
                        {
                            comboBox5.DroppedDown = true;
                            comboBox5.Focus();
                        }
                        if (comboBox5.Items.Count <=0)
                        {
                            PnlPayMeth.Visible = true;
                            Reesom.Focus();
                        }
                        return;
                    }
                }


                #endregion



                ListViewItem li;

                li = new ListViewItem(comboBox5.Text.ToString());

                // li.SubItems.Add(comboBox5.Text.ToString());

                if (Convert.ToDouble(PaymentAmount.Text) == 0)
                {
                    li.SubItems.Add("Free");
                }
                if (Convert.ToDouble(PaymentAmount.Text) != 0)
                {
                    li.SubItems.Add(PaymentAmount.Text.ToString());
                }

                li.SubItems.Add(AutoID.Text.ToString());

                ListVPAySumm.Items.Add(li);

                GetTotalAmount();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }



        public void PAybutton_SaveBtnActiveOrNot()
        {

            if (ListVPAySumm.Items.Count > 0)
            {
                //trobleshoot information pendiig or not=============================
                if (TroSh1.Checked == true)
                {
                    MessageBox.Show("You cannot complete payment Details. because technical solution is pending","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    button5.Enabled = false;
                    button14.Enabled = false;

                    ListVPAySumm.Items.Clear();
                    return;
                }
                if(TroSh2.Checked==true || TroSh3.Checked==true||TroSh4.Checked==true)
                {
                    button5.Enabled = true;
                    button14.Enabled = true;
                }
            }

            else
            {
                button5.Enabled = false;
                button14.Enabled = false;
            
            }
        }

        public void clearTxt()
        {
            ReJobNumber.Text = "";
           
            CusCreditLimit.Text = "00.00";
            CusPreCreditLimit.Text = "00.00";
            CusBalCreditLimit.Text = "00.00";

            ListVPAySumm.Items.Clear();

            PayCash.Text = "00.00";
            PayCheck.Text = "00.00";
            PayCrditCard.Text = "00.00";
            PayDebitCard.Text = "00.00";
            PAyCredits.Text = "00.00";

            CkCkBox.Checked = false;
            groupBox8.Enabled = false;
            CkBank.Text = "00.00";
            CkNumber.Text = "00.00";

            PayAmount.Text = "00.00";
            PayPreInvoived.Text = "00.00";
            PayAmountInvoiced.Text = "00.00";
            PayBalance.Text = "00.00";

            txtSubTotal.Text = "00.00";
            txtTaxPer.Text = "00";
            txtTaxAmt.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalPayment.Text = "0.00";
            txtPaymentDue.Text = "0.00";
            button5.Enabled = false;

            amountCash.Text = "0.00";
            Totalcash.Text = "0.00";
            Balance.Text = "0.00";

            button14.Enabled = false;

        }

        //clear solution pnl
        public void txtSolutionClear()
        {
            TechNote.Text = "";
            CusNote.Text = "";
            txtSolvTech.Text = "";

            TroSh1.Checked = false;
            TroSh2.Checked = false;
            TroSh3.Checked = false;
            TroSh4.Checked = false;

        }

        public void txtfaultClear_AllItemPnl()
        {
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
            FaultOther.Text = "";

            RePowerCbl.Text = "No";
            ReUSB.Text = "No";
            ExtraCoolingFans.Text = "No";
            ReSystemOther.Text = "No";

            txtCusAddfault.Text = "";
            txtCusNameFaul.Text = "";
            txtCustID.Text = "";
            TelNumFault.Text = "";

            txtJobType.Text = "";
            txtAuthPerson.Text = "";

            //clear list all items list view
            ListAllItem.Items.Clear();
        }

        public void txtclearJobInvoice()
        {
            BillCusAddress.Text = "";
            BillCusName.Text = "";
            BillJobType.Text = "";

            CusCreditLimit.Text = "00.00";
            CusPreCreditLimit.Text = "00.00";
            CusBalCreditLimit.Text = "00.00";

            PaymentAmount.Text = "0.00";
        }




        private void RepairJOBPayments_Load(object sender, EventArgs e)
        {
            commonColorSelectItemsLeft();

            PnlAddedItemDetails.Visible = true;
            mouseEnter(BtnItenDetals, lblItemDetails);

            CreateRepairJOBCode();


        }

        private void BtnItenDetals_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsLeft();
            mouseEnter(BtnItenDetals, lblItemDetails);

            HidePanelLeft();

            PnlAddedItemDetails.Visible = true;

        }

        private void lblItemDetails_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsLeft();
            mouseEnter(BtnItenDetals, lblItemDetails);

            HidePanelLeft();

            PnlAddedItemDetails.Visible = true;
        }

        private void btnaultDetails_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsLeft();
            mouseEnter(btnaultDetails, LblFaultDetails);

            HidePanelLeft();

            PnlFault.Visible = true;
        }

        private void LblFaultDetails_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsLeft();
            mouseEnter(btnaultDetails, LblFaultDetails);

            HidePanelLeft();

            PnlFault.Visible = true;
        }

        public void CreateRepairJOBCode()
        {
            #region New JOB Number...........................................
        //    try
        //    {
        //        SqlConnection Conn = new SqlConnection(IMS);
        //        Conn.Open();


        //        //=====================================================================================================================
        //        string sql = "SELECT InvoiceNo FROM SoldInvoiceDetails WHERE InvoiceNo LIKE 'JIV%'";
        //        SqlCommand cmd = new SqlCommand(sql, Conn);
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        //=====================================================================================================================
        //        if (!dr.Read())
        //        {
        //            JobInvoice.Text = "JIV1001";

        //            cmd.Dispose();
        //            dr.Close();

        //        }

        //        else
        //        {

        //            cmd.Dispose();
        //            dr.Close();

        //            string sql1 = " SELECT TOP 1 ItmID FROM NewItemDetails WHERE InvoiceNo LIKE 'JIV%' order by ItmID DESC";
        //            SqlCommand cmd1 = new SqlCommand(sql1, Conn);
        //            SqlDataReader dr7 = cmd1.ExecuteReader();

        //            while (dr7.Read())
        //            {
        //                string no;
        //                no = dr7[0].ToString();

        //                string ItemNumOnly = no.Substring(3);

        //                no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

        //                JobInvoice.Text = "JIV" + no;

        //            }
        //            cmd1.Dispose();
        //            dr7.Close();

        //        }
        //        Conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        //    }
           #endregion
        }


        private void lblTechnical_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsLeft();
            mouseEnter(btnTechnical, lblTechnical);

            HidePanelLeft();

            pnlTechnicalSolution.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            // PnlNote.Visible = false;
            PnlPaymentMethod.Visible = true;

            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PnlJOBSerch.Visible = true;

            try
            {

                #region select the JOB when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT     ReJobNumber, PaymentStatus, PaymentDetails, LgUser, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, FaultStatus, FaultOther, RePowerCbl, 
                      ExtraCoolingFans, ReUSB, ReSystemOther, ReCompletingDate, ReCompletingTime, JobType, ReEngNote, ReCusNote, Solution,Tecnician
                      FROM RepairNotes
                      WHERE     (PaymentDetails = 'Pending') AND (JobStatus = '1')";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19],dr[20]);

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

        private void SeachJOB_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                #region select the JOB when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT     ReJobNumber, PaymentStatus, PaymentDetails, LgUser, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, FaultStatus, FaultOther, RePowerCbl, 
                      ExtraCoolingFans, ReUSB, ReSystemOther, ReCompletingDate, ReCompletingTime,JobType, ReEngNote, ReCusNote, Solution,Tecnician
                      FROM RepairNotes
                      WHERE     (PaymentDetails = 'Pending') AND (JobStatus = '1') AND (ReJobNumber LIKE '%" + SeachJOB.Text + "%' OR CusFirstName LIKE '%" + SeachJOB.Text + "%' OR ReCusID LIKE '%" + SeachJOB.Text + "%' OR CusTelNUmber LIKE '%" + SeachJOB.Text + "%')";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19],dr[20]);

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

            string JobNum = "";

            JobNum = dr.Cells[0].Value.ToString();

            ReJobNumber.Text = JobNum;



            #region   Add data to the Solution Panel==============================================================

            int RadioBtnNumer = Convert.ToInt32(dr.Cells[19].Value.ToString());

            if (RadioBtnNumer == 1)
            {
                TroSh1.Checked = true;
            }
            if (RadioBtnNumer == 2)
            {
                TroSh2.Checked = true;
            }

            if (RadioBtnNumer == 3)
            {
                TroSh3.Checked = true;
            }
            if (RadioBtnNumer == 4)
            {
                TroSh4.Checked = true;
            }


            TechNote.Text = dr.Cells[17].Value.ToString();
            CusNote.Text = dr.Cells[18].Value.ToString();
            txtSolvTech.Text = dr.Cells[20].Value.ToString();

            //=================================================================================================================================
            #endregion


            //======================================================================
            try
            {
                #region add tempary paymetn details to the list view =============================================

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                String viewGrideview1 = @"SELECT       RepairJobReson, Amount,JBAutoNumber
                FROM RepairJobTemp_PAyments WHERE JobID='" + JobNum + "'";

                SqlCommand cmd = new SqlCommand(viewGrideview1, con1);
                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ListVPAySumm.Items.Clear();

                while (dr1.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr1[0].ToString());

                    li.SubItems.Add(dr1[1].ToString());
                    li.SubItems.Add(dr1[2].ToString());

                    ListVPAySumm.Items.Add(li);
                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion

                GetTotalAmount();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }


            //=====================================================================

            #region Select the Total Items According to the JOB umber======================================================================================

            SqlConnection conx = new SqlConnection(IMS);
            conx.Open();

            string addItemsToList = @"SELECT     ReJobNumber, ItemID, ItemName,PurchesDate, Warrany, BarcodeSerial, ItemStatus, InvoceBarrowID, CustomerID, InvoicedBy, RemarkItem
                                    FROM         RepairNoteItems WHERE ReJobNumber='" + JobNum + "'";

            SqlCommand cmdx = new SqlCommand(addItemsToList, conx);
            SqlDataReader Dr = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

            ListAllItem.Items.Clear();

            while (Dr.Read() == true)
            {
                ListViewItem li;

                li = new ListViewItem(Dr[1].ToString());

                li.SubItems.Add(Dr[2].ToString());
                li.SubItems.Add(Dr[3].ToString());
                li.SubItems.Add(Dr[4].ToString());
                
                li.SubItems.Add(Dr[5].ToString());
                li.SubItems.Add(Dr[6].ToString());
                li.SubItems.Add(Dr[7].ToString());
                li.SubItems.Add(Dr[8].ToString());
                li.SubItems.Add(Dr[9].ToString());
                li.SubItems.Add(Dr[10].ToString());

                ListAllItem.Items.Add(li);
            }

            if (conx.State == ConnectionState.Open)
            {
                conx.Close();
                Dr.Close();

            }
            #endregion

            //=============================================================================================================================

            //Select the Fault PAnel==========================================================================================================

            string FaultStatus = dr.Cells[8].Value.ToString();

            #region fault CheckBox select ==================================================================================

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


            FaultOther.Text = dr.Cells[9].Value.ToString();

            RePowerCbl.Text = dr.Cells[10].Value.ToString();
            ExtraCoolingFans.Text = dr.Cells[11].Value.ToString();
            ReUSB.Text = dr.Cells[12].Value.ToString();
            ReSystemOther.Text = dr.Cells[13].Value.ToString();

            txtCustID.Text = dr.Cells[4].Value.ToString();

                    txtCusNameFaul.Text =BillCusName.Text= dr.Cells[5].Value.ToString();

            TelNumFault.Text = dr.Cells[7].Value.ToString();

                    txtCusAddfault.Text =BillCusAddress.Text= dr.Cells[6].Value.ToString();

                    txtJobType.Text =BillJobType.Text= dr.Cells[16].Value.ToString();

            txtAuthPerson.Text = dr.Cells[20].Value.ToString();
            txtAuthPerson.Text = dr.Cells[20].Value.ToString();
            ReCompletingDate.Text = dr.Cells[14].Value.ToString();
            ReCompletingTime.Text = dr.Cells[15].Value.ToString();

            //==============================================================================================================================


            #region select the credit limit=====================================================================================================

                //=====================================================================================
            double LastBalance = 0;
            double New_Bal = 0;

            SqlConnection con1x = new SqlConnection(IMS);
            con1x.Open();

            string CusCreditBalane = "SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = '" + dr.Cells[4].Value.ToString() + "') ORDER BY AutoNum DESC";
            SqlCommand cmd1x = new SqlCommand(CusCreditBalane, con1x);
            SqlDataReader dr2x = cmd1x.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr2x.Read() == true)
            {
                LastBalance = Convert.ToDouble(dr2x[0].ToString());
                CusPreCreditLimit.Text = Convert.ToString(LastBalance);
            }

            if (con1x.State == ConnectionState.Open)
            {
                con1x.Close();
                dr2x.Close();
            }

                //_________________________________________________________

            SqlConnection con1z = new SqlConnection(IMS);
            con1z.Open();

            string CusID = @"SELECT CusCreditLimit FROM CustomerDetails WHERE CusID='" + dr.Cells[4].Value.ToString() + "'";

            SqlCommand cmd1 = new SqlCommand(CusID, con1z);
            SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


            if (dr2.Read() == true)
            {
                CusCreditLimit.Text = dr2[0].ToString();

                CusBalCreditLimit.Text = Convert.ToString(Convert.ToDouble(dr2[0].ToString()) - LastBalance);
            }


                //=====================================================================================
//            SqlConnection con1x = new SqlConnection(IMS);
//            con1x.Open();

//            string CusCreditBalane = @"SELECT CustomerDetails.CusID, CustomerDetails.CusPriceLevel, CustomerDetails.CusCreditLimit, RegCusCredBalance.CreditBalance
//                                        FROM CustomerDetails INNER JOIN
//                                        RegCusCredBalance ON CustomerDetails.CusID = RegCusCredBalance.CusID WHERE CustomerDetails.CusID='" + dr.Cells[4].Value.ToString()+"'";

//            SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1x);
//            SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

//           // string CreLimit = "";

//            if (dr2.Read() == true)
//            {
//               CusCreditLimit.Text = dr2[2].ToString();
//                CusPreCreditLimit.Text = dr2[3].ToString();


//                CusBalCreditLimit.Text = Convert.ToString(Convert.ToDouble(dr2[2].ToString()) - Convert.ToDouble(dr2[3].ToString()));
//            }

            #endregion


            //calculate the Current credit balence that Customer can get 
           // CusBalCreditLimit.Text = Convert.ToString(Convert.ToDouble(CreLimit) - Convert.ToDouble(CusPreCreditLimit.Text));
              

            PnlJOBSerch.Visible = false;


            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusCreditBalane = @"SELECT PaymentType, Amount,AutoID FROM JOBPaymentMethod WHERE PaymentType='" + comboBox5.Text + "'";

            SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
            SqlDataReader dr2 = cmd1.ExecuteReader();
            //SqlDataReader Dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    comboBox5.Items.Clear();

            if (dr2.Read())
            {
                PaymentAmount.Text = dr2[1].ToString();
                AutoID.Text = dr2[2].ToString();

            }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void comboBox5_Click(object sender, EventArgs e)
        {
            try{
            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusCreditBalane = @"SELECT PaymentType, Amount FROM JOBPaymentMethod";

            SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
            SqlDataReader dr2 = cmd1.ExecuteReader();
            //SqlDataReader Dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            comboBox5.Items.Clear();

            while (dr2.Read())
            {
                comboBox5.Items.Add(dr2[0]);
            }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertInToListVeiw();

            PAybutton_SaveBtnActiveOrNot();

            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);
        }

        private void ListVPAySumm_SelectedIndexChanged(object sender, EventArgs e)
        {
            PAybutton_SaveBtnActiveOrNot();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtSubTotal.Text) == 0 || txtSubTotal.Text == "" || txtSubTotal.Text == "0")
            {
                button5.Enabled = false;
                txtTaxPer.Enabled = false;
                button14.Enabled = false;
            }

            if (Convert.ToDouble(txtSubTotal.Text) != 0 || txtSubTotal.Text != "" || txtSubTotal.Text != "")
            {
                button5.Enabled = true;
                txtTaxPer.Enabled = true;
                button14.Enabled = true;
            }

        }

        private void txtTaxPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }
                txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                txtTotal.Text = PayAmountInvoiced.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int val1 = 0;
                int val2 = 0;
                int.TryParse(txtTotal.Text, out val1);
                int.TryParse(txtTotalPayment.Text, out val2);
                int I = (val1 - val2);
                txtPaymentDue.Text = I.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void CkCkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CkCkBox.Checked == true)
            {
                PayCheck.Enabled = true;
                groupBox8.Enabled = true;

                Select_Bank();
            }

            if (CkCkBox.Checked == false)
            {
                PayCheck.Enabled = false;
                groupBox8.Enabled = false;

                PayCheck.Text = "00.00";
                CkBank.Items.Clear();
                CkNumber.Text = "";

            }
        }

        private void PayCheck_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PayCheck.Text == "")
                {
                    PayCheck.Text = "00.00";
                }

                PayMethod();

                double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

                if (Balance < 0)
                {
                    MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PayCheck.Text = "00.00";
                    return;
                }

                PayBalance.Text = Convert.ToString(Balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            
        }

        private void PayCrditCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PayCrditCard.Text == "")
                {
                    PayCrditCard.Text = "00.00";
                }

                PayMethod();

                double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

                if (Balance < 0)
                {
                    MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PayCrditCard.Text = "00.00";
                    return;
                }

                PayBalance.Text = Convert.ToString(Balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            
        }

        private void PayDebitCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PayDebitCard.Text == "")
                {
                    PayDebitCard.Text = "00.00";
                }
                PayMethod();

                double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

                if (Balance < 0)
                {
                    MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PayDebitCard.Text = "00.00";
                    return;
                }

                //double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

                PayBalance.Text = Convert.ToString(Balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void PAyCredits_TextChanged(object sender, EventArgs e)
        {
            //if (PAyCredits.Text == "")
            //{
            //    PAyCredits.Text = "00.00";
            //}
            //PayMethod();

            //double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            //if (Balance < 0)
            //{
            //    MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    PAyCredits.Text = "00.00";
            //    return;
            //}

            //PayBalance.Text = Convert.ToString(Balance);
        }

        private void PayAmountInvoiced_Click(object sender, EventArgs e)
        {
            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //PnlAddress.Visible = false;
            //PnlNote.Visible = false;
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                InsertInToListVeiw();
            }
        }

        private void PayCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void PayCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void PayCrditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void PayDebitCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void PAyCredits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void PayCash_TextChanged(object sender, EventArgs e)
        {
            if (PayCash.Text == "")
            {
                PayCash.Text = "00.00";

            }

            PayMethod();

            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            if (Balance < 0)
            {
                MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PayCash.Text = "00.00";
                return;
            }


            PayBalance.Text = Convert.ToString(Balance);

        }

        private void ListVPAySumm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListVPAySumm.SelectedItems[0].Remove();
            GetTotalAmount();
        }

        private void button14_Click(object sender, EventArgs e)
        {


            if (CkCkBox.Checked == true)
            {
                if (CkNumber.Text == "" || CkBank.Text == "" || PayCheck.Text == "00.00")
                {
                    MessageBox.Show("Please complete check Details correctly", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PnlPaymentMethod.Visible = true;
                    CkNumber.Focus();
                    return;
                }
            }

            //customer credit limit check
            if (Convert.ToDouble(PayBalance.Text) > Convert.ToDouble(CusBalCreditLimit.Text))
            {
                MessageBox.Show("Customer maximum Credit limit is less than your request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PnlPaymentMethod.Visible = true;
                PayCash.Focus();
                return;

            }

            //check the balance paymet is equal to 0
            if (Convert.ToDouble(PayBalance.Text) < 0)
            {
                MessageBox.Show("Credit balance cannot be negative. please change your payment amount", "Wrong Balance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PnlPaymentMethod.Visible = true;
                PayCash.Focus();
                return;
            }



            //'invoiced paydetails
            //==========================================================================================================================

            try{


            DialogResult result = MessageBox.Show("Are you whether you need to complete the invoice?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {


                int i;

                for (i = 0; i <= ListVPAySumm.Items.Count - 1; i++)
                {
                    SqlConnection con3 = new SqlConnection(IMS);
                    con3.Open();
                    string InsertItemRepairJobDetails = @"INSERT INTO  RepairJobPAyments (JobID, RepairJobReson, Amount,InvoicedBy)
                                                        VALUES(@JobID,@RepairJobReson,@Amount,@InvoicedBy)";

                    SqlCommand cmd3 = new SqlCommand(InsertItemRepairJobDetails, con3);


                    cmd3.Parameters.AddWithValue("JobID", ReJobNumber.Text);
                    cmd3.Parameters.AddWithValue("RepairJobReson", ListVPAySumm.Items[i].SubItems[0].Text);
                    cmd3.Parameters.AddWithValue("Amount", ListVPAySumm.Items[i].SubItems[1].Text);
                    cmd3.Parameters.AddWithValue("InvoicedBy",LgUser.Text);

                    cmd3.ExecuteNonQuery();

                    if (con3.State == ConnectionState.Open)
                    {
                        cmd3.Dispose();
                        // dr.Close();
                        con3.Close();
                    }
                }





                //===============================================================================================================================



                //-------------------------------------------------------------------------------------------------------------------------------

                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();
                string PaymentDetails = "UPDATE [RepairNotes] SET PaymentDetails='Paid' WHERE ReJobNumber='" + ReJobNumber.Text + "'";
                SqlCommand cmd2 = new SqlCommand(PaymentDetails, con2);
                cmd2.ExecuteNonQuery();

                if (con2.State == ConnectionState.Open)
                {
                    cmd2.Dispose();
                    //con.Close();
                    // dr.Close();
                }

                // insert Invoice Paymet Details--------------------------------------------------------------------------------------------------------

                SqlConnection con5 = new SqlConnection(IMS);
                con5.Open();
                string InsertInvoicePaymetDetails = "INSERT INTO InvoicePaymentDetails (InvoiceID,SubTotal,VATpresentage,GrandTotal,PayCash,PayCheck,PayCrditCard,PayDebitCard,PAyCredits,PayBalance,InvoiceDate,InvoiceDiscount) VALUES('" + ReJobNumber.Text + "','" + txtSubTotal.Text + "','" + txtTaxPer.Text + "','" + txtTotal.Text + "','" + PayCash.Text + "','" + PayCheck.Text + "','" + PayCrditCard.Text + "','" + PayDebitCard.Text + "','" + PayBalance.Text + "','" + PayBalance.Text + "','" + System.DateTime.Now.ToString() + "','0')";
                SqlCommand cmd5 = new SqlCommand(InsertInvoicePaymetDetails, con5);
                cmd5.ExecuteNonQuery();


                if (con5.State == ConnectionState.Open)
                {
                    cmd5.Dispose();
                    con5.Close();
                }

                // insert Chq Details

                if (CkCkBox.Checked == true)
                {
                    SqlConnection con6 = new SqlConnection(IMS);
                    con6.Open();
                    string InsertCheckPaymetDetails = "INSERT INTO InvoiceCheckDetails(InvoiceID,CkStatus,CkNumber,Bank,Amount,CurrentDate,MentionDate) VALUES('" + ReJobNumber.Text + "','Active','" + CkNumber.Text + "','" + label80.Text + "','" + PayCheck.Text + "','" + System.DateTime.Now.ToString() + "','" + Ckdate.Text + "')";
                    SqlCommand cmd6 = new SqlCommand(InsertCheckPaymetDetails, con6);
                    cmd6.ExecuteNonQuery();


                    if (con6.State == ConnectionState.Open)
                    {
                        cmd6.Dispose();
                        con6.Close();
                    }
                }

                //Customer Credit Balance Update
                if (txtCustID.Text != "WK_Customer")
                {
                    //select Customer
                    #region old customer balance Update--------------------------------------------------------------

                    //double TotCusCredits = 0;

                    //SqlConnection con6 = new SqlConnection(IMS);
                    //con6.Open();
                    //string selectCreditCustomer = "SELECT [CreditBalance] FROM [RegCusCredBalance] WHERE [CusID]='" + txtCustID.Text + "'";
                    //SqlCommand cmd6 = new SqlCommand(selectCreditCustomer, con6);
                    //SqlDataReader dr6 = cmd6.ExecuteReader(CommandBehavior.CloseConnection);

                    //if (dr6.Read() == true)
                    //{
                    //    TotCusCredits = Convert.ToDouble(dr6[0].ToString());
                    //}
                    //if (con6.State == ConnectionState.Open)
                    //{
                    //    cmd6.Dispose();
                    //    con6.Close();
                    //    dr6.Close();
                    //}

                    ////update customer credits

                    //double finalCreditUpdate = TotCusCredits + Convert.ToDouble(PAyCredits.Text);

                    //SqlConnection con7 = new SqlConnection(IMS);
                    //con7.Open();
                    //string InsertItemsToStockCount = "UPDATE RegCusCredBalance SET CreditBalance =" + finalCreditUpdate + " WHERE CusID ='" + txtCustID.Text + "'";
                    //SqlCommand cmd7 = new SqlCommand(InsertItemsToStockCount, con7);
                    //cmd7.ExecuteNonQuery();

                    #endregion

                    Customer_Credit_Balance_Update();
                }

                MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //Open othe window
                RptRepairJobFinal rptfrm = new RptRepairJobFinal();
                rptfrm.PrintingJOBNumber = ReJobNumber.Text;
                rptfrm.InvoiceCopyType = "Original Copy";
                rptfrm.Visible = true;

                clearTxt();
                txtSolutionClear();
                txtfaultClear_AllItemPnl();
                txtclearJobInvoice();

                ReJobNumber.Text = "";

                CreateRepairJOBCode();

            }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void ReJobNumber_TextChanged(object sender, EventArgs e)
        {
            if (ReJobNumber.Text != "")
            {
                PnlJobInvoice.Enabled = true;
            }

            if (ReJobNumber.Text == "")
            {
                PnlJobInvoice.Enabled = false;
            }
        }

        private void ReJobNumber_TextChanged_1(object sender, EventArgs e)
        {
            if (ReJobNumber.Text != "")
            {
                PnlJobInvoice.Enabled = true;
            }
            if (ReJobNumber.Text == "")
            {
                PnlJobInvoice.Enabled = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Reesom.Text == "" || Amount.Text == "")
                {
                    MessageBox.Show("please complete the details correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Reesom.Text == "" && Amount.Text == "")
                {
                    PnlPayMeth.Visible = false;
                    return;
                }

                if (Reesom.Text != "" && Amount.Text != "")
                {
                    button8.Enabled = true;
                }


                //ckek wherther details available like equal to reson and amount. if it is not insert it to DB
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectCreditCustomer = "SELECT PaymentType,Amount FROM [JOBPaymentMethod] WHERE [PaymentType]='" + Reesom.Text + "' AND Amount='" + Amount.Text + "'";
                SqlCommand cmd1 = new SqlCommand(selectCreditCustomer, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                if (!dr1.Read())
                {
                    SqlConnection con6 = new SqlConnection(IMS);
                    con6.Open();
                    string InsertPaymetmethds = "INSERT INTO JOBPaymentMethod(PaymentType,Amount) VALUES('" + Reesom.Text + "','" + Amount.Text + "')";
                    SqlCommand cmd6 = new SqlCommand(InsertPaymetmethds, con6);
                    cmd6.ExecuteNonQuery();

                    MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PnlPayMeth.Visible = false;
                    button8.Enabled = false;

                    Reesom.Text = "";
                    Amount.Text = "";



                    if (con6.State == ConnectionState.Open)
                    {
                        cmd6.Dispose();
                        con6.Close();
                    }
                }
                else
                {
                    MessageBox.Show("This statement also available in the batabase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Reesom.Text = "";
                    Amount.Text = "";
                }
                if (con1.State == ConnectionState.Open)
                {
                    cmd1.Dispose();
                    con1.Close();
                    dr1.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }


            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            PnlPayMeth.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PnlPayMeth.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RptRepairJobFinal rtt = new RptRepairJobFinal();
            rtt.Visible = true;
        }

        private void Totalcash_TextChanged(object sender, EventArgs e)
        {
            if (Totalcash.Text == "")
            {
                Totalcash.Text = "0";
            }

            double x = Convert.ToDouble(Totalcash.Text) - Convert.ToDouble(amountCash.Text);

            Balance.Text = x.ToString();
        }

        private void Reesom_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Amount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PaymentAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void CkBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region select bank ID .....................................

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankName,BankID FROM Bank_Category where BankName='" + CkBank.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================



                while (dr.Read() == true)
                {
                    //CkBank.Items.Add(dr[0].ToString());
                    label80.Text = (dr[1].ToString());
                }

                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            getCreatebANKcode();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Bank create new.......................................
            
            if(textBox1.Text!="")
            {
                try
                {
                    getCreatebANKcode();

                    SqlConnection cnne = new SqlConnection(IMS);
                    cnne.Open();
                    String NewBank = "insert into Bank_Category(BankID,BankName) values('" + label79.Text + "','" + textBox1.Text + "')";
                    SqlCommand cmme = new SqlCommand(NewBank, cnne);
                    cmme.ExecuteNonQuery();

                    MessageBox.Show(" Insert New Bank Successfull.....!");

                    panel5.Visible = false;
                    textBox1.Text = "";
                    Select_Bank();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            else
            {
                MessageBox.Show("Please Enter Bank Name OR Click 'Cancel' Button");
            }
            #endregion
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        
    }
}

