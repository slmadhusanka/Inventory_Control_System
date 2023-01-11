using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class RptPendingRepairs : Form
    {
        public RptPendingRepairs()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

        public string PrintDocumetType = "";





        public void PendingPayments()
        {
            #region Print bill code-----------------------------------------

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



                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();

                // RptRepairJobFinalBill rpt1 = new RptRepairJobFinalBill();

                RptPendingRepairing rpt = new RptPendingRepairing();

                //TextObject Frm;

                //if (rpt.ReportDefinition.ReportObjects["DateFrom"] != null)
                //{
                //    Frm = (TextObject)rpt.ReportDefinition.ReportObjects["DateFrom"];
                //    Frm.Text = PickerDateFrom.Text;

                //}

                //TextObject Dto;

                //if (rpt.ReportDefinition.ReportObjects["DateTo"] != null)
                //{
                //    Dto = (TextObject)rpt.ReportDefinition.ReportObjects["DateTo"];
                //    Dto.Text = PickerDateTo.Text;
                //}



                string ReSelectQ2 = @"SELECT ReJobNumber, PaymentStatus, Tecnician, ReCusID, CusFirstName, ReCompletingDate, ReCompletingTime, CusTelNUmber
FROM            RepairNotes WHERE PaymentDetails='Pending'";
                //WHERE InvoicePaymentDetails.InvoiceDate >= '"+dateFrom.Text+"' AND InvoicePaymentDetails.InvoiceDate <= '"+time+"' AND SoldInvoiceDetails.InvoiceStatus='Sold' ORDER BY SoldInvoiceDetails.InvoiceNo ";

                SqlDataAdapter dscmd2 = new SqlDataAdapter(ReSelectQ2, con2);

                IMSDataSET ds = new IMSDataSET();

                dscmd2.Fill(ds);


                //view the christtal report

                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                CryViewerPending.ReportSource = rpt;
                CryViewerPending.Refresh();



                con2.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void RptPendingRepairs_Load(object sender, EventArgs e)
        {
            PendingPayments();
        }
    }
}
