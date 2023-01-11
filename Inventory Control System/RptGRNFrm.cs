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
    public partial class RptGRNFrm : Form
    {
        public RptGRNFrm()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string PrintingGRNNumber = "";

        public string PrintCopyDetails = "";

        public string GRN_Type = "";

        public void PrintJob(String CopyType)
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





                if (GRN_Type == "Serial")
                {

                    #region Report items with Serial numbers------------------------------------

                    RptGRNSerial rpt1 = new RptGRNSerial();

                    TextObject CopyDetails;


                    if (rpt1.ReportDefinition.ReportObjects["CopyDetails"] != null)
                    {
                        CopyDetails = (TextObject)rpt1.ReportDefinition.ReportObjects["CopyDetails"];
                        CopyDetails.Text = CopyType;

                    }

                    ReSelectQ = @" SELECT     CurrentStockItems.OrderID, CurrentStockItems.ItemID, CurrentStockItems.BarcodeNumber, CurrentStockItems.SystemID, CurrentStockItems.ItemName, 
                              CurrentStockItems.WarrentyPeriod, CurrentStockItems.OrderCost, CurrentStockItems.UserName, CurrentStockItems.ItmSellPrice, GRN_amount_Details.Date, 
                              GRN_amount_Details.GrossAmount, GRN_amount_Details.Discount, GRN_amount_Details.NBT, GRN_amount_Details.VAT, GRN_amount_Details.Net_Amount, 
                              GRN_amount_Details.Credit_Period, GRN_amount_Details.Bill_No, GRN_amount_Details.Bill_Date, GRN_amount_Details.Comment, VenderDetails.VenderID, 
                              VenderDetails.VenderName, VenderDetails.VenderPHAddress, VenderDetails.VenderPTel
                                FROM         VenderDetails INNER JOIN
                              GRN_amount_Details ON VenderDetails.VenderID = GRN_amount_Details.Vender_ID INNER JOIN
                              CurrentStockItems ON GRN_amount_Details.GRN_No = CurrentStockItems.OrderID
                            
                 WHERE     (CurrentStockItems.OrderID = '" + PrintingGRNNumber + "')";

                    SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                    IMSDataSET ds1 = new IMSDataSET();
                    dscmd.Fill(ds1);

                    //ODS1042   WHERE     (CurrentStockItems.OrderID = '" + PrintingGRNNumber + "')";
                    //view the christtal report


                    // RptRepairJobAdding rpt1 = new RptRepairJobAdding();
                    rpt1.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                    RptGRNSerialViewer.ReportSource = rpt1;
                    RptGRNSerialViewer.Refresh();

                    #endregion
                }

                if (GRN_Type == "Whole_Sale")
                {

                    #region Report with Whole sale Items ---------------------------

                    RptGRNWholeSale rpt1 = new RptGRNWholeSale();

                    TextObject CopyDetails;


                    if (rpt1.ReportDefinition.ReportObjects["CopyDetails"] != null)
                    {
                        CopyDetails = (TextObject)rpt1.ReportDefinition.ReportObjects["CopyDetails"];
                        CopyDetails.Text = CopyType;

                    }

                    ReSelectQ = @" SELECT     GRNWholesaleItems.GRNNumber, GRNWholesaleItems.ItemID, GRNWholesaleItems.BarCodeID, GRNWholesaleItems.BatchNumber, 
                      GRNWholesaleItems.ItemWarrenty, GRNWholesaleItems.ItemAdded, GRNWholesaleItems.PerchPrice, GRNWholesaleItems.DiscountAmount, 
                      GRNWholesaleItems.NetAmount, GRN_amount_Details.Date, GRN_amount_Details.GrossAmount, GRN_amount_Details.Discount, GRN_amount_Details.NBT, 
                      GRN_amount_Details.VAT, GRN_amount_Details.Net_Amount, GRN_amount_Details.Credit_Period, GRN_amount_Details.Bill_No, GRN_amount_Details.Bill_Date, 
                      GRN_amount_Details.Comment, GRN_amount_Details.GRNUser, VenderDetails.VenderID, VenderDetails.VenderName, VenderDetails.VenderPHAddress, 
                      VenderDetails.VenderPTel, NewItemDetails.ItmName
                      FROM         GRN_amount_Details INNER JOIN
                      VenderDetails ON GRN_amount_Details.Vender_ID = VenderDetails.VenderID INNER JOIN
                      GRNWholesaleItems ON GRN_amount_Details.GRN_No = GRNWholesaleItems.GRNNumber INNER JOIN
                      NewItemDetails ON GRNWholesaleItems.ItemID = NewItemDetails.ItmID
                    WHERE     (GRNWholesaleItems.GRNNumber = '" + PrintingGRNNumber + "') ORDER BY GRNWholesaleItems.ItemID, GRNWholesaleItems.BatchNumber";

                    SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                    IMSDataSET ds1 = new IMSDataSET();
                    dscmd.Fill(ds1);

                    // view the christtal report


                    //RptRepairJobAdding rpt1 = new RptRepairJobAdding();
                    rpt1.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                    RptGRNSerialViewer.ReportSource = rpt1;
                    RptGRNSerialViewer.Refresh();

                    #endregion

                }

                con1.Close();


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

       

        private void RptGRNFrm_Load(object sender, EventArgs e)
        {
            

            PrintJob(PrintCopyDetails);
        }
    }
}
