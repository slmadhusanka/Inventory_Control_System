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
using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;

namespace Inventory_Control_System
{
    public partial class RptRepairJobAddingFrm : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string PrintingJOBNumber = "";

        public string PrintCopyDetails = "";

        public RptRepairJobAddingFrm()
        {
            InitializeComponent();
        }


        public void PrintJob(string CopyType)
        {
            try
            {
                #region Job printing========================================

                string totalTables = "";

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


                RptRepairJobAdding rpt = new RptRepairJobAdding();

                TextObject CopyDetails;


                if (rpt.ReportDefinition.ReportObjects["CopyDetails"] != null)
                {
                    CopyDetails = (TextObject)rpt.ReportDefinition.ReportObjects["CopyDetails"];
                    CopyDetails.Text = CopyType;

                }

                // Report 
                string ReSelectQ = @" SELECT RepairNotes.ReJobNumber, RepairNotes.PaymentStatus, RepairNotes.PaymentDetails, RepairNotes.JobStatus, RepairNotes.LgUser, RepairNotes.Tecnician, 
                      RepairNotes.ReCusID, RepairNotes.CusFirstName, RepairNotes.CusPersonalAddress, RepairNotes.CusTelNUmber, RepairNotes.FaultStatus, RepairNotes.FaultOther, 
                      RepairNotes.RePowerCbl, RepairNotes.ExtraCoolingFans, RepairNotes.ReUSB, RepairNotes.ReSystemOther, RepairNotes.ReCompletingDate, 
                      RepairNotes.ReCompletingTime, RepairNotes.TroblShootInfor, RepairNotes.JobType, RepairNotes.TimeStamp, RepairNoteItems.ItemID, RepairNoteItems.ItemName, 
                      RepairNoteItems.Warrany, RepairNoteItems.BarcodeSerial, RepairNoteItems.PurchesDate, RepairNoteItems.ItemStatus, RepairNoteItems.InvoceBarrowID
                      FROM RepairNotes INNER JOIN
                      RepairNoteItems ON RepairNotes.ReJobNumber = RepairNoteItems.ReJobNumber
                      WHERE (RepairNotes.ReJobNumber = '" + PrintingJOBNumber + "')";

                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                //view the christtal report
                // RptRepairJobAdding rpt1 = new RptRepairJobAdding();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                CryRepairJobAdding.ReportSource = rpt;
                CryRepairJobAdding.Refresh();


                con1.Close();

                //without viewing print the document using default printer
                //PrinterSettings getprinterName = new PrinterSettings();
                //rpt.PrintOptions.PrinterName = getprinterName.PrinterName;
                //rpt.PrintToPrinter(1, true, 1, 1);


                //ReportDocument cryRpt = new ReportDocument();
                //some your code in here
                //cryRpt.Refresh();
                //cryRpt.PrintToPrinter(2, true, 0, 0); //HERE! 1st number in breckets represents the number of copies

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void RptRepairJobAddingFrm_Load(object sender, EventArgs e)
        {
            PrintJob(PrintCopyDetails);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PrintCopyDetails == "Updated copy")
            {
                string a = "Updated Copy";
                PrintJob(a);
            }

            if (PrintCopyDetails == "Original copy")
            {
                string a = "Original copy";
                PrintJob(a);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PrintCopyDetails == "Updated copy")
            {
                string a = "Updated Customer Copy";
                PrintJob(a);
            }

            if (PrintCopyDetails == "Original copy")
            {
                string a = "Customer Copy";
                PrintJob(a);
            }

          
        }
    }
}
