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
    public partial class Photocopy : Form
    {
        public Photocopy()
        {
            InitializeComponent();
            
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string Gross_ItemVise_Amount = "";

        public void New_Item_Add_Code()
        {
            #region New_Item_Add_Code...........................
            try
            {
                #region New_Item_Add_Code auto generate...........................................
                try
                {
                    SqlConnection Conn = new SqlConnection(IMS);
                    Conn.Open();


                    //=====================================================================================================================
                    string sql = "SELECT CopyID FROM PhotoCopy_Details";
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //=====================================================================================================================
                    if (!dr.Read())
                    {
                        lbl_DOcID.Text = "DOC1001";

                        cmd.Dispose();
                        dr.Close();

                    }

                    else
                    {

                        cmd.Dispose();
                        dr.Close();

                        string sql1 = " SELECT TOP 1 CopyID FROM PhotoCopy_Details order by CopyID DESC";
                        SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                        SqlDataReader dr7 = cmd1.ExecuteReader();

                        if (dr7.Read())
                        {
                            string no;
                            no = dr7[0].ToString();

                            string OrderNumOnly = no.Substring(3);

                            no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                            lbl_DOcID.Text = "DOC" + no;

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

            catch (Exception ex)
            {
                MessageBox.Show("Error is genegating Item code","System Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            #endregion
        }

        public void New_Invoice_Code()
        {
            #region New_Invoice_Code...........................
            try
            {
                #region New_Invoice_Cod generate...........................................
               
                    SqlConnection Conn = new SqlConnection(IMS);
                    Conn.Open();


                    //=====================================================================================================================
                    string sql = "SELECT     PhotoC_Invoice_ID FROM  Photocopy_DOC_Details";
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //=====================================================================================================================
                    if (!dr.Read())
                    {
                        Lbl_Copy_Invoice_ID.Text = "IPT1001";

                        cmd.Dispose();
                        dr.Close();

                    }

                    else
                    {

                        cmd.Dispose();
                        dr.Close();

                        string sql1 = " SELECT TOP 1 PhotoC_Invoice_ID FROM  Photocopy_DOC_Details order by PhotoC_Invoice_ID DESC";
                        SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                        SqlDataReader dr7 = cmd1.ExecuteReader();

                        while (dr7.Read())
                        {
                            string no;
                            no = dr7[0].ToString();

                            string OrderNumOnly = no.Substring(3);

                            no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                            Lbl_Copy_Invoice_ID.Text = "IPT" + no;

                        }
                        cmd1.Dispose();
                        dr7.Close();

                    }
                    Conn.Close();
                
                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error is genegating Item code", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }


        public void Calc_Total_Amount()
        {
            #region Calc_Total_Amount...........................
            try
            {
                double Num_Docs =Convert.ToDouble(txt_Copy_Amount.Text);
                double UnitCost = Convert.ToDouble(txt_Unit_Amount.Text);


                txt_Amount.Text =Gross_ItemVise_Amount= Convert.ToString(Num_Docs * UnitCost);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error is Calc_Total_Amount", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void clear_txt()
        {
            #region Add to listview for add button........................

            txt_Copy_ID.Text = "";
            txtCopy_Type.Text = "";
            txt_Copy_Amount.Text = "0.00";
            txt_Unit_Amount.Text = "0.00";
            txt_Amount.Text = "0.00";
            txt_Discount.Text = "0.00";
            Gross_ItemVise_Amount = "0.00";

            txt_Discount.Enabled = false;
            txt_Copy_Amount.Enabled = false;

            

            #endregion
        }


        public void GetTotalAmount()
        {
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in LstShow.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[6].Text), 2);
            }
            lblGrossAmt.Text = Convert.ToString(gtotal);

        }

        public void CalNetAmount()
        {
            lblNetAmount.Text = Convert.ToString(Convert.ToDouble(lblGrossAmt.Text) - Convert.ToDouble(txtMasterDis.Text));
        }

        private void LstShow_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            New_Item_Add_Code();

            TypeAddGroupBox.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TypeAddGroupBox.Visible = false;
        }

        private void Photocopy_Load(object sender, EventArgs e)
        {
            New_Invoice_Code();
        }

        private void cmb_Printing_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region load items price...........................
            try
            {
                #region load items price...........................................

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT  CopyID, Copy_Name, Unit_Amount FROM PhotoCopy_Details WHERE Copy_Name='" + cmb_Printing_type.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
               
                if (dr.Read())
                {
                    txt_Copy_ID.Text = dr[0].ToString();
                    txtCopy_Type.Text = dr[1].ToString();
                    txt_Unit_Amount.Text = dr[2].ToString();
                    lbl_unit_Cost.Text=dr[2].ToString();

                }
                Conn.Close();

                txt_Copy_Amount.Enabled = true;
                txt_Copy_Amount.Text = "0.00";
                txt_Amount.Text = "0.00";
                txt_Discount.Text = "0.00";
                txt_Copy_Amount.Focus();

                txt_Discount.Enabled = true;
                

                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error is load items", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void cmb_Printing_type_Click(object sender, EventArgs e)
        {
            #region load items...........................
            try
            {
                #region load items...........................................
                
                    SqlConnection Conn = new SqlConnection(IMS);
                    Conn.Open();


                    //=====================================================================================================================
                    string sql = "SELECT Copy_Name FROM PhotoCopy_Details";
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //=====================================================================================================================

                    cmb_Printing_type.Items.Clear();
                    while (dr.Read())
                    {
                        cmb_Printing_type.Items.Add(dr[0].ToString());

                    }
                    Conn.Close();
               
                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error is load items", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region New Print type...........................
            try
            {
                #region New Print type...........................................

                New_Item_Add_Code();

                   SqlConnection con5 = new SqlConnection(IMS);
                con5.Open();

                string InsertNew_Print = @"INSERT INTO  PhotoCopy_Details ( CopyID, Copy_Name, Unit_Amount, Active) 
                                        VALUES('" + lbl_DOcID.Text + "','" + ItmName.Text + "','" + Unit_Cost.Text + "','1')";
                SqlCommand cmd5 = new SqlCommand(InsertNew_Print, con5);
                cmd5.ExecuteNonQuery();


                if (con5.State == ConnectionState.Open)
                {
                    cmd5.Dispose();
                    con5.Close();
                }
                #endregion

                MessageBox.Show("Saved Sucsessfully ", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ItmName.Text = "";
                Unit_Cost.Text = "0.00";

                TypeAddGroupBox.Visible = false;


            }

            catch (Exception ex)
            {
                MessageBox.Show("Error is load items", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void txt_Copy_Amount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_Discount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_Copy_Amount_TextChanged(object sender, EventArgs e)
        {
            if(txt_Copy_Amount.Text=="")
            {
                return;
            }

            Calc_Total_Amount();
        }


        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            if (txt_Discount.Text == "")
            {
                return;
            }

            Gross_ItemVise_Amount = Convert.ToString(Convert.ToDouble(txt_Amount.Text) - Convert.ToDouble(txt_Discount.Text));

            if (Convert.ToDouble(Gross_ItemVise_Amount) < 0)
            {
                MessageBox.Show("Total amount can not bo Negative value","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txt_Discount.Text = "";
            }
        }

        private void txt_Amount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                #region pre check Items------------------------------

                if (txt_Copy_Amount.Text == "" || txt_Copy_Amount.Text == "0.00")
                {
                    MessageBox.Show("please enter correct copy amount", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                #endregion


                #region update ListView..........................................................
                // DiscountChecked();
                for (int i = 0; i <= LstShow.Items.Count - 1; i++)
                {

                    //if equal items
                    if (LstShow.Items[i].SubItems[1].Text == txtCopy_Type.Text)
                    {

                        DialogResult result = MessageBox.Show("This Item alredy in the list.Do you want to update..? ", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            LstShow.Items[i].SubItems[2].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[2].Text) + Convert.ToDouble(txt_Copy_Amount.Text)));
                            LstShow.Items[i].SubItems[4].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[4].Text) + Convert.ToDouble(txt_Amount.Text)));
                            LstShow.Items[i].SubItems[5].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[5].Text) + Convert.ToDouble(txt_Discount.Text)));
                            LstShow.Items[i].SubItems[6].Text = Convert.ToString((Convert.ToDouble(LstShow.Items[i].SubItems[6].Text) + Convert.ToDouble(Gross_ItemVise_Amount)));

                            GetTotalAmount();
                            clear_txt();
                            return;
                        }

                        else
                        {
                            GetTotalAmount();
                            clear_txt();
                            return;
                        }

                    }


                }
                #endregion



                #region Add to listview for add button........................

                ListViewItem li;

                li = new ListViewItem(txt_Copy_ID.Text);
                li.SubItems.Add(txtCopy_Type.Text);
                li.SubItems.Add(txt_Copy_Amount.Text);
                li.SubItems.Add(txt_Unit_Amount.Text);
                li.SubItems.Add(txt_Amount.Text);
                li.SubItems.Add(txt_Discount.Text);
                li.SubItems.Add(Gross_ItemVise_Amount);

                txt_Discount.Enabled = false;
                txt_Copy_Amount.Enabled = false;

                LstShow.Items.Add(li);

                #endregion

                GetTotalAmount();

                button2.Enabled = true;

                clear_txt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void txt_Discount_Leave(object sender, EventArgs e)
        {
            if (txt_Discount.Text == "")
            {

                txt_Discount.Text = "0.00";
            
            }
        }

        private void txt_Copy_Amount_Leave(object sender, EventArgs e)
        {
            if (txt_Copy_Amount.Text == "")
            {

                txt_Copy_Amount.Text = "0.00";

            }
        }

        private void txt_Copy_Amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txt_Discount.Focus();
            }
        }

        private void txt_Discount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btn_add.Focus();
            }
        }

        private void lblGrossAmt_TextChanged(object sender, EventArgs e)
        {
            CalNetAmount();

            if (Convert.ToDouble(lblGrossAmt.Text) > 0)
            {
                button2.Enabled = true;
            }

            if (Convert.ToDouble(lblGrossAmt.Text) <= 0)
            {
                button2.Enabled = false;
            }
        }

        private void txtMasterDis_TextChanged(object sender, EventArgs e)
        {
            if (txtMasterDis.Text == "")
            {
                return;
            }

            CalNetAmount();

            if (Convert.ToDouble(lblNetAmount.Text) < 0)
            {
                MessageBox.Show("Totoam net amount cannot be negative","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtMasterDis.Text = "0.00";
                return;
            }
        }

        private void lblGrossAmt_Click(object sender, EventArgs e)
        {

        }

        private void txtMasterDis_Leave(object sender, EventArgs e)
        {
            if (txtMasterDis.Text == "")
            {
                txtMasterDis.Text = "0.00";
            }
        }

        private void LstShow_DoubleClick(object sender, EventArgs e)
        {
            LstShow.SelectedItems[0].Remove();
            GetTotalAmount();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Close();
            New_Invoice_Code();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            New_Invoice_Code();

            try
            {
              

                DialogResult result = MessageBox.Show("Do you want to complete this Document..?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    #region save data in photocopy details...............................



                    try
                    {
                        New_Invoice_Code();

                        SqlConnection conne = new SqlConnection(IMS);
                        conne.Open();
                        String add_Photocopy_DOC_Details = @"INSERT INTO Photocopy_DOC_Details ( PhotoC_Invoice_ID, Gross_Amount, Discount, Net_Amount, Add_User, Timp_Stamp, Doc_Status) 
                                                                VALUES('" + Lbl_Copy_Invoice_ID.Text + "','" + lblGrossAmt.Text + "','" + txtMasterDis.Text + "','" + lblNetAmount.Text + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "','1')";

                        SqlCommand cmdm = new SqlCommand(add_Photocopy_DOC_Details, conne);
                        cmdm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("This error comes from the add_Photocopy_DOC_Details adding. contact your Administrator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    #endregion

                    for (int i = 0; i <= LstShow.Items.Count - 1; i++)
                    {
                        #region add listview details----------------------------------------------

                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string InsertItemsToTable = @"INSERT INTO  PhotoCopy_Items_Details(Ph_Invoice_ID, Copy_ID, Copies, Unit_Price, Gross_Amount, Discount,Itm_Net_Amount) 
                                                    VALUES(@Ph_Invoice_ID, @Copy_ID, @Copies, @Unit_Price, @Gross_Amount, @Discount, @Itm_Net_Amount)";

                        SqlCommand cmd = new SqlCommand(InsertItemsToTable, con);

                        cmd.Parameters.AddWithValue("Ph_Invoice_ID", Lbl_Copy_Invoice_ID.Text);
                        cmd.Parameters.AddWithValue("Copy_ID", LstShow.Items[i].SubItems[0].Text);
                        cmd.Parameters.AddWithValue("Copies", LstShow.Items[i].SubItems[2].Text);
                        cmd.Parameters.AddWithValue("Unit_Price", LstShow.Items[i].SubItems[3].Text);
                        cmd.Parameters.AddWithValue("Gross_Amount", LstShow.Items[i].SubItems[4].Text);
                        cmd.Parameters.AddWithValue("Discount", LstShow.Items[i].SubItems[5].Text);
                        cmd.Parameters.AddWithValue("Itm_Net_Amount", LstShow.Items[i].SubItems[6].Text);

                        cmd.ExecuteNonQuery();

                        #endregion
                    }

                    #region insert Invoice Paymet Details--------------------------------------------------------------------------------------------------------

                    SqlConnection con5 = new SqlConnection(IMS);
                    con5.Open();
                    string InsertInvoicePaymetDetails = @"INSERT INTO InvoicePaymentDetails (InvoiceID,SubTotal,VATpresentage,GrandTotal,PayCash,PayCheck,PayCrditCard,PayDebitCard,PAyCredits,PayBalance,InvoiceDate,InvoiceDiscount) 
                                                        VALUES('" + Lbl_Copy_Invoice_ID.Text + "','" + lblGrossAmt.Text + "','0','" + lblNetAmount.Text + "','" + lblNetAmount.Text + "','0','0','0','0','0','" + System.DateTime.Now.ToString() + "','" + txtMasterDis.Text + "')";
                    SqlCommand cmd5 = new SqlCommand(InsertInvoicePaymetDetails, con5);
                    cmd5.ExecuteNonQuery();


                    if (con5.State == ConnectionState.Open)
                    {
                        cmd5.Dispose();
                        con5.Close();
                    }

                    #endregion

                    MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);




                    //Open print Doc---------------------------------

                    Frm_Photocopy_Print_Invoice rptfrm = new Frm_Photocopy_Print_Invoice();

                    rptfrm.LgDisplayName = LgDisplayName;
                    rptfrm.LgUser = LgUser;
                    rptfrm.InvoiceCopyType = "Original Copy";
                    rptfrm.InvoiceID = Lbl_Copy_Invoice_ID.Text;

                    rptfrm.Visible = true;


                    New_Invoice_Code();

                    LstShow.Items.Clear();
                    lblGrossAmt.Text = "0.00";
                    lbl_unit_Cost.Text = "0.00";
                    New_Invoice_Code();
                    //cmb_Printing_type.SelectedIndex = -1;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ////double x = 14;
            ////double y = 2;

            ////double z = 22 / 7;

            ////MessageBox.Show(Convert.ToString(Convert.ToInt64(z)));
        }
    }
}
