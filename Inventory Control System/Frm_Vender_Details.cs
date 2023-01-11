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
    public partial class Frm_Vender_Details : Form
    {
        public Frm_Vender_Details()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

       
        public void Vender_Details()
        {
            #region Load Vender....
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

                Rpt_Vender_Details rpt = new Rpt_Vender_Details();

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

                string ReSelectQ = @"SELECT VenderID, VenderName, VenderPHAddress, VenderPTel, VenderPFax, VenderPEmail, CreditValue FROM VenderDetails
                                    WHERE ActiveDeactive ='1' ORDER BY VenderID ASC";


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

        private void Frm_Vender_Details_Load(object sender, EventArgs e)
        {
            Vender_Details();
        }
    }
}
