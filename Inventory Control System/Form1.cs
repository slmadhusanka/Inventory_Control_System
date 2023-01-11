using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;
using Sales_and_Inventory_System__Gadgets_Shop_;
using DevExpress.XtraReports.Design;


namespace Inventory_Control_System
{
    public partial class MainForm : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

      

        User_Cotrol UserCont = new User_Cotrol();

         // public MainForm(string LoginNm)
        public MainForm()
        {
            InitializeComponent();

            //LoginPerson.Text = LoginNm;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void myCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       public void MenuColorChange()
       {
           inventory.BackColor = Color.WhiteSmoke;
           salToolStripMenuItem.BackColor = Color.WhiteSmoke;
           myBillToolStripMenuItem.BackColor = Color.WhiteSmoke;
           customerToolStripMenuItem.BackColor = Color.WhiteSmoke;
           vendersToolStripMenuItem.BackColor = Color.WhiteSmoke;
           adminToolStripMenuItem.BackColor = Color.WhiteSmoke;
       }

       public void PanelVisible()
       {
           PnlMyStock.Visible = false;
           PnlSales.Visible = false;
           PnlRepair.Visible = false;
           PnlCustomer.Visible = false;
           PnlVendor.Visible = false;
           PnlSettings.Visible = false;
       }

        private void inventory_Click(object sender, EventArgs e)
        {
            MenuColorChange();
            PanelVisible();

            inventory.BackColor = Color.LightSkyBlue;
            PnlMyStock.Visible = true;

            //data from Database
            StockItemLoad();
        }

        private void salToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange();
            PanelVisible();

            salToolStripMenuItem.BackColor = Color.LightSkyBlue;
            PnlSales.Visible = true;
        }

        private void myBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange();
            PanelVisible();

