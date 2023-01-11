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
using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;

namespace Inventory_Control_System
{
    public partial class samReport : Form
    {
        public samReport()
        {
            InitializeComponent();
            //ReturnVenderID();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string PrintingRTNNumber = "";

        public string PrintCopyDetails = "";

        public string GRN_Type = "";

        public void PrintJob()
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

            rpt_GRN_ReturnTest rpt1 = new rpt_GRN_ReturnTest();

                //TextObject CopyDetails;


                //if (rpt1.ReportDefinition.ReportObjects["CopyDetails"] != null)
                //{
            //    CopyDetails = (TextObject)rpt1.ReportDefinition.ReportObjects["PrintCopyDetails"];
                //    CopyDetails.Text = CopyType;

                //}

                ReSelectQ = @"SELECT        VenderDetails.VenderName, VenderDetails.VenderPTel, VenderDetails.VenderPHAddress, GRN_Return_Note_1.Vender_ID, GRN_Return_Note_1.Return_Num,
                          GRN_Return_Note_1.GRN_Num, GRN_Return_Note_1.Batch_Num, GRN_Return_Note_1.Itm_ID, GRN_Return_Note_1.Itm_Name, 
                         GRN_Return_Note_1.Serial_Num, GRN_Return_Note_1.Selling_Price, GRN_Return_Note_1.Return_Qnty, GRN_Return_Note_1.Tot_Amount, 
                         GRN_Return_Note_1.Net_Amount, GRN_Return_Note_1.Return_Statement, GRN_Return_Note_1.Return_Employee, GRN_Return_Note_1.Return_Date, 
                         GRN_Return_Note_1.Remark, GRN_Return_Note_1.NBT, GRN_Return_Note_1.VAT
FROM            GRN_Return_Note AS GRN_Return_Note_1 INNER JOIN
                         VenderDetails ON GRN_Return_Note_1.Vender_ID = VenderDetails.VenderID WHERE GRN_Return_Note_1.Return_Num='" + PrintingRTNNumber + "' ";

                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds1 = new IMSDataSET();
                dscmd.Fill(ds1);

                //ODS1042   WHERE     (CurrentStockItems.OrderID = '" + PrintingGRNNumber + "')";
                //view the christtal report


                // RptRepairJobAdding rpt1 = new RptRepairJobAdding();
                rpt1.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                crystalReportViewer1.ReportSource = rpt1;
                crystalReportViewer1.Refresh();

                #endregion

            #endregion

        }

        //public void ReturnVenderID()
        //{
        //    SqlConnection cnn1 = new SqlConnection(IMS);
        //    cnn1.Open();
        //    String VenderID = "select distinct Vender_ID from GRN_Return_Note";
        //    SqlCommand cmm1 = new SqlCommand(VenderID, cnn1);
        //    SqlDataReader dr = cmm1.ExecuteReader();
        //    while(dr.Read())
        //    {
        //        comboBox1.Items.Add(dr[0].ToString());
        //    }
        //}
        private void samReport_Load(object sender, EventArgs e)
        {
            PrintJob();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PrintJob();
        }
    }
}
