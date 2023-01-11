using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;
using System.Security.Cryptography; // auto generate number reference

namespace Inventory_Control_System
{
    public partial class GRN_WholeSaleItem : Form
    {
        public GRN_WholeSaleItem()
        {
            InitializeComponent();
        }

        String addlist_discvalue = "";
        String Amount1 = "";

        

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public void NBTandVATCalc()
        {
            try
            {
                //NBT amount
                double NBTAmount = (Convert.ToDouble(lblTotal.Text) * Convert.ToDouble(txtNBT.Text)) / 100;
                //VAT amount
                double VATAmount = ((NBTAmount + Convert.ToDouble(lblTotal.Text)) * Convert.ToDouble(TxtVatPre.Text)) / 100;
                //totoal TAX amount
                txtVat.Text = Convert.ToString(NBTAmount + VATAmount);
                //discount amount
                txtMasterDis.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text)-Convert.ToDouble(lblTotal.Text));
                //net amount
                label39.Text = Convert.ToString(NBTAmount + VATAmount + Convert.ToDouble(lblTotal.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


      

        private void GRNWholeSaleItem_Load(object sender, EventArgs e)
        {
            GRNCode();
            button5.Focus();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region load vendor details in gridView
            pnlSearchVenderinGRN.Visible = true;

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress FROM  VenderDetails";
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

        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
            #region search vendor in grideview textbox
            try
            {


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress FROM  VenderDetails WHERE VenderID LIKE '%" + textBox14.Text + "%' OR VenderName LIKE '%" + textBox14.Text + "%' ";
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

        private void button8_Click(object sender, EventArgs e)
        {
            pnlSearchVenderinGRN.Visible = false;
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region venderDetails fill in Textbox
            try
            {
                DataGridViewRow dr = dataGridView3.SelectedRows[0];
                VenID.Text = dr.Cells[0].Value.ToString();
                txtVenAddress.Text = dr.Cells[1].Value.ToString();
                VenAddre.Text = dr.Cells[2].Value.ToString();


                pnlSearchVenderinGRN.Visible = false;

                BtnItmSelect.Enabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion

            txtcreditPerion.Focus();
        }

        private void BtnItmSelect_Click(object sender, EventArgs e)
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
        }

        private void textBox16_KeyUp(object sender, KeyEventArgs e)
        {
           

                //string VenSelectAll = "SELECT ItmID,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmWarrenty,ItmVendor,ItmVenID,ItmStkUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmDisc02,ItmDisc03  FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "' AND ItmID LIKE '" + textBox16.Text + "%' OR ItmName like '%" + textBox16.Text + "%' ";
                //SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                //SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView2.Rows.Clear();

            #region insert data in grideview
           
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

                lblDis1.Text = dr.Cells[11].Value.ToString();
                lblDis2.Text = dr.Cells[12].Value.ToString();
                lblDis3.Text = dr.Cells[13].Value.ToString();

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
                BacthCode(dr.Cells[0].Value.ToString());
                PnlItemSearch.Visible = false;
                txtQty.Text = "0.00";
                txtQty.Enabled = true;
                textBox4.Text = "0.00";
                textBox4.Enabled = true;
                txtDiscount.Text = "0.00";
                txtDiscount.Enabled = true;
                chkItmDiscPer.Enabled = true;

                textBox4.Focus();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion

        }
        public void ClearAll()
        {
            cleartextbox();

            VenID.Text = "";
            txtVenAddress.Text = "";
            VenAddre.Text = "";
            txtcreditPerion.Text = "0";
            txtBillNum.Text = "";
            txtBillDate.Text = "";
            button2.Enabled = false;
            
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
            txtsellingPrice.Text = "";
            txtbatch.Text = "";
            chkItmDiscPer.Checked = false;

            BrCodeID.Text = "";

            #endregion
        }
        public void GRNCode()
        {
            #region auto generate GRN Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select GRNNumber from GRNWholesaleItems";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    txtGRNno.Text = "GRN1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 GRNNumber FROM GRNWholesaleItems WHERE GRNNumber LIKE 'GRN%' order by GRNNumber DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();



                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        //                      MessageBox.Show(no);

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        txtGRNno.Text = "GRN" + no;

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
                double New_Bal=0;
                double Debit_Balance = 0;


                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance,Debit_Balance FROM vender_Payment WHERE (VenderID = '" + VenID.Text + "') ORDER BY AutoNum DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    if (dr7.Read())
                    {
                        LastBalance = Convert.ToDouble( dr7[0].ToString());
                        Debit_Balance = Convert.ToDouble(dr7[1].ToString());
                    }
                    cmd1.Dispose();
                    dr7.Close();
                    Conn.Close();

                //======================================

                    #region New Vendor PAyments add

                //balance calc-----------------------------
                    New_Bal = LastBalance - Convert.ToDouble(lblNetAmount.Text);

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();

                    string Vend_DebitPaymet = @"INSERT INTO vender_Payment(VenderID, DocNumber, Credit_Amount,Debit_Balance, Debit_Amount, Balance, Date) 
                                                VALUES  ('" + VenID.Text + "','" + txtGRNno.Text + "','" + lblNetAmount.Text + "','0','" + Debit_Balance + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

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

        public void BacthCode(string ItemIDNum)
        {
            #region auto generate Batch Code...........................................
            try
            {
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select BatchNumber from GRNWholesaleItems WHERE ItemID='" + ItemIDNum + "'";
            //";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                txtbatch.Text = "BTH1001";

                cmd.Dispose();
                dr.Close();

            }

            else
            {

                cmd.Dispose();
                dr.Close();

                string sql1 = " SELECT TOP 1 BatchNumber FROM GRNWholesaleItems WHERE ItemID='" + ItemIDNum + "'  order by BatchNumber DESC ";//
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string OrderNumOnly1 = no.Substring(3);

                    no = (Convert.ToInt32(OrderNumOnly1) + 1).ToString();

                    txtbatch.Text = "BTH" + no;

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

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDouble(textBox4.Text) == 0 && Convert.ToDouble(txtQty.Text) == 0))
            {
                MessageBox.Show("Please add Quantity for the Item","Error",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                textBox4.Focus();
                return;
            }

            //save button enable
           button2.Enabled = true;

            DiscountChecked();

            addListView();
            GetTotalAmount();
            cleartextbox();

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


                Double Purches = Double.Parse(txtPurchase.Text);
                Double qty = Double.Parse(textBox4.Text);
                Double Discount = Double.Parse(txtDiscount.Text);
                Double qtypurches = Purches * qty;
                Double add = 0;
                Double balance;

                // MessageBox.Show(qtypurches.ToString());


                if (chkItmDiscPer.Checked == true)
                {

                    add = (qtypurches / 100) * Discount;
                    addlist_discvalue = add.ToString();

                    balance = qtypurches - add;

                    Amount1 = balance.ToString();

                    // MessageBox.Show(add.ToString());
                }

                if (chkItmDiscPer.Checked == false)
                {

                    add = qtypurches - Discount;

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
                //li.SubItems.Add(txtDiscount.Text);
                li.SubItems.Add(addlist_discvalue);
                li.SubItems.Add(Amount1);
                li.SubItems.Add(lblDis1.Text);
                li.SubItems.Add(lblDis2.Text);
                li.SubItems.Add(lblDis3.Text);


                LstShow.Items.Add(li);

                //LstShow.Columns.Add("Code");
                //LstShow.Columns.Add("Name");
                //LstShow.Columns.Add("Warranty");
                //LstShow.Columns.Add("Unit");
                //LstShow.Columns.Add("Qty");
                //LstShow.Columns.Add("FreeQty");
                //LstShow.Columns.Add("PurchasePrice");
                //LstShow.Columns.Add("SellingPrice");
                //LstShow.Columns.Add("BatchID");
                //LstShow.Columns.Add("discount");
                //LstShow.Columns.Add("Amount");

                //ListViewItem item = new ListViewItem(new[] { txtItemID.Text, txtItemName.Text, txtWarranty.Text, txtunit.Text, textBox4.Text, txtQty.Text, txtPurchase.Text, txtsellingPrice.Text, txtbatch.Text, txtDiscount.Text, Amount1 });
                //LstShow.Items.Add(item);



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }



        public void DiscountChecked()
        {

        }
        //lblTotal.Text = balance.ToString();




        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {


        }

        private void chkItmDiscPer_CheckedChanged(object sender, EventArgs e)
        {
            DiscountChecked();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblGrossAmt_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox4TAXcalcu_Enter(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = true;
            lblSubTotal.Text = lblGrossAmt.Text;
        }
       
        private void chkDisc_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox13_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            
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

        private void txtNBT_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtVatGrn_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = false;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            // only allow one decimal point
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

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            // only allow one decimal point
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
            
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0";
            }
        }


        public void checklistviewValues()
        {
            #region Check items in the list view=====================================

            //for (int i = 0; i <= LstShow.Items.Count - 1; i++)
            //{
            //    if equal items
            //        if (LstShow.Items[i].SubItems[0].Text == LstShow.Items[i].SubItems[0].Text)
            //        {

            //            MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }

            //}

            #endregion
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                txtPurchase.Focus();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void GetTotalAmount()
        {
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in LstShow.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[11].Text),2);
            }
            lblGrossAmt.Text = Convert.ToString(gtotal);
                //lblNetAmount.Text = 
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
                DiscountChecked();

                addListView();
                GetTotalAmount();
                cleartextbox();
            }
        }

        public void CalNetAmount()
        {
            lblNetAmount.Text = Convert.ToString(Convert.ToDouble(lblGrossAmt.Text) - Convert.ToDouble(txtMasterDis.Text) + Convert.ToDouble(txtVat.Text));

        }

        private void lblGrossAmt_TextChanged(object sender, EventArgs e)
        {
            //lblNetAmount.Text = lblGrossAmt.Text;
            CalNetAmount();

        }

        private void lblTotal_TextChanged(object sender, EventArgs e)
        {
            NBTandVATCalc();
        }

        private void LstShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LstShow.SelectedItems[0].Remove();

            if (LstShow.Items.Count == 0)
            {
                button2.Enabled = false;
            }

            GetTotalAmount();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            GRNCode();

            if (txtBillNum.Text == "")
            {
                MessageBox.Show("please Complete the Bill number","Error Bill Number",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtBillNum.Focus();
                return;
            }

            if (txtBillDate.Text == "")
            {
                MessageBox.Show("please Complete the Billing Date", "Error Bill Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBillDate.Focus();
                return;
            }
            


            try
            {

                DialogResult result = MessageBox.Show("Are you sure you want to complete the GRN now","Complete GRN?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    GRNCode();

                    string printingGRN_Number = txtGRNno.Text;

                    int i;

                    #region add items to the wholelase Items table========================================================

                    for (i = 0; i <= LstShow.Items.Count - 1; i++)
                    {
                        BacthCode(LstShow.Items[i].SubItems[0].Text);

                        SqlConnection con3 = new SqlConnection(IMS);
                        con3.Open();
                        string InsertItemsToStock = @"INSERT INTO GRNWholesaleItems(GRNNumber,ItemID,BarCodeID,ItemWarrenty,ItemAdded, PerchPrice,SellingPrice,BatchNumber,DiscountAmount,NetAmount,ItmDisc01, ItmDisc02,ItmDisc03,AvailbleItemCount ) 
                                                    VALUES(@GRNNumber,@ItemID,@BarCodeID,@ItemWarrenty,@ItemAdded, @PerchPrice,@SellingPrice,@BatchNumber,@DiscountAmount,@NetAmount,@ItmDisc01, @ItmDisc02,@ItmDisc03,@AvailbleItemCount )";
                        SqlCommand cmd3 = new SqlCommand(InsertItemsToStock, con3);

                        cmd3.Parameters.AddWithValue("GRNNumber", txtGRNno.Text);
                        cmd3.Parameters.AddWithValue("ItemID", LstShow.Items[i].SubItems[0].Text);
                        cmd3.Parameters.AddWithValue("BarCodeID", LstShow.Items[i].SubItems[1].Text);
                        cmd3.Parameters.AddWithValue("ItemWarrenty", LstShow.Items[i].SubItems[3].Text);
                        cmd3.Parameters.AddWithValue("ItemAdded", LstShow.Items[i].SubItems[5].Text);
                        cmd3.Parameters.AddWithValue("PerchPrice", LstShow.Items[i].SubItems[7].Text);
                        cmd3.Parameters.AddWithValue("SellingPrice", LstShow.Items[i].SubItems[8].Text);
                        cmd3.Parameters.AddWithValue("BatchNumber", txtbatch.Text);
                        cmd3.Parameters.AddWithValue("DiscountAmount", LstShow.Items[i].SubItems[10].Text);
                        cmd3.Parameters.AddWithValue("NetAmount", LstShow.Items[i].SubItems[11].Text);
                        cmd3.Parameters.AddWithValue("ItmDisc01", LstShow.Items[i].SubItems[12].Text);
                        cmd3.Parameters.AddWithValue("ItmDisc02", LstShow.Items[i].SubItems[13].Text);
                        cmd3.Parameters.AddWithValue("ItmDisc03", LstShow.Items[i].SubItems[14].Text);
                        cmd3.Parameters.AddWithValue("AvailbleItemCount", LstShow.Items[i].SubItems[5].Text);

                        cmd3.ExecuteNonQuery();

                        //stock count update===============================================================================
                        string availableStock = "";

                        SqlConnection conn = new SqlConnection(IMS);
                        conn.Open();

                        string SelectStkCOunt = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID='" + LstShow.Items[i].SubItems[0].Text + "'";
                        SqlCommand cmdd = new SqlCommand(SelectStkCOunt, conn);
                        SqlDataReader drr = cmdd.ExecuteReader();

                        if (drr.Read())
                        {
                            availableStock = drr[0].ToString();
                        }



                        //update

                        //calculate the stock ammount after adding new items
                        string newstk = Convert.ToString(Convert.ToDouble(availableStock) + Convert.ToDouble(LstShow.Items[i].SubItems[5].Text));

                        SqlConnection conx = new SqlConnection(IMS);
                        conx.Open();
                        string InsertItemsToStockCount = "UPDATE CurrentStock SET AvailableStockCount ='" + newstk + "' WHERE ItemID ='" + LstShow.Items[i].SubItems[0].Text + "'";
                        SqlCommand cmdx = new SqlCommand(InsertItemsToStockCount, conx);
                        cmdx.ExecuteNonQuery();



                        if (conx.State == ConnectionState.Open)
                        {
                            conx.Close();
                        }
                        //====================================================================================================


                        if (con3.State == ConnectionState.Open)
                        {
                            con3.Close();
                        }

                        //==================================================================================
                    }

                    #endregion



                    #region Add GRN Details to the system====================================================================

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string addgrnwholeSale = @"INSERT INTO GRN_amount_Details(GRN_No, Vender_ID, Date, GrossAmount, Discount, NBT, VAT, Net_Amount, Payment_Method, Credit_Period, Bill_No, Bill_Date, Comment,GRNUser) Values('" + txtGRNno.Text + "','" + VenID.Text + "','" + DateTime.Now.ToString() + "','" + lblGrossAmt.Text + "','" + txtMasterDis.Text + "','" + txtNBT.Text + "','" + TxtVatPre.Text + "','" + lblNetAmount.Text + "','Pending','" + txtcreditPerion.Text + "','" + txtBillNum.Text + "','" + txtBillDate.Text + "','" + txtRemark.Text + "','" + LgUser.Text + "') ";
                    SqlCommand cmd1 = new SqlCommand(addgrnwholeSale, con1);
                    cmd1.ExecuteNonQuery();

                    if (con1.State == ConnectionState.Open)
                    {
                        con1.Close();
                    }

                    #endregion


                    #region Payment Detaiils to the System====================================================================

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();
                    string addgrnwholeSalePaymnets = @"INSERT INTO GRNInvoicePaymentDetails(GRNID, GrossAmount, Net_Amount,  PayBalance, 
                                    GRNTotDiscount, PAymentStatus,GRNBy) 
                                    Values('" + txtGRNno.Text + "','" + lblGrossAmt.Text + "','" + lblNetAmount.Text + "','" + lblNetAmount.Text + "','" + txtMasterDis.Text + "','Not_Completed','" + LgUser.Text + "') ";


                    SqlCommand cmd2 = new SqlCommand(addgrnwholeSalePaymnets, con2);
                    cmd2.ExecuteNonQuery();

                    if (con2.State == ConnectionState.Open)
                    {
                        con2.Close();
                    }

                    #endregion

                   

                    #region add free Items As new batch======================================================================

                    for (i = 0; i <= LstShow.Items.Count - 1; i++)
                    {
                        //if Item has free items_______________________________________
                        if (Convert.ToDouble(LstShow.Items[i].SubItems[6].Text) != 0)
                        {
                            //genarate new batch ID________________________________________________________________________________

                            BacthCode(LstShow.Items[i].SubItems[0].Text);

                           // MessageBox.Show(LstShow.Items[i].SubItems[0].Text);
                          //  MessageBox.Show(txtbatch.Text);

                            //-------------------------------------------------------------------------------------------------------

                            //insert to the DB___________________________________________________________________________________

                            SqlConnection con4 = new SqlConnection(IMS);
                            con4.Open();
                            string InsertItemsToStockFree = @"INSERT INTO GRNWholesaleItems (GRNNumber,ItemID,BarCodeID,ItemWarrenty,ItemAdded, PerchPrice,SellingPrice,BatchNumber,DiscountAmount,NetAmount,ItmDisc01, ItmDisc02,ItmDisc03,AvailbleItemCount ) 
                                                    VALUES(@GRNNumber,@ItemID,@BarCodeID,@ItemWarrenty,@ItemAdded, @PerchPrice,@SellingPrice,@BatchNumber,@DiscountAmount,@NetAmount,@ItmDisc01, @ItmDisc02,@ItmDisc03,@AvailbleItemCount )";
                            SqlCommand cmd4 = new SqlCommand(InsertItemsToStockFree, con4);

                            cmd4.Parameters.AddWithValue("GRNNumber", txtGRNno.Text);
                            cmd4.Parameters.AddWithValue("ItemID", LstShow.Items[i].SubItems[0].Text);
                            cmd4.Parameters.AddWithValue("BarCodeID", LstShow.Items[i].SubItems[1].Text);
                            cmd4.Parameters.AddWithValue("ItemWarrenty", LstShow.Items[i].SubItems[3].Text);
                            cmd4.Parameters.AddWithValue("ItemAdded", LstShow.Items[i].SubItems[6].Text);
                            cmd4.Parameters.AddWithValue("PerchPrice", "0.00");
                            cmd4.Parameters.AddWithValue("SellingPrice", LstShow.Items[i].SubItems[8].Text);
                            cmd4.Parameters.AddWithValue("BatchNumber", txtbatch.Text);
                            cmd4.Parameters.AddWithValue("DiscountAmount", "0.00");
                            cmd4.Parameters.AddWithValue("NetAmount", "0.00");
                            cmd4.Parameters.AddWithValue("ItmDisc01", LstShow.Items[i].SubItems[12].Text);
                            cmd4.Parameters.AddWithValue("ItmDisc02", LstShow.Items[i].SubItems[13].Text);
                            cmd4.Parameters.AddWithValue("ItmDisc03", LstShow.Items[i].SubItems[14].Text);
                            cmd4.Parameters.AddWithValue("AvailbleItemCount", LstShow.Items[i].SubItems[6].Text);

                            cmd4.ExecuteNonQuery();

                            //==================================================================================
                            //stock count update===============================================================================
                            string availableStock = "";

                            SqlConnection conn = new SqlConnection(IMS);
                            conn.Open();

                            string SelectStkCOunt = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID='" + LstShow.Items[i].SubItems[0].Text + "'";
                            SqlCommand cmdd = new SqlCommand(SelectStkCOunt, conn);
                            SqlDataReader drr = cmdd.ExecuteReader();

                            if (drr.Read())
                            {
                                availableStock = drr[0].ToString();
                            }



                            //update

                            //calculate the stock ammount after adding new items
                            string newstk = Convert.ToString(Convert.ToDouble(availableStock) + Convert.ToDouble(LstShow.Items[i].SubItems[6].Text));

                            SqlConnection conx = new SqlConnection(IMS);
                            conx.Open();
                            string InsertItemsToStockCount = "UPDATE CurrentStock SET AvailableStockCount ='" + newstk + "' WHERE ItemID ='" + LstShow.Items[i].SubItems[0].Text + "'";
                            SqlCommand cmdx = new SqlCommand(InsertItemsToStockCount, conx);
                            cmdx.ExecuteNonQuery();



                            if (conx.State == ConnectionState.Open)
                            {
                                conx.Close();
                            }
                            //====================================================================================================

                            if (con4.State == ConnectionState.Open)
                            {
                                con4.Close();
                            }
                            //-----------------------------------------------------------------------------------------------------------------


                        }
                    }

                    #endregion

                    #region Vender Paymet add to the Database------------------------------------------

                    Debit_Credit_Balance();

                    #endregion

                    MessageBox.Show("Added Succesfully", "GRN Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RptGRNFrm grnfm = new RptGRNFrm();
                    grnfm.PrintingGRNNumber = printingGRN_Number;
                    grnfm.PrintCopyDetails = "Original Copy";
                    grnfm.GRN_Type = "Whole_Sale";
                    grnfm.Show();

                    LstShow.Items.Clear();
                    ClearAll();
                    txtRemark.Text = "";

                    GRNCode();

                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
               
        }

        private void txtPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtDiscount.Focus();
            }
        }

        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBillDate.Focus();
                
            }
        }

        private void textBox4_CursorChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "";
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                txtQty.Text = "";
            }
        }

