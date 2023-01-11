using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;

namespace Inventory_Control_System
{
    public partial class GRN_Payment_details : Form
    {
        public GRN_Payment_details()
        {
            InitializeComponent();
            getCreateStockCode();
        }
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        //double DB_Remind_Val = 0;

        string Invoiced_Last_Auto_ID = "";
        String Last_SET_OFF_BAL = "";
        string SetOff_ID = "";


        public void VentoeTotalPaymetFrmDB()
        {
            try
            {
            #region Select vendoor paymernt summary--------------------------

            double LastBalance = 0;
            double Debit_Balance = 0;

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            string sql1 = "SELECT TOP (1) Balance,Debit_Balance FROM vender_Payment WHERE (VenderID = '" + txtSupID.Text + "') ORDER BY AutoNum DESC";
            SqlCommand cmd1 = new SqlCommand(sql1, Conn);
            SqlDataReader dr7 = cmd1.ExecuteReader();

            if (dr7.Read())
            {
                LastBalance = Convert.ToDouble(dr7[0].ToString());
                Debit_Balance = Convert.ToDouble(dr7[1].ToString());
            }

            //check the balesce >0 or <0-----------------------------------

            //if (LastBalance >= 0)
            //{
            //    txtTotalRem.Text = Convert.ToString(Convert.ToDouble(label27.Text)+ LastBalance- Convert.ToDouble(label29.Text));
            //}

            //if (LastBalance < 0)
            //{
            //    txtTotalRem.Text = Convert.ToString(Convert.ToDouble(label27.Text)- Convert.ToDouble(label29.Text));
            //}

              txtTotalRem.Text = Convert.ToString(Convert.ToDouble(label27.Text) - Convert.ToDouble(label29.Text) + Debit_Balance);


            cmd1.Dispose();
            dr7.Close();
            Conn.Close();

            #endregion

             }
            catch (Exception ex)
            {
                MessageBox.Show("this error came from the supplier payment summary", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Vendor_Balance_Payment_Update()
        {
            #region Vendor_Balance_Payment_Update--------------------------

            double LastBalance = 0;
            double New_Bal = 0;

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            string sql1 = "SELECT TOP (1) Balance FROM vender_Payment WHERE (VenderID = '" + txtSupID.Text + "') ORDER BY AutoNum DESC";
            SqlCommand cmd1 = new SqlCommand(sql1, Conn);
            SqlDataReader dr7 = cmd1.ExecuteReader();

            if (dr7.Read())
            {
                LastBalance = Convert.ToDouble(dr7[0].ToString());
            }

            //balance calc-----------------------------
            double Calc_Bal = LastBalance - Convert.ToDouble(label27.Text);

            //check the balesce >0 or <0-----------------------------------

            if (LastBalance >= 0)
            {
                New_Bal = Calc_Bal; 
                #region New Vendor Previos Remainder is Possitive Value----------------------------

//                //balance calc-----------------------------
//                New_Bal = LastBalance + Convert.ToDouble(label27.Text) - Convert.ToDouble(label29.Text);

//                SqlConnection con1 = new SqlConnection(IMS);
//                con1.Open();

//                string Vend_DebitPaymet = @"INSERT INTO vender_Payment(VenderID, DocNumber, Credit_Amount, Debit_Amount, Balance, Date) 
//                                                VALUES  ('" + txtSupID.Text + "','" + lblDocumentNo.Text + "','0','"+label27.Text+"','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

//                SqlCommand cmd21 = new SqlCommand(Vend_DebitPaymet, con1);
//                cmd21.ExecuteNonQuery();

//                if (con1.State == ConnectionState.Open)
//                {
//                    con1.Close();
//                }

                #endregion
            }

            if (LastBalance < 0)
            {
                New_Bal = LastBalance + Convert.ToDouble(label27.Text);

                #region New Vendor Previos Remainder is Negative Value--------------------------------

//                //balance calc-----------------------------
//                New_Bal = LastBalance + Convert.ToDouble(label27.Text);

//                SqlConnection con1 = new SqlConnection(IMS);
//                con1.Open();

//                string Vend_DebitPaymet = @"INSERT INTO vender_Payment(VenderID, DocNumber, Credit_Amount, Debit_Amount, Balance, Date) 
//                                                VALUES  ('" + txtSupID.Text + "','" + lblDocumentNo.Text + "','0','" + label27.Text + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

//                SqlCommand cmd21 = new SqlCommand(Vend_DebitPaymet, con1);
//                cmd21.ExecuteNonQuery();

//                if (con1.State == ConnectionState.Open)
//                {
//                    con1.Close();
//                }

                #endregion
            }

            //balance calc-----------------------------
           // New_Bal = LastBalance + Convert.ToDouble(label27.Text);

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string Vend_DebitPaymet = @"INSERT INTO vender_Payment(VenderID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) 
                                                VALUES  ('" + txtSupID.Text + "','" + lblDocumentNo.Text + "','0','" + label27.Text + "','" + txtTotalRem.Text + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

            SqlCommand cmd21 = new SqlCommand(Vend_DebitPaymet, con1);
            cmd21.ExecuteNonQuery();

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }

            cmd1.Dispose();
            dr7.Close();
            Conn.Close();

            #endregion

        }

        public void Last_SetOFF_Details()
        {

            #region last Invoice Auto ID Select ---------------------------------

            try
            {

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT TOP 1 InvoiceAutoID,Remain_Balance,DOC_Num FROM Set_Off_Details order by AutoID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                if (dr.Read() == true)
                {
                    Invoiced_Last_Auto_ID = dr[0].ToString();
                    Last_SET_OFF_BAL = dr[1].ToString();

                    //create set off ID#--------------------------------------------------------------------
                    string ST_Num = dr[2].ToString();

                    string OrderNumOnly = ST_Num.Substring(3);

                    ST_Num = (Convert.ToInt32(OrderNumOnly) + 1).ToString(); 

                    SetOff_ID = "STF" + ST_Num;  //set off id

                    Conn.Close();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("this error came from the last set off details", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }


        public void Select_Bank()
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

            while(dr.Read()==true)
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

        public void Select_BankID()
        {
            #region select the bank---------------------

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "SELECT BankID FROM Bank_Category WHERE BankName='"+txtBank.Text+"'";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================

            //txtBank.Items.Clear();

            if (dr.Read() == true)
            {
               txtBankId.Text= dr[0].ToString();
            }

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                dr.Close();
            }

            #endregion
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

        public void addcash_card_check_on_listview ()
        {
            #region addcash_card_check_on_listview..........................
            

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

            //Select the Bank account -------------------
            if (Convert.ToDouble(txtCheque.Text) != 0)
            {
                li.SubItems.Add(AccNumbers.Text);
               // MessageBox.Show(AccNumbers.Text);
            }

            if (Convert.ToDouble(txtCheque.Text) == 0)
            {
                li.SubItems.Add("-");
            }

            //------------------------------------------

            //MessageBox.Show(AccNumbers.Text);
            listView2.Items.Add(li);


            #endregion
        }
        public void cleartextbox()
        {
            #region cleartextBox----------------------------

            TxtCash.Text = "0.0";
             txtCheque.Text = "0.0";
             txtCard.Text = "0.0";
             txtBank.Items.Clear();
             txtBranch.Text = "-";
             ChecNo.Text = "-";
             Card_no.Text = "-";
             Card_no.Enabled = false;
             txtBank.Enabled = false;
             AccNumbers.Enabled = false;
             AccNumbers.Items.Clear();

             dateTimePicker1.ResetText();
            
            #endregion
        }
        public void createAcceptedAmountTotal()
        {
            #region createAcceptedAmountTotal

            double add = (double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text));
            label21.Text = add.ToString();
            label27.Text = Convert.ToString(Convert.ToDouble(label27.Text)+add);



            #endregion
        }

        public void CalcTotRemaining()
        {
            #region calculate the total payment we can pay now================================


            txtTotalRem.Text = Convert.ToString(Convert.ToDouble(txtTotalRem.Text)+ Convert.ToDouble(label27.Text)-Convert.ToDouble(label29.Text));

            #endregion
        }
        public void totadueAmount()
        {
            #region Total Due Amount

            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listView1.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[3].Text),2);
            }
            label25.Text = Convert.ToString(gtotal);

            #endregion
        }

        public void TotalPayamount()
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

        public void CkAndCard_details_Validate()
        {
            #region bank and check details OK or Not

            if (Convert.ToDouble(txtCard.Text) != 0)
            {
                if (Card_no.Text == "-")
                {
                    MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
        }


        public void getCreateStockCode()
        {
            #region New Document Code auto generate...........................................
            //try
            //{
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Docu_No from GRN_Payment_Details";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                lblDocumentNo.Text = "VCP10000001";

                cmd.Dispose();
                dr.Close();

            }

            else
            {

                cmd.Dispose();
                dr.Close();

                string sql1 = " SELECT TOP 1 Docu_No FROM GRN_Payment_Details order by Docu_No DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string OrderNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                    lblDocumentNo.Text = "VCP" + no;

                }
                cmd1.Dispose();
                dr7.Close();

            }
            Conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
            #endregion
        }

       
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCard_Layout(object sender, LayoutEventArgs e)
        {
           

        }

        private void txtCard_Leave(object sender, EventArgs e)
        {
            if (txtCard.Text == "" || double.Parse(txtCard.Text) == 0)
            {
                //cleartextbox();
                txtCard.Text = "0.0";
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
                AccNumbers.Enabled = true;
                label33.Visible = true;
                label34.Visible = true;
                label35.Visible = true;
            
            }
            if (double.Parse(txtCheque.Text) == 0 || txtCheque.Text == "")
            {
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ChecNo.Enabled = false;
                dateTimePicker1.Enabled = false;
              //  button3.Enabled = false;
                AccNumbers.Enabled = false;
                txtBranch.Enabled = false;
                label33.Visible = false;
                label34.Visible = false;
                label35.Visible = false;
            }
        }

        private void txtCard_TextChanged(object sender, EventArgs e)
        {
            if(txtCard.Text == "" )
            {
                return;
            }


            if((double.Parse(txtCard.Text)) > 0.0)
            {
                Card_no.Enabled = true;
                label36.Visible = true;
            }
            if (double.Parse(txtCard.Text) == 0 || (txtCard.Text ==""))
            {
                Card_no.Enabled = false;
                label36.Visible = false;
            }
            button3.Enabled = true;
        }

        private void Card_no_Leave(object sender, EventArgs e)
        {
          
        }

        private void Card_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCash_Leave(object sender, EventArgs e)
        {
            if (TxtCash.Text == "")
            {
                TxtCash.Text = "0.00";
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
                AccNumbers.Enabled = false;
                AccNumbers.Items.Clear();
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
                    txtBank.Focus();

                    Select_Bank();
                    txtBank.DroppedDown = true;


                  

                 
                }
            }
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
        Double sumlist = 0;
        Double SumtextValue = 0;
        Double BalanceLAstSetof = 0;
        private void txtCard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (txtCard.Text == "")
                {
                    return;
                }






                if (e.KeyValue == 13)
                {


                    Last_SetOFF_Details();

                    if (Double.Parse(TxtCash.Text) > 0)
                    {

                        if (Last_SET_OFF_BAL == "" || Double.Parse(Last_SET_OFF_BAL) == 0)
                        {

                            MessageBox.Show("You have not Enough money to pay to the Supplier,please Check Setoff Details...! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TxtCash.Text = "0.0";
                            return;

                        }

                        else
                        {
                            // MessageBox.Show("in");
                            sumlist = 0;
                            for (int a = 0; a <= listView2.Items.Count - 1; a++)
                            {
                                sumlist += (Convert.ToDouble(listView2.Items[a].SubItems[0].Text));

                            }

                            // SumtextValue = sumlist + Double.Parse(TxtCash.Text);
                            BalanceLAstSetof = Double.Parse(Last_SET_OFF_BAL) - sumlist;
                            // MessageBox.Show(BalanceLAstSetof.ToString());

                            SumtextValue = BalanceLAstSetof - (Double.Parse(TxtCash.Text));


                            //Last_SetOFF_Details();
                            if (SumtextValue < 0)
                            {
                                // MessageBox.Show(sumlist.ToString());
                                MessageBox.Show("You have only Rs:" + BalanceLAstSetof + " Amount on Hand. Please try less than or equal amount", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                TxtCash.Focus();
                                TxtCash.Text = "0.0";
                                return;

                            }

                        }
                    }


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
                                        Card_no.Focus();
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

                                    if (AccNumbers.Text == "")
                                    {
                                        MessageBox.Show("Please Select an account number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        AccNumbers.Focus();
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
                                addcash_card_check_on_listview();

                                #region addcash_card_check_on_listview
                                //ListViewItem li;
                                //li = new ListViewItem(TxtCash.Text);
                                //li.SubItems.Add(txtCheque.Text);
                                //li.SubItems.Add(txtCard.Text);

                                ////Select the Bank -------------------
                                //if (Convert.ToDouble(txtCheque.Text) != 0)
                                //{ 
                                //    li.SubItems.Add(txtBank.Text);
                                //}

                                //if (Convert.ToDouble(txtCheque.Text) == 0)
                                //{
                                //    li.SubItems.Add("-");
                                //}

                                //------------------------------------------

                                //li.SubItems.Add(ChecNo.Text);
                                //li.SubItems.Add(Card_no.Text);
                                //li.SubItems.Add(txtBranch.Text);
                                //li.SubItems.Add(dateTimePicker1.Text);

                                ////Select the Bank account -------------------
                                //if (Convert.ToDouble(txtCheque.Text) != 0)
                                //{
                                //    li.SubItems.Add(AccNumbers.Text);
                                //    MessageBox.Show(AccNumbers.Text);
                                //}

                                //if (Convert.ToDouble(txtCheque.Text) == 0)
                                //{
                                //    li.SubItems.Add("-");
                                //}

                                ////------------------------------------------

                                ////MessageBox.Show(AccNumbers.Text);
                                //listView2.Items.Add(li);
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
                MessageBox.Show("This error came from the value pass", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Card_no_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCard.Focus();
            }
        }

        private void ChecNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void TxtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCheque.Focus();
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            #region load vendor details in gridView
            pnlSearchVenderinReceiveOrder.Visible = true;
            pnlSearchVenderinReceiveOrder.BringToFront();

            try
            {
                #region Select vendors to the grid--------------------------

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                string VenSelectAll = @"SELECT VenderID,VenderName,VenderPHAddress FROM  VenderDetails";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();
                while (dr.Read())
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2]);
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    dr.Close();
                }
                #endregion


            }
            catch(Exception ex)
            {
                MessageBox.Show("this error came from the button 03 click", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            #region search vendor in grideview textbox
            try
            {


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress FROM  VenderDetails WHERE VenderID LIKE '%" + textBox2.Text + "%' OR VenderName LIKE '%" + textBox2.Text + "%' ";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("this error came from the selecttxtbox 2 key up", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
   
        }

        

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            listView4.Items.Clear();
            label29.Text = "0.00";

            #region venderDetails fill in Textbox
            //try
            //{
                DataGridViewRow dr = dataGridView3.SelectedRows[0];
                txtSupID.Text = dr.Cells[0].Value.ToString();
                Txtsup_name.Text = dr.Cells[1].Value.ToString();


                pnlSearchVenderinReceiveOrder.Visible = false;





            #endregion

                #region add value in list view

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectvalue = "select  GRNInvoicePaymentDetails.GRNID,GRN_amount_Details.[Date],GRNInvoicePaymentDetails.Net_Amount,GRNInvoicePaymentDetails.PayBalance from GRN_amount_Details inner join GRNInvoicePaymentDetails on GRN_amount_Details.GRN_No=GRNInvoicePaymentDetails.GRNID where (GRNInvoicePaymentDetails.PayBalance>0) AND (GRN_amount_Details.Vender_ID='" + txtSupID.Text + "')";
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

                VentoeTotalPaymetFrmDB();


                //ListViewItem li;
                //li = new ListViewItem(dr[0].ToString());


                //li.SubItems.Add(dr[0].ToString());
                //li.SubItems.Add(dr[1].ToString());
                #endregion
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pnlSearchVenderinReceiveOrder.Visible = false;
        }

        private void Card_no_KeyDown_1(object sender, KeyEventArgs e)
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
                            MessageBox.Show("Please fill the Card Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Card_no.Focus();
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

                    addcash_card_check_on_listview();
                    createAcceptedAmountTotal();
                   
                    cleartextbox();
                    button3.Focus();
                }
            }
            #endregion
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
           
        }

       // string storeValue;
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
                            }//close if

                        } //check negotiated value & Remain total

