using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data.Sql;

namespace Inventory_Control_System
{
    public partial class AddVendor : Form
    {
        public AddVendor()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        

        //Vendor ID Select and Create
        public void getCreateVendorCode()
        {
            try
            {

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select VenderID from VenderDetails";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    VenderID.Text = "VEN1001";
                    cmd.Dispose();
                    dr.Close();
                }
                else
                {
                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = "select MAX(VenderID) from VenderDetails";
                    string sql1 = "select TOP 1 VenderID from VenderDetails ORDER BY VenderID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string VenNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                        VenderID.Text = "VEN" + no;


                    }
                    cmd1.Dispose();
                    dr7.Close();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddVendor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormStatus.isSubFormOpen = false;
           timer1.Stop();
           // this.TopMost = false;
            
            
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void AddVendor_Load(object sender, EventArgs e)
        {
            timer1.Start();
            getCreateVendorCode();
            RbNew.Checked = true;
            RbNew.Enabled = false;

            PnlVendorSerch.BringToFront();


        }

        public void UpdateVendor()
        {
            #region Update a Vendor
            //check box select===============================================================================================================
            string acti;
            if (CkDeactivated.Checked)
            {
                acti = "0";
            }
            else
            {
                acti = "1";
            }
            //================================================================================================================================

            SqlConnection con = new SqlConnection(IMS);
            con.Open();

            string VenUpdate = "UPDATE VenderDetails SET ActiveDeactive = '" + acti + "',VenderName = '" + VenderName.Text + "',CreditPeriod = '" + VendercreditPerid.Text + "',discount = '" + VenderDiscontber.Text + "',BRCno = '" + VenderBRCno.Text + "',VATno = '" + VenderVATno.Text + "',CreditValue = '" + VenderCreditValue.Text + "',VenderPHAddress = '" + VenderPHAddress.Text + "',VenderPTel = '" + VenderPTel.Text + "',VenderPFax = '" + VenderPFax.Text + "',VenderPCountry = '" + VenderPCountry.Text + "',VenderPEmail = '" + VenderPEmail.Text + "',VenderRemarks = '" + VenderRemarks.Text + "' WHERE VenderID='" + VenderID.Text + "'";

            SqlCommand cmd2 = new SqlCommand(VenUpdate, con);
            cmd2.ExecuteNonQuery();

            MessageBox.Show("Successfully Updated the Vendor.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            getCreateVendorCode();

            VenderName.Text = "";
            VendercreditPerid.Text = "0.0";
            
            VenderDiscontber.Text = "0.0";
            VenderBRCno.Text = "0.0";
            VenderVATno.Text = "0.0";
            VenderCreditValue.Text = "0.0";
            VenderPHAddress.Text = "";
            VenderPTel.Text = "";
            VenderPFax.Text = "";
            VenderPCountry.Text = "";
            VenderPEmail.Text = "";
            VenderRemarks.Text = "";

            RbUp.Checked = false;
            RbUp.Enabled = false;
            CkDeactivated.Checked = false;
            CkDeactivated.Enabled = false;
            RbNew.Checked = false;
            RbNew.Enabled = false;


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            #endregion
        }

        

        private void VenderSave_Click(object sender, EventArgs e)
        {
           string no;


           string venID = VenderID.Text;

           string VenNumOnly = venID.Substring(3);

            if (VenderName.Text == "")
            {
                MessageBox.Show("Please enter Vendor's Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VenderName.Focus();
                return;
            }

            if (VenderPTel.Text == "")
            {
                MessageBox.Show("Please enter Vendor's Telephone Number", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VenderPTel.Focus();
                return;
            }

            try
            {
                

                  if (RbNew.Checked == true)
                {


                    #region this is for the generate a item code when it save

                    SqlConnection Conn = new SqlConnection(IMS);
                    Conn.Open();


                    //=====================================================================================================================
                    string sql = "select VenderID from VenderDetails";
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //=====================================================================================================================
                    if (!dr.Read())
                    {
                        VenderID.Text = "VEN1001";
                        cmd.Dispose();
                        dr.Close();
                    }
                    else
                    {
                        cmd.Dispose();
                        dr.Close();

                        // string sql1 = "select MAX(VenderID) from VenderDetails";
                        string sql1 = "select TOP 1 VenderID from VenderDetails ORDER BY VenderID DESC";
                        SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                        SqlDataReader dr7 = cmd1.ExecuteReader();

                        while (dr7.Read())
                        {

                            no = dr7[0].ToString();

                            VenNumOnly = no.Substring(3);

                            no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                            VenderID.Text = "VEN" + no;


                        }
                        cmd1.Dispose();
                        dr7.Close();
                    }
                    Conn.Close();
                    #endregion

                    #region Insert New Vendor
                      //check box select===============================================================================================================
                    string acti;
                    if (CkDeactivated.Checked)
                    {
                        acti = "0";
                    }
                    else
                    {
                        acti = "1";
                    }
                    //================================================================================================================================
                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

                    string VenInsert = @"INSERT INTO VenderDetails(VenderID,ActiveDeactive,VenderName,VenderPHAddress,VenderPTel,VenderPFax,VenderPCountry,VenderPEmail,VenderRemarks,CreditPeriod,CreditValue,discount,BRCno,VATno) VALUES  ('" + VenderID.Text + "','" +acti+ "','" + VenderName.Text + "','" + VenderPHAddress.Text + "','" + VenderPTel.Text + "','" + VenderPFax.Text + "','" + VenderPCountry.Text + "','" + VenderPEmail.Text + "','" + VenderRemarks.Text + "','" + VendercreditPerid.Text + "','" + VenderCreditValue.Text + "','" + VenderDiscontber.Text + "','" + VenderBRCno.Text + "','" + VenderVATno.Text + "')";

                    SqlCommand cmd2 = new SqlCommand(VenInsert, con);
                    cmd2.ExecuteNonQuery();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    #endregion

                    #region New Vendor PAyments add

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();

                    string VenInsertPauments = @"INSERT INTO vender_Payment(VenderID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) VALUES  ('" + VenderID.Text + "','" + VenderID.Text + "','0','0','0','0','" + DateTime.Now.ToString() + "')";

                    SqlCommand cmd21 = new SqlCommand(VenInsertPauments, con1);
                    cmd21.ExecuteNonQuery();

                    #endregion

                    MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    

                    #region Clear Items------------------------------------------

                    VenderID.Text = "";
                    CkDeactivated.Checked=false;
                    VenderName.Text = "";
                    VenderPHAddress.Text = "";
                    VenderPTel.Text = "";
                    VenderPFax.Text = "";
                    VenderPCountry.Text = "";
                    VenderPEmail.Text = "";
                    VenderRemarks.Text = "";
                    VendercreditPerid.Text = "0.0";
                    VenderCreditValue.Text = "0.0";
                    VenderDiscontber.Text = "0.0";
                    VenderBRCno.Text = "0.0";
                    VenderVATno.Text = "0.0";

                    #endregion

                    getCreateVendorCode();

                }
                else if (RbUp.Checked == true && CkDeactivated.Checked == false)
                {
                    UpdateVendor();
                }

                else if(RbUp.Checked==true && CkDeactivated.Checked==true)
                {
                    #region Deactivate a Vendor

                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();


                    DialogResult Result = MessageBox.Show("Are you sure you want to deactivate this Vendor?.", "Deactivated", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Result == DialogResult.Yes)
                    {
                        string VenUpdate = "UPDATE VenderDetails SET ActiveDeactive = '" + 0 + "' WHERE VenderID='" + VenderID.Text + "'";

                        SqlCommand cmd2 = new SqlCommand(VenUpdate, con);
                        cmd2.ExecuteNonQuery();

                        MessageBox.Show("Deactivated the Vendor", "Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        getCreateVendorCode();

                        VenderName.Text = "";
                        VendercreditPerid.Text = "0.0";
                       // VenderComAddress.Text = "";
                        VenderDiscontber.Text = "0.0";
                        VenderBRCno.Text = "0.0";
                        VenderVATno.Text = "0.0";
                        VenderCreditValue.Text = "0.0";
                        VenderPHAddress.Text = "";
                        VenderPTel.Text = "";
                        VenderPFax.Text = "";
                        VenderPCountry.Text = "";
                        VenderPEmail.Text = "";
                        VenderRemarks.Text = "";

                        RbUp.Checked = false;
                        RbUp.Enabled = false;
                        CkDeactivated.Checked = false;
                        CkDeactivated.Enabled = false;
                        RbNew.Checked = false;
                        RbNew.Enabled = false;
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
          
        }

        private void VenderCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //VenderPHAddress.Text = VenderComAddress.Text;
            //VenderPTel.Text = VenderTelNumber.Text;
            //VenderPFax.Text = VenderFaxNum.Text;
            //VenderPCountry.Text = VenderCountry.Text;
            //VenderPEmail.Text = VenderEmail.Text;
        }

        private void VenderSerch_Click(object sender, EventArgs e)
        {
            #region insert data in gridview1
            try
            {
                RbNew.Checked = false;
                PnlVendorSerch.Visible = true;

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress,VenderPTel,VenderPFax,VenderPCountry,VenderPEmail,VenderRemarks,CreditPeriod,CreditValue,discount,BRCno,VATno,ActiveDeactive FROM VenderDetails WHERE ActiveDeactive='" + 1 + "'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12],dr[13]);
                   
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

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlVendorSerch.Visible = false;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //#region value pass text box in vender details
            //try
            //{
            //    DataGridViewRow dr = dataGridView1.SelectedRows[0];
            //    VenderID.Text = dr.Cells[0].Value.ToString();
            //    VenderName.Text = dr.Cells[1].Value.ToString();
            //    VendercreditPerid.Text = dr.Cells[2].Value.ToString();
            //    //VenderComAddress.Text = dr.Cells[3].Value.ToString();
            //    VenderDiscontber.Text = dr.Cells[4].Value.ToString();
            //    VenderBRCno.Text = dr.Cells[5].Value.ToString();
            //    VenderVATno.Text = dr.Cells[6].Value.ToString();
            //    VenderCreditValue.Text = dr.Cells[7].Value.ToString();
            //    VenderPHAddress.Text = dr.Cells[8].Value.ToString();
            //    VenderPTel.Text = dr.Cells[9].Value.ToString();
            //    VenderPFax.Text = dr.Cells[10].Value.ToString();
            //    VenderPCountry.Text = dr.Cells[11].Value.ToString();
            //    VenderPEmail.Text = dr.Cells[12].Value.ToString();
            //    VenderRemarks.Text = dr.Cells[13].Value.ToString();

            //    PnlVendorSerch.Visible = false;

            //    LockVendorTxtBox();
            //    //RbUp.Checked = true;
            //    RbNew.Checked = false;
            //    RbNew.Enabled = true;
            //    RbUp.Enabled = true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
            //#endregion
        }

        public void LockVendorTxtBox()
        {
            VenderName.Enabled = false;
            VendercreditPerid.Enabled = false;
            //VenderComAddress.Enabled = false;
            VenderDiscontber.Enabled = false;
            VenderBRCno.Enabled = false;
            VenderVATno.Enabled = false;
            VenderCreditValue.Enabled = false;
            VenderPHAddress.Enabled = false;
            VenderPTel.Enabled = false;
            VenderPFax.Enabled = false;
            VenderPCountry.Enabled = false;
            VenderPEmail.Enabled = false;
            VenderRemarks.Enabled = false;

            //button1.Enabled = false;
            VenderSave.Enabled = false;
        }

        public void UnLockVendorTxtBox()
        {
            VenderName.Enabled = true;
            VendercreditPerid.Enabled = true;
            //VenderComAddress.Enabled = true;
            VenderDiscontber.Enabled = true;
            VenderBRCno.Enabled = true;
            VenderVATno.Enabled = true;
            VenderCreditValue.Enabled = true;
            VenderPHAddress.Enabled = true;
            VenderPTel.Enabled = true;
            VenderPFax.Enabled = true;
            VenderPCountry.Enabled = true;
            VenderPEmail.Enabled = true;
            VenderRemarks.Enabled = true;

            //button1.Enabled = true;
            VenderSave.Enabled = true;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            #region search vender in grideview

            try
            {
                //textBox2.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress,VenderPTel,VenderPFax,VenderPCountry,VenderPEmail,VenderRemarks,CreditPeriod,CreditValue,discount,BRCno,VATno,ActiveDeactive FROM VenderDetails WHERE ActiveDeactive='" + 1 + "' AND VenderID LIKE '%" + textBox1.Text + "%' OR VenderName LIKE '%" + textBox1.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12],dr[13]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                LockVendorTxtBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
                #endregion
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

            ////try
            ////{
            ////    textBox1.Text = "";

            ////    SqlConnection con1 = new SqlConnection(IMS);
            ////    con1.Open();

            ////    string VenSelectAll = "SELECT VenderID,VenderName,VenderComName,VenderComAddress,VenderTelNumber,VenderFaxNum,VenderCountry,VenderEmail,VenderPHAddress,VenderPTel,VenderPFax,VenderPCountry,VenderPEmail,VenderRemarks FROM VenderDetails WHERE ActiveDeactive='" + 1 + "' AND VenderName LIKE '" + textBox2.Text + "%'";
            ////    SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
            ////    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            ////    dataGridView1.Rows.Clear();

            ////    while (dr.Read() == true)
            ////    {
            ////        dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13]);

            ////    }

            ////    if (con1.State == ConnectionState.Open)
            ////    {
            ////        con1.Close();
            ////    }

            ////    LockVendorTxtBox();
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            ////}
        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {

            if (RbUp.Checked == true)
            {
                CkDeactivated.Enabled = true;
                UnLockVendorTxtBox();
                VenderSave.Text = "&Update";
            }
            if (RbUp.Checked != true)
            {
                CkDeactivated.Enabled = false;
                VenderSave.Text = "&Save";
            }
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            CkDeactivated.Checked = false;
            getCreateVendorCode();

            UnLockVendorTxtBox();

            VenderName.Text = "";
            VendercreditPerid.Text = "0.0";
            //VenderComAddress.Text = "";
            VenderDiscontber.Text = "0.0";
            VenderBRCno.Text = "0.0";
            VenderVATno.Text = "0.0";
            VenderCreditValue.Text = "0.0";
            VenderPHAddress.Text = "";
            VenderPTel.Text = "";
            VenderPFax.Text = "";
            VenderPCountry.Text = "";
            VenderPEmail.Text = "";
            VenderRemarks.Text = "";

            RbUp.Enabled = false;
        }

        private void CkDeactivated_CheckedChanged(object sender, EventArgs e)
        {
            if (CkDeactivated.Checked == true && RbUp.Checked==true)
            {
                VenderSave.Text = "Deactivate";
            }

            if (CkDeactivated.Checked == false && RbUp.Checked==true)
            {
                VenderSave.Text = "Update";
            }
        }

        private void VenderAddNew_Click(object sender, EventArgs e)
        {
            getCreateVendorCode();

            UnLockVendorTxtBox();

            VenderName.Text = "";
            VendercreditPerid.Text = "0.0";
            //VenderComAddress.Text = "";
            VenderDiscontber.Text = "0.0";
            VenderBRCno.Text = "0.0";
            VenderVATno.Text = "0.0";
            VenderCreditValue.Text = "0.0";
            VenderPHAddress.Text = "";
            VenderPTel.Text = "";
            VenderPFax.Text = "";
            VenderPCountry.Text = "";
            VenderPEmail.Text = "";
            VenderRemarks.Text = "";

            RbUp.Enabled = false;
            RbNew.Enabled = true;
            RbNew.Checked = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void dataGridView1_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region dataGridView1_RowHeaderMouseDoubleClick
           
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                if (dr.Cells[13].Value.ToString()=="0")
                {
                    CkDeactivated.Checked = true;
                   // CkDeactivated.Text = "Do you need to Deactivate the Vendor? ";
                }


                VenderID.Text = dr.Cells[0].Value.ToString();
               // CkDeactivated.Text = dr.Cells[13].Value.ToString();
                VenderName.Text = dr.Cells[1].Value.ToString();
                VendercreditPerid.Text = dr.Cells[8].Value.ToString();
                //VenderComAddress.Text = dr.Cells[3].Value.ToString();
                VenderDiscontber.Text = dr.Cells[10].Value.ToString();
                VenderBRCno.Text = dr.Cells[11].Value.ToString();
                VenderVATno.Text = dr.Cells[12].Value.ToString();
                VenderCreditValue.Text = dr.Cells[9].Value.ToString();
                VenderPHAddress.Text = dr.Cells[2].Value.ToString();
                VenderPTel.Text = dr.Cells[3].Value.ToString();
                VenderPFax.Text = dr.Cells[4].Value.ToString();
                VenderPCountry.Text = dr.Cells[5].Value.ToString();
                VenderPEmail.Text = dr.Cells[6].Value.ToString();
                VenderRemarks.Text = dr.Cells[7].Value.ToString();

                checkBox1.Checked = false;
                PnlVendorSerch.Visible = false;

                LockVendorTxtBox();
                //RbUp.Checked = true;
                RbNew.Checked = false;
                RbNew.Enabled = true;
                RbUp.Enabled = true;

                VenderName.Focus();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try{

            if (checkBox1.Checked==true )
            {
                SqlConnection con3 = new SqlConnection(IMS);
                con3.Open();
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress,VenderPTel,VenderPFax,VenderPCountry,VenderPEmail,VenderRemarks,CreditPeriod,CreditValue,discount,BRCno,VATno,ActiveDeactive FROM VenderDetails ";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12],dr[13]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }
            }
                if (checkBox1.Checked != true)
                { 
                     RbNew.Checked = false;
                     PnlVendorSerch.Visible = true;

                     SqlConnection con4 = new SqlConnection(IMS);
                     con4.Open();

                     string VenSelectAllcheck = "SELECT VenderID,VenderName,VenderPHAddress,VenderPTel,VenderPFax,VenderPCountry,VenderPEmail,VenderRemarks,CreditPeriod,CreditValue,discount,BRCno,VATno,ActiveDeactive FROM VenderDetails WHERE ActiveDeactive='" + 1 + "'";
                    SqlCommand cmd4 = new SqlCommand(VenSelectAllcheck, con4);
                    SqlDataReader dr1 = cmd4.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();

                    while (dr1.Read() == true)
                    {
                    dataGridView1.Rows.Add(dr1[0], dr1[1], dr1[2], dr1[3], dr1[4], dr1[5], dr1[6], dr1[7], dr1[8], dr1[9], dr1[10], dr1[11], dr1[12],dr1[13]);
                   
                    }

                    if (con4.State == ConnectionState.Open)
                    {
                    con4.Close();
                    }
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
                }

        private void VenderName_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void VenderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderPHAddress.Focus();
            }
        }

        private void VenderPTel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderPFax.Focus();
            }
        }

        private void VenderPFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderPCountry.Focus();
            }
        }

        private void VenderPCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderPEmail.Focus();
            }
        }

        private void VenderPEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VendercreditPerid.Focus();
            }
        }

