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
    public partial class UserProfile : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

        public UserProfile()
        {
            InitializeComponent();
        }

        public void User_Role_Insert()
        {
            try
            {
                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string insertUser = "";

                if (cmbUerLeve.Text == "Admin" || cmbUerLeve.Text == "Chairman")
                {
                    insertUser = @"INSERT INTO User_Settings( User_ID,Add_New_User, Sk_New_Item, Sk_GRN, Sk_Change_Selling_Price, Sk_Barcode, In_Selling_Itm, In_Re_Print_Sell, In_Job, In_Re_Job, 
                      In_GRN_Credit, In_Customer_Credit, Re_New_Note, Re_Paymet_Add, Re_JOB_Pay_Add, Cus_add, Ven_Add, Acc_Petty_Cash, Acc_Set_Off, Acc_Bank, 
                      Rpt_Items_In_Stock, Rpt_Re_Order_Itms, Rpt_Sold_Inv, Rpt_Pay_Bal, Rpt_Profit_Lost, Rpt_Customer, Rpt_Vender, Rpt_Paymet_Details, User_Setting,In_Return_O_Cancel, Acc_Bank_Acc_Create, Acc_DB_Backup, Rpt_Petty_cash, Rpt_Banking_Details, Rpt_Printing_Details,Re_CreditPaymentBalanceRe,In_ManualChangePrice) 
                        Values('" + txtCashCode.Text + "', '1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1')";

                }

                else
                {

                    insertUser = @" INSERT INTO User_Settings(User_ID,Add_New_User, Sk_New_Item, Sk_GRN, Sk_Change_Selling_Price, Sk_Barcode, In_Selling_Itm, In_Re_Print_Sell, In_Job, In_Re_Job, 
                      In_GRN_Credit, In_Customer_Credit, Re_New_Note, Re_Paymet_Add, Re_JOB_Pay_Add, Cus_add, Ven_Add, Acc_Petty_Cash, Acc_Set_Off, Acc_Bank, 
                      Rpt_Items_In_Stock, Rpt_Re_Order_Itms, Rpt_Sold_Inv, Rpt_Pay_Bal, Rpt_Profit_Lost, Rpt_Customer, Rpt_Vender, Rpt_Paymet_Details, User_Setting,In_Return_O_Cancel, Acc_Bank_Acc_Create, Acc_DB_Backup, Rpt_Petty_cash, Rpt_Banking_Details, Rpt_Printing_Details,Re_CreditPaymentBalanceRe,In_ManualChangePrice) 
                        Values('" + txtCashCode.Text + "','0','0','0','0','1','1','0','1','0','0','0','1','0','0','1','1','0','0','0','1','1','0','1','0','1','1','1','0','0','0','0','0','0','0','0','0')";



                }


                SqlCommand cmd = new SqlCommand(insertUser, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void UserName_ck()
        {
            try
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string checkId = "Select UserName from UserProfile WHERE UserName= '" + TxtUserName.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(checkId, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    MessageBox.Show("User Name Also Available.. please try another Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtUserName.Focus();
                    return;
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
                if (txtCashName.Text == "")
                {
                    MessageBox.Show("Please Enter First Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCashName.Focus();
                    return;
                }
                if (txtlastName.Text == "")
                {
                    MessageBox.Show("Please Enter Last Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtlastName.Focus();
                    return;
                }
                if (txtDislayName.Text == "")
                {
                    MessageBox.Show("Please Enter Dispal On System.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDislayName.Focus();
                    return;
                }
                if (txtUserAddr.Text == "")
                {
                    MessageBox.Show("Please Enter Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserAddr.Focus();
                    return;
                }
                if (txttelUse.Text == "")
                {
                    MessageBox.Show("Please Enter Phone Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txttelUse.Focus();
                    return;
                }
                if (txtUserEMail.Text == "")
                {
                    MessageBox.Show("Please Enter E-Mail Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserEMail.Focus();
                    return;
                }

                if (txtConfirmPs.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please Enter correct password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }
                if (cmbUerLeve.Text == "")
                {
                    MessageBox.Show("Please Enter Acting Role.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbUerLeve.Focus();
                    return;
                }




                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string checkId = "Select UserCode from UserProfile WHERE UserCode= '" + txtCashCode.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(checkId, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();



                if (!dr1.Read())
                {

                    GenerateJOBNumbe();

                    //insert user informations to the DB.....
                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();
                    string insertUser = "INSERT INTO UserProfile( UserCode , FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,UserName,ActingRole,AtiveDeactive,CreatedBy )Values('" + txtCashCode.Text + "','" + txtCashName.Text + "','" + txtlastName.Text + "','" + txtDislayName.Text + "','" + txtUserAddr.Text + "','" + txttelUse.Text + "','" + txtUserEMail.Text + "','" + txtPassword.Text + "','" + TxtUserName.Text + "','" + cmbUerLeve.Text + "','1','" + LgUser.Text + "')";
                    SqlCommand cmd = new SqlCommand(insertUser, con);
                    cmd.ExecuteNonQuery();

                    //add user controle settings
                    User_Role_Insert();

                    MessageBox.Show("Successfully saved.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    //GenerateJOBNumbe();
                    Clear();

                    GenerateJOBNumbe();

                    selectListview();


                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }




                }


    //UPdate user================================================================================================================================================
                else
                {

                    if (checkBox1.Checked == true)
                    {
                        #region Deactivate-------------------------
                        DialogResult result = MessageBox.Show("Are You Sure Deactivate?", "Message ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            cmd1.Dispose();
                            dr1.Close();

                            SqlConnection con4 = new SqlConnection(IMS);
                            con4.Open();

                            string UserUpdate1 = "UPDATE  UserProfile SET AtiveDeactive='0' WHERE UserCode='" + txtCashCode.Text + "'";

                            SqlCommand cmd31 = new SqlCommand(UserUpdate1, con4);
                            cmd31.ExecuteNonQuery();

                            MessageBox.Show("Successfully Deactivate the User.", "Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Clear();
                            selectListview();

                            btnSave.Text = "Save";


                            checkBox1.Visible = false;
                            GenerateJOBNumbe();
                        }

                        #endregion
                    }
                    if (checkBox1.Checked == false)
                    {
                        #region Update------------------------

                        cmd1.Dispose();
                        dr1.Close();

                        SqlConnection con3 = new SqlConnection(IMS);
                        con3.Open();

                        string UserUpdate = "UPDATE  UserProfile SET UserCode ='" + txtCashCode.Text + "' ,FirstName ='" + txtCashName.Text + "' ,LastName ='" + txtlastName.Text + "' ,DisplayOn ='" + txtDislayName.Text + "' ,Addre ='" + txtUserAddr.Text + "' ,TPNum ='" + txttelUse.Text + "' ,EmailAddress ='" + txtUserEMail.Text + "' ,Passwod ='" + txtPassword.Text + "' ,UserName ='" + TxtUserName.Text + "'  ,ActingRole ='" + cmbUerLeve.Text + "',AtiveDeactive='1' WHERE UserCode='" + txtCashCode.Text + "'";

                        SqlCommand cmd3 = new SqlCommand(UserUpdate, con3);
                        cmd3.ExecuteNonQuery();

                        con3.Close();



                        #region update user control......................................................

                        if (cmbUerLeve.Text != "Chairman" && cmbUerLeve.Text != "Admin")
                        {
                            #region not Chairman And Admin..........................................

                            SqlConnection cnn = new SqlConnection(IMS);
                            cnn.Open();
                            // MessageBox.Show("start");
                            string upaAction = @"update User_Settings set Add_New_User='0', Sk_New_Item='0', Sk_GRN='0', Sk_Change_Selling_Price='0', Sk_Barcode='1', In_Selling_Itm='1', In_Re_Print_Sell='0', In_Job='1', In_Re_Job='0', 
                                              In_GRN_Credit='0', In_Customer_Credit='0', Re_New_Note='1', Re_Paymet_Add='0', Re_JOB_Pay_Add='0', Cus_add='1', Ven_Add='1', Acc_Petty_Cash='0', Acc_Set_Off='0', Acc_Bank='0', 
                                              Rpt_Items_In_Stock='1', Rpt_Re_Order_Itms='1', Rpt_Sold_Inv='0', Rpt_Pay_Bal='1', Rpt_Profit_Lost='0', Rpt_Customer='1', Rpt_Vender='1', Rpt_Paymet_Details='1',
                                               User_Setting='0',In_Return_O_Cancel='0', Acc_Bank_Acc_Create='0', Acc_DB_Backup='0', Rpt_Petty_cash='0', Rpt_Banking_Details='0', Rpt_Printing_Details='0',Re_CreditPaymentBalanceRe='0',In_ManualChangePrice='0'where User_ID='" + txtCashCode.Text + "'";
                            SqlCommand cmm = new SqlCommand(upaAction, cnn);
                            cmm.ExecuteNonQuery();
                            cnn.Close();
                            //  MessageBox.Show(upaAction);
                            #endregion
                        }

                        if (cmbUerLeve.Text == "Chairman" || cmbUerLeve.Text == "Admin")
                        {
                            #region Chaieman And Admin..........................................

                            SqlConnection cnn4 = new SqlConnection(IMS);
                            cnn4.Open();
                            string AddAdi = @"update User_Settings  set Add_New_User='1', Sk_New_Item='1', Sk_GRN='1', Sk_Change_Selling_Price='1', Sk_Barcode='1', In_Selling_Itm='1', In_Re_Print_Sell='1', In_Job='1', In_Re_Job='1', 
                                              In_GRN_Credit='1', In_Customer_Credit='1', Re_New_Note='1', Re_Paymet_Add='1', Re_JOB_Pay_Add='1', Cus_add='1', Ven_Add='1', Acc_Petty_Cash='1', Acc_Set_Off='1', Acc_Bank='1', 
                                              Rpt_Items_In_Stock='1', Rpt_Re_Order_Itms='1', Rpt_Sold_Inv='1', Rpt_Pay_Bal='1', Rpt_Profit_Lost='1', Rpt_Customer='1', Rpt_Vender='1', Rpt_Paymet_Details='1',
                                               User_Setting='1',In_Return_O_Cancel='1', Acc_Bank_Acc_Create='1', Acc_DB_Backup='1', Rpt_Petty_cash='1', Rpt_Banking_Details='1', Rpt_Printing_Details='1',Re_CreditPaymentBalanceRe='1',In_ManualChangePrice='1' where User_ID='" + txtCashCode.Text + "'";
                            SqlCommand cmm = new SqlCommand(AddAdi, cnn4);
                            cmm.ExecuteNonQuery();
                            #endregion
                        }
                        #endregion



                        MessageBox.Show("Successfully Updated the Customer Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = true;
                        Clear();
                        selectListview();

                        btnSave.Text = "Save";


                        checkBox1.Visible = false;
                        GenerateJOBNumbe();

                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            selectListview();
            GenerateJOBNumbe();
           

        }



        public void GenerateJOBNumbe()
        {
            #region New User Number...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT UserCode FROM UserProfile";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    txtCashCode.Text = "USR1001";
                    // PassInvoiceNumber.Text = "INV1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 UserCode FROM UserProfile order by UserCode DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        txtCashCode.Text = "USR" + no;
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

        #region Clear...........................................
        public void Clear()
        {
            //txtCashCode.Clear();
            txtCashName.Clear();
            txtlastName.Clear();
            txtDislayName.Clear();
            txtUserAddr.Clear();
            txttelUse.Clear();
            txtUserEMail.Clear();
            txtPassword.Clear();
            txtConfirmPs.Clear();
            cmbUerLeve.Items.Add("");
            TxtUserName.Text = "";
            checkBox1.Checked = false;

        }
        #endregion

       



        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        // slect value in list view===================================================================================================================================

        public void selectListview()
        {

            try
            {


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectListView = "SELECT UserCode, FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,UserName,ActingRole,AtiveDeactive FROM UserProfile WHERE AtiveDeactive='1'";
                SqlCommand cmd1 = new SqlCommand(selectListView, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                lstPublic.Items.Clear();
                while (dr.Read() == true)
                {

                    ListViewItem li;
                    li = new ListViewItem(dr[0].ToString());


                    //li.SubItems.Add(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());
                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[10].ToString());


                    lstPublic.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"System Error", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
        
        }

        private void lstPublic_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                //var cl = lstPublic.Items[lstPublic.FocusedItem.Index].SubItems[0].Text;
                //string txtcashco = "";
                ListViewItem item = lstPublic.SelectedItems[0];
                txtCashCode.Text = item.SubItems[0].Text;
                txtCashName.Text = item.SubItems[1].Text;
                txtlastName.Text = item.SubItems[2].Text;
                txtDislayName.Text = item.SubItems[3].Text;
                txtUserAddr.Text = item.SubItems[4].Text;
                txttelUse.Text = item.SubItems[5].Text;
                txtUserEMail.Text = item.SubItems[6].Text;
                txtPassword.Text = item.SubItems[7].Text;
                TxtUserName.Text = item.SubItems[8].Text;
                cmbUerLeve.Text = item.SubItems[9].Text;

                if (item.SubItems[10].Text == "0")
                {
                    checkBox1.Checked = true;
                }

                if (item.SubItems[10].Text == "1")
                {
                    checkBox1.Checked = false;
                }

                txtConfirmPs.Text = "";

                checkBox1.Visible = true;
                btnSave.Text = "Update";
                //btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Clear();
            GenerateJOBNumbe();
            btnSave.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void chbAllView_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbAllView.Checked==true)
            //{
            //    SqlConnection con1 = new SqlConnection(IMS);
            //    con1.Open();
            //    string selectListView = "SELECT UserCode, FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,ConfirmPass,ActingRole,AtiveDeactive FROM UserProfile ";
            //    SqlCommand cmd1 = new SqlCommand(selectListView, con1);
            //    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            //    lstPublic.Items.Clear();
            //    while (dr.Read() == true)
            //    {

            //        ListViewItem li;
            //        li = new ListViewItem(dr[0].ToString());


            //        //li.SubItems.Add(dr[0].ToString());
            //        li.SubItems.Add(dr[1].ToString());
            //        li.SubItems.Add(dr[2].ToString());
            //        li.SubItems.Add(dr[3].ToString());
            //        li.SubItems.Add(dr[4].ToString());
            //        li.SubItems.Add(dr[5].ToString());
            //        li.SubItems.Add(dr[6].ToString());
            //        li.SubItems.Add(dr[7].ToString());
            //        li.SubItems.Add(dr[8].ToString());
            //        li.SubItems.Add(dr[9].ToString());
            //        li.SubItems.Add(dr[10].ToString());


            //        lstPublic.Items.Add(li);
            //    }
            //    if (chbAllView.Checked==false)
            //    {
            //        lstPublic.Items.Clear();
            //        selectListview();
            //    }
            //}
        }

        private void chbAllView_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbAllView.Checked == true)
                {
                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string selectListView = "SELECT UserCode, FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,UserName,ActingRole,AtiveDeactive FROM UserProfile ";
                    SqlCommand cmd1 = new SqlCommand(selectListView, con1);
                    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                    lstPublic.Items.Clear();
                    while (dr.Read() == true)
                    {

                        ListViewItem li;
                        li = new ListViewItem(dr[0].ToString());


                        //li.SubItems.Add(dr[0].ToString());
                        li.SubItems.Add(dr[1].ToString());
                        li.SubItems.Add(dr[2].ToString());
                        li.SubItems.Add(dr[3].ToString());
                        li.SubItems.Add(dr[4].ToString());
                        li.SubItems.Add(dr[5].ToString());
                        li.SubItems.Add(dr[6].ToString());
                        li.SubItems.Add(dr[7].ToString());
                        li.SubItems.Add(dr[8].ToString());
                        li.SubItems.Add(dr[9].ToString());
                        li.SubItems.Add(dr[10].ToString());


                        lstPublic.Items.Add(li);
                    }

                }
                if (chbAllView.Checked == false)
                {
                    lstPublic.Items.Clear();
                    selectListview();

                    //MessageBox.Show("Sedssd");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void TxtUserName_Leave(object sender, EventArgs e)
        {
            UserName_ck();
        }

        private void txtCashName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                txtlastName.Focus();
            }
        }

        private void txtlastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtDislayName.Focus();
            }
        }

        private void txtDislayName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtUserAddr.Focus();
            }
        }

        private void txtUserAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txttelUse.Focus();
            }
        }

        private void txttelUse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtUserEMail.Focus();
            }
        }

        private void txtUserEMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                TxtUserName.Focus();
            }
        }

        private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtConfirmPs.Focus();
            }
        }

        private void txtConfirmPs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbUerLeve.DroppedDown = true;
                cmbUerLeve.Focus();
            }
        }

        private void cmbUerLeve_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnSave.Focus();
            }
        }



    }
}