using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;


namespace Inventory_Control_System
{
    public partial class RepairJOB : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserID = "";
        public string UserDisplayName = "";

        public RepairJOB()
        {
            InitializeComponent();
             //Fillcombo();
        }

        public void Ck_ListV_Repairing_Item_Count()
        {
            if (listVReItem.Items.Count == 0)
            {
                btnaultDetails.Enabled = false;
            }

            if (listVReItem.Items.Count != 0)
            {
                btnaultDetails.Enabled = true;
            }
        }

        void mouseEnter(Panel pnl, Label lblText)
        {
            pnl.BackColor = Color.FromArgb(201, 35, 35);
            lblText.ForeColor = Color.WhiteSmoke;
        }

        void mouseLeave(Panel pnl, Label lblText)
        {
            pnl.BackColor = Color.WhiteSmoke;
            lblText.ForeColor = Color.FromArgb(0, 0, 0);
        }

        public void commonColorSelectItems()
        {

            //invoice
            btnInvoice.BackColor= Color.WhiteSmoke;
            lblInvoice.ForeColor = Color.FromArgb(0, 0, 0);

            //Item Vice
            btnItem.BackColor = Color.WhiteSmoke;
            lblItem.ForeColor = Color.FromArgb(0, 0, 0);

            //Other
            btnOther.BackColor = Color.WhiteSmoke;
            lblOther.ForeColor = Color.FromArgb(0, 0, 0);

        }

        public void commonColorSelectItemsRight()
        {

            //Item Details
            BtnItenDetals.BackColor = Color.WhiteSmoke;
            lblItemDetails.ForeColor = Color.FromArgb(0, 0, 0);

            //Fault Details
            btnaultDetails.BackColor = Color.WhiteSmoke;
            LblFaultDetails.ForeColor = Color.FromArgb(0, 0, 0);
        }

        public void HidePanelLeft()
        {
            pnlInvoiceDetails.Visible = false;
            PnlItemDetails.Visible = false;
            PnlNewItemRepair.Visible = false;
            
        }

        public void HidePanelRight()
        {
            PnlAddedItemDetails.Visible = false;
            PnlFault.Visible = false;
        }

        //Load CustomerID
         public void slectCus()
        {
          //  cmbCustID.Items.Clear();

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = "SELECT CusID FROM CustomerDetails WHERE CusActiveDeactive='1'";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                cmbCustID.Items.Add(dr[0]);
            }
            cmd1.Dispose();
            dr.Close();

            if (con1.State == ConnectionState.Open)
            {

                con1.Close();
            }
         }

        private void RepairJOB_Load(object sender, EventArgs e)
        {
            //display LgU
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;

            LoginForm lg = new LoginForm();

            Ck_ListV_Repairing_Item_Count();

            //LgUser.Text = lgf.UserID;

            rbtnNEW.Checked = true;
            rbtnUpdate.Checked = false;
            rbtnUpdate.Enabled = false;

            commonColorSelectItems();

            desebleall();
            mouseEnter(btnInvoice, lblInvoice);
            mouseEnter(BtnItenDetals, lblItemDetails);
            //dsable textbx.................
            txtCusNameFaul.Enabled = false;
            txtCusAddfault.Enabled = false;
            TelNumFault.Enabled = false;
            TelNumFault.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtCusName.Enabled = false;
            txtAddress.Enabled = false;
            txtInvoBy.Enabled = false;
            InvoDate.Enabled = false;
            textBox6.Enabled = false;
            txtInvoicedBy.Enabled = false;
            txtseAddr.Enabled = false;
            txtinvoNo.Enabled = false;
            custorName.Enabled = false;
            txtBarcodesearch.Enabled = false;
            GenerateJOBNumbe();
        }

         

//================================================================================================================================================
        public void clearListview()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listVReItem.Items.Clear();
        }

        public void ClearTxt()
        {
            #region clear text as default
            ReKeyB.Text = "No";
            ReUSB.Text = "No";
            RePowerCbl.Text = "No";
            txtreOther.Text = "No";

            cmbCustID.Text = "";
            txtCusNameFaul.Text = "";
            TelNumFault.Text = "";
            txtCusAddfault.Text = "";
            TelNumFault.Text = "";
            FaultOther.Text = "";
            cmbAuthPerson.Text = "";
            //cmbCustID.ResetText();

            // ReWarranty.Text="";
            //cmbJobType.Enabled = true;
            //cmbAuthPerson.Enabled = false;



            //clear check boes
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;


            txtCusName.Clear();
            txtinvoNo.Clear();
            txtAddress.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            // listVReItem.Items.Clear();
            txtbarcode.Clear();
            txtItemName.Clear();
            txtInvoicedBy.Clear();
            txtInvoicedBy.Clear();
            txtInvoiceNo.Clear();
            ReCompletingDate.ResetText();
            ReCompletingTime.ResetText();
            FaultOther.Clear();
            textBox1.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox6.Clear();
            txtAddress.Clear();
            txtbarcode.Clear();
            txtBarcodesearch.Clear();
            txtCusName.Clear();
            txtInvoBy.Clear();
            txtInvoicedBy.Clear();
            txtinvoNo.Clear();
            txtseAddr.Clear();

            //enable all list views
            listVReItem.Enabled = true;
            listView3.Enabled = true;
            listView2.Enabled = true;

            PnlItemDetails.Enabled = true;
            pnlInvoiceDetails.Enabled = true;
            PnlNewItemRepair.Enabled = true;
            PnlFault.Enabled = true;
            PnlAddedItemDetails.Enabled = true;


            #endregion

        }


