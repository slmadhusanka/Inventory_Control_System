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
    public partial class FRMcredit_Payment_Balance_details : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        String grnidsql = "";
        String invoiceid = "";

        public string UserID = "";
        public string UserDisplayName = "";
        
        public FRMcredit_Payment_Balance_details()
        {
            InitializeComponent();
        }

        public void LoadVenderId()
        {
            #region Load vender ID..................................................

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String ven= "select VenderID,VenderName from VenderDetails";
            SqlCommand cmm = new SqlCommand(ven, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while(dr.Read())
            {
                cmbSupplier.Items.Add(dr[0].ToString());
            }
            #endregion
        }

        public void LoadCustomerId()
        {
            #region Load customer Id.........................................

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String ven = "select CusID,CusFirstName,CusLastName from CustomerDetails";
            SqlCommand cmm = new SqlCommand(ven, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read())
            {
                cmbCustomer.Items.Add(dr[0].ToString());
            }
            #endregion

        }
     //   select GRN_No,Vender_ID from GRN_amount_Details
        public void LoadGrnId()
        {
            #region load grn id...........................................

            cmbGRNID.Items.Clear();
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String ven = "select GRN_No,Vender_ID from GRN_amount_Details where Vender_ID='"+cmbSupplier.Text+"'";
            SqlCommand cmm = new SqlCommand(ven, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read())
            {
                cmbGRNID.Items.Add(dr[0].ToString());
            }
            #endregion
        }

        public void LoadInvoiceId()
        {
            #region load invoice ID..............................

            cmbInvoice.Items.Clear();

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String ven = "select InvoiceNo,CusStatus from SoldInvoiceDetails where CusStatus='" + cmbCustomer.Text + "'";
            SqlCommand cmm = new SqlCommand(ven, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read())
            {
                cmbInvoice.Items.Add(dr[0].ToString());
            }
            #endregion
        }

        public void SqlGrnID()
        {
            #region sql Supplier .........................................................................
            //if (rbtAllSupplier.Checked == true)
            //{
                #region all supplier.....................................

            grnidsql = @"SELECT        vender_Payment.DocNumber, vender_Payment.VenderID, vender_Payment.Credit_Amount, vender_Payment.Balance, vender_Payment.Debit_Amount, 
                         vender_Payment.Date, GRN_Payment_Doc_Details.GRN_No, GRN_amount_Details.Net_Amount,GRNInvoicePaymentDetails. PayBalance
                         FROM            vender_Payment INNER JOIN
                         GRN_Payment_Doc_Details ON vender_Payment.DocNumber = GRN_Payment_Doc_Details.Docu_No INNER JOIN
                         GRN_amount_Details ON GRN_Payment_Doc_Details.GRN_No = GRN_amount_Details.GRN_No inner join GRNInvoicePaymentDetails on GRN_amount_Details.GRN_No=GRNInvoicePaymentDetails.GRNID where 1=1";
                #endregion
            //}

            //By supplier-------------------------------------------------

                if (rbtBySupplier.Checked == true)
                {
                    grnidsql += "  and vender_Payment.VenderID='" + cmbSupplier.Text + "'";
                }

            //grn Id-----------------------------------------------------------

                if (cmbGRNID.Text != "")
                {
                    grnidsql += "  and GRN_Payment_Doc_Details.GRN_No='" + cmbGRNID.Text + "'";

                }

                grnidsql.Replace("1=1 AND ", "");
                grnidsql.Replace(" WHERE 1=1 ", "");

            // MessageBox.Show(grnidsql);
            #endregion
        }

        public void SqlinvoiceID()
        {
            #region sql invoice...................................................................
            invoiceid = "";
            invoiceid = @"select InvoicePaymentDetails.SubTotal,InvoicePaymentDetails.PAyCredits,InvoicePaymentDetails.PayBalance,SoldInvoiceDetails.CusStatus,
                Customer_Payment_Doc_Details.Docu_No,Customer_Payment_Doc_Details.GRN_No,Customer_Payment_Doc_Details.Paid_amount,Customer_Payment_Doc_Details.[Date]  
                from Customer_Payment_Doc_Details inner join SoldInvoiceDetails on Customer_Payment_Doc_Details.GRN_No = SoldInvoiceDetails.InvoiceNo inner join InvoicePaymentDetails 
                on Customer_Payment_Doc_Details.GRN_No=InvoicePaymentDetails.InvoiceID where 1=1";

            //if(rbtAllCustomer.Checked==true)
            //{
                
            //}

            if(rbtByCustomer.Checked==true)
            {
               invoiceid += " and SoldInvoiceDetails.CusStatus='"+cmbCustomer.Text+"' ";
            }

            if (cmbInvoice.Text != "")
            {
                invoiceid += " and Customer_Payment_Doc_Details.GRN_No='" + cmbInvoice.Text + "'";
            }

            invoiceid.Replace("1=1 AND ", "");
            invoiceid.Replace(" WHERE 1=1 ", "");

           // MessageBox.Show(invoiceid);
            #endregion
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         //  LoadInvoiceId();
        }

        private void rbtCustomer_CheckedChanged(object sender, EventArgs e)
        {
            

            if (rbtCustomer.Checked == true)
            {
                #region  validation...............
                LoadCustomerId();

               // cmbCustomer.Enabled = true;
                cmbInvoice.Enabled = true;
                rbtAllCustomer.Enabled = true;
                rbtByCustomer.Enabled = true;
                cmbSupplier.Enabled = false;
                cmbGRNID.Enabled = false;
                rbtBySupplier.Checked = false;
                rbtAllSupplier.Checked = false;
                rbtBySupplier.Enabled = false;
                rbtAllSupplier.Enabled = false;
                rbtAllCustomer.Checked = true;

                lblSupName.Text = "";
                #endregion
            }
           
        }

        private void rbtVender_CheckedChanged(object sender, EventArgs e)
        {
            

            if (rbtVender.Checked == true)
            {
                #region  validation...............

                LoadVenderId();
                rbtAllCustomer.Checked = false;
                rbtAllCustomer.Enabled = false;
                rbtByCustomer.Enabled = false;
                rbtByCustomer.Checked = false;
                cmbCustomer.Enabled = false;
                cmbInvoice.Enabled = false;
               // cmbSupplier.Enabled = true;
                cmbGRNID.Enabled = true;
                lblcusname.Text = "";
                cmbCustomer.SelectedIndex = -1;
                rbtAllSupplier.Enabled = true;
                rbtBySupplier.Enabled = true;
                rbtAllSupplier.Checked = true;

                #endregion
            }
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region  supplier name pass...............

                LoadGrnId();
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String ven = "select VenderID,VenderName from VenderDetails where VenderID='" + cmbSupplier.Text + "'";
                SqlCommand cmm = new SqlCommand(ven, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    lblSupName.Text = dr[1].ToString();
                }
                #endregion
            }
             
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region  Customer Name Pass...............

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String ven = "select CusID,CusFirstName,CusLastName from CustomerDetails where CusID='" + cmbCustomer.Text + "'";
                SqlCommand cmm = new SqlCommand(ven, cnn);
                SqlDataReader dr = cmm.ExecuteReader();
                while (dr.Read())
                {
                    lblcusname.Text = dr[1].ToString() + "   " + dr[2].ToString();
                }
                LoadInvoiceId();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                #region Load the Report..................................................
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

                if (rbtVender.Checked == true)
                {
                    #region  vender wise....................................................................

                    rpt_credit_Payment_Balance_details rpt1 = new rpt_credit_Payment_Balance_details();

                    TextObject supname, Name;

                    if (rbtBySupplier.Checked == true)
                    {
                        if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
                        {
                            supname = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
                            supname.Text = lblSupName.Text;
                        }

                        if (rpt1.ReportDefinition.ReportObjects["Text7"] != null)
                        {
                            Name = (TextObject)rpt1.ReportDefinition.ReportObjects["Text7"];
                            Name.Text = "Name :";
                        }

                    }

                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();
                    SqlGrnID();
                    //                    String grn = @"SELECT        vender_Payment.DocNumber, vender_Payment.VenderID, vender_Payment.Credit_Amount, vender_Payment.Balance, vender_Payment.Debit_Amount, 
                    //                         vender_Payment.Date, GRN_Payment_Doc_Details.GRN_No, GRN_amount_Details.Net_Amount
                    //FROM            vender_Payment INNER JOIN
                    //                         GRN_Payment_Doc_Details ON vender_Payment.DocNumber = GRN_Payment_Doc_Details.Docu_No INNER JOIN
                    //                         GRN_amount_Details ON GRN_Payment_Doc_Details.GRN_No = GRN_amount_Details.GRN_No where GRN_Payment_Doc_Details.GRN_No='" + cmbGRNID.Text + "'";

                    SqlDataAdapter drc = new SqlDataAdapter(grnidsql, cnn1);
                    IMSDataSET sd = new IMSDataSET();
                    drc.Fill(sd);

                    //View report


                    rpt1.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
                    rptViwercredit_Payment_Balance.ReportSource = rpt1;
                    rptViwercredit_Payment_Balance.Refresh();
                    cnn1.Close();


                    #endregion
                }

                if (rbtCustomer.Checked == true)
                {
                    #region customer wise..................................................................................

                    rpt_credit_Payment_Balance_details_customer rpt1 = new rpt_credit_Payment_Balance_details_customer();

                    if (rbtByCustomer.Checked == true)
                    {


                        TextObject cusname, Name;

                        if (rpt1.ReportDefinition.ReportObjects["Text2"] != null)
                        {
                            cusname = (TextObject)rpt1.ReportDefinition.ReportObjects["Text2"];
                            cusname.Text = lblcusname.Text;
                        }

                        if (rpt1.ReportDefinition.ReportObjects["Text1"] != null)
                        {
                            Name = (TextObject)rpt1.ReportDefinition.ReportObjects["Text1"];
                            Name.Text = "Name :";
                        }
                    }

                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();
                    SqlinvoiceID();
                    SqlDataAdapter drc = new SqlDataAdapter(invoiceid, cnn1);
                    IMSDataSET sd = new IMSDataSET();
                    drc.Fill(sd);


                    rpt1.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
                    rptViwercredit_Payment_Balance.ReportSource = rpt1;
                    rptViwercredit_Payment_Balance.Refresh();
                    cnn1.Close();
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void cmbGRN(object sender, EventArgs e)
        {

        }

        private void rbtAllSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtAllSupplier.Checked == true)
                {
                    rbtBySupplier.Checked = false;
                    cmbSupplier.SelectedIndex = -1;
                    cmbGRNID.Items.Clear();
                    SqlConnection cnn = new SqlConnection(IMS);
                    cnn.Open();
                    String ven = "select GRN_No,Vender_ID from GRN_amount_Details";
                    SqlCommand cmm = new SqlCommand(ven, cnn);
                    SqlDataReader dr = cmm.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbGRNID.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void rbtByCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtByCustomer.Checked == true)
            {
                LoadInvoiceId();
                cmbCustomer.Enabled = true;
            }
            if (rbtByCustomer.Checked == false)
            {
                cmbCustomer.Enabled = false;
                cmbCustomer.SelectedIndex = -1;
                lblcusname.Text = "";
            }
        }

        private void rbtBySupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBySupplier.Checked == true)
            {
                cmbSupplier.Enabled = true;
                cmbGRNID.SelectedIndex = -1;
            }
            if (rbtBySupplier.Checked == false)
            {
                cmbSupplier.Enabled = false;
                cmbSupplier.SelectedIndex = -1;
                lblSupName.Text = "";
                cmbGRNID.SelectedIndex = -1;
            }
        }

        private void rbtAllCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                #region Load Allinvoice...................................................
                if (rbtAllCustomer.Checked == true)
                {
                    cmbInvoice.Items.Clear();

                    SqlConnection cnn = new SqlConnection(IMS);
                    cnn.Open();
                    String ven = "select InvoiceNo,CusStatus from SoldInvoiceDetails";
                    SqlCommand cmm = new SqlCommand(ven, cnn);
                    SqlDataReader dr = cmm.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbInvoice.Items.Add(dr[0].ToString());
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                if(rbtVender.Checked==true)
                {
                    cmbGRNID.SelectedIndex = -1;
                    cmbInvoice.Enabled = false;
                }

                if (rbtCustomer.Checked == true)
                {
                    cmbInvoice.SelectedIndex = -1;
                    cmbGRNID.Enabled = false;
                }
                

                
                
               
                checkEdit1.Checked = false;
            }
        }

        private void cmbInvoice_Click(object sender, EventArgs e)
        {
            if (rbtByCustomer.Checked == true && cmbCustomer.Text=="")
            {
                MessageBox.Show("Please selsect Customer", "Message",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void cmbGRNID_Click(object sender, EventArgs e)
        {
            if (rbtBySupplier.Checked == true && cmbSupplier.Text == "")
            {
                MessageBox.Show("Please selsect Supplier", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void FRMcredit_Payment_Balance_details_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;
        }
    }
}
