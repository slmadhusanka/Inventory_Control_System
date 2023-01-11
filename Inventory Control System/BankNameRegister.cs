using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Inventory_Control_System
{
    public partial class BankNameRegister : Form
    {
         string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        public BankNameRegister()
        {
            InitializeComponent();
            chbDeactive.Hide();
           // getCreatebANKcode();
            selectListview();
            getCreatebBankBalanceid();
           
        }

        private void ClearTextBoxes(Control.ControlCollection cc)
        {
            #region clear all textboxs only
            foreach (Control ctrl in cc)
            {
                TextBox tb = ctrl as TextBox;
                if (tb != null)
                    tb.Text = "";
                    
                else
                    ClearTextBoxes(ctrl.Controls);
            }
            
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
                lblBanklDauto.Text = "BNK1001";

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

                    lblBanklDauto.Text = "BNK" + no;

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
        public void getCreatebBankBalanceid()
        {
            #region New getCreatebBankBalanceid...........................................
            try
            {
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select DoC_ID from Bank_Balance";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                LBLBALANCEID.Text = "DBD10000001";

                cmd.Dispose();
                dr.Close();

            }

            else
            {

                cmd.Dispose();
                dr.Close();

                string sql1 = " SELECT TOP 1 DoC_ID FROM Bank_Balance order by DoC_ID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string OrderNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                    LBLBALANCEID.Text = "DBD" + no;

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

        public void slectBank()
        {
            #region load Bank in CMB
            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "select BankID,BankName from Bank_Category  ";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader();

                cmbBank.Items.Clear();
                cmbBank.Items.Add("<New>");

                while (dr.Read())
                {
                    cmbBank.Items.Add(dr[1].ToString());
                }
                cmd1.Dispose();
                dr.Close();

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
        public void selectListview()
        {
            #region load data in list view

            try
            {


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectListView = "SELECT Bank_Category.[BankName],Bank_Account_Details.BankID,Bank_Account_Details.[Account_Num],Bank_Account_Details.[Acc_Holder],Bank_Account_Details.[Branch],Bank_Account_Details.[Account_Type],Bank_Account_Details.[Acc_Opening_Date],Bank_Account_Details.[Opening_Bal],Bank_Account_Details.[ReMark],Bank_Account_Details.[Ck_Book_Status],Bank_Account_Details.[Acc_Name],Bank_Account_Details.[Acc_Status] FROM [Bank_Account_Details] join Bank_Category on Bank_Account_Details.BankID=Bank_Category.BankID where Acc_Status='1' order by Bank_Account_Details.BankID asc  ";
                SqlCommand cmd1 = new SqlCommand(selectListView, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listView1.Items.Clear();

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
                    if ("0" == dr[9].ToString())
                    {
                        li.SubItems.Add("No");
                    }
                    if ("1" == dr[9].ToString())
                    {
                        li.SubItems.Add("Yes");
                    }
                   
                   // lblBankID.Text = dr[0].ToString();
                    //li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[10].ToString());
                    li.SubItems.Add(dr[11].ToString());


                   


                 listView1.Items.Add(li);
         }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion

        }
        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        String checkbook;
        String deactive;
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            #region values ck B4 seve it--------------------
          
            if (cmbBank.Text=="")
            {
                MessageBox.Show("Please Check Bank Name..!", "Message");
                cmbBank.Focus();
                return;
            }

            if (txtholder.Text == "")
            {
                MessageBox.Show("Please Check Account Holder..!", "Message");
                txtholder.Focus();
                return;
            }

            if (TxtBranch.Text == "")
            {
                MessageBox.Show("Please Check Branch..!", "Message");
                TxtBranch.Focus();
                return;
            }

            if (cmbAcounttype.Text == "")
            {
                MessageBox.Show("Please Check Account Type..!", "Message");
                cmbAcounttype.Focus();
                return;
            }

           
            if (txtaccontname.Text == "")
            {
                MessageBox.Show("Please Check Account Name..!", "Message");
                txtaccontname.Focus();
                return;
            }
            
            if (TxtAccountNo.Text != txtConfirmAcc.Text || TxtAccountNo.Text=="" ||txtConfirmAcc.Text=="")
            {
                MessageBox.Show("Please Check Account No..!", "Message");
                TxtAccountNo.Clear();
                txtConfirmAcc.Clear();
                TxtAccountNo.Focus();
                return;
            }

            if (chbcheckbook.Checked == true)
            {
                checkbook = "1";
            }
            if (chbcheckbook.Checked == false)
            {
                checkbook = "0";
            }

            //if (chbDeactive.Checked == true)
            //{
            //    deactive = "1";
            //}
            //if (chbDeactive.Checked == false)
            //{
            //    deactive = "0";
            //}
            #endregion

            try
            {

                if (btnADd.Text == "Save")
                {
                    #region Save Button-------------------------------------



                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();
                    string search = "select Account_Num from Bank_Account_Details  where BankID='" + lblBankID.Text + "' and Account_Num='" + TxtAccountNo.Text + "'  ";
                    SqlCommand cmd2 = new SqlCommand(search, con2);
                    IDataReader dr = cmd2.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Account number allredy in save..!", "Account Number");
                        return;
                    }

                    //insert data bankbalance table=============================================================================================
                    getCreatebBankBalanceid();

                    SqlConnection con3 = new SqlConnection(IMS);
                    con3.Open();
                    String balanceinsert = @"INSERT INTO Bank_Balance ( DoC_ID ,Amount_Status,Debit_Amount,Credit_Amount,Balance,Add_User,Time_Stamp) values('" + LBLBALANCEID.Text + "','" + " opening" + "','" + txtOpening.Text + "','" + "0.0" + "','" + txtOpening.Text + "','" + LgUser.Text + "','" + dateTimePicker1.Text + "' )";
                    SqlCommand cmd3 = new SqlCommand(balanceinsert, con3);
                    cmd3.ExecuteNonQuery();


                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string CusSelectAll = @"insert INTO   Bank_Account_Details (BankID,Account_Num,Acc_Status,Acc_Name,Acc_Holder,Branch,Account_Type,Ck_Book_Status,Opening_Bal,Acc_Opening_Date,Current_Balance,ReMark,Create_User) 
                                values ('" + lblBankID.Text + "','" + TxtAccountNo.Text + "','" + "1" + "','" + txtaccontname.Text + "','" + txtholder.Text + "','" + TxtBranch.Text + "','" + cmbAcounttype.Text + "','" + checkbook + "','" + txtOpening.Text + "','" + dateTimePicker1.Text + "','" + txtOpening.Text + "','" + txtremark.Text + "','" + LgUser.Text + "')  ";
                    SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                    cmd1.ExecuteNonQuery();


                    

                    //doc detsails................................

                    String BavkBakace = @"INSERT INTO Bank_Doc_details( DoC_ID, Banked_Date, Bank_Name, Acc_Num) values('" + LBLBALANCEID.Text + "','" + DateTime.Now.ToString() + "','" + lblBankID.Text + "', '" + TxtAccountNo.Text + "')";
                    SqlCommand cmd4 = new SqlCommand(BavkBakace, con3);
                    cmd4.ExecuteNonQuery();

                    MessageBox.Show("Insert seccessfull...");

                    //clear all textbox----------------------------------------------

                    ClearTextBoxes(this.Controls);

                    //---------------------------------------------------------------
                    listView1.Items.Clear();
                    selectListview();
                    cmbBank.Items.Clear();
                    // cmbAcounttype.ResetText();
                    cmbAcounttype.SelectedIndex = -1;
                    dateTimePicker1.ResetText();


                    #endregion

                }//close save IF


                if (btnADd.Text == "Update")
                {
                    #region Update Button--------------------------------

                    if (chbDeactive.Checked == true)
                    {
                        deactive = "0";
                    }
                    if (chbDeactive.Checked == false)
                    {
                        deactive = "1";
                    }




                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string updateBank = "UPDATE Bank_Account_Details SET BankID='" + lblBankID.Text + "',Account_Num='" + TxtAccountNo.Text + "',Acc_Status='" + deactive + "',Acc_Name='" + txtaccontname.Text + "',Acc_Holder='" + txtholder.Text + "',Branch='" + TxtBranch.Text + "',Account_Type='" + cmbAcounttype.Text + "',Ck_Book_Status='" + checkbook + "',Opening_Bal='" + txtOpening.Text + "',Current_Balance='" + txtOpening.Text + "',ReMark='" + txtremark.Text + "',[Last_Update_User]='" + LgUser.Text + "',[Last_Update_Date]='" + DateTime.Now.ToString() + "' where BankID='" + bankid + "' and Account_Num='" + accnumber + "'";
                    SqlCommand cmd1 = new SqlCommand(updateBank, con1);
                    cmd1.ExecuteNonQuery();

                    //Update data bankbalance table=============================================================================================

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();
                    string updateBankBalance = "UPDATE Bank_Balance SET [DoC_ID]='" + LBLBALANCEID.Text + "' ,[Amount_Status]='" + " opening" + "',[Debit_Amount]='" + txtOpening.Text + "',[Credit_Amount]='" + "0.0" + "',[Balance]='" + txtOpening.Text + "',[Add_User]='" + LgUser.Text + "',[Time_Stamp]='" + dateTimePicker1.Text + "' where [DoC_ID]='" + LBLBALANCEID.Text + "')";
                    SqlCommand cmd3 = new SqlCommand(updateBankBalance, con2);
                    cmd3.ExecuteNonQuery();


                    MessageBox.Show("Update Successfully.....");

                    //clear all textbox----------------------------------------------

                    ClearTextBoxes(this.Controls);

                    //---------------------------------------------------------------- 
                    cmbBank.SelectedIndex = -1;
                    chbDeactive.Checked = false;
                    chbDeactive.Hide();
                    chbcheckbook.Enabled = false;
                    chbcheckbook.Checked = false;
                    cmbAcounttype.SelectedIndex = -1;
                    lblBankID.Text = "";
                    listView1.Items.Clear();
                    selectListview();

                    #endregion

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            getCreatebBankBalanceid();

            chbcheckbook.Checked = false;
            txtOpening.Text = "0.0";
            txtremark.Text = "-";

            chbDeactive.Checked = false;
        }
         String bankid="";
         String accnumber = "";
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             #region data pass to textbox

             try
             {
                 ListViewItem item1 = listView1.SelectedItems[0];
                 bankid = item1.SubItems[1].Text;
                 accnumber = item1.SubItems[2].Text;

                 cmbBank.SelectedIndex = -1;
                 chbDeactive.Checked = false;
                 chbDeactive.Hide();
                 chbcheckbook.Enabled = false;
                 chbcheckbook.Checked = false;
                 cmbAcounttype.SelectedIndex = -1;
                 lblBankID.Text = "";


                 slectBank();

                 ListViewItem item = listView1.SelectedItems[0];

                 cmbBank.Text = item.SubItems[0].Text;
                 lblBankID.Text = item.SubItems[1].Text;
                 TxtAccountNo.Text = item.SubItems[2].Text;
                 txtholder.Text = item.SubItems[3].Text;
                 TxtBranch.Text = item.SubItems[4].Text;
                 cmbAcounttype.Text = item.SubItems[5].Text;
                 dateTimePicker1.Text = item.SubItems[6].Text;
                 txtOpening.Text = item.SubItems[7].Text;
                 txtremark.Text = item.SubItems[8].Text;
                 //MessageBox.Show(item.SubItems[9].Text);
                 if (item.SubItems[9].Text == "Yes")
                 {
                     chbcheckbook.Checked = true;

                 }
                 if (item.SubItems[9].Text == "No")
                 {
                     chbcheckbook.Checked = false;
                 }
                 if (item.SubItems[10].Text == null)
                 {
                     txtaccontname.Text = "-";
                 }
                 else
                 {
                     txtaccontname.Text = item.SubItems[10].Text;
                 }
                 if (item.SubItems[11].Text == "0")
                 {
                     chbDeactive.Checked = true;
                 }
                 if (item.SubItems[11].Text == "1")
                 {
                     chbDeactive.Checked = false;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             }
             chbDeactive.Show();
             btnADd.Text = "Update";
             #endregion
         }

        private void chbViewall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chbViewall.Checked == true)
                {
                    #region if viewall check box select
                    try
                    {


                        SqlConnection con1 = new SqlConnection(IMS);
                        con1.Open();
                        string selectListView = "SELECT Bank_Category.[BankName],Bank_Account_Details.BankID,Bank_Account_Details.[Account_Num],Bank_Account_Details.[Acc_Holder],Bank_Account_Details.[Branch],Bank_Account_Details.[Account_Type],Bank_Account_Details.[Acc_Opening_Date],Bank_Account_Details.[Opening_Bal],Bank_Account_Details.[ReMark],Bank_Account_Details.[Ck_Book_Status],Bank_Account_Details.[Acc_Name],Acc_Status FROM [Bank_Account_Details] join Bank_Category on Bank_Account_Details.BankID=Bank_Category.BankID order by Bank_Account_Details.BankID ";
                        SqlCommand cmd1 = new SqlCommand(selectListView, con1);
                        SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                        listView1.Items.Clear();
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

                            if ("0" == dr[9].ToString())
                            {
                                li.SubItems.Add("No");
                            }
                            if ("1" == dr[9].ToString())
                            {
                                li.SubItems.Add("Yes");
                            }

                            // li.SubItems.Add(dr[9].ToString());
                            li.SubItems.Add(dr[10].ToString());

                            li.SubItems.Add(dr[11].ToString());



                            // listView1.Items.Clear();
                            listView1.Items.Add(li);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    #endregion
                }

                #region if checkbox unselected

                if (chbViewall.Checked == false)
                {
                    listView1.Items.Clear();
                    selectListview();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void cmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region cmbBank load with pass bank id to textbox



                if (cmbBank.SelectedIndex == -1)
                {
                    chbDeactive.Hide();
                    return;
                }
                if (cmbBank.SelectedItem.ToString() == "<New>")
                {
                    PnlBankName.Visible = true;
                    txtbankName.Focus();
                }

                if (cmbBank.SelectedItem.ToString() != "<New>")
                {
                    PnlBankName.Visible = false;
                }
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "select BankID from Bank_Category where BankName='" + cmbBank.SelectedItem.ToString() + "'  ";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader();

                if (dr.Read())
                {

                    lblBankID.Text = dr[0].ToString();
                }

                getCreatebANKcode();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                #region catagory value pass to in DB

                if (txtbankName.Text == "")
                {
                    MessageBox.Show("Please Enter Bank Name..", "Message");
                    txtbankName.Focus();
                    return;
                }

                DialogResult addcheck = MessageBox.Show("Are you sure about this Reason", "New Reason Update", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (addcheck == DialogResult.OK)
                {
                    getCreatebBankBalanceid();

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string CusSelectAll = "insert INTO   Bank_Category (BankID,BankName) values ('" + lblBanklDauto.Text + "','" + txtbankName.Text + "')  ";
                    SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                    cmd1.ExecuteNonQuery();



                    cmbBank.Items.Clear();
                    slectBank();
                    PnlBankName.Visible = false;



                }
                else
                {
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            getCreatebBankBalanceid();
        }

        private void cmbBank_MouseClick(object sender, MouseEventArgs e)
        {
            slectBank();
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlBankName.Visible = false;
            cmbBank.Items.Clear();
            slectBank();
        }

        private void cmbAcounttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Enable/disable accouttype
            if (cmbAcounttype.SelectedItem == "Current Acc")
            {
                chbcheckbook.Enabled = true;
            }
            if (cmbAcounttype.SelectedItem == "Saving Acc")
            {
                chbcheckbook.Enabled = false;
            }
            #endregion
        }

        private void chbcheckbook_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //clear all textbox----------------------------------------------

            ClearTextBoxes(this.Controls);

            //----------------------------------------------------------------

            cmbBank.Items.Clear();
            //cmbAcounttype.Items.Clear();
            cmbAcounttype.SelectedIndex = -1;
            dateTimePicker1.ResetText();
            btnADd.Text = "Save";
            chbcheckbook.Checked = false;
            chbcheckbook.Enabled = false;
            chbDeactive.Checked = false;
            chbDeactive.Hide();


        }

        private void txtOpening_Leave(object sender, EventArgs e)
        {
            if (txtOpening.Text == "")
            {
                txtOpening.Text = "0.0";
            }
        }

        private void txtremark_Leave(object sender, EventArgs e)
        {
            if (txtremark.Text == "")
            {
                txtremark.Text = "-";
            }
        }

        private void BankNameRegister_Load(object sender, EventArgs e)
        {

        }

        private void txtbankName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                button4.Focus();

            }
        }

        private void cmbBank_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                txtaccontname.Focus();

            }
        }

        private void txtaccontname_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                TxtAccountNo.Focus();

            }
        }

        private void TxtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                txtConfirmAcc.Focus();

            }
        }

        private void txtConfirmAcc_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                txtholder.Focus();

            }
        }

        private void txtholder_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                TxtBranch.Focus();

            }
        }

        private void TxtBranch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                cmbAcounttype.Focus();

            }
        }

        private void cmbAcounttype_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.KeyValue==13)
            {
                chbcheckbook.Focus();

            }
            
        }

        private void chbcheckbook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                dateTimePicker1.Focus();

            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtOpening.Focus();

            }
        }

        private void txtOpening_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtremark.Focus();

            }
        }

        private void txtremark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnADd.Focus();

            }
        }
       
    }
}