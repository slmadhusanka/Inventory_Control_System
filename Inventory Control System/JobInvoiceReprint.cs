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
    public partial class JobInvoiceReprint : Form
    {
        public JobInvoiceReprint()
        {
            InitializeComponent();
            loadjobinvoice();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public void loadjobinvoice()
        {
              try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();


                string SelInvoice = "SELECT InvoiceID FROM InvoicePaymentDetails where InvoiceID like 'REJ%'";
                SqlCommand cmd = new SqlCommand(SelInvoice, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read() == true)
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
             }
            catch(Exception ex)
              {
                  MessageBox.Show(ex.Message);
            }
        }
  



        public void printthebill()
        {
            #region bill print================================

            //string InVoiceNum = comboBox1.Text;


            if (comboBox1.Text == "")
            {
                MessageBox.Show("please enter Valied Invoice Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
            }




            try
            {

                //SqlConnection con = new SqlConnection(IMS);
                //con.Open();


                //string SelInvoice = "SELECT InvoiceID FROM InvoicePaymentDetails WHERE InvoiceID='" + InVoiceNum + "'";
                //SqlCommand cmd = new SqlCommand(SelInvoice, con);
                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //if (dr.Read() == true)
                //{
                    RptRepairJobFinal rptfrm = new RptRepairJobFinal();
                    rptfrm.PrintingJOBNumber = comboBox1.Text;
                    rptfrm.InvoiceCopyType = "RePrint Document";
                    rptfrm.button1.Enabled = false;
                    rptfrm.button2.Enabled = false;


                    rptfrm.Visible = true;

                   // TxtRePrint.Text = "";
                //}

                //else
                //{
                //    MessageBox.Show("Please enter correct invoice number", "Error in Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    TxtRePrint.Text = "";
                //    TxtRePrint.Focus();
                //}

                //TxtRePrint.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }


        private void button1_Click(object sender, EventArgs e)
        {
            printthebill();
        }

        private void JobInvoiceReprint_Load(object sender, EventArgs e)
        {
            //TxtRePrint.Focus();
        }

        private void TxtRePrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                printthebill();
            }
        }

        private void JobInvoiceReprint_Activated(object sender, EventArgs e)
        {
           // TxtRePrint.Focus();
        }
    }
}
