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
    public partial class frmCheckVenderCredit : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";


        public frmCheckVenderCredit()
        {
            InitializeComponent();
        }


        public void LoadVender()
        {
            try
            {
                #region Load Vender............................................

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Vender = "select VenderID,VenderName from VenderDetails";
                SqlCommand cmm = new SqlCommand(Vender, cnn1);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
                comboBox1.Items.Clear();

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
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

            if (rbtAllVender.Checked == true)
            {
                try
                {

                    #region All vendr..................................................

                    rptCheckVenderCredit rpt1 = new rptCheckVenderCredit();

                    TextObject AllSuppl;

                    if (rpt1.ReportDefinition.ReportObjects["Text2"] != null)
                    {
                        AllSuppl = (TextObject)rpt1.ReportDefinition.ReportObjects["Text2"];
                        AllSuppl.Text = lblSupName.Text;

                    }

                    SqlConnection cnn2 = new SqlConnection(IMS);
                    cnn2.Open();
                    String checkVen = "select  GRNInvoicePaymentDetails.GRNID,GRN_amount_Details.[Date],GRNInvoicePaymentDetails.Net_Amount,GRNInvoicePaymentDetails.PayBalance,GRN_amount_Details.Vender_ID from GRN_amount_Details inner join GRNInvoicePaymentDetails on GRN_amount_Details.GRN_No=GRNInvoicePaymentDetails.GRNID where (GRNInvoicePaymentDetails.PayBalance>0)";
                    SqlDataAdapter drs = new SqlDataAdapter(checkVen, cnn2);
                    IMSDataSET ds = new IMSDataSET();
                    drs.Fill(ds);

                    // rptCheckVenderCredit rpt = new rptCheckVenderCredit();
                    rpt1.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                    viwervenderCheckCredit.ReportSource = rpt1;
                    viwervenderCheckCredit.Refresh();
                    cnn2.Close();

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }

            if (rbtBySupplier.Checked == true)
            {
                try
                {
                    #region ByVender................................................................

                    rptCheckVenderCredit rpt = new rptCheckVenderCredit();

                    TextObject bySuppl;

                    if (rpt.ReportDefinition.ReportObjects["Text2"] != null)
                    {
                        bySuppl = (TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                        bySuppl.Text = lblSupName.Text;

                    }

                    SqlConnection cnn2 = new SqlConnection(IMS);
                    cnn2.Open();
                    String checkVen = @"SELECT     GRNInvoicePaymentDetails.GRNID, GRN_amount_Details.Date, GRNInvoicePaymentDetails.Net_Amount, GRNInvoicePaymentDetails.PayBalance, GRN_amount_Details.Vender_ID
                                    FROM         GRN_amount_Details INNER JOIN
                                    GRNInvoicePaymentDetails ON GRN_amount_Details.GRN_No = GRNInvoicePaymentDetails.GRNID where (GRNInvoicePaymentDetails.PayBalance>0) AND (GRN_amount_Details.Vender_ID='" + comboBox1.Text + "')";
                    SqlDataAdapter drs = new SqlDataAdapter(checkVen, cnn2);
                    IMSDataSET ds = new IMSDataSET();
                    drs.Fill(ds);



                    rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                    viwervenderCheckCredit.ReportSource = rpt;
                    viwervenderCheckCredit.Refresh();
                    cnn2.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }

        }

        private void rbtBySupplier_CheckedChanged(object sender, EventArgs e)
        {
            LoadVender();
            if(rbtBySupplier.Checked==true)
            {
                comboBox1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void rbtAllVender_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtAllVender.Checked==true)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = -1;
                rbtBySupplier.Checked = false;
                lblSupName.Text = "All Suppliers";
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                // comboBox1.Items.Clear();
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Vender = "select VenderID,VenderName from VenderDetails where VenderID='" + comboBox1.Text + "'";
                SqlCommand cmm = new SqlCommand(Vender, cnn1);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    lblSupName.Text = dr[1].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void frmCheckVenderCredit_Load(object sender, EventArgs e)
        {

        }
    }
}