        private void txtNBT_TextChanged(object sender, EventArgs e)
        {
            NBTandVATCalc();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
           
        }

        private void txtVatGrn_TextChanged(object sender, EventArgs e)
        {
            NBTandVATCalc();
        }

        private void txtDiscountPre_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscountPre.Text == "")
            {
                txtDiscountPre.Text = "0";
            }

            txtCalDiscount.Text = Convert.ToString((Convert.ToDouble(lblSubTotal.Text) / 100) * Convert.ToDouble(txtDiscountPre.Text));

            lblTotal.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(txtCalDiscount.Text));
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

        private void lblTotal_TextChanged_1(object sender, EventArgs e)
        {
            NBTandVATCalc();

            txtDiscount.Text = Convert.ToString(Convert.ToDouble(lblSubTotal.Text) - Convert.ToDouble(lblTotal.Text));
        }

        private void txtNBT_TextChanged_1(object sender, EventArgs e)
        {

            if (txtNBT.Text == "")
            {
                txtNBT.Text = "0";
            }

            NBTandVATCalc();
        }

        private void TxtVatPre_TextChanged(object sender, EventArgs e)
        {
            if (TxtVatPre.Text == "")
            {
                TxtVatPre.Text = "0";
            }

            NBTandVATCalc();
        }

        private void label39_TextChanged(object sender, EventArgs e)
        {
            lblNetAmount.Text = label39.Text;
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            groupBox4TAXcalcu.Visible = false;
        }

        private void txtMasterDis_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();
        }

        private void LstShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcreditPerion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBillNum.Focus();
            }
        }

        private void txtBillDate_TextChanged(object sender, EventArgs e)
        {
            BtnItmSelect.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PnlItemSearch.Visible = false;
        }

        private void txtBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnItmSelect.Focus();
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

        private void txtPurchase_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtcreditPerion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lblSubTotal_TextChanged(object sender, EventArgs e)
        {
            lblTotal.Text = lblSubTotal.Text;
        }

        private void txtDiscountPre_Leave(object sender, EventArgs e)
        {
            if (txtDiscountPre.Text == "")
            {
                txtDiscountPre.Text = "0";
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

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
                button10.Focus();
            }
        }

        private void txtDiscountPre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCalDiscount.Focus();
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {

            AddVendor adVe = new AddVendor();
            FormStatus.isSubFormOpen = true;
            //this.Enabled = false;
            adVe.Visible = true;
            //VenderAdd.Visible = true;
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                txtQty.Text = "0";
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0.0";
            }
        }
    }
}