//close panal====================================================================================================================================
        public void closePanel()
        {
            PnlIinvoiceSearch.Visible = false;
            PnlJobSearch1.Visible = false;
            PnlCustomerSerch.Visible = false;
            
        }


        private void reset()
        {
            //display LgU
            LgDisplayName.Text = UserDisplayName;
            LgUser.Text = UserID;

            LoginForm lg = new LoginForm();

            Ck_ListV_Repairing_Item_Count();

            //LgUser.Text = lgf.UserID;

            rbtnNEW.Checked = true;
            rbtnUpdate.Checked = false;
            rbtnUpdate.Enabled = false;

            commonColorSelectItems();

            desebleall();
            mouseEnter(btnInvoice, lblInvoice);
            mouseEnter(BtnItenDetals, lblItemDetails);
            //dsable textbx.................
            txtCusNameFaul.Enabled = false;
            txtCusAddfault.Enabled = false;
            TelNumFault.Enabled = false;
            TelNumFault.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtCusName.Enabled = false;
            txtAddress.Enabled = false;
            txtInvoBy.Enabled = false;
            InvoDate.Enabled = false;
            textBox6.Enabled = false;
            txtInvoicedBy.Enabled = false;
            txtseAddr.Enabled = false;
            txtinvoNo.Enabled = false;
            custorName.Enabled = false;
            txtBarcodesearch.Enabled = false;
            GenerateJOBNumbe();
        }


        private void btnInvoice_MouseEnter(object sender, EventArgs e)
        {
            //if (btnInvoice.BackColor != Color.FromArgb(201, 35, 35) && lblInvoice.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnInvoice, lblInvoice);
            //}
        }

        private void lblInvoice_MouseEnter(object sender, EventArgs e)
        {
            //if (btnInvoice.BackColor != Color.FromArgb(201, 35, 35) && lblInvoice.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnInvoice, lblInvoice);
            //}
        }

        private void btnInvoice_MouseLeave(object sender, EventArgs e)
        {

            // if (btnInvoice.BackColor != Color.FromArgb(201, 35, 35) && lblInvoice.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(btnInvoice, lblInvoice);
            //}
            
        }



        private void btnItem_MouseEnter(object sender, EventArgs e)
        {
            //if (btnItem.BackColor != Color.FromArgb(201, 35, 35) && lblItem.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnItem, lblItem);
            //}

            //mouseEnter(btnItem, lblItem);
        }

        private void lblInvoice_MouseLeave(object sender, EventArgs e)
        {
            //if (btnInvoice.BackColor != Color.FromArgb(201, 35, 35) && lblInvoice.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(btnInvoice, lblInvoice);
            //}

            
        }

        private void lblItem_MouseEnter(object sender, EventArgs e)
        {
        //    if (btnItem.BackColor != Color.FromArgb(201, 35, 35) && lblItem.ForeColor != Color.WhiteSmoke)
        //    {
        //        mouseEnter(btnItem, lblItem);
        //    }
        }

        private void btnItem_MouseLeave(object sender, EventArgs e)
        {
            //if (btnItem.BackColor != Color.FromArgb(201, 35, 35) && lblItem.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(btnItem, lblItem);
            //}

            //mouseLeave(btnItem, lblItem);
        }

        private void lblItem_MouseLeave(object sender, EventArgs e)
        {
            //if (btnItem.BackColor != Color.FromArgb(201, 35, 35) && lblItem.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(btnItem, lblItem);
            //}

            
        }

        private void btnInvoice_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnItem_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void lblInvoice_Click(object sender, EventArgs e)
        {
            commonColorSelectItems();

            mouseEnter(btnInvoice, lblInvoice);

            HidePanelLeft();

            pnlInvoiceDetails.Visible = true;
        }

        private void lblItem_Click(object sender, EventArgs e)
        {
            commonColorSelectItems();

            mouseEnter(btnItem, lblItem);
            HidePanelLeft();

            PnlItemDetails.Visible = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOther_MouseEnter(object sender, EventArgs e)
        {
            //if (btnOther.BackColor != Color.FromArgb(201, 35, 35) && lblOther.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnOther, lblOther);
            //}
        }

        private void lblOther_MouseEnter(object sender, EventArgs e)
        {
            //if (btnOther.BackColor != Color.FromArgb(201, 35, 35) && lblOther.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnOther, lblOther);
            //}
        }

        private void btnOther_MouseLeave(object sender, EventArgs e)
        {
            //if (btnOther.BackColor != Color.FromArgb(201, 35, 35) && lblOther.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(btnOther, lblOther);
            //}
        }

        private void lblOther_MouseLeave(object sender, EventArgs e)
        {
            //if (btnOther.BackColor != Color.FromArgb(201, 35, 35) && lblOther.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(btnOther, lblOther);
            //}
        }

        private void BtnItenDetals_MouseEnter(object sender, EventArgs e)
        {
            //if (btnOther.BackColor != Color.FromArgb(201, 35, 35) && lblOther.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(BtnItenDetals, lblItemDetails);
            //}
        }

        private void lblItemDetails_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void BtnItenDetals_MouseLeave(object sender, EventArgs e)
        {
            //if (BtnItenDetals.BackColor != Color.FromArgb(201, 35, 35) && lblItemDetails.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseLeave(BtnItenDetals, lblItemDetails);
            //}
        }

        private void lblItemDetails_MouseEnter(object sender, EventArgs e)
        {
            //if (btnOther.BackColor != Color.FromArgb(201, 35, 35) && lblOther.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(BtnItenDetals, lblItemDetails);
            //}
        }

        private void lblItemDetails_MouseLeave(object sender, EventArgs e)
        {
            //if (BtnItenDetals.BackColor != Color.FromArgb(201, 35, 35) && lblItemDetails.ForeColor != Color.WhiteSmoke)
            //{
               // mouseLeave(BtnItenDetals, lblItemDetails);
            //}
        }

        private void btnaultDetails_MouseEnter(object sender, EventArgs e)
        {
            //if (btnaultDetails.BackColor != Color.FromArgb(201, 35, 35) && LblFaultDetails.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnaultDetails, LblFaultDetails);
            //}
        }

        private void LblFaultDetails_MouseEnter(object sender, EventArgs e)
        {
            //if (btnaultDetails.BackColor != Color.FromArgb(201, 35, 35) && LblFaultDetails.ForeColor != Color.WhiteSmoke)
            //{
            //    mouseEnter(btnaultDetails, LblFaultDetails);
            //}
        }

        private void LblFaultDetails_MouseLeave(object sender, EventArgs e)
        {
            ////if (btnaultDetails.BackColor != Color.FromArgb(201, 35, 35) && LblFaultDetails.ForeColor != Color.WhiteSmoke)
            ////{
            //    mouseLeave(btnaultDetails, LblFaultDetails);
            ////}
        }

        private void btnaultDetails_MouseLeave(object sender, EventArgs e)
        {
        //    //if (btnaultDetails.BackColor != Color.FromArgb(201, 35, 35) && LblFaultDetails.ForeColor != Color.WhiteSmoke)
        //    //{
        //        mouseLeave(btnaultDetails, LblFaultDetails);
        //    //}
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            commonColorSelectItems();

            mouseEnter(btnInvoice, lblInvoice);
            HidePanelLeft();

            pnlInvoiceDetails.Visible = true;
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            commonColorSelectItems();

            mouseEnter(btnItem, lblItem);
            HidePanelLeft();

            PnlItemDetails.Visible = true;
        }

        private void lblOther_Click(object sender, EventArgs e)
        {
            commonColorSelectItems();

            mouseEnter(btnOther, lblOther);
            HidePanelLeft();

            PnlNewItemRepair.Visible = true;
            txtItemName.Focus();
        }

        private void btnOther_Click(object sender, EventArgs e)
        {

            commonColorSelectItems();

            mouseEnter(btnOther, lblOther);
            HidePanelLeft();


            PnlNewItemRepair.Visible = true;
        }

        private void BtnItenDetals_Click(object sender, EventArgs e)
        {

            commonColorSelectItemsRight();

            mouseEnter(BtnItenDetals, lblItemDetails);
            HidePanelRight();

            PnlAddedItemDetails.Visible = true;
        }

       
        

        private void lblItemDetails_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsRight();

            mouseEnter(BtnItenDetals, lblItemDetails);
            HidePanelRight();

            PnlAddedItemDetails.Visible = true;

            slectCus();
        }

        private void LblFaultDetails_Click(object sender, EventArgs e)
        {
            commonColorSelectItemsRight();

            mouseEnter(btnaultDetails, LblFaultDetails);
            HidePanelRight();

            PnlFault.Visible = true;

            try
            {
                if (checkButton1.Checked == true)
                {
                    #region only Visit Customer details load ...............................................................

                    SqlConnection cnn1 = new SqlConnection(IMS);
                    cnn1.Open();
                    String LoadCusID1 = "select CusID from Repair_VisitNote ";
                    SqlCommand cmm1 = new SqlCommand(LoadCusID1, cnn1);
                    SqlDataReader dr1 = cmm1.ExecuteReader();
                    while (dr1.Read())
                    {
                        cmbCustID.Items.Add(dr1[0].ToString());
                    }

                    cnn1.Close();


                    DataGridViewRow dr2 = dtagridSearchprevious.SelectedRows[0];
                    SqlConnection cnn = new SqlConnection(IMS);
                    cnn.Open();
                    String LoadCusID = "select CusID,CusName,CusAddre,Custel from Repair_VisitNote where Visit_ID='" + dr2.Cells[0].Value.ToString() + "'";
                    SqlCommand cmm = new SqlCommand(LoadCusID, cnn);
                    SqlDataReader dr = cmm.ExecuteReader();


                    if (dr.Read())
                    {
                        cmbCustID.Text = dr[0].ToString();
                        //  MessageBox.Show(cmbCustID.Text);
                        txtCusNameFaul.Text = dr[1].ToString();
                        txtCusAddfault.Text = dr[2].ToString();
                        TelNumFault.Text = dr[3].ToString();

                        // MessageBox.Show(LoadCusID);
                    }
                    #endregion
                }
                else
                {
                    slectCus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            


        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PnlIinvoiceSearch.Visible = true;

            try
            {

            SqlConnection ser1 = new SqlConnection(IMS);
            ser1.Open();

            string viewgrideviewInRepairJob="SELECT InvoiceNo,CusStatus,CusFirstName,CusPersonalAddress,CreatedBy FROM SoldInvoiceDetails";
            SqlCommand cmd1 = new SqlCommand(viewgrideviewInRepairJob, ser1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridView2.Rows.Clear();

            while (dr.Read()==true)
            {
                dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);

            }

             if (ser1.State == ConnectionState.Open)
            {
                ser1.Close();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }



        }

        private void button8_Click(object sender, EventArgs e)
        {
            PnlIinvoiceSearch.Visible = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView2.SelectedRows[0];

                string InvNum="";

                InvNum = dr.Cells[0].Value.ToString();

                txtInvoiceNo.Text=InvNum ;
                
                txtCusName.Text = dr.Cells[2].Value.ToString();
                txtAddress.Text = dr.Cells[3].Value.ToString();
                txtInvoBy.Text = dr.Cells[4].Value.ToString();

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                {
                    string listviewInRepairJob = @"SELECT  SoldItemDetails.ItemID, SoldItemDetails.ItemName, SoldItemDetails.BarcodeID, 
                                                SoldItemDetails.ItemWarrenty, InvoicePaymentDetails.InvoiceDate, SoldInvoiceDetails.CusStatus, SoldInvoiceDetails.CreatedBy, SoldInvoiceDetails.InvoiceRemark
                                                 FROM InvoiceCheckDetails FULL OUTER JOIN
                                                 InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                                                 SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                                                 SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo WHERE InvoicePaymentDetails.InvoiceID='"+InvNum+"'";

                    SqlCommand cmd1 = new SqlCommand(listviewInRepairJob, con);
                    SqlDataReader sa = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                   
                    listView1.Items.Clear();

                    //string InvDate = "";
                    

                    while (sa.Read() == true)
                    {
                        ListViewItem li;

                        li = new ListViewItem(sa[0].ToString());


                        li.SubItems.Add(sa[1].ToString());
                        li.SubItems.Add(sa[4].ToString());
                        li.SubItems.Add(sa[3].ToString());
                        li.SubItems.Add(sa[2].ToString());
                        li.SubItems.Add("Invoiced");
                        li.SubItems.Add(InvNum);
                        li.SubItems.Add(sa[5].ToString());
                        li.SubItems.Add(sa[6].ToString());
                        li.SubItems.Add(sa[7].ToString());

                        listView1.Items.Add(li);

                        InvoDate.Text = sa[4].ToString();
                        PnlIinvoiceSearch.Hide();
                       
                    }

                    
                   
                  
                }




                //PnlIinvoiceSearch.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        
        
        
        
        
        
      //Invoice search

        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
               
                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string viewgrideviewInRepairJob = "SELECT InvoiceNo,CusStatus,CusFirstName,CusPersonalAddress,CreatedBy FROM SoldInvoiceDetails WHERE invoiceNo  LIKE '%" + textBox19.Text + "%' OR CusFirstName LIKE '%" + textBox19.Text + "%' OR CusStatus LIKE'%"+ textBox19.Text+"%' ";
            SqlCommand cmd1 = new SqlCommand(viewgrideviewInRepairJob, con);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridView2.Rows.Clear();
            // string VenSelectAll = "SELECT ItmID,ItmName,ItmMark02,ItmDisc03,ItmMark03 FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "' AND ItmID LIKE '" + textBox4.Text+ "%'";
            while (dr.Read()==true)
            {
                dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);

            }

             if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }

        public void AddtoAllListView()
        {
            #region AddtoAllListView1
            try
            {

                ListViewItem li;

                li = new ListViewItem(listView1.SelectedItems[0].SubItems[0].Text);

                li.SubItems.Add(listView1.SelectedItems[0].SubItems[1].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[2].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[3].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[4].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[5].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[6].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[7].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[8].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[9].Text);

                listVReItem.Items.Add(li);

                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            #region check dupplicate value in listview1
            try
            {
                

                if (listVReItem.Items.Count != 0 || listVReItem.Items.Count == 0)
                {
                    for (int i = 0; i <= listVReItem.Items.Count - 1; i++)
                    {

               if (listVReItem.Items[i].SubItems[0].Text == listView1.SelectedItems[0].SubItems[0].Text && listVReItem.Items[i].SubItems[2].Text == listView1.SelectedItems[0].SubItems[2].Text && listVReItem.Items[i].SubItems[3].Text == listView1.SelectedItems[0].SubItems[3].Text && listVReItem.Items[i].SubItems[4].Text == listView1.SelectedItems[0].SubItems[4].Text && listVReItem.Items[i].SubItems[5].Text == listView1.SelectedItems[0].SubItems[5].Text && listVReItem.Items[i].SubItems[6].Text == listView1.SelectedItems[0].SubItems[6].Text)
                        {
                            MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                    AddtoAllListView();

                    Ck_ListV_Repairing_Item_Count();
                }
            }

             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             }
            #endregion
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            pllSerchSerialNo.Visible = true;

            txtSerialNo.Focus();

            

            try
            {

                SqlConnection ser1 = new SqlConnection(IMS);
                ser1.Open();

                string viewgrideviewInRepairJob = @"SELECT     SoldItemDetails.ItemID, SoldItemDetails.ItemName, InvoicePaymentDetails.InvoiceDate, SoldItemDetails.ItemWarrenty, SoldItemDetails.BarcodeID, 
                                                InvoicePaymentDetails.InvoiceID, SoldInvoiceDetails.CusStatus, SoldInvoiceDetails.CreatedBy, SoldInvoiceDetails.InvoiceRemark, SoldInvoiceDetails.CusFirstName, 
                                                SoldInvoiceDetails.CusPersonalAddress, SoldItemDetails.SoldID, SoldItemDetails.SystemID
                                                FROM         InvoiceCheckDetails FULL OUTER JOIN
                                                InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                                                SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                                                SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                                                WHERE     (SoldItemDetails.ItemID LIKE 'ITM%')";

//                string viewgrideviewInRepairJob = @"SELECT    SoldItemDetails.BarcodeID, SoldItemDetails.ItemName, SoldInvoiceDetails.InvoiceNo, 
//                                                  SoldInvoiceDetails.CusStatus, SoldInvoiceDetails.CusFirstName, SoldInvoiceDetails.CusPersonalAddress, SoldInvoiceDetails.CreatedBy
//                                                 FROM InvoiceCheckDetails FULL OUTER JOIN
//                                                 InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
//                                                 SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
//                                                 SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo";


                SqlCommand cmd1 = new SqlCommand(viewgrideviewInRepairJob, ser1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                //string no = "";
                //string printType = "";

                while (dr.Read() == true)
                {
                    
                    //no = dr[5].ToString();

                    //string InvoicedType = no.Substring(0, 3);

                    //if (InvoicedType == "INV")
                    //{
                    //    printType = "Invoiced";
                    //}

                    //if (InvoicedType == "BRW")
                    //{
                    //    printType = "Barrowed";
                    //}

                    dataGridView1.Rows.Add(dr[4],dr[12], dr[1], dr[5], dr[6], dr[9], dr[10], dr[7], dr[0], dr[2], dr[3], "Invoiced", dr[8], dr[11]); 
                  
                     //no = "";
                     //printType = "";

                }
               

                if (ser1.State == ConnectionState.Open)
                {
                    ser1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();


                string viewgrideviewInRepairJob = @"SELECT     SoldItemDetails.ItemID, SoldItemDetails.ItemName, InvoicePaymentDetails.InvoiceDate, SoldItemDetails.ItemWarrenty, SoldItemDetails.BarcodeID, 
                                                InvoicePaymentDetails.InvoiceID, SoldInvoiceDetails.CusStatus, SoldInvoiceDetails.CreatedBy, SoldInvoiceDetails.InvoiceRemark, SoldInvoiceDetails.CusFirstName, 
                                                SoldInvoiceDetails.CusPersonalAddress, SoldItemDetails.SoldID, SoldItemDetails.SystemID
                                                FROM         InvoiceCheckDetails FULL OUTER JOIN
                                                InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                                                SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                                                SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo
                                                WHERE     (SoldItemDetails.ItemID LIKE 'ITM%') AND (SoldItemDetails.BarcodeID LIKE'%" + txtSerialNo.Text + "%' OR SoldItemDetails.SystemID LIKE'%" + txtSerialNo.Text + "%')";

                SqlCommand cmd1 = new SqlCommand(viewgrideviewInRepairJob, con);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[4], dr[12],dr[1], dr[5], dr[6], dr[9], dr[10], dr[7], dr[0], dr[2], dr[3], "Invoiced", dr[8], dr[11]);
                    



                }
               

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
               
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];

            string serialNo = "";

            serialNo = dr.Cells[0].Value.ToString();
            txtBarcodesearch.Text = serialNo;

            txtinvoNo.Text = dr.Cells[3].Value.ToString();
            custorName.Text = dr.Cells[5].Value.ToString();
            txtseAddr.Text = dr.Cells[6].Value.ToString();
            txtInvoicedBy.Text = dr.Cells[7].Value.ToString();





            SqlConnection con = new SqlConnection(IMS);
            con.Open();
            {
                string insertlistview = @"SELECT  SoldItemDetails.ItemID, SoldItemDetails.ItemName, InvoicePaymentDetails.InvoiceDate,SoldItemDetails.ItemWarrenty,SoldItemDetails.BarcodeID, 
                                                             InvoicePaymentDetails.InvoiceID, SoldInvoiceDetails.CusStatus, SoldInvoiceDetails.CreatedBy, SoldInvoiceDetails.InvoiceRemark,SoldItemDetails.SoldID
                                                             FROM InvoiceCheckDetails FULL OUTER JOIN
                                                             InvoicePaymentDetails ON InvoiceCheckDetails.InvoiceID = InvoicePaymentDetails.InvoiceID FULL OUTER JOIN
                                                             SoldItemDetails ON InvoicePaymentDetails.InvoiceID = SoldItemDetails.InvoiceID FULL OUTER JOIN
                                                             SoldInvoiceDetails ON InvoicePaymentDetails.InvoiceID = SoldInvoiceDetails.InvoiceNo WHERE SoldItemDetails.SoldID='" + dr.Cells[13].Value.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(insertlistview, con);

                SqlDataReader sa = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                //listView2.Items.Clear();

                while (sa.Read() == true)
                {
                   

                    ListViewItem li;
                    li = new ListViewItem(sa[0].ToString());
                    li.SubItems.Add(sa[1].ToString());
                    li.SubItems.Add(sa[2].ToString());
                    li.SubItems.Add(sa[3].ToString());
                    li.SubItems.Add(sa[4].ToString());
                    li.SubItems.Add("Invoiced");
                    li.SubItems.Add(sa[5].ToString());
                    li.SubItems.Add(sa[6].ToString());
                    li.SubItems.Add(sa[7].ToString());
                    li.SubItems.Add(sa[8].ToString());
                    // li.SubItems.Add(sa[9].ToString());
                    // li.SubItems.Add(sa[1].ToString());


                    listView2.Items.Add(li);
                   textBox6.Text = sa[2].ToString();
                   txtSerialNo.Focus();
                }
            }


           // MessageBox.Show("Added", "Message");

            this.txtSerialNo.Focus();



        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        public void AddtoAllListView2()
        {
            #region AddtoAllListView2
            try
            {
                ListViewItem li;
                li = new ListViewItem(listView2.SelectedItems[0].SubItems[0].Text);




                li.SubItems.Add(listView2.SelectedItems[0].SubItems[1].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[2].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[3].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[4].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[5].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[6].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[7].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[8].Text);
                li.SubItems.Add(listView2.SelectedItems[0].SubItems[9].Text);


                listVReItem.Items.Add(li);

                Ck_ListV_Repairing_Item_Count();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            #region check duplicate value in listview2
            try
            {
                if (listVReItem.Items.Count != 0 || listVReItem.Items.Count == 0)
                {
                    for (int i = 0; i <= listVReItem.Items.Count - 1; i++)
                    {

                        if (listVReItem.Items[i].SubItems[0].Text == listView2.SelectedItems[0].SubItems[0].Text && listVReItem.Items[i].SubItems[2].Text == listView2.SelectedItems[0].SubItems[2].Text && listVReItem.Items[i].SubItems[3].Text == listView2.SelectedItems[0].SubItems[3].Text && listVReItem.Items[i].SubItems[4].Text == listView2.SelectedItems[0].SubItems[4].Text && listVReItem.Items[i].SubItems[5].Text == listView2.SelectedItems[0].SubItems[5].Text && listVReItem.Items[i].SubItems[6].Text == listView2.SelectedItems[0].SubItems[6].Text)
                        {
                            MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                    AddtoAllListView2();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pllSerchSerialNo.Visible = false;
        }
//listview data remove---------------------------------
        private void listVReItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listVReItem.SelectedItems[0].Remove();

            Ck_ListV_Repairing_Item_Count();
        }
//select & viewlist other form value
        private void NewRepairAdd_Click(object sender, EventArgs e)
        {
            if(cmbWarrantyStatus.SelectedIndex==-1)
            {
                MessageBox.Show("Please Select Warranty Status","Message");
                cmbWarrantyStatus.DroppedDown=true;
                cmbWarrantyStatus.Focus();
                return;

            }

            if (txtItemName.Text == "")
            {
                  MessageBox.Show("Please complete Item Name", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  txtItemName.Focus();
                return;
            }

            if (txtbarcode.Text == "")
            {
                MessageBox.Show("Please complete Barcode", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbarcode.Focus();
                return;
            }

            try
            {
                    ListViewItem li = new ListViewItem("NO_Code");

                    li.SubItems.Add(txtItemName.Text);
                    li.SubItems.Add("1900-01-01 00:00:33.337");
                    li.SubItems.Add(LblNoWarranty.Text);
                    li.SubItems.Add(txtbarcode.Text);
                    li.SubItems.Add("Other Item");
                    li.SubItems.Add("Not_Barrow");
                    li.SubItems.Add("Other_Customer");
                    li.SubItems.Add("Other");
                    li.SubItems.Add("Other");

                    listView3.Items.Add(li);

                    txtItemName.Text = "";
                    txtbarcode.Text = "";
                    cmbWarrantyStatus.SelectedIndex = -1;
                
                    txtItemName.Focus();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void w(bool p)
        {
            throw new NotImplementedException();
        }

        private void btnOther_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void listView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        public void AddtoAllListView3()
        {
            #region AddtoAllListView3
            try
            {

                ListViewItem li;
                li = new ListViewItem(listView3.SelectedItems[0].SubItems[0].Text);




                li.SubItems.Add(listView3.SelectedItems[0].SubItems[1].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[2].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[3].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[4].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[5].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[6].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[7].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[8].Text);
                li.SubItems.Add(listView3.SelectedItems[0].SubItems[9].Text);


                listVReItem.Items.Add(li);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            #region check dupplicate value in listview3
            try
            {

                if (listVReItem.Items.Count != 0 || listVReItem.Items.Count == 0)
                {
                    for (int i = 0; i <= listVReItem.Items.Count - 1; i++)
                    {

                        if (listVReItem.Items[i].SubItems[0].Text == listView3.SelectedItems[0].SubItems[0].Text && listVReItem.Items[i].SubItems[2].Text == listView3.SelectedItems[0].SubItems[2].Text && listVReItem.Items[i].SubItems[3].Text == listView3.SelectedItems[0].SubItems[3].Text && listVReItem.Items[i].SubItems[4].Text == listView3.SelectedItems[0].SubItems[4].Text && listVReItem.Items[i].SubItems[5].Text == listView3.SelectedItems[0].SubItems[5].Text && listVReItem.Items[i].SubItems[6].Text == listView3.SelectedItems[0].SubItems[6].Text)
                        {
                            MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                    AddtoAllListView3();

                    Ck_ListV_Repairing_Item_Count();
                   
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void PnlFault_Paint(object sender, PaintEventArgs e)
        {
            
        
        }
        string tot;

        public void FaultSelect()
        {
            #region faultselection Binary order =tot

            string a, b, c, d, f, g, h, i, j, k;

            if (checkBox1.Checked == true)
            {
                a = "1";
            }
            else
            {
                a = "0";
            }

            if (checkBox2.Checked == true)
            {
                b = "1";
            }
            else
            {
                b = "0";
            }

            if (checkBox3.Checked == true)
            {
                c = "1";
            }
            else
            {
                c = "0";
            }
            if (checkBox4.Checked == true)
            {
                d = "1";
            }
            else
            {
                d = "0";
            }
            if (checkBox5.Checked == true)
            {
                f = "1";
            }
            else
            {
                f = "0";
            }
            if (checkBox6.Checked == true)
            {
                g = "1";
            }
            else
            {
                g = "0";
            }
            if (checkBox7.Checked == true)
            {
                h = "1";
            }
            else
            {
                h = "0";
            }
            if (checkBox8.Checked == true)
            {
                i = "1";
            }
            else
            {
                i = "0";

            }
            if (checkBox9.Checked == true)
            {
                j = "1";
            }
            else
            {
                j = "0";
            }
            if (checkBox10.Checked == true)
            {
                k = "1";
            }
            else
            {
                k = "0";
            }

            tot = a + b + c + d + f + g + h + i + j + k;
            //MessageBox.Show("message correct", "fult");

            #endregion
        }


//insert fault value-------------------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {

            if (txtCusNameFaul.Text == "" || txtCusAddfault.Text == "" || TelNumFault.Text == "")
            {
                MessageBox.Show("Please complete customer Details", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //PnlAddress.Visible = true;
                txtCusNameFaul.Focus();
                return;
            }
            if (listVReItem.Items.Count== 0)
            {
                MessageBox.Show("Please Add at least Details", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                HidePanelRight();
                PnlAddedItemDetails.Visible = true;

                commonColorSelectItemsRight();

                mouseEnter(BtnItenDetals, lblItemDetails);
                


                PnlAddedItemDetails.BringToFront();
                //PnlAddress.Visible = true;
               // txtCusNameFaul.Focus();
                return;
            }

            if(rbtnNEW.Checked==false && rbtnUpdate.Checked==false && checkButton1.Checked==false )
            {
                MessageBox.Show("Please Select New OR Update OR View Visit Details", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            try
            {
                DialogResult result = MessageBox.Show("Are you whether you need to complete the invoice?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (checkButton1.Checked == true)
                    {
                        #region Update visit Note Details...........................................

                        SqlConnection cnnPre = new SqlConnection(IMS);
                        cnnPre.Open();
                        String SaveVisitDetails = "update Repair_VisitNote set AddedRepairNotes='" + ReJobNumber.Text + "' where  Visit_ID='" + lblPreviousVisitID.Text + "'";
                        SqlCommand cmmpre = new SqlCommand(SaveVisitDetails, cnnPre);
                        cmmpre.ExecuteNonQuery();
                        #endregion

                        if (rbtnNEW.Checked == true)
                        {

                            #region insert Data to database

                            FaultSelect();
                            GenerateJOBNumbe();

                            SqlConnection con1 = new SqlConnection(IMS);
                            con1.Open();
                            //MessageBox.Show("Message", "Message");
                            string CusIntert = @"INSERT INTO RepairNotes (ReJobNumber, PaymentStatus,PaymentDetails, JobStatus,LgUser,Tecnician, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, FaultStatus, FaultOther, 
                     RePowerCbl,ExtraCoolingFans ,ReUSB, ReSystemOther, ReCompletingDate, ReCompletingTime,JobType,Solution,TimeStamp,Updated) VALUES 
                    ('" + ReJobNumber.Text + "','" + cmbJobType.Text + "','Pending','1','" + LgUser.Text + "','" + cmbAuthPerson.Text + "','" + cmbCustID.Text + "','" + txtCusNameFaul.Text + "','" + txtCusAddfault.Text + "','" + TelNumFault.Text + "','" + tot + "','" + FaultOther.Text + "','" + RePowerCbl.Text + "','" + ReKeyB.Text + "','" + ReUSB.Text + "','" + txtreOther.Text + "','" + ReCompletingDate.Text + "','" + ReCompletingTime.Text + "','" + cmbJobType.Text + "','1','" + System.DateTime.Now.ToString() + "','0')";



                            SqlCommand cmd1 = new SqlCommand(CusIntert, con1);
                            cmd1.ExecuteNonQuery();

                            if (con1.State == ConnectionState.Open)
                            {
                                con1.Close();
                            }

                            //======================================================================================================
                            //add itemsto the db
                            //item save in listview==========================================================================================================
                            int i;
                            for (i = 0; i <= listVReItem.Items.Count - 1; i++)
                            {
                                SqlConnection con = new SqlConnection(IMS);
                                con.Open();
                                string InsertItmDetailsInvoice = "INSERT INTO RepairNoteItems(ReJobNumber,ItemID,ItemName,PurchesDate,Warrany,BarcodeSerial,ItemStatus,InvoceBarrowID,CustomerID,InvoicedBy,RemarkItem) VALUES(@ReJobNumber,@ItemID,@ItemName,@PurchesDate,@Warrany,@BarcodeSerial,@ItemStatus,@InvoceBarrowID,@CustomerID,@InvoicedBy,@RemarkItem)";
                                SqlCommand cmd = new SqlCommand(InsertItmDetailsInvoice, con);

                                //Convert.ToDateTime("0000-00-00").ToLongDateString()
                                cmd.Parameters.AddWithValue("ReJobNumber", ReJobNumber.Text);
                                cmd.Parameters.AddWithValue("ItemID", listVReItem.Items[i].SubItems[0].Text);
                                cmd.Parameters.AddWithValue("ItemName", listVReItem.Items[i].SubItems[1].Text);
                                cmd.Parameters.AddWithValue("PurchesDate", listVReItem.Items[i].SubItems[2].Text);
                                cmd.Parameters.AddWithValue("Warrany", listVReItem.Items[i].SubItems[3].Text);
                                cmd.Parameters.AddWithValue("BarcodeSerial", listVReItem.Items[i].SubItems[4].Text);

                                cmd.Parameters.AddWithValue("ItemStatus", listVReItem.Items[i].SubItems[5].Text);
                                cmd.Parameters.AddWithValue("InvoceBarrowID", listVReItem.Items[i].SubItems[6].Text);
                                cmd.Parameters.AddWithValue("CustomerID", listVReItem.Items[i].SubItems[7].Text);
                                cmd.Parameters.AddWithValue("InvoicedBy", listVReItem.Items[i].SubItems[8].Text);
                                cmd.Parameters.AddWithValue("RemarkItem", listVReItem.Items[i].SubItems[9].Text);

                                cmd.ExecuteNonQuery();



                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                            }



                            RptRepairJobAddingFrm rptfrm = new RptRepairJobAddingFrm();
                            rptfrm.PrintingJOBNumber = ReJobNumber.Text;
                            rptfrm.PrintCopyDetails = "Original copy";

                            rptfrm.Visible = true;

                            ClearTx();
                           
                           // btnaultDetails.Enabled = false;
                            PnlAddedItemDetails.Visible = true;
                            this.BtnItenDetals_Click(sender,e);
                          //  BtnItenDetals.Enabled = false;
                           // checkButton1.Checked = false;

                            Ck_ListV_Repairing_Item_Count();
                            checkButton1.Checked = false;
                            

                            GenerateJOBNumbe();

                            #endregion
                        }

                    }
                    else
                    {

                        if (rbtnNEW.Checked == true)
                        {

                            #region insert Data to database

                            FaultSelect();
                            GenerateJOBNumbe();

                            SqlConnection con1 = new SqlConnection(IMS);
                            con1.Open();
                            //MessageBox.Show("Message", "Message");
                            string CusIntert = @"INSERT INTO RepairNotes (ReJobNumber, PaymentStatus,PaymentDetails, JobStatus,LgUser,Tecnician, ReCusID, CusFirstName, CusPersonalAddress, CusTelNUmber, FaultStatus, FaultOther, 
                     RePowerCbl,ExtraCoolingFans ,ReUSB, ReSystemOther, ReCompletingDate, ReCompletingTime,JobType,Solution,TimeStamp,Updated) VALUES 
                    ('" + ReJobNumber.Text + "','" + cmbJobType.Text + "','Pending','1','" + LgUser.Text + "','" + cmbAuthPerson.Text + "','" + cmbCustID.Text + "','" + txtCusNameFaul.Text + "','" + txtCusAddfault.Text + "','" + TelNumFault.Text + "','" + tot + "','" + FaultOther.Text + "','" + RePowerCbl.Text + "','" + ReKeyB.Text + "','" + ReUSB.Text + "','" + txtreOther.Text + "','" + ReCompletingDate.Text + "','" + ReCompletingTime.Text + "','" + cmbJobType.Text + "','1','" + System.DateTime.Now.ToString() + "','0')";



                            SqlCommand cmd1 = new SqlCommand(CusIntert, con1);
                            cmd1.ExecuteNonQuery();

                            if (con1.State == ConnectionState.Open)
                            {
                                con1.Close();
                            }

                            //======================================================================================================
                            //add itemsto the db
                            //item save in listview==========================================================================================================
                            int i;
                            for (i = 0; i <= listVReItem.Items.Count - 1; i++)
                            {
                                SqlConnection con = new SqlConnection(IMS);
                                con.Open();
                                string InsertItmDetailsInvoice = "INSERT INTO RepairNoteItems(ReJobNumber,ItemID,ItemName,PurchesDate,Warrany,BarcodeSerial,ItemStatus,InvoceBarrowID,CustomerID,InvoicedBy,RemarkItem) VALUES(@ReJobNumber,@ItemID,@ItemName,@PurchesDate,@Warrany,@BarcodeSerial,@ItemStatus,@InvoceBarrowID,@CustomerID,@InvoicedBy,@RemarkItem)";
                                SqlCommand cmd = new SqlCommand(InsertItmDetailsInvoice, con);

                                //Convert.ToDateTime("0000-00-00").ToLongDateString()
                                cmd.Parameters.AddWithValue("ReJobNumber", ReJobNumber.Text);
                                cmd.Parameters.AddWithValue("ItemID", listVReItem.Items[i].SubItems[0].Text);
                                cmd.Parameters.AddWithValue("ItemName", listVReItem.Items[i].SubItems[1].Text);
                                cmd.Parameters.AddWithValue("PurchesDate", listVReItem.Items[i].SubItems[2].Text);
                                cmd.Parameters.AddWithValue("Warrany", listVReItem.Items[i].SubItems[3].Text);
                                cmd.Parameters.AddWithValue("BarcodeSerial", listVReItem.Items[i].SubItems[4].Text);

                                cmd.Parameters.AddWithValue("ItemStatus", listVReItem.Items[i].SubItems[5].Text);
                                cmd.Parameters.AddWithValue("InvoceBarrowID", listVReItem.Items[i].SubItems[6].Text);
                                cmd.Parameters.AddWithValue("CustomerID", listVReItem.Items[i].SubItems[7].Text);
                                cmd.Parameters.AddWithValue("InvoicedBy", listVReItem.Items[i].SubItems[8].Text);
                                cmd.Parameters.AddWithValue("RemarkItem", listVReItem.Items[i].SubItems[9].Text);

                                cmd.ExecuteNonQuery();



                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                            }



                            RptRepairJobAddingFrm rptfrm = new RptRepairJobAddingFrm();
                            rptfrm.PrintingJOBNumber = ReJobNumber.Text;
                            rptfrm.PrintCopyDetails = "Original copy";

                            rptfrm.Visible = true;

                            ClearTx();


                            PnlAddedItemDetails.Visible = true;
                            this.BtnItenDetals_Click(sender, e);


                            Ck_ListV_Repairing_Item_Count();
                            checkButton1.Checked = false;

                            GenerateJOBNumbe();

                            #endregion
                        }

                        if (rbtnUpdate.Checked == true)
                        {
                            #region Update Repair JOB details

                            //delet list view details===================================================================================================================
                            SqlConnection con1 = new SqlConnection(IMS);
                            con1.Open();

                            string delete = "DELETE from RepairNoteItems  WHERE ReJobNumber='" + ReJobNumber.Text + "'  ";
                            SqlCommand cmd3 = new SqlCommand(delete, con1);
                            cmd3.ExecuteNonQuery();

                            if (con1.State == ConnectionState.Open)
                            {
                                con1.Close();
                            }

                            //insert list view details===========================================================================================================================
                            foreach (ListViewItem li in listVReItem.Items)
                            {
                                SqlConnection con4 = new SqlConnection(IMS);
                                con4.Open();
                                string InsertItmDetails = "INSERT INTO RepairNoteItems(ReJobNumber,ItemID,ItemName,PurchesDate,Warrany,BarcodeSerial,ItemStatus,InvoceBarrowID,CustomerID,InvoicedBy,RemarkItem) VALUES('" + ReJobNumber.Text + "','" + li.SubItems[0].Text + "','" + li.SubItems[1].Text + "','" + li.SubItems[2].Text + "','" + li.SubItems[3].Text + "','" + li.SubItems[4].Text + "','" + li.SubItems[5].Text + "','" + li.SubItems[6].Text + "','" + li.SubItems[7].Text + "','" + li.SubItems[8].Text + "','" + li.SubItems[9].Text + "')";

                                SqlCommand cmd = new SqlCommand(InsertItmDetails, con4);

                                cmd.ExecuteNonQuery();

                                if (con4.State == ConnectionState.Open)
                                {
                                    con4.Close();
                                }
                            }

                            //============================================================================================================================

                            FaultSelect();



                            //Update the Repair Note Table-----------------------------------------------------------------------------------------------
                            SqlConnection con = new SqlConnection(IMS);
                            con.Open();
                            string faultUpdate = @"UPDATE RepairNotes SET  PaymentStatus='" + Convert.ToString(cmbJobType.SelectedItem) + "', JobStatus='1', ReCusID='" + cmbCustID.Text + "', CusFirstName='" + txtCusNameFaul.Text + "', CusPersonalAddress='" + txtCusAddfault.Text + "', CusTelNUmber='" + TelNumFault.Text + "', FaultStatus='" + tot + "', FaultOther='" + FaultOther.Text + "', RePowerCbl='" + RePowerCbl.Text + "',ExtraCoolingFans='" + ReKeyB.Text + "' ,ReUSB='" + ReUSB.Text + "', ReSystemOther='" + txtreOther.Text + "', ReCompletingDate='" + ReCompletingDate.Text + "', ReCompletingTime='" + ReCompletingTime.Text + "',JobType='" + cmbJobType.Text + "',Solution='1',Updated='1',UpdateUser='" + LgUser.Text + "', UpdatedTimeStamp='" + System.DateTime.Now.ToString() + "' where ReJobNumber='" + ReJobNumber.Text + "'";

                            SqlCommand cmd2 = new SqlCommand(faultUpdate, con);
                            cmd2.ExecuteNonQuery();

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }

                            //---------------------------------------------------------------------------------------------------------------------

                            MessageBox.Show("Successfully Updated the JOB Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            //load the report and print===============================================================================

                            RptRepairJobAddingFrm rptfrm = new RptRepairJobAddingFrm();
                            rptfrm.PrintingJOBNumber = ReJobNumber.Text;
                            rptfrm.PrintCopyDetails = "Updated copy";

                            rptfrm.Visible = true;
                            //=============================================================================================================

                            ClearTxt();

                            clearListview();
                            GenerateJOBNumbe();
                            closePanel();
                            PnlAddedItemDetails.Visible = true;
                            this.BtnItenDetals_Click(sender, e);


                            Ck_ListV_Repairing_Item_Count();
                            checkButton1.Checked = false;
                            button3.Text = "Save And Print";

                            #endregion
                        }
                    }
                    ClearTxt();
                    cmbCustID.SelectedIndex = -1;
                    cmbJobType.SelectedIndex = -1;
                    cmbAuthPerson.SelectedIndex = -1;

                   PnlAddedItemDetails.Visible = true;
                   this.BtnItenDetals_Click(sender,e);
                          

                   Ck_ListV_Repairing_Item_Count();
                   checkButton1.Checked = false;

                   GenerateJOBNumbe();
                            
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_011", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

           


        }

         
        public void ClearTx()
        {
            #region clear text as default
           ReKeyB.Text="No";
           ReUSB.Text="No";
           RePowerCbl.Text = "No";
           txtreOther.Text = "No";

            cmbCustID.Text = "";
            txtCusNameFaul.Text = "";
            TelNumFault.Text = "";
            txtCusAddfault.Text = "";
            TelNumFault.Text = "";

            // ReWarranty.Text="";
            //cmbJobType.Enabled = false;
            //cmbAuthPerson.Enabled = false;


           
            //clear check boes
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;

            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listVReItem.Items.Clear();
           

            #endregion

        }

      

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                

                PnlCustomerSerch.Visible = true;

                #region select the customer when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusTelNUmber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE CusActiveDeactive='" + 1 + "'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = false;
        }

        //private void dataGridView3_KeyUp(object sender, KeyEventArgs e)
        //{
           
        //}

        //customer search KeyUp---------------------------------------------------------------------------------
        private void textBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
          
            try
            {
                textBox18.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE 
                CusID LIKE '%" + textBox1.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView3.SelectedRows[0];

            string InvNum = "";

            InvNum = dr.Cells[0].Value.ToString();

            cmbCustID.Text = InvNum;

            txtCusNameFaul.Text = dr.Cells[1].Value.ToString()+" "+dr.Cells[2].Value.ToString();

           // MessageBox.Show(dr.Cells[4].Value.ToString());
           txtCusAddfault.Text = dr.Cells[4].Value.ToString();

          // MessageBox.Show(dr.Cells[3].Value.ToString());
            TelNumFault.Text = dr.Cells[5].Value.ToString();

            PnlCustomerSerch.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            cmbCustID.SelectedIndex = -1;
            cmbJobType.SelectedIndex = -1;
            cmbAuthPerson.SelectedIndex = -1;
        }

        private void textBox18_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                textBox1.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails WHERE 
                CusFirstName LIKE '%" + textBox18.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void cmbCustID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbCustID.Items.Clear();
            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                String LaodCus = "SELECT CusID,CusFirstName,CusPersonalAddress,CusTelNUmber,CusLastName FROM CustomerDetails WHERE CusID='" + cmbCustID.Text + "' ";
                SqlCommand cmd = new SqlCommand(LaodCus, con1);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtCusNameFaul.Text = dr[1].ToString() + " " + dr[4].ToString();
                    txtCusAddfault.Text = dr[2].ToString();
                    TelNumFault.Text = dr[3].ToString();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error comes from when you selecting the customer.","Error selecting customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
     
        }

        private void cmbCustID_Click(object sender, EventArgs e)
        {
            cmbCustID.Items.Clear();
            slectCus();
        }

        private void label37_Click(object sender, EventArgs e)
        {
            CustomerReg CusReg = new CustomerReg();
            CusReg.Visible = true;
        }

        private void cmbJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbJobType.SelectedIndex == 0 || cmbJobType.SelectedIndex==1)
            {
                LblFreeRepairngMsg.Text = "-- ";
            }
            
            if (cmbJobType.SelectedIndex == 2)
            {
                LblFreeRepairngMsg.Text = " Please Athorized Check";
            }
            if (cmbJobType.SelectedIndex == 3)
            {
                LblFreeRepairngMsg.Text = " Please Athorized Check";
            }

        }

        private void listVReItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ck_ListV_Repairing_Item_Count();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //try
            //{
                //int i;
                //for (i = 0; i <= listVReItem.Items.Count - 1; i++)
                //{
                //    SqlConnection con = new SqlConnection(IMS);
                //    con.Open();
                //    string InsertItmDetailsInvoice = "INSERT INTO RepairNoteItems(ReJobNumber,ItemID,ItemName,Warrany,BarcodeSerial,ItemStatus,InvoceBarrowID,CustomerID,InvoicedBy,RemarkItem) VALUES(@ReJobNumber,@ItemID,@ItemName,@Warrany,@BarcodeSerial,@ItemStatus,@InvoceBarrowID,@CustomerID,@InvoicedBy,@RemarkItem)";
                //    SqlCommand cmd = new SqlCommand(InsertItmDetailsInvoice, con);


                //    cmd.Parameters.AddWithValue("ReJobNumber", ReJobNumber.Text);
                //    cmd.Parameters.AddWithValue("ItemID", listVReItem.Items[i].SubItems[0].Text);
                //    cmd.Parameters.AddWithValue("ItemName", listVReItem.Items[i].SubItems[1].Text);
                //    cmd.Parameters.AddWithValue("Warrany", listVReItem.Items[i].SubItems[3].Text);
                //    cmd.Parameters.AddWithValue("BarcodeSerial", listVReItem.Items[i].SubItems[4].Text);

                //    cmd.Parameters.AddWithValue("ItemStatus", listVReItem.Items[i].SubItems[5].Text);
                //    cmd.Parameters.AddWithValue("InvoceBarrowID", listVReItem.Items[i].SubItems[6].Text);
                //    cmd.Parameters.AddWithValue("CustomerID", listVReItem.Items[i].SubItems[7].Text);
                //    cmd.Parameters.AddWithValue("InvoicedBy", listVReItem.Items[i].SubItems[8].Text);
                //    cmd.Parameters.AddWithValue("RemarkItem", listVReItem.Items[i].SubItems[9].Text);


                //    MessageBox.Show("Value Insert Successful..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    cmd.ExecuteNonQuery();
                    
                //    if (con.State == ConnectionState.Open)
                //    {
                //        con.Close();
                //    }
                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //keyup for searchJobNuber=====================================================================

        private void txtseachjob_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = "SELECT  ReJobNumber, PaymentStatus, PaymentDetails,JobStatus,LgUser,ReCusID,CusFirstName,CusPersonalAddress,CusTelNUmber,FaultStatus,FaultOther,RePowerCbl,ExtraCoolingFans,ReUSB,ReSystemOther,ReCompletingDate,ReCompletingTime,TroblShootInfor,JobType,ReEngNote,ReCusNote,Solution,TimeStamp FROM RepairNotes WHERE ReJobNumber LIKE'%" + txtseachjob.Text + "%' ";
                SqlCommand cmd = new SqlCommand(viewGrideview, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dgvjobIdRepairing1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dgvjobIdRepairing1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8],dr[9],dr[10],dr[11],dr[12],dr[13],dr[14],dr[15],dr[16],dr[17],dr[18],dr[19],dr[20],dr[21],dr[22]);

                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //txtseachjob.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dgvjobIdRepairing_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Select the Authorized person to the dropdown list========================================================

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = @"SELECT DisplayOn FROM UserProfile";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);

            cmbAuthPerson.Items.Clear();


            SqlDataReader drx = cmd1.ExecuteReader();

            while (drx.Read())
            {
                cmbAuthPerson.Items.Add(drx[0].ToString());
            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }

            //==========================================================================================================

            //load the customer ID======================================================================================

            SqlConnection concu = new SqlConnection(IMS);
            concu.Open();

            string CusSelect = "SELECT CusID FROM CustomerDetails WHERE CusActiveDeactive='1'";
            SqlCommand cmdcu = new SqlCommand(CusSelect, concu);
            SqlDataReader dry = cmdcu.ExecuteReader();

            while (dry.Read())
            {
                cmbCustID.Items.Add(dry[0].ToString());
            }
            cmdcu.Dispose();
            dry.Close();

            if (concu.State == ConnectionState.Open)
            {

                concu.Close();
            }

            //===========================================================================================================

            //Change Update and New Radio Button========================================================================
            rbtnNEW.Enabled = true;
            rbtnNEW.Checked = false;
            rbtnUpdate.Checked = false;
            rbtnUpdate.Enabled = true;

            
            //===================================================================================

            DataGridViewRow dr = dgvjobIdRepairing1.SelectedRows[0];
            string JobNo = "";
            
            JobNo = dr.Cells[0].Value.ToString();
            ReJobNumber.Text = JobNo;

            //cmbCustID.Text = dr.Cells[5].Value.ToString();

            ReKeyB.Text = dr.Cells[12].Value.ToString();
            ReUSB.Text = dr.Cells[13].Value.ToString();
            RePowerCbl.Text = dr.Cells[11].Value.ToString();
            txtreOther.Text = dr.Cells[14].Value.ToString();
            cmbCustID.Text = dr.Cells[5].Value.ToString();
           
            cmbJobType.Text=dr.Cells[18].Value.ToString();
            ReCompletingDate.Text = dr.Cells[15].Value.ToString();
            ReCompletingTime.Text = dr.Cells[16].Value.ToString();
            FaultOther.Text = dr.Cells[10].Value.ToString();
            cmbAuthPerson.Text = dr.Cells[23].Value.ToString();

            string FaultStatus = dr.Cells[9].Value.ToString();


            PnlJobSearch1.Visible = false;
            #region fault CheckBox select

            string Num01 = FaultStatus.Substring(0, 1);
            string Num02 = FaultStatus.Substring(1, 1);
            string Num03 = FaultStatus.Substring(2, 1);
            string Num04 = FaultStatus.Substring(3, 1);
            string Num05 = FaultStatus.Substring(4, 1);
            string Num06 = FaultStatus.Substring(5, 1);
            string Num07 = FaultStatus.Substring(6, 1);
            string Num08 = FaultStatus.Substring(7, 1);
            string Num09 = FaultStatus.Substring(8, 1);
            string Num10 = FaultStatus.Substring(9, 1);

            //For the check box 01
            if (Num01 == "1")
            {
                checkBox1.Checked = true;
            }
            if (Num01 == "0")
            {
                checkBox1.Checked = false;
            }
            //For the check box 02
            if (Num02 == "1")
            {
                checkBox2.Checked = true;
            }
            if (Num02 == "0")
            {
                checkBox2.Checked = false;
            }
            //For the check box 03
            if (Num03 == "1")
            {
                checkBox3.Checked = true;
            }
            if (Num03 == "0")
            {
                checkBox3.Checked = false;
            }
            //For the check box 04
            if (Num04 == "1")
            {
                checkBox4.Checked = true;
            }
            if (Num04 == "0")
            {
                checkBox4.Checked = false;
            }
            //For the check box 05
            if (Num05 == "1")
            {
                checkBox1.Checked = true;
            }
            if (Num05 == "0")
            {
                checkBox5.Checked = false;
            }
            //For the check box 06
            if (Num06 == "1")
            {
                checkBox6.Checked = true;
            }
            if (Num06 == "0")
            {
                checkBox6.Checked = false;
            }
            //For the check box 07
            if (Num07 == "1")
            {
                checkBox1.Checked = true;
            }
            if (Num07 == "0")
            {
                checkBox7.Checked = false;
            }
            //For the check box 08
            if (Num08 == "1")
            {
                checkBox8.Checked = true;
            }
            if (Num08 == "0")
            {
                checkBox8.Checked = false;

            }
            //For the check box 09
            if (Num09 == "1")
            {
                checkBox9.Checked = true;
            }
            if (Num09 == "0")
            {
                checkBox9.Checked = false;
            }
            //For the check box 10
            if (Num10 == "1")
            {
                checkBox10.Checked = true;
            }
            if (Num10 == "0")
            {
                checkBox10.Checked = false;
            }

            #endregion

            ReKeyB.Enabled = false;
            ReUSB.Enabled = false;
            RePowerCbl.Enabled = false;
            txtreOther.Enabled = false;
            cmbCustID.Enabled = false;
            cmbJobType.Enabled = false;
            ReCompletingDate.Enabled = false;
            ReCompletingTime.Enabled = false;
            FaultOther.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;
            checkBox9.Enabled = false;
            checkBox10.Enabled = false;


            listVReItem.Enabled = false;

            //Diable all list views===============
            listVReItem.Enabled = false;
            listView3.Enabled = false;
            listView2.Enabled = false;

            PnlItemDetails.Enabled = false;
            pnlInvoiceDetails.Enabled = false;
            PnlNewItemRepair.Enabled = false;
            PnlFault.Enabled = false;
            PnlAddedItemDetails.Enabled = false;
            //=========================================


            {
                DataGridViewRow d = dgvjobIdRepairing1.SelectedRows[0];
                string JobNum = "";

                JobNum = d.Cells[0].Value.ToString();

                ReJobNumber.Text = JobNum;


                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = @"SELECT   ItemID, ItemName,PurchesDate, Warrany, BarcodeSerial, ItemStatus, InvoceBarrowID, CustomerID, InvoicedBy, RemarkItem,ReJobNumber
                                    FROM RepairNoteItems where ReJobNumber = '" + JobNum + "' ";
                SqlCommand cmd = new SqlCommand(viewGrideview, con);
                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                listVReItem.Items.Clear();

                while (dr1.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr1[0].ToString());
                    li.SubItems.Add(dr1[1].ToString());
                    li.SubItems.Add(dr1[2].ToString());
                    li.SubItems.Add(dr1[3].ToString());
                    li.SubItems.Add(dr1[4].ToString());
                    li.SubItems.Add(dr1[5].ToString());
                    li.SubItems.Add(dr1[6].ToString());
                    li.SubItems.Add(dr1[7].ToString());
                    li.SubItems.Add(dr1[8].ToString());
                    li.SubItems.Add(dr1[9].ToString());
                    li.SubItems.Add(dr1[10].ToString());



                    listVReItem.Items.Add(li);


                }

                PnlJobSearch1.Visible = false;
                Ck_ListV_Repairing_Item_Count();
            }


        }




        //add job number value in DtagridView===========================================================================
        private void button6_Click(object sender, EventArgs e)
        {
            PnlJobSearch1.Visible = true;
            PnlJobSearch1.BringToFront();
            try
            {

                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                String viewGrideview = "SELECT  ReJobNumber, PaymentStatus, PaymentDetails,JobStatus,LgUser,ReCusID,CusFirstName,CusPersonalAddress,CusTelNUmber,FaultStatus,FaultOther,RePowerCbl,ExtraCoolingFans,ReUSB,ReSystemOther,ReCompletingDate,ReCompletingTime,TroblShootInfor,JobType,ReEngNote,ReCusNote,Solution,TimeStamp,Tecnician FROM RepairNotes WHERE PaymentDetails='Pending'";
                SqlCommand cmd = new SqlCommand(viewGrideview, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dgvjobIdRepairing1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dgvjobIdRepairing1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22], dr[23]);

                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //txtseachjob.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PnlJobSearch1.Visible = false;
        }

        private void r(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
 //enable all in newradiobutton==================================================================================
            ReKeyB.Enabled = true;
            ReUSB.Enabled = true;
            RePowerCbl.Enabled = true;
            txtreOther.Enabled = true;
            cmbCustID.Enabled = true;
            cmbJobType.Enabled = true;
            ReCompletingDate.Enabled = true;
            ReCompletingTime.Enabled = true;
            FaultOther.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
            checkBox9.Enabled = true;
            checkBox10.Enabled = true;
            txtCusName.Enabled = false;
            txtCusAddfault.Enabled = true;
            TelNumFault.Enabled=true;

            PnlFault.Enabled = true;
            PnlAddedItemDetails.Enabled = true;
            PnlItemDetails.Enabled = true;
            pnlInvoiceDetails.Enabled = true;
            PnlNewItemRepair.Enabled = true;

            btnaultDetails.Enabled = true;

            listVReItem.Enabled = true;

           button3.Text = "Update JOB";
//==================================================================================================================================

        }

        private void rbtnNEW_CheckedChanged(object sender, EventArgs e)
        {
            closePanel();
            ClearTxt();
            txtCusName.Enabled = false;
            TelNumFault.Enabled = false;

            btnaultDetails.Enabled = false;

            cmbCustID.SelectedIndex = -1;
            cmbJobType.SelectedIndex=-1;
            cmbAuthPerson.SelectedIndex = -1;
            PnlAddedItemDetails.Visible = true;

            commonColorSelectItemsRight();

            PnlAddedItemDetails.Visible = true;
            this.BtnItenDetals_Click(sender, e);


            Ck_ListV_Repairing_Item_Count();


            clearListview();
            button3.Text = "Save";
            GenerateJOBNumbe();

        }

        public void GenerateJOBNumbe()
        {
            #region New JOB Number...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT ReJobNumber FROM RepairNotes";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    ReJobNumber.Text = "REJ1001";
                    // PassInvoiceNumber.Text = "INV1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 ReJobNumber FROM RepairNotes order by ReJobNumber DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        ReJobNumber.Text = "REJ" + no;
                        // PassInvoiceNumber.Text = "INV" + no;

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
       
            ReKeyB.Enabled = true;
            ReUSB.Enabled = true;
            RePowerCbl.Enabled = true;
            txtreOther.Enabled = true;
            cmbCustID.Enabled = true;
            cmbJobType.Enabled = true;
            ReCompletingDate.Enabled = true;
            ReCompletingTime.Enabled = true;
            FaultOther.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
            checkBox9.Enabled = true;
            checkBox10.Enabled = true;
            txtCusName.Enabled = true;
            txtCusAddfault.Enabled = true;
            TelNumFault.Enabled = true;
        }

        private void PnlIinvoiceSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PnlJobSearch1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCusName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlInvoiceDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PnlCustomerSerch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            PnlJobSearch1.Visible = false;
        }
//=================desable all ===================================================================================
        public void desebleall()
        {
            ReKeyB.Enabled = false;
            ReUSB.Enabled = false;
            RePowerCbl.Enabled = false;
            txtreOther.Enabled = false;
            cmbCustID.Enabled = false;
            cmbJobType.Enabled = false;
            ReCompletingDate.Enabled = false;
            ReCompletingTime.Enabled = false;
            FaultOther.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;
            checkBox9.Enabled = false;
            checkBox10.Enabled = false;
            txtCusName.Enabled = false;
            txtCusAddfault.Enabled = false;
            TelNumFault.Enabled = false;
        }

        private void ReCompletingDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbAuthPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbAuthPerson_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT DisplayOn FROM UserProfile WHERE AtiveDeactive='1'";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);

                cmbAuthPerson.Items.Clear();

               
                SqlDataReader dr = cmd1.ExecuteReader();

                while (dr.Read())
                {
                    cmbAuthPerson.Items.Add(dr[0].ToString());
                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            FaultSelect();
            MessageBox.Show(tot);
        }

        private void listVReItem_TabIndexChanged(object sender, EventArgs e)
        {
            Ck_ListV_Repairing_Item_Count();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==13)
            {
                txtbarcode.Focus();
            }
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue== 13)
            {
                cmbWarrantyStatus.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblNoWarranty.Text = cmbWarrantyStatus.Text;
        }

        private void cmbWarrantyStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                NewRepairAdd.Focus();
            }
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            #region load vendor details in gridView

            GenerateJOBNumbe();

            if(checkButton1.Checked==true)
            {
                pnlPreviousNotSearch.Visible = true;
                checkButton1.Text = "Viewed Visit Note";
                listVReItem.Items.Clear();

               // currentSkin = DevExpress.Skins.CommonSkins.GetSkin(checkButton1.LookAndFeel);
                DevExpress.Skins.CommonSkins.GetSkin(checkButton1.LookAndFeel).Colors["Control"] = Color.Green;

            try
            {

                ClearTxt();
                cmbCustID.SelectedIndex = -1;
                cmbJobType.SelectedIndex = -1;
                cmbAuthPerson.SelectedIndex = -1;

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string VenSelectAll = @"SELECT  Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, Solution,[Visited _Discription],VisitedPerson,visited_Date, Visited_time, Date, addUser FROM Repair_VisitNote where (Solution='Repaire Successful' OR Solution='Borrow Item') and  AddedRepairNotes='-'";
            SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            dtagridSearchprevious.Rows.Clear();

            while (dr.Read() == true)
            {
                dtagridSearchprevious.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);

            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }




            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            }
            #endregion

            if (checkButton1.Checked == false)
            {
                ClearTxt();
                pnlPreviousNotSearch.Visible = false;
                cmbCustID.SelectedIndex = -1;
                cmbJobType.SelectedIndex = -1;
                cmbAuthPerson.SelectedIndex = -1;
                checkButton1.Text = "View Visit Note";
            }
        }

        private void dtagridSearchprevious_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dtagridSearchprevious.SelectedRows[0];
            cmbCustID.Enabled = true;
            lblPreviousVisitID.Text = dr.Cells[0].Value.ToString();
            cmbCustID.Text = dr.Cells[1].Value.ToString();
            txtCusNameFaul.Text = dr.Cells[2].Value.ToString();
            txtCusAddfault.Text = dr.Cells[3].Value.ToString();
            TelNumFault.Text = dr.Cells[4].Value.ToString();
            FaultOther.Text = dr.Cells[7].Value.ToString();

            pnlPreviousNotSearch.Visible = false;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            #region Search Previous.........................................................

            try
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string VenSelectAll = @"select Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, Solution,[Visited _Discription],VisitedPerson,visited_Date, Visited_time, Date, addUser FROM Repair_VisitNote where (Solution='Repaire Successful' OR Solution='Borrow Item') and  AddedRepairNotes='-' and (Visit_ID like '%" + textBox2.Text + "%'OR CusID like '%" + textBox2.Text + "%' OR CusName like '%" + textBox2.Text + "%' OR CusAddre like '%" + textBox2.Text + "%' OR Custel like '%" + textBox2.Text + "%' OR ConformPerson like '%" + textBox2.Text + "%' OR Solution like '%" + textBox2.Text + "%' OR VisitedPerson like '%" + textBox2.Text + "%' ) ";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dtagridSearchprevious.Rows.Clear();

                while (dr.Read() == true)
                {
                    dtagridSearchprevious.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void button10_Click_2(object sender, EventArgs e)
        {
            pnlPreviousNotSearch.Visible = false;
            checkButton1.Checked = false;
        }

//load value combobox----------------------------------------------------------------------------------------------------------
        //void Fillcombo()
        //{
        //    SqlConnection con1 = new SqlConnection(IMS);
        //    con1.Open();

        //    string CusSelectAll = "SELECT CusID,CusFirstName,CusLastName,CusCompanyName,CusPersonalAddress,CusMobileNumber,CusPriceLevel,CusCreditLimit FROM CustomerDetails";
        //    SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
        //    try
        //    {

        //        con1.Open();
        //        SqlDataReader dr = cmd1.ExecuteReader();
        //        while (dr.Read())
        //        {

        //            string name = dr.GetString("CusID");
        //            cmbCustID.Items.Add(name);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
//-----------------------------------------------------------------------------------------------------------------
      
        
    }
   



    }

