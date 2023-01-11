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

namespace Inventory_Control_System
{
    public partial class RepairJOBPaymentEnter : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

        public RepairJOBPaymentEnter()
        {
            InitializeComponent();
        }

        private void RepairJOBPaymentEnter_Load(object sender, EventArgs e)
        {

        }

        private void BTNUpadate_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PnlJobSearch.Visible = true;
            PnlJobSearch.BringToFront();

            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = "SELECT ReJobNumber,ReCusID,CusFirstName,ReCompletingDate,ReCompletingTime,JobType,ReEngNote,ReCusNote,Solution,FaultStatus,PaymentStatus FROM RepairNotes WHERE PaymentDetails='Pending'";
                SqlCommand cmd = new SqlCommand(viewGrideview, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dgvjobIdRepairing.Rows.Clear();

                while (dr.Read() == true)
                {
                    dgvjobIdRepairing.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10]);

                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                txtseachjob.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            
        }

        private void dgvjobIdRepairing_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {



            groupBox2.Enabled = true;

            DataGridViewRow dr = dgvjobIdRepairing.SelectedRows[0];
            string JobNo = "";
            JobNo = dr.Cells[0].Value.ToString();
            JobID.Text = JobNo;

            txtCusID.Text = dr.Cells[1].Value.ToString();
            txtCusName.Text = dr.Cells[2].Value.ToString();
            dtpDate.Text = dr.Cells[3].Value.ToString();
            dtTime.Text = dr.Cells[4].Value.ToString();
            txtJobType.Text = dr.Cells[5].Value.ToString();
            txtEngNote.Text = dr.Cells[6].Value.ToString();
            txtcusNot.Text = dr.Cells[7].Value.ToString();

            int RadioBtnNumer = Convert.ToInt32(dr.Cells[8].Value.ToString());

            if (RadioBtnNumer == 1)
            {
                TroSh1.Checked = true;
            }
            if (RadioBtnNumer == 2)
            {
                TroSh2.Checked = true;
            }

            if (RadioBtnNumer == 3)
            {
                TroSh3.Checked = true;
            }
            if (RadioBtnNumer == 4)
            {
                TroSh4.Checked = true;
            }

            string FaultStatus = dr.Cells[9].Value.ToString();

            #region fault CheckBox select

            string Num01 = FaultStatus.Substring(0, 1);
            string Num02 = FaultStatus.Substring(1, 1);
            string Num03 = FaultStatus.Substring(2, 1);
            string Num04 = FaultStatus.Substring(3, 1);
            string Num05 = FaultStatus.Substring(4, 1);
            string Num06 = FaultStatus.Substring(5, 1);
            string Num07 = FaultStatus.Substring(6, 1);
            string Num08 = FaultStatus.Substring(7, 1);
            string Num09 = FaultStatus.Substring(8, 1);
            string Num10 = FaultStatus.Substring(9, 1);

            //For the check box 01
            if (Num01 == "1")
            {
                checkBox11.Checked = true;
            }
            if (Num01 == "0")
            {
                checkBox11.Checked = false;
            }
            //For the check box 02
            if (Num02 == "1")
            {
                checkBox2.Checked = true;
            }
            if (Num02 == "0")
            {
                checkBox2.Checked = false;
            }
            //For the check box 03
            if (Num03 == "1")
            {
                checkBox3.Checked = true;
            }
            if (Num03 == "0")
            {
                checkBox3.Checked = false;
            }
            //For the check box 04
            if (Num04 == "1")
            {
                checkBox4.Checked = true;
            }
            if (Num04 == "0")
            {
                checkBox4.Checked = false;
            }
            //For the check box 05
            if (Num05 == "1")
            {
                checkBox1.Checked = true;
            }
            if (Num05 == "0")
            {
                checkBox5.Checked = false;
            }
            //For the check box 06
            if (Num06 == "1")
            {
                checkBox6.Checked = true;
            }
            if (Num06 == "0")
            {
                checkBox6.Checked = false;
            }
            //For the check box 07
            if (Num07 == "1")
            {
                checkBox1.Checked = true;
            }
            if (Num07 == "0")
            {
                checkBox7.Checked = false;
            }
            //For the check box 08
            if (Num08 == "1")
            {
                checkBox8.Checked = true;
            }
            if (Num08 == "0")
            {
                checkBox8.Checked = false;

            }
            //For the check box 09
            if (Num09 == "1")
            {
                checkBox9.Checked = true;
            }
            if (Num09 == "0")
            {
                checkBox9.Checked = false;
            }
            //For the check box 10
            if (Num10 == "1")
            {
                checkBox10.Checked = true;
            }
            if (Num10 == "0")
            {
                checkBox10.Checked = false;
            }

            #endregion

            //payments add to the list view

            //======================================================================
            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                String viewGrideview1 = @"SELECT       RepairJobReson, Amount,JBAutoNumber
                FROM RepairJobTemp_PAyments WHERE JobID='" + JobNo + "'";

                SqlCommand cmd = new SqlCommand(viewGrideview1, con1);
                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ListVPAySumm.Items.Clear();

                while (dr1.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr1[0].ToString());

                    li.SubItems.Add(dr1[1].ToString());
                    li.SubItems.Add(dr1[2].ToString());

                    ListVPAySumm.Items.Add(li);
                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }


                txtseachjob.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }


            //=====================================================================

            PnlJobSearch.Visible = false;


            comboBox5.Enabled = true;
            PaymentAmount.Enabled = true;
            selectjobpayMNethod();

            if (comboBox5.Items.Count > 0)
            {
                comboBox5.DroppedDown = true;
                comboBox5.Focus();
            }
            if (comboBox5.Items.Count <=0)
            {
                PnlPayMeth.Visible = true;
                Reesom.Focus();

            }

            //button2.Enabled = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            PnlPayMeth.Visible = true;
            Reesom.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PnlPayMeth.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Reesom.Text == "" || Amount.Text == "")
                {
                    MessageBox.Show("please complete the details correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Reesom.Text == "" && Amount.Text == "")
                {
                    PnlPayMeth.Visible = false;
                    return;
                }

                if (Reesom.Text != "" && Amount.Text != "")
                {
                    button8.Enabled = true;
                }


                //ckek wherther details available like equal to reson and amount. if it is not insert it to DB
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectCreditCustomer = "SELECT PaymentType,Amount FROM [JOBPaymentMethod] WHERE [PaymentType]='" + Reesom.Text + "' AND Amount='" + Amount.Text + "'";
                SqlCommand cmd1 = new SqlCommand(selectCreditCustomer, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                if (!dr1.Read())
                {
                    SqlConnection con6 = new SqlConnection(IMS);
                    con6.Open();
                    string InsertPaymetmethds = "INSERT INTO JOBPaymentMethod(PaymentType,Amount) VALUES('" + Reesom.Text + "','" + Amount.Text + "')";
                    SqlCommand cmd6 = new SqlCommand(InsertPaymetmethds, con6);
                    cmd6.ExecuteNonQuery();

                    MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PnlPayMeth.Visible = false;
                    button8.Enabled = false;

                    Reesom.Text = "";
                    Amount.Text = "";



                    if (con6.State == ConnectionState.Open)
                    {
                        cmd6.Dispose();
                        con6.Close();
                    }
                }
                else
                {
                    MessageBox.Show("This statement also available in the batabase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Reesom.Text = "";
                    Amount.Text = "";
                }
                if (con1.State == ConnectionState.Open)
                {
                    cmd1.Dispose();
                    con1.Close();
                    dr1.Close();
                }

                selectjobpayMNethod();
                comboBox5.DroppedDown = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void selectjobpayMNethod()
        {
            #region select job pay Method()..............................................................

            try
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusCreditBalane = @"SELECT PaymentType, Amount FROM JOBPaymentMethod";

                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader();
                //SqlDataReader Dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                comboBox5.Items.Clear();

                while (dr2.Read())
                {
                    comboBox5.Items.Add(dr2[0]);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }





        private void comboBox5_Click(object sender, EventArgs e)
        {
            selectjobpayMNethod();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusCreditBalane = @"SELECT PaymentType, Amount,AutoID FROM JOBPaymentMethod WHERE PaymentType='" + comboBox5.Text + "'";

                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader();
                //SqlDataReader Dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //    comboBox5.Items.Clear();

                if (dr2.Read())
                {
                    PaymentAmount.Text = dr2[1].ToString();
                    AutoID.Text = dr2[2].ToString();

                }

                button2.Enabled = true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void InsertInToListVeiw()
        {
            try
            {

                #region check the data is in list========================================================

                for (int i = 0; i <= ListVPAySumm.Items.Count - 1; i++)
                {
                    if (ListVPAySumm.Items[i].SubItems[2].Text == AutoID.Text)
                    {
                        MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox5.Focus();

                        return;
                    }
                }


                #endregion



                ListViewItem li;

                li = new ListViewItem(comboBox5.Text.ToString());

                // li.SubItems.Add(comboBox5.Text.ToString());

                if (Convert.ToDouble(PaymentAmount.Text) == 0)
                {
                    li.SubItems.Add("Free");
                }
                if (Convert.ToDouble(PaymentAmount.Text) != 0)
                {
                    li.SubItems.Add(PaymentAmount.Text.ToString());
                }

                li.SubItems.Add(AutoID.Text.ToString());

                ListVPAySumm.Items.Add(li);

                //////////////////////////////////////////////////////////////////////================================================

                GetTotalAmount();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        public void PAybutton_SaveBtnActiveOrNot()
        {

            if (ListVPAySumm.Items.Count > 0)
            {
                //trobleshoot information pendiig or not=============================
                if (TroSh1.Checked == true)
                {
                    MessageBox.Show("You cannot complete payment Details. because technical solution is pending", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // button5.Enabled = false;
                    button14.Enabled = false;

                    ListVPAySumm.Items.Clear();
                    return;
                }
                if (TroSh2.Checked == true || TroSh3.Checked == true || TroSh4.Checked == true)
                {
                   // button5.Enabled = true;
                    button14.Enabled = true;
                }
            }

            else
            {
               // button5.Enabled = false;
                button14.Enabled = false;

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            InsertInToListVeiw();

            PAybutton_SaveBtnActiveOrNot();

            //double Balance = Convert.ToDouble(PayAmountInvoiced.Text) - Convert.ToDouble(PayAmount.Text) - Convert.ToDouble(PayPreInvoived.Text);

            //PayBalance.Text = Convert.ToString(Balance);
        }

        private void Reesom_TextChanged(object sender, EventArgs e)
        {
            if (Reesom.Text != "" && Amount.Text != "")
            {
                button8.Enabled = true;
            }

            if (Reesom.Text == "" && Amount.Text == "")
            {
                button8.Enabled = false;
            }
        }

        private void Reesom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Reesom.Text != "" && Amount.Text != "")
            {
                button8.Enabled = true;
            }

            if (Reesom.Text == "" || Amount.Text == "")
            {
                button8.Enabled = false;
            }
        }

        private void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Reesom.Text != "" && Amount.Text != "")
            {
                button8.Enabled = true;
            }

            if (Reesom.Text == "" || Amount.Text == "")
            {
                button8.Enabled = false;
            }
        }

        private void PaymentAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyValue== 13)
            {
                InsertInToListVeiw();

                PAybutton_SaveBtnActiveOrNot();
            }
        }

        public void clearTxt()
        {
            #region clear textbox=======================================================================

            JobID.Text = "";
            txtCusID.Text = "";
            txtCusName.Text = "";
            txtEngNote.Text = "";
            txtJobType.Text = "";
            txtCusID.Text = "";
            FaultOther.Text = "";
            PaymentAmount.Text = "";
            LblBillTotalAmount.Text = "000.00";

            ListVPAySumm.Items.Clear();

            checkBox1.Checked = false;
            TroSh1.Checked = false;
            TroSh2.Checked = false;
            TroSh3.Checked = false;
            TroSh4.Checked = false;

            checkBox11.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;

            #endregion
        }

        

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you whether you need to complete the payment Details?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    SqlConnection con6 = new SqlConnection(IMS);
                    con6.Open();
                    string selectJOBID = "SELECT [JobID] FROM [RepairJobTemp_PAyments] WHERE [JobID]='" + JobID.Text + "'";
                    SqlCommand cmd6 = new SqlCommand(selectJOBID, con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader(CommandBehavior.CloseConnection);

                    if (dr6.Read() == true)
                    {

                        SqlConnection con7 = new SqlConnection(IMS);
                        con7.Open();


                        string deleteID = "DELETE FROM [RepairJobTemp_PAyments] WHERE [JobID]='" + JobID.Text + "'";
                        SqlCommand cmd4 = new SqlCommand(deleteID, con7);
                        cmd4.ExecuteNonQuery();

                        //------------------------------------------------------------------------------------------

                        int i;
                        for (i = 0; i <= ListVPAySumm.Items.Count - 1; i++)
                        {
                            SqlConnection con3 = new SqlConnection(IMS);
                            con3.Open();
                            string InsertItemRepairJobDetails = @"INSERT INTO  RepairJobTemp_PAyments (JobID, RepairJobReson, Amount,InvoicedBy,PAymetStaus,JBAutoNumber)
                                                  VALUES(@JobID,@RepairJobReson,@Amount,@InvoicedBy,'Pending',@JBAutoNumber)";

                            SqlCommand cmd3 = new SqlCommand(InsertItemRepairJobDetails, con3);


                            cmd3.Parameters.AddWithValue("JobID", JobID.Text);
                            cmd3.Parameters.AddWithValue("RepairJobReson", ListVPAySumm.Items[i].SubItems[0].Text);
                            cmd3.Parameters.AddWithValue("Amount", ListVPAySumm.Items[i].SubItems[1].Text);
                            cmd3.Parameters.AddWithValue("InvoicedBy", LgUser.Text);
                            cmd3.Parameters.AddWithValue("JBAutoNumber", ListVPAySumm.Items[i].SubItems[2].Text);


                            cmd3.ExecuteNonQuery();

                            if (con3.State == ConnectionState.Open)
                            {
                                cmd3.Dispose();
                                // dr.Close();
                                con3.Close();
                            }
                        }

                    }
                    else
                    {

                        int i;
                        for (i = 0; i <= ListVPAySumm.Items.Count - 1; i++)
                        {
                            SqlConnection con3 = new SqlConnection(IMS);
                            con3.Open();
                            string InsertItemRepairJobDetails = @"INSERT INTO  RepairJobTemp_PAyments (JobID, RepairJobReson, Amount,InvoicedBy,PAymetStaus,JBAutoNumber)
                                                  VALUES(@JobID,@RepairJobReson,@Amount,@InvoicedBy,'Pending',@JBAutoNumber)";

                            SqlCommand cmd3 = new SqlCommand(InsertItemRepairJobDetails, con3);


                            cmd3.Parameters.AddWithValue("JobID", JobID.Text);
                            cmd3.Parameters.AddWithValue("RepairJobReson", ListVPAySumm.Items[i].SubItems[0].Text);
                            cmd3.Parameters.AddWithValue("Amount", ListVPAySumm.Items[i].SubItems[1].Text);
                            cmd3.Parameters.AddWithValue("InvoicedBy", LgUser.Text);
                            cmd3.Parameters.AddWithValue("JBAutoNumber", ListVPAySumm.Items[i].SubItems[2].Text);


                            cmd3.ExecuteNonQuery();

                            if (con3.State == ConnectionState.Open)
                            {
                                cmd3.Dispose();
                                // dr.Close();
                                con3.Close();
                            }
                        }

                    }

                    if (con6.State == ConnectionState.Open)
                    {
                        cmd6.Dispose();
                        con6.Close();
                        dr6.Close();
                    }




                }

                MessageBox.Show("Sucessfully Completed", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearTxt();

                comboBox5.Enabled = false;
                PaymentAmount.Enabled = false;
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void GetTotalAmount()
        {
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in ListVPAySumm.Items)
            {
                if (lstItem.SubItems[1].Text != "Free")
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[1].Text), 2);
                }
            }
            LblBillTotalAmount.Text = Convert.ToString(gtotal);

            

        }

        private void ListVPAySumm_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalAmount();

            PAybutton_SaveBtnActiveOrNot();
        }

        private void ListVPAySumm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListVPAySumm.SelectedItems[0].Remove();
            GetTotalAmount();

            PAybutton_SaveBtnActiveOrNot();
            
        }

        private void txtseachjob_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = "SELECT ReJobNumber,ReCusID,CusFirstName,ReCompletingDate,ReCompletingTime,JobType,ReEngNote,ReCusNote,Solution FROM RepairNotes WHERE PaymentDetails='Pending'AND (ReJobNumber  LIKE'%" + txtseachjob.Text + "%' OR CusFirstName LIKE '%" + txtseachjob.Text + "%' )";
                SqlCommand cmd = new SqlCommand(viewGrideview, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dgvjobIdRepairing.Rows.Clear();

                while (dr.Read() == true)
                {
                    dgvjobIdRepairing.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8]);

                }
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

        private void button7_Click(object sender, EventArgs e)
        {
            PnlJobSearch.Visible = false;
        }

        private void Amount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                button8.Focus();
            }
        }

        private void Reesom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Amount.Focus();
            }
        }

        private void PnlPayMeth_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                comboBox5.DroppedDown = true;
            }
        }

        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                button2.Focus();
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                ListVPAySumm.Focus();
            }
        }

        private void ListVPAySumm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button14.Focus();
            }
        }
    }
}
