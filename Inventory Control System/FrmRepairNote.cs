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
    public partial class FrmRepairNote : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string passedValv = ""; 

        public FrmRepairNote(string JOBNUM)
        {
            passedValv = JOBNUM;

            InitializeComponent();
        }

        private void FrmRepairNote_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT     ReJobNumber, LgUser, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, ReWarranty, PerchesDate, FaultStatus, FaultOther, ReProcrssor, ReMemory, 
                      ReChassis, ReGraphics, ReMouse, RePowerCbl, RePowerBx, ReMBoard, ReHDD, ReRom, ReExtCard, ReKeyB, ReUSB, ReSystemOther, ReCompletingDate, 
                      ReCompletingTime, TroblShootInfor, ReEngNote, ReCusNote, TimeStamp
                      FROM RepairNotes WHERE ReJobNumber='" + passedValv + "'";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);



                //view the christtal report
                RptRepairNote rpt = new RptRepairNote();
                rpt.SetDataSource(ds.Tables[8]);
                RepairItmViewer.ReportSource = rpt;
                RepairItmViewer.Refresh();
                con1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
