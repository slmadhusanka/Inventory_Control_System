using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Inventory_Control_System
{
    public partial class Customer_Credit_Payments : Form
    {
        public Customer_Credit_Payments()
        {
            InitializeComponent();
            TxtCash.Focus();
        }

        string IMS=ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string Invoiced_Last_Auto_ID = "";
        String Last_SET_OFF_BAL = "";

        public void Select_Bank()
        {
            try
            {

                #region select the bank---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankName FROM Bank_Category";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                txtBank.Items.Clear();

                while (dr.Read() == true)
                {
                    txtBank.Items.Add(dr[0].ToString());
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

        public void Last_SetOFF_Details()
        {

            #region last Invoice Auto ID Select ---------------------------------

            try
            {

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT TOP 1 InvoiceAutoID,Remain_Balance FROM Set_Off_Details order by AutoID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                if (dr.Read() == true)
                {
                    Invoiced_Last_Auto_ID = dr[0].ToString();
                    Last_SET_OFF_BAL = dr[1].ToString();

                    Conn.Close();
                    dr.Close();
                }
                else
                {
                    Invoiced_Last_Auto_ID = "0";
                    Last_SET_OFF_BAL = "0";
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Customer_Total_Paymet_FrmDB()
        {
            try
            {
                #region Select Custoemr paymernt summary--------------------------

                double LastBalance = 0;
                double Debit_Balance = 0;

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + txtSupID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                if (dr7.Read())
                {
                    LastBalance = Convert.ToDouble(dr7[0].ToString());
                    Debit_Balance = Convert.ToDouble(dr7[1].ToString());

                    //MessageBox.Show(LastBalance.ToString()+" "+Debit_Balance.ToString());
                    //MessageBox.Show(LastBalance.ToString());
                }

                txtTotalRem.Text = Convert.ToString(Convert.ToDouble(label27.Text) - Convert.ToDouble(label29.Text) + Debit_Balance);

                cmd1.Dispose();
                dr7.Close();
                Conn.Close();



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Customer_Final_Balance_Update()
        {
            try
            {

                #region Customer_Final_Balance_Update--------------------------

                double LastBalance = 0;
                double New_Bal = 0;

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = '" + txtSupID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                if (dr7.Read())
                {
                    LastBalance = Convert.ToDouble(dr7[0].ToString());
                }

                #region New Vendor Previos Remainder is Possitive Value----------------------------

                //balance calc-----------------------------
                double Calc_Bal = LastBalance - Convert.ToDouble(label27.Text);

                //if there is some remaining credits
                if (Calc_Bal >= 0)
                {
                    New_Bal = Calc_Bal;
                }
                //if cedite over and some balace on our hand....
                if (Calc_Bal < 0)
                {
                    New_Bal = 0;
                }

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Vend_DebitPaymet = @"INSERT INTO RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount, Debit_Balance, Balance, Date) 
                                        VALUES  ('" + txtSupID.Text + "','" + lblDocumentNo.Text + "','0','" + label27.Text + "','" + txtTotalRem.Text + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

                SqlCommand cmd21 = new SqlCommand(Vend_DebitPaymet, con1);
                cmd21.ExecuteNonQuery();

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion

                cmd1.Dispose();
                dr7.Close();
                Conn.Close();



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void ListView4Item_Count()
        {
            #region if GRN paymet list view empty, apply button dissable

            if (listView4.Items.Count == 0)
            {
                button1.Enabled = false;

            }

            else
                button1.Enabled = true;

            #endregion
        }


        public void CalcTotRemaining()
        {
            #region calculate the total payment we can pay now================================

            txtTotalRem.Text = Convert.ToString(Convert.ToDouble(label27.Text) - Convert.ToDouble(label29.Text));

            #endregion
        }


        public void getCreateStockCode()
        {
            #region New Document Code auto generate...........................................
            try
            {
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Docu_No from Customer_Payment_Details";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                lblDocumentNo.Text = "CCP10000001";

                cmd.Dispose();
                dr.Close();

            }

            else
            {

                cmd.Dispose();
                dr.Close();

                string sql1 = " SELECT TOP 1 Docu_No FROM Customer_Payment_Details order by Docu_No DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string OrderNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                    lblDocumentNo.Text = "CCP" + no;

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

        public void addcash_card_check_on_listview()
        {
            try
            {

                #region addcash_card_check_on_listview
                ListViewItem li;
                li = new ListViewItem(TxtCash.Text);
                li.SubItems.Add(txtCheque.Text);
                li.SubItems.Add(txtCard.Text);

                //Select the Bank -------------------
                if (Convert.ToDouble(txtCheque.Text) != 0)
                {
                    li.SubItems.Add(txtBank.Text);
                }

                if (Convert.ToDouble(txtCheque.Text) == 0)
                {
                    li.SubItems.Add("-");
                }

                //------------------------------------------

                li.SubItems.Add(ChecNo.Text);
                li.SubItems.Add(Card_no.Text);
                li.SubItems.Add(txtBranch.Text);
                li.SubItems.Add(dateTimePicker1.Text);

                listView2.Items.Add(li);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void TotalPayamount()
        {
            try
            {
                #region Total Pay amount
                decimal gtotal = 0;
                foreach (ListViewItem lstItem in listView4.Items)
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[4].Text), 2);
                }
                label29.Text = Convert.ToString(gtotal);


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void cleartextbox()
        {
            try
            {
                #region cleartextBox

                TxtCash.Text = "0.0";
                txtCheque.Text = "0.0";
                txtCard.Text = "0.0";
                txtBank.Enabled = false;
                txtBranch.Text = "-";
                ChecNo.Text = "-";
                Card_no.Text = "-";
                Card_no.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ChecNo.Enabled = false;



                dateTimePicker1.ResetText();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        public void createAcceptedAmountTotal()
        {
            try
            {

                #region createAcceptedAmountTotal

                double add = (double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text));
                label21.Text = add.ToString();
                label27.Text = Convert.ToString(Convert.ToDouble(label27.Text) + add);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        public void totadueAmount()
        {
            try
            {
                #region Total Due Amount
                decimal gtotal = 0;
                foreach (ListViewItem lstItem in listView1.Items)
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[3].Text), 2);
                }
                label25.Text = Convert.ToString(gtotal);


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void TxtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCheque.Focus();
            }
        }

        private void TxtCash_Leave(object sender, EventArgs e)
        {
            if (TxtCash.Text == "")
            {
                TxtCash.Text = "0.00";
            }
        }

        private void TxtCash_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void txtCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txtCheque.Text == "" || Convert.ToDouble(txtCheque.Text) == 0)
                {
                    txtCard.Focus();
                }

                if (Convert.ToDouble(txtCheque.Text) > 0)
                {
                    Select_Bank();
                    txtBank.Focus();
                    txtBank.DroppedDown = true;
                }
            }
        }

        private void txtCheque_Leave(object sender, EventArgs e)
        {
            if (txtCheque.Text == "" || double.Parse(txtCheque.Text) == 0)
            {
                // cleartextbox();
                txtCheque.Text = "0.0";
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ChecNo.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            if (txtCheque.Text == "")
            {
                return;
            }

            if (double.Parse(txtCheque.Text) > 0)
            {
                txtBank.Enabled = true;
                txtBranch.Enabled = true;
                ChecNo.Enabled = true;
                dateTimePicker1.Enabled = true;
                button3.Enabled = true;

            }
        }

        private void txtCard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (txtCard.Text == "")
                {
                    txtCard.Text = "0.0";
                }

                if (e.KeyValue == 13)
                {
                    // check cash,sheque,card AMOUNT-----------------------------------------------------------------------------------------------
                    if ((double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text)) > 0)
                    {
                        //  -----------------------------------------------------------------------------------------------------------------------------

                        //IF CARD AMOUNT 0 VALUES PASS LIST VIEW ------------------------------------------------------------------------------------------
                        if (double.Parse(txtCard.Text) == 0)
                        {
                            DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (re == DialogResult.Yes)
                            {

                                #region bank and check details OK or Not

                                if (Convert.ToDouble(txtCard.Text) != 0)
                                {
                                    if (Card_no.Text == "-")
                                    {
                                        MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtCard.Focus();
                                        return;
                                    }
                                }//end txtcard

                                if (Convert.ToDouble(txtCheque.Text) != 0)
                                {
                                    if (txtBank.Text == "-")
                                    {
                                        MessageBox.Show("Please fill the Bank Name !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtBank.Focus();
                                        return;
                                    }

                                    if (ChecNo.Text == "-")
                                    {
                                        MessageBox.Show("Please fill the Cheque number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        ChecNo.Focus();
                                        return;
                                    }
                                }//end txtcheque details

                                #endregion


                                if (txtCard.Text == "")
                                {
                                    txtCard.Text = "0.0";
                                    return;
                                }
                                createAcceptedAmountTotal();
                                // addcash_card_check_on_listview();

                                #region addcash_card_check_on_listview
                                ListViewItem li;
                                li = new ListViewItem(TxtCash.Text);
                                li.SubItems.Add(txtCheque.Text);
                                li.SubItems.Add(txtCard.Text);

                                //Select the Bank -------------------
                                if (Convert.ToDouble(txtCheque.Text) != 0)
                                {
                                    li.SubItems.Add(txtBank.Text);
                                }

                                if (Convert.ToDouble(txtCheque.Text) == 0)
                                {
                                    li.SubItems.Add("-");
                                }

                                //------------------------------------------

                                li.SubItems.Add(ChecNo.Text);
                                li.SubItems.Add(Card_no.Text);
                                li.SubItems.Add(txtBranch.Text);
                                li.SubItems.Add(dateTimePicker1.Text);

                                listView2.Items.Add(li);
                                #endregion
                                cleartextbox();
                                button3.Focus();
                            }
                        }
                        else
                        {
                            Card_no.Enabled = true;
                            Card_no.Focus();
                        }
                        //  -----------------------------------------------------------------------------------------------------------------------------

                    }
                    else
                    {
                        DialogResult rest = MessageBox.Show("Please Enter Value..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (rest == DialogResult.OK)
                        {
                            TxtCash.Focus();
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtCard_Leave(object sender, EventArgs e)
        {
            if (txtCard.Text == "" || double.Parse(txtCard.Text) == 0)
            {
                //cleartextbox();
                txtCard.Text = "0.0";
            }
        }

        private void txtCard_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void txtBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBranch.Focus();
            }
        }

        private void txtBranch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                ChecNo.Focus();
            }
        }

        private void ChecNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCard.Focus();
            }
        }

        private void Card_no_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (e.KeyValue == 13)
                {
                    DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (re == DialogResult.Yes)
                    {
                        #region bank and check details OK or Not

                        if (Convert.ToDouble(txtCard.Text) != 0)
                        {
                            if (Card_no.Text == "-")
                            {
                                MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtCard.Focus();
                                return;
                            }
                        }//end txtcard

                        if (Convert.ToDouble(txtCheque.Text) != 0)
                        {
                            if (txtBank.Text == "-")
                            {
                                MessageBox.Show("Please fill the Bank Name !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBank.Focus();
                                return;
                            }

                            if (ChecNo.Text == "-")
                            {
                                MessageBox.Show("Please fill the Cheque number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ChecNo.Focus();
                                return;
                            }
                        }//end txtcheque details

                        #endregion


                        createAcceptedAmountTotal();
                        addcash_card_check_on_listview();
                        cleartextbox();
                        button3.Focus();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtBank_Leave(object sender, EventArgs e)
        {
            if (txtBank.Text == "")
            {
                txtBank.Text = "-";
            }
        }

        private void txtBranch_Leave(object sender, EventArgs e)
        {
            if (txtBranch.Text == "")
            {
                txtBranch.Text = "-";
            }
        }

        private void ChecNo_Leave(object sender, EventArgs e)
        {

            if (ChecNo.Text == "")
            {
                ChecNo.Text = "-";
            }
        }

        private void Card_no_Leave(object sender, EventArgs e)
        {
            if (Card_no.Text == "")
            {
                Card_no.Text = "-";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

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
        }

        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                PnlCustomerSerch.Visible = true;

               

                #region select the customer in the textbox by ID

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "' AND CusID LIKE '%" + textBox19.Text + "%' OR CusFirstName LIKE '%"+textBox19.Text+"%'";
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
            listView4.Items.Clear();
            label29.Text = "0.00";
            try
            {


                #region venderDetails fill in Textbox
                try
                {
                    DataGridViewRow dr = dataGridView1.SelectedRows[0];
                    txtSupID.Text = dr.Cells[0].Value.ToString();
                    Txtsup_name.Text = dr.Cells[1].Value.ToString();


                    PnlCustomerSerch.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                #endregion

                #region add value in list view
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectvalue = @" SELECT     InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.InvoiceDate, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance
            FROM         InvoicePaymentDetails FULL OUTER JOIN
            SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo FULL OUTER JOIN
            RepairNotes ON InvoicePaymentDetails.InvoiceID = RepairNotes.ReJobNumber
            WHERE     (InvoicePaymentDetails.PayBalance > 0) AND (SoldInvoiceDetails.CusStatus='" + txtSupID.Text + "' OR RepairNotes.ReCusID='" + txtSupID.Text + "')";

                SqlCommand cmd = new SqlCommand(selectvalue, con1);
                SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                listView1.Items.Clear();
                while (sd.Read() == true)
                {

                    ListViewItem li;
                    li = new ListViewItem(sd[0].ToString());
                    li.SubItems.Add(sd[1].ToString());
                    li.SubItems.Add(sd[2].ToString());
                    li.SubItems.Add(sd[3].ToString());
                    listView1.Items.Add(li);


                }

                totadueAmount();

                Customer_Total_Paymet_FrmDB();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //from Balance on list view  pass to Negotiated Amount textbox--------------------------------------------------------------  
            for (int a = 0; a <= listView4.Items.Count - 1; a++)
            {
                if (listView1.SelectedItems[0].SubItems[0].Text == listView4.Items[a].SubItems[0].Text)
                {
                    MessageBox.Show("This already in the payment table. please try another one or remove it and try again", "Duplicate Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ListViewItem item = listView1.SelectedItems[0];
            txtnegotiaAmount.Text = item.SubItems[3].Text;
            txtnegotiaAmount.Enabled = true;
            txtnegotiaAmount.Focus();
            //--------------------------------------------------------------------------------------------------------------------------
           
        }

        string storeValue;

        private void txtnegotiaAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                #region value pass from listview1 to listview2 with calculation

                if (e.KeyValue == 13)
                {
                    //from Balance on list view check Graterthan Negotiated Amount textbox--------------------------------------------------------------  

                    if (txtnegotiaAmount.Text == "")
                    {
                        return;
                    }

                    ListViewItem item1 = listView1.SelectedItems[0];



                    if ((double.Parse(item1.SubItems[3].Text) >= (double.Parse(txtnegotiaAmount.Text))))
                    {

                        //-----------------------------------------------
                        if (Convert.ToDouble(txtnegotiaAmount.Text) == 0)
                        {
                            MessageBox.Show("Negotiate amount cannot be Zero or empty!", "Error negotiate amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtnegotiaAmount.Focus();
                            return;
                        }//close if
                        //--------------------------------------------


                        //check negotiated value & Remain total--------------------------------------------------------------  
                        if ((double.Parse(txtTotalRem.Text) >= (double.Parse(txtnegotiaAmount.Text))))
                        {

                            //string min2 = (double.Parse(txtTotalRem.Text) - double.Parse(txtnegotiaAmount.Text)).ToString();
                            //txtTotalRem.Text = min2;
                            //storeValue = label27.Text;



                            ListViewItem item = listView1.SelectedItems[0];
                            ListViewItem li;
                            li = new ListViewItem(item.SubItems[0].Text);
                            li.SubItems.Add(dateTimePicker2.Text);
                            li.SubItems.Add(item.SubItems[2]);

                            //
                            string pastBalan = (double.Parse(item.SubItems[3].Text) - double.Parse(txtnegotiaAmount.Text)).ToString();
                            li.SubItems.Add(pastBalan);
                            li.SubItems.Add(txtnegotiaAmount.Text);

                            //=====================================================================

                            listView4.Items.Add(li);

                            txtnegotiaAmount.Text = "0.0";
                            txtnegotiaAmount.Enabled = false;

                            TotalPayamount();

                            MessageBox.Show("Added to the payment Details", "Added Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);




                            if (txtnegotiaAmount.Text == "")
                            {
                                txtnegotiaAmount.Text = "0.00";
                            }

                        } //check negotiated value & Remain total

                        else
                        {
                            MessageBox.Show("given money less than you type price", "WarningException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtnegotiaAmount.Text = "0.0";
                            return;

                        }

                    }//from Balance on list view check Graterthan Negotiated Amount textbox
                    else
                    {
                        MessageBox.Show("You cannot pay more than that Invoice Have", "WarningException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtnegotiaAmount.Text = "0.0";
                        return;

                    }
                }//end mail if
                #endregion

                ListView4Item_Count();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            double cash = Convert.ToDouble(listView2.SelectedItems[0].SubItems[0].Text);
            double chek = Convert.ToDouble(listView2.SelectedItems[0].SubItems[1].Text);
            double card = Convert.ToDouble(listView2.SelectedItems[0].SubItems[2].Text);

            label27.Text = Convert.ToString(Convert.ToDouble(label27.Text) - (cash + chek + card));

            listView2.SelectedItems[0].Remove();
            txtBank.SelectedIndex = -1;

        }

        private void Customer_Credit_Payments_Load(object sender, EventArgs e)
        {
            TxtCash.Focus();
            getCreateStockCode();
            
        }

        private void label27_TextChanged(object sender, EventArgs e)
        {
            //CalcTotRemaining();

            Customer_Total_Paymet_FrmDB();

            //vender button ennable or dissable-------------------------------
            //if (Convert.ToDouble(label27.Text) == 0)
            //{
            //    button3.Enabled = false;
            //}

            //else
            //{
            //    button3.Enabled = true;
            //}
        }

        private void txtTotalRem_TextChanged(object sender, EventArgs e)
        {
           // CalcTotRemaining();

                //if (Convert.ToDouble(txtTotalRem.Text) == 0)
                //{
                //    button1.Enabled = false;
                //    listView1.Enabled = false;
                //    txtnegotiaAmount.Enabled = false;
                //}

                //else
                //{
                //    button1.Enabled = false;
                //    listView1.Enabled = false;
                //    txtnegotiaAmount.Enabled = false;
                //}
        }

        private void listView4_MouseDoubleClick(object sender, MouseEventArgs e)
        {         

            listView4.SelectedItems[0].Remove();

            TotalPayamount();

            ListView4Item_Count();

        }

        Double totCash = 0;
        Double totCheque = 0;
        Double totcreditcard = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region value insert to Customer_Payment_Details table  in DB

                DialogResult result = MessageBox.Show("Do you want to complete the GRN Payment details..?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    getCreateStockCode();

                    for (int i = 0; i <= listView2.Items.Count - 1; i++)
                    {

                        totCash +=Double.Parse( listView2.Items[i].SubItems[0].Text);
                        totCheque += Double.Parse(listView2.Items[i].SubItems[1].Text);
                        totcreditcard += Double.Parse(listView2.Items[i].SubItems[0].Text);



                        #region customer paymet details Insert------------------------------------------------

                        SqlConnection con1 = new SqlConnection(IMS);
                        con1.Open();
                        string add = "INSERT INTO Customer_Payment_Details (Docu_No,Cash_Amount,Cheque_Amount,Cheque_No,Branch,Card_Amount,Card_No )values(@Docu_No,@Cash_Amount,@Cheque_Amount,@Cheque_No,@Branch,@Card_Amount,@Card_No )";
                        SqlCommand cmd = new SqlCommand(add, con1);

                        cmd.Parameters.AddWithValue("Docu_No", lblDocumentNo.Text);
                        cmd.Parameters.AddWithValue("Cash_Amount", listView2.Items[i].SubItems[0].Text);
                        cmd.Parameters.AddWithValue("Cheque_Amount", listView2.Items[i].SubItems[1].Text);

                        // cmd.Parameters.AddWithValue("Bank", listView2.Items[i].SubItems[3].Text);
                        cmd.Parameters.AddWithValue("Cheque_No", listView2.Items[i].SubItems[4].Text);
                        cmd.Parameters.AddWithValue("Branch", listView2.Items[i].SubItems[6].Text);
                        // cmd.Parameters.AddWithValue("Cheque_date", listView2.Items[i].SubItems[7].Text);
                        cmd.Parameters.AddWithValue("Card_Amount", listView2.Items[i].SubItems[2].Text);
                        cmd.Parameters.AddWithValue("Card_No", listView2.Items[i].SubItems[5].Text);

                        cmd.ExecuteNonQuery();

                        if (con1.State == ConnectionState.Open)
                        {
                            con1.Close();
                        }

                        #endregion

                        #region Update Set_Off_Details_when pay cash from main cash.....................................................

                        if (Convert.ToDouble(listView2.Items[i].SubItems[0].Text) != 0)
                        {
                            //generate last invoice balance and last invoice number.............................
                           // Last_SetOFF_Details();

                            //if (Double.Parse(Last_SET_OFF_BAL) == 0.0)
                            //{
                            //    MessageBox.Show("Please Set off....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            //else
                            //{
                                //Last_SetOFF_Details();

                              //  Double Calc_SET_OFF_Bal = Convert.ToDouble(Last_SET_OFF_BAL) + Convert.ToDouble(listView2.Items[i].SubItems[0].Text);

                                try
                                {
//                                    SqlConnection cnn = new SqlConnection(IMS);
//                                    cnn.Open();
//                                    string insernewid = @"insert into Set_Off_Details(DOC_Num, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
//                                        values('" + lblDocumentNo.Text + "','Customer_Credit_Payment','" + Invoiced_Last_Auto_ID + "','" + Last_SET_OFF_BAL + "','" + listView2.Items[i].SubItems[0].Text + "','0','" + Calc_SET_OFF_Bal + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "') ";
//                                    SqlCommand cmm = new SqlCommand(insernewid, cnn);
//                                    cmm.ExecuteNonQuery();
//                                    cnn.Close();

                                    


                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                }
                           // }
                        }

                        #endregion

                        #region insert Chq Details===============================================================================================

                        if (Convert.ToDouble(listView2.Items[i].SubItems[1].Text) != 0)
                        {
                            SqlConnection con6 = new SqlConnection(IMS);
                            con6.Open();
                            string InsertCheckPaymetDetails = "INSERT INTO InvoiceCheckDetails(InvoiceID,CkStatus,CkNumber,Bank,Amount,CurrentDate,MentionDate) VALUES(@InvoiceID1,@CkStatus,@CkNumber,@Bank,@Amount,@CurrentDate,@MentionDate)";
                            SqlCommand cmd6 = new SqlCommand(InsertCheckPaymetDetails, con6);

                            cmd6.Parameters.AddWithValue("InvoiceID1", lblDocumentNo.Text);
                            cmd6.Parameters.AddWithValue("CkStatus", "Active");
                            cmd6.Parameters.AddWithValue("CkNumber", listView2.Items[i].SubItems[4].Text);
                            //cmd6.Parameters.AddWithValue("Bank", listView2.Items[i].SubItems[3].Text);
                            cmd6.Parameters.AddWithValue("Bank", BankID);
                            cmd6.Parameters.AddWithValue("Amount", listView2.Items[i].SubItems[1].Text);
                            cmd6.Parameters.AddWithValue("CurrentDate", DateTime.Today.ToShortDateString());
                            cmd6.Parameters.AddWithValue("MentionDate", listView2.Items[i].SubItems[7].Text);

                            cmd6.ExecuteNonQuery();

                           
                            if (con6.State == ConnectionState.Open)
                            {
                                cmd6.Dispose();
                                con6.Close();
                            }
                        }

                        #endregion

                    }

                    SqlConnection cnn = new SqlConnection(IMS);
                    cnn.Open();
                    string invoPyDetails = @"insert into InvoicePaymentDetails(InvoiceID, SubTotal, VATpresentage, GrandTotal, PayCash, PayCheck, PayCrditCard, PayDebitCard, PAyCredits, PayBalance, InvoiceDate, InvoiceDiscount)
                                            values('"+lblDocumentNo.Text+"','"+label27.Text+"','"+"0.0"+"','"+label27.Text+"','"+totCash+"','"+totCheque+"','"+totcreditcard+"','"+"0.0"+"','"+"0.0"+"','"+"0.0"+"','"+dateTimePicker2.Text +"','"+"0.0"+"')";

                   // MessageBox.Show(invoPyDetails);
                    SqlCommand cmm = new SqlCommand(invoPyDetails, cnn);
                    cmm.ExecuteNonQuery();
                    cnn.Close();

                    



                #endregion

                #region value insert to Customer_Payment_Doc_Details table  in DB

                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

                    for (int a = 0; a <= listView4.Items.Count - 1; a++)
                    {
                        string add2 = "INSERT INTO Customer_Payment_Doc_Details (Docu_No,GRN_No,Date,Paid_amount,UserID,DOC_Status) values(@Docu_No,@GRN_No,@Date,@Paid_amount,@UserID,@DOC_Status)";
                        SqlCommand cmd2 = new SqlCommand(add2, con);
                        cmd2.Parameters.AddWithValue("Docu_No", lblDocumentNo.Text);
                        cmd2.Parameters.AddWithValue("GRN_No", listView4.Items[a].SubItems[0].Text);
                        cmd2.Parameters.AddWithValue("Date", dateTimePicker2.Text);
                        cmd2.Parameters.AddWithValue("Paid_amount", listView4.Items[a].SubItems[4].Text);
                        cmd2.Parameters.AddWithValue("UserID", LgUser.Text);
                        cmd2.Parameters.AddWithValue("DOC_Status","1");
                        cmd2.ExecuteNonQuery();


                        // update pay balance==================================================
                        SqlConnection con3 = new SqlConnection(IMS);
                        con3.Open();
                        string upadatevalue = "UPDATE InvoicePaymentDetails SET PayBalance='" + listView4.Items[a].SubItems[3].Text + "'  where InvoiceID='" + listView4.Items[a].SubItems[0].Text + "'";
                        SqlCommand cmd = new SqlCommand(upadatevalue, con3);


                        cmd.ExecuteNonQuery();



                        //=======================================================

                    }

                    Customer_Final_Balance_Update();

                    MessageBox.Show("Successfully updated Customer credit payment values", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    listView1.Items.Clear();
                    listView2.Items.Clear();
                    listView4.Items.Clear();
                    label25.Text = "0.0";
                    label21.Text = "0.0";
                    txtTotalRem.Text = "0.00";
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                    ChecNo.Enabled = false;
                    Card_no.Enabled = false;
                    txtSupID.Clear();
                    Txtsup_name.Clear();
                    TxtCash.Focus();
                    getCreateStockCode();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                #endregion

                    label27.Text = "0.00";
                    label29.Text = "0.00";
                    button1.Enabled = false;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void label29_TextChanged(object sender, EventArgs e)
        {

           // CalcTotRemaining();

            Customer_Total_Paymet_FrmDB();
        }

        private void TxtCash_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCard_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ChecNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtnegotiaAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBank_DropDown(object sender, EventArgs e)
        {

        }

        private void txtBank_Click(object sender, EventArgs e)
        {
            Select_Bank();
        }

        private void txtBank_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                txtBranch.Focus();
            }
        }
        string BankID;
        private void txtBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                #region select the bank ID---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankName,BankID FROM Bank_Category where BankName='" + txtBank.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================



                while (dr.Read() == true)
                {
                    //txtBank.Items.Add(dr[0].ToString());
                    BankID = (dr[1].ToString());
                }

                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }

                #endregion

                if (txtBank.SelectedIndex == -1)
                {
                    txtBank.Focus();
                    return;
                }
                if (txtBank.SelectedIndex != -1)
                {
                    ChecNo.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (txtCard.Text == "")
                {
                    txtCard.Text = "0.0";
                }

                if (txtCard.Text == "")
                {
                    txtCard.Text = "0.0";
                    return;
                }

                if (TxtCash.Text == "" && txtCard.Text == "" && txtCheque.Text == "0.0" || TxtCash.Text == "0.0" && txtCard.Text == "0.0" && txtCheque.Text == "0.0")
                {
                    MessageBox.Show("Please Enter Amount....", "Message");
                    TxtCash.Focus();
                    return;

                }




                #region bank and check details OK or Not

                if (Convert.ToDouble(txtCard.Text) != 0)
                {
                    if (Card_no.Text == "-")
                    {
                        MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCard.Focus();
                        return;
                    }
                }//end txtcard

                if (Convert.ToDouble(txtCheque.Text) != 0)
                {
                    if (txtBank.Text == "-")
                    {
                        MessageBox.Show("Please fill the Bank Name !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBank.Focus();
                        return;
                    }

                    if (ChecNo.Text == "-")
                    {
                        MessageBox.Show("Please fill the Cheque number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ChecNo.Focus();
                        return;
                    }
                }//end txtcheque details

                #endregion




                // check cash,sheque,card AMOUNT-----------------------------------------------------------------------------------------------
                if ((double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text)) > 0)
                {
                    //  -----------------------------------------------------------------------------------------------------------------------------

                    //IF CARD AMOUNT 0 VALUES PASS LIST VIEW ------------------------------------------------------------------------------------------
                    if (double.Parse(txtCard.Text) == 0)
                    {
                        DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (re == DialogResult.Yes)
                        {



                            createAcceptedAmountTotal();
                            // addcash_card_check_on_listview();

                            #region addcash_card_check_on_listview
                            ListViewItem li;
                            li = new ListViewItem(TxtCash.Text);
                            li.SubItems.Add(txtCheque.Text);
                            li.SubItems.Add(txtCard.Text);

                            //Select the Bank -------------------
                            if (Convert.ToDouble(txtCheque.Text) != 0)
                            {
                                li.SubItems.Add(txtBank.Text);
                            }

                            if (Convert.ToDouble(txtCheque.Text) == 0)
                            {
                                li.SubItems.Add("-");
                            }

                            //------------------------------------------

                            li.SubItems.Add(ChecNo.Text);
                            li.SubItems.Add(Card_no.Text);
                            li.SubItems.Add(txtBranch.Text);
                            li.SubItems.Add(dateTimePicker1.Text);

                            listView2.Items.Add(li);
                            #endregion
                            cleartextbox();
                            txtBank.SelectedIndex = -1;
                            button3.Focus();
                        }
                    }
                    else
                    {
                        Card_no.Enabled = true;
                        Card_no.Focus();
                    }
                    //  -----------------------------------------------------------------------------------------------------------------------------

                }
                else
                {
                    DialogResult rest = MessageBox.Show("Please Enter Value..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (rest == DialogResult.OK)
                    {
                        TxtCash.Focus();
                    }
                }


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
