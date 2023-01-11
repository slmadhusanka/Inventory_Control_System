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
    public partial class Quotation_form : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        

        public Quotation_form()
        {
            InitializeComponent();
            QuotationCode();
        }

        String addlist_discvalue = "";
        String Amount1 = "";
        String Amount12 = "";

        public void QuotationCode()
        {
            #region auto generate Quotation  Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select Quotation_ID from Quotation_Details";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    txtQtationID.Text = "QTN1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 Quotation_ID FROM Quotation_Details WHERE Quotation_ID LIKE 'QTN%' order by Quotation_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();



                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        //                      MessageBox.Show(no);

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        txtQtationID.Text = "QTN" + no;

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
            #region Update List View, Discount Value Calculation, Add to List View
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
                            #region Update Amount And Discount....................................................................................

                            Double Purches1 = Double.Parse(txtsellingPrice.Text);
                            Double qty1 = Double.Parse(textBox4.Text);
                            Double Discount1 = Double.Parse(txtDiscount.Text);
                            Double qtySelling1 = Purches1 * qty1;
                            Double add1 = 0;
                            Double balance1;



                            if (chkItmDiscPer.Checked == true)
                            {

                                add1 = (qtySelling1 / 100) * Discount1;
                                addlist_discvalue = add1.ToString();

                                balance1 = qtySelling1 - add1;

                                Amount12 = balance1.ToString();

                                // MessageBox.Show(add.ToString());
                            }

                            if (chkItmDiscPer.Checked == false)
                            {

                                add1 = qtySelling1 - Discount1;

                                Amount12 = add1.ToString();
                                addlist_discvalue = txtDiscount.Text;

                                //MessageBox.Show(addlist_discvalue);

                            }

                           
                            //.......................................................................................
                            #endregion

                            LstShow.Items[i].SubItems[5].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[5].Text) + Convert.ToDouble(textBox4.Text)));
                            LstShow.Items[i].SubItems[11].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[11].Text) + (Convert.ToDouble(Amount12))));
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
            #endregion
        }

        public void GetTotalAmount()
        {
            #region GetTotalAmount.................................................
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


        private void BtnItmSelect_Click(object sender, EventArgs e)
        {
            #region insert data in grideview

            try
            {
               
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
                

                cleartextbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
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
               // MessageBox.Show(VATAmount.ToString());
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

                NBTandVATCalc();

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
            #region add to listview.........................................

            if ((Convert.ToDouble(textBox4.Text) == 0 && Convert.ToDouble(txtQty.Text) == 0))
            {
                MessageBox.Show("Please add Quantity for the Item", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }

            decimal x;
            bool isNum = decimal.TryParse(txtsellingPrice.Text, out x);

            if (isNum == false)
            {
                MessageBox.Show("Please add correct order price", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                txtsellingPrice.Focus();
                return;
            }



            //save button enable
            button2.Enabled = true;

            addListView();
            txtsellingPrice.Text = "";
            GetTotalAmount();
            NBTandVATCalc();  
            cleartextbox();
            BtnItmSelect.Focus();
            #endregion
        }

        private void chkItmDiscPer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = true;

            #region
            try
            {

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String CusID = "select CusID, CusFirstName, CusLastName, CusPersonalAddress from CustomerDetails where CusActiveDeactive='1'";
                SqlCommand cmm = new SqlCommand(CusID, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2],dr[3]);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            
            
            #endregion
        }

        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {
            #region search Customer.................
            try
            {

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String CusID = "select CusID, CusFirstName, CusLastName from CustomerDetails where (CusID like'%" + textBox19.Text + "%' OR CusFirstName like'%" + textBox19.Text + "%' OR CusLastName like'" + textBox19.Text + "') and CusActiveDeactive='1'";
                SqlCommand cmm = new SqlCommand(CusID, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            CusID.Text = dr.Cells[0].Value.ToString();
            txtCusName.Text = dr.Cells[1].Value.ToString() + "  " + dr.Cells[2].Value.ToString();
            CusAddre.Text = dr.Cells[3].Value.ToString();

            PnlCustomerSerch.Visible = false;
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = false;
        }

        private void CusID_TextChanged(object sender, EventArgs e)
        {
            if (CusID.Text != "")
            {
                BtnItmSelect.Enabled = true;
                txtreportBody.Enabled = true;
                txtreportHeader.Enabled = true;
            }
            if (CusID.Text == "")
            {
                BtnItmSelect.Enabled = false;
                txtreportBody.Enabled = false;
                txtreportHeader.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            #region wolking customer....................................

            if (checkBox1.Checked==true)
            {
                CusID.Text = "";
                CusAddre.Text = "";
                txtCusName.Text = "";
                txtwCusName.Enabled = true;
                txtWcusAdd.Enabled = true;
                BtnItmSelect.Enabled = true;
                txtreportBody.Enabled = true;
                txtreportHeader.Enabled = true;

            }
            if (checkBox1.Checked == false)
            {
                CusID.Text = "";
                CusAddre.Text = "";
                txtCusName.Text = "";
                txtwCusName.Enabled = false;
                txtWcusAdd.Enabled = false;
                BtnItmSelect.Enabled = false;
                txtreportBody.Enabled = false;
                txtreportHeader.Enabled = false;

            }
            #endregion
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
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

        private void checkBox1_Click(object sender, EventArgs e)
        {
            #region Walking customer...........................................

            if (checkBox1.Checked == true)
            {
                CusID.Text = "";
                CusAddre.Text = "";
                txtCusName.Text = "";
                txtwCusName.Enabled = true;
                txtWcusAdd.Enabled = true;
                BtnItmSelect.Enabled = true;
                txtreportBody.Enabled = true;
                txtreportHeader.Enabled = true;

            }
            if (checkBox1.Checked == false)
            {
                CusID.Text = "";
                CusAddre.Text = "";
                txtCusName.Text = "";
                txtwCusName.Enabled = false;
                txtWcusAdd.Enabled = false;
                BtnItmSelect.Enabled = false;
                txtreportBody.Enabled = false;
                txtreportHeader.Enabled = false;

            }
            #endregion
        }

        private void lblGrossAmt_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
            if (Double.Parse(lblGrossAmt.Text) == 0)
            {
                txtMasterDis.Text = "0";
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = true;
            txtDiscountPre.Focus();
            lblSubTotal.Text = lblGrossAmt.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = false;
            lblNetAmount.Text = label39.Text;
        }

        private void txtDiscountPre_TextChanged(object sender, EventArgs e)
        {
            #region Discvount..............................................................
            try
            {
                if (txtDiscountPre.Text == "")
                {
                    return;
                }

                txtCalDiscount.Text = Convert.ToString((Convert.ToDouble(lblSubTotal.Text) / 100) * Convert.ToDouble(txtDiscountPre.Text));

                lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion

            if(Double.Parse( lblSubTotal.Text)==0)
            {
                txtDiscountPre.Text = "0";
            }
        }

        private void txtCalDiscount_TextChanged(object sender, EventArgs e)
        {
            #region discount......................................................................
            try
            {
                if (txtCalDiscount.Text == "")
                {
                    return;
                }

                txtDiscountPre.Text = Convert.ToString((Convert.ToDouble(txtCalDiscount.Text) / Convert.ToDouble(lblSubTotal.Text)) * 100);

                lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion


            if (Double.Parse(txtCalDiscount.Text) == 0)
            {
                txtMasterDis.Text = "0";
            }
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

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNBT_TextChanged(object sender, EventArgs e)
        {
            if (txtNBT.Text == "")
            {
                return;
            }

            NBTandVATCalc();
        }

        private void TxtVatPre_TextChanged(object sender, EventArgs e)
        {
            if (txtVat.Text == "")
            {
                return;
            }
            NBTandVATCalc();
        }

        private void lblSubTotal_Click(object sender, EventArgs e)
        {
            lblTotal.Text = lblSubTotal.Text;
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            NBTandVATCalc();

            txtDiscount.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(lblTotal.Text));
        }

        private void label39_Click(object sender, EventArgs e)
        {
            lblNetAmount.Text = label39.Text;
        }

        private void txtDiscountPre_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
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
                TxtVatPre.Focus();
            }
        }

        private void TxtVatPre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button6.Focus();
            }
        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button2.Focus();
            }
        }

        private void LstShow_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = LstShow.SelectedItems[0];
            item.Remove();
            //GetTotalAmount();
            if (LstShow.Items.Count == 0)
            {
                txtsellingPrice.Text = "";
                LstShow.Items.Clear();
                // ClearAll();
                txtRemark.Text = "";
                //checkBox1.Checked = false;
                //txtwCusName.Text = "";
                //txtWcusAdd.Text = "";
                //txtreportBody.Text = "";
                //txtreportHeader.Text = "";
                lblNetAmount.Text = "0";
                txtVat.Text = "0";
                txtMasterDis.Text = "0";
                BtnItmSelect.Enabled = false;
                txtDiscountPre.Text = "0";
                txtCalDiscount.Text = "0";
                lblTotal.Text = "0.00";
                txtNBT.Text = "0";
                TxtVatPre.Text = "0";
                lblSubTotal.Text = "0.00";
                lblGrossAmt.Text = "0.00";
                txtMasterDis.Text = "0";
                txtVat.Text = "0";
                lblNetAmount.Text = "0";
                //  GetTotalAmount();
                // NBTandVATCalc();

            }
            else
            {
                GetTotalAmount();
                NBTandVATCalc();
            }

            #region wolking customer....................................

            if (checkBox1.Checked == true)
            {
                CusID.Text = "";
                CusAddre.Text = "";
                txtCusName.Text = "";
                txtwCusName.Enabled = true;
                txtWcusAdd.Enabled = true;
                BtnItmSelect.Enabled = true;
                txtreportBody.Enabled = true;
                txtreportHeader.Enabled = true;

            }
            if (checkBox1.Checked == false)
            {
                CusID.Text = "";
                CusAddre.Text = "";
                txtCusName.Text = "";
                txtwCusName.Enabled = false;
                txtWcusAdd.Enabled = false;
                BtnItmSelect.Enabled = false;
                txtreportBody.Enabled = false;
                txtreportHeader.Enabled = false;

            }
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region Cancel button Clear.............................

            txtsellingPrice.Text = "";
            LstShow.Items.Clear();
            ClearAll();
            txtRemark.Text = "";
            checkBox1.Checked = false;
            txtwCusName.Text = "";
            txtWcusAdd.Text = "";
            txtreportBody.Text = "";
            txtreportHeader.Text = "";
            lblNetAmount.Text = "0";
            txtVat.Text = "0";
            txtMasterDis.Text = "0";
            BtnItmSelect.Enabled = false;
            txtDiscountPre.Text = "0";
            txtCalDiscount.Text = "0";
            lblTotal.Text = "0.00";
            txtNBT.Text = "0";
            TxtVatPre.Text = "0";
            

            if (CusID.Text == "" || checkBox1.Checked==false)
            {
                txtreportBody.Enabled = false;
                txtreportHeader.Enabled = false;
            }

            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region  Insert DB............................................

          //  QuotationCode();

            if (txtreportHeader.Text == "" && txtreportBody.Text == "")
            {
                MessageBox.Show("Please Fill Report Header & Body Deatils", "Message");
                txtreportHeader.Focus();
                return;
            }

            if(checkBox1.Checked==true)
            {
                if (txtwCusName.Text == "" || txtWcusAdd.Text == "")
                {
                    MessageBox.Show("Please Fill Customer Deatils", "Message");
                    txtCusName.Focus();
                    return;
                }
            }

            try
            {

                DialogResult result = MessageBox.Show("Are you sure you want to complete the GRN now", "Complete GRN?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    QuotationCode();

                    #region insert value to Purchase_Order.........................................................

                    int i;
                    for (i = 0; i <= LstShow.Items.Count - 1; i++)
                    {

                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        String PurchaseAdd = "insert into Quotation_Details(Quotation_ID, ItemNo,ItemName,   SellingPrice, QTY, Itemdiscount, total,  freeQTY)values(@Quotation_ID, @ItemNo,@ItemName,  @SellingPrice, @QTY, @Itemdiscount, @total,  @freeQTY)";
                        SqlCommand cmm = new SqlCommand(PurchaseAdd, cnn);

                        cmm.Parameters.AddWithValue("@Quotation_ID", txtQtationID.Text);
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

                    if (checkBox1.Checked == false)
                    {
                        SqlConnection cnn1 = new SqlConnection(IMS);
                        cnn1.Open();
                        String PurchaseAddDoc = @"insert into Quotation_Details_DOCUME(Quotation_ID, CusID, CusName, CusAdd, NBT, VAT, DocDiscount, GrandTotal, Other, Date, Header, Body)values('" + txtQtationID.Text + "','" + CusID.Text + "','" + txtCusName.Text + "','" + CusAddre.Text + "','" + txtNBT.Text + "','" + TxtVatPre.Text + "','" + txtMasterDis.Text + "','" + lblNetAmount.Text + "','" + txtRemark.Text + "','" + dateTimePicker1.Text + "','" + txtreportHeader.Text + "','" + txtreportBody.Text + "')";
                        SqlCommand cmm1 = new SqlCommand(PurchaseAddDoc, cnn1);
                        cmm1.ExecuteNonQuery();

                    }


                    //--------------------------------------------------------------------------
                    if (checkBox1.Checked == true)
                    {
                        SqlConnection cnn1 = new SqlConnection(IMS);
                        cnn1.Open();
                        String PurchaseAddDoc = @"insert into Quotation_Details_DOCUME(Quotation_ID, CusID, CusName, CusAdd, NBT, VAT, DocDiscount, GrandTotal, Other, Date, Header, Body)values('" + txtQtationID.Text + "','" + "WK Customer" + "','" + txtwCusName.Text + "','" + txtWcusAdd.Text + "','" + txtNBT.Text + "','" + TxtVatPre.Text + "','" + txtMasterDis.Text + "','" + lblNetAmount.Text + "','" + txtRemark.Text + "','" + dateTimePicker1.Text + "','" + txtreportHeader.Text + "','" + txtreportBody.Text + "')";
                        SqlCommand cmm1 = new SqlCommand(PurchaseAddDoc, cnn1);
                        cmm1.ExecuteNonQuery();
                    }
                    #endregion

                    MessageBox.Show("Added Succesfully", "GRN Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Report_Form----------------------------------------------------------------------------------

                    frmQuotationReport qua = new frmQuotationReport();
                    qua.QutaIDPass123 = txtQtationID.Text;
                    qua.Show();


                    #region Cancel button Clear.............................

                    txtsellingPrice.Text = "";
                    LstShow.Items.Clear();
                    ClearAll();
                    txtRemark.Text = "";
                    checkBox1.Checked = false;
                    txtwCusName.Text = "";
                    txtWcusAdd.Text = "";
                    txtreportBody.Text = "";
                    txtreportHeader.Text = "";
                    lblNetAmount.Text = "0";
                    txtVat.Text = "0";
                    txtMasterDis.Text = "0";
                    BtnItmSelect.Enabled = false;
                    txtDiscountPre.Text = "0";
                    txtCalDiscount.Text = "0";
                    lblTotal.Text = "0.00";
                    txtNBT.Text = "0";
                    TxtVatPre.Text = "0";


                    if (CusID.Text == "" || checkBox1.Checked == false)
                    {
                        txtreportBody.Enabled = false;
                        txtreportHeader.Enabled = false;
                    }

                    #endregion
                   
                    if (checkBox1.Checked == false)
                    {
                        CusID.Text = "";
                        CusAddre.Text = "";
                        txtCusName.Text = "";
                        txtwCusName.Enabled = false;
                        txtWcusAdd.Enabled = false;
                        BtnItmSelect.Enabled = false;
                        txtreportBody.Enabled = false;
                        txtreportHeader.Enabled = false;

                    }
                    btnCusSearch.Focus();
                    QuotationCode();
                   
                    

                }
                if (result == DialogResult.No)
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

        private void txtwCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtWcusAdd.Focus();
            }
        }

        private void txtWcusAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtreportHeader.Focus();
            }
        }

        private void lblTotal_TextChanged(object sender, EventArgs e)
        {
            NBTandVATCalc();
        }

        private void lblSubTotal_TextChanged(object sender, EventArgs e)
        {
            #region sub total calculate.................................................
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
            #endregion


            if (Double.Parse(lblSubTotal.Text) == 0)
            {
                txtNBT.Text = "0";
                txtDiscountPre.Text = "0";
                txtCalDiscount.Text = "0";
                TxtVatPre.Text = "0";
                lblSubTotal.Text = "0.00";
                lblGrossAmt.Text = "0.00";
                lblTotal.Text = "0.00";
            }
        }

        private void Quotation_form_Load(object sender, EventArgs e)
        {

        }

        private void LstShow_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtDiscountPre_Leave(object sender, EventArgs e)
        {
            if(txtDiscountPre.Text=="")
            {
                txtDiscountPre.Text = "0";
            }
        }

        private void txtCalDiscount_Leave(object sender, EventArgs e)
        {
            if (txtCalDiscount.Text == "")
            {
                txtCalDiscount.Text = "0";
            }
        }

        private void txtNBT_Leave(object sender, EventArgs e)
        {
            if (txtNBT.Text == "")
            {
                txtNBT.Text = "0";
            }
        }

        private void TxtVatPre_Leave(object sender, EventArgs e)
        {
            if (TxtVatPre.Text == "")
            {
                TxtVatPre.Text = "0";
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            CustomerReg cus = new CustomerReg();
            cus.Visible = true;
        }

        private void lblGrossAmt_Click(object sender, EventArgs e)
        {

        }
    }
}
