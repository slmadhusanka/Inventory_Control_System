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
    public partial class ReprintInvoice : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public ReprintInvoice()
        {
            InitializeComponent();
            loadinvoice();
        }

        public void loadinvoice()
        {
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                string SelInvoice = "SELECT InvoiceNo FROM SoldInvoiceDetails ";
                SqlCommand cmd = new SqlCommand(SelInvoice, cnn);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read() == true)
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        private void TxtRePrint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        public void rreprintdetails()
        {
            string InVoiceNum = comboBox1.Text;

            if (comboBox1.Text == "")
            {
                MessageBox.Show("please enter Valied Invoice Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
            }

            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                string SelInvoice = "SELECT InvoiceNo FROM SoldInvoiceDetails WHERE InvoiceNo='" + comboBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(SelInvoice, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.Read() == true)
                {
                    BillForm bf = new BillForm();
                    bf.InvoiveNumberToPrint = InVoiceNum;
                    bf.PrintCopyDetails = "Duplicated Copy";
                    bf.Show();

                   // TxtRePrint.Text = "";
                }

                else
                {
                    MessageBox.Show("Please enter correct invoice number", "Error in Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // TxtRePrint.Text = "";
                   // TxtRePrint.Focus();
                }

                //TxtRePrint.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rreprintdetails();

        }

        private void ReprintInvoice_Load(object sender, EventArgs e)
        {
           // TxtRePrint.Focus();
        }

        private void TxtRePrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                rreprintdetails();
            }
        }

        private void ReprintInvoice_Activated(object sender, EventArgs e)
        {
            //TxtRePrint.Focus();
        }
    }
}
