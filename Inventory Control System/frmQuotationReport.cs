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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.Sql;
//using System.Configuration;
using System.Drawing.Printing;

namespace Inventory_Control_System
{
    public partial class frmQuotationReport : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string QutaIDPass123 = "";

        public frmQuotationReport()
        {
            InitializeComponent();
        }

        private void frmQuotationReport_Load(object sender, EventArgs e)
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


            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String QuotationID = @"SELECT        Quotation_Details.Quotation_ID, Quotation_Details.ItemNo, Quotation_Details.ItemName, Quotation_Details.SellingPrice, Quotation_Details.QTY, 
                         Quotation_Details.freeQTY, Quotation_Details.Itemdiscount, Quotation_Details.total, Quotation_Details_DOCUME.CusName, 
                         Quotation_Details_DOCUME.CusID, Quotation_Details_DOCUME.CusAdd, Quotation_Details_DOCUME.NBT, Quotation_Details_DOCUME.DocDiscount, 
                         Quotation_Details_DOCUME.GrandTotal, Quotation_Details_DOCUME.Other, Quotation_Details_DOCUME.Date, Quotation_Details_DOCUME.Header, 
                         Quotation_Details_DOCUME.Body, Quotation_Details_DOCUME.VAT
                         FROM            Quotation_Details INNER JOIN
                         Quotation_Details_DOCUME ON Quotation_Details.Quotation_ID = Quotation_Details_DOCUME.Quotation_ID where Quotation_Details.Quotation_ID='"+QutaIDPass123+"'";
            SqlDataAdapter drc = new SqlDataAdapter(QuotationID, cnn);
            IMSDataSET sd = new IMSDataSET();
            drc.Fill(sd);


            rptQuotation_Report rpt1 = new rptQuotation_Report();
            rpt1.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
            ViwerQuotationReport.ReportSource = rpt1;
            ViwerQuotationReport.Refresh();
            cnn.Close();
        }
    }
}