        private void VendercreditPerid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderCreditValue.Focus();
            }
        }

        private void VenderDiscontber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderBRCno.Focus();
            }
        }

        private void VenderBRCno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderVATno.Focus();
            }
        }

        private void VenderVATno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderRemarks.Focus();
            }
        }

        private void VenderRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                VenderSave.Focus();
            }
        }

        private void VenderPTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one dash point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void VenderPFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one dash point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void VendercreditPerid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void VenderCreditValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one dash point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void VenderVATno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void VenderDiscontber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void VenderCreditValue_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                VenderDiscontber.Focus();
            }
        }

        private void VenderVATno_Leave(object sender, EventArgs e)
        {
            if (VenderVATno.Text == "")
            {
                VenderVATno.Text = "0.0";
            }
        }

        private void VenderBRCno_Leave(object sender, EventArgs e)
        {
            if (VenderBRCno.Text == "")
            {
                VenderBRCno.Text = "0.0";
            }
        }

        private void VenderCreditValue_Leave(object sender, EventArgs e)
        {
            if (VenderCreditValue.Text == "")
            {
                VenderCreditValue.Text = "0.0";
            }
        }

        private void VenderDiscontber_Leave(object sender, EventArgs e)
        {
            if (VenderDiscontber.Text == "")
            {
                VenderDiscontber.Text = "0.0";
            }
        }

        private void VendercreditPerid_Leave(object sender, EventArgs e)
        {
            if (VendercreditPerid.Text == "")
            {
                VendercreditPerid.Text = "0.0";
            }
        }
           
    
    
    
    }
       


}

       
    

