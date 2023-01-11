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
    public partial class PurchaseOrder : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public PurchaseOrder()
        {
            InitializeComponent();
            GRNCode();
           // panelWalkingCus.Visible = false;
            PnlItemSearch.Visible = false;
            button5.Focus();
        }

        String addlist_discvalue = "";
        String Amount1 = "";


        public void GRNCode()
        {
            #region auto generate GRN Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select Purchase_ID from Purchase_Order";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    txtPurchaseno.Text = "POR1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 Purchase_ID FROM Purchase_Order WHERE Purchase_ID LIKE 'POR%' order by Purchase_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();



                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        //                      MessageBox.Show(no);

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        txtPurchaseno.Text = "POR" + no;

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

         public void addListView()
        {
            try
            {
                #region update ListView..........................................................
                // DiscountChecked();
                for (int i = 0; i <= LstShow.Items.Count - 1; i++)
                {

                    //if equal items
                    if (LstShow.Items[i].SubItems[0].Text == txtItemID.Text)
                    {

                        DialogResult result = MessageBox.Show("This Item alredy in the list.Do you want to update..? ", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (result == DialogResult.Yes)
                        {

                            LstShow.Items[i].SubItems[5].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[5].Text) + Convert.ToDouble(textBox4.Text)));
                            LstShow.Items[i].SubItems[11].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[11].Text) + Convert.ToDouble(Amount1)));
                            LstShow.Items[i].SubItems[10].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[10].Text) + Convert.ToDouble(addlist_discvalue)));
                            LstShow.Items[i].SubItems[6].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[6].Text) + Convert.ToDouble(txtQty.Text)));

                            return;
                        }

                        else
                        {
                            return;
                        }

                    }


                }
                #endregion

                #region discount value checked or false
                // create defaul value in txtdiscount------------------------------------------------------------------------------------------------ 
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";

                }
                //------------------------------------------------------------------------------------------------ 


                // check item id ------------------------------------------------------------------------------------------------ 
                if (txtItemID.Text == "")
                {
                    MessageBox.Show("please select item..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                //------------------------------------------------------------------------------------------------ 


                Double Purches = Double.Parse(txtsellingPrice.Text);
                Double qty = Double.Parse(textBox4.Text);
                Double Discount = Double.Parse(txtDiscount.Text);
                Double qtySelling = Purches * qty;
                Double add = 0;
                Double balance;

                // MessageBox.Show(qtypurches.ToString());


                if (chkItmDiscPer.Checked == true)
                {

                    add = (qtySelling / 100) * Discount;
                    addlist_discvalue = add.ToString();

                    balance = qtySelling - add;

                    Amount1 = balance.ToString();

                    // MessageBox.Show(add.ToString());
                }

                if (chkItmDiscPer.Checked == false)
                {

                    add = qtySelling - Discount;

                    Amount1 = add.ToString();
                    addlist_discvalue = txtDiscount.Text;

                    //MessageBox.Show(addlist_discvalue);

                }
                //balance = qtypurches - add;
                //Amount1 = balance.ToString();

                #endregion



                #region Add to listview for add button
                ListViewItem li;

                li = new ListViewItem(txtItemID.Text);
                li.SubItems.Add(BrCodeID.Text);
                li.SubItems.Add(txtItemName.Text);
                li.SubItems.Add(txtWarranty.Text);
                li.SubItems.Add(txtunit.Text);
                li.SubItems.Add(textBox4.Text);
                li.SubItems.Add(txtQty.Text);
                li.SubItems.Add(txtPurchase.Text);
                li.SubItems.Add(txtsellingPrice.Text);
                //li.SubItems.Add(txtbatch.Text);
                li.SubItems.Add(txtbatch.Text);
                // li.SubItems.Add(txtDiscount.Text);
                li.SubItems.Add(addlist_discvalue);
                li.SubItems.Add(Amount1);



                LstShow.Items.Add(li);




                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
         public void ClearAll()
         {
             #region Clear..................................
             cleartextbox();
             lblGrossAmt.Text = "0.0";
             CusID.Text = "";
             CusAddre.Text = "";
             txtCusName.Text = "";
             txtWcusAdd.Text = "";
             txtwCusName.Text = "";
           
            // txtreportBody.Text = "";
            // txtreportHeader.Text = "";

            
             button2.Enabled = false;
             #endregion
         }
         public void cleartextbox()
         {
             #region Clear textbox------------------------------------------------------------
             txtQty.Text = "0.00";
             txtQty.Enabled = false;
             textBox4.Text = "0.00";
             textBox4.Enabled = false;
             txtDiscount.Text = "0.00";
             txtDiscount.Enabled = false;
             chkItmDiscPer.Enabled = false;
             txtItemID.Text = "";
             txtItemName.Text = "";
             txtWarranty.Text = "";
             txtunit.Text = "";
             txtPurchase.Text = "";
             txtsellingPrice.SelectedIndex = -1;
             txtbatch.Text = "";
             chkItmDiscPer.Checked = false;
             txtsellingPrice.Enabled = false;
            
            
             BrCodeID.Text = "";

             #endregion
         }

         public void CalNetAmount()
         {
             lblNetAmount.Text = Convert.ToString(Convert.ToDouble(lblGrossAmt.Text) - Convert.ToDouble(txtMasterDis.Text) + Convert.ToDouble(txtVat.Text));

         }

         public void GetTotalAmount()
         {
             try
             {
                 decimal gtotal = 0;
                 foreach (ListViewItem lstItem in LstShow.Items)
                 {
                     gtotal += Math.Round(decimal.Parse(lstItem.SubItems[11].Text), 2);
                 }
                 lblGrossAmt.Text = Convert.ToString(gtotal);
                 //lblNetAmount.Text = 
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             }
         }

         public void NBTandVATCalc()
         {
             #region NBT and VAT Calculate
             try
             {
                 //NBT amount
                 double NBTAmount = (Convert.ToDouble(lblTotal.Text) * Convert.ToDouble(txtNBT.Text)) / 100;
                 //VAT amount
                 double VATAmount = ((NBTAmount + Convert.ToDouble(lblTotal.Text)) * Convert.ToDouble(TxtVatPre.Text)) / 100;
                 //totoal TAX amount
                 txtVat.Text = Convert.ToString(NBTAmount + VATAmount);
                 //discount amount
                 txtMasterDis.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(lblTotal.Text));
                 //net amount
                 label39.Text = Convert.ToString(NBTAmount + VATAmount + Convert.ToDouble(lblTotal.Text));
             }
             catch (Exception ex)
             {

             }
             #endregion
         }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region load customer details in gridView
           
            PnlVendorSerch.Visible = true;

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = @"SELECT     VenderID, VenderName, VenderPHAddress
                                        FROM         VenderDetails
                                        WHERE     (ActiveDeactive = '1')";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);

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

        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridViewRow dr = dataGridView3.SelectedRows[0];
            //CusID.Text = dr.Cells[0].Value.ToString();
            //txtCusName.Text = dr.Cells[1].Value.ToString() + " " + dr.Cells[2].Value.ToString();
            //CusAddre.Text = dr.Cells[3].Value.ToString();


            //pnlSearchVenderinGRN.Visible = false;
            //txtreportBody.Enabled = true;
            //txtreportHeader.Enabled = true;

            //checkBox1.Checked = false;
            //txtwCusName.Text = "";
            //txtWcusAdd.Text = "";
            //BtnItmSelect.Enabled = true;

          
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                #region Walking customer chec/uncheck.............................................

                if (checkBox1.Checked == true)
                {
                    PnlVendorSerch.Visible = false;
                    txtwCusName.Enabled = true;

                    txtWcusAdd.Enabled = true;
                    txtCusName.Text = "";
                    CusID.Text = "";
                    CusAddre.Text = "";
                    BtnItmSelect.Enabled = true;
                    txtreportHeader.Enabled = true;
                    txtreportBody.Enabled = true;

                }
                if (checkBox1.Checked == false)
                {
                    //panelWalkingCus.Visible = false;
                    txtwCusName.Text = "";

                    txtWcusAdd.Text = "";
                    txtwCusName.Enabled = false;

                    txtWcusAdd.Enabled = false;
                    BtnItmSelect.Enabled = false;
                    //  txtreportHeader.Enabled = false;
                    //txtreportBody.Enabled = false;
                    if (CusID.Text == "")
                    {
                        txtreportBody.Enabled = false;
                        txtreportHeader.Enabled = false;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            

        }

        private void button8_Click(object sender, EventArgs e)
        {
            PnlVendorSerch.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //panelWalkingCus.Visible = false;
        }

        private void BtnItmSelect_Click(object sender, EventArgs e)
        {
            try
            {
                #region insert data in grideview
                dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);

                PnlItemSearch.BringToFront();
                PnlItemSearch.Visible = true;

                try
                {

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();

                    string VenSelectAll = "SELECT ItmID,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmWarrenty,ItmVendor,ItmVenID,ItmStkUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03,ItemBarCode FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "'";
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
                #endregion

                cleartextbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox16_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void textBox16_KeyUp(object sender, KeyEventArgs e)
        {
            #region insert items data in grideview

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmWarrenty,ItmVendor,ItmVenID,ItmStkUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03,ItemBarCode FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "' AND ItmID LIKE '" + textBox16.Text + "%' OR ItmName like '%" + textBox16.Text + "%' ";
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
            #endregion
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PnlItemSearch.Visible = false;
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region  isert value TextBox from grideview............................
            try
            {


                DataGridViewRow dr = dataGridView2.SelectedRows[0];

                txtItemID.Text = dr.Cells[0].Value.ToString();
                txtItemName.Text = dr.Cells[1].Value.ToString();
                txtWarranty.Text = dr.Cells[5].Value.ToString();
                txtunit.Text = dr.Cells[8].Value.ToString();
                txtPurchase.Text = dr.Cells[9].Value.ToString();
                txtsellingPrice.Text = dr.Cells[10].Value.ToString();

                //-------------------------------------------------------------------------------------------------
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03 FROM NewItemDetails WHERE ItmID='"+txtItemID.Text+"' ";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                txtsellingPrice.Items.Clear();
                while(dr2.Read())
                {
                    txtsellingPrice.Items.Add(dr2[1].ToString());
                    txtsellingPrice.Items.Add(dr2[2].ToString());
                    txtsellingPrice.Items.Add(dr2[3].ToString());
                    txtsellingPrice.Items.Add(dr2[4].ToString());
                }

              

                BrCodeID.Text = dr.Cells[14].Value.ToString();

                if (BrCodeID.Text == "")
                {
                    BrCodeID.Text = dr.Cells[0].Value.ToString();
                }

                string warent = dr.Cells[5].Value.ToString();

                string WarType = warent.Substring(0, 1);
                string WarPeriod = warent.Substring(1);

                if (WarType == "0")
                {
                    txtWarranty.Text = "No Warranty";
                }

                if (WarType == "1")
                {
                    txtWarranty.Text = WarPeriod + " Year(s)";
                }
                if (WarType == "2")
                {
                    txtWarranty.Text = WarPeriod + " Month(s)";
                }

               // BacthCode(dr.Cells[0].Value.ToString());
                PnlItemSearch.Visible = false;
                txtQty.Text = "0.00";
                txtQty.Enabled = true;
                textBox4.Text = "0.00";
                textBox4.Enabled = true;
                txtDiscount.Text = "0.00";
                txtDiscount.Enabled = true;
                chkItmDiscPer.Enabled = true;
                txtsellingPrice.Enabled = true;

                textBox4.Focus();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if ((Convert.ToDouble(textBox4.Text) == 0 && Convert.ToDouble(txtQty.Text) == 0))
            {
                MessageBox.Show("Please add Quantity for the Item", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                textBox4.Focus();
                return;
            }

            decimal x;
            bool isNum=decimal.TryParse(txtsellingPrice.Text, out x);

            if (isNum == false)
            {
                MessageBox.Show("Please add correct order price", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                txtsellingPrice.Focus();
                return;
            }
            

         
            //save button enable
            button2.Enabled = true;
           
            addListView();
            txtsellingPrice.Text="";
            GetTotalAmount();
            cleartextbox();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = true;
            lblSubTotal.Text = lblGrossAmt.Text;
        }

        private void lblSubTotal_TextChanged(object sender, EventArgs e)
        {
            lblTotal.Text = lblSubTotal.Text;
        }

        private void txtDiscountPre_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void TxtVatPre_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void txtsellingPrice_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void txtNBT_TextChanged(object sender, EventArgs e)
        {
            if (txtNBT.Text == "")
            {
                txtNBT.Text = "0";
            }

            NBTandVATCalc();
        }

        private void lblGrossAmt_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
        }

        private void txtDiscountPre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscountPre.Text == "")
                {
                    txtDiscountPre.Text = "0";
                }

                txtCalDiscount.Text = Convert.ToString((Convert.ToDouble(lblSubTotal.Text) / 100) * Convert.ToDouble(txtDiscountPre.Text));

                lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtCalDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCalDiscount.Text == "")
                {
                    txtCalDiscount.Text = "0";
                }

                txtDiscountPre.Text = Convert.ToString((Convert.ToDouble(txtCalDiscount.Text) / Convert.ToDouble(lblSubTotal.Text)) * 100);

                lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtDiscountPre_Leave(object sender, EventArgs e)
        {
            if (txtDiscountPre.Text == "")
            {
                txtDiscountPre.Text = "0";
            }
        }

        private void TxtVatPre_TextChanged(object sender, EventArgs e)
        {
            if (txtVat.Text == "")
            {
                txtVat.Text = "0";
            }
            NBTandVATCalc();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = false;
        }

        private void label39_TextChanged(object sender, EventArgs e)
        {
            lblNetAmount.Text = label39.Text;
        }

        private void lblTotal_TextChanged(object sender, EventArgs e)
        {
            NBTandVATCalc();

            txtDiscount.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(lblTotal.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region  Insert DB............................................

            GRNCode();

            if(txtreportHeader.Text=="" && txtreportBody.Text=="")
            {
                MessageBox.Show("Please Fill Report Header & Body Deatils","Message" );
                return;
            }


            try
            {

                DialogResult result = MessageBox.Show("Are you sure you want to complete the GRN now", "Complete GRN?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {




                    #region insert value to Purchase_Order.........................................................

                    int i;
                    for (i = 0; i <= LstShow.Items.Count - 1; i++)
                    {

                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        String PurchaseAdd = "insert into Purchase_Order(Purchase_ID, ItemNo,ItemName,   SellingPrice, QTY, Itemdiscount, total,  freeQTY)values(@Purchase_ID, @ItemNo,@ItemName,  @SellingPrice, @QTY, @Itemdiscount, @total,  @freeQTY)";
                        SqlCommand cmm = new SqlCommand(PurchaseAdd, cnn);

                            cmm.Parameters.AddWithValue("@Purchase_ID", txtPurchaseno.Text);
                            cmm.Parameters.AddWithValue("@ItemNo", LstShow.Items[i].SubItems[0].Text);
                            cmm.Parameters.AddWithValue("@ItemName", LstShow.Items[i].SubItems[2].Text);
                            cmm.Parameters.AddWithValue("@SellingPrice", LstShow.Items[i].SubItems[8].Text);
                            cmm.Parameters.AddWithValue("@QTY", LstShow.Items[i].SubItems[5].Text);
                            cmm.Parameters.AddWithValue("@Itemdiscount", LstShow.Items[i].SubItems[10].Text);
                            cmm.Parameters.AddWithValue("@total", LstShow.Items[i].SubItems[11].Text);
                            cmm.Parameters.AddWithValue("@freeQTY", LstShow.Items[i].SubItems[6].Text);
                            
                        cmm.ExecuteNonQuery();
                    #endregion
                    }

                    #region inert data to PurchaseOrder_DOCUMEnt-------------------------------------------------------------

                    if (checkBox1.Checked==false)
                    {
                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();
                    String PurchaseAddDoc = @"insert into PurchaseOrder_DOCUME(PurchesID, CusID, CusName, CusAdd, NBT, VAT, DocDiscount, GrandTotal, Other, Date, Header, Body)values('"+txtPurchaseno.Text+"','"+CusID.Text+"','"+txtCusName.Text+"','"+CusAddre.Text+"','"+txtNBT.Text+"','"+TxtVatPre.Text+"','"+txtMasterDis.Text+"','"+lblNetAmount.Text+"','"+txtRemark.Text+"','"+dateTimePicker1.Text+"','"+txtreportHeader.Text+"','"+txtreportBody.Text+"')";
                    SqlCommand cmm1 = new SqlCommand(PurchaseAddDoc, cnn1);
                    cmm1.ExecuteNonQuery();

                    }


                    //--------------------------------------------------------------------------
                    if(checkBox1.Checked==true)
                    {
                        SqlConnection cnn1 = new SqlConnection(IMS);
                        cnn1.Open();
                        String PurchaseAddDoc = @"insert into PurchaseOrder_DOCUME(PurchesID, CusID, CusName, CusAdd, NBT, VAT, DocDiscount, GrandTotal, Other, Date, Header, Body)values('" + txtPurchaseno.Text + "','" + "WK Customer" + "','" + txtwCusName.Text + "','" + txtWcusAdd.Text + "','" + txtNBT.Text + "','" + TxtVatPre.Text + "','" + txtMasterDis.Text + "','" + lblNetAmount.Text + "','" + txtRemark.Text + "','" + dateTimePicker1.Text + "','" + txtreportHeader.Text + "','" + txtreportBody.Text + "')";
                        SqlCommand cmm1 = new SqlCommand(PurchaseAddDoc, cnn1);
                        cmm1.ExecuteNonQuery();
                    }
                    #endregion

                    MessageBox.Show("Added Succesfully", "GRN Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Report_Form----------------------------------------------------------------------------------

                    rptPurchaseOrderReportForm grnfm = new rptPurchaseOrderReportForm();
                    grnfm.PuchaseReport = txtPurchaseno.Text;
                    grnfm.Show();


                    LstShow.Items.Clear();
                    ClearAll();
                    txtreportBody.Text = "";
                    txtreportHeader.Text = "";
                    txtRemark.Text = "";
                    GRNCode();

                }
                if(result==DialogResult.No)
                {               
                    BtnItmSelect.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            


           
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtsellingPrice.Text = "";
            LstShow.Items.Clear();
            ClearAll();
            txtRemark.Text = "";
            checkBox1.Checked = false;
            txtwCusName.Text = "";
            txtWcusAdd.Text = "";
            txtreportBody.Text = "";
            txtreportHeader.Text = "";
            

            if (CusID.Text == "")
            {
                txtreportBody.Enabled = false;
                txtreportHeader.Enabled = false;
            }

        }

        private void txtwCusPost_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region Select item............................................................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03 FROM NewItemDetails WHERE ItmID='" + txtItemID.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                txtsellingPrice.Items.Clear();
                while (dr2.Read())
                {
                    txtsellingPrice.Items.Add(dr2[1].ToString());
                    txtsellingPrice.Items.Add(dr2[2].ToString());
                    txtsellingPrice.Items.Add(dr2[3].ToString());
                    txtsellingPrice.Items.Add(dr2[4].ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void CusID_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtwCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtWcusAdd.Focus();
            }
        }

        private void txtWcusAdd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue == 13)
            //{
            //    BtnItmSelect.Focus();
            //}
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtQty.Focus();
            }
   
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtsellingPrice.Focus();
            }
        }

        private void txtsellingPrice_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                txtDiscount.Focus();
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                chkItmDiscPer.Focus();
            }
        }

        private void chkItmDiscPer_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                button1.Focus();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                if ((Convert.ToDouble(textBox4.Text) == 0 && Convert.ToDouble(txtQty.Text) == 0))
                {
                    MessageBox.Show("Please add Quantity for the Item", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    textBox4.Focus();
                    return;
                }

                //save button enable
                button2.Enabled = true;

                addListView();
                txtsellingPrice.Text = "";
                GetTotalAmount();
                cleartextbox();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AddVendor cusreg = new AddVendor();
            cusreg.Visible = true;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "0.00";
            }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                txtQty.Text = "0.00";
            }
        }

        private void txtsellingPrice_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //{
            //    e.Handled = true;
            //}

            //// only allow one dash point
            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];

                CusID.Text = dr.Cells[0].Value.ToString();
                txtCusName.Text = dr.Cells[1].Value.ToString();
                CusAddre.Text = dr.Cells[2].Value.ToString();

                checkBox1.Checked = false;

                PnlVendorSerch.Visible = false;
                txtreportBody.Enabled = true;
                txtreportHeader.Enabled = true;

                checkBox1.Checked = false;
                txtwCusName.Text = "";
                txtWcusAdd.Text = "";
                BtnItmSelect.Enabled = true;

                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlVendorSerch.Visible = false;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            #region load customer details in gridView

          //  PnlVendorSerch.Visible = true;

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = @"SELECT     VenderID, VenderName, VenderPHAddress
                                        FROM         VenderDetails
                                        WHERE     (VenderID LIKE '%" + textBox2.Text + "%' OR VenderName LIKE '%" + textBox2.Text + "%') AND (ActiveDeactive = '1')";

                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);

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

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
