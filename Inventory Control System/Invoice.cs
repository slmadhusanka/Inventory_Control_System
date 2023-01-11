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
    public partial class Invoice : Form
    {

        User_Cotrol UserCont = new User_Cotrol();

        public Invoice(string LoginPsn)
        {
            InitializeComponent();
            Select_Bank();
            InvLoginPerson.Text = LoginPsn;
           // selectUserSetting();
           
           
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


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
                    //label58.Text = (dr[1].ToString());
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
                                                VALUES  ('" + CusID.Text + "','" + InvoiceID.Text + "','" + PayBalance.Text + "','0','" + Deb_Bal + "','" + Convert.ToString(New_Bal) + "','" + DateTime.Now.ToString() + "')";

                // MessageBox.Show(Cus_DebitPaymet);
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
                label54.Text = "BNK1001";

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

                    label54.Text = "BNK" + no;

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

        public void selectUserSetting()
        {
            try
            {
                #region manual selling price

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



        private void txtPaymentDue_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = true;
            CusFirstName.Focus();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
            //PnlNote.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
            //PnlNote.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;

            ItmBarCode.Focus();   
           // PnlNote.Visible = false;

            if (checkBox1.Checked == true)
            {
                CusName.Text = CusFirstName.Text;
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
           // PnlNote.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
            //PnlNote.Visible = false;
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;

            Totalcash.Focus();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {









        }

        private void button5_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
           // PnlNote.Visible = false;
            PnlPaymentMethod.Visible = true;
           // PAyCredits.Enabled = true;

           

            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);

            PayCash.Focus();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            PnlAddress.Visible = false;
           // PnlNote.Visible = false;
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            InvDate.Text = System.DateTime.Today.ToString("dd MMMM yyyy");
            getCreateInvoiceCode();
           // selectUserSetting();

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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

                PnlCustomerSerch.Visible = true;

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

        public void ClearTxt()
        {
            CusFirstName.Text = "";
            CusPersonalAddress.Text = "";
            CusTelNUmber.Text = "";
          //  CusRemark.Text = "";


        }

        public void RadioUncheck()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
              

                DataGridViewRow dr = dataGridView1.SelectedRows[0];

                CusPreCreditLimit.Text = "00.00";

                String Name = dr.Cells[1].Value.ToString() + " " + dr.Cells[2].Value.ToString(); 

                CusID.Text = dr.Cells[0].Value.ToString();
                CusName.Text = Name;
                CusFirstName.Text = Name;
                CusPersonalAddress.Text = dr.Cells[4].Value.ToString();  
                CusTelNUmber.Text = dr.Cells[5].Value.ToString();

               string CusPriceLevel = dr.Cells[6].Value.ToString();

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



                CusCreditLimit.Text = dr.Cells[7].Value.ToString();

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusCreditBalane = "SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = '" + dr.Cells[0].Value.ToString() + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView1.Rows.Clear();

                if (dr2.Read() == true)
                {
                    CusPreCreditLimit.Text = dr2[0].ToString();

                }


                //calculate the Current credit balence that Customer can get 
                CusBalCreditLimit.Text = Convert.ToString(Convert.ToDouble(CusCreditLimit.Text) - Convert.ToDouble(CusPreCreditLimit.Text));
              

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ClearTxt();
            RadioUncheck();
            if (checkBox1.Checked == true)
            {
                button4.Enabled = false;
                chbQuickCustomer.Checked = false;


                //credit paymet dissable
                PAyCredits.Enabled = false;
                PAyCredits.Text = "00.00";

                CusID.Text = "WK_Customer";
                CusName.Text = CusFirstName.Text;
                CusCreditLimit.Text = "0.00";
                CusPreCreditLimit.Text = "0.00";
                CusBalCreditLimit.Text = "0.00";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;

                //Rs. Lable Eneble fales
                label14.Enabled = false;
                label18.Enabled = false;
                label13.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = false;
                Dis04.Enabled = false;
                Dis02.Enabled = true;

                //pannel visible..........
                PnlAddress.Visible = true;
                CusFirstName.Focus();

            }

            if (checkBox1.Checked == false)
            {
                button4.Enabled = true;

                //credit payment active
                PAyCredits.Enabled = true;
                PAyCredits.Text = "00.00";

                CusID.Text = "";
                CusName.Text = "";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;

                //Rs. Lable Eneble
                label14.Enabled = true;
                label18.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = true;
                Dis04.Enabled = true;

                GetTotalAmount();
                listView2.Items.Clear();
                LblBillTotalAmount.Text = "00.00";
                txtSubTotal.Text = "00.00";
                amountCash.Text = "00.00";
                PayCash.Text = "00.00";
                // RadioBtn();
                Dis01.Text = "0.00";
                Dis02.Text = "0.00";
                Dis03.Text = "0.00";
                Dis04.Text = "0.00";

            }
        }

        public void insertToListTable()
        {
            try
            {
             SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();
            string selectItemOnly = "SELECT ItemID,ItemName,BarcodeNumber,SystemID,WarrentyPeriod,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03,OrderCost FROM CurrentStockItems WHERE BarcodeNumber='" + ItmBarCode.Text + "' AND ItmStatus='Stock'";
            SqlCommand cmd1 = new SqlCommand(selectItemOnly, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


            if (dr.Read() == true)
            {
                ListViewItem li;

                li = new ListViewItem(dr[0].ToString());

                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());
                li.SubItems.Add(dr[7].ToString());
                li.SubItems.Add(dr[8].ToString());

                //0=selling price, 1=dis01, 2=dis02, 3=dis03
                li.SubItems.Add("0");

                li.SubItems.Add(dr[5].ToString());

                li.SubItems.Add(dr[9].ToString());


                listView2.Items.Add(li);


                ItmBarCode.Text = "";
                ItmBarCode.Focus();

                //calculate the total amount
                GetTotalAmount();
            }

            else
            {
                MessageBox.Show("This Item is not in the Stock. Please contact your store keeper", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ItmBarCode.Text = "";
                ItmBarCode.Focus();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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
                LblBillTotalAmount.Text = txtSubTotal.Text = Convert.ToString(gtotal);

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

                if (listView2.Items.Count == 0)
                {
                    button5.Enabled = false;
                    txtTaxPer.Enabled = false;
                    button14.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error in get total amount. please contact you System Administrator","System Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        public void InsertInToListVeiw()
        {
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
                    //MessageBox.Show(listView2.Items[i].SubItems[3].Text);

                    if (listView2.Items[i].SubItems[3].Text == ItmBarCode.Text)
                    {
                      //  MessageBox.Show(listView2.Items[i].SubItems[3].Text);

                        MessageBox.Show("The Item alredy in the list.", "Error_1", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        
            //==============================================================================================================

        }

        //this method is not in use........................
        public void InsertInToListVeiw11111()
        {
            try
            {

            #region check the data is in list========================================================

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

            #region check the data is in the Database. but its not available to Sell ========================================================

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();
            string selectItemOnly = "SELECT BarcodeNumber,ItmStatus FROM CurrentStockItems WHERE BarcodeNumber='" + ItmBarCode.Text + "' AND ItmStatus !='Stock'";
            SqlCommand cmd1 = new SqlCommand(selectItemOnly, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


            if (dr.Read() == true)
            {
                string StatusOfItem = dr[1].ToString();
                string NoItm = dr[0].ToString();

                string Msg = "";


                if (StatusOfItem == "Sold")
                {
                    Msg = "The Item alredy sold. Please Check it again.";
                }

                if (StatusOfItem == "Return")
                {
                    Msg = "The Item is a return Item. Please do not sell it. contact your vendor.";
                }

                if (StatusOfItem == "Damaged")
                {
                    Msg = "The Item is Damaged. Please do not sell it. contact your vendor";
                }

                if (NoItm == "")
                {
                    Msg = "This Item is not in the Stock. Please contact your store keeper!!!";
                }


                MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ItmBarCode.Text = "";
                ItmBarCode.Focus();
                return;


            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
                dr.Close();
            }

            #endregion



               //Check wherther this item is unit Item or holesale Item ======================================================

                SqlConnection connn = new SqlConnection(IMS);
                connn.Open();
                string CkItemCode = "SELECT ItemBarCode FROM NewItemDetails WHERE ItemBarCode='" + ItmBarCode.Text + "'";
                SqlCommand cmd1n = new SqlCommand(CkItemCode, connn);
                SqlDataReader drr = cmd1n.ExecuteReader(CommandBehavior.CloseConnection);



                if (drr.Read() == true)
                {
                    #region if Items have with serials........................................

                    if (connn.State == ConnectionState.Open)
                    {
                        connn.Close();
                        drr.Close();
                    }

                    #region now Item has Unique Barcode. now ck item has serial numbers=========================================()

                    SqlConnection ConA = new SqlConnection(IMS);
                    ConA.Open();

                    string countNumbersofItems = @"SELECT BarcodeNumber FROM CurrentStockItems WHERE SystemID='" + ItmBarCode.Text + "'";

                    SqlCommand cmdA = new SqlCommand(countNumbersofItems, ConA);
                    SqlDataReader drA = cmdA.ExecuteReader(CommandBehavior.CloseConnection);

                    #endregion


                   
                    if (drA.Read() == true)
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
                                                        WHERE     (ItmStatus = 'Stock') AND (SystemID = '" + ItmBarCode.Text + "')";

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
                    //if Item Has Barcode but no Serial Numbers----------------------------------------------------------------
                    else
                    {
                        #region Item has barcode but No serials..................................................................

                        #region Select the Items in wholesales Items=========================================

                        SqlConnection ConC = new SqlConnection(IMS);
                        ConC.Open();

                        string WholeSalefItems = @"SELECT     NewItemDetails.ItmID, GRNWholesaleItems.BatchNumber, NewItemDetails.ItmName, GRNWholesaleItems.ItemWarrenty, GRNWholesaleItems.ItmDisc01, 
                                            GRNWholesaleItems.ItmDisc02, GRNWholesaleItems.ItmDisc03, GRNWholesaleItems.SellingPrice, GRNWholesaleItems.PerchPrice, GRNWholesaleItems.GrnAutiID, 
                                            GRNWholesaleItems.AvailbleItemCount,GRNWholesaleItems.BarCodeID
                                            FROM         NewItemDetails INNER JOIN
                                            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID
                                                WHERE    ( (NewItemDetails.ItmID = '" + ItmBarCode.Text + "') OR (GRNWholesaleItems.BarCodeID = '" + ItmBarCode.Text + "') )AND (GRNWholesaleItems.AvailbleItemCount>0)";

                        SqlCommand cmdC = new SqlCommand(WholeSalefItems, ConC);
                        SqlDataReader drC = cmdC.ExecuteReader(CommandBehavior.CloseConnection);

                        #endregion



                        //clear all items in the listview
                        ListMultiItems.Items.Clear();

                        PnlMultiItems.Visible = true;
                        // need to add item quntity here.
                        btnPnlItmFreQty.Enabled = true;
                        btnPnlItmQty.Enabled = true;


                        while (drC.Read() == true)
                        {

                            ListViewItem li;

                            li = new ListViewItem(drC[0].ToString());
                            //batch number
                            li.SubItems.Add(drC[1].ToString());
                            //ItemName
                            li.SubItems.Add(drC[2].ToString());
                            //ItemBarcode
                            li.SubItems.Add(drC[11].ToString());
                            //systemID
                            li.SubItems.Add("No_ID");
                            //Warrenty
                            li.SubItems.Add(drC[3].ToString());
                            //SellingPrice
                            li.SubItems.Add(drC[7].ToString());
                            //Quantitiy
                            li.SubItems.Add(btnPnlItmQty.Text);
                            //freeQty
                            li.SubItems.Add(btnPnlItmFreQty.Text);

                            //Totoal Amount
                            //
                            //
                            li.SubItems.Add("0");

                            //dis1,2,3
                            li.SubItems.Add(drC[4].ToString());
                            li.SubItems.Add(drC[5].ToString());
                            li.SubItems.Add(drC[6].ToString());

                            //PriceLevel..................
                            li.SubItems.Add("0");

                            //selling price Orijinal...............
                            li.SubItems.Add(drC[7].ToString());


                            //perchese price
                            li.SubItems.Add(drC[8].ToString());
                            //selling original...
                            li.SubItems.Add(drC[9].ToString());
                            //available count............
                            li.SubItems.Add(drC[10].ToString());


                            ListMultiItems.Items.Add(li);

                        }

                        #endregion
                    }

                    


                    //double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

                    //PayBalance.Text = Convert.ToString(Balance);


                    #endregion
                }

            //===============================================================================================================
            //===============================================================================================================

                else
                {


                    #region Select the Items in wholesales Items=========================================

                    SqlConnection ConC = new SqlConnection(IMS);
                    ConC.Open();

                    string WholeSalefItems = @"SELECT     NewItemDetails.ItmID, GRNWholesaleItems.BatchNumber, NewItemDetails.ItmName, GRNWholesaleItems.ItemWarrenty, GRNWholesaleItems.ItmDisc01, 
                                            GRNWholesaleItems.ItmDisc02, GRNWholesaleItems.ItmDisc03, GRNWholesaleItems.SellingPrice, GRNWholesaleItems.PerchPrice, GRNWholesaleItems.GrnAutiID, 
                                            GRNWholesaleItems.AvailbleItemCount,GRNWholesaleItems.BarCodeID
                                            FROM         NewItemDetails INNER JOIN
                                            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID
                                                WHERE    (NewItemDetails.ItmID = '" + ItmBarCode.Text + "') AND (GRNWholesaleItems.AvailbleItemCount>0)";

                    SqlCommand cmdC = new SqlCommand(WholeSalefItems, ConC);
                    SqlDataReader drC = cmdC.ExecuteReader(CommandBehavior.CloseConnection);

                    #endregion



                    //clear all items in the listview
                    ListMultiItems.Items.Clear();

                    PnlMultiItems.Visible = true;
                    // need to add item quntity here.
                    btnPnlItmFreQty.Enabled = true;
                    btnPnlItmQty.Enabled = true;


                    while (drC.Read() == true)
                    {

                        ListViewItem li;

                        li = new ListViewItem(drC[0].ToString());
                        //batch number
                        li.SubItems.Add(drC[1].ToString());
                        //ItemName
                        li.SubItems.Add(drC[2].ToString());
                        //SerialNumber
                        li.SubItems.Add("No_ID");
                        //BArCode
                        li.SubItems.Add(drC[11].ToString());
                        //Warrenty
                        li.SubItems.Add(drC[3].ToString());
                        //SellingPrice
                        li.SubItems.Add(drC[7].ToString());
                        //Quantitiy
                        li.SubItems.Add(btnPnlItmQty.Text);
                        //freeQty
                        li.SubItems.Add(btnPnlItmFreQty.Text);

                        //Totoal Amount
                        //
                        //
                        li.SubItems.Add("0");

                        //dis1,2,3
                        li.SubItems.Add(drC[4].ToString());
                        li.SubItems.Add(drC[5].ToString());
                        li.SubItems.Add(drC[6].ToString());

                        //PriceLevel..................
                        li.SubItems.Add("0");

                        //selling price Orijinal...............
                        li.SubItems.Add(drC[7].ToString());


                        //perchese price
                        li.SubItems.Add(drC[8].ToString());
                        //selling original...
                        li.SubItems.Add(drC[9].ToString());
                        //available count............
                        li.SubItems.Add(drC[10].ToString());


                        ListMultiItems.Items.Add(li);

                    }




         }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            InsertInToListVeiw();

            ListMultiItems.Focus();


        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            selectUserSetting();
        }

        public void RadioBtn()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //walking customer validation...................................................................

                if (listView2.SelectedItems.Count > 0)
                {
                    //if (checkBox1.Checked == false)
                    //{
                    //    radioButton1.Enabled = true;
                    //    radioButton2.Enabled = true;
                    //    radioButton3.Enabled = true;
                    //    radioButton4.Enabled = true;

                    //}
                    ////chbManualPrice.Enabled = true;
                    button6.Enabled = true;

                    //--------------------------------------------------------------------------------------------------------------


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

                    if (item.SubItems[13].Text == "4")
                    {
                        chbManualPrice.Checked = true;
                        txtmanualPrice.Text = item.SubItems[6].Text;
                    }

                }
                else
                {
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                    radioButton4.Enabled = false;
                    chbManualPrice.Enabled = false;
                    Dis01.Text = "00.00";
                    Dis02.Text = "00.00";
                    Dis03.Text = "00.00";
                    Dis04.Text = "00.00";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    button6.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            //change the price in the listview and change the item value in the listview.
           
            try
            {

                if (listView2.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView2.SelectedItems[0];

                    if (radioButton1.Checked == true)
                    {
                        item.SubItems[6].Text = Dis01.Text;

                       item.SubItems[9].Text= Convert.ToString(Convert.ToDouble(Dis01.Text) * Convert.ToDouble(item.SubItems[7].Text));
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

                    if(chbManualPrice.Checked==true)
                    {
                        item.SubItems[6].Text = txtmanualPrice.Text;
                        item.SubItems[9].Text = Convert.ToString(Convert.ToDouble(txtmanualPrice.Text) * Convert.ToDouble(item.SubItems[7].Text));
                        item.SubItems[13].Text = "4";
                    }

                    //if ((Double.Parse( item.SubItems[15].Text)) > (Double.Parse (txtmanualPrice.Text)))
                    //{
                    //    MessageBox.Show("Manual price is less than Purchase price,Please Change Manual Price");
                    //    return;
                    //}
                    
                    MessageBox.Show("Price has been Changed", "Price Change", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtmanualPrice.Text = "0.00";
                    chbManualPrice.Checked = false;

                    GetTotalAmount();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView2.SelectedItems[0].Remove();
            GetTotalAmount();
            RadioBtn();
            Dis01.Text = "0.00";
            Dis02.Text = "0.00";
            Dis03.Text = "0.00";
            Dis04.Text = "0.00";


            
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
            //int val1 = 0;
            //int val2 = 0;
            //int.TryParse(txtTotal.Text, out val1);
            //int.TryParse(txtTotalPayment.Text, out val2);
            //int I = (val1 - val2);
            //txtPaymentDue.Text = I.ToString();

            PayAmountInvoiced.Text = txtTotalPayment.Text;
        }

        private void CkCkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CkCkBox.Checked == true)
            {
                PayCheck.Enabled = true;
                groupBox8.Enabled = true;

                Select_Bank();

                PayCheck.Focus();
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

        public void PayMethod()
        {
            double Cash = Convert.ToDouble(PayCash.Text);
            double Check = Convert.ToDouble(PayCheck.Text);
            double CreCard = Convert.ToDouble(PayCrditCard.Text);
            double Debit = Convert.ToDouble(PayDebitCard.Text);
           // double Credits = Convert.ToDouble(PAyCredits.Text);

            //double GndTotal = Cash + Check + CreCard + Debit + Credits;

            double GndTotal = Cash + Check + CreCard + Debit;
            
            PayAmount.Text = Convert.ToString(GndTotal);

        }

        private void PayCash_TextChanged(object sender, EventArgs e)
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void PayCheck_TextChanged(object sender, EventArgs e)
        {
            if (PayCheck.Text == "")
            {
                return;
            }
            
                PayMethod();

                double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text)-Convert.ToDouble(PayPreInvoived.Text);

                if (Balance < 0)
                {
                    MessageBox.Show("your amount is greater than the Total. please try again ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PayCheck.Text = "00.00";
                    return;
                }

                PayBalance.Text =  Convert.ToString(Balance);
            
        }

        private void PayCrditCard_TextChanged(object sender, EventArgs e)
        {
            try
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
            //    return;
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
                    InvoiceID.Text = "INV1001";
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

                        InvoiceID.Text = "INV" + no;
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

        public void clearTxt()
        {
            InvoiceID.Text = "";
            CusName.Text = "";
            CusID.Text = "";
            CusCreditLimit.Text = "00.00";
            CusPreCreditLimit.Text = "00.00";
            CusBalCreditLimit.Text = "00.00";

            checkBox1.Checked = false;

            CusFirstName.Text = "";
            CusPersonalAddress.Text = "";
            CusTelNUmber.Text = "";

            LblBillTotalAmount.Text = "000.00";

            listView2.Items.Clear();

            PayCash.Text = "00.00";
            PayCheck.Text = "00.00";
            PayCrditCard.Text = "00.00";
            PayDebitCard.Text = "00.00";
            PAyCredits.Text = "00.00";

            CkCkBox.Checked = false;
            groupBox8.Enabled = false;
            CkBank.Text="00.00";
            CkNumber.Text = "00.00";

            PayAmount.Text = "00.00";
            PayPreInvoived.Text = "00.00";
            PayAmountInvoiced.Text = "00.00";
            PayBalance.Text = "00.00";

            Ckdate.Text = System.DateTime.Now.ToString();

            InvoiceRemark.Text = "";

            txtSubTotal.Text= "00.00";
            txtTaxPer.Text = "00";
            txtTaxAmt.Text = "0.00";
            txtTotal.Text = "0.00";
            txtTotalPayment.Text = "0.00";
            txtPaymentDue.Text = "0.00";
            txtDiscount.Text = "0.00";
            btxtdiscValu.Text = "0.00";
            button5.Enabled = false;

            amountCash.Text = "0.00";
            Totalcash.Text = "0.00";
            Balance.Text = "0.00";

            button14.Enabled = false;

        }

        


        private void button14_Click(object sender, EventArgs e)
        {
            getCreateInvoiceCode();

            PassInvoiceNumber.Text = InvoiceID.Text;
            
            if (CusFirstName.Text == "" || CusPersonalAddress.Text == "" || CusTelNUmber.Text == "")
            {
                MessageBox.Show("Please complete customer Details", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PnlAddress.Visible = true;
                CusFirstName.Focus();
                return;
            }

            if (CkCkBox.Checked == true)
            {
                if (CkNumber.Text == "" || CkBank.Text == "" || PayCheck.Text=="00.00")
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

            try
            {
                DialogResult result = MessageBox.Show("Are you whether you need to complete the invoice?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                string inID = "";

                if (result == DialogResult.Yes)
                {

                    getCreateInvoiceCode();

                    #region add invoice selling details including customer details----------------------------------------------------------------------------

                    string CusStatus = "";

                    if (checkBox1.Checked == true)
                    {
                        CusStatus = "Walking_Customer";
                    }

                    if (chbQuickCustomer.Checked == true)
                    {
                        CusStatus = "Quick_Customer";
                    }


                    if (CusID.Text != "")
                    {
                        CusStatus = CusID.Text;
                    }

                    //CusStatus = Sold,Cancel 
                    SqlConnection con4 = new SqlConnection(IMS);
                    con4.Open();
                    string InsertInvoiceDetails = "INSERT INTO SoldInvoiceDetails(InvoiceNo,InvoiceStatus,CusStatus,CusFirstName,CusPersonalAddress,CusTelNUmber,CreatedBy,InvoiceRemark,Return_Invoice_Or_Original,Return_Invoice_Num) VALUES('" + InvoiceID.Text + "','Sold','" + CusStatus + "','" + CusFirstName.Text + "','" + CusPersonalAddress.Text + "','" + CusTelNUmber.Text + "','" + LgUser.Text + "','" + InvoiceRemark.Text + "','Original','No')";
                    SqlCommand cmd4 = new SqlCommand(InsertInvoiceDetails, con4);
                    cmd4.ExecuteNonQuery();


                    if (con4.State == ConnectionState.Open)
                    {
                        cmd4.Dispose();
                        con4.Close();
                    }
                    //------------------------------------------------------------------------------------------------------------------------------------
                    #endregion



                    inID = InvoiceID.Text;

                    int i;
                    string x = "";

                    #region insert data in list view to DB========================================================

                    for (i = 0; i <= listView2.Items.Count - 1; i++)
                    {
                        #region chage item status to Sold---------------------------------------------------------------------------------------------
                        if (listView2.Items[i].SubItems[3].Text != "No_ID")
                        {
                            SqlConnection con = new SqlConnection(IMS);
                            con.Open();
                            string UpateStockStock = "UPDATE CurrentStockItems SET ItmStatus='Sold' WHERE BarcodeNumber='" + listView2.Items[i].SubItems[3].Text + "' AND SystemID='" + listView2.Items[i].SubItems[4].Text + "'";
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

                        if (listView2.Items[i].SubItems[3].Text == "No_ID")
                        {
                            //MessageBox.Show("Read wholesale");

                            SqlConnection cony = new SqlConnection(IMS);
                            cony.Open();
                            string SelectTotalWholesaleStockCount = "SELECT AvailbleItemCount FROM GRNWholesaleItems WHERE ItemID= '" + listView2.Items[i].SubItems[0].Text + "' AND BatchNumber='" + listView2.Items[i].SubItems[1].Text + "'";

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

                            double FinalCount = StoskCount - (Convert.ToDouble(listView2.Items[i].SubItems[7].Text) + Convert.ToDouble(listView2.Items[i].SubItems[8].Text));



                            SqlConnection conx = new SqlConnection(IMS);
                            conx.Open();
                            string UpateStockStockWholesale = "UPDATE GRNWholesaleItems SET AvailbleItemCount='" + Convert.ToString(FinalCount) + "' WHERE ItemID='" + listView2.Items[i].SubItems[0].Text + "' AND BatchNumber='" + listView2.Items[i].SubItems[1].Text + "'";
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
                        string SelectTotalStockCount = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID= '" + listView2.Items[i].SubItems[0].Text + "'";
                        SqlCommand cmd1 = new SqlCommand(SelectTotalStockCount, con1);
                        SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dr1.Read())
                        {
                            x = dr1[0].ToString();
                        }
                        double z = Convert.ToDouble(x);

                        if (con1.State == ConnectionState.Open)
                        {
                            con1.Close();
                            dr1.Close();
                        }
                        double y = z - (Convert.ToDouble(listView2.Items[i].SubItems[7].Text) + Convert.ToDouble(listView2.Items[i].SubItems[8].Text));

                        SqlConnection con2 = new SqlConnection(IMS);
                        con2.Open();
                        string UpateStockCount = "UPDATE [CurrentStock] SET AvailableStockCount=" + y + " WHERE ItemID='" + listView2.Items[i].SubItems[0].Text + "'";
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

                        cmd3.Parameters.AddWithValue("InvoiceID", InvoiceID.Text);
                        cmd3.Parameters.AddWithValue("ItemID", listView2.Items[i].SubItems[0].Text);
                        cmd3.Parameters.AddWithValue("ItemName", listView2.Items[i].SubItems[2].Text);
                        cmd3.Parameters.AddWithValue("ItemWarrenty", listView2.Items[i].SubItems[5].Text);

                        //Barcode Item is = to serial number
                        cmd3.Parameters.AddWithValue("BarcodeID", listView2.Items[i].SubItems[3].Text);
                        //systemID=Barcode
                        cmd3.Parameters.AddWithValue("SystemID", listView2.Items[i].SubItems[4].Text);

                        cmd3.Parameters.AddWithValue("BuyPrice", listView2.Items[i].SubItems[15].Text);
                        cmd3.Parameters.AddWithValue("SoldPrice", listView2.Items[i].SubItems[6].Text);
                        cmd3.Parameters.AddWithValue("ItmQuantity", listView2.Items[i].SubItems[7].Text);
                        cmd3.Parameters.AddWithValue("FreeQuantity", listView2.Items[i].SubItems[8].Text);
                        cmd3.Parameters.AddWithValue("BatchID", listView2.Items[i].SubItems[1].Text);

                       

                        cmd3.ExecuteNonQuery();

                        if (con3.State == ConnectionState.Open)
                        {
                            cmd3.Dispose();
                            // dr.Close();
                            con3.Close();
                        }

                        #endregion

                    }

                
                  
                    #region check whether discount checkbox is checkek or not---------------------------

                    //string discount="";

                    //if (chkDisc.Checked == true)
                    //{
                    //    discount = "T";
                    //}

                    //if (chkDisc.Checked == true)
                    //{
                    //    discount = "F";
                    //}

                    #endregion

                    //discount = discount + txtDiscount.Text;

                    #region insert Invoice Paymet Details--------------------------------------------------------------------------------------------------------

                    SqlConnection con5 = new SqlConnection(IMS);
                    con5.Open();
                    string InsertInvoicePaymetDetails = "INSERT INTO InvoicePaymentDetails (InvoiceID,SubTotal,VATpresentage,GrandTotal,PayCash,PayCheck,PayCrditCard,PayDebitCard,PAyCredits,PayBalance,InvoiceDate,InvoiceDiscount) VALUES('" + InvoiceID.Text + "','" + txtSubTotal.Text + "','" + txtTaxPer.Text + "','" + txtTotal.Text + "','" + PayCash.Text + "','" + PayCheck.Text + "','" + PayCrditCard.Text + "','" + PayDebitCard.Text + "','" + PayBalance.Text + "','" + PayBalance.Text + "','" + System.DateTime.Now.ToString() + "','" + btxtdiscValu.Text + "')";
                    SqlCommand cmd5 = new SqlCommand(InsertInvoicePaymetDetails, con5);
                    cmd5.ExecuteNonQuery();


                    if (con5.State == ConnectionState.Open)
                    {
                        cmd5.Dispose();
                        con5.Close();
                    }

                    #endregion

                    #region insert Chq Details===============================================================================================

                    if (CkCkBox.Checked == true)
                    {
                        SqlConnection con6 = new SqlConnection(IMS);
                        con6.Open();
                        string InsertCheckPaymetDetails = "INSERT INTO InvoiceCheckDetails(InvoiceID,CkStatus,CkNumber,Bank,Amount,CurrentDate,MentionDate) VALUES('" + InvoiceID.Text + "','Active','" + CkNumber.Text + "','" + label58.Text + "','" + PayCheck.Text + "','" + System.DateTime.Now.ToString() + "','" + Ckdate.Text + "')";
                        SqlCommand cmd6 = new SqlCommand(InsertCheckPaymetDetails, con6);
                        cmd6.ExecuteNonQuery();


                        if (con6.State == ConnectionState.Open)
                        {
                            cmd6.Dispose();
                            con6.Close();
                        }
                    }

                    #endregion



                    if (CusID.Text != "WK_Customer" && CusID.Text != "Quick_Customer")
                    {
                        //|| CusID.Text != "Quick_Customer"
                       // MessageBox.Show("start");


                        Customer_Credit_Balance_Update();

                        #region Customer Credit Balance Update===========================================================================
                        //select Customer

                //        double TotCusCredits=0;

                //SqlConnection con6 = new SqlConnection(IMS);
                //con6.Open();
                //string selectCreditCustomer = "SELECT [CreditBalance] FROM [RegCusCredBalance] WHERE [CusID]='" + CusID.Text + "'";
                //SqlCommand cmd6 = new SqlCommand(selectCreditCustomer, con6);
                //SqlDataReader dr6 = cmd6.ExecuteReader(CommandBehavior.CloseConnection);

                //if (dr6.Read() == true)
                //{
                //    TotCusCredits =Convert.ToDouble(dr6[0].ToString());
                //}
                //if (con6.State == ConnectionState.Open)
                //{
                //    cmd6.Dispose();
                //    con6.Close();
                //    dr6.Close();
                //}

                    #endregion

                        #region update customer credits========================================================================================

                //double finalCreditUpdate = TotCusCredits + Convert.ToDouble(PAyCredits.Text);

                //SqlConnection con7 = new SqlConnection(IMS);
                //con7.Open();
                //string InsertItemsToStockCount = "UPDATE RegCusCredBalance SET CreditBalance =" + finalCreditUpdate + " WHERE CusID ='" + CusID.Text + "'";
                //SqlCommand cmd7 = new SqlCommand(InsertItemsToStockCount, con7);
                //cmd7.ExecuteNonQuery();

                #endregion

                    }

                    


                    MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    BillForm billfm = new BillForm();
                    billfm.InvoiveNumberToPrint = inID;
                        //PassInvoiceNumber.Text;
                    billfm.PrintCopyDetails = "Original Copy";
                    billfm.Visible = true;

                    //clearTxt();
                    clearTxt();
                    chbQuickCustomer.Checked = false;
                    getCreateInvoiceCode();

                    
                }
            //------------------------------------------------------------------------------------------------------------------------------

            //

                

                //----------------------------------------------------------------------------------------------------------------------------------------


                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }



            clearTxt();
            RadioUncheck();
            if (CusID.Text == "")
            {
                button16.Enabled = false;
                ItmBarCode.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                Dis01.Text = "0.00";
                Dis02.Text = "0.00";
                Dis03.Text = "0.00";
                Dis04.Text = "0.00";
                txtmanualPrice.Enabled = false;
                chbManualPrice.Checked = false;
                chbManualPrice.Enabled = false;
                button6.Enabled = false;
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

        private void Totalcash_TextChanged(object sender, EventArgs e)
        {
            if (Totalcash.Text == "")
            {
                //Totalcash.Text = "0";
                return;
            }

            double x = Convert.ToDouble(Totalcash.Text) - Convert.ToDouble(amountCash.Text);

            Balance.Text = x.ToString();
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

        private void CusID_TextChanged(object sender, EventArgs e)
        {
            if (CusID.Text != "")
            {
                button16.Enabled = true;
                ItmBarCode.Enabled = true;
            }
        }

        private void CusTelNUmber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one '-' point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
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

            //if(chbQuickCustomer.Checked==true)
            //{
            //    PayCash.Text = txtSubTotal.Text;
            //    amountCash.Text=txtSubTotal.Text;
            //}
        }

        private void PayAmountInvoiced_Click(object sender, EventArgs e)
        {
            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);
        }

        private void LblBillTotalAmount_Click(object sender, EventArgs e)
        {
            //PayMethod();
            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);
            PayBalance.Text = Convert.ToString(Balance);

            PayMethod();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ItmBarCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void PlnHide_Click(object sender, EventArgs e)
        {
            PnlMultiItems.Visible = false;
            ItmBarCode.Text = "";
            ItmBarCode.Focus();
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
                    //if (ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[0].Text != ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[3].Text)
                    //{
                    //    if invoicelist item is unique item
                    //    if (listView2.Items[i].SubItems[0].Text != listView2.Items[i].SubItems[3].Text)
                    //    {
                    //        if equal items
                    //        if (listView2.Items[i].SubItems[16].Text == ListMultiItems.SelectedItems[Convert.ToInt32(LvIndex.Text)].SubItems[16].Text)
                    //        {
                    //            MessageBox.Show("The Item alredy in the list.", "Error_2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            return;
                    //        }
                    //    }
                    //}

                    //
                    if (ListMultiItems.Items[Convert.ToInt32(LvIndex.Text)].SubItems[0].Text == listView2.Items[i].SubItems[0].Text)//if item code ame
                    {
                        if (ListMultiItems.SelectedItems[0].SubItems[16].Text == "1")//if it is serial
                        {
                            if (ListMultiItems.SelectedItems[Convert.ToInt32(LvIndex.Text)].SubItems[3].Text == listView2.Items[i].SubItems[3].Text)//if serial same
                            {
                                MessageBox.Show("The Item alredy in the list.", "Error_21", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                PnlMultiItems.Visible = false;
                                ItmBarCode.Focus();
                                return;
                            }
                        }

                        if (ListMultiItems.SelectedItems[0].SubItems[16].Text == "2")//if item is wholesale
                        {
                            if (ListMultiItems.SelectedItems[Convert.ToInt32(LvIndex.Text)].SubItems[1].Text == listView2.Items[i].SubItems[1].Text)//if batch same
                            {
                                MessageBox.Show("The Item alredy in the list.", "Error_31", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                PnlMultiItems.Visible = false;
                                ItmBarCode.Focus();
                                return;
                            }
                        }
                    }

                }
                #endregion


                #region Item stock count and request count ck=======================================================

                double totalItemcount=Convert.ToDouble(btnPnlItmQty.Text)+Convert.ToDouble(btnPnlItmFreQty.Text);
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

        private void ListMultiItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           // selectUserSetting();
            AddMultiItemToInvoiceList();

            GetTotalAmount();
        }

        private void btnPnlItmQty_TextChanged(object sender, EventArgs e)
        {
           // ListMultiItems.SelectedItems[0].SubItems[9].Text = Convert.ToString(Convert.ToDouble(ListMultiItems.SelectedItems[0].SubItems[6].Text) * Convert.ToDouble(ListMultiItems.SelectedItems[0].SubItems[7].Text));

          
        }

        private void btnPnlItmFreQty_TextChanged(object sender, EventArgs e)
        {
            //ListMultiItems.SelectedItems[0].SubItems[8].Text = btnPnlItmFreQty.Text; 

          
        }

        private void ListMultiItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //if (e.IsSelected)
            //    e.Item.Selected = false;
        }

        private void ListMultiItems_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void ListMultiItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a=ListMultiItems.FocusedItem.Index;

            ListMultiItems.Items[a].BackColor = Color.AliceBlue;

            LvIndex.Text = Convert.ToString(a);
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {

            AddMultiItemToInvoiceList();


            GetTotalAmount();

            ListMultiItems.Items.Clear();
            
          
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

        private void btnPnlItmQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnPnlItmFreQty.Focus();
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

        private void chkDisc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "")
            {
                //txtDiscountPresant.Text = "0.0";
                return;
            }
            Double disc = Double.Parse(txtDiscount.Text);
            Double sub = Double.Parse(txtSubTotal.Text);
            Double add = (disc * sub) / 100;
            btxtdiscValu.Text = add.ToString();
           // MessageBox.Show(add.ToString());
        }

        private void btxtdiscValu_TextChanged(object sender, EventArgs e)
        {

            if (btxtdiscValu.Text == "")
            {
                // btxtdiscValu.Text = "0.0";
                return;

            }
            double sa = double.Parse(btxtdiscValu.Text);
            Double sub = Double.Parse(txtSubTotal.Text);
            double add = sa / sub * 100;
            btxtdiscValu.Text = add.ToString();
            double min = double.Parse(txtSubTotal.Text) - double.Parse(btxtdiscValu.Text);
            txtTotal.Text = min.ToString();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotalPayment.Text = txtTotal.Text;
        }

        private void btxtdiscValu_TextChanged_1(object sender, EventArgs e)
        {
            //if (btxtdiscValu.Text == "")
            //{
            //    // btxtdiscValu.Text = "0.0";
            //    return;

            //}

            //double sa = double.Parse(btxtdiscValu.Text);
            //Double sub = Double.Parse(txtSubTotal.Text);
            //double add = sa / sub * 100;
            //btxtdiscValu.Text = add.ToString();
            //double min = double.Parse(txtSubTotal.Text) - double.Parse(btxtdiscValu.Text);
            //txtTotal.Text = min.ToString();

            if (btxtdiscValu.Text == "")
            {
                return;
            }

            double subtot=Convert.ToDouble(txtSubTotal.Text);
            double discountValv = Convert.ToDouble(btxtdiscValu.Text);
            //double discount = Convert.ToDouble(txtDiscount.Text);

            double dispre=discountValv*100/subtot;

            txtDiscount.Text = Convert.ToString(dispre);

            txtTotal.Text = Convert.ToString(subtot-discountValv);




        }

        private void PayAmountInvoiced_TextChanged(object sender, EventArgs e)
        {
            double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            PayBalance.Text = Convert.ToString(Balance);

            //********************************************************

            if (chbQuickCustomer.Checked == true)
            {
                PayCash.Text = amountCash.Text = PayAmountInvoiced.Text;

            }
        }

        private void btxtdiscValu_Leave(object sender, EventArgs e)
        {
            if (btxtdiscValu.Text == "")
            {
                btxtdiscValu.Text = "0.00";
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (txtDiscount.Text=="")
            {
                txtDiscount.Text = "0.00";
            }
        }

        private void PayCash_Leave(object sender, EventArgs e)
        {
            if (PayCash.Text == "")
            {
                PayCash.Text = "00.00";

            }
        }

        private void PayCheck_Leave(object sender, EventArgs e)
        {
            if (PayCheck.Text == "")
            {
                PayCheck.Text = "00.00";
            }
        }

        private void PayCrditCard_Leave(object sender, EventArgs e)
        {
            if (PayCrditCard.Text == "")
            {
                PayCrditCard.Text = "00.00";
            }
        }

        private void PayDebitCard_Leave(object sender, EventArgs e)
        {
            if (PayDebitCard.Text == "")
            {
                PayDebitCard.Text = "00.00";
            }
        }

        private void PAyCredits_Leave(object sender, EventArgs e)
        {
            if (PAyCredits.Text == "")
            {
                PAyCredits.Text = "00.00";
            }
        }

        private void PayAmount_TextChanged(object sender, EventArgs e)
        {
           // txtTotalPayment.Text = PayAmount.Text;
        }

        private void PayBalance_TextChanged(object sender, EventArgs e)
        {
            //txtPaymentDue.Text = PayBalance.Text;
        }

        private void btnPnlItmQty_Leave(object sender, EventArgs e)
        {
            if (btnPnlItmQty.Text == "")
            {
                btnPnlItmQty.Text = "0";

            }
        }

        private void btnPnlItmFreQty_Leave(object sender, EventArgs e)
        {
            if (btnPnlItmFreQty.Text == "")
            {
                btnPnlItmFreQty.Text = "0";

            }
        }

        private void label42_Click(object sender, EventArgs e)
        {
          
            

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                #region Bank create new.......................................

                SqlConnection cnne = new SqlConnection(IMS);
                cnne.Open();
                String NewBank = "insert into Bank_Category(BankID,BankName) values('" + label54.Text + "','" + textBox1.Text + "')";
                SqlCommand cmme = new SqlCommand(NewBank, cnne);
                cmme.ExecuteNonQuery();

                MessageBox.Show(" Insert New Bank Successfull.....!");

                panel6.Visible = false;
                textBox1.Text = "";
                Select_Bank();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void label42_MouseClick(object sender, MouseEventArgs e)
        {
            getCreatebANKcode();
            panel6.Visible = true;
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
                    label58.Text = (dr[1].ToString());
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

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void chbManualPrice_CheckedChanged(object sender, EventArgs e)
        {
            if(chbManualPrice.Checked==true)
            {
                txtmanualPrice.Enabled = true;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }
            if (chbManualPrice.Checked == false)
            {
                txtmanualPrice.Text="0.00";
                txtmanualPrice.Enabled = false;
            }
        }

        private void txtmanualPrice_Leave(object sender, EventArgs e)
        {
            if(txtmanualPrice.Text=="")
            {
                txtmanualPrice.Text = "0.00";
            }
        }

        private void txtmanualPrice_TextChanged(object sender, EventArgs e)
        {
            if(txtmanualPrice.Text=="0.00")
            {
                return;
            }
        }

        private void radioButton1_TextChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtmanualPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                button6.Focus();
            }
        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                chbManualPrice.Checked = false;
                txtmanualPrice.Text = "0.00";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                chbManualPrice.Checked = false;
                txtmanualPrice.Text = "0.00";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                chbManualPrice.Checked = false;
                txtmanualPrice.Text = "0.00";
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                chbManualPrice.Checked = false;
                txtmanualPrice.Text = "0.00";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Totalcash_Leave(object sender, EventArgs e)
        {
            if (Totalcash.Text == "")
            {
                Totalcash.Text = "0";
                
            }
        }

        private void CusFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusPersonalAddress.Focus();
            }
        }

        private void CusPersonalAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusTelNUmber.Focus();
            }
        }

        private void CusTelNUmber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button8.Focus();
            }
        }

        private void button8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PnlAddress.Visible = false;
                 

                if (checkBox1.Checked == true)
                {
                    CusName.Text = CusFirstName.Text;
                }
            }
        }

        private void PayCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PAyCredits.Focus();
            }
        }

        private void PayDebitCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button13.Focus();
            }
        }

        private void PayCrditCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PayDebitCard.Focus();
            }
        }

        private void button13_KeyDown(object sender, KeyEventArgs e)
        {
            PnlAddress.Visible = false;
            //PnlNote.Visible = false;
            PnlPaymentMethod.Visible = false;

            amountCash.Text = PayCash.Text;

            Totalcash.Focus();
        }

        private void PayCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CkBank.Focus();
            }
        }

        private void Totalcash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button14.Focus();
            }
        }

        private void label64_Click(object sender, EventArgs e)
        {
            CustomerReg cusreg = new CustomerReg();
            cusreg.Visible = true;
        }

        private void chbQuickCustomer_CheckedChanged(object sender, EventArgs e)
        {
            ClearTxt();
           

            if (chbQuickCustomer.Checked == true)
            {
                #region Quick Customer true.........................................

                
               // button4.Enabled = false;

                PAyCredits.Enabled = false;
                PAyCredits.Text = "00.00";
                PnlAddress.Visible = false;
                

                button4.Enabled = false;
                CusID.Text = "Quick_Customer";
                label64.Enabled = false;
                CusFirstName.Text = "Quick_Customer";
                CusPersonalAddress.Text = "Quick_Customer";
                CusTelNUmber.Text = "Quick_Customer";
                checkBox1.Checked = false;
                CusName.Text = "Quick_Customer";
                CusCreditLimit.Text = "0.00";
                CusPreCreditLimit.Text = "0.00";
                CusBalCreditLimit.Text = "0.00";




                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;

                //Rs. Lable Eneble fales
                label14.Enabled = false;
                label18.Enabled = false;
                label13.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = false;
                Dis04.Enabled = false;
                Dis02.Enabled = true;


                PayCash.Enabled = false;
                ItmBarCode.Focus();
                #endregion

            }
            if(chbQuickCustomer.Checked==false)
            {
                #region Quick Customer False...........................
                ClearTxt();

                label64.Enabled = true;
                CusID.Text = "";
                CusFirstName.Text = "";
                CusPersonalAddress.Text = "";
                CusTelNUmber.Text = "";


                button4.Enabled = true;

                //credit payment active
                PAyCredits.Enabled = true;
                PAyCredits.Text = "00.00";

                CusID.Text = "";
                CusName.Text = "";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;

                //Rs. Lable Eneble
                label14.Enabled = true;
                label18.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = true;
                Dis04.Enabled = true;

                GetTotalAmount();
                listView2.Items.Clear();
                LblBillTotalAmount.Text = "00.00";
                txtSubTotal.Text = "00.00";
                amountCash.Text = "00.00";
                PayCash.Text = "00.00";
               // RadioBtn();
                Dis01.Text = "0.00";
                Dis02.Text = "0.00";
                Dis03.Text = "0.00";
                Dis04.Text = "0.00";
                txtTotal.Text = "00.00";

                ItmBarCode.Enabled = false;
                button16.Enabled = false;

                PayCash.Enabled = true;
                #endregion

            }

        }

        private void LblBillTotalAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chbQuickCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                ItmBarCode.Focus();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            CusFirstName.Clear();
            CusPersonalAddress.Clear();
            CusTelNUmber.Clear();
            PnlAddress.Visible = true;
        }

        private void chbQuickCustomer_Click(object sender, EventArgs e)
        {
            ClearTxt();


            if (chbQuickCustomer.Checked == true)
            {
                #region Quick Customer true.........................................


                //button4.Enabled = false;

                PAyCredits.Enabled = false;
                PAyCredits.Text = "00.00";
                PnlAddress.Visible = false;


                button4.Enabled = false;
                CusID.Text = "Quick_Customer";
                label64.Enabled = false;
                CusFirstName.Text = "Quick_Customer";
                CusPersonalAddress.Text = "Quick_Customer";
                CusTelNUmber.Text = "Quick_Customer";
                checkBox1.Checked = false;
                CusName.Text = "Quick_Customer";
                CusCreditLimit.Text = "0.00";
                CusPreCreditLimit.Text = "0.00";
                CusBalCreditLimit.Text = "0.00";




                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;

                //Rs. Lable Eneble fales
                label14.Enabled = false;
                label18.Enabled = false;
                label13.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = false;
                Dis04.Enabled = false;
                Dis02.Enabled = true;


                PayCash.Enabled = false;
                ItmBarCode.Focus();
                #endregion

            }
            if (chbQuickCustomer.Checked == false)
            {
                #region Quick Customer False...........................
                ClearTxt();

                label64.Enabled = true;
                CusID.Text = "";
                CusFirstName.Text = "";
                CusPersonalAddress.Text = "";
                CusTelNUmber.Text = "";


                button4.Enabled = true;

                //credit payment active
                PAyCredits.Enabled = true;
                PAyCredits.Text = "00.00";

                CusID.Text = "";
                CusName.Text = "";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;

                //Rs. Lable Eneble
                label14.Enabled = true;
                label18.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = true;
                Dis04.Enabled = true;

                GetTotalAmount();
                listView2.Items.Clear();
                LblBillTotalAmount.Text = "00.00";
                txtSubTotal.Text = "00.00";
                amountCash.Text = "00.00";
                PayCash.Text = "00.00";
                // RadioBtn();
                Dis01.Text = "0.00";
                Dis02.Text = "0.00";
                Dis03.Text = "0.00";
                Dis04.Text = "0.00";
                txtTotal.Text = "00.00";

                ItmBarCode.Enabled = false;
                button16.Enabled = false;

                PayCash.Enabled = true;
                #endregion

            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            ClearTxt();
            RadioUncheck();
            if (checkBox1.Checked == true)
            {
                button4.Enabled = false;
                chbQuickCustomer.Checked = false;


                //credit paymet dissable
                PAyCredits.Enabled = false;
                PAyCredits.Text = "00.00";

                CusID.Text = "WK_Customer";
                CusName.Text = CusFirstName.Text;
                CusCreditLimit.Text = "0.00";
                CusPreCreditLimit.Text = "0.00";
                CusBalCreditLimit.Text = "0.00";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;

                //Rs. Lable Eneble fales
                label14.Enabled = false;
                label18.Enabled = false;
                label13.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = false;
                Dis04.Enabled = false;
                Dis02.Enabled = true;

                //pannel visible..........
                PnlAddress.Visible = true;
                CusFirstName.Focus();

            }

            if (checkBox1.Checked == false)
            {
                button4.Enabled = true;
                PnlAddress.Visible = false;

                //credit payment active
                PAyCredits.Enabled = true;
                PAyCredits.Text = "00.00";

                CusID.Text = "";
                CusName.Text = "";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;

                //Rs. Lable Eneble
                label14.Enabled = true;
                label18.Enabled = true;

                //Price Lavel eneble 
                Dis03.Enabled = true;
                Dis04.Enabled = true;

                GetTotalAmount();
                listView2.Items.Clear();
                LblBillTotalAmount.Text = "00.00";
                txtSubTotal.Text = "00.00";
                amountCash.Text = "00.00";
                PayCash.Text = "00.00";
                // RadioBtn();
                Dis01.Text = "0.00";
                Dis02.Text = "0.00";
                Dis03.Text = "0.00";
                Dis04.Text = "0.00";

            }
        }

       

       
    }
}
