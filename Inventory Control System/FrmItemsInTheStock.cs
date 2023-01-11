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
    public partial class FrmItemsInTheStock : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public FrmItemsInTheStock()
        {
            InitializeComponent();
        }

        private void FrmItemsInTheStock_Load(object sender, EventArgs e)
        {

            //StockItem plc = new StockItem();
            //plc.DataDefinition.FormulaFields["ReportNameChange1"].Text = "Jude";

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

                string ReSelectQ = @" SELECT     NewItemDetails.ItmID, NewItemDetails.ItmName, CurrentStock.AvailableStockCount, NewItemDetails.ItmCategory, NewItemDetails.ItmStockType, 
                         NewItemDetails.ItmLocation, NewItemDetails.ItmVenID, NewItemDetails.ItmReoderLvl, NewItemDetails.ItmTarQuntity
                         FROM         CurrentStock INNER JOIN
                         NewItemDetails ON CurrentStock.ItemID = NewItemDetails.ItmID
                           ORDER BY NewItemDetails.ItmID";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                //view the christtal report
                StockItem rpt = new StockItem();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                ReportItems.ReportSource = rpt;
                ReportItems.Refresh();
                con1.Close();



                
            //TextObject txt;

            //if (rpt.ReportDefinition.ReportObjects["Text10"] != null)
            //{
            //    txt = (TextObject)rpt.ReportDefinition.ReportObjects["Text10"];
            //    txt.Text = "Jude";
            //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }
    }
}
