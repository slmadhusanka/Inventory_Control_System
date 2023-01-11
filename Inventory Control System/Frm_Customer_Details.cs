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
    public partial class Frm_Customer_Details : Form
    {
        public Frm_Customer_Details()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        public void Customer_Load()
        {
            #region Load Customer....
            try
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

                //---------------------------------------------------------------------------------------

                // string time = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                Rpt_Customer_Details rpt = new Rpt_Customer_Details();

                #region pass value to the report (dates From TO)---------------------------------------------------------

                //TextObject From;
                //TextObject To;
                TextObject LG_user;

                //if (rpt.ReportDefinition.ReportObjects["Text14"] != null)
                //{
                //    From = (TextObject)rpt.ReportDefinition.ReportObjects["Text14"];
                //    From.Text = PickerDateFrom.Text;

                //    From.Text = "Jude123";

                //}

                //if (rpt.ReportDefinition.ReportObjects["Text15"] != null)
                //{
                //    To = (TextObject)rpt.ReportDefinition.ReportObjects["Text15"];
                //    To.Text = PickerDateTo.Text;
                //}

                if (rpt.ReportDefinition.ReportObjects["Text16"] != null)
                {
                    LG_user = (TextObject)rpt.ReportDefinition.ReportObjects["Text16"];
                    LG_user.Text = LgUser.Text;
                }

                #endregion



                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT DISTINCT 
                                CustomerDetails.CusID, CustomerDetails.CusFirstName, CustomerDetails.CusLastName, CustomerDetails.CusPersonalAddress, CustomerDetails.CusMobileNumber, 
                                CustomerDetails.CusTelNUmber, CustomerDetails.CusEmailAddress, CustomerDetails.CusCreditLimit, CustomerDetails.CusRemarks,
                                (SELECT     TOP (1) Balance
                                FROM          RegCusCredBalance
                                WHERE      (CusID = CustomerDetails.CusID)
                                ORDER BY AutoNum DESC) AS Balance   
                                FROM         CustomerDetails INNER JOIN
                                RegCusCredBalance AS RegCusCredBalance_1 ON CustomerDetails.CusID = RegCusCredBalance_1.CusID
                                WHERE     (CustomerDetails.CusActiveDeactive = '1 ')
                                ORDER BY CustomerDetails.CusID";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);



                //view the christtal report

                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                CrystalReVie_Profit.ReportSource = rpt;
                CrystalReVie_Profit.Refresh();
                con1.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Frm_Customer_Details_Load(object sender, EventArgs e)
        {
            Customer_Load();
        }
    }
}
