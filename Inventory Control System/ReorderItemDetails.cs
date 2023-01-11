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
    public partial class ReorderItemDetails : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public ReorderItemDetails()
        {
            InitializeComponent();
        }

        private void ReorderItemDetails_Load(object sender, EventArgs e)
        {
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


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT NewItemDetails.ItmID, NewItemDetails.ItmName, NewItemDetails.ItmCategory, NewItemDetails.ItmModel, NewItemDetails.ItmVenID, NewItemDetails.ItmVendor, 
                                    NewItemDetails.ItmOderCost, CurrentStock.AvailableStockCount, NewItemDetails.ItmReoderLvl, NewItemDetails.ItmTarQuntity
                                    FROM CurrentStock INNER JOIN
                                    NewItemDetails ON CurrentStock.ItemID = NewItemDetails.ItmID AND CurrentStock.AvailableStockCount <= NewItemDetails.ItmReoderLvl ORDER BY NewItemDetails.ItmID ASC";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                //view the christtal report
                ReOrderItems rpt = new ReOrderItems();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                crystalReportReOrder.ReportSource = rpt;
                crystalReportReOrder.Refresh();
                con1.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void ReOrderItems1_InitReport(object sender, EventArgs e)
        {

        }
    }
}