            myBillToolStripMenuItem.BackColor = Color.LightSkyBlue;
            PnlRepair.Visible = true;

        }

        public void CustomerDetailsLoad()
        {
            try
            {

                #region Customer Details load to the main page

                listViewCustomer.Items.Clear();

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT DISTINCT CustomerDetails.CusID, CustomerDetails.CusFirstName, CustomerDetails.CusLastName, CustomerDetails.CusPersonalAddress, 
                     CustomerDetails.CusMobileNumber,CustomerDetails.CusTelNUmber, CustomerDetails.CusEmailAddress, CustomerDetails.CusCreditLimit, 
                       CustomerDetails.CusRemarks,(SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = CustomerDetails.CusID) ORDER BY AutoNum DESC)
                      FROM CustomerDetails INNER JOIN
                      RegCusCredBalance ON CustomerDetails.CusID = RegCusCredBalance.CusID
                      WHERE CustomerDetails.CusActiveDeactive = '" + 1 + "' ORDER BY CustomerDetails.CusID ASC";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listViewCustomer.Items.Clear();

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());

                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());

                    //================================================

                    #region Add customer last credit balance----------------------------------------

                    //SqlConnection Conn = new SqlConnection(IMS);
                    //Conn.Open();

                    //string sql1 = "SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = '" + dr[0].ToString() + "') ORDER BY AutoNum DESC";
                    //SqlCommand cmd1x = new SqlCommand(sql1, Conn);
                    //SqlDataReader dr7 = cmd1x.ExecuteReader();

                    //if (dr7.Read())
                    //{
                    //    li.SubItems.Add(dr7[0].ToString());
                    //}

                    //if (Conn.State == ConnectionState.Open)
                    //{
                    //    Conn.Close();
                    //    dr7.Close();
                    //}
                    #endregion

                    //================================================
                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[8].ToString());



                    listViewCustomer.Items.Add(li);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                    dr.Close();
                }

                //count total Items in the batabase--------------------------------------------------------------------

                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();
                string CountVendors = "SELECT COUNT(CusID) FROM CustomerDetails WHERE CusActiveDeactive = " + 1 + "";
                SqlCommand cmd2 = new SqlCommand(CountVendors, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr2.Read() == true)
                {
                    TotalCustomer.Text = dr2[0].ToString();
                }

                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                    dr2.Close();
                }

                //-=--------------------------------------------------------------
                // add color
                for (int i = 0; i <= listViewCustomer.Items.Count - 1; i++)
                {
                    double availableCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[8].Text);
                    double CusCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[7].Text);


                    if (CusCredit > availableCredit && availableCredit > 0)
                    {
                        listViewCustomer.Items[i].BackColor = Color.LightCoral;
                    }

                    if (CusCredit == availableCredit)
                    {
                        listViewCustomer.Items[i].BackColor = Color.Red;
                    }


                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }



        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerDetailsLoad();
            
            
            MenuColorChange();
            PanelVisible();

            customerToolStripMenuItem.BackColor = Color.LightSkyBlue;
            PnlCustomer.Visible = true;
        }


        public void VenderDetailsLoad()
        {
            try
            {
                #region Vendor Details load to the main page

                listViewVendor.Items.Clear();

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = @"SELECT VenderID, VenderName, VenderPHAddress, VenderPTel, VenderPFax, VenderPEmail, CreditValue FROM VenderDetails
                                    WHERE ActiveDeactive ='" + 1 + "' ORDER BY VenderID ASC";

                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listViewVendor.Items.Clear();

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());

                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());



                    listViewVendor.Items.Add(li);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                    dr.Close();
                }

                //count total Items in the batabase--------------------------------------------------------------------

                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();
                string CountVendors = "SELECT COUNT(VenderID) FROM VenderDetails WHERE ActiveDeactive ='" + 1 + "'";
                SqlCommand cmd2 = new SqlCommand(CountVendors, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr2.Read() == true)
                {
                    TotalVendors.Text = dr2[0].ToString();
                }

                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                    dr2.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        private void vendersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VenderDetailsLoad();

            MenuColorChange();
            PanelVisible();

            vendersToolStripMenuItem.BackColor = Color.LightSkyBlue;
            PnlVendor.Visible = true;
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuColorChange();
            PanelVisible();

            adminToolStripMenuItem.BackColor =Color.LightSkyBlue;
            PnlSettings.Visible = true; 
        }

        public void StockItemLoad()
        {
            try
            {

                #region Stock Item load to the main page

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ItmSelectAll = @"SELECT NewItemDetails.ItmID, NewItemDetails.ItmName, NewItemDetails.ItmWarrenty, CurrentStock.AvailableStockCount, NewItemDetails.ItmSellPrice, 
                      NewItemDetails.ItmCategory, NewItemDetails.ItmManufacturer, NewItemDetails.ItmModel, NewItemDetails.ItmReoderLvl, NewItemDetails.ItmLocation, 
                      NewItemDetails.ItmDescription
                      FROM CurrentStock INNER JOIN
                      NewItemDetails ON CurrentStock.ItemID = NewItemDetails.ItmID WHERE NewItemDetails.ItmActiveOrNot='" + 1 + "' ORDER BY NewItemDetails.ItmID ASC";

                SqlCommand cmd1 = new SqlCommand(ItmSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listViewStock.Items.Clear();

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());

                    li.SubItems.Add(dr[1].ToString());

                    //warrenty period Calculate-----------------------------

                    string warranyPer = (dr[2].ToString());

                    string WarType = warranyPer.Substring(0, 1);
                    string WarPeriod = warranyPer.Substring(1);

                    if (WarType == "0")
                    {
                        li.SubItems.Add("No Warranty");
                    }

                    if (WarType == "1")
                    {
                        li.SubItems.Add(WarPeriod + " Year(s)");
                    }
                    if (WarType == "2")
                    {
                        li.SubItems.Add(WarPeriod + " Month(s)");
                    }

                    //------------------------------------------------------------
                    //------------------------------------------------------------
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());
                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[10].ToString());

                    listViewStock.Items.Add(li);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                    dr.Close();
                }

                //count total Items in the batabase--------------------------------------------------------------------

                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();
                string CountItems = "SELECT COUNT(ItmID) FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "'";
                SqlCommand cmd2 = new SqlCommand(CountItems, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr2.Read() == true)
                {
                    ItemCount.Text = dr2[0].ToString();
                }

                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                    dr2.Close();
                }

                //-------------------------------------------------------------------------------------------------------------

                for (int i = 0; i <= listViewStock.Items.Count - 1; i++)
                {
                    double availableCount = Convert.ToDouble(listViewStock.Items[i].SubItems[3].Text);
                    double ReorderLevel = Convert.ToDouble(listViewStock.Items[i].SubItems[8].Text);


                    if (availableCount < ReorderLevel)
                    {
                        listViewStock.Items[i].BackColor = Color.LightCoral;
                    }

                    if (availableCount == ReorderLevel)
                    {
                        listViewStock.Items[i].BackColor = Color.Khaki;
                    }

                    if (availableCount <= 0)
                    {
                        listViewStock.Items[i].BackColor = Color.Red;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Select_User_Settings()
        {
            try
            {
               // UserCont.User_Setting

                SqlDataReader dr = UserCont.User_Setting();
                if (dr.Read())
                {
                    #region File Menu----
                    //addnew user
                    if (dr[2].ToString() == "0")
                    {
                        addNewUserToolStripMenuItem.Enabled = false;
                    }
                    if (dr[2].ToString() == "1")
                    {
                        addNewUserToolStripMenuItem.Enabled = true;
                    }

                    //AddNewItem-----
                    if (dr[3].ToString() == "0")
                    {
                        addNewItemToolStripMenuItem1.Enabled = false;
                    }
                    if (dr[3].ToString() == "1")
                    {
                        addNewItemToolStripMenuItem1.Enabled = true;
                    }

                    //GRN-----
                    if (dr[4].ToString() == "0")
                    {
                        addNewItemToolStripMenuItem2.Enabled = false;
                    }
                    if (dr[4].ToString() == "1")
                    {
                        addNewItemToolStripMenuItem2.Enabled = true;
                    }

                    //Change Item Price
                    if (dr[5].ToString() == "0")
                    {
                        changeItemDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[5].ToString() == "1")
                    {
                        changeItemDetailsToolStripMenuItem.Enabled = true;
                    }

                    //BArcode
                    if (dr[6].ToString() == "0")
                    {
                        barCodeToolStripMenuItem.Enabled = false;
                    }
                    if (dr[6].ToString() == "1")
                    {
                        barCodeToolStripMenuItem.Enabled = true;
                    }

                    //new Invoice
                    if (dr[7].ToString() == "0")
                    {
                        newInvoiceToolStripMenuItem1.Enabled = false;
                    }
                    if (dr[7].ToString() == "1")
                    {
                        newInvoiceToolStripMenuItem1.Enabled = true;
                    }

                    //Re print invoice....
                    if (dr[8].ToString() == "0")
                    {
                        returnInvoiceToolStripMenuItem.Enabled = false;
                    }
                    if (dr[8].ToString() == "1")
                    {
                        returnInvoiceToolStripMenuItem.Enabled = true;
                    }

                    //repair Job
                    if (dr[9].ToString() == "0")
                    {
                        repairingJOBInvoiceToolStripMenuItem.Enabled = false;
                    }
                    if (dr[9].ToString() == "1")
                    {
                        repairingJOBInvoiceToolStripMenuItem.Enabled = true;
                    }

                    //repair Job  reprint 
                    if (dr[10].ToString() == "0")
                    {
                        reprintRepairingJOBInvoiceToolStripMenuItem.Enabled = false;
                    }
                    if (dr[10].ToString() == "1")
                    {
                        reprintRepairingJOBInvoiceToolStripMenuItem.Enabled = true;
                    }

                    //GRN Paymet details 
                    if (dr[11].ToString() == "0")
                    {
                        gRNPaymentDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[11].ToString() == "1")
                    {
                        gRNPaymentDetailsToolStripMenuItem.Enabled = true;
                    }

                    //Customer Credit payments
                    if (dr[12].ToString() == "0")
                    {
                        customerCreditPaymentDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[12].ToString() == "1")
                    {
                        customerCreditPaymentDetailsToolStripMenuItem.Enabled = true;
                    }

                    //CRepair JOB Add
                    if (dr[13].ToString() == "0")
                    {
                        takeInItemsToolStripMenuItem1.Enabled = false;
                    }
                    if (dr[13].ToString() == "1")
                    {
                        takeInItemsToolStripMenuItem1.Enabled = true;
                    }

                    //Repaird Items
                    if (dr[14].ToString() == "0")
                    {
                        repairedItemsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[14].ToString() == "1")
                    {
                        repairedItemsToolStripMenuItem.Enabled = true;
                    }

                    //JOB Payments add
                    if (dr[15].ToString() == "0")
                    {
                        jOBPaymentsAddToolStripMenuItem.Enabled = false;
                    }
                    if (dr[15].ToString() == "1")
                    {
                        jOBPaymentsAddToolStripMenuItem.Enabled = true;
                    }

                    //Add customer
                    if (dr[16].ToString() == "0")
                    {
                        addNewCustomerToolStripMenuItem.Enabled = false;
                    }
                    if (dr[16].ToString() == "1")
                    {
                        addNewCustomerToolStripMenuItem.Enabled = true;
                    }

                    //Add vendor
                    if (dr[17].ToString() == "0")
                    {
                        addNewVendorToolStripMenuItem.Enabled = false;
                    }
                    if (dr[17].ToString() == "1")
                    {
                        addNewVendorToolStripMenuItem.Enabled = true;
                    }

                    //Add petty cash
                    if (dr[18].ToString() == "0")
                    {
                        pettyCashToolStripMenuItem.Enabled = false;
                    }
                    if (dr[18].ToString() == "1")
                    {
                        pettyCashToolStripMenuItem.Enabled = true;
                    }


                    //Add Set OFF
                    if (dr[19].ToString() == "0")
                    {
                        setOFFCashToolStripMenuItem.Enabled = false;
                    }
                    if (dr[19].ToString() == "1")
                    {
                        setOFFCashToolStripMenuItem.Enabled = true;
                    }

                    //Add Set OFF
                    if (dr[20].ToString() == "0")
                    {
                        bankDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[20].ToString() == "1")
                    {
                        bankDetailsToolStripMenuItem.Enabled = true;
                    }

                    //bank Details--
                    if (dr[20].ToString() == "0")
                    {
                        bankDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[20].ToString() == "1")
                    {
                        bankDetailsToolStripMenuItem.Enabled = true;
                    }

                    //Rpt Item in the stock--
                    if (dr[21].ToString() == "0")
                    {
                        itemsInStockToolStripMenuItem.Enabled = false;
                    }
                    if (dr[21].ToString() == "1")
                    {
                        itemsInStockToolStripMenuItem.Enabled = true;
                    }

                    //Rpt Reorder Item in the stock--
                    if (dr[22].ToString() == "0")
                    {
                        reOrderItemsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[22].ToString() == "1")
                    {
                        reOrderItemsToolStripMenuItem.Enabled = true;
                    }

                    //Rpt Sales item--
                    if (dr[23].ToString() == "0")
                    {
                        salesDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[23].ToString() == "1")
                    {
                        salesDetailsToolStripMenuItem.Enabled = true;
                    }

                    //Rpt Payment Bal-
                    if (dr[24].ToString() == "0")
                    {
                        paymentBalanceReportToolStripMenuItem.Enabled = false;
                    }
                    if (dr[24].ToString() == "1")
                    {
                        paymentBalanceReportToolStripMenuItem.Enabled = true;
                    }


                    //Rpt Lost and proffit...
                    if (dr[25].ToString() == "0")
                    {
                        profitAndLostToolStripMenuItem.Enabled = false;
                    }
                    if (dr[25].ToString() == "1")
                    {
                        profitAndLostToolStripMenuItem.Enabled = true;
                    }


                    //view customer details...
                    if (dr[26].ToString() == "0")
                    {
                        viewCustomerDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[26].ToString() == "1")
                    {
                        viewCustomerDetailsToolStripMenuItem.Enabled = true;
                    }

                    //Rpt Vendor details...
                    if (dr[27].ToString() == "0")
                    {
                        viewVendorsDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[27].ToString() == "1")
                    {
                        viewVendorsDetailsToolStripMenuItem.Enabled = true;
                    }

                    //Rpt repairing pending details...
                    if (dr[28].ToString() == "0")
                    {
                        pendingRepairDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[28].ToString() == "1")
                    {
                        pendingRepairDetailsToolStripMenuItem.Enabled = true;
                    }

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    //User Settings...
                    if (dr[29].ToString() == "0")
                    {
                        myUserSettingsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[29].ToString() == "1")
                    {
                        myUserSettingsToolStripMenuItem.Enabled = true;
                    }


                    //Invoice Calcel...
                    if (dr[30].ToString() == "0")
                    {
                        invoiceReturnOrCancelToolStripMenuItem.Enabled = false;
                    }
                    if (dr[30].ToString() == "1")
                    {
                        invoiceReturnOrCancelToolStripMenuItem.Enabled = true;
                    }

                    //Bank Account Create...
                    if (dr[31].ToString() == "0")
                    {
                        bankAccountCreateToolStripMenuItem.Enabled = false;
                    }
                    if (dr[31].ToString() == "1")
                    {
                        bankAccountCreateToolStripMenuItem.Enabled = true;
                    }

                    //Database BAckup...
                    if (dr[32].ToString() == "0")
                    {
                        dataBaseBackupToolStripMenuItem1.Enabled = false;
                    }
                    if (dr[32].ToString() == "1")
                    {
                        dataBaseBackupToolStripMenuItem1.Enabled = true;
                    }

                    //petty cash rpt...
                    if (dr[33].ToString() == "0")
                    {
                        pettyCashToolStripMenuItem1.Enabled = false;
                    }
                    if (dr[33].ToString() == "1")
                    {
                        pettyCashToolStripMenuItem1.Enabled = true;
                    }

                    //Banking Details ...
                    if (dr[34].ToString() == "0")
                    {
                        bankingDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[34].ToString() == "1")
                    {
                        bankingDetailsToolStripMenuItem.Enabled = true;
                    }

                    //print details...
                    if (dr[35].ToString() == "0")
                    {
                        printingDetailsToolStripMenuItem.Enabled = false;
                    }
                    if (dr[35].ToString() == "1")
                    {
                        printingDetailsToolStripMenuItem.Enabled = true;
                       
                    }

                    if (dr[37].ToString() == "0")
                    {
                        gRNInvoiceCreditPaymentsFlowDetailsToolStripMenuItem.Enabled = false;
                       
                    }
                    if (dr[37].ToString() == "1")
                    {
                        gRNInvoiceCreditPaymentsFlowDetailsToolStripMenuItem.Enabled = true;
                    }
                  
                  

                    #endregion
                }

            }
            catch (Exception ex)
            { 
                
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            inventory.BackColor = Color.LightSkyBlue;
            PnlMyStock.Visible = true;

         //   toolStripStatusLabel3.Text = System.DateTime.Now.ToString();

            timer2.Start();

            LoginPerson.Text= UserDisplayName;
            LoginUserID.Text = UserID;
        //public string UserDisplayName = "";

            Select_User_Settings();

            try
            {

                StockItemLoad();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void addNewVendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVendor adVe = new AddVendor();
            FormStatus.isSubFormOpen = true;
           //this.Enabled = false;
            adVe.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!FormStatus.isSubFormOpen)
            {
                this.BringToFront();
                this.Enabled = true;
            }

           // toolStripStatusLabel3.Text = System.DateTime.Now.ToString();
        }

        private void addNewItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewItem nmItem = new NewItem();
            nmItem.Show();
        }

        private void addNewItemToolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
        }

        private void newInvoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Invoice inv = new Invoice(LoginPerson.Text);
            inv.LgDisplayName.Text = UserDisplayName;
            inv.LgUser.Text = UserID;
            inv.Show();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void addNewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerReg cusreg = new CustomerReg();
            cusreg.Visible = true;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = System.DateTime.Now.ToString();
        }

        private void listViewStock_ForeColorChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvw in listViewStock.Items)
            {
                if (Convert.ToDouble(lvw.SubItems[3]) <0)
                {
                    lvw.BackColor = Color.Red;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        { 
        
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {

            #region Stock Items serch by Item ID

            try
            {

            txtItemName.Text = "";

            listViewStock.Items.Clear();

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string ItmSelectAll = @"SELECT NewItemDetails.ItmID, NewItemDetails.ItmName, NewItemDetails.ItmWarrenty, CurrentStock.AvailableStockCount, NewItemDetails.ItmSellPrice, 
                      NewItemDetails.ItmCategory, NewItemDetails.ItmManufacturer, NewItemDetails.ItmModel, NewItemDetails.ItmReoderLvl, NewItemDetails.ItmLocation, 
                      NewItemDetails.ItmDescription
                      FROM CurrentStock INNER JOIN
                      NewItemDetails ON CurrentStock.ItemID = NewItemDetails.ItmID WHERE NewItemDetails.ItmActiveOrNot='" + 1 + "' AND NewItemDetails.ItmID LIKE '%" + textBox6.Text + "%' ORDER BY NewItemDetails.ItmID ASC";

            SqlCommand cmd1 = new SqlCommand(ItmSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            //dataGridView2.Rows.Clear();

            while (dr.Read() == true)
            {
                ListViewItem li;

                li = new ListViewItem(dr[0].ToString());

                li.SubItems.Add(dr[1].ToString());

                //warrenty period Calculate-----------------------------

                string warranyPer = (dr[2].ToString());

                string WarType = warranyPer.Substring(0, 1);
                string WarPeriod = warranyPer.Substring(1);

                if (WarType == "0")
                {
                    li.SubItems.Add("No Warranty");
                }

                if (WarType == "1")
                {
                    li.SubItems.Add(WarPeriod + " Year(s)");
                }
                if (WarType == "2")
                {
                    li.SubItems.Add(WarPeriod + " Month(s)");
                }

                //------------------------------------------------------------
                //------------------------------------------------------------
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());
                li.SubItems.Add(dr[7].ToString());
                li.SubItems.Add(dr[8].ToString());
                li.SubItems.Add(dr[9].ToString());
                li.SubItems.Add(dr[10].ToString());

                listViewStock.Items.Add(li);

            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
                dr.Close();
            }

            //count total Items in the batabase--------------------------------------------------------------------

            SqlConnection con2 = new SqlConnection(IMS);
            con2.Open();
            string CountItems = "SELECT COUNT(ItmID) FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "'";
            SqlCommand cmd2 = new SqlCommand(CountItems, con2);
            SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr2.Read() == true)
            {
                ItemCount.Text = dr2[0].ToString();
            }

            if (con2.State == ConnectionState.Open)
            {
                con2.Close();
                dr2.Close();
            }

            //-------------------------------------------------------------------------------------------------------------

            for (int i = 0; i <= listViewStock.Items.Count - 1; i++)
            {
                double availableCount = Convert.ToDouble(listViewStock.Items[i].SubItems[3].Text);
                double ReorderLevel = Convert.ToDouble(listViewStock.Items[i].SubItems[8].Text);


                if (availableCount < ReorderLevel)
                {
                    listViewStock.Items[i].BackColor = Color.LightCoral;
                }

                if (availableCount == ReorderLevel)
                {
                    listViewStock.Items[i].BackColor = Color.Khaki;
                }

                if (availableCount <= 0)
                {
                    listViewStock.Items[i].BackColor = Color.Red;
                }
            }

             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            #region Stock Items serch by Item Name

            try{

            textBox6.Text = "";

            listViewStock.Items.Clear();

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string ItmSelectAll = @"SELECT NewItemDetails.ItmID, NewItemDetails.ItmName, NewItemDetails.ItmWarrenty, CurrentStock.AvailableStockCount, NewItemDetails.ItmSellPrice, 
                      NewItemDetails.ItmCategory, NewItemDetails.ItmManufacturer, NewItemDetails.ItmModel, NewItemDetails.ItmReoderLvl, NewItemDetails.ItmLocation, 
                      NewItemDetails.ItmDescription
                      FROM CurrentStock INNER JOIN
                      NewItemDetails ON CurrentStock.ItemID = NewItemDetails.ItmID WHERE NewItemDetails.ItmActiveOrNot='" + 1 + "' AND NewItemDetails.ItmName LIKE '%" + txtItemName.Text + "%' ORDER BY NewItemDetails.ItmID ASC";

            SqlCommand cmd1 = new SqlCommand(ItmSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            //dataGridView2.Rows.Clear();

            while (dr.Read() == true)
            {
                ListViewItem li;

                li = new ListViewItem(dr[0].ToString());

                li.SubItems.Add(dr[1].ToString());

                //warrenty period Calculate-----------------------------

                string warranyPer = (dr[2].ToString());

                string WarType = warranyPer.Substring(0, 1);
                string WarPeriod = warranyPer.Substring(1);

                if (WarType == "0")
                {
                    li.SubItems.Add("No Warranty");
                }

                if (WarType == "1")
                {
                    li.SubItems.Add(WarPeriod + " Year(s)");
                }
                if (WarType == "2")
                {
                    li.SubItems.Add(WarPeriod + " Month(s)");
                }

                //------------------------------------------------------------
                //------------------------------------------------------------
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());
                li.SubItems.Add(dr[7].ToString());
                li.SubItems.Add(dr[8].ToString());
                li.SubItems.Add(dr[9].ToString());
                li.SubItems.Add(dr[10].ToString());

                listViewStock.Items.Add(li);

            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
                dr.Close();
            }

            //count total Items in the batabase--------------------------------------------------------------------

            SqlConnection con2 = new SqlConnection(IMS);
            con2.Open();
            string CountItems = "SELECT COUNT(ItmID) FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "'";
            SqlCommand cmd2 = new SqlCommand(CountItems, con2);
            SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr2.Read() == true)
            {
                ItemCount.Text = dr2[0].ToString();
            }

            if (con2.State == ConnectionState.Open)
            {
                con2.Close();
                dr2.Close();
            }

            //-------------------------------------------------------------------------------------------------------------

            for (int i = 0; i <= listViewStock.Items.Count - 1; i++)
            {
                double availableCount = Convert.ToDouble(listViewStock.Items[i].SubItems[3].Text);
                double ReorderLevel = Convert.ToDouble(listViewStock.Items[i].SubItems[8].Text);


                if (availableCount < ReorderLevel)
                {
                    listViewStock.Items[i].BackColor = Color.LightCoral;
                }

                if (availableCount == ReorderLevel)
                {
                    listViewStock.Items[i].BackColor = Color.Khaki;
                }

                if (availableCount <= 0)
                {
                    listViewStock.Items[i].BackColor = Color.Red;
                }
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void label12_Click(object sender, EventArgs e)
        {
            NewItem NwItem = new NewItem();
            NwItem.Show();
        }

        private void inventory_MouseHover(object sender, EventArgs e)
        {

        }

        


        private void label12_MouseHover(object sender, EventArgs e)
        {


            label12.ForeColor = Color.Blue;
            label12.Font = new Font(label12.Font.Name, label12.Font.SizeInPoints, FontStyle.Underline);
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.Black;
            label12.Font = new Font(label12.Font.Name, label12.Font.SizeInPoints, FontStyle.Regular);
        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

            #region Vendor Details serch by ID

                //textBox7.Text = "";

            listViewVendor.Items.Clear();

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string VenSelectAll = @"SELECT VenderID, VenderName, VenderPHAddress, VenderPTel, VenderPFax, VenderPEmail, CreditValue FROM VenderDetails
                                    WHERE ActiveDeactive ='" + 1 + "' AND (VenderID LIKE '%" + textBox8.Text + "%' OR VenderName LIKE '%" + textBox8.Text + "%' OR VenderPTel LIKE '%" + textBox8.Text + "%') ORDER BY VenderID ASC";

            SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            listViewVendor.Items.Clear();

            while (dr.Read() == true)
            {
                ListViewItem li;

                li = new ListViewItem(dr[0].ToString());

                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());
                //li.SubItems.Add(dr[7].ToString());


                listViewVendor.Items.Add(li);

            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
                dr.Close();
            }
            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
       }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void updateVenLink_MouseHover(object sender, EventArgs e)
        {
            updateVenLink.ForeColor = Color.Blue;
            updateVenLink.Font = new Font(updateVenLink.Font.Name, updateVenLink.Font.SizeInPoints, FontStyle.Underline);
        }

        private void updateVenLink_MouseLeave(object sender, EventArgs e)
        {

            updateVenLink.ForeColor = Color.Black;
            updateVenLink.Font = new Font(updateVenLink.Font.Name, updateVenLink.Font.SizeInPoints, FontStyle.Regular);
        }

        private void updateVenLink_Click(object sender, EventArgs e)
        {
            AddVendor ven = new AddVendor();
            ven.Show();
        }

        private void CusID_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                #region Customer Details load to the main page

                // CusfirstName.Text = "";
                //CusMobile.Text = "";

                listViewCustomer.Items.Clear();

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT DISTINCT CustomerDetails.CusID, CustomerDetails.CusFirstName, CustomerDetails.CusLastName, CustomerDetails.CusPersonalAddress, 
                                    CustomerDetails.CusMobileNumber,CustomerDetails.CusTelNUmber, CustomerDetails.CusEmailAddress, CustomerDetails.CusCreditLimit, 
                                    CustomerDetails.CusRemarks,(SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = CustomerDetails.CusID) ORDER BY AutoNum DESC)
                                    FROM CustomerDetails INNER JOIN
                                    RegCusCredBalance ON CustomerDetails.CusID = RegCusCredBalance.CusID
                                    WHERE CustomerDetails.CusActiveDeactive = '1'  AND (CustomerDetails.CusID LIKE '%" + CusID.Text + "%' OR CustomerDetails.CusFirstName LIKE '%" + CusID.Text + "%' OR CustomerDetails.CusMobileNumber LIKE '%" + CusID.Text + "%') ORDER BY CustomerDetails.CusID ASC";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                // listViewCustomer.Items.Clear();

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());

                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());

                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[8].ToString());
                    // li.SubItems.Add(dr[10].ToString());


                    listViewCustomer.Items.Add(li);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                    dr.Close();
                }

                //count total Items in the batabase--------------------------------------------------------------------

                SqlConnection con2 = new SqlConnection(IMS);
                con2.Open();
                string CountVendors = "SELECT COUNT(CusID) FROM CustomerDetails WHERE CusActiveDeactive = " + 1 + "";
                SqlCommand cmd2 = new SqlCommand(CountVendors, con2);
                SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr2.Read() == true)
                {
                    TotalCustomer.Text = dr2[0].ToString();
                }

                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                    dr2.Close();
                }

                //-=--------------------------------------------------------------
                // add color
                for (int i = 0; i <= listViewCustomer.Items.Count - 1; i++)
                {
                    double availableCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[8].Text);
                    double CusCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[7].Text);


                    if (CusCredit > availableCredit && availableCredit > 0)
                    {
                        listViewCustomer.Items[i].BackColor = Color.LightCoral;
                    }

                    if (CusCredit == availableCredit)
                    {
                        listViewCustomer.Items[i].BackColor = Color.Red;
                    }


                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void CusID_TextChanged(object sender, EventArgs e)
        {

        }

        private void CusfirstName_KeyUp(object sender, KeyEventArgs e)
        {
           #region Customer Details search by Cus First Name
  

//            CusID.Text = "";
//            CusMobile.Text = "";

//            listViewCustomer.Items.Clear();

//            SqlConnection con1 = new SqlConnection(IMS);
//            con1.Open();

//            string CusSelectAll = @"SELECT CustomerDetails.CusID, CustomerDetails.CusFirstName, CustomerDetails.CusLastName, CustomerDetails.CusCompanyName, CustomerDetails.CusPersonalAddress, 
//                      CustomerDetails.CusMobileNumber,CustomerDetails.CusTelNUmber, CustomerDetails.CusEmailAddress, CustomerDetails.CusCreditLimit, 
//                      RegCusCredBalance.CreditBalance, CustomerDetails.CusRemarks
//                      FROM CustomerDetails INNER JOIN
//                      RegCusCredBalance ON CustomerDetails.CusID = RegCusCredBalance.CusID
//                      WHERE CustomerDetails.CusActiveDeactive = '" + 1 + "' AND CustomerDetails.CusFirstName LIKE '%" + CusfirstName.Text + "%' ORDER BY CustomerDetails.CusFirstName ASC";

//            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
//            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

//            // listViewCustomer.Items.Clear();

//            while (dr.Read() == true)
//            {
//                ListViewItem li;

//                li = new ListViewItem(dr[0].ToString());

//                li.SubItems.Add(dr[1].ToString());
//                li.SubItems.Add(dr[2].ToString());
//                li.SubItems.Add(dr[3].ToString());
//                li.SubItems.Add(dr[4].ToString());
//                li.SubItems.Add(dr[5].ToString());
//                li.SubItems.Add(dr[6].ToString());
//                li.SubItems.Add(dr[7].ToString());
//                li.SubItems.Add(dr[8].ToString());
//                li.SubItems.Add(dr[9].ToString());
//                li.SubItems.Add(dr[10].ToString());


//                listViewCustomer.Items.Add(li);

//            }

//            if (con1.State == ConnectionState.Open)
//            {
//                con1.Close();
//                dr.Close();
//            }

//            //count total Items in the batabase--------------------------------------------------------------------

//            SqlConnection con2 = new SqlConnection(IMS);
//            con2.Open();
//            string CountVendors = "SELECT COUNT(CusID) FROM CustomerDetails WHERE CusActiveDeactive = " + 1 + "";
//            SqlCommand cmd2 = new SqlCommand(CountVendors, con2);
//            SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

//            if (dr2.Read() == true)
//            {
//                TotalCustomer.Text = dr2[0].ToString();
//            }

//            if (con2.State == ConnectionState.Open)
//            {
//                con2.Close();
//                dr2.Close();
//            }

//            //-=--------------------------------------------------------------
//            // add color
//            for (int i = 0; i <= listViewCustomer.Items.Count - 1; i++)
//            {
//                double availableCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[9].Text);
//                double CusCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[8].Text);


//                if (CusCredit > availableCredit && availableCredit > 0)
//                {
//                    listViewCustomer.Items[i].BackColor = Color.LightCoral;
//                }

//                if (CusCredit == availableCredit)
//                {
//                    listViewCustomer.Items[i].BackColor = Color.Red;
//                }


//            }

           #endregion
        }

        private void CusMobile_KeyUp(object sender, KeyEventArgs e)
        {
            #region Customer Details search by Cus First Name


//            CusID.Text = "";
//            CusfirstName.Text = "";

//            listViewCustomer.Items.Clear();

//            SqlConnection con1 = new SqlConnection(IMS);
//            con1.Open();

//            string CusSelectAll = @"SELECT CustomerDetails.CusID, CustomerDetails.CusFirstName, CustomerDetails.CusLastName, CustomerDetails.CusCompanyName, CustomerDetails.CusPersonalAddress, 
//                       CustomerDetails.CusMobileNumber,CustomerDetails.CusTelNUmber, CustomerDetails.CusEmailAddress, CustomerDetails.CusCreditLimit, 
//                      RegCusCredBalance.CreditBalance, CustomerDetails.CusRemarks
//                      FROM CustomerDetails INNER JOIN
//                      RegCusCredBalance ON CustomerDetails.CusID = RegCusCredBalance.CusID
//                      WHERE CustomerDetails.CusActiveDeactive = '" + 1 + "' AND CustomerDetails.CusMobileNumber LIKE '%" + CusMobile.Text + "%' ORDER BY CustomerDetails.CusMobileNumber ASC";

//            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
//            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

//            // listViewCustomer.Items.Clear();

//            while (dr.Read() == true)
//            {
//                ListViewItem li;

//                li = new ListViewItem(dr[0].ToString());

//                li.SubItems.Add(dr[1].ToString());
//                li.SubItems.Add(dr[2].ToString());
//                li.SubItems.Add(dr[3].ToString());
//                li.SubItems.Add(dr[4].ToString());
//                li.SubItems.Add(dr[5].ToString());
//                li.SubItems.Add(dr[6].ToString());
//                li.SubItems.Add(dr[7].ToString());
//                li.SubItems.Add(dr[8].ToString());
//                li.SubItems.Add(dr[9].ToString());
//                li.SubItems.Add(dr[10].ToString());


//                listViewCustomer.Items.Add(li);

//            }

//            if (con1.State == ConnectionState.Open)
//            {
//                con1.Close();
//                dr.Close();
//            }

//            //count total Items in the batabase--------------------------------------------------------------------

//            SqlConnection con2 = new SqlConnection(IMS);
//            con2.Open();
//            string CountVendors = "SELECT COUNT(CusID) FROM CustomerDetails WHERE CusActiveDeactive = " + 1 + "";
//            SqlCommand cmd2 = new SqlCommand(CountVendors, con2);
//            SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

//            if (dr2.Read() == true)
//            {
//                TotalCustomer.Text = dr2[0].ToString();
//            }

//            if (con2.State == ConnectionState.Open)
//            {
//                con2.Close();
//                dr2.Close();
//            }

//            //-=--------------------------------------------------------------
//            // add color
//            for (int i = 0; i <= listViewCustomer.Items.Count - 1; i++)
//            {
//                double availableCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[9].Text);
//                double CusCredit = Convert.ToDouble(listViewCustomer.Items[i].SubItems[8].Text);


//                if (CusCredit > availableCredit && availableCredit > 0)
//                {
//                    listViewCustomer.Items[i].BackColor = Color.LightCoral;
//                }

//                if (CusCredit == availableCredit)
//                {
//                    listViewCustomer.Items[i].BackColor = Color.Red;
//                }


//            }

            #endregion
        }

        private void label69_MouseHover(object sender, EventArgs e)
        {
            label69.ForeColor = Color.Blue;
            label69.Font = new Font(label69.Font.Name, label69.Font.SizeInPoints, FontStyle.Underline);
        }

        private void label69_MouseLeave(object sender, EventArgs e)
        {
            label69.ForeColor = Color.Black;
            label69.Font = new Font(label69.Font.Name, label69.Font.SizeInPoints, FontStyle.Regular);
        }

        private void label69_Click(object sender, EventArgs e)
        {
            CustomerReg cus = new CustomerReg();
            cus.Show();
        }

        private void salesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void itemsInStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void reOrderItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReorderItemDetails RO = new ReorderItemDetails();
            RO.Show();
        }

        private void paymentBalanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInvoicePayments InP = new FrmInvoicePayments();
            InP.Show();
        }

        private void takeInItemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RepairJOB rn = new RepairJOB();
            rn.UserDisplayName = UserDisplayName;
            rn.UserID = UserID;
            rn.Show();
        }

        private void returnInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReprintInvoice rp = new ReprintInvoice();
            rp.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void repairingJOBInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepairJOBPayments repairPay = new RepairJOBPayments();
            repairPay.LgDisplayName.Text = LoginPerson.Text;
            repairPay.LgUser.Text = LoginUserID.Text;

            repairPay.Show();
        }

        private void myUserSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //user settings

            UserSetting repairPay = new UserSetting();
            repairPay.LgDisplayName.Text = LoginPerson.Text;
            repairPay.LgUser.Text = LoginUserID.Text;

            repairPay.Show();
        }

        private void repairedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepairSolution rps = new RepairSolution();

            rps.UserDisplayName = LoginPerson.Text;
            rps.UserID = LoginUserID.Text;

            rps.Visible = true;
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reprintRepairingJOBInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JobInvoiceReprint jpb = new JobInvoiceReprint();
           
            jpb.Visible = true;
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserProfile up = new UserProfile();
            up.LgDisplayName.Text = UserDisplayName;
            up.LgUser.Text = UserID;
            up.Visible = true;
        }

        private void gRNItemViceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveOrders reo = new ReceiveOrders(LoginPerson.Text);
            reo.LgDisplayName.Text = UserDisplayName;
            reo.LgUser.Text = UserID;
            reo.Show();
        }

        private void gRNWholesalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GRN_WholeSaleItem reo = new GRN_WholeSaleItem();

            reo.LgDisplayName.Text = UserDisplayName;
            reo.LgUser.Text = UserID;
            reo.Show();
        }

        private void jOBPaymentsAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepairJOBPaymentEnter jbp = new RepairJOBPaymentEnter();

            jbp.LgDisplayName.Text = UserDisplayName;
            jbp.LgUser.Text = UserID;

            jbp.Show();
        }

        private void changeItemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //change_Discount_Price

                change_Discount_Price jbp = new change_Discount_Price();

                     jbp.LgDisplayName.Text = UserDisplayName;
                        jbp.LgUser.Text = UserID;

                    jbp.Show();
        }

        private void pendingRepairDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paymentCompletedNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //completed payment reports 
            try
            {

                RptRepairCompletedIssue jbp = new RptRepairCompletedIssue();

                jbp.LgDisplayName.Text = UserDisplayName;
                jbp.LgUser.Text = UserID;
                jbp.PrintDocumetType = "Completed_Rep";

                jbp.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void pendingRepairingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pending repairings

            RptPendingRepairs jbp = new RptPendingRepairs();
            jbp.Show();
        }

        private void barCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Barcode_builder_For_GRNs brgen = new Barcode_builder_For_GRNs();
                brgen.LgDisplayName.Text = UserDisplayName;
                brgen.LgUser.Text = UserID;
                brgen.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void gRNPaymentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GRN MAymet Details adding


            GRN_Payment_details jbp = new GRN_Payment_details();

            jbp.LgDisplayName.Text = UserDisplayName;
            jbp.LgUser.Text = UserID;
            jbp.Show();
        }

        private void pettyCashToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //pettyCashForm
            pettyCashForm petych = new pettyCashForm();

            petych.LgDisplayName.Text = UserDisplayName;
            petych.LgUser.Text = UserID;

            petych.Show();
        }

        private void customerCreditPaymentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //custoemr credit payment details

            Customer_Credit_Payments cuscrP = new Customer_Credit_Payments();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();

        }

        private void setOFFCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set off Cash-------------
            try
            {

                SET_OFF cuscrP = new SET_OFF();

                cuscrP.LgDisplayName.Text = UserDisplayName;
                cuscrP.LgUser.Text = UserID;

                cuscrP.Show();
            }
            catch (Exception ex)
            {
            }
        }

        private void profitAndLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Profit and lost-------------
            try
            {

                Profit_AND_Lost cuscrP = new Profit_AND_Lost();

                cuscrP.LgDisplayName.Text = UserDisplayName;
                cuscrP.LgUser.Text = UserID;

                cuscrP.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void bankDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Bank_Details

            Deposit_Details cuscrP = new Deposit_Details();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void PnlSettings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void viewCustomerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewVendorsDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm lg = new LoginForm();
            lg.Show();

            this.Hide();
           
        }

        private void invoiceReturnOrCancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //invoice Return......................
            Return_Invoice cuscrP = new Return_Invoice();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();

        }

        private void bankAccountCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new bank account......................
            BankNameRegister cuscrP = new BankNameRegister();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void gRNReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GRN Return......................
            GRNReturn cuscrP = new GRNReturn();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void dataBaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dataBaseBackupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //DB Backup......................
            DataBase_Backups cuscrP = new DataBase_Backups();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void pettyCashToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void gRNReturnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //GRN Return......................
            GRNReturn cuscrP = new GRNReturn();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;
            

            cuscrP.Show();
        }

        private void issedPettyCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Issued Petty cash......................
            Frm_Petty_cash cuscrP = new Frm_Petty_cash();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void pettyCashFlowWithCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Issued Petty cash......................
            Rpt_Petty_Cash_Flow cuscrP = new Rpt_Petty_Cash_Flow();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void bankBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Issued Petty cash......................
            Frm_Bank_Balance cuscrP = new Frm_Bank_Balance();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void completedRepairedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void photoCopyPrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //photocopy......................
            Photocopy cuscrP = new Photocopy();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PurchaseOrder......................
            PurchaseOrder cuscrP = new PurchaseOrder();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void printingDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frm_Photocopy_Report......................
            Frm_Photocopy_Report cuscrP = new Frm_Photocopy_Report();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void soldInvoiceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RptInvoiceDetails rptinv = new RptInvoiceDetails();
            rptinv.Show();
        }

        private void itemViseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rpt Item Wise Sale Report......................
            rptItemWiseSaleReport cuscrP = new rptItemWiseSaleReport();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();

          //  rptItemWiseSaleReport dfg = new rptItemWiseSaleReport();
          //  dfg.Visible = true;
        }

        private void availableStockCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmItemsInTheStock avStock = new FrmItemsInTheStock();
            avStock.Show();
        }

        private void availableStockValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rpt Item Wise Sale Report......................
            FrmCurrentStock cuscrP = new FrmCurrentStock();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void viewCustomerDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Frm_Customer_Details........................

            Frm_Customer_Details cuscrP = new Frm_Customer_Details();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void customerCreditPaymentDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Frm_Customer_credit paymetns........................

            frmcustermorCreditCheck cuscrP = new frmcustermorCreditCheck();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void viewSuppliersDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Vender Details................................

            Frm_Vender_Details cuscrP = new Frm_Vender_Details();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void suppliersCreditPaymentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Vender Details................................

            frmCheckVenderCredit cuscrP = new frmCheckVenderCredit();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void gRNInvoiceCreditPaymentsFlowDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // GRN/ Invoice Credit Payments flow details................................

            FRMcredit_Payment_Balance_details cuscrP = new FRMcredit_Payment_Balance_details();

            cuscrP.LgDisplayName.Text = UserDisplayName;
            cuscrP.LgUser.Text = UserID;

            cuscrP.Show();
        }

        private void aboutIMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.Show();
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
           
            

            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
              DateTime DT = DateTime.Now;

              this.toolStripStatusLabel3.Text = DT.ToString();
        }

        private void visitingRepairingNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visiting_RepairingNote jbp = new Visiting_RepairingNote();

            jbp.LgDisplayName.Text = UserDisplayName;
            jbp.LgUser.Text = UserID;

            jbp.Show();
        }

        private void quotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quotation_form Quta = new Quotation_form();

            Quta.LgDisplayName.Text = UserDisplayName;
            Quta.LgUser.Text = UserID;

            Quta.Show();
        }
       
        
    }
}
