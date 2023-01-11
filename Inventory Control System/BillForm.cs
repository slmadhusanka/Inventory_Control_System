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
    public partial class BillForm : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

       public string InvoiveNumberToPrint = "";

       public string PrintCopyDetails = "";

        public BillForm()
        {
            InitializeComponent();
            

        }

       

        private void BillForm_Load(object sender, EventArgs e)
        {
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

                //-------------------------------------------------

                ////return karapu items walata daapu invoice 1kda nadda kiyaala baanwa------------------------------
                //SqlConnection conq = new SqlConnection(IMS);
                //conq.Open();

                //string Return_Invoice = @"SELECT RptNumbers FROM RptNumbers";
                //SqlCommand cmdq = new SqlCommand(Return_Invoice, conq);
                //SqlDataReader drq = cmdq.ExecuteReader(CommandBehavior.CloseConnection);

                //if (drq.Read() == true)
                //{
                //    totalTables = drx[0].ToString();
                //}

                ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                CustomerInvoice rpt = new CustomerInvoice();

                TextObject CopyDetails;


                if (rpt.ReportDefinition.ReportObjects["CopyDetails"] != null)
                {
                    CopyDetails = (TextObject)rpt.ReportDefinition.ReportObjects["CopyDetails"];
                    CopyDetails.Text = PrintCopyDetails;

                }

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string ReSelectQ = @" SELECT        InvoicePaymentDetails.InvoiceID AS Expr1, SoldItemDetails.InvoiceID AS Expr3, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                         SoldItemDetails.SystemID, SoldItemDetails.ItemWarrenty, SoldItemDetails.SoldPrice, InvoiceCheckDetails.InvoiceID AS Expr2, InvoiceCheckDetails.CkNumber, 
                         InvoiceCheckDetails.Bank, InvoiceCheckDetails.Amount, InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.SubTotal, InvoicePaymentDetails.VATpresentage, 
                         InvoicePaymentDetails.GrandTotal, InvoicePaymentDetails.PayCash, InvoicePaymentDetails.PayCheck, InvoicePaymentDetails.PayCrditCard, 
                         InvoicePaymentDetails.PayDebitCard, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance, InvoicePaymentDetails.InvoiceDate, 
                         InvoicePaymentDetails.InvoiceDiscount, SoldInvoiceDetails.InvoiceNo, SoldInvoiceDetails.InvoiceStatus, SoldInvoiceDetails.CusStatus, 
                         SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CusTelNUmber, SoldInvoiceDetails.CreatedBy, 
                         SoldInvoiceDetails.InvoiceRemark,SoldInvoiceDetails.Return_Invoice_Or_Original,SoldInvoiceDetails.Return_Invoice_Num, SoldItemDetails.ItmQuantity, SoldItemDetails.FreeQuantity, SoldItemDetails.BatchID
                        FROM            InvoiceCheckDetails FULL OUTER JOIN
                         InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                         SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                         SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                      WHERE (InvoicePaymentDetails.InvoiceID = '" + InvoiveNumberToPrint + "')";


            SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
            IMSDataSET ds = new IMSDataSET();
            dscmd.Fill(ds);

            //view the christtal report
           
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            BillCrystalReportViwer.ReportSource = rpt;
            BillCrystalReportViwer.Refresh();
            con1.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("THis error come form the Billng CRP. please comtact your system administrator.", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }


        }
    }
}
