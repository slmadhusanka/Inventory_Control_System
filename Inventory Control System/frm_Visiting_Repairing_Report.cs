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




//using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using System.Data.Sql;
//using System.Configuration;
using System.Drawing.Printing;








namespace Inventory_Control_System
{
    public partial class frm_Visiting_Repairing_Report : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string VisitIDPassToReport = "";
        public Boolean NewInsert = false;

        public frm_Visiting_Repairing_Report()
        {
            InitializeComponent();
        }

        String totalTables;
        private void frm_Visiting_Repairing_Report_Load(object sender, EventArgs e)
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

            if (NewInsert == true)
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String LoadVisitDetails = @"SELECT Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, ConformDate, ConformTime, Conform_Discription, Solution, visited_Date, Visited_time, 
                         VisitedPerson, [Visited _Discription], Date, addUser, LastUpdateUser, LastUpdate_date FROM Repair_VisitNote where Visit_ID='" + VisitIDPassToReport + "'";
                SqlDataAdapter dscmd = new SqlDataAdapter(LoadVisitDetails, cnn);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                rptVisitingRepair_Insert_Report rpt = new rptVisitingRepair_Insert_Report();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                viwer_Visiting_Repairing_Report.ReportSource = rpt;
                viwer_Visiting_Repairing_Report.Refresh();
                cnn.Close();

                
            }



            if (NewInsert == false)
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String LoadVisitDetails = @"SELECT Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, ConformDate, ConformTime, Conform_Discription, Solution, visited_Date, Visited_time, 
                         VisitedPerson, [Visited _Discription], Date, addUser, LastUpdateUser, LastUpdate_date FROM Repair_VisitNote where Visit_ID='" + VisitIDPassToReport + "'";
                SqlDataAdapter dscmd = new SqlDataAdapter(LoadVisitDetails, cnn);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                rptVisitingRepair_Update_Report rpt = new rptVisitingRepair_Update_Report();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                viwer_Visiting_Repairing_Report.ReportSource = rpt;
                viwer_Visiting_Repairing_Report.Refresh();
                cnn.Close();
            }

            }


        }
    }

