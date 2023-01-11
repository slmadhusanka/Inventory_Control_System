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
    public partial class Return_Invoice : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string Invoiced_Item_Quntity = "";

        User_Cotrol UserCont=new User_Cotrol();

        public Return_Invoice()
        {
            InitializeComponent();
            Select_Bank();
            getCreatebANKcode();
        }


        public void selectUserSetting()
        {
            try
            {
                #region manual selling price

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                if (CusID.Text != "WK_Customer" && CusID.Text != "Quick_Customer")
                {
                    #region select customer details...........................................................

                    string customer_Details = @"SELECT     CusPriceLevel, CusCreditLimit
                                            FROM         CustomerDetails
                                            WHERE     (CusID = '" + CusID.Text + "')";
                    SqlCommand cmd_Cus = new SqlCommand(customer_Details, con1);
                    SqlDataReader dr_Reader = cmd_Cus.ExecuteReader(CommandBehavior.CloseConnection);

                    string CusPriceLevel = "";
                    string Cus_Credit_Limit = "";

                    if (dr_Reader.Read())
                    {
                        CusPriceLevel = dr_Reader[0].ToString();
                        Cus_Credit_Limit = dr_Reader[1].ToString();
                    }

                    //cloase data reader''''''''''''''''''
                    dr_Reader.Close();

                    // MessageBox.Show(CusPriceLevel);
                    #endregion

                    #region active/deactive customer price level................................

                    button1.Enabled = true;
                    if (CusPriceLevel == "Selling Price")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = false;
                        radioButton3.Enabled = false;
                        radioButton4.Enabled = false;

                        label12.Enabled = true;
                        label13.Enabled = false;
                        label14.Enabled = false;
                        label18.Enabled = false;

                        Dis01.Enabled = true;
                        Dis02.Enabled = false;
                        Dis03.Enabled = false;
                        Dis04.Enabled = false;
                    }

                    if (CusPriceLevel == "Discount 01")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = false;
                        radioButton4.Enabled = false;

                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = false;
                        label18.Enabled = false;

                        Dis01.Enabled = true;
                        Dis02.Enabled = true;
                        Dis03.Enabled = false;
                        Dis04.Enabled = false;
                    }

                    if (CusPriceLevel == "Discount 02")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = true;
                        radioButton4.Enabled = false;

                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = true;
                        label18.Enabled = false;

                        Dis01.Enabled = true;
                        Dis02.Enabled = true;
                        Dis03.Enabled = true;
                        Dis04.Enabled = false;
                    }

                    if (CusPriceLevel == "Discount 03")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = true;
                        radioButton4.Enabled = true;

                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = true;
                        label18.Enabled = true;

                        Dis01.Enabled = true;
                        Dis02.Enabled = true;
                        Dis03.Enabled = true;
                        Dis04.Enabled = true;
                    }


                    #endregion
                }





                //######################################################################

                SqlDataReader dr = UserCont.User_Setting();

                if (dr.Read())
                {





                    //MessageBox.Show(dr[36].ToString() );
                    //Manual selling price
                    if (dr[36].ToString() == "0")
                    {
                        chbManualPrice.Enabled = false;

                    }
                    if (dr[36].ToString() == "1")
                    {
                        chbManualPrice.Enabled = true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        public void RadioBtn()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            chbManualPrice.Checked = false;

        }

        

        public void clear_txt()
        {
            #region clear all......................

            listView2.Items.Clear();
            listV_Return_Item.Items.Clear();
            listV_Invoiced_Item.Items.Clear();

            lbl_Return_Amount.Text = "00.00";
            lbl_Invoiced_Amount.Text = "00.00";

            CusID.Text = "";
            CusName.Text = "";
            CusCreditLimit.Text = "00.00";
            CusBalCreditLimit.Text = "00.00";
            CusPreCreditLimit.Text = "00.00";
            CusPersonalAddress.Text = "";
            CusTelNUmber.Text = "";
            CusPersonalAddress.Text = "";

            Inved_Person.Text = "Invoiced By";
            InvDate.Text = "Invoiced date";

            PayCash.Text = "00.00";
            PayCheck.Text = "00.00";
            PayCrditCard.Text = "00.00";
            PAyCredits.Text = "00.00";

            CkCkBox.Checked = false;
            CkNumber.Text = "";
            groupBox8.Enabled = false;

            PayAmountInvoiced.Text = "00.00";
            PayPreInvoived.Text = "00.00";
            PayAmount.Text = "00.00";
            PayBalance.Text = "00.00";

            btnPnlItmFreQty.Text = "0";
            btnPnlItmQty.Text = "0";
            InvoiceID.Text = "";

            PnlAddress.Visible = false;




            #endregion
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
                label76.Text = "BNK1001";

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

                    label76.Text = "BNK" + no;

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




        public void Vender_Credit_Payment()
        {
            #region Vender_Credit_Payment-----------------------------------------------------------

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                if (CusID.Text != "WK_Customer" && CusID.Text != "Quick_Customer")
                {
                    #region select customer details...........................................................

                   // MessageBox.Show("Enter here");
                    //return;

                    string customer_Details = @"SELECT     CusPriceLevel, CusCreditLimit
                                            FROM         CustomerDetails
                                            WHERE     (CusID = '" + CusID.Text + "')";
                    SqlCommand cmd_Cus = new SqlCommand(customer_Details, con1);
                    SqlDataReader dr_Reader = cmd_Cus.ExecuteReader(CommandBehavior.CloseConnection);

                    string CusPriceLevel = "";
                    string Cus_Credit_Limit = "";

                    if (dr_Reader.Read())
                    {
                        CusPriceLevel = dr_Reader[0].ToString();
                        Cus_Credit_Limit = dr_Reader[1].ToString();
                    }

                    //cloase data reader''''''''''''''''''
                    dr_Reader.Close();

                    #endregion

                    #region active/deactive customer price level................................

                    if (CusPriceLevel == "Selling Price")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = false;
                        radioButton3.Enabled = false;
                        radioButton4.Enabled = false;

                        label12.Enabled = true;
                        label13.Enabled = false;
                        label14.Enabled = false;
                        label18.Enabled = false;

                        Dis01.Enabled = true;
                        Dis02.Enabled = false;
                        Dis03.Enabled = false;
                        Dis04.Enabled = false;
                    }

                    if (CusPriceLevel == "Discount 01")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = false;
                        radioButton4.Enabled = false;

                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = false;
                        label18.Enabled = false;

                        Dis01.Enabled = true;
                        Dis02.Enabled = true;
                        Dis03.Enabled = false;
                        Dis04.Enabled = false;
                    }

                    if (CusPriceLevel == "Discount 02")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = true;
                        radioButton4.Enabled = false;

                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = true;
                        label18.Enabled = false;

                        Dis01.Enabled = true;
                        Dis02.Enabled = true;
                        Dis03.Enabled = true;
                        Dis04.Enabled = false;
                    }

                    if (CusPriceLevel == "Discount 03")
                    {
                        radioButton1.Enabled = true;
                        radioButton2.Enabled = true;
                        radioButton3.Enabled = true;
                        radioButton4.Enabled = true;

                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = true;
                        label18.Enabled = true;

                        Dis01.Enabled = true;
                        Dis02.Enabled = true;
                        Dis03.Enabled = true;
                        Dis04.Enabled = true;
                    }


                    #endregion

                    CusCreditLimit.Text = Cus_Credit_Limit;

                    #region select customer credit payment-----------------------

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();

                    string CusCreditBalane = "SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = '" + CusID.Text + "') ORDER BY AutoNum DESC";
                    SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con2);
                    SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                    //dataGridView1.Rows.Clear();

                    if (dr2.Read() == true)
                    {
                        CusPreCreditLimit.Text = dr2[0].ToString();

                    }

                    //calculate the Current credit balence that Customer can get 
                    CusBalCreditLimit.Text = Convert.ToString(Convert.ToDouble(CusCreditLimit.Text) - Convert.ToDouble(CusPreCreditLimit.Text));


                    //close dr2-------------------------
                    dr2.Close();

                    #endregion

                } //end : if (CusID.Text != "WK_Customer")

                if (CusID.Text == "WK_Customer" && CusID.Text == "Quick_Customer")
                {
                    CusCreditLimit.Text = "0.00";
                    CusPreCreditLimit.Text = "0.00";
                    CusBalCreditLimit.Text = "0.00";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Customer selection methord. please contact your administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
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

                string CusCreditBalane = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + CusID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView1.Rows.Clear();

                if (dr2.Read() == true)
                {
                    LastBalance = Convert.ToDouble(dr2[0].ToString());
                    Deb_Bal = dr2[1].ToString();

                }
                //check the balesce >0 or <0-----------------------------------

                #region  Customer Previos Remainder is Possitive Value----------------------------

                //balance calc-----------------------------
                New_Bal = LastBalance + Convert.ToDouble(PayBalance.Text);

                SqlConnection con1x = new SqlConnection(IMS);
                con1x.Open();

                string Cus_DebitPaymet = @"INSERT INTO RegCusCredBalance( CusID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) 
                                                VALUES  ('" + CusID.Text + "','" + NewInvoiceID.Text + "','" + PAyCredits.Text + "','0','" + Deb_Bal + "','" + Convert.ToString(New_Bal) + "','" + DateTime.Now.ToString() + "')";

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
                MessageBox.Show(ex.Message, "System Error 1", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Customer_Final_Balance_Update()
        {
            try
            {
                #region Customer_Final_Balance_Update--------------------------

                double LastBalance = 0;
                double Last_Debit_Balance = 0;
                // double New_Bal = 0;

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + CusID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                if (dr7.Read())
                {
                    LastBalance = Convert.ToDouble(dr7[0].ToString());
                    Last_Debit_Balance = Convert.ToDouble(dr7[1].ToString());
                }

                #region New Vendor Previos Remainder is Possitive Value----------------------------

                //balance calc-----------------------------
                double Calc_Debit_Bal = 0;

                //if total balance is negative (y-(-x)=y+x).......................................
                if (Convert.ToDouble(txtTotal.Text) < 0)
                {
                    Calc_Debit_Bal = Last_Debit_Balance - Convert.ToDouble(txtTotal.Text);
                }
                ////if total balance is posi (y+x)=y+x)...............................................
                //if (Convert.ToDouble(txtTotal.Text) >= 0)
                //{
                //    Calc_Debit_Bal = Last_Debit_Balance + Convert.ToDouble(txtTotal.Text);
                //}

                //if there is some remaining credits
                //if (Calc_Bal >= 0)
                //{
                //    New_Bal = Calc_Bal;
                //}
                //if cedite over and some balace on our hand....
                //if (Calc_Bal < 0)
                //{
                //    New_Bal = 0;
                //}

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Vend_DebitPaymet = @"INSERT INTO RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount, Debit_Balance, Balance, Date) 
                                        VALUES  ('" + CusID.Text + "','" + Rtn_ID.Text + "','0','0','" + Calc_Debit_Bal + "','" + LastBalance + "','" + DateTime.Now.ToString() + "')";

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
                MessageBox.Show(ex.Message, "System Error 2", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void getCreateInvoiceCode()
        {
            #region New Invoice Number...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT InvoiceNo FROM SoldInvoiceDetails WHERE InvoiceNo LIKE 'INV%' ";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    NewInvoiceID.Text = "INV1001";
                    // PassInvoiceNumber.Text = "INV1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 InvoiceNo FROM SoldInvoiceDetails WHERE InvoiceNo LIKE 'INV%' order by InvoiceNo DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        NewInvoiceID.Text = "INV" + no;
                        // PassInvoiceNumber.Text = "INV" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error 3", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        public void Return_Auto_ID_Create()
        {
            #region New Return Code...........................................
            try
            {
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Return_Num from Return_Note";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                Rtn_ID.Text = "IRT1001";

                cmd.Dispose();
                dr.Close();

            }

            else
            {

                cmd.Dispose();
                dr.Close();

                string sql1 = " SELECT TOP 1 Return_Num FROM Return_Note order by Auto_Num DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string OrderNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                    Rtn_ID.Text = "IRT" + no;

                }
                cmd1.Dispose();
                dr7.Close();

            }
            Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error 4", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        public void Return_Item_txt_clear()
        {
            #region clear txt----------------
            txtID.Text = "";
            txtBatch.Text = "";
            txtName.Text = "";
            txtSerial.Text = "";
            txtWarr.Text = "";
            txtselling.Text = "";

            txtQnty.Text = "";

            txtFreeQnt.Text = "";
            txtAmount.Text = "";
            txtreturnAmount.Text = "";
            #endregion

        }

        public void Insert_To_return_Listview()
        {
            try
            {
                #region add to the return list view--------

                ListViewItem li = new ListViewItem(txtID.Text);

                //li.SubItems.Add(txtID.Text);
                li.SubItems.Add(txtBatch.Text);
                li.SubItems.Add(txtName.Text);
                li.SubItems.Add(txtSerial.Text);

                li.SubItems.Add(txtSerial.Text);

                li.SubItems.Add(txtWarr.Text);
                li.SubItems.Add(txtselling.Text);
                li.SubItems.Add(txtQnty.Text);
                li.SubItems.Add("0");

                string tot = Convert.ToString(Convert.ToDouble(txtselling.Text) * Convert.ToDouble(txtQnty.Text));

                li.SubItems.Add(tot);

                li.SubItems.Add(txtreturnAmount.Text);

                string netAmount = Convert.ToString(Convert.ToDouble(tot) - Convert.ToDouble(txtreturnAmount.Text));
                li.SubItems.Add(netAmount);
                li.SubItems.Add(Status.Text);

                listV_Return_Item.Items.Add(li);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error 5", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }


       public void check_Item_Already_return_Or_Not_and_Insert()
        {
            #region check_Item_Already_return _Or_Not..................................................

            try
            {
                
                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string Select_Item = @"SELECT     Invoice_Num, Batch_Num, Item_ID, Serial_Num, Return_Qnty,Return_Date,Return_Num
                                           FROM         Return_Note
                                            WHERE     Invoice_Num = '" + InvoiceID.Text + "' AND Item_ID='" + txtID.Text + "' AND Serial_Num='" + txtSerial.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(Select_Item, con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

               if (txtBatch.Text == "No")
                {
                    if (dr1.Read() == true)
                    {
                        string Date_Is = dr1[5].ToString();
                        string Return_ID = dr1[6].ToString();

                        MessageBox.Show("This Item is already return to the company in '" + Date_Is + "'. please try another one. The Return ID# is '" + Return_ID + "'","Cannot return",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }//end if..
                }//end if.

               else if (txtBatch.Text != "No")
                {
                    if (dr1.Read() == true)
                    {
                        string Date_Is = dr1[5].ToString();
                        string Return_ID = dr1[6].ToString();

                        double enterd_amount = Convert.ToDouble(txtQnty.Text);
                        double Already_return_Count = 0;
                        dr1.Close();

                        SqlConnection con2 = new SqlConnection(IMS);
                        con2.Open();

                        string Select_count = @"SELECT sum(Return_Qnty) AS Total
                                                FROM         Return_Note
                                                WHERE     (Invoice_Num = '"+InvoiceID.Text+"') GROUP BY Invoice_Num";

                        SqlCommand cmd2 = new SqlCommand(Select_count, con2);
                        SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dr2.Read() == true)
                        {
                            Already_return_Count = Convert.ToDouble(dr2[0].ToString());

                            if ((Already_return_Count + Convert.ToDouble(txtQnty.Text)) > Convert.ToDouble(Invoiced_Item_Quntity))
                            {
                                MessageBox.Show("This Item has Already return '" + Already_return_Count + "' item(s). Last return date is '" + Date_Is + "' and The Return ID# is '" + Return_ID + "'. Please try another amount.","Cannot return this amount",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return;
                            }//end if...
                        }//end if..

                    }//end if.
                }//end if else

           // else{
                    Insert_To_return_Listview();
              //  }
            }//end try
            catch (Exception ex)
            {
                MessageBox.Show("This error generate fron the check_Item_Already_return_Or_Not. please contact your administrator", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }


        

        public void calc_return_tot()
        {
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listV_Return_Item.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[11].Text), 2);
            }
            lbl_Return_Amount.Text =lbl_return_Amount_02.Text= Convert.ToString(gtotal);
        }


        public void calc_Invoice_tot()
        {
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listV_Invoiced_Item.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[9].Text), 2);
            }
            lbl_Invoiced_Amount.Text = Convert.ToString(gtotal);
        }

        public void Active_Deactive_Payment_Panel()
        {
            #region Active_Deactive_Payment_Panel()

            if (Convert.ToDouble(txtSubTotal.Text) <= 0 || txtSubTotal.Text == "" || txtSubTotal.Text == "0")
            {
                button5.Enabled = false;
                txtTaxPer.Enabled = false;
                button14.Enabled = false;
            }

            if (Convert.ToDouble(txtSubTotal.Text) > 0 || txtSubTotal.Text != "" || txtSubTotal.Text != "")
            {
                button5.Enabled = true;
                txtTaxPer.Enabled = true;
                button14.Enabled = true;
            }
            #endregion
        }

        public void InsertInToListVeiw()
        {
            #region Priveous Code.............................................................................
            //            try
//            {
//                #region check the data is in list========================================================

//                //do not allow to show witout enter any thing to the textbox..............................
//                if (ItmBarCode.Text == "")
//                {
//                    MessageBox.Show("Please type an Item code or name in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    ItmBarCode.Text = "";
//                    ItmBarCode.Focus();
//                    return;
//                }

//                btnPnlItmFreQty.Text = "0";
//                btnPnlItmFreQty.Text = "0";

//                for (int i = 0; i <= listView2.Items.Count - 1; i++)
//                {
//                    if (listView2.Items[i].SubItems[3].Text == ItmBarCode.Text)
//                    {
//                        MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        ItmBarCode.Text = "";
//                        ItmBarCode.Focus();
//                        return;
//                    }
//                }

//                #endregion


//                #region check the data is in the Database. but its not available to Sell (This Only check Uniqe Items(with serials))========================================================

//                SqlConnection con1 = new SqlConnection(IMS);
//                con1.Open();
//                string selectItemOnly = "SELECT BarcodeNumber,ItmStatus FROM CurrentStockItems WHERE (SystemID LIKE '" + ItmBarCode.Text + "%' OR BarcodeNumber LIKE '" + ItmBarCode.Text + "%') AND ItmStatus ='Stock'";
//                SqlCommand cmd1 = new SqlCommand(selectItemOnly, con1);
//                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


//                string dr_Read_Value = "";


//                if (dr.Read() == true)
//                {
//                    #region if Item Has Barcode and Serial Numbers---------------------------------------------(From Uniq ItemTable)

//                    //clear all items in the listview
//                    ListMultiItems.Items.Clear();

//                    PnlMultiItems.Visible = true;


//                    btnPnlItmFreQty.Enabled = true;
//                    btnPnlItmQty.Enabled = true;

//                    btnPnlItmFreQty.Text = "0";
//                    btnPnlItmQty.Text = "1";

//                    SqlConnection Conb = new SqlConnection(IMS);
//                    Conb.Open();

//                    string SelectUnicItems = @"SELECT     ItemID, ItemName, BarcodeNumber, SystemID, WarrentyPeriod, ItmDisc01, ItmDisc02, ItmDisc03, ItmSellPrice, OrderCost, ItemAutoID
//                                                        FROM         CurrentStockItems
//                                                        WHERE     (ItmStatus = 'Stock') AND (SystemID LIKE '" + ItmBarCode.Text + "%' OR BarcodeNumber LIKE '" + ItmBarCode.Text + "%' OR ItemName LIKE '%" + ItmBarCode.Text + "%')";

//                    SqlCommand cmdB = new SqlCommand(SelectUnicItems, Conb);
//                    SqlDataReader drB = cmdB.ExecuteReader(CommandBehavior.CloseConnection);



//                    while (drB.Read() == true)
//                    {

//                        ListViewItem li;

//                        li = new ListViewItem(drB[0].ToString());
//                        //batch number
//                        li.SubItems.Add("No");
//                        li.SubItems.Add(drB[1].ToString());
//                        li.SubItems.Add(drB[2].ToString());
//                        li.SubItems.Add(drB[3].ToString());
//                        li.SubItems.Add(drB[4].ToString());

//                        //selling price
//                        li.SubItems.Add(drB[8].ToString());
//                        // qty
//                        li.SubItems.Add("1");
//                        // free
//                        li.SubItems.Add("0");
//                        // totoa; Item price
//                        li.SubItems.Add(drB[8].ToString());

//                        li.SubItems.Add(drB[5].ToString());
//                        li.SubItems.Add(drB[6].ToString());
//                        li.SubItems.Add(drB[7].ToString());
//                        li.SubItems.Add("0");
//                        li.SubItems.Add(drB[8].ToString());
//                        li.SubItems.Add(drB[9].ToString());
//                        li.SubItems.Add(drB[10].ToString());
//                        li.SubItems.Add("1");

//                        ListMultiItems.Items.Add(li);

//                    }



//                    #endregion
//                }


//                else //if this item is not a unique item-------------------------------------------------
//                {
//                    dr_Read_Value = "Not_Rd";

//                }


//                #region if Item hasnt a serial number========================================================


//                SqlConnection con1x = new SqlConnection(IMS);
//                con1x.Open();
//                string selectItemOnlyx = @"SELECT     NewItemDetails.ItmName,  GRNWholesaleItems.BarCodeID
//                                            FROM         NewItemDetails INNER JOIN
//                                            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID
//	                                        WHERE    (GRNWholesaleItems.BarCodeID LIKE '" + ItmBarCode.Text + "%' OR NewItemDetails.ItmName LIKE '%" + ItmBarCode.Text + "%') AND (GRNWholesaleItems.AvailbleItemCount>0)";

//                SqlCommand cmd1x = new SqlCommand(selectItemOnlyx, con1x);
//                SqlDataReader drx = cmd1x.ExecuteReader(CommandBehavior.CloseConnection);

//                string drx_Read_Value = "";
//                if (drx.Read() == true)
//                {
//                    #region Select the Items in wholesales Items=========================================

//                    SqlConnection ConC = new SqlConnection(IMS);
//                    ConC.Open();

//                    string WholeSalefItems = @"SELECT     NewItemDetails.ItmID, GRNWholesaleItems.BatchNumber, NewItemDetails.ItmName, GRNWholesaleItems.ItemWarrenty, GRNWholesaleItems.ItmDisc01, 
//                                            GRNWholesaleItems.ItmDisc02, GRNWholesaleItems.ItmDisc03, GRNWholesaleItems.SellingPrice, GRNWholesaleItems.PerchPrice, GRNWholesaleItems.GrnAutiID, 
//                                            GRNWholesaleItems.AvailbleItemCount,GRNWholesaleItems.BarCodeID
//                                            FROM         NewItemDetails INNER JOIN
//                                            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID
//                                            WHERE    (GRNWholesaleItems.BarCodeID LIKE '" + ItmBarCode.Text + "%' OR NewItemDetails.ItmName LIKE '%" + ItmBarCode.Text + "%') AND (GRNWholesaleItems.AvailbleItemCount>0)";

//                    SqlCommand cmdC = new SqlCommand(WholeSalefItems, ConC);
//                    SqlDataReader drC = cmdC.ExecuteReader(CommandBehavior.CloseConnection);

//                    // clear all items in the listview
//                    // ListMultiItems.Items.Clear();

//                    PnlMultiItems.Visible = true;
//                    // need to add item quntity here.
//                    btnPnlItmFreQty.Enabled = true;
//                    btnPnlItmQty.Enabled = true;


//                    while (drC.Read() == true)
//                    {

//                        ListViewItem li;

//                        li = new ListViewItem(drC[0].ToString());
//                        //  batch number
//                        li.SubItems.Add(drC[1].ToString());
//                        //  ItemName
//                        li.SubItems.Add(drC[2].ToString());
//                        //  SerialNumber
//                        li.SubItems.Add("No_ID");
//                        //   BArCode
//                        li.SubItems.Add(drC[11].ToString());
//                        //  Warrenty
//                        li.SubItems.Add(drC[3].ToString());
//                        //  SellingPrice
//                        li.SubItems.Add(drC[7].ToString());
//                        // Quantitiy
//                        li.SubItems.Add(btnPnlItmQty.Text);
//                        // freeQty
//                        li.SubItems.Add(btnPnlItmFreQty.Text);

//                        // Totoal Amount
//                        //
//                        //
//                        li.SubItems.Add("0");

//                        //  dis1,2,3
//                        li.SubItems.Add(drC[4].ToString());
//                        li.SubItems.Add(drC[5].ToString());
//                        li.SubItems.Add(drC[6].ToString());

//                        //  PriceLevel..................
//                        li.SubItems.Add("0");

//                        // selling price Orijinal...............
//                        li.SubItems.Add(drC[7].ToString());


//                        //  perchese price
//                        li.SubItems.Add(drC[8].ToString());
//                        //  / selling original...
//                        li.SubItems.Add(drC[9].ToString());
//                        // available count............
//                        li.SubItems.Add(drC[10].ToString());


//                        ListMultiItems.Items.Add(li);


//                    }
//                    #endregion

//                }

//                if (drx.Read() == false) //if this item is not a unique item-------------------------------------------------
//                {
//                    drx_Read_Value = "Not_Rd";
//                }

//                if (drx_Read_Value == "Not_Rd" && dr_Read_Value == "Not_Rd")
//                {
//                    if (ListMultiItems.Items.Count == 0)
//                    {
//                        PnlMultiItems.Visible = false;

//                        MessageBox.Show("This Item is not in the Stock or code is not available. Please contact your store keeper", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        ItmBarCode.Text = "";
//                        ItmBarCode.Focus();
//                        return;
//                    }

//                }

//                if (con1x.State == ConnectionState.Open)
//                {
//                    con1x.Close();
//                    drx.Close();
//                }

//                #endregion


//                if (con1.State == ConnectionState.Open)
//                {
//                    con1.Close();
//                    dr.Close();
//                }

//                #endregion

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "System Error 6", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //            }


            #endregion
           
            
            //==============================================================================================================



            try
            {

                #region check the data is in list========================================================

                //do not allow to show witout enter any thing to the textbox..............................
                if (ItmBarCode.Text == "")
                {
                    MessageBox.Show("Please type an Item code or name in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ItmBarCode.Text = "";
                    ItmBarCode.Focus();
                    return;
                }

                btnPnlItmFreQty.Text = "0";
                btnPnlItmFreQty.Text = "0";

                for (int i = 0; i <= listView2.Items.Count - 1; i++)
                {
                    if (listView2.Items[i].SubItems[3].Text == ItmBarCode.Text)
                    {
                        MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ItmBarCode.Text = "";
                        ItmBarCode.Focus();
                        return;
                    }
                }

                #endregion


                #region check the data is in the Database. but its not available to Sell (This Only check Uniqe Items(with serials))========================================================

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectItemOnly = "SELECT BarcodeNumber,ItmStatus FROM CurrentStockItems WHERE (SystemID like'" + ItmBarCode.Text + "%' OR BarcodeNumber LIKE'" + ItmBarCode.Text + "%' OR ItemName LIKE '%" + ItmBarCode.Text + "%') AND ItmStatus ='Stock'";
                SqlCommand cmd1 = new SqlCommand(selectItemOnly, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


                string dr_Read_Value = "";


                if (dr.Read() == true)
                {
                    #region if Item Has Barcode and Serial Numbers---------------------------------------------(From Uniq ItemTable)

                    //clear all items in the listview
                    ListMultiItems.Items.Clear();

                    PnlMultiItems.Visible = true;


                    btnPnlItmFreQty.Enabled = true;
                    btnPnlItmQty.Enabled = true;

                    btnPnlItmFreQty.Text = "0";
                    btnPnlItmQty.Text = "1";

                    SqlConnection Conb = new SqlConnection(IMS);
                    Conb.Open();

                    string SelectUnicItems = @"SELECT     ItemID, ItemName, BarcodeNumber, SystemID, WarrentyPeriod, ItmDisc01, ItmDisc02, ItmDisc03, ItmSellPrice, OrderCost, ItemAutoID
                                                        FROM         CurrentStockItems
                                                        WHERE     (ItmStatus = 'Stock') AND (SystemID LIKE '" + ItmBarCode.Text + "%' OR BarcodeNumber LIKE '" + ItmBarCode.Text + "%' OR ItemName LIKE '%" + ItmBarCode.Text + "%')";

                    SqlCommand cmdB = new SqlCommand(SelectUnicItems, Conb);
                    SqlDataReader drB = cmdB.ExecuteReader(CommandBehavior.CloseConnection);



                    while (drB.Read() == true)
                    {

                        ListViewItem li;

                        li = new ListViewItem(drB[0].ToString());
                        //batch number
                        li.SubItems.Add("No");
                        li.SubItems.Add(drB[1].ToString());
                        li.SubItems.Add(drB[2].ToString());
                        li.SubItems.Add(drB[3].ToString());
                        li.SubItems.Add(drB[4].ToString());

                        //selling price
                        li.SubItems.Add(drB[8].ToString());
                        // qty
                        li.SubItems.Add("1");
                        // free
                        li.SubItems.Add("0");
                        // totoa; Item price
                        li.SubItems.Add(drB[8].ToString());

                        li.SubItems.Add(drB[5].ToString());
                        li.SubItems.Add(drB[6].ToString());
                        li.SubItems.Add(drB[7].ToString());
                        li.SubItems.Add("0");
                        li.SubItems.Add(drB[8].ToString());
                        li.SubItems.Add(drB[9].ToString());
                        li.SubItems.Add(drB[10].ToString());
                        li.SubItems.Add("1");

                        ListMultiItems.Items.Add(li);

                    }



                    #endregion
                }


                else //if this item is not a unique item-------------------------------------------------
                {
                    dr_Read_Value = "Not_Rd";

                }


                #region if Item hasnt a serial number========================================================


                SqlConnection con1x = new SqlConnection(IMS);
                con1x.Open();
                string selectItemOnlyx = @"SELECT     NewItemDetails.ItmName,  GRNWholesaleItems.BarCodeID
                                            FROM         NewItemDetails INNER JOIN
                                            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID
	                                        WHERE    (GRNWholesaleItems.BarCodeID LIKE '" + ItmBarCode.Text + "%' OR NewItemDetails.ItmName LIKE '%" + ItmBarCode.Text + "%') AND (GRNWholesaleItems.AvailbleItemCount>0)";

                SqlCommand cmd1x = new SqlCommand(selectItemOnlyx, con1x);
                SqlDataReader drx = cmd1x.ExecuteReader(CommandBehavior.CloseConnection);

                string drx_Read_Value = "";
                if (drx.Read() == true)
                {
                    #region Select the Items in wholesales Items=========================================

                    SqlConnection ConC = new SqlConnection(IMS);
                    ConC.Open();

                    string WholeSalefItems = @"SELECT     NewItemDetails.ItmID, GRNWholesaleItems.BatchNumber, NewItemDetails.ItmName, GRNWholesaleItems.ItemWarrenty, GRNWholesaleItems.ItmDisc01, 
                                            GRNWholesaleItems.ItmDisc02, GRNWholesaleItems.ItmDisc03, GRNWholesaleItems.SellingPrice, GRNWholesaleItems.PerchPrice, GRNWholesaleItems.GrnAutiID, 
                                            GRNWholesaleItems.AvailbleItemCount,GRNWholesaleItems.BarCodeID
                                            FROM         NewItemDetails INNER JOIN
                                            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID
                                                WHERE    (GRNWholesaleItems.BarCodeID LIKE '" + ItmBarCode.Text + "%' OR NewItemDetails.ItmName LIKE '%" + ItmBarCode.Text + "%') AND (GRNWholesaleItems.AvailbleItemCount>0)";

                    SqlCommand cmdC = new SqlCommand(WholeSalefItems, ConC);
                    SqlDataReader drC = cmdC.ExecuteReader(CommandBehavior.CloseConnection);

                    // clear all items in the listview
                    // ListMultiItems.Items.Clear();

                    PnlMultiItems.Visible = true;
                    // need to add item quntity here.
                    btnPnlItmFreQty.Enabled = true;
                    btnPnlItmQty.Enabled = true;


                    while (drC.Read() == true)
                    {

                        ListViewItem li;

                        li = new ListViewItem(drC[0].ToString());
                        //  batch number
                        li.SubItems.Add(drC[1].ToString());
                        //  ItemName
                        li.SubItems.Add(drC[2].ToString());
                        //  SerialNumber
                        li.SubItems.Add("No_ID");
                        //   BArCode
                        li.SubItems.Add(drC[11].ToString());
                        //  Warrenty
                        li.SubItems.Add(drC[3].ToString());
                        //  SellingPrice
                        li.SubItems.Add(drC[7].ToString());
                        // Quantitiy
                        li.SubItems.Add(btnPnlItmQty.Text);
                        // freeQty
                        li.SubItems.Add(btnPnlItmFreQty.Text);

                        // Totoal Amount
                        //
                        //
                        li.SubItems.Add("0");

                        //  dis1,2,3
                        li.SubItems.Add(drC[4].ToString());
                        li.SubItems.Add(drC[5].ToString());
                        li.SubItems.Add(drC[6].ToString());

                        //  PriceLevel..................
                        li.SubItems.Add("0");

                        // selling price Orijinal...............
                        li.SubItems.Add(drC[7].ToString());


                        //  perchese price
                        li.SubItems.Add(drC[8].ToString());
                        //  / selling original...
                        li.SubItems.Add(drC[9].ToString());
                        // available count............
                        li.SubItems.Add(drC[10].ToString());


                        ListMultiItems.Items.Add(li);


                    }
                    #endregion

                }

                if (drx.Read() == false) //if this item is not a unique item-------------------------------------------------
                {
                    drx_Read_Value = "Not_Rd";
                }

                if (drx_Read_Value == "Not_Rd" && dr_Read_Value == "Not_Rd")
                {
                    if (ListMultiItems.Items.Count == 0)
                    {
                        PnlMultiItems.Visible = false;

                        MessageBox.Show("This Item is not in the Stock or code is not available. Please contact your store keeper", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        ItmBarCode.Text = "";
                        ItmBarCode.Focus();
                        return;
                    }

                }

                if (con1x.State == ConnectionState.Open)
                {
                    con1x.Close();
                    drx.Close();
                }

                #endregion


                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                    dr.Close();
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }





        }

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
                MessageBox.Show(ex.Message, "System Error 7", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void PayMethod()
        {
            double Cash = Convert.ToDouble(PayCash.Text);
            double Check = Convert.ToDouble(PayCheck.Text);
            double CreCard = Convert.ToDouble(PayCrditCard.Text);
            double Debit = Convert.ToDouble(PayDebitCard.Text);
           // double Credits = Convert.ToDouble(PAyCredits.Text);

            //double GndTotal = Cash + Check + CreCard + Debit + Credits;
            double GndTotal = Cash + Check + CreCard + Debit ;

            PayAmount.Text = Convert.ToString(GndTotal);

        }

        public void AddMultiItemToInvoiceList()
        {
            try
            {
                if (ListMultiItems.Items.Count == 0)
                {
                    return;
                }


                #region Check items in the list view=====================================

                for (int i = 0; i <= listView2.Items.Count - 1; i++)
                {
                    //if multilist view selected item is unique item
                    if (ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[0].Text != ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[3].Text)
                    {
                        //if invoicelist item is unique item
                        if (listView2.Items[i].SubItems[0].Text != listView2.Items[i].SubItems[3].Text)
                        {
                            //if equal items
                            if (listView2.Items[i].SubItems[16].Text == ListMultiItems.SelectedItems[0].SubItems[16].Text)
                            {
                                MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                }
                #endregion


                #region Item stock count and request count ck=======================================================

                double totalItemcount = Convert.ToDouble(btnPnlItmQty.Text) + Convert.ToDouble(btnPnlItmFreQty.Text);
                double AvilableStock = Convert.ToDouble(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[17].Text);

                if ((AvilableStock - totalItemcount) < 0)
                {
                    MessageBox.Show("Not Enougth Stock to issue. Please check the Item count..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnPnlItmQty.Focus();
                    return;
                }

                #endregion




                #region add to invoce listview from ultilist view==============================================

                if (ListMultiItems.Items.Count != 0)
                {

                    ListViewItem li;


                    li = new ListViewItem(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[0].Text);
                    //batch number
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[1].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[2].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[3].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[4].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[5].Text);

                    //selling price
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[6].Text);


                    //================================================================================================================
                    //if invoicelist item has a serial number______________________________________________________________________
                    if (ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[3].Text != "No_ID")
                    {
                        //qty
                        li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[7].Text);
                        //free
                        li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[8].Text);


                        //totoa; Item price
                        li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[9].Text);
                    }

                    //if invoicelist item is Wholesale Item________________________________________________________________________
                    if (ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[3].Text == "No_ID")
                    {
                        //qty
                        li.SubItems.Add(btnPnlItmQty.Text);
                        //free
                        li.SubItems.Add(btnPnlItmFreQty.Text);

                        //calculate the total amount qty and selling price-------------------------------------------------
                        double x = Convert.ToDouble(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[6].Text) * Convert.ToDouble(btnPnlItmQty.Text);

                        //totoa; Item price
                        li.SubItems.Add(Convert.ToString(x));

                        #region Ck the adding items is 0 or not =======================================================


                        if (Convert.ToDouble(btnPnlItmQty.Text) <= 0)
                        {
                            MessageBox.Show("Item count is Zero. Please enter valied count..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnPnlItmQty.Focus();
                            return;
                        }

                        GetTotalAmount();

                        #endregion
                    }
                    //====================================================================================================================


                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[10].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[11].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[12].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[13].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[14].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[15].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[16].Text);
                    li.SubItems.Add(ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[17].Text);


                    listView2.Items.Add(li);

                    PnlMultiItems.Visible = false;

                    btnPnlItmFreQty.Text = "0";
                    btnPnlItmQty.Text = "0";
                    LvIndex.Text = " selected Index Number";

                    ItmBarCode.Text = "";
                    ItmBarCode.Focus();

                }

                #endregion
            }

            catch (Exception ex)
            {

            }
        }

        public void GetTotalAmount()
        {
            try
            {

                decimal gtotal = 0;
                foreach (ListViewItem lstItem in listView2.Items)
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[9].Text), 2);
                }
                LblBillTotalAmount.Text =  Convert.ToString(gtotal);

                txtSubTotal.Text = Convert.ToString(Convert.ToDouble(LblBillTotalAmount.Text) - Convert.ToDouble(lbl_return_Amount_02.Text));


                //MessageBox.Show(txtSubTotal.Text);
                //MessageBox.Show(txtSubTotal.Text);

                //------------------------------------
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "0.00";
                    txtTotal.Text = "0.00";
                    return;
                }

                txtTaxAmt.Text = Convert.ToString((Convert.ToDouble(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                txtTotal.Text = PayAmountInvoiced.Text = Convert.ToString(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtTaxAmt.Text));

                if (listV_Return_Item.Items.Count == 0)
                {
                    button5.Enabled = false;
                    txtTaxPer.Enabled = false;
                    button14.Enabled = false;
                }

                if (listV_Return_Item.Items.Count != 0)
                {
                    button5.Enabled = true;
                    txtTaxPer.Enabled = true;
                    button14.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error in get total amount. please contact you System Administrator", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void CusTelNUmber_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Return_Invoice_Load(object sender, EventArgs e)
        {
            Return_Auto_ID_Create();

            getCreateInvoiceCode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                txtItem_Search.Text = "";

                Pnl_Invoice_Search.Visible = true;
                txtItem_Search.Focus();

                #region select the Invoice when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string InvoiceItem_All = @"SELECT        SoldItemDetails.InvoiceID, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, SoldItemDetails.SystemID, SoldItemDetails.ItmQuantity, 
                         SoldItemDetails.FreeQuantity, SoldItemDetails.BatchID, SoldInvoiceDetails.CusFirstName, SoldItemDetails.SoldPrice
                         FROM            SoldInvoiceDetails INNER JOIN
                         SoldItemDetails ON SoldInvoiceDetails.InvoiceNo = SoldItemDetails.InvoiceID ORDER BY SoldItemDetails.InvoiceID ASC";
                SqlCommand cmd1 = new SqlCommand(InvoiceItem_All, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

              

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error 8", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Pnl_Invoice_Search.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = true;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
        }

        private void txtItem_Search_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                Pnl_Invoice_Search.Visible = true;

                #region select the Invoice when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string InvoiceItem_All = @"SELECT        SoldItemDetails.InvoiceID, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, SoldItemDetails.SystemID, SoldItemDetails.ItmQuantity, 
                         SoldItemDetails.FreeQuantity, SoldItemDetails.BatchID, SoldInvoiceDetails.CusFirstName, SoldItemDetails.SoldPrice
                         FROM            SoldInvoiceDetails INNER JOIN
                         SoldItemDetails ON SoldInvoiceDetails.InvoiceNo = SoldItemDetails.InvoiceID WHERE (SoldItemDetails.InvoiceID LIKE '%" + txtItem_Search.Text + "%' OR SoldItemDetails.ItemName LIKE '%" + txtItem_Search.Text + "%' OR SoldItemDetails.BarcodeID LIKE '%" + txtItem_Search.Text + "%' OR SoldItemDetails.SystemID LIKE '%" + txtItem_Search.Text + "%' OR SoldItemDetails.BatchID LIKE '%" + txtItem_Search.Text + "%' OR SoldInvoiceDetails.CusFirstName LIKE '%" + txtItem_Search.Text + "%') ORDER BY SoldItemDetails.InvoiceID ASC";
                SqlCommand cmd1 = new SqlCommand(InvoiceItem_All, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error 9", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void InvoiceID_TextChanged(object sender, EventArgs e)
        {
            //try
            //{

                Pnl_Invoice_Search.Visible = true;

                #region select the Invoice detsils...................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string InvoiceItem_All = @"SELECT        InvoicePaymentDetails.InvoiceID AS Expr1, SoldItemDetails.InvoiceID AS Expr3, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                         SoldItemDetails.SystemID, SoldItemDetails.ItemWarrenty, SoldItemDetails.SoldPrice, InvoiceCheckDetails.InvoiceID AS Expr2, InvoiceCheckDetails.CkNumber, 
                         InvoiceCheckDetails.Bank, InvoiceCheckDetails.Amount, InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.SubTotal, InvoicePaymentDetails.VATpresentage, 
                         InvoicePaymentDetails.GrandTotal, InvoicePaymentDetails.PayCash, InvoicePaymentDetails.PayCheck, InvoicePaymentDetails.PayCrditCard, 
                         InvoicePaymentDetails.PayDebitCard, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance, InvoicePaymentDetails.InvoiceDate, 
                         InvoicePaymentDetails.InvoiceDiscount, SoldInvoiceDetails.InvoiceNo, SoldInvoiceDetails.InvoiceStatus, SoldInvoiceDetails.CusStatus, 
                         SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CusTelNUmber, SoldInvoiceDetails.CreatedBy, 
                         SoldInvoiceDetails.InvoiceRemark, SoldItemDetails.ItmQuantity, SoldItemDetails.FreeQuantity, SoldItemDetails.BatchID
                        FROM            InvoiceCheckDetails FULL OUTER JOIN
                         InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                         SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                         SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                      WHERE (InvoicePaymentDetails.InvoiceID = '"+InvoiceID.Text+"')";

                SqlCommand cmd1 = new SqlCommand(InvoiceItem_All, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listV_Invoiced_Item.Items.Clear();
                listV_Return_Item.Items.Clear();
                Return_Item_txt_clear();

                listView2.Items.Clear();

                string tot = "";

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[2].ToString());
                    //Batch
                    li.SubItems.Add(dr[34].ToString());
                    //Name
                    li.SubItems.Add(dr[3].ToString());
                    //Serial
                    li.SubItems.Add(dr[4].ToString());
                    //barc
                    li.SubItems.Add(dr[5].ToString());
                    //Warrenty
                    li.SubItems.Add(dr[6].ToString());
                    //selling pr
                    li.SubItems.Add(dr[7].ToString());
                    //qnty
                    li.SubItems.Add(dr[32].ToString());
                    //free
                    li.SubItems.Add(dr[33].ToString());

                   
                    //total amount.....
                    if (dr[32].ToString() != "" && dr[7].ToString() != "")
                    {
                        tot = Convert.ToString(Convert.ToDouble(dr[32].ToString()) * Convert.ToDouble(dr[7].ToString()));
                    }
                    else
                    {
                        tot = "0";
                    }
                    li.SubItems.Add(tot);
                    //------------------------------------------------------------------
                    listV_Invoiced_Item.Items.Add(li);

                    //Add to lables----------------------------------


                    //invoiced amount

                    calc_Invoice_tot();
                   // lbl_Invoiced_Amount.Text = dr[13].ToString() ;

                    //Customer Details ------------------------------
                    CusID.Text = dr[26].ToString();
                    //cus name
                    CusName.Text=CusFirstName.Text= dr[27].ToString();
                    //cusaddress
                    CusPersonalAddress.Text = dr[28].ToString();
                    //cus tel--
                    CusTelNUmber.Text = dr[29].ToString();

                    //------------------------------------------------
                    //invoiced by----------
                    Inved_Person.Text = dr[30].ToString();
                    //invoiced date-----------
                    InvDate.Text = dr[23].ToString();

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error 10", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView2.SelectedRows[0];

            InvoiceID.Text = dr.Cells[0].Value.ToString();

          //  PassInvoiceNumber.Text = dr.Cells[0].Value.ToString();

            //clear 

           


            listV_Return_Item.Items.Clear();
            listView2.Items.Clear();
            calc_return_tot();
            GetTotalAmount();


            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            chbManualPrice.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            Dis01.Text = "00.00";
            Dis02.Text = "00.00";
            Dis03.Text = "00.00";
            Dis04.Text = "00.00";
            button1.Enabled = false;
            Pnl_Invoice_Search.Visible = false;

           



        }

        private void listV_Invoiced_Item_DoubleClick(object sender, EventArgs e)
        {
           // ListViewItem li = new ListViewItem(listV_Invoiced_Item.SelectedItems[0].SubItems[0].Text);
            try
            {

                txtID.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[0].Text;
                txtBatch.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[1].Text;
                txtName.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[2].Text;
                txtSerial.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[3].Text;
                txtWarr.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[5].Text;
                txtselling.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[6].Text;

                txtQnty.Text = Invoiced_Item_Quntity = listV_Invoiced_Item.SelectedItems[0].SubItems[7].Text;

                txtqtyCopy.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[7].Text;

                txtFreeQnt.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[8].Text;
                txtAmount.Text = listV_Invoiced_Item.SelectedItems[0].SubItems[9].Text;

                txtQnty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if do not select 
            //if (Status1.Text == "")
            //{
            //    MessageBox.Show("Please Select the Item ststus that customer return.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Status1.Focus();
            //    return;
            //}//end if...

            //if (Convert.ToDouble(txtQnty.Text) > Convert.ToDouble(txtqtyCopy.Text) && Convert.ToDouble(txtqtyCopy.Text) > 0)
            //{
            //    MessageBox.Show("Please add less than the amount you perchese.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtQnty.Focus();
            //    return;
            //}//end if...
            try
            {

                if (txtreturnAmount.Text == "")
                {
                    MessageBox.Show("Please Enter Return Amount", "Message");
                    return;
                }






                for (int i = 0; i <= listV_Return_Item.Items.Count - 1; i++)
                {
                    //MessageBox.Show("ID: " + listV_Return_Item.Items[i].SubItems[0].Text);
                    //MessageBox.Show("bat: " + listV_Return_Item.Items[i].SubItems[1].Text);
                    //MessageBox.Show("serial: " + listV_Return_Item.Items[i].SubItems[3].Text);

                    if (listV_Return_Item.Items[i].SubItems[0].Text == txtID.Text && listV_Return_Item.Items[i].SubItems[1].Text == txtBatch.Text && listV_Return_Item.Items[i].SubItems[3].Text == txtSerial.Text)
                    {
                        MessageBox.Show("This Item already added to return. please try another item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Return_Item_txt_clear();
                        return;

                    }//end if...
                }//end for...

                check_Item_Already_return_Or_Not_and_Insert();


                //clear txt box....
                Return_Item_txt_clear();
                txtreturnAmount.Text = "0";
                // Status.Text = "";

                //calc total rturn amount...
                calc_return_tot();

                GetTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listV_Return_Item_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listV_Return_Item.SelectedItems[0].Remove();

            calc_return_tot();
            GetTotalAmount();
        }

        private void txtQnty_TextChanged(object sender, EventArgs e)
        {
            if (txtQnty.Text == "")
            {
                btnAdd.Enabled = false;
                Status1.Enabled = false;
                return;
            }

            if (Convert.ToDouble(txtQnty.Text) > 0)
            {
                btnAdd.Enabled = true;
                Status1.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                Status1.Enabled = false;
            }

            

        }

        private void ItmBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                //InsertInToListVeiw();

                InsertInToListVeiw();
                ListMultiItems.Focus();

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            InsertInToListVeiw();

            ListMultiItems.Focus();
        }

        private void ListMultiItems_KeyDown(object sender, KeyEventArgs e)
        {

            //if (LvIndex.Text != " selected Index Number")
            if (ListMultiItems.Items.Count > -1)
            {
                // ListMultiItems.Focus();

                if (e.KeyValue == 13)
                {
                    if (btnPnlItmQty.Enabled == true)
                    {
                        btnPnlItmQty.Focus();
                    }

                    else
                    {
                        AddMultiItemToInvoiceList();
                        GetTotalAmount();
                    }

                }

                if (e.KeyCode == Keys.Escape)
                {
                    if (PnlMultiItems.Visible == true)
                    {
                        PnlMultiItems.Visible = false;
                        ListMultiItems.Items.Clear();
                        btnPnlItmQty.Text = "0";
                        btnPnlItmFreQty.Text = "0";
                        LvIndex.Text = " selected Index Number";
                    }
                }

            }
        }

        private void ListMultiItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {


            AddMultiItemToInvoiceList();

            GetTotalAmount();
        }

        private void ListMultiItems_SelectedIndexChanged(object sender, EventArgs e)
        {

            int a = ListMultiItems.FocusedItem.Index;

            ListMultiItems.Items[a].BackColor = Color.AliceBlue;

            LvIndex.Text = Convert.ToString(a);
        }

        private void btnPnlItmQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnPnlItmFreQty.Focus();
            }
        }

        private void btnPnlItmQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnPnlItmQty_Leave(object sender, EventArgs e)
        {
            if (btnPnlItmQty.Text == "")
            {
                btnPnlItmQty.Text = "0";

            }
        }

        private void btnPnlItmFreQty_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                btnAddToList.Focus();
            }
        }

        private void btnPnlItmFreQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnAddToList_Click(object sender, EventArgs e)
        {

            AddMultiItemToInvoiceList();

            GetTotalAmount();

            ListMultiItems.Items.Clear();
        }

        private void PlnHide_Click(object sender, EventArgs e)
        {
            PnlMultiItems.Visible = false;
            ItmBarCode.Text = "";
            ItmBarCode.Focus();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtSubTotal.Text) <= 0 )
            {
                button5.Enabled = false;
                txtTaxPer.Enabled = false;
                button14.Enabled = false;

                
            }

            if (Convert.ToDouble(txtSubTotal.Text) > 0)
            {
                button5.Enabled = true;
                txtTaxPer.Enabled = true;
                button14.Enabled = true;
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {

            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0.00";
            }
        }

        private void btxtdiscValu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btxtdiscValu.Text == "")
                {
                    return;
                }

                double subtot = Convert.ToDouble(txtSubTotal.Text);
                double discountValv = Convert.ToDouble(btxtdiscValu.Text);
                //double discount = Convert.ToDouble(txtDiscount.Text);

                double dispre = discountValv * 100 / subtot;

                txtDiscount.Text = Convert.ToString(dispre);

                txtTotal.Text = Convert.ToString(subtot - discountValv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btxtdiscValu_Leave(object sender, EventArgs e)
        {
            if (btxtdiscValu.Text == "")
            {
                btxtdiscValu.Text = "0.00";
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotalPayment.Text = txtTotal.Text;

            if (Convert.ToDouble(txtSubTotal.Text) <= 0 )
            {
                button5.Enabled = false;
                txtTaxPer.Enabled = false;
                button14.Enabled = false;

            }

            if (Convert.ToDouble(txtSubTotal.Text) > 0 )
            {
                button5.Enabled = true;
                txtTaxPer.Enabled = true;
                button14.Enabled = true;
            }
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            PayAmountInvoiced.Text = txtTotalPayment.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                PnlAddress.Visible = false;
                // PnlNote.Visible = false;
                PnlPaymentMethod.Visible = true;
                // PAyCredits.Enabled = true;

                double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

                PayBalance.Text = Convert.ToString(Balance);
                // PAyCredits.Text = Convert.ToString(Balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Totalcash_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Totalcash_TextChanged(object sender, EventArgs e)
        {
            if (Totalcash.Text == "")
            {
                Totalcash.Text = "0";
            }

            double x = Convert.ToDouble(Totalcash.Text) - Convert.ToDouble(amountCash.Text);

            Balance.Text = x.ToString();
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

        private void PayCash_Leave(object sender, EventArgs e)
        {
            if (PayCash.Text == "")
            {
                PayCash.Text = "00.00";

            }
        }

        private void PayCash_TextChanged(object sender, EventArgs e)
        {
            if (PayCash.Text == "")
            {
                return;
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
           // PayBalance.Text = Convert.ToString(Balance);

           // if (PAyCredits.Text == "0.00")
           // {
               // PAyCredits.Text = Convert.ToString(Balance);
          //  }
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
                // CkBank.Text = "";
                CkNumber.Text = "";

                CkBank.Items.Clear();
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

        private void PayCheck_Leave(object sender, EventArgs e)
        {
            if (PayCheck.Text == "")
            {
                PayCheck.Text = "00.00";
            }
        }

        private void PayCheck_TextChanged(object sender, EventArgs e)
        {
            if (PayCheck.Text == "")
            {
                return;
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
           // PAyCredits.Text = Convert.ToString(Balance);
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

        private void PayCrditCard_Leave(object sender, EventArgs e)
        {
            if (PayCrditCard.Text == "")
            {
                PayCrditCard.Text = "00.00";
            }
        }

        private void PayCrditCard_TextChanged(object sender, EventArgs e)
        {
            if (PayCrditCard.Text == "")
            {
                return;
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
            //PAyCredits.Text = Convert.ToString(Balance);
            
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

        private void PayDebitCard_Leave(object sender, EventArgs e)
        {

            if (PayDebitCard.Text == "")
            {
                PayDebitCard.Text = "00.00";
            }
        }

        private void PayDebitCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PayDebitCard.Text == "")
                {
                    return;
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
                //  PAyCredits.Text = Convert.ToString(Balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void PAyCredits_Leave(object sender, EventArgs e)
        {
            if (PAyCredits.Text == "")
            {
                PAyCredits.Text = "00.00";
            }
        }

        private void PAyCredits_TextChanged(object sender, EventArgs e)
        {
            if (PAyCredits.Text == "")
            {
                return;
            }

            PayMethod();

            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            if (Balance < 0)
            {
                MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PAyCredits.Text = "00.00";
                return;
            }

            PayBalance.Text = Convert.ToString(Balance);
           // PAyCredits.Text = Convert.ToString(Balance);
        }

        private void PayAmountInvoiced_TextChanged(object sender, EventArgs e)
        {
            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);
           // PAyCredits.Text = Convert.ToString(Balance);
        }

        private void PayAmountInvoiced_Click(object sender, EventArgs e)
        {
            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);

            //PAyCredits.Text = Convert.ToString(Balance);
        }

        private void button13_Click(object sender, EventArgs e)
        {
             PnlAddress.Visible = false;
            //PnlNote.Visible = false;
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;
            Totalcash.Focus();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
            // PnlNote.Visible = false;
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView2.SelectedItems[0].Remove();

            #region Uncheck RadioBution And clear Manuala Price.................................................
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            chbManualPrice.Checked = false;
            Dis01.Text = "00.00";
            Dis02.Text = "00.00";
            Dis03.Text = "00.00";
            Dis04.Text = "00.00";
            txtManualPRice.Text = "0.00";
            #endregion

            Active_Deactive_Payment_Panel();

            GetTotalAmount();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Return_Auto_ID_Create();

            try
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
                PayBalance.Focus();
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

            if ((CusID.Text == "WK_Customer" && CusID.Text != "Quick_Customer") && Convert.ToDouble(txtTotal.Text) < 0)
            {
                MessageBox.Show("Walking coustomer's cannot add debit payments. please issue some items to clear return items.","cannot pay as debits",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            string GRN_ODS_Vdener = "";
            string GRN_ODS_Number = "";

             DialogResult result = MessageBox.Show("Are you whether you need to complete the invoice?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             if (result == DialogResult.Yes)
             {
                
                

                 if (listView2.Items.Count > 0)
                 {
                     #region add invoice selling details including customer details----------------------------------------------------------------------------

                     string CusStatus = "";


                     if (CusID.Text != "")
                     {
                         CusStatus = CusID.Text;
                     }


                     getCreateInvoiceCode();

                     //CusStatus = Sold,Cancel 
                     SqlConnection con4 = new SqlConnection(IMS);
                     con4.Open();
                     string InsertInvoiceDetails = "INSERT INTO SoldInvoiceDetails(InvoiceNo,InvoiceStatus,CusStatus,CusFirstName,CusPersonalAddress,CusTelNUmber,CreatedBy,InvoiceRemark,Return_Invoice_Or_Original,Return_Invoice_Num) VALUES('" + NewInvoiceID.Text + "','Sold','" + CusStatus + "','" + CusFirstName.Text + "','" + CusPersonalAddress.Text + "','" + CusTelNUmber.Text + "','" + LgUser.Text + "','" + InvoiceRemark.Text + "','Returned','" + InvoiceID.Text + "')";
                     SqlCommand cmd4 = new SqlCommand(InsertInvoiceDetails, con4);
                     cmd4.ExecuteNonQuery();


                     if (con4.State == ConnectionState.Open)
                     {
                         cmd4.Dispose();
                         con4.Close();
                     }
                     //------------------------------------------------------------------------------------------------------------------------------------
                     #endregion
                 }


                 PassInvoiceNumber.Text = NewInvoiceID.Text;

                 for (int i = 0; i <= listV_Return_Item.Items.Count - 1; i++)
                 {

                     //#####################################################.............................................................
                     //Update the Stock ------------------------------------------------.............................................................
                     //#####################################################.............................................................


                     #region if Items return as Unique Item..................

                     //chek if its a Unique item...............
                     if (listV_Return_Item.Items[i].SubItems[1].Text == "No")
                     {

                         SqlConnection con1 = new SqlConnection(IMS);
                         con1.Open();

                         //select vender ID Unique Item........###############################################

                         string select_UnQue_Item_Vender = "SELECT VendorID,OrderID FROM CurrentStockItems WHERE ([ItemID]='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BarcodeNumber]='" + listV_Return_Item.Items[i].SubItems[3].Text + "')";
                         SqlCommand cmdVen = new SqlCommand(select_UnQue_Item_Vender, con1);
                         SqlDataReader drrs = cmdVen.ExecuteReader();

                         if (drrs.Read())
                         {
                             GRN_ODS_Vdener = drrs[0].ToString();
                             GRN_ODS_Number = drrs[1].ToString();
                         }


                         if (con1.State == ConnectionState.Open)
                         {
                             drrs.Close();
                         }

                         //###############################################################


                         //if Item is not damage and add to stock again.....................
                         if (listV_Return_Item.Items[i].SubItems[12].Text == "Stock")
                         {
                             //MessageBox.Show(listV_Return_Item.Items[i].SubItems[12].Text);

                             string UpdateStock_Status = @"UPDATE CurrentStockItems SET ItmStatus='Stock' WHERE ([ItemID]='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BarcodeNumber]='" + listV_Return_Item.Items[i].SubItems[3].Text + "')";
                             SqlCommand cmd1 = new SqlCommand(UpdateStock_Status, con1);
                             cmd1.ExecuteNonQuery();

                             //update Item Count------------------------------------------

                             #region Update available stock count-----------------------------------

                             string availableStock = "";

                             SqlConnection conn = new SqlConnection(IMS);
                             conn.Open();

                             string SelectStkCOunt = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID='" + listV_Return_Item.Items[i].SubItems[0].Text + "'";
                             SqlCommand cmdd = new SqlCommand(SelectStkCOunt, conn);
                             SqlDataReader drr = cmdd.ExecuteReader();

                             if (drr.Read())
                             {
                                 availableStock = drr[0].ToString();
                             }
                             if (conn.State == ConnectionState.Open)
                             {
                                 drr.Close();
                             }

                             //update

                             //calculate the stock ammount after adding new items
                             string newstk = Convert.ToString(Convert.ToInt32(availableStock) + 1);

                             //MessageBox.Show(newstk);

                             SqlConnection con12 = new SqlConnection(IMS);
                             con12.Open();
                             string InsertItemsToStockCount = "UPDATE CurrentStock SET AvailableStockCount ='" + newstk + "' WHERE ItemID ='" + listV_Return_Item.Items[i].SubItems[0].Text + "'";
                             SqlCommand cmd2 = new SqlCommand(InsertItemsToStockCount, con12);
                             cmd2.ExecuteNonQuery();


                             if (con12.State == ConnectionState.Open)
                             {
                                 con12.Close();
                             }


                             //================================================================================================================================
                             #endregion

                             //string UpdateItm_Count = @"UPDATE CurrentStockItems SET ItmStatus='Stock' WHERE ([ItemID]='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BarcodeNumber]='" + listV_Return_Item.Items[i].SubItems[3].Text + "')";
                             //SqlCommand cmd3 = new SqlCommand(UpdateItm_Count, con1);
                             //cmd3.ExecuteNonQuery();

                         }//end if...

                         //if Item is Damgage---###########################################################################---

                         if (listV_Return_Item.Items[i].SubItems[1].Text == "Damage")
                         {
                             string UpdateStock_Status = @"UPDATE CurrentStockItems SET ItmStatus='Damage' WHERE ([ItemID]='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BarcodeNumber]='" + listV_Return_Item.Items[i].SubItems[3].Text + "')";
                             SqlCommand cmd1 = new SqlCommand(UpdateStock_Status, con1);
                             cmd1.ExecuteNonQuery();
                         }//end if.........


                     }
                     #endregion

                     #region if Items return as Wholesale Item..................

                     if (listV_Return_Item.Items[i].SubItems[1].Text != "No")
                     {
                        // MessageBox.Show("no batch");

                         SqlConnection con1 = new SqlConnection(IMS);
                         con1.Open();

                         //select GRN NUmber whosale Item........###############################################

                         string select_UnQue_Item_GRN = "SELECT GRNNumber FROM GRNWholesaleItems WHERE ([ItemID]='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BatchNumber]='" + listV_Return_Item.Items[i].SubItems[1].Text + "')";
                         SqlCommand cmdVen = new SqlCommand(select_UnQue_Item_GRN, con1);
                         SqlDataReader drrs = cmdVen.ExecuteReader();

                         if (drrs.Read())
                         {
                             GRN_ODS_Number = drrs[0].ToString();

                            // MessageBox.Show(drrs[0].ToString());

                         }


                         if (con1.State == ConnectionState.Open)
                         {
                             drrs.Close();
                         }

                         //###################################################################################


                         //select Vebdoe whosale Item........###############################################

                         string select_UnQue_Item_Vender = "SELECT Vender_ID FROM GRN_amount_Details WHERE ([GRN_No]='" + GRN_ODS_Number + "')";
                         SqlCommand cmdV = new SqlCommand(select_UnQue_Item_Vender, con1);
                         SqlDataReader drrsz = cmdV.ExecuteReader();

                         if (drrsz.Read())
                         {
                             GRN_ODS_Vdener = drrsz[0].ToString();
                            // MessageBox.Show(GRN_ODS_Vdener);

                         }


                         if (con1.State == ConnectionState.Open)
                         {
                             drrsz.Close();
                         }

                         //###################################################################################
                        // MessageBox.Show(listV_Return_Item.Items[i].SubItems[10].Text);
                         //if Item is not damage and add to stock again.....................
                         if (listV_Return_Item.Items[i].SubItems[12].Text == "Stock")
                         {
                            // MessageBox.Show("Stock");
                             string itemCount1 = "";

                             //------------------------------------------------------
                             string SelectStkCOuntWhol = "SELECT AvailbleItemCount FROM GRNWholesaleItems WHERE (ItemID='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND BatchNumber='" + listV_Return_Item.Items[i].SubItems[1].Text + "')";
                             SqlCommand cmdd = new SqlCommand(SelectStkCOuntWhol, con1);
                             SqlDataReader drr1x = cmdd.ExecuteReader();



                             if (drr1x.Read())
                             {
                                 itemCount1 = drr1x[0].ToString();
                             }

                             if (con1.State == ConnectionState.Open)
                             {
                                 drr1x.Close();
                             }

                             string newstk = Convert.ToString(Convert.ToDouble(itemCount1) + Convert.ToDouble(listV_Return_Item.Items[i].SubItems[7].Text));

                             //MessageBox.Show(drr1x[0].ToString());
                             //MessageBox.Show(listV_Return_Item.Items[i].SubItems[7].Text);
                            // MessageBox.Show(newstk);


                             //--------------------------------------------------

                             string UpdateStock_Status = @"UPDATE GRNWholesaleItems SET AvailbleItemCount='" + newstk + "' WHERE (ItemID='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BatchNumber]='" + listV_Return_Item.Items[i].SubItems[1].Text + "')";
                             SqlCommand cmd1 = new SqlCommand(UpdateStock_Status, con1);
                             cmd1.ExecuteNonQuery();

                             //update Item Count------------------------------------------

                             #region Update available stock count -----------------------------------

                             string availableStock = "";

                             SqlConnection conn = new SqlConnection(IMS);
                             conn.Open();

                             string SelectStkCOunt = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID='" + listV_Return_Item.Items[i].SubItems[0].Text + "'";
                             SqlCommand cmdd1 = new SqlCommand(SelectStkCOunt, conn);
                             SqlDataReader drr1 = cmdd1.ExecuteReader();

                             if (drr1.Read())
                             {
                                 availableStock = drr1[0].ToString();
                             }

                         

                             if (con1.State == ConnectionState.Open)
                             {
                                 drr1.Close();
                             }



                             //update

                             //calculate the stock ammount after adding new items
                             string newstk1 = Convert.ToString(Convert.ToDouble(availableStock) + Convert.ToDouble(listV_Return_Item.Items[i].SubItems[7].Text));

                             //SqlConnection con = new SqlConnection(IMS);
                             //con1.Open();
                             string InsertItemsToStockCount = "UPDATE CurrentStock SET AvailableStockCount ='" + newstk1 + "' WHERE ItemID ='" + listV_Return_Item.Items[i].SubItems[0].Text + "'";
                             SqlCommand cmd2 = new SqlCommand(InsertItemsToStockCount, con1);
                             cmd2.ExecuteNonQuery();

                             //if (conn.State == ConnectionState.Open)
                             //{
                             //    drr1.Close();
                             //}

                             #endregion


                         }



                         if (con1.State == ConnectionState.Open)
                         {
                             con1.Close();
                         }

                         //================================================================================================================================


                         //string UpdateItm_Count = @"UPDATE CurrentStockItems SET ItmStatus='Stock' WHERE ([ItemID]='" + listV_Return_Item.Items[i].SubItems[0].Text + "' AND [BarcodeNumber]='" + listV_Return_Item.Items[i].SubItems[3].Text + ")'";
                         //SqlCommand cmd3 = new SqlCommand(UpdateItm_Count, con1);
                         //cmd3.ExecuteNonQuery();

                     }//end if...
                     #endregion

                     #region update Damage Item items.....



                     if (listV_Return_Item.Items[i].SubItems[10].Text == "Damage")
                     {
                         SqlConnection conx = new SqlConnection(IMS);
                         conx.Open();

                         string Add_Damage_wholeSale = @"INSERT INTO Damage_Return_Note( Dmg_Return_Num,Vender_ID, Invoice_Num, GRN_Num, Batch_Num,Item_ID, Itm_Name, Serial_Num, Warr_Period, Selling_Price, Damage_Qnty, Tot_Amount, Date,ReturnAmount,Net_Total) 
                                                              VALUES( @Dmg_Return_Num,@Vender_ID, @Invoice_Num, @GRN_Num, @Batch_Num,@Item_ID, @Itm_Name, @Serial_Num, @Warr_Period, @Selling_Price, @Damage_Qnty, @Tot_Amount, @Date,@ReturnAmount,@Net_Total)";
                         SqlCommand cmd = new SqlCommand(Add_Damage_wholeSale, conx);

                         cmd.Parameters.AddWithValue("Dmg_Return_Num", Rtn_ID.Text);
                         cmd.Parameters.AddWithValue("Vender_ID", GRN_ODS_Vdener);
                         cmd.Parameters.AddWithValue("Invoice_Num", InvoiceID.Text);
                         cmd.Parameters.AddWithValue("GRN_Num", GRN_ODS_Number);
                         cmd.Parameters.AddWithValue("Batch_Num", listV_Return_Item.Items[i].SubItems[1].Text);
                         cmd.Parameters.AddWithValue("Item_ID", listV_Return_Item.Items[i].SubItems[0].Text);
                         cmd.Parameters.AddWithValue("Itm_Name", listV_Return_Item.Items[i].SubItems[2].Text);
                         cmd.Parameters.AddWithValue("Serial_Num", listV_Return_Item.Items[i].SubItems[3].Text);
                         cmd.Parameters.AddWithValue("Warr_Period", listV_Return_Item.Items[i].SubItems[5].Text);

                         cmd.Parameters.AddWithValue("Selling_Price", listV_Return_Item.Items[i].SubItems[6].Text);
                         cmd.Parameters.AddWithValue("Damage_Qnty", listV_Return_Item.Items[i].SubItems[7].Text);
                         cmd.Parameters.AddWithValue("Tot_Amount", listV_Return_Item.Items[i].SubItems[12].Text);
                         cmd.Parameters.AddWithValue("Date", DateTime.Now.ToString());
                         cmd.Parameters.AddWithValue("ReturnAmount", listV_Return_Item.Items[i].SubItems[10].Text);
                         cmd.Parameters.AddWithValue("Net_Total", listV_Return_Item.Items[i].SubItems[11].Text);
                        // MessageBox.Show(listV_Return_Item.Items[i].SubItems[10].Text);

                         cmd.ExecuteNonQuery();

                     }//end if.....



                     #endregion

                     #region update Return Item items.....

                     Return_Auto_ID_Create();

                     SqlConnection cony = new SqlConnection(IMS);
                     cony.Open();

                     string Add_Return = @"INSERT INTO Return_Note(Return_Num, Vender_ID, Invoice_Num, GRN_Num, Batch_Num,Item_ID, Itm_Name, Serial_Num, Warr_Period, Selling_Price, Return_Qnty, Tot_Amount, 
                                            Return_Statement, Return_Accept_by, Return_Date,ReturnAmount,Net_Total) VALUES(@Return_Num, @Vender_ID, @Invoice_Num, @GRN_Num, @Batch_Num,@Item_ID, @Itm_Name, @Serial_Num, @Warr_Period, @Selling_Price, @Return_Qnty, @Tot_Amount, 
                                            @Return_Statement, @Return_Accept_by, @Return_Date,@ReturnAmount,@Net_Total)";

                     SqlCommand cmdy = new SqlCommand(Add_Return, cony);

                     cmdy.Parameters.AddWithValue("Return_Num", Rtn_ID.Text);
                     cmdy.Parameters.AddWithValue("Vender_ID", GRN_ODS_Vdener);
                     cmdy.Parameters.AddWithValue("Invoice_Num", InvoiceID.Text);
                     cmdy.Parameters.AddWithValue("GRN_Num", GRN_ODS_Number);
                     cmdy.Parameters.AddWithValue("Batch_Num", listV_Return_Item.Items[i].SubItems[1].Text);
                     cmdy.Parameters.AddWithValue("Item_ID", listV_Return_Item.Items[i].SubItems[0].Text);
                     cmdy.Parameters.AddWithValue("Itm_Name", listV_Return_Item.Items[i].SubItems[2].Text);
                     cmdy.Parameters.AddWithValue("Serial_Num", listV_Return_Item.Items[i].SubItems[3].Text);

                     cmdy.Parameters.AddWithValue("Warr_Period", listV_Return_Item.Items[i].SubItems[5].Text);
                     cmdy.Parameters.AddWithValue("Selling_Price", listV_Return_Item.Items[i].SubItems[6].Text);
                     cmdy.Parameters.AddWithValue("Return_Qnty", listV_Return_Item.Items[i].SubItems[7].Text);
                     cmdy.Parameters.AddWithValue("Tot_Amount", listV_Return_Item.Items[i].SubItems[9].Text);
                     cmdy.Parameters.AddWithValue("Return_Statement", listV_Return_Item.Items[i].SubItems[12].Text);
                     cmdy.Parameters.AddWithValue("Return_Accept_by", LgUser.Text);
                     cmdy.Parameters.AddWithValue("Return_Date", DateTime.Now.ToString());
                     cmdy.Parameters.AddWithValue("ReturnAmount", listV_Return_Item.Items[i].SubItems[10].Text);
                     cmdy.Parameters.AddWithValue("Net_Total", listV_Return_Item.Items[i].SubItems[11].Text);
                     //MessageBox.Show(listV_Return_Item.Items[i].SubItems[17].Text);
                     //MessageBox.Show(listV_Return_Item.Items[i].SubItems[18].Text);
                    // return;
                     cmdy.ExecuteNonQuery();

                     if (cony.State == ConnectionState.Open)
                     {
                         cony.Close();
                     }

                     #endregion

                 }

                 int z;
                 string x = "";

                 #region insert data in list view to DB========================================================

                 for (z = 0; z <= listView2.Items.Count - 1; z++)
                 {

                     //#####################################################.............................................................
                     //Invoice Details ------------------------------------------------.............................................................
                     //#####################################################.............................................................

                     //if Payment Balance is <0, then the totoal negative ammount debited to customer current credit payments....
                     //if customer s warking customer, we cannot add to credit payments. we must return payments......

                     #region chage item status to Sold---------------------------------------------------------------------------------------------
                     if (listView2.Items[z].SubItems[3].Text != "No_ID")
                     {
                         SqlConnection con = new SqlConnection(IMS);
                         con.Open();
                         string UpateStockStock = "UPDATE CurrentStockItems SET ItmStatus='Sold' WHERE BarcodeNumber='" + listView2.Items[z].SubItems[3].Text + "' AND SystemID='" + listView2.Items[z].SubItems[4].Text + "'";
                         SqlCommand cmd = new SqlCommand(UpateStockStock, con);
                         cmd.ExecuteNonQuery();

                         if (con.State == ConnectionState.Open)
                         {
                             con.Close();
                         }
                     }
                     //-----------------------------------------------------------------------------------------------------------------------------
                     #endregion

                     #region update stock wholesale========================================================================================

                     if (listView2.Items[z].SubItems[3].Text == "No_ID")
                     {
                         //MessageBox.Show("Read wholesale");

                         SqlConnection cony = new SqlConnection(IMS);
                         cony.Open();
                         string SelectTotalWholesaleStockCount = "SELECT AvailbleItemCount FROM GRNWholesaleItems WHERE ItemID= '" + listView2.Items[z].SubItems[0].Text + "' AND BatchNumber='" + listView2.Items[z].SubItems[1].Text + "'";

                         SqlCommand cmdy = new SqlCommand(SelectTotalWholesaleStockCount, cony);
                         SqlDataReader dry = cmdy.ExecuteReader(CommandBehavior.CloseConnection);

                         string Cnt = "";

                         if (dry.Read())
                         {
                             Cnt = dry[0].ToString();
                         }
                         double StoskCount = Convert.ToDouble(Cnt);

                         if (cony.State == ConnectionState.Open)
                         {
                             cony.Close();
                             dry.Close();
                         }

                         double FinalCount = StoskCount - (Convert.ToDouble(listView2.Items[z].SubItems[7].Text) + Convert.ToDouble(listView2.Items[z].SubItems[8].Text));



                         SqlConnection conx = new SqlConnection(IMS);
                         conx.Open();
                         string UpateStockStockWholesale = "UPDATE GRNWholesaleItems SET AvailbleItemCount='" + Convert.ToString(FinalCount) + "' WHERE ItemID='" + listView2.Items[z].SubItems[0].Text + "' AND BatchNumber='" + listView2.Items[z].SubItems[1].Text + "'";
                         SqlCommand cmdx = new SqlCommand(UpateStockStockWholesale, conx);
                         cmdx.ExecuteNonQuery();

                         if (conx.State == ConnectionState.Open)
                         {
                             conx.Close();
                         }

                         //================================================================================================================
                     }
                     #endregion


                     #region Reduse the Stock Count-------------------------------------------------------------------------------------------------------

                     SqlConnection con1 = new SqlConnection(IMS);
                     con1.Open();
                     string SelectTotalStockCount = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID= '" + listView2.Items[z].SubItems[0].Text + "'";
                     SqlCommand cmd1 = new SqlCommand(SelectTotalStockCount, con1);
                     SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                     if (dr1.Read())
                     {
                         x = dr1[0].ToString();
                     }
                     double Q = Convert.ToDouble(x);

                     if (con1.State == ConnectionState.Open)
                     {
                         con1.Close();
                         dr1.Close();
                     }
                     double y = Q - (Convert.ToDouble(listView2.Items[z].SubItems[7].Text) + Convert.ToDouble(listView2.Items[z].SubItems[8].Text));

                     SqlConnection con2 = new SqlConnection(IMS);
                     con2.Open();
                     string UpateStockCount = "UPDATE [CurrentStock] SET AvailableStockCount=" + y + " WHERE ItemID='" + listView2.Items[z].SubItems[0].Text + "'";
                     SqlCommand cmd2 = new SqlCommand(UpateStockCount, con2);
                     cmd2.ExecuteNonQuery();

                     if (con2.State == ConnectionState.Open)
                     {
                         cmd2.Dispose();
                         con2.Close();
                         // dr.Close();
                     }
                     //----------------------------------------------------------------------------------------------------------------------------------------------
                     #endregion

                     #region Inseret Sold Item details.----------------------------------------------------------------------------------------------------------

                     SqlConnection con3 = new SqlConnection(IMS);
                     con3.Open();
                     string InsertItemSoldCustomerDetails = "INSERT INTO SoldItemDetails(InvoiceID,ItemID,ItemName,ItemWarrenty,BarcodeID,SystemID,BuyPrice,SoldPrice,ItmQuantity,FreeQuantity,BatchID) VALUES(@InvoiceID,@ItemID,@ItemName,@ItemWarrenty,@BarcodeID,@SystemID,@BuyPrice,@SoldPrice,@ItmQuantity,@FreeQuantity,@BatchID)";

                     SqlCommand cmd3 = new SqlCommand(InsertItemSoldCustomerDetails, con3);

                     cmd3.Parameters.AddWithValue("InvoiceID", NewInvoiceID.Text);
                     cmd3.Parameters.AddWithValue("ItemID", listView2.Items[z].SubItems[0].Text);
                     cmd3.Parameters.AddWithValue("ItemName", listView2.Items[z].SubItems[2].Text);
                     cmd3.Parameters.AddWithValue("ItemWarrenty", listView2.Items[z].SubItems[5].Text);

                     //Barcode Item is = to serial number
                     cmd3.Parameters.AddWithValue("BarcodeID", listView2.Items[z].SubItems[3].Text);
                     //systemID=Barcode
                     cmd3.Parameters.AddWithValue("SystemID", listView2.Items[z].SubItems[4].Text);

                     cmd3.Parameters.AddWithValue("BuyPrice", listView2.Items[z].SubItems[15].Text);
                     cmd3.Parameters.AddWithValue("SoldPrice", listView2.Items[z].SubItems[6].Text);
                     cmd3.Parameters.AddWithValue("ItmQuantity", listView2.Items[z].SubItems[7].Text);
                     cmd3.Parameters.AddWithValue("FreeQuantity", listView2.Items[z].SubItems[8].Text);
                     cmd3.Parameters.AddWithValue("BatchID", listView2.Items[z].SubItems[1].Text);



                     cmd3.ExecuteNonQuery();

                     if (con3.State == ConnectionState.Open)
                     {
                         cmd3.Dispose();
                         // dr.Close();
                         con3.Close();
                     }

                     #endregion

                 }//end for loop..

                

                  
                     #region insert Chq Details===============================================================================================

                 if (CkCkBox.Checked == true)
                 {
                     SqlConnection con6 = new SqlConnection(IMS);
                     con6.Open();
                     string InsertCheckPaymetDetails = "INSERT INTO InvoiceCheckDetails(InvoiceID,CkStatus,CkNumber,Bank,Amount,CurrentDate,MentionDate) VALUES('" + NewInvoiceID.Text + "','Active','" + CkNumber.Text + "','" + label77.Text + "','" + PayCheck.Text + "','" + System.DateTime.Now.ToString() + "','" + Ckdate.Text + "')";
                     SqlCommand cmd6 = new SqlCommand(InsertCheckPaymetDetails, con6);
                     cmd6.ExecuteNonQuery();


                     if (con6.State == ConnectionState.Open)
                     {
                         cmd6.Dispose();
                         con6.Close();
                     }
                 }

                 #endregion


                 #region credit paymetn table update----------------------------------------

                 double total_Invoice_Amount = Convert.ToDouble(txtTotal.Text);

                 if (total_Invoice_Amount >= 0 && CusID.Text != "WK_Customer" && CusID.Text != "Quick_Customer")
                 {
                     Customer_Credit_Balance_Update();

                 }//end if.


                 //if total amount is negative, if that customer is regular, we debited his alance to the"Debit Balance."
                 if (total_Invoice_Amount < 0 && CusID.Text != "WK_Customer" && CusID.Text != "Quick_Customer")
                 {
                     Customer_Final_Balance_Update();

                 }//end if.

                 #endregion

                 #region insert Invoice Paymet Details--------------------------------------------------------------------------------------------------------

                 if (Convert.ToDouble(txtSubTotal.Text) <= 0)
                 {
                     txtSubTotal.Text = "0";
                     txtTaxPer.Text = "0";
                     txtTotal.Text = "0";
                 }

                 if (Convert.ToDouble(txtSubTotal.Text) > 0)
                 {
                     txtSubTotal.Text = txtSubTotal.Text;
                     txtTaxPer.Text = txtTaxPer.Text;
                     txtTotal.Text = txtTotal.Text;
                 }

                 SqlConnection con5 = new SqlConnection(IMS);
                 con5.Open();
                 string InsertInvoicePaymetDetails = "INSERT INTO InvoicePaymentDetails (InvoiceID,SubTotal,VATpresentage,GrandTotal,PayCash,PayCheck,PayCrditCard,PayDebitCard,PAyCredits,PayBalance,InvoiceDate,InvoiceDiscount) VALUES('" + NewInvoiceID.Text + "','" + txtSubTotal.Text + "','" + txtTaxPer.Text + "','" + txtTotal.Text + "','" + PayCash.Text + "','" + PayCheck.Text + "','" + PayCrditCard.Text + "','" + PayDebitCard.Text + "','" + PayBalance.Text + "','" + PayBalance.Text + "','" + System.DateTime.Now.ToString() + "','" + btxtdiscValu.Text + "')";
                 SqlCommand cmd5 = new SqlCommand(InsertInvoicePaymetDetails, con5);
                 cmd5.ExecuteNonQuery();


                 if (con5.State == ConnectionState.Open)
                 {
                     cmd5.Dispose();
                     con5.Close();
                 }

                 #endregion


                 #endregion


                 MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 tabControl1.SelectedIndex = 0;
                 if (listView2.Items.Count > 0)
                 {
                     BillForm billfm = new BillForm();
                     billfm.InvoiveNumberToPrint = PassInvoiceNumber.Text;
                     billfm.PrintCopyDetails = "Original Copy";
                     billfm.Visible = true;
                 }

                 rptInvoiceReturn sms = new rptInvoiceReturn();
                 sms.PrintingRTNInvoiceNumber = Rtn_ID.Text;
                 sms.Show();

                 clear_txt();
                 getCreateInvoiceCode();
                 Return_Auto_ID_Create();
                 LblBillTotalAmount.Text = "000.00";
                 lbl_return_Amount_02.Text = "000.00";
                 radioButton1.Checked = false;
                 radioButton2.Checked = false;
                 radioButton3.Checked = false;
                 radioButton4.Checked = false;
                 chbManualPrice.Checked = false;
                 txtManualPRice.Enabled = false;
                 radioButton1.Enabled = false;
                 radioButton2.Enabled = false;
                 radioButton3.Enabled = false;
                 radioButton4.Enabled = false;
                 button1.Enabled = false;

                 Pnl_Invoice_Search.Visible = false;
                
             }

            }//end try

            catch (Exception ex)
            {
                MessageBox.Show("Error when saving your details. please contact your system administrator.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end catch
        }

        private void CusID_TextChanged(object sender, EventArgs e)
        {
            if (CusID.Text != "")
            {
                Vender_Credit_Payment();

                if (CusID.Text == "WK_Customer" && CusID.Text == "Quick_Customer")
                {
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = false;

                    label12.Enabled = true;
                    label13.Enabled = true;
                    label14.Enabled = false;
                    label18.Enabled = false;

                    Dis01.Enabled = true;
                    Dis02.Enabled = true;
                    Dis03.Enabled = false;
                    Dis04.Enabled = false;
                }
            }

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //radioButton1.Enabled = true;
            //radioButton2.Enabled = true;
            //radioButton3.Enabled = true;
            //radioButton4.Enabled = true;
            //chbManualPrice.Enabled = true;
            try
            {
                if (listView2.SelectedItems.Count > 0)
                {
                    #region Vender_Credit_Payment-----------------------------------------------------------


                    button1.Enabled = true;
                   


                        ListViewItem item = listView2.SelectedItems[0];
                        Dis01.Text = item.SubItems[14].Text;
                        Dis02.Text = item.SubItems[10].Text;
                        Dis03.Text = item.SubItems[11].Text;
                        Dis04.Text = item.SubItems[12].Text;


                        int _isRadioButton = Convert.ToInt32(item.SubItems[13].Text);

                        if (_isRadioButton == 0)
                        {
                            RadioBtn();
                            radioButton1.Checked = true;
                        }

                        if (_isRadioButton == 1)
                        {
                            RadioBtn();
                            radioButton2.Checked = true;
                        }

                        if (_isRadioButton == 2)
                        {
                            RadioBtn();
                            radioButton3.Checked = true;
                        }

                        if (_isRadioButton == 3)
                        {
                            RadioBtn();
                            radioButton4.Checked = true;
                        }
                        if (_isRadioButton == 4)
                        {
                            RadioBtn();
                            chbManualPrice.Checked = true;
                            txtManualPRice.Text = item.SubItems[6].Text;
                        }


                        selectUserSetting();

//                        SqlConnection con1 = new SqlConnection(IMS);
//                        con1.Open();

//                        if (CusID.Text != "WK_Customer")
//                        {
//                            #region select customer details...........................................................

//                            string customer_Details = @"SELECT     CusPriceLevel, CusCreditLimit
//                                            FROM         CustomerDetails
//                                            WHERE     (CusID = '" + CusID.Text + "')";
//                            SqlCommand cmd_Cus = new SqlCommand(customer_Details, con1);
//                            SqlDataReader dr_Reader = cmd_Cus.ExecuteReader(CommandBehavior.CloseConnection);

//                            string CusPriceLevel = "";
//                            string Cus_Credit_Limit = "";

//                            if (dr_Reader.Read())
//                            {
//                                CusPriceLevel = dr_Reader[0].ToString();
//                                Cus_Credit_Limit = dr_Reader[1].ToString();
//                            }

//                            //cloase data reader''''''''''''''''''
//                            dr_Reader.Close();

//                            #endregion

//                            #region active/deactive customer price level................................

//                            if (CusPriceLevel == "Selling Price")
//                            {
//                                radioButton1.Enabled = true;
//                                radioButton2.Enabled = false;
//                                radioButton3.Enabled = false;
//                                radioButton4.Enabled = false;

//                                label12.Enabled = true;
//                                label13.Enabled = false;
//                                label14.Enabled = false;
//                                label18.Enabled = false;

//                                Dis01.Enabled = true;
//                                Dis02.Enabled = false;
//                                Dis03.Enabled = false;
//                                Dis04.Enabled = false;
//                            }

//                            if (CusPriceLevel == "Discount 01")
//                            {
//                                radioButton1.Enabled = true;
//                                radioButton2.Enabled = true;
//                                radioButton3.Enabled = false;
//                                radioButton4.Enabled = false;

//                                label12.Enabled = true;
//                                label13.Enabled = true;
//                                label14.Enabled = false;
//                                label18.Enabled = false;

//                                Dis01.Enabled = true;
//                                Dis02.Enabled = true;
//                                Dis03.Enabled = false;
//                                Dis04.Enabled = false;
//                            }

//                            if (CusPriceLevel == "Discount 02")
//                            {
//                                radioButton1.Enabled = true;
//                                radioButton2.Enabled = true;
//                                radioButton3.Enabled = true;
//                                radioButton4.Enabled = false;

//                                label12.Enabled = true;
//                                label13.Enabled = true;
//                                label14.Enabled = true;
//                                label18.Enabled = false;

//                                Dis01.Enabled = true;
//                                Dis02.Enabled = true;
//                                Dis03.Enabled = true;
//                                Dis04.Enabled = false;
//                            }

//                            if (CusPriceLevel == "Discount 03")
//                            {
//                                radioButton1.Enabled = true;
//                                radioButton2.Enabled = true;
//                                radioButton3.Enabled = true;
//                                radioButton4.Enabled = true;

//                                label12.Enabled = true;
//                                label13.Enabled = true;
//                                label14.Enabled = true;
//                                label18.Enabled = true;

//                                Dis01.Enabled = true;
//                                Dis02.Enabled = true;
//                                Dis03.Enabled = true;
//                                Dis04.Enabled = true;
//                            }


//                            #endregion
//                        }


                    #endregion
                }
                else
                {
                   
                        radioButton1.Enabled = false;
                        radioButton2.Enabled = false;
                        radioButton3.Enabled = false;
                        radioButton4.Enabled = false;
                        chbManualPrice.Enabled = false;
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;
                        Dis01.Text = "00.00";
                        Dis02.Text = "00.00";
                        Dis03.Text = "00.00";
                        Dis04.Text = "00.00";
                        button1.Enabled = false;

                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                if (listView2.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView2.SelectedItems[0];

                    if (radioButton1.Checked == true)
                    {
                        item.SubItems[6].Text = Dis01.Text;

                        item.SubItems[9].Text = Convert.ToString(Convert.ToDouble(Dis01.Text) * Convert.ToDouble(item.SubItems[7].Text));
                        item.SubItems[13].Text = "0";

                    }

                    if (radioButton2.Checked == true)
                    {
                        item.SubItems[6].Text = Dis02.Text;
                        item.SubItems[9].Text = Convert.ToString(Convert.ToDouble(Dis02.Text) * Convert.ToDouble(item.SubItems[7].Text));
                        item.SubItems[13].Text = "1";
                    }

                    if (radioButton3.Checked == true)
                    {
                        item.SubItems[6].Text = Dis03.Text;
                        item.SubItems[9].Text = Convert.ToString(Convert.ToDouble(Dis03.Text) * Convert.ToDouble(item.SubItems[7].Text));
                        item.SubItems[13].Text = "2";
                    }

                    if (radioButton4.Checked == true)
                    {
                        item.SubItems[6].Text = Dis04.Text;
                        item.SubItems[9].Text = Convert.ToString(Convert.ToDouble(Dis04.Text) * Convert.ToDouble(item.SubItems[7].Text));
                        item.SubItems[13].Text = "3";
                    }

                    if (chbManualPrice.Checked == true)
                    {
                        item.SubItems[6].Text = txtManualPRice.Text;
                        item.SubItems[9].Text = Convert.ToString(Convert.ToDouble(txtManualPRice.Text) * Convert.ToDouble(item.SubItems[7].Text));
                        item.SubItems[13].Text = "4";
                    }


                    MessageBox.Show("Price has been Changed", "Price Change", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GetTotalAmount();

                    //------------------------------------------
                    #region Uncheck RadioBution And clear Manuala Price.................................................
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    chbManualPrice.Checked = false;
                    Dis01.Text = "00.00";
                    Dis02.Text = "00.00";
                    Dis03.Text = "00.00";
                    Dis04.Text = "00.00";
                    txtManualPRice.Text = "00.00";
                    #endregion
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void txtQnty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtreturnAmount.Focus();
            }
        }

        private void Status_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {

                btnAdd.Focus();    
            }
        }

        private void label42_Click(object sender, EventArgs e)
        {
            Select_Bank();
            getCreatebANKcode();
            panel8.Visible = true;
            textBox1.Focus();
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
                    label77.Text = (dr[1].ToString());
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

        private void button4_Click(object sender, EventArgs e)
        {
            #region Bank create new.......................................
            if (textBox1.Text != "")
            {
                try
                {
                    SqlConnection cnne = new SqlConnection(IMS);
                    cnne.Open();
                    String NewBank = "insert into Bank_Category(BankID,BankName) values('" + label76.Text + "','" + textBox1.Text + "')";
                    SqlCommand cmme = new SqlCommand(NewBank, cnne);
                    cmme.ExecuteNonQuery();

                    MessageBox.Show(" Insert New Bank Successfull.....!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Enter Bank name or click 'Cancel' button ");
            }
                panel8.Visible = false;
                textBox1.Text = "";
                Select_Bank();
            
            #endregion
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
        }

        private void PayBalance_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtreturnAmount_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                btnAdd.Focus();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i <= listV_Return_Item.Items.Count - 1; i++)
            //{
            //    //MessageBox.Show(listV_Return_Item.Items[i].SubItems[10].Text);
            //   MessageBox.Show(listV_Return_Item.Items[i].SubItems[12].Text);


            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                chbManualPrice.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton2.Checked == true)
            {
                chbManualPrice.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton3.Checked == true)
            {
                chbManualPrice.Checked = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton4.Checked == true)
            {
                chbManualPrice.Checked = false;
            }
        }

        private void chbManualPrice_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void txtManualPRice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button1.Focus();
            }

        }

        private void txtManualPRice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtManualPRice_TextChanged(object sender, EventArgs e)
        {
            if (txtManualPRice.Text == "")
            {
                return;
            }
        }

        private void chbManualPrice_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbManualPrice.Checked == true)
            {
                radioButton4.Checked = false;
                radioButton3.Checked = false;
                radioButton2.Checked = false;
                radioButton1.Checked = false;
                txtManualPRice.Enabled = true;
            }
            if (chbManualPrice.Checked == false)
            {
                txtManualPRice.Text = "0.00";
                txtManualPRice.Enabled = false;
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            selectUserSetting();

            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Return_Invoice reinvoice = new Return_Invoice();
            reinvoice.Visible = false;
            

        }

        private void PayCash_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                CkCkBox.Focus();
            }
        }

        private void CkCkBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PayCheck.Focus();
            }
        }

        private void PayCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PayCrditCard.Focus();
            }
        }

        private void PayCrditCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PayDebitCard.Focus();
            }
        }

        private void PayDebitCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CkBank.Focus();
            }
        }

        private void CkBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CkNumber.Focus();
            }
        }

        private void CkNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Ckdate.Focus();
            }
        }

        private void Ckdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button13.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button4.Focus();
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            //for (int w = 0; w <= listV_Return_Item.Items.Count - 1; w++)
            //{
            //  MessageBox.Show(listV_Return_Item.Items[w].SubItems[12].Text);
            
            //}
        }

        private void txtreturnAmount_Leave(object sender, EventArgs e)
        {
            if (txtreturnAmount.Text == "")
            {
                txtreturnAmount.Text = "0";
            }
        }
      
        }
    }

