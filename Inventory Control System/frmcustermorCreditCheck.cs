using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Control_System
{
    public partial class frmcustermorCreditCheck : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

        public frmcustermorCreditCheck()
        {
            InitializeComponent();
            LoadCus();
        }
        public void LoadCus()
        {
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String Cus = "select CusID from CustomerDetails";
                SqlCommand cmm = new SqlCommand(Cus, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        private void frmcustermorCreditCheck_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;
            rbtAll.Checked = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String Cus = "select CusID,CusFirstName,CusLastName from CustomerDetails where CusID='" + comboBox1.Text + "'";
                SqlCommand cmm = new SqlCommand(Cus, cnn);
                // comboBox1.Items.Clear();
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    label1.Text = (dr[1].ToString() + "  " + dr[2].ToString());
                }

                // -----------------------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void rbtByCus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtByCus.Checked == true)
            {
                comboBox1.Enabled = true;
            }
            if (rbtByCus.Checked == false)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = -1;

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //SqlConnection cnn = new SqlConnection(IMS);
            //cnn.Open();
            //String Cus = "select CusID,CusFirstName,CusLastName from CustomerDetails";
            //SqlCommand cmm = new SqlCommand(Cus, cnn);
            //SqlDataReader dr = cmm.ExecuteReader();
            //while (dr.Read())
            //{
            //    label1.Text = (dr[1].ToString() + "  " + dr[2].ToString());
            //}

            //-----------------------------------------------------------------
            try
            {
                string totalTables = "";

                //select the print datatables number----------------
                SqlConnection conx = new SqlConnection(IMS);
                conx.Open();

                string ReSelecttableNumbers = @"SELECT RptNumbers FROM RptNumbers";
                SqlCommand cmdx = new SqlCommand(ReSelecttableNumbers, conx);
                SqlDataReader drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

                if (drx.Read() == true)
                {
                    totalTables = drx[0].ToString();
                }

                if (conx.State == ConnectionState.Open)
                {
                    conx.Close();
                    drx.Close();
                }
                //===================================================================
                #region Load report.........................................

                if (rbtByCus.Checked == true)
                {
                    #region By Customer.................................................

                    rptCheckCustomerCredit rpt = new rptCheckCustomerCredit();

                    TextObject ByCus;

                    if (rpt.ReportDefinition.ReportObjects["Text16"] != null)
                    {
                        ByCus = (TextObject)rpt.ReportDefinition.ReportObjects["Text16"];
                        ByCus.Text = label1.Text;

                    }


                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();
                    String Check = @"SELECT     SoldInvoiceDetails.CusStatus,SoldInvoiceDetails.CusFirstName,SoldInvoiceDetails.CusPersonalAddress,InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.InvoiceDate, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance
            FROM         InvoicePaymentDetails FULL OUTER JOIN
            SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo FULL OUTER JOIN
            RepairNotes ON InvoicePaymentDetails.InvoiceID = RepairNotes.ReJobNumber 
            WHERE     (InvoicePaymentDetails.PayBalance > 0) AND (SoldInvoiceDetails.CusStatus='" + comboBox1.Text + "' OR RepairNotes.ReCusID='" + comboBox1.Text + "')";
                    SqlDataAdapter sdr = new SqlDataAdapter(Check, cnn1);
                    IMSDataSET sa = new IMSDataSET();
                    sdr.Fill(sa);


                    rpt.SetDataSource(sa.Tables[Convert.ToInt32(totalTables)]);
                    viwerCheckCusCredit.ReportSource = rpt;
                    viwerCheckCusCredit.Refresh();
                    cnn1.Close();
                    #endregion
                }


                if (rbtAll.Checked == true)
                {

                    #region All Customer...................................................

                    TextObject allCuss;

                    rptCheckCustomerCredit rpt = new rptCheckCustomerCredit();



                    if (rpt.ReportDefinition.ReportObjects["Text16"] != null)
                    {
                        allCuss = (TextObject)rpt.ReportDefinition.ReportObjects["Text16"];
                        allCuss.Text = label1.Text;

                    }


                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();
                    String Check = @"SELECT     SoldInvoiceDetails.CusStatus,SoldInvoiceDetails.CusFirstName,SoldInvoiceDetails.CusPersonalAddress,InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.InvoiceDate, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance
            FROM         InvoicePaymentDetails FULL OUTER JOIN
            SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo FULL OUTER JOIN
            RepairNotes ON InvoicePaymentDetails.InvoiceID = RepairNotes.ReJobNumber 
            WHERE     (InvoicePaymentDetails.PayBalance > 0)";
                    SqlDataAdapter sdr = new SqlDataAdapter(Check, cnn1);
                    IMSDataSET sa = new IMSDataSET();
                    sdr.Fill(sa);


                    rpt.SetDataSource(sa.Tables[Convert.ToInt32(totalTables)]);
                    viwerCheckCusCredit.ReportSource = rpt;
                    viwerCheckCusCredit.Refresh();
                    cnn1.Close();
                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void rbtAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAll.Checked == true)
            {

                label1.Text = "All Customers";
            }
        }
    }
}
