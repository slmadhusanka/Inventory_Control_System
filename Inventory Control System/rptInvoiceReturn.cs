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
    public partial class rptInvoiceReturn : Form
    {
        public rptInvoiceReturn()
        {
            InitializeComponent();
        }
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string PrintingRTNInvoiceNumber = "";

        public string PrintCopyDetails = "";

        public string GRN_Type = "";

        public void PrintJob()
        {
            try
            {
                #region Job printing========================================

                string totalTables = "";

                string ReSelectQ = "";

                //select the print datatables number----------------
                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();

                string ReSelecttableNumbers = @"SELECT RptNumbers FROM RptNumbers";
                SqlCommand cmd2 = new SqlCommand(ReSelecttableNumbers, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr2.Read() == true)
                {
                    totalTables = dr2[0].ToString();
                }

                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                    dr2.Close();
                }

                //-------------------------------------------------

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();





                #region Report items with Serial numbers------------------------------------

                RPTinvoiceReturnNote rpt1 = new RPTinvoiceReturnNote();

                //TextObject CopyDetails;


                //if (rpt1.ReportDefinition.ReportObjects["CopyDetails"] != null)
                //{
                //    CopyDetails = (TextObject)rpt1.ReportDefinition.ReportObjects["PrintCopyDetails"];
                //    CopyDetails.Text = CopyType;

                //}

                ReSelectQ = @"SELECT        SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CusTelNUmber, SoldInvoiceDetails.CusStatus, 
                         Return_Note.Invoice_Num, Return_Note.Return_Num, Return_Note.Batch_Num, Return_Note.Item_ID, Return_Note.Itm_Name, Return_Note.Serial_Num, 
                         Return_Note.Warr_Period, Return_Note.Selling_Price, Return_Note.Return_Qnty, Return_Note.Tot_Amount, Return_Note.Return_Statement, 
                         Return_Note.Return_Accept_by, Return_Note.Return_Date
FROM            Return_Note INNER JOIN
                         SoldInvoiceDetails ON Return_Note.Invoice_Num = SoldInvoiceDetails.InvoiceNo where Return_Note.Return_Num='" + PrintingRTNInvoiceNumber + "' ";

                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds1 = new IMSDataSET();
                dscmd.Fill(ds1);

                //ODS1042   WHERE     (CurrentStockItems.OrderID = '" + PrintingGRNNumber + "')";
                //view the christtal report


                // RptRepairJobAdding rpt1 = new RptRepairJobAdding();
                rpt1.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                ReportViewInvoiceReturn.ReportSource = rpt1;
                ReportViewInvoiceReturn.Refresh();

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }
        private void rptInvoiceReturn_Load(object sender, EventArgs e)
        {
            PrintJob();
        }

        private void RPTinvoiceReturnNote1_InitReport(object sender, EventArgs e)
        {

        }
    }
}
