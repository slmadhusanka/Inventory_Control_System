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
   

    public partial class Cancel_Invoice : Form
    {
        public Cancel_Invoice()
        {
            InitializeComponent();
            InvoiceIDLoad();
            getCreatebANKcode();

        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        public string UserID = "";
        public string UserDisplayName = "";


        public void InvoiceIDLoad()
        {
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String InvoiceID = "select InvoiceNo from SoldInvoiceDetails where InvoiceStatus='Sold'";
            SqlCommand cmm = new SqlCommand(InvoiceID,cnn);
            SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            comboBox1.Items.Clear();
            while(dr.Read()==true)
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        public void getCreatebANKcode()
        {
            #region New getCreateReasoncode...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select Cancel_Num from Cancel_Invoice";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    lblCancelID.Text = "CNL1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 Cancel_Num FROM Cancel_Invoice order by Cancel_Num DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        lblCancelID.Text = "CNL" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

       

        private void Cancel_Invoice_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;
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




            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string ReSelectQ = @" SELECT        InvoicePaymentDetails.InvoiceID AS Expr1, SoldItemDetails.InvoiceID AS Expr3, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                         SoldItemDetails.SystemID, SoldItemDetails.ItemWarrenty, SoldItemDetails.SoldPrice, InvoiceCheckDetails.InvoiceID AS Expr2, InvoiceCheckDetails.CkNumber, 
                         InvoiceCheckDetails.Bank, InvoiceCheckDetails.Amount, InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.SubTotal, InvoicePaymentDetails.VATpresentage, 
                         InvoicePaymentDetails.GrandTotal, InvoicePaymentDetails.PayCash, InvoicePaymentDetails.PayCheck, InvoicePaymentDetails.PayCrditCard, 
                         InvoicePaymentDetails.PayDebitCard, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance, InvoicePaymentDetails.InvoiceDate, 
                         InvoicePaymentDetails.InvoiceDiscount, SoldInvoiceDetails.InvoiceNo, SoldInvoiceDetails.InvoiceStatus, SoldInvoiceDetails.CusStatus, 
                         SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CusTelNUmber, SoldInvoiceDetails.CreatedBy, 
                         SoldInvoiceDetails.InvoiceRemark,SoldInvoiceDetails.Return_Invoice_Or_Original,SoldInvoiceDetails.Return_Invoice_Num, SoldItemDetails.ItmQuantity, SoldItemDetails.FreeQuantity, SoldItemDetails.BatchID
                        FROM            InvoiceCheckDetails FULL OUTER JOIN
                         InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                         SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                         SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                      WHERE (InvoicePaymentDetails.InvoiceID = '" + comboBox1.Text + "')";

            SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
            IMSDataSET ds = new IMSDataSET();
            dscmd.Fill(ds);



            //view the christtal report
            CustomerInvoice rpt = new CustomerInvoice();
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            viwerCancelInvoice.ReportSource = rpt;
            viwerCancelInvoice.Refresh();
            con1.Close();

            //pass value to the report (dates From TO)




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }
        string InvoiceIDCheck = "";
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string x = "";
             Double check;
             String SoldInvoiceId = "";



             try
             {


                 SqlConnection con1 = new SqlConnection(IMS);
                 con1.Open();

                 string ReSelectQ = @" SELECT        InvoicePaymentDetails.InvoiceID AS Expr1, SoldItemDetails.InvoiceID AS Expr3, SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                         SoldItemDetails.SystemID, SoldItemDetails.ItemWarrenty, SoldItemDetails.SoldPrice, InvoiceCheckDetails.InvoiceID AS Expr2, InvoiceCheckDetails.CkNumber, 
                         InvoiceCheckDetails.Bank, InvoiceCheckDetails.Amount, InvoicePaymentDetails.InvoiceID, InvoicePaymentDetails.SubTotal, InvoicePaymentDetails.VATpresentage, 
                         InvoicePaymentDetails.GrandTotal, InvoicePaymentDetails.PayCash, InvoicePaymentDetails.PayCheck, InvoicePaymentDetails.PayCrditCard, 
                         InvoicePaymentDetails.PayDebitCard, InvoicePaymentDetails.PAyCredits, InvoicePaymentDetails.PayBalance, InvoicePaymentDetails.InvoiceDate, 
                         InvoicePaymentDetails.InvoiceDiscount, SoldInvoiceDetails.InvoiceNo, SoldInvoiceDetails.InvoiceStatus, SoldInvoiceDetails.CusStatus, 
                         SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CusTelNUmber, SoldInvoiceDetails.CreatedBy, 
                         SoldInvoiceDetails.InvoiceRemark,SoldInvoiceDetails.Return_Invoice_Or_Original,SoldInvoiceDetails.Return_Invoice_Num, SoldItemDetails.ItmQuantity, SoldItemDetails.FreeQuantity, SoldItemDetails.BatchID
                        FROM            InvoiceCheckDetails FULL OUTER JOIN
                         InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                         SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                         SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                      WHERE (InvoicePaymentDetails.InvoiceID = '" + comboBox1.Text + "')";

                 SqlCommand dscmd = new SqlCommand(ReSelectQ, con1);
                 SqlDataReader ds = dscmd.ExecuteReader(CommandBehavior.CloseConnection);

                 while (ds.Read() == true)
                 {

                     check = Convert.ToDouble(ds[17].ToString());

                     // MessageBox.Show("CurrentStockItems");

                     //   MessageBox.Show(ds[36].ToString() + "No");


                     #region chage item status to Sold---------------------------------------------------------------------------------------------
                     if (ds[36].ToString() == "No")
                     {
                         // MessageBox.Show(ds[4].ToString() + "CurrentStockItems        " + ds[5].ToString());

                         SqlConnection con = new SqlConnection(IMS);
                         con.Open();
                         string UpateStockStock = "UPDATE CurrentStockItems SET ItmStatus='" + "Stock" + "' WHERE BarcodeNumber='" + ds[4].ToString() + "' AND SystemID='" + ds[5].ToString() + "'";
                         SqlCommand cmd = new SqlCommand(UpateStockStock, con);
                         cmd.ExecuteNonQuery();

                         if (con.State == ConnectionState.Open)
                         {
                             con.Close();
                         }
                         // MessageBox.Show(UpateStockStock);
                     }
                     #endregion

                     #region update stock wholesale========================================================================================

                     if (ds[36].ToString() != "No")
                     {
                         MessageBox.Show("Read wholesale");

                         SqlConnection cony = new SqlConnection(IMS);
                         cony.Open();
                         string SelectTotalWholesaleStockCount = "SELECT AvailbleItemCount FROM GRNWholesaleItems WHERE ItemID= '" + ds[2].ToString() + "' AND BatchNumber='" + ds[36].ToString() + "'";

                         SqlCommand cmdy = new SqlCommand(SelectTotalWholesaleStockCount, cony);
                         SqlDataReader dry = cmdy.ExecuteReader(CommandBehavior.CloseConnection);

                         string Cnt = "";

                         if (dry.Read())
                         {

                             MessageBox.Show(ds[36].ToString() + "Read wholesale");

                             Cnt = dry[0].ToString();
                         }
                         double StoskCount = Convert.ToDouble(Cnt);

                         if (cony.State == ConnectionState.Open)
                         {
                             cony.Close();
                             dry.Close();
                         }

                         double FinalCount = StoskCount + (Convert.ToDouble(ds[34].ToString()) + Convert.ToDouble(ds[35].ToString()));

                         MessageBox.Show(FinalCount.ToString() + "Read wholesale");

                         SqlConnection conx = new SqlConnection(IMS);
                         conx.Open();
                         string UpateStockStockWholesale = "UPDATE GRNWholesaleItems SET AvailbleItemCount='" + Convert.ToString(FinalCount) + "' WHERE ItemID='" + ds[2].ToString() + "' AND BatchNumber='" + ds[36].ToString() + "'";
                         SqlCommand cmdx = new SqlCommand(UpateStockStockWholesale, conx);
                         cmdx.ExecuteNonQuery();

                         if (conx.State == ConnectionState.Open)
                         {
                             conx.Close();
                         }

                         //================================================================================================================
                     }
                     #endregion

                     #region Reduse the Stock Count-------------------------------------------------------------------------------------------------------

                     SqlConnection con2 = new SqlConnection(IMS);
                     con2.Open();
                     string SelectTotalStockCount = "SELECT AvailableStockCount FROM CurrentStock WHERE ItemID= '" + ds[2].ToString() + "'";
                     SqlCommand cmd1 = new SqlCommand(SelectTotalStockCount, con2);
                     SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                     if (dr1.Read())
                     {
                         x = dr1[0].ToString();
                     }
                     double z = Convert.ToDouble(x);

                     if (con2.State == ConnectionState.Open)
                     {
                         con2.Close();
                         dr1.Close();
                     }
                     double y = z + (Convert.ToDouble(ds[34].ToString()) + Convert.ToDouble(ds[35].ToString()));

                     MessageBox.Show(y.ToString() + "Reduse the Stock Count");
                     SqlConnection con3 = new SqlConnection(IMS);
                     con3.Open();
                     string UpateStockCount = "UPDATE [CurrentStock] SET AvailableStockCount=" + y + " WHERE ItemID='" + ds[2].ToString() + "'";
                     SqlCommand cmd2 = new SqlCommand(UpateStockCount, con3);
                     cmd2.ExecuteNonQuery();

                     if (con3.State == ConnectionState.Open)
                     {
                         cmd2.Dispose();
                         con3.Close();
                         // dr.Close();
                     }
                     // ----------------------------------------------------------------------------------------------------------------------------------------------
                     #endregion

                     #region Update SoldInvoiceDetails Details..........................................

                     SqlConnection cnn41 = new SqlConnection(IMS);
                     cnn41.Open();
                     String SoldAmount = "Select InvoiceNo from SoldInvoiceDetails where InvoiceNo='" + comboBox1.Text + "'  ";
                     SqlCommand cmm41 = new SqlCommand(SoldAmount, cnn41);
                     SqlDataReader dr41 = cmm41.ExecuteReader();

                     if (dr41.Read())
                     {
                         SoldInvoiceId = dr41[0].ToString();
                     }




                     if (ds[0].ToString() == SoldInvoiceId)
                     {
                         SqlConnection cnn4 = new SqlConnection(IMS);
                         cnn4.Open();
                         String ChecAmount = "update SoldInvoiceDetails  set InvoiceStatus='" + "Cancel" + "' where InvoiceNo='" + ds[0].ToString() + "'";
                         SqlCommand cmm4 = new SqlCommand(ChecAmount, cnn4);
                         cmm4.ExecuteNonQuery();
                     }


                     #endregion


                     // MessageBox.Show("Customer PaymentDetails");



                     #region Select check amount for Customert...............................................

                     SqlConnection cnn10 = new SqlConnection(IMS);
                     cnn10.Open();
                     String CheckCreditcheque = @"select Customer_Payment_Details.Docu_No,Customer_Payment_Details.Cheque_Amount,Customer_Payment_Doc_Details.GRN_No from Customer_Payment_Details inner join Customer_Payment_Doc_Details on Customer_Payment_Details.Docu_No=Customer_Payment_Doc_Details.Docu_No where Customer_Payment_Doc_Details.GRN_No='" + comboBox1.Text + "'";
                     SqlCommand cmm10 = new SqlCommand(CheckCreditcheque, cnn10);
                     SqlDataReader dr10 = cmm10.ExecuteReader();
                     // MessageBox.Show("Customer PaymentDetails1");
                     if (dr10.Read())
                     {
                         String ChequeAmount = dr10[1].ToString();
                        // MessageBox.Show(ChequeAmount + "Customer PaymentDetails");
                     }
                     // MessageBox.Show(ds[17].ToString());
                     #endregion


                     #region select Cheque Value and top 1 & insert data in RegCusCredBalance table ...........................

                     String DepositAmount = "";
                     if (Double.Parse(ds[17].ToString()) > 0)
                     {

                         SqlConnection cnnw = new SqlConnection(IMS);
                         cnnw.Open();
                         String DepositCheque = "select InvoicePaymentDetails.InvoiceID,InvoiceCheckDetails.Amount,Customer_Payment_Doc_Details.GRN_No,SoldInvoiceDetails.CusStatus,InvoiceCheckDetails.CkStatus jhg  from InvoicePaymentDetails left outer join Customer_Payment_Doc_Details on InvoicePaymentDetails.InvoiceID=Customer_Payment_Doc_Details.Docu_No left outer join SoldInvoiceDetails on SoldInvoiceDetails.InvoiceNo=Customer_Payment_Doc_Details.GRN_No inner join InvoiceCheckDetails on InvoicePaymentDetails.InvoiceID=InvoiceCheckDetails.InvoiceID where (InvoicePaymentDetails.InvoiceID='" + comboBox1.Text + "' or Customer_Payment_Doc_Details.GRN_No='" + comboBox1.Text + "') and InvoiceCheckDetails.CkStatus='Deposited'";
                         SqlCommand cmm = new SqlCommand(DepositCheque, cnnw);
                         SqlDataReader dr = cmm.ExecuteReader();
                         while (dr.Read())
                         {
                             DepositAmount = dr[1].ToString();

                             //  MessageBox.Show(DepositAmount + "Amount");

                             Double LastDebitBalance;
                             Double Balance;

                             SqlConnection Conn = new SqlConnection(IMS);
                             Conn.Open();
                             // MessageBox.Show(DepositAmount + "TOP (1)");

                             string sql1 = "SELECT TOP (1) Balance,Debit_Balance,DocNumber FROM RegCusCredBalance WHERE (CusID = '" + ds[26].ToString() + "') ORDER BY AutoNum DESC";
                             SqlCommand cmd15 = new SqlCommand(sql1, Conn);
                             SqlDataReader dr7 = cmd15.ExecuteReader();
                             //MessageBox.Show(sql1);

                             if (dr7.Read())
                             {

                                 LastDebitBalance = Convert.ToDouble(dr7[1].ToString());

                                 Balance = Convert.ToDouble(dr7[0].ToString());

                                 //  MessageBox.Show(LastDebitBalance.ToString());
                                 //SqlConnection Conn1 = new SqlConnection(IMS);
                                 //Conn1.Open();

                                 //string sql11 = "SELECT Amount FROM  InvoiceCheckDetails WHERE (InvoiceID = '" + dr7[2].ToString() + "') ORDER BY InvoiceID DESC";
                                 //SqlCommand cmd11 = new SqlCommand(sql11, Conn1);
                                 //SqlDataReader dr8 = cmd11.ExecuteReader(CommandBehavior.CloseConnection);
                                 //if (dr8.Read() == true)
                                 //{
                                 LastDebitBalance += Convert.ToDouble(DepositAmount);

                                 // MessageBox.Show(LastDebitBalance.ToString());


                                 //}

                                 //Conn1.Close();
                                 //dr8.Close();

                                 SqlConnection Conn11 = new SqlConnection(IMS);
                                 Conn11.Open();

                                 string sql111 = @"Insert into RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount, Debit_Balance, Balance, Date)Values
                                        ('" + ds[26].ToString() + "','" + lblCancelID.Text + "','" + "0.00" + "','" + "0.00" + "','" + LastDebitBalance + "','" + Balance + "','" + DateTime.Now.ToShortDateString() + "')";
                                 SqlCommand cmd111 = new SqlCommand(sql111, Conn11);
                                 cmd111.ExecuteNonQuery();

                                 //  MessageBox.Show(sql111);
                     #endregion
                             }





                             //  MessageBox.Show("RegCusCredBalance");

                             //                        SqlConnection Conn11 = new SqlConnection(IMS);
                             //                        Conn11.Open();

                             //                        string sql111 = @"Insert into RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount, Debit_Balance, Balance, Date)Values
                             //                                        ('" + ds[26].ToString() + "','" + lblCancelID.Text + "','" + "0.00" + "','" + "0.00" + "','" + LastDebitBalance + "','" + Balance + "','" + DateTime.Now.ToShortDateString() + "')";
                             //                        SqlCommand cmd111 = new SqlCommand(sql111, Conn11);
                             //                        cmd111.ExecuteNonQuery();

                             //                        MessageBox.Show(sql111);

                             //MessageBox.Show(LastBalance.ToString()+" "+Debit_Balance.ToString());
                             //MessageBox.Show(LastBalance.ToString());
                         }
                     }


                     #region insert Cancel Note Details--------------------------------------------------------------------------------------------------------

                     SqlConnection con5 = new SqlConnection(IMS);
                     con5.Open();
                     string InsertInvoicePaymetDetails = @"INSERT INTO Cancel_Invoice( Cancel_Num, Invoice_Num, Batch_Num, Item_ID, Itm_Name, Serial_Num, Warr_Period, Selling_Price,Tot_Amount, Return_Statement, Return_Accept_by, Return_Date, ReturnAmount, Net_Total,Return_Qnty,Paycheck, PayCreditCard, PayCash, PayDebit, PayBalance,PaycreditsAmount) VALUES
                    ('" + lblCancelID.Text + "','" + ds[12].ToString() + "','" + ds[36].ToString() + "','" + ds[2].ToString() + "','" + ds[3].ToString() + "','" + ds[4].ToString() + "','" + ds[6].ToString() + "','" + ds[7].ToString() + "','" + ds[15].ToString() + "','" + "Cancel" + "','" + "User" + "','" + System.DateTime.Now.ToString() + "','" + ds[15].ToString() + "','" + ds[15].ToString() + "','" + ds[34].ToString() + "','" + ds[17].ToString() + "','" + ds[18].ToString() + "','" + ds[16].ToString() + "','" + ds[19].ToString() + "','" + ds[21].ToString() + "','" + ds[20].ToString() + "')";
                     SqlCommand cmd5 = new SqlCommand(InsertInvoicePaymetDetails, con5);
                     cmd5.ExecuteNonQuery();


                     if (con5.State == ConnectionState.Open)
                     {
                         cmd5.Dispose();
                         con5.Close();
                     }

                     #endregion

                     #region Update Invoice Check Details..........................................



                     if (check > 0)
                     {
                         SqlConnection cnn411 = new SqlConnection(IMS);
                         cnn411.Open();
                         String ChecAmount411 = "select InvoiceCheckDetails.CkStatus,InvoiceCheckDetails.InvoiceID,Customer_Payment_Doc_Details.GRN_No,Customer_Payment_Doc_Details.Docu_No from InvoiceCheckDetails right outer join Customer_Payment_Doc_Details on Customer_Payment_Doc_Details.Docu_No=InvoiceCheckDetails.InvoiceID where Customer_Payment_Doc_Details.GRN_No='" + comboBox1.Text + "' and InvoiceCheckDetails.CkStatus='Active'";
                         SqlCommand cmm411 = new SqlCommand(ChecAmount411, cnn411);
                         SqlDataReader dr411 = cmm411.ExecuteReader();
                         if (dr411.Read())
                         {
                             InvoiceIDCheck = dr411[1].ToString();
                         }



                         SqlConnection cnn4 = new SqlConnection(IMS);
                         cnn4.Open();
                         String ChecAmount = "update InvoiceCheckDetails  set CkStatus='" + "Cancel" + "' where InvoiceID='" + InvoiceIDCheck + "'";
                         SqlCommand cmm4 = new SqlCommand(ChecAmount, cnn4);
                         cmm4.ExecuteNonQuery();
                     }


                     #endregion



                     //  MessageBox.Show(" Banked_Cheque_Details");

                     #region select Banked_Cheque_Details (invoice vise)......................

                     //SqlConnection cnn5 = new SqlConnection(IMS);
                     //cnn5.Open();
                     //String DepositCheck = "select Ck_Invoice_Num from  Banked_Cheque_Details where Ck_Invoice_Num='" + ds[0].ToString() + "'";
                     //SqlCommand cmm5 = new SqlCommand(DepositCheck, cnn5);
                     //SqlDataReader dr5 = cmm5.ExecuteReader();

                     //if (dr5.Read() == true)
                     //{
                     //    //  MessageBox.Show(" Banked_Cheque_Details2");
                     //    SqlConnection cnn6 = new SqlConnection(IMS);
                     //    cnn6.Open();
                     //    String DepositCheckUpdate = "update Banked_Cheque_Details set Status='" + "Cancel" + "'   where Ck_Invoice_Num='" + ds[0].ToString() + "'";
                     //    SqlCommand cmm6 = new SqlCommand(DepositCheckUpdate, cnn6);
                     //    cmm6.ExecuteNonQuery();

                         //---------------------------------------------------------


                         // MessageBox.Show(DepositCheckUpdate + " Banked_Cheque_Details");

                    // }

                     #endregion

                     MessageBox.Show("Message", "Invoice Cancel Succesfully......");

                     getCreatebANKcode();

                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             }
        }
    }
}
