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
    public partial class CustomerReg : Form
    {
        public CustomerReg()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;



        public void getCreateCustomerCode()
        {
            #region New Customer Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select CusID from CustomerDetails";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    CusID.Text = "CUS1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 CusID FROM CustomerDetails order by CusID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string CusNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(CusNumOnly) + 1).ToString();

                        CusID.Text = "CUS" + no;

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



        private void CustomerCredCkBx_CheckedChanged(object sender, EventArgs e)
        {
            if (CrditLimitYESNO.Checked == true)
            {
                CusCreditLimit.Enabled = true;
            }

            if (CrditLimitYESNO.Checked != true)
            {
                CusCreditLimit.Enabled = false;
            }
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                getCreateCustomerCode();

                clearAndDefault();

                EnableAll();

                CusCreditLimit.Enabled = false;

                BtnSave.Text = "Save";

                CkDeactivated.Enabled = false;

                BtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {
            EnableAll();
            CusCreditLimit.Enabled = false;

            BtnSave.Text = "Update";

            CkDeactivated.Enabled = true;

            BtnSave.Enabled = true;

        }

        private void CustomerReg_Load(object sender, EventArgs e)
        {
            getCreateCustomerCode();
            CusFirstName.Focus();
            RbNew.Checked = true;
        }

        public void clearAndDefault()
        { 
            CusFirstName.Text="";
            CusLastName.Text="";
            CusCompanyName.Text="";
            CusPersonalAddress.Text="";
            CusTelNUmber.Text="";
            CusMobileNumber.Text="";
            CusFaxNumber.Text="";
            CusEmailAddress.Text="";
            CusCountry.Text="";
            CusTaxGroup.Text="";
            CusPriceLevel.Text="";
            CusCreditLimit.Text="";
            CusRemarks.Text = "";

            CkDeactivated.Enabled = false;
        }

        public void DesableAll()
        {
            CusFirstName.Enabled = false;
            CusLastName.Enabled = false;
            CusCompanyName.Enabled = false;
            CusPersonalAddress.Enabled = false;
            CusTelNUmber.Enabled = false;
            CusMobileNumber.Enabled = false;
            CusFaxNumber.Enabled = false;
            CusEmailAddress.Enabled = false;
            CusCountry.Enabled = false;
            CusTaxGroup.Enabled = false;
            CusPriceLevel.Enabled = false;
            CusCreditLimit.Enabled = false;
            CusRemarks.Enabled = false;
            CrditLimitYESNO.Enabled = false;
        }

        public void EnableAll()
        {
            CusFirstName.Enabled = true;
            CusLastName.Enabled = true;
            CusCompanyName.Enabled = true;
            CusPersonalAddress.Enabled = true;
            CusTelNUmber.Enabled = true;
            CusMobileNumber.Enabled = true;
            CusFaxNumber.Enabled = true;
            CusEmailAddress.Enabled = true;
            CusCountry.Enabled = true;
            CusTaxGroup.Enabled = true;
            CusPriceLevel.Enabled = true;
            CusCreditLimit.Enabled = true;
            CusRemarks.Enabled = true;
            CrditLimitYESNO.Enabled = true;
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                string timeStamp = System.DateTime.Now.ToString();

                #region missing things======================================================================

                //customer name_________________________________________________________________________________
                if (CusFirstName.Text == "")
                {
                    MessageBox.Show("please enter Valied Customer Name", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    CusFirstName.Focus();
                    return;
                }

                if (CusPriceLevel.Text == "")
                {
                    MessageBox.Show("Please Select Price Level", "Message");
                    CusPriceLevel.Focus();
                    return;

                }

                #endregion

                if (CusCreditLimit.Text == "")
                {
                    CusCreditLimit.Text = "0.00";
                }

                if (RbNew.Checked)
                {
                    #region insert data

                    getCreateCustomerCode();

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();
                    string CusIntert = "INSERT INTO CustomerDetails(CusID,CusFirstName,CusLastName,CusActiveDeactive,CusCompanyName,CusPersonalAddress,CusTelNUmber,CusMobileNumber,CusFaxNumber,CusEmailAddress,CusCountry,CusTaxGroup,CusPriceLevel,CusCreditLimit,CusRemarks,RegDate) VALUES('" + CusID.Text + "','" + CusFirstName.Text + "','" + CusLastName.Text + "','" + 1 + "','" + CusCompanyName.Text + "','" + CusPersonalAddress.Text + "','" + CusTelNUmber.Text + "','" + CusMobileNumber.Text + "','" + CusFaxNumber.Text + "','" + CusEmailAddress.Text + "','" + CusCountry.Text + "','" + CusTaxGroup.Text + "','" + CusPriceLevel.Text + "','" + CusCreditLimit.Text + "','" + CusRemarks.Text + "','" + timeStamp + "')";
                    SqlCommand cmd1 = new SqlCommand(CusIntert, con1);
                    cmd1.ExecuteNonQuery();

                    //insert Credit Details
                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();
                    string CustomerCredit = "INSERT INTO RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) VALUES('" + CusID.Text + "','" + CusID.Text + "','0','0','0','0','" + DateTime.Now.ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(CustomerCredit, con1);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    if (con1.State == ConnectionState.Open || con2.State == ConnectionState.Open)
                    {
                        con1.Close();
                        con2.Close();
                    }

                    getCreateCustomerCode();

                    clearAndDefault();

                    #endregion
                }

                if (RbUp.Checked)
                {
                    #region Update a Customer

                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

                    string CusUpdate = "UPDATE CustomerDetails SET CusFirstName='" + CusFirstName.Text + "',CusLastName='" + CusLastName.Text + "',CusActiveDeactive='" + 1 + "',CusCompanyName='" + CusCompanyName.Text + "',CusPersonalAddress='" + CusPersonalAddress.Text + "',CusTelNUmber='" + CusTelNUmber.Text + "',CusMobileNumber='" + CusMobileNumber.Text + "',CusFaxNumber='" + CusFaxNumber.Text + "',CusEmailAddress='" + CusEmailAddress.Text + "',CusCountry='" + CusCountry.Text + "',CusTaxGroup='" + CusTaxGroup.Text + "',CusPriceLevel='" + CusPriceLevel.Text + "',CusCreditLimit='" + CusCreditLimit.Text + "',CusRemarks='" + CusRemarks.Text + "' WHERE CusID='" + CusID.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(CusUpdate, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated the Customer Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    RbUp.Checked = false;
                    RbUp.Enabled = false;
                    CkDeactivated.Checked = false;
                    CkDeactivated.Enabled = false;
                    RbNew.Checked = true;
                    RbNew.Enabled = true;

                    BtnSave.Text = "Save";


                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }


                    getCreateCustomerCode();

                    clearAndDefault();

                    #endregion
                }

                if (RbUp.Checked && CkDeactivated.Checked)
                {
                    #region Deactivate a Customer

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();


                    DialogResult Result = MessageBox.Show("Are you sure you want to deactivate this Customer?.", "Deactivated", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Result == DialogResult.Yes)
                    {
                        string CusDeactivate = "UPDATE CustomerDetails SET CusActiveDeactive = '" + 0 + "' WHERE CusID='" + CusID.Text + "'";

                        SqlCommand cmd3 = new SqlCommand(CusDeactivate, con2);
                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("Deactivated the Vendor", "Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        RbUp.Checked = false;
                        RbUp.Enabled = false;
                        CkDeactivated.Checked = false;
                        CkDeactivated.Enabled = false;
                        RbNew.Checked = true;
                        RbNew.Enabled = true;

                        BtnSave.Text = "Save";
                    }

                    if (con2.State == ConnectionState.Open)
                    {
                        con2.Close();
                    }

                    getCreateCustomerCode();

                    clearAndDefault();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                RbNew.Checked = false;
                RbUp.Checked = false;
                PnlCustomerSerch.Visible = true;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                     //dt is the data table
                
               


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusTelNUmber,CusMobileNumber,CusFaxNumber,CusEmailAddress,CusCountry,CusTaxGroup,CusPriceLevel,CusCreditLimit,CusRemarks FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13]);

                    dataGridView1[1, 0].Selected = true;

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

           //---------------------------------------------------------------------------

            //===========================================================
            

            
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

            DataGridViewRow dr = dataGridView1.SelectedRows[0];

            CusID.Text = dr.Cells[0].Value.ToString();
            CusFirstName.Text = dr.Cells[1].Value.ToString();
            CusLastName.Text = dr.Cells[2].Value.ToString();
            CusCompanyName.Text = dr.Cells[3].Value.ToString();
            CusPersonalAddress.Text = dr.Cells[4].Value.ToString();
            CusTelNUmber.Text = dr.Cells[5].Value.ToString();
            CusMobileNumber.Text = dr.Cells[6].Value.ToString();
            CusFaxNumber.Text = dr.Cells[7].Value.ToString();
            CusEmailAddress.Text = dr.Cells[8].Value.ToString();
            CusCountry.Text = dr.Cells[9].Value.ToString();
            CusTaxGroup.Text = dr.Cells[10].Value.ToString();
            CusPriceLevel.Text = dr.Cells[11].Value.ToString();
            CusCreditLimit.Text = dr.Cells[12].Value.ToString();
            CusRemarks.Text = dr.Cells[13].Value.ToString();

            PnlCustomerSerch.Visible = false;

            RbUp.Enabled = true;
            RbNew.Enabled = true;
            DesableAll();

            textBox1.Text = "";
            textBox2.Text = "";

            BtnSave.Enabled = false;

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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
            SqlConnection con = new SqlConnection(IMS);
            con.Open();

            string CusUpdate = "UPDATE CustomerDetails SET CusFirstName='" + CusFirstName.Text + "',CusLastName='" + CusLastName.Text + "',CusActiveDeactive='" + 1 + "',CusCompanyName='" + CusCompanyName.Text + "',CusPersonalAddress='" + CusPersonalAddress.Text + "',CusTelNUmber='" + CusTelNUmber.Text + "',CusMobileNumber='" + CusMobileNumber.Text + "',CusFaxNumber='" + CusFaxNumber.Text + "',CusEmailAddress='" + CusEmailAddress.Text + "',CusCountry='" + CusCountry.Text + "',CusTaxGroup='" + CusTaxGroup.Text + "',CusPriceLevel='" + CusPriceLevel.Text + "',CusCreditLimit='" + CusCreditLimit.Text + "',CusRemarks='" + CusRemarks.Text + "' WHERE CusID='" + CusID.Text + "'";

            SqlCommand cmd2 = new SqlCommand(CusUpdate, con);
            cmd2.ExecuteNonQuery();

            MessageBox.Show("Successfully Updated the Customer Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            getCreateCustomerCode();

            clearAndDefault();

            RbUp.Checked = false;
            RbUp.Enabled = false;
            CkDeactivated.Checked = false;
            CkDeactivated.Enabled = false;
            RbNew.Checked = true;
            RbNew.Enabled = true;

           // BtnSave.Text = "Save";


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (RbUp.Checked && CkDeactivated.Checked)
                {
                    #region Deactivate a Customer

                    SqlConnection con2 = new SqlConnection(IMS);
                    con2.Open();


                    DialogResult Result = MessageBox.Show("Are you sure you want to deactivate this Customer?.", "Deactivated", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Result == DialogResult.Yes)
                    {
                        string CusDeactivate = "UPDATE CustomerDetails SET CusActiveDeactive = '" + 0 + "' WHERE CusID='" + CusID.Text + "'";

                        SqlCommand cmd3 = new SqlCommand(CusDeactivate, con2);
                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("Deactivated the Vendor", "Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        getCreateCustomerCode();

                        clearAndDefault();

                        RbUp.Checked = false;
                        RbUp.Enabled = false;
                        CkDeactivated.Checked = false;
                        CkDeactivated.Enabled = false;
                        RbNew.Checked = true;
                        RbNew.Enabled = true;

                        //BtnSave.Text = "Save";
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
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                #region select According to the Customer ID
                textBox2.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusTelNUmber,CusMobileNumber,CusFaxNumber,CusEmailAddress,CusCountry,CusTaxGroup,CusPriceLevel,CusCreditLimit,CusRemarks FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "' AND CusID LIKE '" + textBox1.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                DesableAll();

               

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                #region select According to the Customer First Name

                textBox1.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusTelNUmber,CusMobileNumber,CusFaxNumber,CusEmailAddress,CusCountry,CusTaxGroup,CusPriceLevel,CusCreditLimit,CusRemarks FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "' AND CusFirstName LIKE '" + textBox2.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                DesableAll();

              

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {

        }

        private void CusCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CusTelNUmber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusMobileNumber.Focus();
            }

        }

        private void CusTelNUmber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CusTelNUmber_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void CusMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CusFaxNumber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CrditLimitYESNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (CrditLimitYESNO.Checked == true)
            {
                CusCreditLimit.Focus();
            }
            else
            {
                CusRemarks.Focus();
            }
        }

        private void CusFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                CusLastName.Focus();
            }
        }

        private void CusCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusPersonalAddress.Focus();
            }
        }

        private void CusLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusCompanyName.Focus();
            }
        }

        private void CusPersonalAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusTelNUmber.Focus();
            }
        }

        private void CusMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusFaxNumber.Focus();
            }
        }

        private void CusFaxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusEmailAddress.Focus();
            }
        }

        private void CusEmailAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusCountry.Focus();
            }
        }

        private void CusCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusTaxGroup.DroppedDown = true;
                CusTaxGroup.Focus();
            }
        }

        private void CusTaxGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusPriceLevel.DroppedDown = true;
                CusPriceLevel.Focus();
            }
        }

        private void CusPriceLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
               
                CrditLimitYESNO.Focus();
            }
        }

        private void CusRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnSave.Focus();
            }
        }

        private void CusCreditLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusRemarks.Focus();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    DataGridViewRow dr = dataGridView1.SelectedRows[0];

                    CusID.Text = dr.Cells[0].Value.ToString();
                    CusFirstName.Text = dr.Cells[1].Value.ToString();
                    CusLastName.Text = dr.Cells[2].Value.ToString();
                    CusCompanyName.Text = dr.Cells[3].Value.ToString();
                    CusPersonalAddress.Text = dr.Cells[4].Value.ToString();
                    CusTelNUmber.Text = dr.Cells[5].Value.ToString();
                    CusMobileNumber.Text = dr.Cells[6].Value.ToString();
                    CusFaxNumber.Text = dr.Cells[7].Value.ToString();
                    CusEmailAddress.Text = dr.Cells[8].Value.ToString();
                    CusCountry.Text = dr.Cells[9].Value.ToString();
                    CusTaxGroup.Text = dr.Cells[10].Value.ToString();
                    CusPriceLevel.Text = dr.Cells[11].Value.ToString();
                    CusCreditLimit.Text = dr.Cells[12].Value.ToString();
                    CusRemarks.Text = dr.Cells[13].Value.ToString();

                    PnlCustomerSerch.Visible = false;

                    RbUp.Enabled = true;
                    RbNew.Enabled = true;
                    DesableAll();

                    textBox1.Text = "";
                    textBox2.Text = "";

                    BtnSave.Enabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }   
        }

        
    }
}
