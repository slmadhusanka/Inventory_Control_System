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
using System.IO;
using System.Security.Cryptography; // auto generate number reference

namespace Inventory_Control_System
{
    public partial class ReceiveOrders : Form
    {
        public ReceiveOrders(string loginUser)
        {
            InitializeComponent();
            SystemUser.Text = loginUser;
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        private void button4_Click(object sender, EventArgs e)
        {

            this.Close();

           
        }

        public void getCreateStockCode()
        {
            #region New Stock Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                //  string sql = "select OrderID from CurrentStockItems";
                string sql = "select GRNID from GRNInvoicePaymentDetails";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    ODSNumber.Text = "ODS1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
                    string sql1 = " SELECT TOP 1 GRNID FROM GRNInvoicePaymentDetails order by GRNID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        ODSNumber.Text = "ODS" + no;

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

        public void Debit_Credit_Balance()
        {

            #region Debit_Credit_Balance...........................................

            try
            {
                double LastBalance = 0;
                double New_Bal = 0;
                double Debit_Balance = 0;

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance,Debit_Balance FROM vender_Payment WHERE (VenderID = '" + VenID.Text + "') ORDER BY Date DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                if (dr7.Read())
                {
                    LastBalance = Convert.ToDouble(dr7[0].ToString());
                    Debit_Balance = Convert.ToDouble(dr7[1].ToString());
                }
                cmd1.Dispose();
                dr7.Close();
                Conn.Close();

                //======================================

                #region New Vendor PAyments add

                //balance calc .this is credit balace-----------------------------
                New_Bal = LastBalance - Convert.ToDouble(lblNetAmount.Text);

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Vend_DebitPaymet = @"INSERT INTO vender_Payment(VenderID, DocNumber, Credit_Amount, Debit_Amount, Debit_Balance, Balance, Date) 
                                                VALUES  ('" + VenID.Text + "','" + ODSNumber.Text + "','" + lblNetAmount.Text + "','0','" + Debit_Balance + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

                SqlCommand cmd21 = new SqlCommand(Vend_DebitPaymet, con1);
                cmd21.ExecuteNonQuery();

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
      
        }


        public static string GetUniqueKey(int maxSize)
        {
            //try
            //{
                char[] chars = new char[62];
                chars = "0123456789".ToCharArray();
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(maxSize);
                foreach (byte b in data)
                {
                    result.Append(chars[b % (chars.Length)]);
                }
                return result.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
        }


        private void BtnItmSelect_Click(object sender, EventArgs e)
        {
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);

            PnlItemSearch.BringToFront();
            PnlItemSearch.Visible = true;

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItemBarCode,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmWarrenty,ItmVendor,ItmVenID,ItmStkUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03 FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            //button5.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PnlItemSearch.Visible = false;
        }

        private void textBox12_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItemBarCode,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmWarrenty,ItmVendor,ItmVenID,ItmStkUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03 FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "' AND ItmName LIKE '%" + textBox12.Text + "%' OR ItemBarCode LIKE '%" + textBox12.Text + "%' OR ItmID LIKE '%" + textBox12.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void textBox11_KeyUp(object sender, KeyEventArgs e)
        {
            
        }


        public void insertToListTable()
        {
            try
            {
                ListViewItem li;

                li = new ListViewItem(ItmID.Text.ToString());

                li.SubItems.Add(ItmBarCode.Text.ToString());
                li.SubItems.Add(ItmSystemID.Text.ToString());
                li.SubItems.Add(VenID.Text.ToString());
                li.SubItems.Add(ItmName.Text.ToString());
                li.SubItems.Add(ItmWarraty.Text.ToString());
                li.SubItems.Add(ItmOrderUnitCost.Text.ToString());
                li.SubItems.Add(SystemUser.Text.ToString());
                li.SubItems.Add(System.DateTime.Now.ToString());

                li.SubItems.Add(ItmSellPrice.Text.ToString());
                li.SubItems.Add(ItmDisc01.Text.ToString());
                li.SubItems.Add(ItmDisc02.Text.ToString());
                li.SubItems.Add(ItmDisc03.Text.ToString());
                li.SubItems.Add("Stock");

                listView1.Items.Add(li);

                double add = 0;
                int g;
                for (g = 0; g <= listView1.Items.Count - 1; g++)
                {
                    add = add + Convert.ToDouble(listView1.Items[g].SubItems[6].Text);

                }

                lblGross.Text = Convert.ToString(add);




                ItmBarCode.Text = "";
                ItmBarCode.Focus();

                //ItmSystemID.Text = GetUniqueKey(9);

                button1.Enabled = false;
                button2.Enabled = false;
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
                if (ItmBarCode.Text == "")
                {
                    MessageBox.Show("Please add serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ItmBarCode.Focus();
                    return;
                }


                #region check the data is in list========================================================

                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    if (listView1.Items[i].SubItems[0].Text == ItmID.Text && listView1.Items[i].SubItems[1].Text == ItmBarCode.Text)
                    {
                        MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ItmBarCode.Text = "";
                        ItmBarCode.Focus();

                        button1.Enabled = false;
                        button2.Enabled = false;

                        return;
                    }
                }

                #endregion

                #region check the data is in the Database ========================================================

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectItemOnly = "SELECT ItemID,BarcodeNumber FROM CurrentStockItems WHERE ItemID='" + ItmID.Text + "' AND BarcodeNumber='" + ItmBarCode.Text + "'";
                SqlCommand cmd1 = new SqlCommand(selectItemOnly, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView2.Rows.Clear();

                if (dr.Read() == true)
                {

                    if (dr[0].ToString() == ItmID.Text && dr[1].ToString() == ItmBarCode.Text)
                    {
                        MessageBox.Show("The Item alredy in the Database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ItmBarCode.Text = "";
                        ItmBarCode.Focus();

                        button1.Enabled = false;
                        button2.Enabled = false;

                        return;
                    }

                    if (con1.State == ConnectionState.Open)
                    {
                        con1.Close();
                        dr.Close();
                    }
                }


                #endregion

                #region check the data is in the Database barcode same but Item Diffrence ========================================================

                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();
                string selectBar = "SELECT ItemID,BarcodeNumber FROM CurrentStockItems WHERE BarcodeNumber='" + ItmBarCode.Text + "'";
                SqlCommand cmd2 = new SqlCommand(selectBar, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr2.Read() == true)
                {

                    if (dr2[0].ToString() != ItmID.Text && dr2[1].ToString() == ItmBarCode.Text)
                    {
                        DialogResult result = MessageBox.Show("The Barcode already in the Database. But that item is deference. Do you need to insert this Item to stock?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            insertToListTable();

                            return;
                        }
                        else if (result == DialogResult.No)
                        {

                            ItmBarCode.Text = "";
                            ItmBarCode.Focus();

                            button1.Enabled = false;
                            button2.Enabled = false;

                            return;
                        }
                    }

                    if (con2.State == ConnectionState.Open)
                    {
                        con2.Close();
                        dr2.Close();
                    }
                }


                #endregion


                insertToListTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                listView1.SelectedItems[0].Remove();

                double add = 0;
                int g;
                for (g = 0; g <= listView1.Items.Count - 1; g++)
                {
                    add = add + Convert.ToDouble(listView1.Items[g].SubItems[6].Text);

                }

                lblGross.Text = Convert.ToString(add);

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

                ItmID.Text = dr.Cells[0].Value.ToString();
                ItmSystemID.Text = dr.Cells[1].Value.ToString();
                ItmName.Text = dr.Cells[2].Value.ToString();
                //ItmStkType.Text = dr.Cells[2].Value.ToString();
                ItmStkType.Text = dr.Cells[4].Value.ToString();
                ItmLocation.Text = dr.Cells[5].Value.ToString();

                string warent = dr.Cells[6].Value.ToString();

                string WarType = warent.Substring(0, 1);
                string WarPeriod = warent.Substring(1);

                if (WarType == "0")
                {
                    ItmWarraty.Text = "No Warranty";
                }

                if (WarType == "1")
                {
                    ItmWarraty.Text = WarPeriod + " Year(s)";
                }
                if (WarType == "2")
                {
                    ItmWarraty.Text = WarPeriod + " Month(s)";
                }

                //VenName.Text = dr.Cells[6].Value.ToString();
                //VenID.Text = dr.Cells[7].Value.ToString();
                ItmStock.Text = dr.Cells[9].Value.ToString();
                ItmOrderUnitCost.Text = dr.Cells[10].Value.ToString();

                ItmOderCostEdit.Text = dr.Cells[10].Value.ToString();

                ItmSellPrice.Text = dr.Cells[11].Value.ToString();
                ItmDisc01.Text = dr.Cells[12].Value.ToString();
                ItmDisc02.Text = dr.Cells[13].Value.ToString();
                ItmDisc03.Text = dr.Cells[14].Value.ToString();

                // button1.Enabled = true;



                ItmBarCode.Enabled = true;
                button5.Enabled = true;
                BtnItmSelect.Enabled = true;
                ItmBarCode.Focus();
                //ItmOrderUnitCost.Enabled = true;
                CkEditPrice.Enabled = true;
                PnlItemSearch.Visible = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void ReceiveOrders_Load(object sender, EventArgs e)
        {
           // ItmSystemID.Text = GetUniqueKey(9);

            getCreateStockCode();
        }

        private void ItmBarCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (ItmBarCode.Text != "")
            {
                button2.Enabled = true;
                button1.Enabled = true;
            }

            if (ItmBarCode.Text == "")
            {
                button2.Enabled = false;
                button1.Enabled = false;
            }
        }

        public void ClearText()
        {
            //clear paymets textboxes
            lblGross.Text = "0.00";
            txtDiscount.Text = "0";
            txtTax.Text = "0";
            lblNetAmount.Text = "0.00";
            txtBillNo.Text = "";
            txtcreditPeriod.Text = "0";
            txtComment.Text = "";

            ItmSystemID.Text = "";
            BtnItmSelect.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;

            ItmID.Text = "";
            ItmName.Text = "";
            ItmWarraty.Text = "";
            VenName.Text = "";
            VenID.Text = "";
            ItmStkType.Text = "";
            ItmStock.Text = "";
            ItmLocation.Text = "";
            ItmOrderUnitCost.Text = "0.00";

            ItmOderCostEdit.Text = "0.00";
            ItmDisc01.Text = "0.00";
            ItmDisc02.Text = "0.00";
            ItmDisc03.Text = "0.00";

            ItmBarCode.Text = "";

            listView1.Items.Clear();
            
            lblNetAmount.Text = "0.00";
            lblSubTotal.Text = "0.00";
            lblTotal.Text = "0.00";
           // txtBillNo.Clear();
           // txtCalDiscount.Clear();
            txtComment.Clear();
           // txtDiscount.Clear();
           // txtTax.Clear();
           // chkcash.Checked = false;
            //chkCheque.Checked = false;
           // chkCredit.Checked = false;
            dateTimePicker1.ResetText();
            txtcreditPeriod.Clear();

            

        }
        string tot;

        //public void PaymentMethod()
        //{
        //    #region select paymentMethod
        //    string a, b, c;
        //    if (chkcash.Checked == true)
        //    {
        //        a = "1";
        //    }
        //    else
        //    {
        //        a = "0";
        //    }
        //    if (chkCredit.Checked == true)
        //    {
        //        b = "1";
        //    }
        //    else
        //    {
        //        b = "0";
        //    }
        //    if (chkCheque.Checked == true)
        //    {
        //        c = "1";
        //    }
        //    else
        //    {
        //        c = "0";
        //    }
        //    tot = a + b + c;
        //    #endregion
        //}


        private void button3_Click(object sender, EventArgs e)
        {
           // getCreateStockCode();

            try
            {

          
                //PaymentMethod();
                int i;
               // #region insert data in list view ========================================================

          //check payment Method==========================================================================================================================


                if (txtBillNo.Text == "")
                {
                    MessageBox.Show("Please Enter Bill No..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBillNo.Focus();
                    return;
                }

                

                DialogResult result = MessageBox.Show("Do you want to complete the GRN..?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string printingGRN_Number = ODSNumber.Text;
                    getCreateStockCode();
                   

                    #region Payment Detaiils to the System====================================================================

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();
                    string addgrnwholeSalePaymnets = @"INSERT INTO GRNInvoicePaymentDetails(GRNID, GrossAmount, Net_Amount, PayBalance, 
                                    GRNTotDiscount, PAymentStatus,GRNBy) 
                                    Values('" + ODSNumber.Text + "','" + lblGross.Text + "','" + lblNetAmount.Text + "','" + lblNetAmount.Text + "','" + txtDiscount.Text + "','Not_Completed','" + LgUser.Text + "') ";


                    SqlCommand cmd2 = new SqlCommand(addgrnwholeSalePaymnets, con2);
                    cmd2.ExecuteNonQuery();

                    if (con2.State == ConnectionState.Open)
                    {
                        con2.Close();
                    }

                    #endregion
                
                    #region add detils GRN_Amount_Details Table

                    SqlConnection conne = new SqlConnection(IMS);
                    conne.Open();
                    String add_detils_GRN_Amount_Details_Table = "INSERT INTO GRN_amount_Details (GRN_No,Vender_ID,Date,GrossAmount,Discount,NBT,VAT,Net_Amount,Payment_Method,Credit_Period,Bill_No,Bill_Date,Comment,GRNUser)VALUES('" + ODSNumber.Text + "','" + VenID.Text + "','" + System.DateTime.Now.ToString() + "','" + lblGross.Text + "','" + txtDiscount.Text + "','" + txtNBT.Text + "','" + txtVAT.Text + "','" + lblNetAmount.Text + "','Pending','" + txtcreditPeriod.Text + "','" + txtBillNo.Text + "','" + dateTimePicker1.Text + "','" + txtComment.Text + "','"+LgUser.Text+"')";
                    SqlCommand cmdm = new SqlCommand(add_detils_GRN_Amount_Details_Table, conne);
                    cmdm.ExecuteNonQuery();
                    //MessageBox.Show("message", "added", MessageBoxButtons.OK, MessageBoxIcon.Error);

                #endregion

                   

                    for (i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                                #region add Items to the Database in listview------------------------------------------

                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string InsertItemsToStock = "INSERT INTO CurrentStockItems(OrderID,ItemID,BarcodeNumber,SystemID,VendorID,ItemName,WarrentyPeriod,OrderCost,UserName,AddedDate,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03,ItmStatus) VALUES(@OrderID,@ItemID,@SerialNumber,@BarCode,@VendorID,@ItemName,@WarrentyPeriod,@OrderCost,@UserName,@AddedDate,@ItmSellPrice,@ItmDisc01,@ItmDisc02,@ItmDisc03,@ItmStatus)";
                        SqlCommand cmd = new SqlCommand(InsertItemsToStock, con);

                        cmd.Parameters.AddWithValue("OrderID", ODSNumber.Text);
                        cmd.Parameters.AddWithValue("ItemID", listView1.Items[i].SubItems[0].Text);
                        // database Barcode= Serial Number and SystemID=Barcode
                        cmd.Parameters.AddWithValue("SerialNumber", listView1.Items[i].SubItems[1].Text);
                        cmd.Parameters.AddWithValue("BarCode", listView1.Items[i].SubItems[2].Text);

                        cmd.Parameters.AddWithValue("VendorID", listView1.Items[i].SubItems[3].Text);
                        cmd.Parameters.AddWithValue("ItemName", listView1.Items[i].SubItems[4].Text);
                        cmd.Parameters.AddWithValue("WarrentyPeriod", listView1.Items[i].SubItems[5].Text);
                        cmd.Parameters.AddWithValue("OrderCost", listView1.Items[i].SubItems[6].Text);
                        cmd.Parameters.AddWithValue("UserName", listView1.Items[i].SubItems[7].Text);
                        cmd.Parameters.AddWithValue("AddedDate", listView1.Items[i].SubItems[8].Text);
                        cmd.Parameters.AddWithValue("ItmSellPrice", listView1.Items[i].SubItems[9].Text);
                        cmd.Parameters.AddWithValue("ItmDisc01", listView1.Items[i].SubItems[10].Text);
                        cmd.Parameters.AddWithValue("ItmDisc02", listView1.Items[i].SubItems[11].Text);
                        cmd.Parameters.AddWithValue("ItmDisc03", listView1.Items[i].SubItems[12].Text);

                        cmd.Parameters.AddWithValue("ItmStatus", listView1.Items[i].SubItems[13].Text);

                        cmd.ExecuteNonQuery();

                        //===================================================================================

                    #endregion

                                 #region Update available stock count-----------------------------------

                        string availableStock = "";

                        SqlConnection conn = new SqlConnection(IMS);
                        conn.Open();

                        string SelectStkCOunt = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID='" + listView1.Items[i].SubItems[0].Text + "'";
                        SqlCommand cmdd = new SqlCommand(SelectStkCOunt, conn);
                        SqlDataReader drr = cmdd.ExecuteReader();

                        if (drr.Read())
                        {
                            availableStock = drr[0].ToString();
                           
                        }

                        //MessageBox.Show(availableStock);


                        //update

                        //calculate the stock ammount after adding new items
                        string newstk = Convert.ToString(Convert.ToInt32(availableStock) + 1);
                       // MessageBox.Show(newstk);

                        SqlConnection con1 = new SqlConnection(IMS);
                        con1.Open();
                        string InsertItemsToStockCount = "UPDATE CurrentStock SET AvailableStockCount ='" + newstk + "' WHERE ItemID ='" + listView1.Items[i].SubItems[0].Text + "'";
                        SqlCommand cmd1 = new SqlCommand(InsertItemsToStockCount, con1);
                        cmd1.ExecuteNonQuery();



                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                        //================================================================================================================================

                    #endregion
                    }
               

                   

                    Debit_Credit_Balance();

                    MessageBox.Show("Update the stock successfully..", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RptGRNFrm grnfm = new RptGRNFrm();
                    grnfm.PrintingGRNNumber = printingGRN_Number;
                    grnfm.PrintCopyDetails = "Original Copy";
                    grnfm.GRN_Type = "Serial";
                    grnfm.Show();

                    

                    //Clear Text boxes
                    ClearText();


                    //generate New Stock Number
                    getCreateStockCode();

                    //lock Barcode
                    ItmBarCode.Enabled = false;

                    //lock vendor serch
                    button5.Enabled = true;
 
                }
            }

            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
               



        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void ItmBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    if (ItmBarCode.Text == "")
                    {
                        MessageBox.Show("Please add serial number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ItmBarCode.Focus();
                        return;
                    }

                    #region check the data is in list========================================================

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        if (listView1.Items[i].SubItems[0].Text == ItmID.Text && listView1.Items[i].SubItems[1].Text == ItmBarCode.Text)
                        {
                            MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ItmBarCode.Text = "";
                            ItmBarCode.Focus();

                            button1.Enabled = false;
                            button2.Enabled = false;

                            return;
                        }
                    }

                    #endregion

                    #region check the data is in the Database ========================================================

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string selectItemOnly = "SELECT ItemID,BarcodeNumber FROM CurrentStockItems WHERE ItemID='" + ItmID.Text + "' AND BarcodeNumber='" + ItmBarCode.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(selectItemOnly, con1);
                    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                    //dataGridView2.Rows.Clear();

                    if (dr.Read() == true)
                    {

                        if (dr[0].ToString() == ItmID.Text && dr[1].ToString() == ItmBarCode.Text)
                        {
                            MessageBox.Show("The Item alredy in the Database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ItmBarCode.Text = "";
                            ItmBarCode.Focus();

                            button1.Enabled = false;
                            button2.Enabled = false;

                            return;
                        }

                        if (con1.State == ConnectionState.Open)
                        {
                            con1.Close();
                            dr.Close();
                        }
                    }


                    #endregion

                    #region check the data is in the Database barcode same but Item Diffrence ========================================================

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();
                    string selectBar = "SELECT ItemID,BarcodeNumber FROM CurrentStockItems WHERE BarcodeNumber='" + ItmBarCode.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(selectBar, con2);
                    SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                    if (dr2.Read() == true)
                    {

                        if (dr2[0].ToString() != ItmID.Text && dr2[1].ToString() == ItmBarCode.Text)
                        {
                            DialogResult result = MessageBox.Show("The Barcode already in the Database. But that item is deference. Do you need to insert this Item to stock?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                insertToListTable();

                                return;
                            }
                            else if (result == DialogResult.No)
                            {

                                ItmBarCode.Text = "";
                                ItmBarCode.Focus();

                                button1.Enabled = false;
                                button2.Enabled = false;

                                return;
                            }
                        }

                        if (con2.State == ConnectionState.Open)
                        {
                            con2.Close();
                            dr2.Close();
                        }
                    }


                    #endregion


                    insertToListTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void CkEditPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (CkEditPrice.Checked == true)
            {
                ItmPriceChangeGroupBox.Visible = true;

            }

            if (CkEditPrice.Checked == false)
            {
                ItmPriceChangeGroupBox.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Double.Parse(ItmSellPrice.Text) < Double.Parse(ItmOderCostEdit.Text))
            {
                MessageBox.Show("Selling price is less than the Unit cost..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ItmSellPrice.Focus();
                return;
            }
            if ((Double.Parse(ItmDisc01.Text) < Double.Parse(ItmOderCostEdit.Text)) || (Double.Parse(ItmDisc01.Text) > Double.Parse(ItmSellPrice.Text)))
            {
                MessageBox.Show("Discount price 01 is less than the Unit cost OR grater than Selling price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ItmDisc01.Focus();
                return;
            }


            if ((Double.Parse(ItmDisc01.Text) < Double.Parse(ItmDisc02.Text)) || (Double.Parse(ItmOderCostEdit.Text) > Double.Parse(ItmDisc02.Text)))
            {
                MessageBox.Show("Discount price 02 is less than the Unit cost OR grater than Discount price 01", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ItmDisc02.Focus();
                return;
            }


            if ((Double.Parse(ItmDisc02.Text) < Double.Parse(ItmDisc03.Text)) || (Double.Parse(ItmOderCostEdit.Text) > Double.Parse(ItmDisc03.Text)))
            {
                MessageBox.Show("Discount price 03 is less than the Unit cost OR grater than Discount price 02", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ItmDisc03.Focus();
                return;
            }




            ItmPriceChangeGroupBox.Visible = false;
            CkEditPrice.Checked = false;

            ItmOrderUnitCost.Text = ItmOderCostEdit.Text;
        }

        private void ItmOderCostEdit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ItmSellPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ItmDisc01_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ItmDisc02_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ItmDisc03_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ItmPriceChangeGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region load vendor details in gridView
            pnlSearchVenderinReceiveOrder.Visible = true;
            pnlSearchVenderinReceiveOrder.BringToFront();

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress,CreditPeriod FROM  VenderDetails";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2],dr[3]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

                string VenSelectAll = "SELECT SELECT VenderID,VenderName,VenderPHAddress,CreditPeriod FROM  VenderDetails WHERE VenderID LIKE '%" + textBox2.Text + "%' OR VenderName LIKE '%" + textBox2.Text + "%' ";
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
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            pnlSearchVenderinReceiveOrder.Visible = false;
            //VenName.Text = "";
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region venderDetails fill in Textbox
            try
            {
                DataGridViewRow dr = dataGridView3.SelectedRows[0];
                VenID.Text = dr.Cells[0].Value.ToString();
                VenName.Text = dr.Cells[1].Value.ToString();
                txtcreditPeriod.Text = dr.Cells[3].Value.ToString();


                pnlSearchVenderinReceiveOrder.Visible = false;

                BtnItmSelect.Enabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion

        }

        private void ItmSystemID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void SystemUser_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Receiving_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = true;
            lblSubTotal.Text = lblGross.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = false;

        }

        private void lblSubTotal_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4TAXcalcu_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            lblNetAmount.Text = label39.Text;
            groupBox4TAXcalcu.Visible = false;



        }

        private void txtCalDiscount_KeyUp(object sender, KeyEventArgs e)
        {


            DiscountChecked();




        }

        public void DiscountChecked()
        {
            try
            {
                #region discount value checked or false
                if (txtCalDiscount.Text == "")
                {
                    txtCalDiscount.Text = "0";

                }

                Double discoun = Double.Parse(txtCalDiscount.Text);
                Double subtotal = Double.Parse(lblSubTotal.Text);
                Double add;
                Double balance;

                //if (chkDisc.Checked == true)
                //{

                //    add = (discoun / 100) * subtotal;
                //    txtDiscount.Text = discoun.ToString() + "%";
                //}

                //else
                //{

                //    add = discoun;
                //    txtDiscount.Text = txtCalDiscount.Text;

                //}
                //balance = subtotal - add;
                //lblTotal.Text = balance.ToString();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        private void txtCalDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region type only dcimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            #endregion
            if (txtCalDiscount.Text == "")
            {
                txtCalDiscount.Text = "0";
            }
        }

        private void chkDisc_CheckedChanged(object sender, EventArgs e)
        {
            DiscountChecked();
        }

        private void txtNBT_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void txtVAT_MouseUp(object sender, MouseEventArgs e)
        {



        }

        private void txtNBT_KeyUp(object sender, KeyEventArgs e)
        {
            #region calculate NBT and passdetails in txtbox
            try
            {

                if (txtNBT.Text == "")
                {
                    txtNBT.Text = "0";
                }
                Double addnbt;
                Double tot;
                Double totnbt;


                addnbt = (Double.Parse(lblTotal.Text) * Double.Parse(txtNBT.Text)) / 100;
                tot = addnbt + Double.Parse(lblTotal.Text);
                totnbt = tot;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            }

            #endregion

        }

        private void txtVAT_KeyUp(object sender, KeyEventArgs e)
        {
            #region calculate vat and passdetails in txtbox

            try
            {

                if (txtVAT.Text == "")
                {
                    txtVAT.Text = "0";
                }
                Double addnbt;
                Double tot;
                Double totnbt;
                Double addTax;
                Double taxTot;
                Double vatnbt;

                addnbt = (Double.Parse(lblTotal.Text) * Double.Parse(txtNBT.Text)) / 100;
                tot = addnbt + Double.Parse(lblTotal.Text);
                totnbt = tot;



                addTax = totnbt * double.Parse(txtVAT.Text) / 100;
                vatnbt = addnbt + addTax;
                txtTax.Text = vatnbt.ToString();
                taxTot = totnbt + addTax;

                label39.Text = taxTot.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            
            }

            #endregion


        }

        private void txtCalDiscount_TextChanged(object sender, EventArgs e)
        {

            if (txtCalDiscount.Text == "")
            {
                txtCalDiscount.Text = "0";
            }

            txtDiscountPre.Text=Convert.ToString ((Convert.ToDouble(txtCalDiscount.Text) / Convert.ToDouble(lblSubTotal.Text))*100);

            lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));

           
        }

        private void txtNBT_TextChanged(object sender, EventArgs e)
        {
            if (txtNBT.Text == "")
            {
                txtNBT.Text = "0";
            }
            
            NBTandVATCalc();

        }

        private void txtNBT_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region type only dcimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            #endregion

            if (txtNBT.Text == "")
            {
                txtNBT.Text = "0";
            }
        }

        private void txtVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region type only dcimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            #endregion

            if (txtVAT.Text == "")
            {
                txtVAT.Text = "0";
            }
        }

        public void TypeonlyDecimal(object sender, KeyPressEventArgs e)
        {
           
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region type only dcimal
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
            #endregion
        }

        private void lblNetAmount_Click(object sender, EventArgs e)
        {
           
        }

        public void TaxCalculator()
        {
           // lblSubTotal.Text;
        }

        public void CalNetAmount()
        {
            lblNetAmount.Text=Convert.ToString(Convert.ToDouble(lblGross.Text) - Convert.ToDouble(txtDiscount.Text) + Convert.ToDouble(txtTax.Text));
        }

        private void lblGross_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void CalDiscount_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void txtDiscountPre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscountPre.Text == "")
                {
                    //txtDiscountPre.Text = "0";
                    return;
                }

                txtCalDiscount.Text = Convert.ToString((Convert.ToDouble(lblSubTotal.Text) / 100) * Convert.ToDouble(txtDiscountPre.Text));

                lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void NBTandVATCalc()
        {
            try
            {

                double NBTAmount = (Convert.ToDouble(lblTotal.Text) * Convert.ToDouble(txtNBT.Text)) / 100;
                double VATAmount = ((NBTAmount + Convert.ToDouble(lblTotal.Text)) * Convert.ToDouble(txtVAT.Text)) / 100;

                txtTax.Text = Convert.ToString(NBTAmount + VATAmount);

                label39.Text = Convert.ToString(NBTAmount + VATAmount + Convert.ToDouble(lblTotal.Text));
            }
            catch(Exception ex)
            {
            
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            if (txtVAT.Text == "")
            {
                txtVAT.Text = "0";
            }

            NBTandVATCalc();
        }

        private void lblTotal_TextChanged(object sender, EventArgs e)
        {
            NBTandVATCalc();

            txtDiscount.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text)-Convert.ToDouble(lblTotal.Text));
        }

        private void label39_TextChanged(object sender, EventArgs e)
        {
            lblNetAmount.Text = label39.Text;
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
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
                txtcreditPeriod.Focus();
            }
        }

        private void txtcreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button3.Focus();
            }
        }

        private void txtDiscountPre_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            txtDiscountPre.Text = "0";
        }

        private void lblSubTotal_TextChanged(object sender, EventArgs e)
        {
            lblTotal.Text = lblSubTotal.Text;

            if (double.Parse(lblSubTotal.Text) > 0)
            {
                txtDiscountPre.Enabled = true;
                txtCalDiscount.Enabled = true;
            }
        }

        private void txtDiscountPre_Leave(object sender, EventArgs e)
        {
            if(txtDiscountPre.Text == "")
            {
                txtDiscountPre.Text = "0";
            }

        }

        private void txtDiscountPre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCalDiscount.Focus();
            }
        }

        private void txtCalDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtNBT.Focus();
            }
        }

        private void txtNBT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtVAT.Focus();
            }
        }

        private void txtVAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button10.Focus();
            }
        }

        private void label49_Click(object sender, EventArgs e)
        {
            AddVendor newItemvisi = new AddVendor();
            newItemvisi.Visible = true;
        }

        private void label50_Click(object sender, EventArgs e)
        {
            NewItem newitm = new NewItem();
            newitm.Visible = true;
        }
    }
}