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
    public partial class Frm_Photocopy_Print_Invoice : Form
    {
        public Frm_Photocopy_Print_Invoice()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

      public string Status_Is = "";
      public string InvoiceCopyType = "";
      public string InvoiceID = "";

        private void Frm_Photocopy_Print_Invoice_Load(object sender, EventArgs e)
        {
            //select the print datatables number-----------------------------------------------------

           // LgDisplayName.Text=

          //  MessageBox.Show(InvoiceID);

            try
            {

                string totalTables = "";

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




           // TextObject _Status;
            TextObject _Invoice_IS;
           // TextObject LG_user;

            Rpt_Photocopy_Invoice rpt = new Rpt_Photocopy_Invoice();

           
            if (rpt.ReportDefinition.ReportObjects["Text27"] != null)
            {
                _Invoice_IS = (TextObject)rpt.ReportDefinition.ReportObjects["Text27"];
                _Invoice_IS.Text = InvoiceCopyType;

            }

            

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT     PhotoCopy_Details.CopyID, PhotoCopy_Details.Copy_Name, PhotoCopy_Items_Details.Unit_Price, PhotoCopy_Items_Details.Copies, 
                      PhotoCopy_Items_Details.Gross_Amount, PhotoCopy_Items_Details.Discount, PhotoCopy_Items_Details.Itm_Net_Amount, 
                      Photocopy_DOC_Details.PhotoC_Invoice_ID, Photocopy_DOC_Details.Gross_Amount AS Expr1, Photocopy_DOC_Details.Discount AS Expr2, 
                      Photocopy_DOC_Details.Net_Amount, Photocopy_DOC_Details.Add_User, Photocopy_DOC_Details.Timp_Stamp
                        FROM         PhotoCopy_Items_Details INNER JOIN
                      PhotoCopy_Details ON PhotoCopy_Items_Details.Copy_ID = PhotoCopy_Details.CopyID INNER JOIN
                      Photocopy_DOC_Details ON PhotoCopy_Items_Details.Ph_Invoice_ID = Photocopy_DOC_Details.PhotoC_Invoice_ID WHERE Photocopy_DOC_Details.PhotoC_Invoice_ID='" + InvoiceID + "'";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                IMSDataSET ds = new IMSDataSET();
                dscmd.Fill(ds);

                //view the christtal report

                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                CrystalReVie_Print_Invoice.ReportSource = rpt;
                CrystalReVie_Print_Invoice.Refresh();

                con1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }



        }
    }
}
