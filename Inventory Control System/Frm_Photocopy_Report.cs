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
//using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;

namespace Inventory_Control_System
{
    public partial class Frm_Photocopy_Report : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        string ReSelectQ1 = "";
        string ReSelectQ = "";

        public Frm_Photocopy_Report()
        {
            InitializeComponent();
           // rbtinvoice.Checked = true;
         //   rbtAll.Checked = true;
        }

        public void LoadID()
        {
            try
            {
                #region Load Item Id................................

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String Load = "select CopyID,Copy_Name  from PhotoCopy_Details";
                SqlCommand cmm = new SqlCommand(Load, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    CmbItemID.Items.Add(dr[1].ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        public void copyID()
        {
            try
            {

                #region item wise sql---------------------------------------

                string xyz = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                ReSelectQ1 = @" SELECT        PhotoCopy_Items_Details.Ph_Invoice_ID, PhotoCopy_Items_Details.Copy_ID, PhotoCopy_Details.Copy_Name, PhotoCopy_Items_Details.Copies, 
                         PhotoCopy_Items_Details.Unit_Price, PhotoCopy_Items_Details.Gross_Amount, PhotoCopy_Items_Details.Discount, PhotoCopy_Items_Details.Itm_Net_Amount, 
                         Photocopy_DOC_Details_1.Gross_Amount AS Expr1, Photocopy_DOC_Details_1.Discount AS Expr2, Photocopy_DOC_Details_1.Net_Amount, 
                         Photocopy_DOC_Details_1.Add_User, Photocopy_DOC_Details_1.Timp_Stamp
                            FROM            PhotoCopy_Items_Details INNER JOIN
                         PhotoCopy_Details ON PhotoCopy_Items_Details.Copy_ID = PhotoCopy_Details.CopyID INNER JOIN
                         Photocopy_DOC_Details AS Photocopy_DOC_Details_1 ON PhotoCopy_Items_Details.Ph_Invoice_ID = Photocopy_DOC_Details_1.PhotoC_Invoice_ID WHERE   Photocopy_DOC_Details_1.Timp_Stamp between  '" + PickerDateFrom.Text + "'  and '" + xyz + "'  And  1=1";


                if (rbtItemIDWise.Checked == true)
                {
                    ReSelectQ1 += " AND  PhotoCopy_Items_Details.Copy_ID='" + lblItemID.Text + "' ";
                }



                ReSelectQ1.Replace("1=1 AND ", "");
                ReSelectQ1.Replace(" WHERE 1=1 ", "");


                // MessageBox.Show(ReSelectQ1);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void LoadInvoiceID()
        {
            try
            {
                #region Invoice ID Load---------------------------------------------------

                SqlConnection cnn2 = new SqlConnection(IMS);
                cnn2.Open();
                String Load = "select distinct PhotoC_Invoice_ID  from Photocopy_DOC_Details";
                SqlCommand cmm = new SqlCommand(Load, cnn2);
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    cmbInvoiceID.Items.Add(dr[0].ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void InvoiceId()
        {
            try
            {
                #region Invoice Sql---------------------------------------------------------------------------

                string xyz = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                ReSelectQ = @" SELECT        PhotoCopy_Items_Details.Ph_Invoice_ID, PhotoCopy_Items_Details.Copy_ID, PhotoCopy_Details.Copy_Name, PhotoCopy_Items_Details.Copies, 
                         PhotoCopy_Items_Details.Unit_Price, PhotoCopy_Items_Details.Gross_Amount, PhotoCopy_Items_Details.Discount, PhotoCopy_Items_Details.Itm_Net_Amount, 
                         Photocopy_DOC_Details_1.Gross_Amount AS Expr1, Photocopy_DOC_Details_1.Discount AS Expr2, Photocopy_DOC_Details_1.Net_Amount, 
                         Photocopy_DOC_Details_1.Add_User, Photocopy_DOC_Details_1.Timp_Stamp
                            FROM            PhotoCopy_Items_Details INNER JOIN
                         PhotoCopy_Details ON PhotoCopy_Items_Details.Copy_ID = PhotoCopy_Details.CopyID INNER JOIN
                         Photocopy_DOC_Details AS Photocopy_DOC_Details_1 ON PhotoCopy_Items_Details.Ph_Invoice_ID = Photocopy_DOC_Details_1.PhotoC_Invoice_ID WHERE 1=1";

                if (rbtByinvoice.Checked == true)
                {
                    ReSelectQ += " AND PhotoCopy_Items_Details.Ph_Invoice_ID='" + cmbInvoiceID.Text + "' ";
                }

                ReSelectQ += " and Photocopy_DOC_Details_1.Timp_Stamp BETWEEN '" + PickerDateFrom.Text + "' AND '" + xyz + "'";

                ReSelectQ.Replace("1=1 AND ", "");
                ReSelectQ.Replace(" WHERE 1=1 ", "");

                // MessageBox.Show(ReSelectQ);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region data pass to Report................................................................

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


                    if (radioButton1.Checked == true)
                    {

                        #region Invoice wise................................................................................

                        Rpt_Photocopy_report rpt = new Rpt_Photocopy_report();

                        TextObject FristDate, secondDate;

                        if (rpt.ReportDefinition.ReportObjects["txtFrom"] != null)
                        {
                            FristDate = (TextObject)rpt.ReportDefinition.ReportObjects["txtFrom"];
                            FristDate.Text = PickerDateFrom.Text;

                        }

                        //Second date----------------------------------------------------------------------
                        if (rpt.ReportDefinition.ReportObjects["txtTo"] != null)
                        {
                            secondDate = (TextObject)rpt.ReportDefinition.ReportObjects["txtTo"];
                            secondDate.Text = PickerDateTo.Text;

                        }

                        SqlConnection con1 = new SqlConnection(IMS);
                        con1.Open();
                        InvoiceId();
                        SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                        IMSDataSET ds = new IMSDataSET();
                        dscmd.Fill(ds);

                        //view the christtal report
                        // Rpt_Photocopy_report rpt = new Rpt_Photocopy_report();
                        rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                        CrystalReVie_Stationary.ReportSource = rpt;
                        CrystalReVie_Stationary.Refresh();
                        con1.Close();

                        #endregion
                    }


                    if (rbtItemId.Checked == true)
                    {
                        #region Item Wise....................................................................................

                        rpt_Photocopy_report_CopyIDwise rpt1 = new rpt_Photocopy_report_CopyIDwise();

                        TextObject FristDate, secondDate;

                        if (rpt1.ReportDefinition.ReportObjects["txtFrom"] != null)
                        {
                            FristDate = (TextObject)rpt1.ReportDefinition.ReportObjects["txtFrom"];
                            FristDate.Text = PickerDateFrom.Text;

                        }

                        //Second date----------------------------------------------------------------------
                        if (rpt1.ReportDefinition.ReportObjects["txtTo"] != null)
                        {
                            secondDate = (TextObject)rpt1.ReportDefinition.ReportObjects["txtTo"];
                            secondDate.Text = PickerDateTo.Text;

                        }


                        SqlConnection con2 = new SqlConnection(IMS);
                        con2.Open();
                        copyID();
                        SqlDataAdapter dscmd1 = new SqlDataAdapter(ReSelectQ1, con2);
                        IMSDataSET ds1 = new IMSDataSET();
                        dscmd1.Fill(ds1);

                        //view the christtal report
                        //  rpt_Photocopy_report_CopyIDwise rpt1 = new rpt_Photocopy_report_CopyIDwise();
                        rpt1.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                        CrystalReVie_Stationary.ReportSource = rpt1;
                        CrystalReVie_Stationary.Refresh();
                        con2.Close();

                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Frm_Photocopy_Report_Load(object sender, EventArgs e)
        {

        }

        private void rbtItemIDWise_CheckedChanged(object sender, EventArgs e)
        {
            LoadID();
            if(rbtItemIDWise.Checked==true)
            {
                CmbItemID.Enabled=true;
                rbtByinvoice.Checked = false;
                rbtByinvoice.Enabled = false;
            }
            if (rbtItemIDWise.Checked == false)
            {
                CmbItemID.Enabled = false;
                CmbItemID.Enabled = false;
                CmbItemID.SelectedIndex = -1;
                lblItemID.Text = "";
            }

        }

        private void CmbItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String Load = "select CopyID,Copy_Name  from PhotoCopy_Details where Copy_Name='" + CmbItemID.Text + "' ";
                SqlCommand cmm = new SqlCommand(Load, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    lblItemID.Text = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbtAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAll.Checked == true)
            {
                CmbItemID.SelectedIndex = -1;
                rbtItemIDWise.Checked = false;
                CmbItemID.Enabled = false;
                //rbtByinvoice.Enabled = true;
            }
        }

        private void rbtByinvoice_CheckedChanged(object sender, EventArgs e)
        {
            LoadInvoiceID();

            if (rbtByinvoice.Checked == true)
            {
                cmbInvoiceID.Enabled = true;
                rbtItemIDWise.Checked = false;
                rbtItemIDWise.Enabled = false;
                
            }
        }

        private void rbtinvoice_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtinvoice.Checked == true)
            {
                cmbInvoiceID.Enabled = false;
                cmbInvoiceID.SelectedIndex = -1;
               

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) 
            {
                rbtinvoice.Enabled = true;
                rbtByinvoice.Enabled = true;
                rbtAll.Enabled = false;
                rbtItemIDWise.Enabled = false;
                rbtAll.Checked = false;
                rbtItemIDWise.Checked = false;
                rbtinvoice.Checked = true;
            }
        }

        private void rbtItemId_CheckedChanged(object sender, EventArgs e)
        {
             if (rbtItemId.Checked == true) 
            {
                rbtinvoice.Enabled = false;
                rbtByinvoice.Enabled = false;
                rbtinvoice.Checked = false;
                rbtByinvoice.Checked = false;
                rbtAll.Enabled = true;
                rbtItemIDWise.Enabled = true;
                rbtAll.Checked = true;

            }
        }
    }
}
