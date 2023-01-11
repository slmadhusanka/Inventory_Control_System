using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Inventory_Control_System
{
    public partial class UserSetting : Form
    {
         string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        public UserSetting()
        {
            InitializeComponent();
            StaffLoad();
           // defaltsettingSelect();
            disablecheckbox();
           
            
        }

        public void StaffLoad()
        {
            try
            {
                #region Staff ID load in combobox...............................

                SqlConnection sd = new SqlConnection(IMS);
                sd.Open();
                String add = "Select UserCode from UserProfile where AtiveDeactive='1'";
                SqlCommand cmm = new SqlCommand(add, sd);
                SqlDataReader dr1 = cmm.ExecuteReader();
                while (dr1.Read())
                {
                    comboBox1.Items.Add(dr1[0].ToString());
                }
                sd.Close();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
       

        public void defaltsettingSelect()
        {
            try
            {
                #region select user Setting.........................

                SqlConnection sd = new SqlConnection(IMS);
                sd.Open();
                String add1 = "Select Add_New_User, Sk_New_Item, Sk_GRN, Sk_Change_Selling_Price, Sk_Barcode, In_Selling_Itm, In_Re_Print_Sell, In_Job, In_Re_Job, In_GRN_Credit,In_Customer_Credit,Re_New_Note,Re_Paymet_Add,Re_JOB_Pay_Add,Cus_add,Ven_Add,Acc_Petty_Cash,Acc_Set_Off,Acc_Bank,Rpt_Items_In_Stock,Rpt_Re_Order_Itms,Rpt_Sold_Inv,Rpt_Pay_Bal,Rpt_Profit_Lost,Rpt_Customer,Rpt_Vender,Rpt_Paymet_Details,User_Setting, In_Return_O_Cancel, Acc_Bank_Acc_Create, Acc_DB_Backup, Rpt_Petty_cash, Rpt_Banking_Details, Rpt_Printing_Details,In_ManualChangePrice,Re_CreditPaymentBalanceRe from User_Settings where User_ID='" + comboBox1.SelectedItem + "'";
                SqlCommand cmm1 = new SqlCommand(add1, sd);
                SqlDataReader dr = cmm1.ExecuteReader();

                while (dr.Read())
                {

                    if (dr[0].ToString() == "1")
                    {
                        cbaddnewUser.Checked = true;
                    }
                    if (dr[0].ToString() == "0")
                    {
                        cbaddnewUser.Checked = false;
                    }//--------------------------------------------------

                    if (dr[1].ToString() == "1")
                    {
                        cbAddNewItem.Checked = true;
                    }
                    if (dr[1].ToString() == "0")
                    {
                        cbAddNewItem.Checked = false;
                    }//--------------------------------------------------
                    if (dr[2].ToString() == "1")
                    {
                        cbAddGRN.Checked = true;
                    }
                    if (dr[2].ToString() == "0")
                    {
                        cbAddGRN.Checked = false;
                    }//--------------------------------------------------


                    if (dr[3].ToString() == "1")
                    {
                        cbChangeSellingPrice.Checked = true;
                    }
                    if (dr[3].ToString() == "0")
                    {
                        cbChangeSellingPrice.Checked = false;
                    }//--------------------------------------------------
                    if (dr[4].ToString() == "1")
                    {
                        cbBarcodeCreate.Checked = true;
                    }
                    if (dr[4].ToString() == "0")
                    {
                        cbBarcodeCreate.Checked = false;
                    }//--------------------------------------------------
                    if (dr[5].ToString() == "1")
                    {
                        SellingItem.Checked = true;
                    }
                    if (dr[5].ToString() == "0")
                    {
                        SellingItem.Checked = false;
                    }//--------------------------------------------------
                    if (dr[6].ToString() == "1")
                    {
                        cbRePrintItemSelling.Checked = true;
                    }
                    if (dr[6].ToString() == "0")
                    {
                        cbRePrintItemSelling.Checked = false;
                    }//--------------------------------------------------

                    if (dr[7].ToString() == "1")
                    {
                        cbJobReparingInvoice.Checked = true;
                    }
                    if (dr[7].ToString() == "0")
                    {
                        cbJobReparingInvoice.Checked = false;
                    }//--------------------------------------------------
                    if (dr[8].ToString() == "1")
                    {
                        cbJobRePrint.Checked = true;
                    }
                    if (dr[8].ToString() == "0")
                    {
                        cbJobRePrint.Checked = false;
                    }//--------------------------------------------------
                    if (dr[9].ToString() == "1")
                    {
                        cbGRNCreatingPaymentDetails.Checked = true;
                    }
                    if (dr[9].ToString() == "0")
                    {
                        cbGRNCreatingPaymentDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[10].ToString() == "1")
                    {
                        cbCustomerCreatePaymentDetails.Checked = true;
                    }
                    if (dr[10].ToString() == "0")
                    {
                        cbCustomerCreatePaymentDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[11].ToString() == "1")
                    {
                        cbNewRepairNote.Checked = true;
                    }
                    if (dr[11].ToString() == "0")
                    {
                        cbNewRepairNote.Checked = false;
                    }//--------------------------------------------------
                    if (dr[12].ToString() == "1")
                    {
                        cbRepaiedItem.Checked = true;
                    }
                    if (dr[12].ToString() == "0")
                    {
                        cbRepaiedItem.Checked = false;
                    }//--------------------------------------------------
                    if (dr[13].ToString() == "1")
                    {
                        cbJobPaymentAdd.Checked = true;
                    }
                    if (dr[13].ToString() == "0")
                    {
                        cbJobPaymentAdd.Checked = false;
                    }//--------------------------------------------------
                    if (dr[14].ToString() == "1")
                    {
                        cbNewCustomer.Checked = true;
                    }
                    if (dr[14].ToString() == "0")
                    {
                        cbNewCustomer.Checked = false;
                    }//--------------------------------------------------
                    if (dr[15].ToString() == "1")
                    {
                        cbAddNewVendor.Checked = true;
                    }
                    if (dr[15].ToString() == "0")
                    {
                        cbAddNewVendor.Checked = false;
                    }//--------------------------------------------------
                    if (dr[16].ToString() == "1")
                    {
                        cbPettyCash.Checked = true;
                    }
                    if (dr[16].ToString() == "0")
                    {
                        cbPettyCash.Checked = false;
                    }//--------------------------------------------------
                    if (dr[17].ToString() == "1")
                    {
                        cbSetOffCash.Checked = true;
                    }
                    if (dr[17].ToString() == "0")
                    {
                        cbSetOffCash.Checked = false;
                    }//--------------------------------------------------
                    if (dr[18].ToString() == "1")
                    {
                        cbBankDetails.Checked = true;
                    }
                    if (dr[18].ToString() == "0")
                    {
                        cbBankDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[19].ToString() == "1")
                    {
                        cbIteminStock.Checked = true;
                    }
                    if (dr[19].ToString() == "0")
                    {
                        cbIteminStock.Checked = false;
                    }//--------------------------------------------------
                    if (dr[20].ToString() == "1")
                    {
                        cbReOrderItems.Checked = true;
                    }
                    if (dr[20].ToString() == "0")
                    {
                        cbReOrderItems.Checked = false;

                    }//--------------------------------------------------
                    if (dr[21].ToString() == "1")
                    {
                        cbSoldInvoiceDetails.Checked = true;
                    }
                    if (dr[21].ToString() == "0")
                    {
                        cbSoldInvoiceDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[22].ToString() == "1")
                    {
                        cbPaymentBalanceReport.Checked = true;
                    }
                    if (dr[22].ToString() == "0")
                    {
                        cbPaymentBalanceReport.Checked = false;
                    }//--------------------------------------------------
                    if (dr[23].ToString() == "1")
                    {
                        cbProfitAndLost.Checked = true;
                    }
                    if (dr[23].ToString() == "0")
                    {
                        cbProfitAndLost.Checked = false;
                    }//--------------------------------------------------
                    if (dr[24].ToString() == "1")
                    {
                        cbViewCustomerDetails.Checked = true;
                    }
                    if (dr[24].ToString() == "0")
                    {
                        cbViewCustomerDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[25].ToString() == "1")
                    {
                        cbViewVenderDetails.Checked = true;
                    }
                    if (dr[25].ToString() == "0")
                    {
                        cbViewVenderDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[26].ToString() == "1")
                    {
                        cbPaymentDetails.Checked = true;
                    }
                    if (dr[26].ToString() == "0")
                    {
                        cbPaymentDetails.Checked = false;
                    }//--------------------------------------------------
                    if (dr[27].ToString() == "1")
                    {
                        cbUserSetting.Checked = true;
                    }
                    if (dr[27].ToString() == "0")
                    {
                        cbUserSetting.Checked = false;
                    }//--------------------------------------------------
                    if (dr[28].ToString() == "1")
                    {
                        cbInvoice_return.Checked = true;
                    }
                    if (dr[28].ToString() == "0")
                    {
                        cbInvoice_return.Checked = false;
                    }//--------------------------------------------------
                    if (dr[29].ToString() == "1")
                    {
                        cbCreate_New_Bank.Checked = true;
                    }
                    if (dr[29].ToString() == "0")
                    {
                        cbCreate_New_Bank.Checked = false;
                    }//--------------------------------------------------
                    if (dr[30].ToString() == "1")
                    {
                        cb_DB_Backup.Checked = true;
                    }
                    if (dr[30].ToString() == "0")
                    {
                        cb_DB_Backup.Checked = false;
                    }//--------------------------------------------------
                    if (dr[31].ToString() == "1")
                    {
                        cb_Petty_Cash.Checked = true;
                    }
                    if (dr[31].ToString() == "0")
                    {
                        cb_Petty_Cash.Checked = false;
                    }//--------------------------------------------------
                    if (dr[32].ToString() == "1")
                    {
                        cb_Banking_Details.Checked = true;
                    }
                    if (dr[32].ToString() == "0")
                    {
                        cb_Banking_Details.Checked = false;
                    }//--------------------------------------------------
                    if (dr[33].ToString() == "1")
                    {
                        cb_Printing_Details.Checked = true;
                    }
                    if (dr[33].ToString() == "0")
                    {
                        cb_Printing_Details.Checked = false;
                    }//--------------------------------------------------
                    // In_ManualChangePrice
                    if (dr[34].ToString() == "1")
                    {
                        checkBox1.Checked = true;
                    }
                    if (dr[34].ToString() == "0")
                    {
                        checkBox1.Checked = false;

                    }
                    //--------------------------------------------------

                    //Report credit paymentBalance details...............................

                    if (dr[35].ToString() == "1")
                    {
                        chb_reportCreditPaymentBalanceDetails.Checked = true;
                    }
                    if (dr[35].ToString() == "0")
                    {
                        chb_reportCreditPaymentBalanceDetails.Checked = false;
                    }//--------------------------------------------------



                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }
        public void disablecheckbox()
        {
            #region  disablecheckbox.............................

            cbaddnewUser.Enabled = false;
            cbAddNewItem.Enabled = false;
            cbAddGRN.Enabled = false;
            cbChangeSellingPrice.Enabled = false;
            cbBarcodeCreate.Enabled = false;
            SellingItem.Enabled = false;
            cbRePrintItemSelling.Enabled = false;
            cbJobReparingInvoice.Enabled = false;
            cbJobRePrint.Enabled = false;
            cbGRNCreatingPaymentDetails.Enabled = false;
            cbCustomerCreatePaymentDetails.Enabled = false;
            cbNewRepairNote.Enabled = false;
            cbRepaiedItem.Enabled = false;
            cbJobPaymentAdd.Enabled = false;
            cbNewCustomer.Enabled = false;
            cbAddNewVendor.Enabled = false;
            cbPettyCash.Enabled = false;
            cbSetOffCash.Enabled = false;
            cbBankDetails.Enabled = false;
            cbIteminStock.Enabled = false;
            cbReOrderItems.Enabled = false;
            cbSoldInvoiceDetails.Enabled = false;
            cbPaymentBalanceReport.Enabled = false;
            cbProfitAndLost.Enabled = false;
            cbViewCustomerDetails.Enabled = false;
            cbViewVenderDetails.Enabled = false;
            cbPaymentDetails.Enabled = false;
            cbUserSetting.Enabled = false;

            cbInvoice_return.Enabled = false;
            cbCreate_New_Bank.Enabled = false;
            cb_DB_Backup.Enabled = false;
            cb_Petty_Cash.Enabled = false;
            cb_Banking_Details.Enabled = false;
            cb_Printing_Details.Enabled = false;
            checkBox1.Enabled = false;
            chb_reportCreditPaymentBalanceDetails.Enabled = false;

            #endregion
        }

        public void uncheck_Ck_BOX()
        {
            #region Uncheck Bexes...............................

            cbaddnewUser.Checked = false;
            cbAddNewItem.Checked = false;
            cbAddGRN.Checked = false;
            cbChangeSellingPrice.Checked = false;
            cbBarcodeCreate.Checked = false;
            SellingItem.Checked = false;
            cbRePrintItemSelling.Checked = false;
            cbJobReparingInvoice.Checked = false;
            cbJobRePrint.Checked = false;
            cbGRNCreatingPaymentDetails.Checked = false;
            cbCustomerCreatePaymentDetails.Checked = false;
            cbNewRepairNote.Checked = false;
            cbRepaiedItem.Checked = false;
            cbJobPaymentAdd.Checked = false;
            cbNewCustomer.Checked = false;
            cbAddNewVendor.Checked = false;
            cbPettyCash.Checked = false;
            cbSetOffCash.Checked = false;
            cbBankDetails.Checked = false;
            cbIteminStock.Checked = false;
            cbReOrderItems.Checked = false;
            cbSoldInvoiceDetails.Checked = false;
            cbPaymentBalanceReport.Checked = false;
            cbProfitAndLost.Checked = false;
            cbViewCustomerDetails.Checked = false;
            cbViewVenderDetails.Checked = false;
            cbPaymentDetails.Checked = false;
            cbUserSetting.Checked = false;

            cbInvoice_return.Checked = false;
            cbCreate_New_Bank.Checked = false;
            cb_DB_Backup.Checked = false;
            cb_Petty_Cash.Checked = false;
            cb_Banking_Details.Checked = false;
            cb_Printing_Details.Checked = false;

            btnEdit.Enabled = false;
            checkBox1.Checked = false;
            chb_reportCreditPaymentBalanceDetails.Checked = false;

            #endregion
        }

        public void enablecheckbox()
        {
            #region  enablecheckbox.............................

            cbaddnewUser.Enabled = true;
            cbAddNewItem.Enabled = true;
            cbAddGRN.Enabled = true;
            cbChangeSellingPrice.Enabled = true;
            cbBarcodeCreate.Enabled = true;
            SellingItem.Enabled = true;
            cbRePrintItemSelling.Enabled = true;
            cbJobReparingInvoice.Enabled = true;
            cbJobRePrint.Enabled = true;
            cbGRNCreatingPaymentDetails.Enabled = true;
            cbCustomerCreatePaymentDetails.Enabled = true;
            cbNewRepairNote.Enabled = true;
            cbRepaiedItem.Enabled = true;
            cbJobPaymentAdd.Enabled = true;
            cbNewCustomer.Enabled = true;
            cbAddNewVendor.Enabled = true;
            cbPettyCash.Enabled = true;
            cbSetOffCash.Enabled = true;
            cbBankDetails.Enabled = true;
            cbIteminStock.Enabled = true;
            cbReOrderItems.Enabled = true;
            cbSoldInvoiceDetails.Enabled = true;
            cbPaymentBalanceReport.Enabled = true;
            cbProfitAndLost.Enabled = true;
            cbViewCustomerDetails.Enabled = true;
            cbViewVenderDetails.Enabled = true;
            cbPaymentDetails.Enabled = true;
            cbUserSetting.Enabled = true;

            cbInvoice_return.Enabled = true;
            cbCreate_New_Bank.Enabled = true;
            cb_DB_Backup.Enabled = true;
            cb_Petty_Cash.Enabled = true;
            cb_Banking_Details.Enabled = true;
            cb_Printing_Details.Enabled = true;
            checkBox1.Enabled=true;
            chb_reportCreditPaymentBalanceDetails.Enabled = true;

            #endregion
        }
       
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        String Add_New_User1, Sk_New_Item1, Sk_GRN1, Sk_Change_Selling_Price1, Sk_Barcode1, In_Selling_Itm1, In_Re_Print_Sell1, In_Job1, In_Re_Job1, In_GRN_Credit1, In_Customer_Credit1, Re_New_Note1, Re_Paymet_Add1, Re_JOB_Pay_Add1, Cus_add1, Ven_Add1, Acc_Petty_Cash1, Acc_Set_Off1, Acc_Bank1, Rpt_Items_In_Stock1, Rpt_Re_Order_Itms1, Rpt_Sold_Inv1, Rpt_Pay_Bal1, Rpt_Profit_Lost1, Rpt_Customer1, Rpt_Vender1, Rpt_Paymet_Details1, User_Setting1, Invoice_return1, Create_New_Bank1, DB_Backup1, Petty_Cash1, Banking_Details1, Printing_Details1, ManualChangePrice,ReCreditPaymentBalanceDetails;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region Update user setting.........................

                if (cbaddnewUser.Checked == true)
                {
                    Add_New_User1 = "1";
                }
                if (cbaddnewUser.Checked == false)
                {
                    Add_New_User1 = "0";
                }//--------------------------------------------------

                if (cbAddNewItem.Checked == true)
                {
                    Sk_New_Item1 = "1";
                }
                if (cbAddNewItem.Checked == false)
                {
                    Sk_New_Item1 = "0";
                }//--------------------------------------------------
                if (cbAddGRN.Checked == true)
                {
                    Sk_GRN1 = "1";
                }
                if (cbAddGRN.Checked == false)
                {
                    Sk_GRN1 = "0";
                }//--------------------------------------------------


                if (cbChangeSellingPrice.Checked == true)
                {
                    Sk_Change_Selling_Price1 = "1";
                }
                if (cbChangeSellingPrice.Checked == false)
                {
                    Sk_Change_Selling_Price1 = "0";
                }//--------------------------------------------------
                if (cbBarcodeCreate.Checked == true)
                {
                    Sk_Barcode1 = "1";
                }
                if (cbBarcodeCreate.Checked == false)
                {
                    Sk_Barcode1 = "0";
                }//--------------------------------------------------
                if (SellingItem.Checked == true)
                {
                    In_Selling_Itm1 = "1";
                }
                if (SellingItem.Checked == false)
                {
                    In_Selling_Itm1 = "0";
                }//--------------------------------------------------
                if (cbRePrintItemSelling.Checked == true)
                {
                    In_Re_Print_Sell1 = "1";
                }
                if (cbRePrintItemSelling.Checked == false)
                {
                    In_Re_Print_Sell1 = "0";
                }//--------------------------------------------------

                if (cbJobReparingInvoice.Checked == true)
                {
                    In_Job1 = "1";
                }
                if (cbJobReparingInvoice.Checked == false)
                {
                    In_Job1 = "0";
                }//--------------------------------------------------
                if (cbJobRePrint.Checked == true)
                {
                    In_Re_Job1 = "1";
                }
                if (cbJobRePrint.Checked == false)
                {
                    In_Re_Job1 = "0";
                }//--------------------------------------------------
                if (cbGRNCreatingPaymentDetails.Checked == true)
                {
                    In_GRN_Credit1 = "1";
                }
                if (cbGRNCreatingPaymentDetails.Checked == false)
                {
                    In_GRN_Credit1 = "0";
                }//--------------------------------------------------
                if (cbCustomerCreatePaymentDetails.Checked == true)
                {
                    In_Customer_Credit1 = "1";
                }
                if (cbCustomerCreatePaymentDetails.Checked == false)
                {
                    In_Customer_Credit1 = "0";
                }//--------------------------------------------------
                if (cbNewRepairNote.Checked == true)
                {
                    Re_New_Note1 = "1";
                }
                if (cbNewRepairNote.Checked == false)
                {
                    Re_New_Note1 = "0";
                }//--------------------------------------------------
                if (cbRepaiedItem.Checked == true)
                {
                    Re_Paymet_Add1 = "1";
                }
                if (cbRepaiedItem.Checked == false)
                {
                    Re_Paymet_Add1 = "0";
                }//--------------------------------------------------
                if (cbJobPaymentAdd.Checked == true)
                {
                    Re_JOB_Pay_Add1 = "1";
                }
                if (cbJobPaymentAdd.Checked == false)
                {
                    Re_JOB_Pay_Add1 = "0";
                }//--------------------------------------------------
                if (cbNewCustomer.Checked == true)
                {
                    Cus_add1 = "1";
                }
                if (cbNewCustomer.Checked == false)
                {
                    Cus_add1 = "0";
                }//--------------------------------------------------
                if (cbAddNewVendor.Checked == true)
                {
                    Ven_Add1 = "1";
                }
                if (cbAddNewVendor.Checked == false)
                {
                    Ven_Add1 = "0";
                }//--------------------------------------------------
                if (cbPettyCash.Checked == true)
                {
                    Acc_Petty_Cash1 = "1";
                }
                if (cbPettyCash.Checked == false)
                {
                    Acc_Petty_Cash1 = "0";
                }//--------------------------------------------------
                if (cbSetOffCash.Checked == true)
                {
                    Acc_Set_Off1 = "1";
                }
                if (cbSetOffCash.Checked == false)
                {
                    Acc_Set_Off1 = "0";
                }//--------------------------------------------------
                if (cbBankDetails.Checked == true)
                {
                    Acc_Bank1 = "1";
                }
                if (cbBankDetails.Checked == false)
                {
                    Acc_Bank1 = "0";
                }//--------------------------------------------------
                if (cbIteminStock.Checked == true)
                {
                    Rpt_Items_In_Stock1 = "1";
                }
                if (cbIteminStock.Checked == false)
                {
                    Rpt_Items_In_Stock1 = "0";
                }//--------------------------------------------------
                if (cbReOrderItems.Checked == true)
                {
                    Rpt_Re_Order_Itms1 = "1";
                }
                if (cbReOrderItems.Checked == false)
                {
                    Rpt_Re_Order_Itms1 = "0";

                }//--------------------------------------------------
                if (cbSoldInvoiceDetails.Checked == true)
                {
                    Rpt_Sold_Inv1 = "1";
                }
                if (cbSoldInvoiceDetails.Checked == false)
                {
                    Rpt_Sold_Inv1 = "0";
                }//--------------------------------------------------
                if (cbPaymentBalanceReport.Checked == true)
                {
                    Rpt_Pay_Bal1 = "1";
                }
                if (cbPaymentBalanceReport.Checked == false)
                {
                    Rpt_Pay_Bal1 = "0";
                }//--------------------------------------------------
                if (cbProfitAndLost.Checked == true)
                {
                    Rpt_Profit_Lost1 = "1";
                }
                if (cbProfitAndLost.Checked == false)
                {
                    Rpt_Profit_Lost1 = "0";
                }//--------------------------------------------------
                if (cbViewCustomerDetails.Checked == true)
                {
                    Rpt_Customer1 = "1";
                }
                if (cbViewCustomerDetails.Checked == false)
                {
                    Rpt_Customer1 = "0";
                }//--------------------------------------------------
                if (cbViewVenderDetails.Checked == true)
                {
                    Rpt_Vender1 = "1";
                }
                if (cbViewVenderDetails.Checked == false)
                {
                    Rpt_Vender1 = "0";
                }//--------------------------------------------------
                if (cbPaymentDetails.Checked == true)
                {
                    Rpt_Paymet_Details1 = "1";
                }
                if (cbPaymentDetails.Checked == false)
                {
                    Rpt_Paymet_Details1 = "0";
                }//--------------------------------------------------
                if (cbUserSetting.Checked == true)
                {
                    User_Setting1 = "1";
                }
                if (cbUserSetting.Checked == false)
                {
                    User_Setting1 = "0";
                }//--------------------------------------------------

                if (cbInvoice_return.Checked == true)
                {
                    Invoice_return1 = "1";
                }
                if (cbInvoice_return.Checked == false)
                {
                    Invoice_return1 = "0";
                }//--------------------------------------------------

                if (cbCreate_New_Bank.Checked == true)
                {
                    Create_New_Bank1 = "1";
                }
                if (cbCreate_New_Bank.Checked == false)
                {
                    Create_New_Bank1 = "0";
                }//--------------------------------------------------

                if (cb_DB_Backup.Checked == true)
                {
                    DB_Backup1 = "1";
                }
                if (cb_DB_Backup.Checked == false)
                {
                    DB_Backup1 = "0";
                }//--------------------------------------------------

                if (cb_Petty_Cash.Checked == true)
                {
                    Petty_Cash1 = "1";
                }
                if (cb_Petty_Cash.Checked == false)
                {
                    Petty_Cash1 = "0";
                }//--------------------------------------------------

                if (cb_Banking_Details.Checked == true)
                {
                    Banking_Details1 = "1";
                }
                if (cb_Banking_Details.Checked == false)
                {
                    Banking_Details1 = "0";
                }//--------------------------------------------------

                if (cb_Printing_Details.Checked == true)
                {
                    Printing_Details1 = "1";
                }
                if (cb_Printing_Details.Checked == false)
                {
                    Printing_Details1 = "0";
                }//--------------------------------------------------

                if (checkBox1.Checked == true)
                {
                    ManualChangePrice = "1";
                }
                if (checkBox1.Checked == false)
                {
                    ManualChangePrice = "0";
                }

                //-----------------------------------------------------------------

                if (chb_reportCreditPaymentBalanceDetails.Checked == true)
                {
                    ReCreditPaymentBalanceDetails = "1";
                }
                if (chb_reportCreditPaymentBalanceDetails.Checked == false)
                {
                    ReCreditPaymentBalanceDetails = "0";
                }
                #endregion

                #region pass data to DB --------------------------------------

                DialogResult se = MessageBox.Show("Dou you want to Edit User Setting ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (se == DialogResult.Yes)
                {
                    SqlConnection cnn = new SqlConnection(IMS);
                    cnn.Open();
                    String update1 = @"update User_Settings set Add_New_User='" + Add_New_User1 + "',Sk_New_Item='" + Sk_New_Item1 + "',Sk_GRN='" + Sk_GRN1 + "',Sk_Change_Selling_Price='" + Sk_Change_Selling_Price1 + "',Sk_Barcode='" + Sk_Barcode1 + "',In_Selling_Itm='" + In_Selling_Itm1 + "',In_Re_Print_Sell='" + In_Re_Print_Sell1 + "',In_Job='" + In_Job1 + "',In_Re_Job='" + In_Re_Job1 + "',In_GRN_Credit='" + In_GRN_Credit1 + "',In_Customer_Credit='" + In_Customer_Credit1 + "',Re_New_Note='" + Re_New_Note1 + "',Re_Paymet_Add='" + Re_Paymet_Add1 + "',Re_JOB_Pay_Add='" + Re_JOB_Pay_Add1 + "',Cus_add='" + Cus_add1 + "',Ven_Add='" + Ven_Add1 + "',Acc_Petty_Cash='" + Acc_Petty_Cash1 + "',Acc_Set_Off='" + Acc_Set_Off1 + "',Acc_Bank='" + Acc_Bank1 + "',Rpt_Items_In_Stock='" + Rpt_Items_In_Stock1 + "',Rpt_Re_Order_Itms='" + Rpt_Re_Order_Itms1 + "',Rpt_Sold_Inv='" + Rpt_Sold_Inv1 + "',Rpt_Pay_Bal='" + Rpt_Pay_Bal1 + "',Rpt_Profit_Lost='" + Rpt_Profit_Lost1 + "',Rpt_Customer='" + Rpt_Customer1 + "',Rpt_Vender='" + Rpt_Vender1 + "',Rpt_Paymet_Details='" + Rpt_Paymet_Details1 + "',User_Setting='" + User_Setting1 + "', In_Return_O_Cancel='" + Invoice_return1 + "',Acc_Bank_Acc_Create='" + Create_New_Bank1 + "', Acc_DB_Backup='" + DB_Backup1 + "', Rpt_Petty_cash='" + Petty_Cash1 + "', Rpt_Banking_Details='" + Banking_Details1 + "', Rpt_Printing_Details='" + Printing_Details1 + "',In_ManualChangePrice='" + ManualChangePrice + "',Re_CreditPaymentBalanceRe='" + ReCreditPaymentBalanceDetails + "' where [User_ID]='" + comboBox1.Text + "' ";
                    SqlCommand cmm = new SqlCommand(update1, cnn);
                    cmm.ExecuteNonQuery();
                    MessageBox.Show("Saved Successfully...");
                }
                #endregion

                #region Clear checkbox and combo box................

                comboBox1.SelectedIndex = -1;

                disablecheckbox();

                uncheck_Ck_BOX();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            defaltsettingSelect();

            if (comboBox1.SelectedIndex != -1)
            {
                btnEdit.Enabled = true;
                btnCancel.Enabled = true;
                enablecheckbox();
            }
            if (comboBox1.SelectedIndex == -1)
            {
                btnEdit.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region Clear checkbox and combo box

                comboBox1.SelectedIndex = -1;
                uncheck_Ck_BOX();
                disablecheckbox();

                
            #endregion

        }

        private void btnEdit_MouseDown(object sender, MouseEventArgs e)
        {
            
            
        }

        private void btnEdit_MouseLeave(object sender, EventArgs e)
        {
            btnEdit.UseVisualStyleBackColor = true;
        }

        private void btnEdit_MouseEnter(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.OrangeRed;
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.OrangeRed;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.UseVisualStyleBackColor = true;
        }

        private void UserSetting_Load(object sender, EventArgs e)
        {
           
        }
        }
    }

