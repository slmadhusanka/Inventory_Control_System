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
    public partial class rptPurchaseOrderReportForm : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;



        public string PuchaseReport = "";

        public rptPurchaseOrderReportForm()
        {
            InitializeComponent();
        }

        private void rptPurchaseOrderReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                String totalTables = "";

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
                //....................................................................................................

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String PurchesID = @"SELECT        Purchase_Order.Purchase_ID, Purchase_Order.ItemNo,Purchase_Order.ItemName, Purchase_Order.SellingPrice, Purchase_Order.QTY, Purchase_Order.freeQTY, 
                         Purchase_Order.Itemdiscount, Purchase_Order.total, PurchaseOrder_DOCUME.CusID, PurchaseOrder_DOCUME.CusName, 
                         PurchaseOrder_DOCUME.CusAdd, PurchaseOrder_DOCUME.NBT, PurchaseOrder_DOCUME.VAT, PurchaseOrder_DOCUME.DocDiscount, 
                         PurchaseOrder_DOCUME.GrandTotal, PurchaseOrder_DOCUME.Other, PurchaseOrder_DOCUME.Date, PurchaseOrder_DOCUME.Header, 
                         PurchaseOrder_DOCUME.Body
FROM            Purchase_Order INNER JOIN
                         PurchaseOrder_DOCUME ON Purchase_Order.Purchase_ID = PurchaseOrder_DOCUME.PurchesID where Purchase_Order.Purchase_ID='" + PuchaseReport + "' ";
                SqlDataAdapter dscmd = new SqlDataAdapter(PurchesID, cnn);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);



                //view the christtal report
                // TextObject StatusWise;
                rptPurchaseOrderReport rpt = new rptPurchaseOrderReport();




                //  TextObject to = (TextObject)rpt.ReportDefinition.Sections["Section2"].ReportObjects["Body1"];



                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                viewPurchaseOrderReport.ReportSource = rpt;
                viewPurchaseOrderReport.Refresh();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

         
    }
}
