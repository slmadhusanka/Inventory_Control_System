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
    public partial class frmCustomerCreditPayment : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public frmCustomerCreditPayment()
        {
            InitializeComponent();
        }

        private void frmCustomerCreditPayment_Load(object sender, EventArgs e)
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

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String CreditCustomer = @"SELECT        Customer_Payment_Details.Docu_No, Customer_Payment_Details.Cash_Amount, Customer_Payment_Details.Cheque_Amount, 
                         Customer_Payment_Details.Cheque_No, Customer_Payment_Details.Branch, Customer_Payment_Details.Card_No, 
                         Customer_Payment_Doc_Details.GRN_No, Customer_Payment_Doc_Details.Date, Customer_Payment_Doc_Details.Paid_amount, 
                         SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CusTelNUmber, SoldInvoiceDetails.CusStatus, 
                         Customer_Payment_Details.Card_Amount, InvoiceCheckDetails.Bank, InvoiceCheckDetails.MentionDate, InvoiceCheckDetails.Amount
                         FROM            Customer_Payment_Details INNER JOIN
                         Customer_Payment_Doc_Details ON Customer_Payment_Details.Docu_No = Customer_Payment_Doc_Details.Docu_No INNER JOIN
                         SoldInvoiceDetails ON Customer_Payment_Doc_Details.GRN_No = SoldInvoiceDetails.InvoiceNo FULL JOIN
                         InvoiceCheckDetails ON Customer_Payment_Details.Docu_No = InvoiceCheckDetails.InvoiceID wHERE Customer_Payment_Details.Docu_No='CCP10000004'";
            SqlDataAdapter ds = new SqlDataAdapter(CreditCustomer, cnn);
            IMSDataSET sd = new IMSDataSET();
            ds.Fill(sd);


           

            aaaaaaaaaaaaaa rpt = new aaaaaaaaaaaaaa();
            MessageBox.Show(CreditCustomer);
            rpt.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
            ViwerCustomerCreditPayment.ReportSource = rpt;
            ViwerCustomerCreditPayment.Refresh();
            cnn.Close();
        }
    }
}
