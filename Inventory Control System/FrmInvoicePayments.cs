using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;


namespace Inventory_Control_System
{
    public partial class FrmInvoicePayments : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public FrmInvoicePayments()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InvoicePaymentsViewer.Visible = true;
            InvoicePaymentsViewer.BringToFront();

            try
            {
                string totalTables = "";

                //select the print datatables number-----------------------------------------------------
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

                //---------------------------------------------------------------------------------------

               // string time = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                RptInvoicePayments rpt = new RptInvoicePayments();

                #region pass value to the report (dates From TO)---------------------------------------------------------

                TextObject From;
                TextObject To;

                if (rpt.ReportDefinition.ReportObjects["Text20"] != null)
                {
                    From = (TextObject)rpt.ReportDefinition.ReportObjects["Text20"];
                    From.Text = PickerDateFrom.Text;

                    //From.Text = "Jude123";

                }

                if (rpt.ReportDefinition.ReportObjects["Text21"] != null)
                {
                    To = (TextObject)rpt.ReportDefinition.ReportObjects["Text21"];
                    To.Text = PickerDateTo.Text;
                }

                #endregion
                


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT     InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.SubTotal, InvoicePaymentDetails.VATpresentage, InvoicePaymentDetails.GrandTotal, 
                        InvoicePaymentDetails.PayCash, InvoicePaymentDetails.PayCheck, InvoicePaymentDetails.PayCrditCard, InvoicePaymentDetails.PayDebitCard, 
                        InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance, InvoicePaymentDetails.InvoiceDate
                        FROM         InvoicePaymentDetails INNER JOIN
                        SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                        WHERE InvoicePaymentDetails.InvoiceDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + PickerDateTo.Text + "' AND (SoldInvoiceDetails.InvoiceStatus = 'Sold') AND (InvoicePaymentDetails.InvoiceID LIKE 'INV%') ORDER BY InvoicePaymentDetails.InvoiceID";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);



                //view the christtal report
                
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                InvoicePaymentsViewer.ReportSource = rpt;
                InvoicePaymentsViewer.Refresh();
                con1.Close();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void FrmInvoicePayments_Load(object sender, EventArgs e)
        {

        }
    }
}
