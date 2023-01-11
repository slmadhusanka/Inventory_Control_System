using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Control_System
{
    public partial class GRNReturn : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public GRNReturn()
        {
            InitializeComponent();
            GRNCode();
           
            
        }


        public void loadVenderGRNwise()
        {
            try
            {
                #region load Vender GRN wise---------------------------------------------------------------
                SqlConnection con3 = new SqlConnection(IMS);
                con3.Open();

                string VenSelectAllgrn = @"SELECT   VenderDetails.VenderID, GRN_amount_Details.GRN_No, VenderDetails.VenderName, VenderDetails.VenderPHAddress
                                           FROM     VenderDetails INNER JOIN
                                           GRN_amount_Details ON VenderDetails.VenderID = GRN_amount_Details.Vender_ID where GRN_amount_Details.GRN_No='" + txtGRNno.Text + "'";
                SqlCommand cmd3 = new SqlCommand(VenSelectAllgrn, con3);
                SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);


                while (dr3.Read())
                {

                    txtVenID.Text = dr3[0].ToString();
                    txtVenName.Text = dr3[2].ToString();
                    txtVenAddre.Text = dr3[3].ToString();

                }
                #endregion---------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void filllistview()
        {
           
        }


        public void sumGrossAmount()
        {
           //// MessageBox.Show(listView1.Items[0].SubItems[9].Text);

           // decimal gtotal = 0;
           // foreach (ListViewItem lstItem in listView1.Items)
           // {
           //     gtotal += decimal.Parse(lstItem.SubItems[9].Text);
           // }
           // lblGrossAmt.Text = Convert.ToString(gtotal);
        }

        public void sumDiscount()
        {
           //// MessageBox.Show(listView1.Items[0].SubItems[10].Text);
           // decimal gtotal = 0;
           // foreach (ListViewItem lstItem in listView1.Items)
           // {
           //     gtotal += decimal.Parse(lstItem.SubItems[10].Text);
           // }
           // txtMasterDis.Text = Convert.ToString(gtotal);
        }

        public void sumNetAmount()
        {
          //  MessageBox.Show(listView1.Items[0].SubItems[11].Text);

            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listView1.Items)
            {
                gtotal += decimal.Parse(lstItem.SubItems[11].Text);
            }
            lblGrossAmt.Text = Convert.ToString(gtotal);
           
        }

        public void calculationGrossDiscountNet()
        {
            try
            {

                #region calculation gross, discount, net
                //calculate Gross amount----------------------------------------------
                if (txtreturnqty.Text == "")
                {
                    return;
                }

                Double qtypurches = ((Double.Parse(txtreturnqty.Text)) * (Double.Parse(txpurches.Text)));
                txtgrossamount.Text = qtypurches.ToString();

                //Calculate Discount---------------------------------------------------

                Double calculateDiscReturnItem = ((Double.Parse(txtreturnqty.Text)) * (Double.Parse(discount1)));
                txtDiscount.Text = calculateDiscReturnItem.ToString();

                //calculate neat amount-------------------------------------------------

                Double Amount = qtypurches - calculateDiscReturnItem;
                tot = Amount.ToString();
                txtBatchNo.Text = tot;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void nomalclear()
        {
           // txtGrnID.Clear();
            txtItemName.Clear();
            txtitemID.Clear();
            txtBarcode.Clear();
            txtSerialNo.Clear();
            txtitemWarranty.Clear();
            txtreturnqty.Clear();
            txpurches.Clear();
            txtgrossamount.Text = "0.0";
            txtDiscount.Text = "0.0";
            txtBatchNo.Clear();
            txtWarran.Clear();
        }

        public void GRNCode()
        {
            #region auto generate return Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();
                

                //=====================================================================================================================
                string sql = "select Return_Num from GRN_Return_Note";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    txtreturnNo.Text = "RTN1001";
                    
                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 Return_Num  FROM GRN_Return_Note WHERE Return_Num LIKE 'RTN%' order by Return_Num DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();



                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        //                      

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        txtreturnNo.Text = "RTN" + no;

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
        private void splitterControl1_SplitterMoved(object sender, SplitterEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void pnlSearchVenderinGRN_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PnlItemSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            #region load grn & vender in gridView

            panel3.Visible = true;

            try
            {
                #region load wholesale item in grid view
                {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = @"SELECT GRNWholesaleItems.GRNNumber, GRNWholesaleItems.ItemID,GRNWholesaleItems.BarCodeID, GRNWholesaleItems.BatchNumber, GRN_amount_Details.Vender_ID, GRN_amount_Details.GRN_No, VenderDetails.VenderName, VenderDetails.VenderPHAddress
                                        FROM GRN_amount_Details INNER JOIN VenderDetails ON GRN_amount_Details.Vender_ID = VenderDetails.VenderID INNER JOIN
                                        GRNWholesaleItems ON GRN_amount_Details.GRN_No = GRNWholesaleItems.GRNNumber where GRNWholesaleItems.AvailbleItemCount!='"+0+"'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3],dr[4]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
                #endregion
                }


                {
                    #region item wise load in gride view

                    SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();

                string VenSelectAll2 = @"SELECT CurrentStockItems.OrderID, CurrentStockItems.ItemID, CurrentStockItems.SystemID, CurrentStockItems.BarcodeNumber, CurrentStockItems.VendorID
                                         FROM CurrentStockItems INNER JOIN VenderDetails ON CurrentStockItems.VendorID = VenderDetails.VenderID where ItmStatus!='Return' ";
                SqlCommand cmd2 = new SqlCommand(VenSelectAll2, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                // dataGridView1.Rows.Clear();

                while (dr2.Read() == true)
                {
                    dataGridView1.Rows.Add(dr2[0], dr2[1], dr2[2], dr2[3],dr2[4]);

                }

                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                }
                    #endregion

                }
            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            #region Search grn in gridView
            
            try
            {
                #region search wholesale grn...............................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = @"SELECT GRNWholesaleItems.GRNNumber, GRNWholesaleItems.ItemID,GRNWholesaleItems.BarCodeID, GRNWholesaleItems.BatchNumber, GRN_amount_Details.Vender_ID, GRN_amount_Details.GRN_No, VenderDetails.VenderName, VenderDetails.VenderPHAddress
                                        FROM   GRN_amount_Details INNER JOIN VenderDetails ON GRN_amount_Details.Vender_ID = VenderDetails.VenderID INNER JOIN
                                        GRNWholesaleItems ON GRN_amount_Details.GRN_No = GRNWholesaleItems.GRNNumber where GRNWholesaleItems.GRNNumber like '%" + textBox2.Text + "%' or GRNWholesaleItems.ItemID like '%" + textBox2.Text + "%' or GRNWholesaleItems.BarCodeID like '%" + textBox2.Text + "%' or GRNWholesaleItems.BatchNumber like '%" + textBox2.Text + "%' or GRN_amount_Details.Vender_ID like '%" + textBox2.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3],dr[4]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
                #endregion
                //----------------------------------------------------------------------------------------------------------

                {
                    #region search grn item wise..............................................

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();

                    string VenSelectAll2 =@"SELECT CurrentStockItems.OrderID, CurrentStockItems.ItemID, CurrentStockItems.SystemID, CurrentStockItems.BarcodeNumber, CurrentStockItems.VendorID
                                         FROM CurrentStockItems INNER JOIN VenderDetails ON CurrentStockItems.VendorID = VenderDetails.VenderID 
                                         where  OrderID like '%" + textBox2.Text + "%' or ItemID like '%" + textBox2.Text + "%' or BarcodeNumber like '%" + textBox2.Text + "%' or SystemID like '%" + textBox2.Text + "%' or CurrentStockItems.VendorID like '%" + textBox2.Text + "%'";
                    SqlCommand cmd2 = new SqlCommand(VenSelectAll, con2);
                    SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();

                    while (dr2.Read() == true)
                    {
                        dataGridView1.Rows.Add(dr2[0], dr2[1], dr2[2], dr2[3],dr2[4]);

                    }

                    if (con2.State == ConnectionState.Open)
                    {
                        con2.Close();
                    }



                    #endregion
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          


        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           

        }
       
        private void txtGRNno_TextChanged(object sender, EventArgs e)
        {
        #region load and check  data in listview
            try
            {

                {
                    #region load wholesale items in listview

                    LstShow.Items.Clear();

                    SqlConnection cmm = new SqlConnection(IMS);
                    cmm.Open();

                    String add = @"SELECT  GRNWholesaleItems.GRNNumber,NewItemDetails.ItmName, GRNWholesaleItems.ItemID, GRNWholesaleItems.BarCodeID, 
                               GRNWholesaleItems.BatchNumber, GRNWholesaleItems.ItemWarrenty, GRNWholesaleItems.AvailbleItemCount,GRNWholesaleItems.PerchPrice, 
                               GRN_amount_Details.GrossAmount, GRNWholesaleItems.DiscountAmount, GRN_amount_Details.Net_Amount  FROM            NewItemDetails INNER JOIN
                               GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID INNER JOIN GRN_amount_Details ON GRNWholesaleItems.GRNNumber = GRN_amount_Details.GRN_No where GRNWholesaleItems.GRNNumber='" + txtGRNno.Text + "' and GRNWholesaleItems.AvailbleItemCount!='0' ORDER BY GRNWholesaleItems.GRNNumber  ";

                    SqlCommand comd = new SqlCommand(add, cmm);
                    SqlDataReader dr2 = comd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr2.Read())
                    {
                        ListViewItem li;
                        li = new ListViewItem(dr2[0].ToString());
                        li.SubItems.Add(dr2[1].ToString());
                        li.SubItems.Add(dr2[2].ToString());
                        li.SubItems.Add(dr2[3].ToString());
                        li.SubItems.Add("NO");
                        li.SubItems.Add(dr2[4].ToString());
                        li.SubItems.Add(dr2[5].ToString());
                        li.SubItems.Add(dr2[6].ToString());
                        li.SubItems.Add(dr2[7].ToString());
                        li.SubItems.Add(dr2[8].ToString());
                        li.SubItems.Add(dr2[9].ToString());
                        li.SubItems.Add(dr2[10].ToString());



                        LstShow.Items.Add(li);


                    }

                    if (cmm.State == ConnectionState.Open)
                    {
                        cmm.Close();
                    }
                    #endregion
                    {
                        #region load Item wise in list view

                        SqlConnection cmm1 = new SqlConnection(IMS);
                        cmm1.Open();
                        String Itemwiseselect = @"SELECT CurrentStockItems.OrderID, CurrentStockItems.ItemName, CurrentStockItems.ItemID, CurrentStockItems.SystemID,  CurrentStockItems.BarcodeNumber,
                                                CurrentStockItems.WarrentyPeriod, CurrentStockItems.OrderCost,GRN_amount_Details.GrossAmount, GRN_amount_Details.Discount, GRN_amount_Details.Net_Amount 
                                                FROM CurrentStockItems INNER JOIN GRN_amount_Details ON CurrentStockItems.OrderID = GRN_amount_Details.GRN_No where CurrentStockItems. OrderID='" + txtGRNno.Text + "' and [CurrentStockItems].[ItmStatus]!='Return' order by CurrentStockItems.OrderID asc  ";
                            


                        SqlCommand comd1 = new SqlCommand(Itemwiseselect, cmm1);
                        SqlDataReader dr3 = comd1.ExecuteReader(CommandBehavior.CloseConnection);


                        while (dr3.Read())
                        {
                            ListViewItem li;
                            li = new ListViewItem(dr3[0].ToString());
                            li.SubItems.Add(dr3[1].ToString());
                            li.SubItems.Add(dr3[2].ToString());
                            li.SubItems.Add(dr3[3].ToString());
                            li.SubItems.Add(dr3[4].ToString());
                            li.SubItems.Add("NO");
                            li.SubItems.Add(dr3[5].ToString()); 
                            li.SubItems.Add("1");
                            li.SubItems.Add(dr3[6].ToString());
                            li.SubItems.Add(dr3[7].ToString());
                            li.SubItems.Add(dr3[8].ToString());
                            li.SubItems.Add(dr3[9].ToString());


                            LstShow.Items.Add(li);
                        }
                        if (cmm1.State == ConnectionState.Open)
                        {
                            cmm1.Close();
                        }
                    }

                    loadVenderGRNwise();
                    //--------------------------------------

                        #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        #endregion


            try
            {
                SqlConnection cms = new SqlConnection(IMS);
                cms.Open();
                String GRnAmount = "SELECT GRN_No, GrossAmount, Discount, NBT, VAT, Net_Amount FROM GRN_amount_Details where GRN_No='" + txtGRNno.Text + "'";
                SqlCommand cmm2 = new SqlCommand(GRnAmount, cms);
                SqlDataReader dr = cmm2.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    lblGrn.Text = dr[0].ToString();
                    lblgrosvat.Text = dr[1].ToString();
                    lblDiscountAm.Text = dr[2].ToString();
                    lblnbt.Text = dr[3].ToString();
                    lblvat.Text = dr[4].ToString();
                    lblnetVat.Text = dr[5].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LstShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            calculationGrossDiscountNet();

        }
        Double discount;
        String discount1;
        private void LstShow_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                #region select row pass valu textbox


                ListViewItem itmes = LstShow.SelectedItems[0];
                //txtGrnID.Text = itmes.SubItems[0].Text;
                txtItemName.Text = itmes.SubItems[1].Text;
                txtitemID.Text = itmes.SubItems[2].Text;
                txtBarcode.Text = itmes.SubItems[3].Text;
                txtSerialNo.Text = itmes.SubItems[4].Text;
                txtitemWarranty.Text = itmes.SubItems[5].Text;
                txtWarran.Text = itmes.SubItems[6].Text;
                txtreturnqty.Text = itmes.SubItems[7].Text;
                txpurches.Text = itmes.SubItems[8].Text;
                txtgrossamount.Text = itmes.SubItems[9].Text;
                txtDiscount.Text = itmes.SubItems[10].Text;
                txtBatchNo.Text = itmes.SubItems[11].Text;

                discount = Double.Parse(itmes.SubItems[10].Text) / Double.Parse(itmes.SubItems[7].Text);
                discount1 = discount.ToString();


                txtreturnqty.Focus();
                #endregion
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
                #region check checkbox & check dupplicate value in list view .........................................

                if (txtreturnqty.Text == "")
                {
                    MessageBox.Show("Please Fill in the Details..");
                    return;
                }

                if ((Double.Parse(txtreturnqty.Text)) <= 0.0)
                {
                    MessageBox.Show("Please Fill in the Details..");
                    return;
                }


                String valable = "";

                valable = LstShow.SelectedItems[0].SubItems[7].Text;

                Double sad = Double.Parse(txtreturnqty.Text);
                String gir = sad.ToString();

                if ((Double.Parse(gir)) > (Double.Parse(valable)))
                {
                    MessageBox.Show("Grater than");
                    return;
                }





                String checkItemID = LstShow.SelectedItems[0].SubItems[2].ToString();
                String Barcode = LstShow.SelectedItems[0].SubItems[3].ToString();
                String batch = LstShow.SelectedItems[0].SubItems[5].ToString();
                // String sssss = listView1.Items[i].SubItems[2].Text;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    String sssss = listView1.Items[i].SubItems[2].Text;
                    if ((listView1.Items[i].SubItems[5].Text) == (txtitemWarranty.Text))
                    {
                        MessageBox.Show("alredy details");
                        return;
                    }
                }


                //SqlConnection cnn2 = new SqlConnection(IMS);
                //cnn2.Open();
                //String check = "SELECT GRNNumber, [ItemID],[BarCodeID] ,[BatchNumber],[ItemWarrenty],[ItemAdded] FROM GRNWholesaleItems where  GRNNumber='"+txtGRNno.Text+"'";
                //SqlCommand cmm2 = new SqlCommand(check,cnn2);
                //SqlDataReader dr = cmm2.ExecuteReader(CommandBehavior.CloseConnection);

                // String checkItemID = LstShow.SelectedItems[0].SubItems[2].ToString();






                ListViewItem li;
                li = new ListViewItem(txtItemName.Text);

                li.SubItems.Add(txtItemName.Text);
                li.SubItems.Add(txtitemID.Text);
                li.SubItems.Add(txtBarcode.Text);
                li.SubItems.Add(txtSerialNo.Text);
                li.SubItems.Add(txtitemWarranty.Text);
                li.SubItems.Add(txtWarran.Text);
                li.SubItems.Add(txtreturnqty.Text);
                li.SubItems.Add(txpurches.Text);
                li.SubItems.Add(txtgrossamount.Text);
                li.SubItems.Add(txtDiscount.Text);
                li.SubItems.Add(txtBatchNo.Text);

                listView1.Items.Add(li);

                sumGrossAmount();
                sumDiscount();
                sumNetAmount();
                nomalclear();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        String tot;
        private void txtreturnqty_TextChanged(object sender, EventArgs e)
        {
            if (txtreturnqty.Text == "" || txpurches.Text=="")
            {
               // txtreturnqty.Text = "0.0";
                return;
            }


            calculationGrossDiscountNet();
            

        }

        private void txtreturnqty_Leave(object sender, EventArgs e)
        {
            if (txtreturnqty.Text == "")
            {
                txtreturnqty.Text = "0.0";
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //remove Row--------------------------------------------------------------

            listView1.SelectedItems[0].Remove();
            sumGrossAmount();
            sumDiscount();
            sumNetAmount();
            return;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                #region value pass to GRNno text box----------------------------------------
                loadVenderGRNwise();
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtGRNno.Text = dr.Cells[0].Value.ToString();
                panel3.Visible = false;

                listView1.Items.Clear();

                textBox2.Text = "";
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        String batchno, ReturnQty, serialNo, available, Balance, WholesalAvalable, reduseAvalable, grossAmou, NETAmoun, discount2, payBalance, DebitAmount, BalanceAmoun;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                GRNCode();

                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("Please enter details or click 'Close' button");
                    return;
                }

                int a;
                DialogResult sa = MessageBox.Show("Do you  want to save?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (sa == DialogResult.Yes)
                {
                    GRNCode();

                    for (a = 0; a <= listView1.Items.Count - 1; a++)
                    {
                        //if(listView1.Items.Count==0)
                        //{
                        //    MessageBox.Show("Please enter datails or click 'Cancel' button");
                        //}
                        #region Insert to GRNReturn....................................

                        SqlConnection cnn1 = new SqlConnection(IMS);
                        cnn1.Open();

                        String insertGrnReturn = @"insert GRN_Return_Note ([Return_Num],[Vender_ID] ,[GRN_Num] ,[Batch_Num],[Itm_ID] ,[Itm_Name] ,[Serial_Num],[Selling_Price],[Return_Qnty],[Tot_Amount],[Net_Amount],[Return_Statement],[Return_Employee],[Return_Date],[Remark],NBT,VAT)values
                    ('" + txtreturnNo.Text + "','" + txtVenID.Text + "','" + txtGRNno.Text + "','" + listView1.Items[a].SubItems[5].Text + "','" + listView1.Items[a].SubItems[2].Text + "','" + listView1.Items[a].SubItems[1].Text + "','" + listView1.Items[a].SubItems[4].Text + "','" + listView1.Items[a].SubItems[8].Text + "','" + listView1.Items[a].SubItems[7].Text + "','" + lblGrossAmt.Text + "','" + lblNetAmount.Text + "','" + "return" + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "','" + txtRemark.Text + "','" + lblNBTLast.Text + "','" + lblVATLast.Text + "' ) ";

                        SqlCommand cmm1 = new SqlCommand(insertGrnReturn, cnn1);
                        cmm1.ExecuteNonQuery();
                        #endregion

                        #region update Current Stock Items..................

                        batchno = listView1.Items[a].SubItems[5].Text;
                        if (batchno == "NO")
                        {
                            SqlConnection cnn2 = new SqlConnection(IMS);
                            cnn2.Open();
                            String curenrStockItemUpdate = "update CurrentStockItems set ItmStatus='" + "Return" + "'   where OrderID='" + txtGRNno.Text + "' and ItemID='" + listView1.Items[a].SubItems[2].Text + "' and SystemID='" + listView1.Items[a].SubItems[3].Text + "' and BarcodeNumber='" + listView1.Items[a].SubItems[4].Text + "'   ";
                            SqlCommand cmm2 = new SqlCommand(curenrStockItemUpdate, cnn2);
                            cmm2.ExecuteNonQuery();

                        }
                        #endregion

                        #region Upadate Available Stock Count.................................

                        ReturnQty = (listView1.Items[a].SubItems[7].Text);

                        //select Available Stock Count ----------------------------------------------------

                        SqlConnection cnn4 = new SqlConnection(IMS);
                        cnn4.Open();
                        String AvailableStock = "select AvailableStockCount from CurrentStock where ItemID='" + listView1.Items[a].SubItems[2].Text + "' ";
                        SqlCommand cmm4 = new SqlCommand(AvailableStock, cnn4);
                        SqlDataReader dr = cmm4.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read() == true)
                        {
                            available = dr[0].ToString();
                        }
                        //----------------------------------------------------------------------------------

                        //reduce return qty from Available Stock Count-------------------------------------

                        Balance = (Double.Parse(available) - Double.Parse(ReturnQty)).ToString();

                        //---------------------------------------------------------------------------------

                        //Update curenrt Stock Item--------------------------------------------------------

                        SqlConnection cnn3 = new SqlConnection(IMS);
                        cnn3.Open();
                        String curenrStockItemUpdate1 = "update CurrentStock set AvailableStockCount='" + Balance + "' where ItemID='" + listView1.Items[a].SubItems[2].Text + "'  ";
                        SqlCommand cmm3 = new SqlCommand(curenrStockItemUpdate1, cnn3);
                        cmm3.ExecuteNonQuery();

                        #endregion

                        #region select avalable Item Count & Upadate available Count................................................

                        serialNo = listView1.Items[a].SubItems[4].Text;
                        if (serialNo == "NO")
                        {
                            #region select avalable Item Count......................................................................

                            SqlConnection cnn5 = new SqlConnection(IMS);
                            cnn5.Open();
                            String WholesaleItem = "SELECT [AvailbleItemCount] FROM [GRNWholesaleItems] where [GRNNumber]='" + txtGRNno.Text + "' and [ItemID]='" + listView1.Items[a].SubItems[2].Text + "'and [BarCodeID]='" + listView1.Items[a].SubItems[3].Text + "' and [BatchNumber]='" + listView1.Items[a].SubItems[5].Text + "' ";
                            SqlCommand cmm5 = new SqlCommand(WholesaleItem, cnn5);
                            SqlDataReader dr1 = cmm5.ExecuteReader(CommandBehavior.CloseConnection);
                            while (dr1.Read() == true)
                            {
                                WholesalAvalable = dr1[0].ToString();
                            }
                            #endregion
                            // reduse return QTY from grnavailable item count---------------------------------------------

                            reduseAvalable = ((Double.Parse(WholesalAvalable)) - (Double.Parse(ReturnQty))).ToString();

                            //---------------------------------------------------------------------------------------------

                            #region Upadate available Count................................................

                            SqlConnection cnn6 = new SqlConnection(IMS);
                            cnn6.Open();
                            String WholesaleUpdate = "update  [GRNWholesaleItems]set [AvailbleItemCount]='" + reduseAvalable + "',Rtn_Items='" + listView1.Items[a].SubItems[7].Text + "' where [GRNNumber]='" + txtGRNno.Text + "' and [ItemID]='" + listView1.Items[a].SubItems[2].Text + "'and [BarCodeID]='" + listView1.Items[a].SubItems[3].Text + "' and [BatchNumber]='" + listView1.Items[a].SubItems[5].Text + "' ";
                            SqlCommand cmm6 = new SqlCommand(WholesaleUpdate, cnn6);
                            cmm6.ExecuteNonQuery();

                            #endregion
                        }

                        #endregion

                        #region select GRN Invoice Payment Details..........................................................

                        SqlConnection cnn7 = new SqlConnection(IMS);
                        cnn7.Open();
                        String grnAmountDetails = "SELECT [GrossAmount],[Net_Amount],[PayBalance],[GRNTotDiscount]FROM [GRNInvoicePaymentDetails] where [GRNID]='" + txtGRNno.Text + "'";
                        SqlCommand cmm7 = new SqlCommand(grnAmountDetails, cnn7);
                        SqlDataReader dr2 = cmm7.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr2.Read() == true)
                        {
                            grossAmou = dr2[0].ToString();
                            payBalance = dr2[2].ToString();
                            NETAmoun = dr2[1].ToString();
                            discount2 = dr2[3].ToString();
                        }


                        #region Calculation gross,discount,vat,nbt,paybalance..........................................


                        String Updategross = ((Double.Parse(grossAmou)) - (Double.Parse(lblGrossAmt.Text))).ToString();
                        //  String UpdateNBT = ((Double.Parse(NBT)) - (Double.Parse(lblNBTLast.Text))).ToString();
                        //  String UpdateVAt = ((Double.Parse(VAT)) - (Double.Parse(lblVATLast.Text))).ToString();
                        String UpdateNetAmount = ((Double.Parse(NETAmoun)) - (Double.Parse(lblNetAmount.Text))).ToString();
                        String updatediscount = ((Double.Parse(discount2)) - (Double.Parse(lblMasterdiscount.Text))).ToString();
                        String UpadatePayBalance = ((Double.Parse(payBalance)) - (Double.Parse(lblNetAmount.Text))).ToString();

                        //MessageBox.Show(Updategross);
                        //MessageBox.Show(UpadatePayBalance);
                        //MessageBox.Show(UpdateNetAmount);
                        //MessageBox.Show(updatediscount);
                        #endregion
                        #endregion

                        #region if pay balance graterthan 0 ,update GRN Invoice Payment Details...........................

                        if ((Double.Parse(UpadatePayBalance)) >= 0)
                        {
                            {
                                SqlConnection cnn8 = new SqlConnection(IMS);
                                cnn8.Open();
                                String updateGRNPaymentDetails = "update GRNInvoicePaymentDetails set GrossAmount='" + Updategross + "',Net_Amount='" + UpdateNetAmount + "',PayBalance='" + UpadatePayBalance + "',GRNTotDiscount='" + updatediscount + "' where [GRNID]='" + txtGRNno.Text + "'";
                                SqlCommand cmm8 = new SqlCommand(updateGRNPaymentDetails, cnn8);
                                cmm8.ExecuteNonQuery();

                            }

                            //{
                            //    SqlConnection cnn10 = new SqlConnection(IMS);
                            //    cnn10.Open();
                            //    String insertvenderPayment = "insert into [vender_Payment]  ( [VenderID],[DocNumber],[Credit_Amount],[Debit_Amount],[Balance])values('" + txtVenID.Text + "','" + txtreturnNo.Text + "','" + "0.0" + "','" + Debitcal + "','" + lblNetAmount.Text + "') FROM  where VenderID='" + txtVenID.Text + "'";
                            //    SqlCommand cmm10 = new SqlCommand(insertvenderPayment, cnn10);
                            //    cmm10.ExecuteNonQuery();
                            //    MessageBox.Show("Update grn invoice Payment Details Successfull....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //}
                        }
                        #endregion

                        #region GRN Invoice Payment Detail...............................................
                        if ((Double.Parse(UpadatePayBalance)) < 0)
                        {
                            {
                                #region GRN Invoice Payment Detail update.........................................

                                SqlConnection cnn8 = new SqlConnection(IMS);
                                cnn8.Open();
                                String updateGRNPaymentDetails = "update GRNInvoicePaymentDetails set GrossAmount='" + Updategross + "',Net_Amount='" + UpdateNetAmount + "',PayBalance='" + "0" + "',GRNTotDiscount='" + updatediscount + "' where [GRNID]='" + txtGRNno.Text + "'";
                                SqlCommand cmm8 = new SqlCommand(updateGRNPaymentDetails, cnn8);
                                cmm8.ExecuteNonQuery();
                                //MessageBox.Show("Update grn invoice Payment Details Successfull....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                #endregion
                            }

                            {
                                #region select vender Payment..............................................

                                SqlConnection cnn9 = new SqlConnection(IMS);
                                cnn9.Open();
                                String Selectvender_Payment = "SELECT  top 1 AutoNum,  [VenderID],[DocNumber],[Credit_Amount],[Debit_Amount],[Balance] FROM [vender_Payment] where VenderID='" + txtVenID.Text + "' order by AutoNum desc";
                                SqlCommand cmm9 = new SqlCommand(Selectvender_Payment, cnn9);
                                SqlDataReader dr3 = cmm9.ExecuteReader(CommandBehavior.CloseConnection);
                                while (dr3.Read() == true)
                                {
                                    DebitAmount = dr3[3].ToString();
                                    BalanceAmoun = dr3[4].ToString();
                                }
                                #endregion
                            }
                            String Debitcal = ((Double.Parse(DebitAmount)) - (Double.Parse(lblNetAmount.Text))).ToString();
                            String BalanceAmount = ((Double.Parse(BalanceAmoun)) + (Double.Parse(lblNetAmount.Text))).ToString();

                            {
                                MessageBox.Show(txtVenID.Text);
                                #region Insert to vender Patment................................
                                SqlConnection cnn10 = new SqlConnection(IMS);
                                cnn10.Open();
                                String insertvenderPayment = "insert into [vender_Payment]  ( [VenderID],[DocNumber],[Credit_Amount],[Debit_Amount],[Balance])values('" + txtVenID.Text + "','" + txtreturnNo.Text + "','" + "0.0" + "','" + Debitcal + "','" + BalanceAmount + "')   where VenderID='" + txtVenID.Text + "'";
                                SqlCommand cmm10 = new SqlCommand(insertvenderPayment, cnn10);
                                cmm10.ExecuteNonQuery();
                                #endregion
                            }




                        #endregion
                        }


                        MessageBox.Show("Save Return Note Successfull....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        samReport sm = new samReport();
                        sm.PrintingRTNNumber = txtreturnNo.Text;
                        sm.Show();

                       
                    }
                }



                //----------------------------------------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #region clear All------------------------------------------------------------------------------------------

            txtGRNno.Clear();
            txtItemName.Clear();
            txtitemID.Clear();
            txtBarcode.Clear();
            txtSerialNo.Clear();
            txtitemWarranty.Clear();
            txtreturnqty.Clear();
            txpurches.Clear();
            txtgrossamount.Text = "0.0";
            txtDiscount.Text = "0.0";
            txtBatchNo.Clear();
            txtWarran.Clear();
            listView1.Items.Clear();
            txtRemark.Clear();
            txtgrossamount.Text = "0.00";
            txtMasterDis.Text = "0.00";
            lblNetAmount.Text = "0.00";
            txtVenAddre.Clear();
            txtVenName.Clear();
            txtVenID.Clear();
            lblGrossAmt.Text = "0.00";
            lblNetAmount.Text = "0.00";
            txtMasterDis.Text = "0.00";
            lblgrosvat.Text = "0.00";
            lblnetVat.Text = "0.00";
            button2.Enabled = false;
            GRNCode();
            #endregion

            GRNCode();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBatchNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtreturnqty_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                ListViewItem itmes = LstShow.SelectedItems[0];
                Decimal QTY = (Decimal.Parse(itmes.SubItems[7].Text));

                if (QTY < (Decimal.Parse(txtreturnqty.Text)))
                {
                    MessageBox.Show("value is graterthan to GRN value");

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            //if(txtreturnqty.Text=="")
            //{
            //    txtreturnqty.Text = "0.0";
            //}
            //calculationGrossDiscountNet();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblGrossAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblSubTotal.Text = lblGrossAmt.Text;
                //Calculate Discount-----------------------------------------------------------------------------------------------------------------

                String discount = ((Double.Parse(lblDiscountAm.Text) / Double.Parse(lblgrosvat.Text)) * Double.Parse(lblGrossAmt.Text)).ToString();
                lblMasterdiscount.Text = discount;
                lblMasterdiscount.Text = discount;
                txtMasterDis.Text = discount;


                //Calculate Total(GrossAmount + Discount)-----------------------------------------------------------------------------------------------------------------

                String toatal = ((Double.Parse(lblGrossAmt.Text)) - (Double.Parse(discount))).ToString();
                lbltotal.Text = toatal;
                lbltotal.Text = toatal;

                //Calculate nbt(nbt(grn)/((grossamount(grngrossAmount)-discount(GRN)))*total()return-----------------------------------------------------------------------------------------------------------------

                String nbt = ((Double.Parse(lblnbt.Text)) / ((Double.Parse(lblgrosvat.Text)) - (Double.Parse(lblDiscountAm.Text))) * (Double.Parse(lbltotal.Text))).ToString();
                ///MessageBox.Show(nbt);
                lblNBTLast.Text = nbt;
                lblNBTLast.Text = nbt;
                lbltotalamount2.Text = ((Double.Parse(toatal)) + (Double.Parse(nbt))).ToString();
                lbltotalamount2.Text = ((Double.Parse(toatal)) + (Double.Parse(nbt))).ToString();

                //Calculate GRNtotal grnVAT-grnDISCOUNT+NBT-----------------------------------------------------------------------------------------------------------------
                String laternbt = ((Double.Parse(lblgrosvat.Text)) - (Double.Parse(lblDiscountAm.Text)) + (Double.Parse(lblnbt.Text))).ToString();

                //Calculate vat(grnVAT/total*lbltotalamount2)-----------------------------------------------------------------------------------------------------------------

                string vat = ((Double.Parse(lblvat.Text)) / (Double.Parse(laternbt)) * (Double.Parse(lbltotalamount2.Text))).ToString();
                lblVATLast.Text = vat;
                lblVATLast.Text = vat;

                //Calculate net Amount(lbltotalamount2 +vat )-----------------------------------------------------------------------------------------------------------------

                String netAmount = ((Double.Parse(lbltotalamount2.Text)) + (Double.Parse(lblVATLast.Text))).ToString();
                lblNetAmount.Text = netAmount;
                label39.Text = netAmount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = false;
        }

        private void txtreturnqty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtreturnqty_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.KeyValue==13)
            {
                button1.Focus();
            }

            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            samReport s = new samReport();
            s.Show();
        }

        private void lblNetAmount_TextChanged(object sender, EventArgs e)
        {
            //if (Double.Parse(lblNetAmount.Text) > 0)
            //{
            //    button2.Enabled = true;
            //}
        }

        private void lblMasterdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                lblnbt.Focus();
            }
        }

        private void lblNBTLast_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                lblvat.Focus();
            }
        }

        private void lblVATLast_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button10.Focus();
            }
        }

        private void GRNReturn_Load(object sender, EventArgs e)
        {

        }
    }
}
