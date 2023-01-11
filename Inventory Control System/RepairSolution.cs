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
    public partial class RepairSolution : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

        public RepairSolution()
        {
            InitializeComponent();
        }

        private void textBox12_KeyUp(object sender, KeyEventArgs e)
        {
              
            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = "SELECT ReJobNumber,ReCusID,CusFirstName,ReCompletingDate,ReCompletingTime,JobType,ReEngNote,ReCusNote,Solution,FaultStatus,PaymentStatus FROM RepairNotes WHERE PaymentDetails='Pending'AND (ReJobNumber  LIKE'%" + txtseachjob.Text + "%' OR CusFirstName LIKE '%" + txtseachjob.Text + "%' )";
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            PnlJobSearch.Visible = true;

            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = "SELECT ReJobNumber,ReCusID,CusFirstName,ReCompletingDate,ReCompletingTime,JobType,ReEngNote,ReCusNote,Solution FROM RepairNotes WHERE PaymentDetails='Pending'";
                SqlCommand cmd = new SqlCommand(viewGrideview, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dgvjobIdRepairing.Rows.Clear();

                while (dr.Read() == true)
                {
                    dgvjobIdRepairing.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5],dr[6],dr[7],dr[8]);

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

        //SelecteradioButton
       

        private void dgvjobIdRepairing_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
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
                    rbtFixed.Checked = true;
                }

                if (RadioBtnNumer == 3)
                {
                    rbtCanNot.Checked = true;
                }
                if (RadioBtnNumer == 4)
                {
                    rbtnotfully.Checked = true;
                }


                PnlJobSearch.Visible = false;
                TroSh1.Focus();
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

        private void RepairSolution_Load(object sender, EventArgs e)
        {
            //MainForm mfm = new MainForm();

            //LgDisplayName.Text = mfm.LoginPerson.Text;
            //LgUser.Text = mfm.LoginUserID.Text;

            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                txtcusNot.Enabled = true;

            }
            else 
            {
                txtcusNot.Enabled = false;
            }

        }
        public void waranty()
        {
            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string a="";
            if (TroSh1.Checked)
            {
                a = "1";
            }
            if (rbtFixed.Checked)
            {
                a = "2";
            }
            if (rbtCanNot.Checked)
            {
                a = "3";
            }
            if (rbtnotfully.Checked)
            {
                a = "4";
            }


            if (JobID.Text == "")
            {
                MessageBox.Show("Please select Job Number", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                string RepairUP = "UPDATE RepairNotes SET ReEngNote='" + txtEngNote.Text + "',ReCusNote='" + txtcusNot.Text + "',Solution='"+a+"' WHERE ReJobNumber='" + JobID.Text + "'";
                SqlCommand cmd = new SqlCommand(RepairUP, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                JobID.Clear();
                txtCusID.Clear();
                txtCusName.Clear();
               //this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
               //this.dateTimePicker1.CustomFormat = " ";
               //this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
               //this.dateTimePicker2.CustomFormat = " ";
                dtpDate.Value =DateTime.Now;
                this.dtTime.Value = DateTime.Now;
               txtJobType.Clear();
               TroSh1.Checked = false;
               rbtFixed.Checked = false;
               rbtCanNot.Checked = false;
               rbtnotfully.Checked = false;
               txtEngNote.Clear();
               txtcusNot.Clear();
               checkBox1.Checked = false;

                    



                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dgvjobIdRepairing_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PnlJobSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvjobIdRepairing_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                TroSh1.Focus();

            }
        }

        private void TroSh1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtEngNote.Focus();

            }
        }

        private void rbtFixed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtEngNote.Focus();

            }
        }

        private void rbtCanNot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtEngNote.Focus();

            }
        }

        private void rbtnotfully_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtEngNote.Focus();

            }
        }

        private void txtEngNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                checkBox1.Focus();

            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (e.KeyValue == 13)
                {
                    txtcusNot.Focus();

                }
            }
            else
            {
                BTNUpadate.Focus();
            }
        }
    }
}
