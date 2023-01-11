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
    public partial class RptRepairJobFinal : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string PrintingJOBNumber = "";

        public string InvoiceCopyType = "";

        //string OriginalCopy = "Original Copy";
        //string CusCopy = "Customer Copy";

        public RptRepairJobFinal()
        {
            InitializeComponent();
        }

        public void Printbill(string InvoiceCopyType)
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

                RptRepairJobFinalBill rpt = new RptRepairJobFinalBill();

                TextObject CopyType;

                if (rpt.ReportDefinition.ReportObjects["TxtCopyType"] != null)
                {
                    CopyType = (TextObject)rpt.ReportDefinition.ReportObjects["TxtCopyType"];
                    CopyType.Text = InvoiceCopyType;


                }

                string ReSelectQ2 = @"SELECT     RepairNotes.ReJobNumber, RepairNotes.JobStatus, RepairNotes.LgUser, RepairNotes.Tecnician, RepairNotes.ReCusID, RepairNotes.CusFirstName, 
                      RepairNotes.CusPersonalAddress, RepairNotes.CusTelNUmber, RepairNotes.FaultStatus, RepairNotes.FaultOther, RepairNotes.RePowerCbl, 
                      RepairNotes.ExtraCoolingFans, RepairNotes.ReUSB, RepairNotes.ReSystemOther, RepairNotes.ReCompletingDate, RepairNotes.ReCompletingTime, 
                      RepairNotes.TroblShootInfor, RepairNotes.JobType, RepairNotes.ReEngNote, RepairNotes.ReCusNote, RepairNotes.Solution, RepairNotes.TimeStamp, 
                      RepairJobPAyments.JobID, RepairJobPAyments.RepairJobReson, RepairJobPAyments.Amount, RepairJobPAyments.InvoicedBy, InvoicePaymentDetails.PayCash, 
                      InvoicePaymentDetails.VATpresentage, InvoicePaymentDetails.GrandTotal, InvoicePaymentDetails.PayCheck, InvoicePaymentDetails.SubTotal, 
                      InvoicePaymentDetails.PayCrditCard, InvoicePaymentDetails.PayDebitCard, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance, 
                      InvoicePaymentDetails.InvoiceDate, RepairNotes.PaymentStatus, InvoiceCheckDetails.Bank, InvoiceCheckDetails.Amount AS Expr2, 
                      InvoiceCheckDetails.CkNumber
                        FROM         RepairNotes FULL OUTER JOIN
                      RepairJobPAyments ON RepairNotes.ReJobNumber = RepairJobPAyments.JobID FULL OUTER JOIN
                      InvoicePaymentDetails ON RepairNotes.ReJobNumber = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                      InvoiceCheckDetails ON RepairNotes.ReJobNumber = InvoiceCheckDetails.InvoiceID
                                    WHERE     (RepairNotes.ReJobNumber='" + PrintingJOBNumber + "')";


                SqlDataAdapter dscmd2 = new SqlDataAdapter(ReSelectQ2, con2);

                IMSDataSET ds = new IMSDataSET();

                dscmd2.Fill(ds);


                //view the christtal report

                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                RptRepairJobFinalBill.ReportSource = rpt;
                RptRepairJobFinalBill.Refresh();



                con2.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void RptRepairJobFinal_Load(object sender, EventArgs e)
        {
           
            Printbill(InvoiceCopyType);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a = "Original Copy";
            Printbill(a);

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "Customer Copy";
            Printbill(a);
        }

        private void RptRepairJobFinalBill_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
