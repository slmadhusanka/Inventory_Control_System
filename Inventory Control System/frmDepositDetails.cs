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
    public partial class frmDepositDetails : Form
    {
        public frmDepositDetails()
        {
            InitializeComponent();
        }
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string diposiID = "";


        public string UserID = "";
        public string UserDisplayName = "";


        public void PrintJob()
        {
           
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
        Double totalDoc = 0;
        private void frmDepositDetails_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;
           
            #region Data Pass to report.................................................................

            try
            {

                #region Job printing========================================

                string totalTables = "";

                //string ReSelectQ = "";

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
                #endregion

                //-------------------------------------------------

                #region Select Comman Field in deposit details.......................................................

                rptCheque_CashDepositDetails rpt1 = new rptCheque_CashDepositDetails();

                TextObject DocID, Banked_Date, Bank_Name, Acc_Num, adduser, totalDocument, user;



                SqlConnection cnn3 = new SqlConnection(IMS);
                cnn3.Open();
                String selectDocID = "SELECT distinct Bank_Doc_details.DoC_ID, Bank_Doc_details.Banked_Date, Bank_Doc_details.Bank_Name, Bank_Doc_details.Acc_Num,Bank_Balance.Add_User FROM  Bank_Doc_details inner join Bank_Balance on Bank_Doc_details.DoC_ID=Bank_Balance.DoC_ID where Bank_Doc_details.DoC_ID='" + diposiID + "'";
                SqlCommand cmm1 = new SqlCommand(selectDocID, cnn3);
                SqlDataReader dr1 = cmm1.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr1.Read())
                {
                    DocID = (TextObject)rpt1.ReportDefinition.ReportObjects["txtDocid"];
                    DocID.Text = dr1[0].ToString();



                    DateTime date = Convert.ToDateTime(dr1[1].ToString());
                    var shortDate = date.ToString("yyyy-MM-dd");


                    Banked_Date = (TextObject)rpt1.ReportDefinition.ReportObjects["txtBankDate"];
                    Banked_Date.Text = shortDate.ToString();



                    Bank_Name = (TextObject)rpt1.ReportDefinition.ReportObjects["txtBankName"];
                    Bank_Name.Text = dr1[2].ToString();

                    Acc_Num = (TextObject)rpt1.ReportDefinition.ReportObjects["txtAccNO"];
                    Acc_Num.Text = dr1[3].ToString();

                    user = (TextObject)rpt1.ReportDefinition.ReportObjects["txtUserID"];
                    user.Text = dr1[4].ToString();


                }
                #endregion

                //...................................................................................

                #region SUM of Cheque Deposit..........................................................

                SqlConnection cnnx = new SqlConnection(IMS);
                cnnx.Open();

                String ChequeQury1 = @"select Bank_Doc_details.DoC_ID,Bank_Doc_details.Bank_Name,Bank_Doc_details.Acc_Num,
                            Bank_Doc_details.Banked_Date,Banked_Cheque_Details.Ck_Invoice_Num,
                            Banked_Cheque_Details.Ck_Number,Bank_Balance.Debit_Amount,Bank_Balance.Add_User from 
                            Bank_Doc_details right outer join Banked_Cheque_Details on Bank_Doc_details.DoC_ID=Banked_Cheque_Details.Bank_Doc_ID
                            inner join Bank_Balance on Bank_Doc_details.DoC_ID=Bank_Balance.DoC_ID where Bank_Doc_details.DoC_ID='" + diposiID + "'and Bank_Balance.Amount_Status='Cheque Deposit' ";

                SqlCommand cmd1 = new SqlCommand(ChequeQury1, cnnx);
                SqlDataReader drx = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                double total_Chek = 0;
                while (drx.Read() == true)
                {
                    total_Chek += Convert.ToDouble(drx[6].ToString());
                }

                // MessageBox.Show(total_Chek.ToString());
                drx.Close();
                #endregion

                //..................................................................................


                #region Select Cash Deposit.................................................................................
                Double tot_Cash = 0;
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String DociDcheck = @"select Bank_Doc_details.DoC_ID,Bank_Doc_details.Bank_Name,Bank_Doc_details.Acc_Num,Bank_Balance.Debit_Amount,Bank_Balance.Balance,Bank_Balance.Add_User,
                                   Bank_Balance.Time_Stamp from Bank_Doc_details right outer join Banked_Cheque_Details on 
                                    Bank_Doc_details.DoC_ID=Banked_Cheque_Details.Bank_Doc_ID right outer join Bank_Balance on 
                                    Bank_Doc_details.DoC_ID=Bank_Balance.DoC_ID where Bank_Balance.DoC_ID='" + diposiID + "'and Bank_Balance.Amount_Status='Cash'";
                SqlCommand cmm = new SqlCommand(DociDcheck, cnn);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
                // MessageBox.Show(DociDcheck);

                while (dr.Read() == true)
                {

                    adduser = (TextObject)rpt1.ReportDefinition.ReportObjects["txtdebitAmount"];
                    adduser.Text = dr[3].ToString();

                    tot_Cash = Convert.ToDouble(dr[3].ToString());


                }
                //  MessageBox.Show(dr[3].ToString());
                #endregion

                //Cheque.......................................................................................

                #region cheque deposit ....................................................................

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();

                String ChequeQury = @"select Bank_Doc_details.DoC_ID,Bank_Doc_details.Bank_Name,Bank_Doc_details.Acc_Num,
                            Bank_Doc_details.Banked_Date,Banked_Cheque_Details.Ck_Invoice_Num,
                            Banked_Cheque_Details.Ck_Number,Bank_Balance.Debit_Amount,Bank_Balance.Add_User from 
                            Bank_Doc_details right outer join Banked_Cheque_Details on Bank_Doc_details.DoC_ID=Banked_Cheque_Details.Bank_Doc_ID
                            inner join Bank_Balance on Bank_Doc_details.DoC_ID=Bank_Balance.DoC_ID where Bank_Doc_details.DoC_ID='" + diposiID + "'and Bank_Balance.Amount_Status='Cheque Deposit' ";
                #endregion


                #region Total of Document.....................................................

                totalDoc = (tot_Cash + total_Chek);

                totalDocument = (TextObject)rpt1.ReportDefinition.ReportObjects["Text28"];
                totalDocument.Text = totalDoc.ToString();

                #endregion


                SqlDataAdapter drc = new SqlDataAdapter(ChequeQury, cnn1);
                IMSDataSET sd = new IMSDataSET();
                drc.Fill(sd);



                rpt1.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
                viwerCash_cheque_deposit.ReportSource = rpt1;
                viwerCash_cheque_deposit.Refresh();
                cnn1.Close();

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
    }
}
