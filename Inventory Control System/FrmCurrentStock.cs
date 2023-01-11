using CrystalDecisions.CrystalReports.Engine;
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
    public partial class FrmCurrentStock : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
       
        String sqlItems="";
        String wholesale = "";
        String WholesaleAvaila = "";

        public string UserID = "";
        public string UserDisplayName = "";

        public FrmCurrentStock()
        {
            InitializeComponent();
            rbtAllCategory.Checked = true;
            rbtAllItems.Checked = true;
            rbtWholeSaleCurrentStock.Checked = true;
        }

        public void sqlQury()
        {
            try
            {
                #region Items..........................................

                sqlItems = @"SELECT        CurrentStockItems.ItemID, CurrentStockItems.BarcodeNumber, CurrentStockItems.SystemID, CurrentStockItems.ItemName, CurrentStockItems.OrderCost, CurrentStockItems.ItmSellPrice, CurrentStockItems.ItmStatus
                        FROM            CurrentStockItems inner join NewItemDetails on CurrentStockItems.ItemID=NewItemDetails.ItmID where CurrentStockItems.ItmStatus!='sold' and CurrentStockItems.ItmStatus!='Return'";

                if (rbtbyItems.Checked == true)
                {
                    sqlItems += "and CurrentStockItems.ItemID='" + IblItemID.Text + "' ";
                }

                if (rbtCatogery.Checked == true)
                {
                    sqlItems += "and NewItemDetails.ItmCategory='" + cmbCategory.Text + "' ";
                }

                sqlItems.Replace("1=1 AND ", "");
                sqlItems.Replace(" WHERE 1=1 ", "");

                // MessageBox.Show(sqlItems);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void SqlQuaryWholSale()
        {
            try
            {
                #region wholesale......................................................

                wholesale = @"SELECT         CurrentStock.ItemID, NewItemDetails.ItmName,CurrentStock.AvailableStockCount, NewItemDetails.ItmSellPrice, NewItemDetails.ItmOderCost
                          FROM            CurrentStock INNER JOIN
                         NewItemDetails ON CurrentStock.ItemID = NewItemDetails.ItmID where 1=1";

                if (rbtbyItems.Checked == true)
                {
                    wholesale += "and CurrentStock.ItemID='" + IblItemID.Text + "' ";
                }


                if (rbtCatogery.Checked == true)
                {
                    wholesale += "and NewItemDetails.ItmCategory='" + cmbCategory.Text + "' ";
                }

                sqlItems.Replace("1=1 AND ", "");
                sqlItems.Replace(" WHERE 1=1 ", "");

                // MessageBox.Show(wholesale);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void SqlQuery()
        {
            try
            {
                #region Available wholesale................................................................

                WholesaleAvaila = @"SELECT         GRNWholesaleItems.ItemID, NewItemDetails.ItmName, NewItemDetails.ItmCategory,GRNWholesaleItems.BarCodeID, GRNWholesaleItems.BatchNumber, GRNWholesaleItems.AvailbleItemCount, GRNWholesaleItems.PerchPrice, 
                                GRNWholesaleItems.SellingPrice
                                FROM            GRNWholesaleItems INNER JOIN
                                NewItemDetails ON GRNWholesaleItems.ItemID = NewItemDetails.ItmID where GRNWholesaleItems.AvailbleItemCount!=0 ";

                if (rbtbyItems.Checked == true)
                {
                    WholesaleAvaila += "And GRNWholesaleItems.ItemID='" + IblItemID.Text + "' ";
                }

                if (rbtCatogery.Checked == true)
                {
                    WholesaleAvaila += "And NewItemDetails.ItmCategory='" + cmbCategory.Text + "' ";
                }




                WholesaleAvaila.Replace("1=1 AND ", "");
                WholesaleAvaila.Replace(" WHERE 1=1 ", "");

                // MessageBox.Show(WholesaleAvaila);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void LoadCategory()
        {
            try
            {
                #region LoadCategory...............................................

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String loadID = "select CatName from ItemCategory";
                SqlCommand cmm = new SqlCommand(loadID, cnn1);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
                cmbCategory.Items.Clear();
                while (dr.Read())
                {
                    cmbCategory.Items.Add(dr[0].ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void loadItem()
        {
            try
            {
                #region Load item......................................

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String loadID = "select ItmID,ItmName from NewItemDetails";
                SqlCommand cmm = new SqlCommand(loadID, cnn1);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
                cmbItemWise.Items.Clear();
                while (dr.Read())
                {
                    cmbItemWise.Items.Add(dr[1].ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }




        private void simpleButton1_Click(object sender, EventArgs e)
        {
            #region View report Code........................................................

            #region report number..................................................
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
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
            #endregion
            //===================================================================


            if(rbtCatogery.Checked==true && cmbCategory.Text=="")
            {
                MessageBox.Show("Please Select Category","Message");
                cmbCategory.Focus();
                return;
            }
            if (rbtbyItems.Checked == true && cmbItemWise.Text == "")
            {
                MessageBox.Show("Please Select Item", "Message");
                cmbItemWise.Focus();
                return;
            }

           


            if (rbtWholeSaleCurrentStock.Checked == true)
            {
                try
                {
                    #region WholeSale Stock...........................................

                    RPTCurrentWholesaleStock rpt1 = new RPTCurrentWholesaleStock();

                    TextObject Allcategory, bycategory;

                    if (rbtAllCategory.Checked == true)
                    {
                        if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
                        {
                            Allcategory = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
                            Allcategory.Text = "All";
                        }
                    }

                    if (rbtAllCategory.Checked == true)
                    {
                        if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
                        {
                            Allcategory = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
                            Allcategory.Text = "All";

                        }
                    }

                    if (rbtCatogery.Checked == true)
                    {
                        if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
                        {
                            bycategory = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
                            bycategory.Text = cmbCategory.Text;

                        }
                    }


                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();

                    SqlQuaryWholSale();


                    SqlDataAdapter drc = new SqlDataAdapter(wholesale, cnn1);
                    IMSDataSET sd = new IMSDataSET();
                    drc.Fill(sd);



                    rpt1.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
                    viwerWholsaleStockReport.ReportSource = rpt1;
                    viwerWholsaleStockReport.Refresh();
                    cnn1.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }




            if (rbtItemsCurStock.Checked == true)
            {
                try
                {

                    #region Item Wise Stock.............................................................

                    rptItemsCurrentStock rpt = new rptItemsCurrentStock();

                    TextObject Allcategory, bycategory;

                    if (rbtAllCategory.Checked == true)
                    {
                        if (rpt.ReportDefinition.ReportObjects["Text10"] != null)
                        {
                            Allcategory = (TextObject)rpt.ReportDefinition.ReportObjects["Text10"];
                            Allcategory.Text = "All";

                        }
                    }



                    if (rbtCatogery.Checked == true)
                    {
                        if (rpt.ReportDefinition.ReportObjects["Text10"] != null)
                        {
                            bycategory = (TextObject)rpt.ReportDefinition.ReportObjects["Text10"];
                            bycategory.Text = cmbCategory.Text;

                        }
                    }

                    SqlConnection cnn2 = new SqlConnection(IMS);
                    cnn2.Open();
                    sqlQury();
                    // String Items = "SELECT  ItemID, BarcodeNumber, SystemID, ItemName, OrderCost, ItmSellPrice, ItmStatus FROM CurrentStockItems  where ItmStatus='stock' ";
                    SqlDataAdapter drc1 = new SqlDataAdapter(sqlItems, cnn2);
                    IMSDataSET sd1 = new IMSDataSET();
                    drc1.Fill(sd1);


                    rpt.SetDataSource(sd1.Tables[Convert.ToInt32(totalTables)]);
                    viwerWholsaleStockReport.ReportSource = rpt;
                    viwerWholsaleStockReport.Refresh();
                    cnn2.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            
            if(radioButton1.Checked==true)
            {
                try
                {
                    #region Avilable Current Stock.....................................................

                    rptWholesaleAvailableStock rpt = new rptWholesaleAvailableStock();

                    TextObject allItems, ByItems;

                    if (rbtAllItems.Checked == true)
                    {
                        if (rpt.ReportDefinition.ReportObjects["txtItems"] != null)
                        {
                            allItems = (TextObject)rpt.ReportDefinition.ReportObjects["txtItems"];
                            allItems.Text = "All Items";

                        }
                    }
                    if (rbtbyItems.Checked == true)
                    {
                        if (rpt.ReportDefinition.ReportObjects["txtItems"] != null)
                        {
                            ByItems = (TextObject)rpt.ReportDefinition.ReportObjects["txtItems"];
                            ByItems.Text = cmbItemWise.Text;

                        }
                    }

                    //Category----------------------------------------------------------------------------------
                    //try
                    //{

                        if (rbtAllCategory.Checked == true)
                        {
                            if (rpt.ReportDefinition.ReportObjects["txtCategory"] != null)
                            {
                                allItems = (TextObject)rpt.ReportDefinition.ReportObjects["txtCategory"];
                                allItems.Text = "All Category";

                            }
                        }
                        if (rbtCatogery.Checked == true)
                        {
                            if (rpt.ReportDefinition.ReportObjects["txtCategory"] != null)
                            {
                                ByItems = (TextObject)rpt.ReportDefinition.ReportObjects["txtCategory"];
                                ByItems.Text = cmbCategory.Text;

                            }
                        }

                        SqlConnection cnn2 = new SqlConnection(IMS);
                        SqlQuery();
                        // String Items = "SELECT  ItemID, BarcodeNumber, SystemID, ItemName, OrderCost, ItmSellPrice, ItmStatus FROM CurrentStockItems  where ItmStatus='stock' ";
                        SqlDataAdapter drc1 = new SqlDataAdapter(WholesaleAvaila, cnn2);
                        IMSDataSET sd1 = new IMSDataSET();
                        drc1.Fill(sd1);


                        rpt.SetDataSource(sd1.Tables[Convert.ToInt32(totalTables)]);
                        viwerWholsaleStockReport.ReportSource = rpt;
                        viwerWholsaleStockReport.Refresh();
                        cnn2.Close();


                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    //}
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            #endregion
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void rbtbyItems_CheckedChanged(object sender, EventArgs e)
        {
            loadItem();
            if(rbtbyItems.Checked==true)
            {
                cmbItemWise.Enabled = true;
                rbtAllCategory.Checked = true;
            }

           
        }

        private void rbtCatogery_CheckedChanged(object sender, EventArgs e)
        {
            LoadCategory();
            if(rbtCatogery.Checked==true)
            {
                cmbCategory.Enabled = true;
                rbtAllItems.Checked = true;
            }
        }

        private void cmbItemWise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String loadID = "select ItmID,ItmName from NewItemDetails where ItmName='" + cmbItemWise.Text + "'";
                SqlCommand cmm = new SqlCommand(loadID, cnn1);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    IblItemID.Text = (dr[0].ToString());
                }

                if (cmbItemWise.Text == "")
                {
                    IblItemID.Text = "--";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rbtAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAllCategory.Checked == true)
            {
                cmbCategory.SelectedIndex = -1;
                cmbCategory.Enabled = false;
                
            }

        }

        private void rbtAllItems_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtbyItems.Checked == false)
            {
                cmbItemWise.SelectedIndex = -1;
                cmbItemWise.Enabled = false;
                IblItemID.Text = "--";
                
            }
        }

        private void FrmCurrentStock_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
