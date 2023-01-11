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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

namespace Inventory_Control_System
{
    public partial class rptItemWiseSaleReport : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        string ReSelectQ = "";

        

        public rptItemWiseSaleReport()
        {
            InitializeComponent();
            RBtAllDate.Checked = true;

           
            
            
        }

        public void conSql()
        {
            try
            {
                ReSelectQ = @"SELECT  InvoicePaymentDetails.InvoiceDate, SoldItemDetails.InvoiceID, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                         SoldItemDetails.BuyPrice, SoldItemDetails.SoldPrice, NewItemDetails.ItmCategory
                         FROM            InvoicePaymentDetails INNER JOIN
                         SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID INNER JOIN
                         NewItemDetails ON SoldItemDetails.ItemID = NewItemDetails.ItmID where 1=1";


                ReSelectQ.Replace("1=1 AND ", "");
                ReSelectQ.Replace(" WHERE 1=1 ", "");

                if (rbtFromDate.Checked == true)
                {
                    string xyz = Convert.ToDateTime(dateTimePicker2.Text.Trim()).AddDays(1).ToShortDateString();

                    ReSelectQ += " AND InvoiceDate BETWEEN '" + dateTimePicker1.Text + "' AND '" + xyz + "' ";
                }

                if (rbtItemWise.Checked == true)
                {
                    ReSelectQ += " AND SoldItemDetails.ItemID='" + IblItemID.Text + "' ";
                }

                if (rbtByCategory.Checked == true)
                {
                    ReSelectQ += " AND NewItemDetails.ItmCategory='" + cmbCategory.Text + "' ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
           // MessageBox.Show(ReSelectQ);
           // return; 
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

        public void LoadCategory()
        {
            try
            {
                #region...............................................

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


        private void rptItemWiseSaleReport_Load(object sender, EventArgs e)
        {
        }

        private void RBtAllItem_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Refresh();
            dateTimePicker1.Refresh();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
                //===================================================================

                RPTItemWiseInvoiceReport rpt = new RPTItemWiseInvoiceReport();

                TextObject FristDate, secondDate;

                //  checked dateFrom--------------------------------------------------------------------------

                if (rbtFromDate.Checked == true)
                {
                    // frist date----------------------------------------------------------------------
                    if (rpt.ReportDefinition.ReportObjects["Text13"] != null)
                    {
                        FristDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text13"];
                        FristDate.Text = dateTimePicker1.Text;

                    }

                    //Second date----------------------------------------------------------------------
                    if (rpt.ReportDefinition.ReportObjects["Text14"] != null)
                    {
                        secondDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text14"];
                        secondDate.Text = dateTimePicker2.Text;

                    }
                }

                //Checked all date----------------------------------------------------------------------
                if (rbtFromDate.Checked == false)
                {
                    // frist date----------------------------------------------------------------------
                    if (rpt.ReportDefinition.ReportObjects["Text13"] != null)
                    {
                        FristDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text13"];
                        FristDate.Text = dateTimePicker1.Text;

                    }

                    //Second date----------------------------------------------------------------------
                    if (rpt.ReportDefinition.ReportObjects["Text14"] != null)
                    {
                        secondDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text14"];
                        secondDate.Text = dateTimePicker2.Text;

                    }
                }



                SqlConnection cnn1 = new SqlConnection(IMS);

                cnn1.Open();
                conSql();


                //            String ItemWise = @"SELECT        InvoicePaymentDetails.InvoiceDate, SoldItemDetails.InvoiceID, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                //                         SoldItemDetails.BuyPrice, SoldItemDetails.SoldPrice, NewItemDetails.ItmCategory
                //FROM            InvoicePaymentDetails INNER JOIN
                //                         SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID INNER JOIN
                //                         NewItemDetails ON SoldItemDetails.ItemID = NewItemDetails.ItmID";
                SqlDataAdapter drc = new SqlDataAdapter(ReSelectQ, cnn1);
                IMSDataSET sd = new IMSDataSET();
                drc.Fill(sd);






                //view the christtal report

                rpt.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
                ViwerItemWiseSaleReport.ReportSource = rpt;
                ViwerItemWiseSaleReport.Refresh();
                cnn1.Close();

                //pass value to the report (dates From TO)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void rbtItemWise_CheckedChanged(object sender, EventArgs e)
        {
            cmbItemWise.Enabled = true;
            loadItem();
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void IblItemID_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbtByCategory_CheckedChanged(object sender, EventArgs e)
        {
            cmbCategory.Enabled = true;
            LoadCategory();
        }

        private void RbtAlllCategory_CheckedChanged(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = -1;
            cmbCategory.Enabled = false;
        }

        private void rbtAllItems_CheckedChanged(object sender, EventArgs e)
        {
            cmbItemWise.SelectedIndex = -1;
            cmbItemWise.Enabled = false;
            IblItemID.Text = "--";
        }

        private void rbtFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtFromDate.Checked==true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