                        else
                        {
                            MessageBox.Show("given money less than you type price", "WarningException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtnegotiaAmount.Text = "0.0";
                            return;

                        }//end else

                    }//from Balance on list view check Graterthan Negotiated Amount textbox
                    else
                    {
                        MessageBox.Show("You cannot pay more than that Invoice Have", "WarningException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtnegotiaAmount.Text = "0.0";
                        return;

                    }//end else
                }//end mail if
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("this error came from the txtnegotiat amount", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            ListView4Item_Count();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txtnegotiaAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtnegotiaAmount_Leave(object sender, EventArgs e)
        {
           
        }

        private void label27_Leave(object sender, EventArgs e)
        {
          

        }

        private void label21_TextChanged(object sender, EventArgs e)
        {
            //label29.Text = label21.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            #region value insert to GRN_Payment_Details table  in DB

            DialogResult result = MessageBox.Show("Do you want to complete the GRN..?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    getCreateStockCode();

                    for(int i=0; i<= listView2.Items.Count-1 ;i++)
                    {
                        
                     #region customer paymet details Insert------------------------------------------------

                     SqlConnection con1 = new SqlConnection(IMS);
                     con1.Open();
                     string add = "INSERT INTO GRN_Payment_Details (Docu_No,Cash_Amount,Cheque_Amount,Cheque_No,Branch,Card_Amount,Card_No )values(@Docu_No,@Cash_Amount,@Cheque_Amount,@Cheque_No,@Branch,@Card_Amount,@Card_No )";
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
                         Last_SetOFF_Details();

                         Double Calc_SET_OFF_Bal = Convert.ToDouble(Last_SET_OFF_BAL) - Convert.ToDouble(listView2.Items[i].SubItems[0].Text);

                         try
                         {
                             SqlConnection cnn = new SqlConnection(IMS);
                             cnn.Open();
                             string insernewid = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                        values('" + SetOff_ID + "','" + lblDocumentNo.Text + "','Pay_From_GRN','" + Invoiced_Last_Auto_ID + "','" + Last_SET_OFF_BAL + "','0','" + listView2.Items[i].SubItems[0].Text + "','" + Calc_SET_OFF_Bal + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "') ";
                             SqlCommand cmm = new SqlCommand(insernewid, cnn);
                             cmm.ExecuteNonQuery();
                             cnn.Close();


                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                         }
                        
                     }

                     #endregion

                     #region insert Chq Details===============================================================================================

                     if (Convert.ToDouble(listView2.Items[i].SubItems[1].Text) != 0)
                     {
                         SqlConnection con6 = new SqlConnection(IMS);
                         con6.Open();
                         string InsertCheckPaymetDetails = "INSERT INTO InvoiceCheckDetails(InvoiceID,CkStatus,CkNumber,Bank,Amount,CurrentDate,MentionDate,Ck_Bank_acc_Number) VALUES(@InvoiceID1,@CkStatus,@CkNumber,@Bank,@Amount,@CurrentDate,@MentionDate,@Ck_Bank_acc_Number)";
                         SqlCommand cmd6 = new SqlCommand(InsertCheckPaymetDetails, con6);

                       //  MessageBox.Show(listView2.Items[i].SubItems[7].Text);

                         cmd6.Parameters.AddWithValue("InvoiceID1", lblDocumentNo.Text);
                         cmd6.Parameters.AddWithValue("CkStatus", "Active");
                         cmd6.Parameters.AddWithValue("CkNumber", listView2.Items[i].SubItems[4].Text);
                         cmd6.Parameters.AddWithValue("Bank", txtBankId.Text);
                         cmd6.Parameters.AddWithValue("Amount", listView2.Items[i].SubItems[1].Text);
                         cmd6.Parameters.AddWithValue("CurrentDate", DateTime.Today.ToShortDateString());
                         cmd6.Parameters.AddWithValue("MentionDate", listView2.Items[i].SubItems[7].Text);
                         cmd6.Parameters.AddWithValue("Ck_Bank_acc_Number",listView2.Items[i].SubItems[8].Text);

                         cmd6.ExecuteNonQuery();


                         if (con6.State == ConnectionState.Open)
                         {
                             cmd6.Dispose();
                             con6.Close();
                         }
                     }

                     #endregion

                    }//close for loop
            #endregion

            #region value insert to GRN_Payment_Doc_Details table  in DB

                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

                    for(int a=0; a<= listView4.Items.Count-1 ;a++)
                    {
                        string add2 = "INSERT INTO GRN_Payment_Doc_Details (Docu_No,GRN_No,Date,Paid_amount,UserID) values(@Docu_No,@GRN_No,@Date,@Paid_amount,@UserID)";
                     SqlCommand cmd2 = new SqlCommand(add2, con);
                     cmd2.Parameters.AddWithValue("Docu_No", lblDocumentNo.Text);
                     cmd2.Parameters.AddWithValue("GRN_No", listView4.Items[a].SubItems[0].Text);
                     cmd2.Parameters.AddWithValue("Date", dateTimePicker2.Text);
                     cmd2.Parameters.AddWithValue("Paid_amount", listView4.Items[a].SubItems[4].Text);
                     cmd2.Parameters.AddWithValue("UserID", LgUser.Text);

                     cmd2.ExecuteNonQuery();


                        // update pay balance==================================================
                     SqlConnection con3 = new SqlConnection(IMS);
                     con3.Open();
                     string upadatevalue = "UPDATE GRNInvoicePaymentDetails SET PayBalance='" + listView4.Items[a].SubItems[3].Text + "'  where GRNID='" + listView4.Items[a].SubItems[0].Text + "'";
                     SqlCommand cmd = new SqlCommand(upadatevalue, con3);
                   

                     cmd.ExecuteNonQuery();

                        //=======================================================

                    }

            

                    Vendor_Balance_Payment_Update();

                    MessageBox.Show("Successfully updated GRN credit payment values","Completed",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    #region clear Txt as default-----------

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
                    label27.Text = "0.00";
                    label29.Text = "0.00";

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    getCreateStockCode();
                    #endregion

                }//close result yes IF

                getCreateStockCode();
            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_1", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            getCreateStockCode();

        }

        private void GRN_Payment_details_Load(object sender, EventArgs e)
        {
            getCreateStockCode();
        }

        private void Card_no_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void TxtCash_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // this.Visible = false;
            #region clear Txt as default-----------

            //listView1.Items.Clear();
            //listView2.Items.Clear();
            //listView4.Items.Clear();
            //label25.Text = "0.0";
            //label21.Text = "0.0";
            //txtTotalRem.Text = "0.00";
            //txtBank.Enabled = false;
            //txtBranch.Enabled = false;
            //ChecNo.Enabled = false;
            //Card_no.Enabled = false;
            //txtSupID.Clear();
            //Txtsup_name.Clear();
            //TxtCash.Focus();
            //getCreateStockCode();
            //label27.Text = "0.00";
            //label29.Text = "0.00";

           

            #endregion

            Last_SetOFF_Details();

           // MessageBox.Show(Last_SET_OFF_BAL);
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

        private void Card_no_Leave_1(object sender, EventArgs e)
        {
            if (Card_no.Text == "")
            {
                Card_no.Text = "-";
            }
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
           // txtTotalRem.Text = Convert.ToString(Convert.ToDouble(txtTotalRem.Text) - Convert.ToDouble(listView4.SelectedItems[0].SubItems[4].Text));


            listView4.SelectedItems[0].Remove();

            TotalPayamount();

            ListView4Item_Count();
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                double cash = Convert.ToDouble(listView2.SelectedItems[0].SubItems[0].Text);
                double chek = Convert.ToDouble(listView2.SelectedItems[0].SubItems[1].Text);
                double card = Convert.ToDouble(listView2.SelectedItems[0].SubItems[2].Text);

                label27.Text = Convert.ToString(Convert.ToDouble(label27.Text) - (cash + chek + card));

                listView2.SelectedItems[0].Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_02", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
           


        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label27_TextChanged(object sender, EventArgs e)
        {
           // CalcTotRemaining();

            VentoeTotalPaymetFrmDB();

            ////vender button ennable or dissable-------------------------------
            //if (Convert.ToDouble(label27.Text) == 0)
            //{
            //    button3.Enabled = false;
            //}

            //else
            //{
            //    button3.Enabled = true;
            //}
        }

        private void label29_TextChanged(object sender, EventArgs e)
        {
           // CalcTotRemaining();

            VentoeTotalPaymetFrmDB();

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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_03", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void txtTotalRem_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtTotalRem.Text) <= 0)
            {
                txtTotalRem.Text = "0.00";
                button1.Enabled = false;
            }

            if (Convert.ToDouble(txtTotalRem.Text) > 0)
            {
                
                button1.Enabled = true;
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

        private void txtBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_BankID();
            if (txtBank.SelectedIndex == -1)
            {
                //MessageBox.Show("sfsfs");
               // txtBank.DroppedDown = false;
                AccNumbers.Enabled = false;

                return;
            }
        }

        private void txtBank_Click(object sender, EventArgs e)
        {
            Select_Bank();
        }

        private void txtBank_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txtBank.SelectedIndex == -1)
                {
                    
                    txtBank.DroppedDown = false;
                    AccNumbers.Enabled = false;
                    txtBank.Focus();

                    return;
                }
                if (txtBank.SelectedIndex != -1)
                {

                    txtBank.DroppedDown = true;
                    AccNumbers.Enabled = true;
                    AccNumbers.Focus();

                    
                }

                AccNumbers.Focus();
                Select_BankID();
                AccNumbers.DroppedDown = true;
            }
        }

