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
    public partial class RptInvoiceDetails : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public RptInvoiceDetails()
        {
            InitializeComponent();
        }

        private void RptInvoiceDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RptInvoicesVeivew.Visible = true;
            RptInvoicesVeivew.BringToFront();

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

                //double days = Convert.ToDouble(DateTo.Text.Trim());
                string time = Convert.ToDateTime(DateTo.Text.Trim()).AddDays(1).ToShortDateString();
                //DateTime timeIn = Convert.ToDateTime(OutFrom.Text.Trim());

                InvoiceSold rpt = new InvoiceSold();

                TextObject From;
                TextObject To;

                if (rpt.ReportDefinition.ReportObjects["DateFrom"] != null)
                {
                    From = (TextObject)rpt.ReportDefinition.ReportObjects["DateFrom"];
                    From.Text = dateFrom.Text;

                    //From.Text = "Jude123";

                }

                if (rpt.ReportDefinition.ReportObjects["DateTo"] != null)
                {
                    To = (TextObject)rpt.ReportDefinition.ReportObjects["DateTo"];
                    To.Text = DateTo.Text;
                }


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @" SELECT SoldInvoiceDetails.InvoiceNo, SoldInvoiceDetails.InvoiceStatus, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                      SoldItemDetails.BuyPrice, SoldItemDetails.SoldPrice, InvoicePaymentDetails.InvoiceDate,SoldInvoiceDetails.CusStatus,
                      SoldInvoiceDetails.CusFirstName,SoldInvoiceDetails.CusTelNUmber,SoldInvoiceDetails.InvoiceRemark
                      FROM SoldInvoiceDetails INNER JOIN
                      SoldItemDetails ON SoldInvoiceDetails.InvoiceNo = SoldItemDetails.InvoiceID INNER JOIN
                      InvoicePaymentDetails ON SoldItemDetails.InvoiceID = InvoicePaymentDetails.InvoiceID
                      
                      WHERE InvoicePaymentDetails.InvoiceDate >= '" +dateFrom.Text+"' AND InvoicePaymentDetails.InvoiceDate <= '"+time+"' AND SoldInvoiceDetails.InvoiceStatus='Sold' ORDER BY SoldInvoiceDetails.InvoiceNo ";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                

                //view the christtal report
              //  InvoiceSold rpt = new InvoiceSold();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                RptInvoicesVeivew.ReportSource = rpt;
                RptInvoicesVeivew.Refresh();
                con1.Close();

                //pass value to the report (dates From TO)

                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void InvoiceSold1_InitReport(object sender, EventArgs e)
        {

        }
    }
}