        private void txtBankId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region select the bank Accounts Numbers---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT Account_Num FROM Bank_Account_Details WHERE BankID='" + txtBankId.Text + "' AND Account_Type='Current Acc' AND Ck_Book_Status='1'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                AccNumbers.Items.Clear();

                while (dr.Read() == true)
                {
                    AccNumbers.Items.Add(dr[0].ToString());
                }

                if (AccNumbers.Items.Count == 0)
                {
                    MessageBox.Show("This Bank Do Not Issue any cheque books. please select anothe Bank", "No Cheque Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBank.Focus();
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
                MessageBox.Show(ex.Message, "System Error_04", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void AccNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBranch.Focus();
            }
        }

        private void txtTotalRem_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Last_SetOFF_Details();
                if (Last_SET_OFF_BAL == "" || Last_SET_OFF_BAL == "")
                {

                    MessageBox.Show("You have not Enough money to pay to the Supplier,please Check Setoff Details...! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCash.Text = "0.0";
                    return;

                }


                if (Decimal.Parse(txtCheque.Text) > 0)
                {
                    if (txtBank.Text == "" || AccountNumber.Text == "")
                    {
                        MessageBox.Show("Please Enter Bank Name and Account Number");
                        return;
                        txtBank.Focus();
                    }
                }

                if (TxtCash.Text == "" && txtCheque.Text == "" && txtCard.Text == "" || TxtCash.Text == "0.0" && txtCheque.Text == "0.0" && txtCard.Text == "0.0")
                {
                    MessageBox.Show("Please Enert Amount", "Message");
                    TxtCash.Focus();
                    return;
                }

                #region bank and check details OK or Not

                if (Convert.ToDouble(txtCard.Text) != 0)
                {
                    if (Card_no.Text == "-")
                    {
                        MessageBox.Show("Please fill the Card Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Card_no.Focus();
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


                DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
                {

                    addcash_card_check_on_listview();
                    createAcceptedAmountTotal();

                    cleartextbox();
                    button3.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_05", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void label32_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void label32_Click(object sender, EventArgs e)
        {
            BankNameRegister bankname = new BankNameRegister();
            bankname.Visible = true;
        }

        private void label23_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void AccNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(AccNumbers.SelectedIndex==-1)
            {
                txtBank.Focus();
                AccNumbers.DroppedDown = false;
                return;

            }
        }

        private void txtBank_TextChanged(object sender, EventArgs e)
        {
            Select_BankID();
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
