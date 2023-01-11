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

namespace Inventory_Control_System
{
    public partial class NewItem : Form
    {
        

        public NewItem()
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
            ItmName.Focus();
        }


        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        string ImgLoc = "";

       
        public void getCreateItemCode()
        {
            #region New Item Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select ItmID from NewItemDetails";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    ItmID.Text = "ITM1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 ItmID FROM NewItemDetails order by ItmID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        ItmID.Text = "ITM" + no;

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


        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string InsNewItem = "INSERT INTO ItemCategory(CatName) VALUES('" + Stkcategory.Text + "')";
                SqlCommand cmd = new SqlCommand(InsNewItem, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            SelectCat();
            PnlhideAll();
            
        }

        public void PnlhideAll()
        {
            PnlLocation.Visible = false;
            PnlCategory.Visible = false;
            //PnlStockType.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PnlhideAll();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            PnlhideAll();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PnlhideAll();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PnlhideAll();
      
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string InsNewItem = "INSERT INTO ItemLocation(ItmLocationName) VALUES('" + NewItemLocation.Text + "')";
                SqlCommand cmd = new SqlCommand(InsNewItem, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NewItemLocation.Clear();
                PnlLocation.Visible = false;
                ItmLocation.DroppedDown = true;
                ItmLocation.Focus();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            SelectLocation();
            PnlhideAll();
            
        }

        private void label45_Click(object sender, EventArgs e)
        {
            PnlhideAll();
            PnlCategory.Visible = true;
            Stkcategory.Focus();
        }

        private void label47_Click(object sender, EventArgs e)
        {
            PnlhideAll();
            PnlLocation.Visible = true;
            NewItemLocation.Focus();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            if (ItmName.Text == "")
            {
                MessageBox.Show("Please enter the product name","Item Name Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tabControl1.SelectTab(0);

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                ItmSellPrice.Focus();
               // MessageBox.Show("sd");
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

           
            
            if (ItmName.Text == "" && tabControl1.SelectedIndex!=0)
            {
                MessageBox.Show("Please enter the product name", "Item Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl1.SelectTab(0);
                return;
            }


           

            PnlVendorSerch.Visible = false;
            PnlItemSearch.Visible = false;
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                dlg.FilterIndex = 4;

                dlg.Title = "select Product Image";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ItmImage.Image = null;
                    ImgLoc = dlg.FileName.ToString();
                    ItmImage.ImageLocation = ImgLoc;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ItmImage.Image = null;
            ItmImage.Image = Inventory_Control_System.Properties.Resources.No_Image_01;
        }

        private void button4_Click(object sender, EventArgs e)
        {


            #region before Save check missisng things=======================================================================================

            // name empty________________________________________________________________________________________________________
            if (ItmName.Text == "")
            {
                MessageBox.Show("please enter Valied Product Name", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                ItmName.Focus();
                return;
            }

            //=========================================================================================================================

            //warenty 0 and no worrenty ckd ________________________________________________________________________________________
            if (ItmWarrenty.Text != "0" && rbNoWarr.Checked == true)
            {
                MessageBox.Show("please enter correct warrenty period", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                tabControl1.SelectTab(0);
                ItmWarrenty.Focus();


                return;
            }

            //=========================================================================================================================

            // correct Order Unit Cost_________________________________________________________________________________________________
            if (ItmOderCost.Text == "0.00" || ItmOderCost.Text == "0" || ItmOderCost.Text == "")
            {
                MessageBox.Show("please enter correct Order Unit Cost", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                tabControl1.SelectTab(1);
                ItmOderCost.Focus();
                return;
            }

            //=========================================================================================================================

            Double a = Convert.ToDouble(ItmOderCost.Text);
            Double b = Convert.ToDouble(ItmSellPrice.Text);

            //ceck Selling price and Order Price_________________________________________________________________________________________
            if (a > b)
            {
                MessageBox.Show("Your Selling Price lower than The Cost of It.please enter correct Selling Cost", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                tabControl1.SelectTab(2);
                ItmSellPrice.Focus();
                return;
            }

            //=========================================================================================================================

            if (ItmWarrenty.Text == "0" && (rbMonths.Checked == true || rbYears.Checked == true))
            {
                MessageBox.Show("please enter correct warrenty period", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                tabControl1.SelectTab(0);
                ItmWarrenty.Focus();

                return;
            }

            //========================================================================================================================


            //check barcode same or not_________________________________________________________________________________________

            if (ChkAddBarCode.Checked == true)
            {
                if (txtBarcode.Text != "")
                {
                    if (txtBarcode.Text != txtBarcodeConfirm.Text)
                    {
                        MessageBox.Show("Your Barcode is wrong. please enter correct barcode", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        tabControl1.SelectTab(0);
                        txtBarcode.Focus();
                        return;
                    }
                }
            }

            if (txtBarcode.Text == "")
            {
                txtBarcode.Text = ItmID.Text;
            }


            //  checked StockItem unit ----------------------------------------------------------
            if (ItmStkUnit.Text == "")
            {
                MessageBox.Show("Please select Stock Unit", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ItmStkUnit.Focus();
                return;
            }

            //================================================================


            if (ItmDisc02.Text == "" || Double.Parse(ItmDisc02.Text) == 0)
            {
                ItmDisc02.Text = ItmOderCost.Text;
                ItmMark02.Text = "0.0";
            }

            if (ItmDisc01.Text == "" || Double.Parse(ItmDisc01.Text) == 0)
            {
                ItmDisc01.Text = ItmOderCost.Text;
                ItmMark01.Text = "0.0";
            }

            if (ItmDisc03.Text == "" || Double.Parse(ItmDisc03.Text) == 0)
            {
                ItmDisc03.Text = ItmOderCost.Text;
                ItmMark03.Text = "0.0";
            }


            //check discount---------------------------------------------------------------------------
            //if (Double.Parse( ItmDisc01.Text)==0 || ItmDisc01.Text == "")
            //{
            //    MessageBox.Show("Please enter discount","Message");
            //    ItmDisc01.Focus();
            //    return;
            //}
            //if (Double.Parse( ItmDisc02.Text) ==0  || ItmDisc02.Text == "" )
            //{
            //    MessageBox.Show("Please enter discount", "Message");
            //    ItmDisc02.Focus();
            //    return;
            //}
            //if (Double.Parse(ItmDisc03.Text) ==0 || ItmDisc03.Text == " ")
            //{
            //    MessageBox.Show("Please enter discount", "Message");
            //    ItmDisc03.Focus();
            //    return;
            //}

           
            //=========================================================================================================================



            #endregion



            //Select warrenty period==================================================
            //0-no warrenty, 1- number of years, 2- number of months

            string warrenty = "";

            if (rbNoWarr.Checked == true)
                warrenty = "0";
            if (rbYears.Checked == true)
                warrenty = "1";
            if (rbMonths.Checked == true)
                warrenty = "2";

            warrenty = warrenty + ItmWarrenty.Text;
            //========================================================================
            byte[] img = null;

            try
            {



                if (RbNew.Checked == true)
                {


                    #region Save if select the Item New

                    getCreateItemCode();

                    if (ImgLoc != "")
                    {

                        FileStream fs = new FileStream(ImgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);


                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string NewItem = "INSERT INTO NewItemDetails(ItmID,ItmActiveOrNot,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmManufacturer,ItmModel,ItmWarrenty,ItmDescription,ItmVenID,ItmVendor,ItmStkUnit,ItmReoderLvl,ItmTarQuntity,ItmOrderUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmMark01,ItmDisc02,ItmMark02,ItmDisc03,ItmMark03,ItmImage,ItemBarCode) VALUES('" + ItmID.Text + "','" + 1 + "','" + ItmName.Text + "','" + ItmCategory.Text + "','" + ItmStockType.Text + "','" + ItmLocation.Text + "','" + ItmManufacturer.Text + "','" + ItmModel.Text + "','" + warrenty + "','" + ItmDescription.Text + "','" + ItmVenID.Text + "','" + ItmVendor.Text + "','" + ItmStkUnit.Text + "','" + ItmReoderLvl.Text + "','" + ItmTarQuntity.Text + "','" + ItmOrderUnit.Text + "','" + ItmOderCost.Text + "','" + ItmSellPrice.Text + "','" + ItmDisc01.Text + "','" + ItmMark01.Text + "','" + ItmDisc02.Text + "','" + ItmMark02.Text + "','" + ItmDisc03.Text + "','" + ItmMark03.Text + "',@img,'" + txtBarcode.Text + "')";
                        SqlCommand cmd = new SqlCommand(NewItem, con);

                        cmd.Parameters.Add(new SqlParameter("@img", img));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //  getCreateItemCode();

                        tabControl1.SelectTab(0);

                        tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
                        ItmName.Focus();

                        // ClearItems();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                    else
                    {
                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string NewItem = "INSERT INTO NewItemDetails(ItmID,ItmActiveOrNot,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmManufacturer,ItmModel,ItmWarrenty,ItmDescription,ItmVenID,ItmVendor,ItmStkUnit,ItmReoderLvl,ItmTarQuntity,ItmOrderUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmMark01,ItmDisc02,ItmMark02,ItmDisc03,ItmMark03,ItemBarCode) VALUES('" + ItmID.Text + "','" + 1 + "','" + ItmName.Text + "','" + ItmCategory.Text + "','" + ItmStockType.Text + "','" + ItmLocation.Text + "','" + ItmManufacturer.Text + "','" + ItmModel.Text + "','" + warrenty + "','" + ItmDescription.Text + "','" + ItmVenID.Text + "','" + ItmVendor.Text + "','" + ItmStkUnit.Text + "','" + ItmReoderLvl.Text + "','" + ItmTarQuntity.Text + "','" + ItmOrderUnit.Text + "','" + ItmOderCost.Text + "','" + ItmSellPrice.Text + "','" + ItmDisc01.Text + "','" + ItmMark01.Text + "','" + ItmDisc02.Text + "','" + ItmMark02.Text + "','" + ItmDisc03.Text + "','" + ItmMark03.Text + "','" + txtBarcode.Text + "')";
                        SqlCommand cmd = new SqlCommand(NewItem, con);


                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // getCreateItemCode();



                        tabControl1.SelectTab(0);
                        tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
                        ItmName.Focus();
                        //ItmName.Focus();


                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }



                    }

                    #endregion

                    //add Item details to the Current Stock table==============================================

                    SqlConnection con3 = new SqlConnection(IMS);
                    con3.Open();

                    string InsertToCurrentStock = "INSERT INTO CurrentStock(ItemID,AvailableStockCount) VALUES ('" + ItmID.Text + "','" + 0 + "')";
                    SqlCommand cmd3 = new SqlCommand(InsertToCurrentStock, con3);

                    cmd3.ExecuteNonQuery();

                    if (con3.State == ConnectionState.Open)
                    {
                        con3.Close();
                    }

                    // ClearItems();

                    //getCreateItemCode();

                }

                if (RbUp.Checked == true && CkDeactivated.Checked == false)
                {

                    #region Update the Item Details

                    if (ImgLoc != "")
                    {

                        FileStream fs = new FileStream(ImgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);


                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string UpdateItem = "UPDATE NewItemDetails SET ItmActiveOrNot= '" + 1 + "',ItmName='" + ItmName.Text + "',ItmCategory='" + ItmCategory.Text + "',ItmStockType='" + ItmStockType.Text + "',ItmLocation='" + ItmLocation.Text + "',ItmManufacturer='" + ItmManufacturer.Text + "',ItmModel='" + ItmModel.Text + "',ItmWarrenty='" + warrenty + "',ItmDescription='" + ItmDescription.Text + "',ItmVenID='" + ItmVenID.Text + "',ItmVendor='" + ItmVendor.Text + "',ItmStkUnit='" + ItmStkUnit.Text + "',ItmReoderLvl='" + ItmReoderLvl.Text + "',ItmTarQuntity='" + ItmTarQuntity.Text + "',ItmOrderUnit='" + ItmOrderUnit.Text + "',ItmOderCost='" + ItmOderCost.Text + "',ItmSellPrice='" + ItmSellPrice.Text + "',ItmDisc01='" + ItmDisc01.Text + "',ItmMark01='" + ItmMark01.Text + "',ItmDisc02='" + ItmDisc02.Text + "',ItmMark02='" + ItmMark02.Text + "',ItmDisc03='" + ItmDisc03.Text + "',ItmMark03='" + ItmMark03.Text + "',ItmImage=@img,ItemBarCode='" + txtBarcode.Text + "'  WHERE ItmID='" + ItmID.Text + "'";
                        //ItmID,ItmActiveOrNot,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmManufacturer,ItmModel,ItmWarrenty,ItmDescription,ItmVenID,ItmVendor,ItmStkUnit,ItmReoderLvl,ItmTarQuntity,ItmOrderUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmMark01,ItmDisc02,ItmMark02,ItmDisc03,ItmMark03,ItmImage,ItemBarCode
                        SqlCommand cmd = new SqlCommand(UpdateItem, con);

                        cmd.Parameters.Add(new SqlParameter("@img", img));
                        cmd.ExecuteNonQuery();

                        //MessageBox.Show(ItmReoderLvl.Text);
                        MessageBox.Show("Successfully Updated.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // getCreateItemCode();

                        tabControl1.SelectTab(0);
                        ItmName.Focus();

                        // ClearItems();
                        RbNew.Checked = true;

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                    else
                    {
                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string UpdateItem = "UPDATE NewItemDetails SET ItmActiveOrNot= '" + 1 + "',ItmName='" + ItmName.Text + "',ItmCategory='" + ItmCategory.Text + "',ItmStockType='" + ItmStockType.Text + "',ItmLocation='" + ItmLocation.Text + "',ItmManufacturer='" + ItmManufacturer.Text + "',ItmModel='" + ItmModel.Text + "',ItmWarrenty='" + warrenty + "',ItmDescription='" + ItmDescription.Text + "',ItmVenID='" + ItmVenID.Text + "',ItmVendor='" + ItmVendor.Text + "',ItmStkUnit='" + ItmStkUnit.Text + "',ItmReoderLvl='" + ItmReoderLvl.Text + "',ItmTarQuntity='" + ItmTarQuntity.Text + "',ItmOrderUnit='" + ItmOrderUnit.Text + "',ItmOderCost='" + ItmOderCost.Text + "',ItmSellPrice='" + ItmSellPrice.Text + "',ItmDisc01='" + ItmDisc01.Text + "',ItmMark01='" + ItmMark01.Text + "',ItmDisc02='" + ItmDisc02.Text + "',ItmMark02='" + ItmMark02.Text + "',ItmDisc03='" + ItmDisc03.Text + "',ItmMark03='" + ItmMark03.Text + "',ItemBarCode='" + txtBarcode.Text + "' WHERE ItmID='" + ItmID.Text + "'";
                        SqlCommand cmd = new SqlCommand(UpdateItem, con);
                        cmd.ExecuteNonQuery();

                        // MessageBox.Show(UpdateItem);
                        MessageBox.Show("Successfully Updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        //  ClearItems();

                        RbNew.Checked = true;
                        tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
                        ItmName.Focus();

                        //  getCreateItemCode();

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    #endregion

                    }

                }

                if (RbUp.Checked == true && CkDeactivated.Checked == true)
                {



                    #region Deactivate the Item Details

                    if (ImgLoc != "")
                    {

                        FileStream fs = new FileStream(ImgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);


                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();

                        DialogResult Result = MessageBox.Show("Are you sure you want to deactivate this Item?.", "Deactivated", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (Result == DialogResult.Yes)
                        {
                            string UpdateItem = "UPDATE NewItemDetails SET ItmActiveOrNot= '" + 0 + "' WHERE ItmID='" + ItmID.Text + "'";
                            SqlCommand cmd = new SqlCommand(UpdateItem, con);

                            cmd.Parameters.Add(new SqlParameter("@img", img));
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Successfully Updated.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //   getCreateItemCode();

                            tabControl1.SelectTab(0);
                            ItmName.Focus();

                            //   ClearItems();
                            RbNew.Checked = true;
                            //RbNew.Checked = true;

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }

                    else
                    {

                        SqlConnection con = new SqlConnection(IMS);
                        con.Open();
                        string UpdateItem = "UPDATE NewItemDetails SET ItmActiveOrNot= '" + 0 + "' WHERE ItmID='" + ItmID.Text + "'";
                        SqlCommand cmd = new SqlCommand(UpdateItem, con);

                        //cmd.Parameters.Add(new SqlParameter("@img", img));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully Updated.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //getCreateItemCode();

                        // ClearItems();
                        RbNew.Checked = true;

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }

                    #endregion


                    }


                    //Clear Image

                    ClearItems();
                }

                ClearItems();

                getCreateItemCode();
                tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
                ItmName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("System is Busy,Please try again later", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        

        public void ClearItems()
        {
            #region defaults values --------------------------------------------

            ItmName.Text = "";
            ItmCategory.Text = "";
            ItmStockType.Text = "";
            ItmLocation.Text = "";
            ItmManufacturer.Text = "";
            ItmModel.Text = "";
            ItmWarrenty.Text = "0";
            ItmDescription.Text = "";
            ItmVendor.Text = "";
            ItmVenID.SelectedText = "";
            ItmStkUnit.SelectedText = "";
            ItmReoderLvl.Text = "1";
            ItmTarQuntity.Text = "5";
            ItmOrderUnit.SelectedText = "";
            ItmOderCost.Text = "0.00";
            ItmSellPrice.Text = "0.00";
            ItmDisc01.Text = "0.00";
            ItmMark01.Text = "";
            ItmDisc02.Text = "0.00";
            ItmMark02.Text = "";
            ItmDisc03.Text = "0.00";
            ItmMark03.Text = "";
            txtBarcode.Text = "";
            txtBarcodeConfirm.Text = "";
            ChkAddBarCode.Checked = false;
           
            ItmCategory.SelectedIndex = -1;
            ItmLocation.SelectedIndex = -1;
            ItmStockType.SelectedIndex = -1;

            ItmImage.Image = Inventory_Control_System.Properties.Resources.No_Image_01;

            rbNoWarr.Checked = true;

            CkDeactivated.Enabled = false;

            #endregion
        }

        public void SelectCat()
        {
            try
            {
                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string selectCat = "SELECT CatName FROM ItemCategory ORDER BY CatName ASC";
                SqlCommand cmd = new SqlCommand(selectCat, con);
                SqlDataReader dr = cmd.ExecuteReader();

                ItmCategory.Items.Clear();
                while (dr.Read())
                {
                    ItmCategory.Items.Add(dr[0]);
                }
                cmd.Dispose();
                dr.Close();

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

        //Item Location Select
        public void SelectLocation()
        {
            try
            {
                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string selectLoc = "SELECT ItmLocationName FROM ItemLocation ORDER BY ItmLocationName ASC";
                SqlCommand cmd = new SqlCommand(selectLoc, con);
                SqlDataReader dr = cmd.ExecuteReader();

                ItmLocation.Items.Clear();
                while (dr.Read())
                {
                    ItmLocation.Items.Add(dr[0]);
                }
                cmd.Dispose();
                dr.Close();

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

        private void NewItem_Load(object sender, EventArgs e)
        {
            try
            {
                //create a new Item code when form load==================================================
                getCreateItemCode();


                PnlItemSearch.BringToFront();
                PnlLocation.BringToFront();
                PnlCategory.BringToFront();


                tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];

                //========================================================================================

                #region load vendors==========================================================================

                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string selectVen = "SELECT VenderID,VenderName,VenderPHAddress FROM VenderDetails WHERE ActiveDeactive='1' ORDER BY VenderID ASC";
                SqlCommand cmd = new SqlCommand(selectVen, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ItmVenID.Items.Add(dr[0]);
                }
                cmd.Dispose();
                dr.Close();

                if (con.State == ConnectionState.Open)
                {

                    con.Close();
                }
                #endregion

                // Load Item Catogery=====================================================================================================
                SelectCat();

                // Load Item Location=====================================================================================================
                SelectLocation();

                RbNew.Enabled = false;
                RbUp.Enabled = false;
                RbNew.Checked = true;

                CkDeactivated.Enabled = false;

                ItmVendor.Enabled = false;
                ItmName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PnlVendorSerch.Visible = true;

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderPHAddress FROM VenderDetails WHERE ActiveDeactive='" + 1 + "'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);

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

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlVendorSerch.Visible = false;
        }

        private void ItmVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderName FROM VenderDetails WHERE VenderID='" + ItmVenID.Text + "'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
               // dataGridView1.Rows.Clear();

                if (dr.Read())
                {
                   ItmVendor.Text= dr[0].ToString();

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

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {


            try
            {
                textBox2.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT VenderID,VenderName,VenderComName FROM VenderDetails WHERE ActiveDeactive='" + 1 + "' AND VenderID LIKE '" + textBox1.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);

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

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                textBox1.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

              
                string VenSelectAll = "SELECT VenderID,VenderName,VenderComName FROM VenderDetails WHERE ActiveDeactive='" + 1 + "' AND VenderName LIKE '" + textBox2.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);

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

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];

                ItmVenID.Text = dr.Cells[0].Value.ToString();
                ItmVendor.Text = dr.Cells[1].Value.ToString();
                


                PnlVendorSerch.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

            

        }

        private void ItmWarrenty_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void ItmDisc01_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (ItmDisc01.Text == "")
                {
                    return;
                }

                Double sell = Convert.ToDouble(ItmSellPrice.Text);
                Double DiscP1 = Convert.ToDouble(ItmDisc01.Text);

                if (sell >= DiscP1)
                {
                    Double Presentage01 = ((sell - DiscP1) * 100) / sell;
                    ItmMark01.Text = Convert.ToString(Math.Round(Presentage01, 2));
                    label35.Text = "Rs." + ItmDisc01.Text;

                }

                else
                {
                    MessageBox.Show("Enterd amount is grater than the Selling price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ItmDisc01.Text = "";
                    ItmDisc01.Focus();
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void ItmDisc02_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (ItmDisc02.Text == "")
                {
                    return;
                }

                Double sell = Convert.ToDouble(ItmSellPrice.Text);
                Double DiscP2 = Convert.ToDouble(ItmDisc02.Text);

                if (sell >= DiscP2)
                {
                    Double Presentage02 = ((sell - DiscP2) * 100) / sell;
                    ItmMark02.Text = Convert.ToString(Math.Round(Presentage02, 2));
                    label36.Text = "Rs." + ItmDisc02.Text;

                }

                else
                {
                    MessageBox.Show("Enterd amount is grater than the Selling price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void ItmDisc03_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (ItmDisc03.Text == "")
                {
                    return;
                }

                Double sell = Convert.ToDouble(ItmSellPrice.Text);
                Double DiscP3 = Convert.ToDouble(ItmDisc03.Text);

                if (sell >= DiscP3)
                {
                    Double Presentage03 = ((sell - DiscP3) * 100) / sell;
                    ItmMark03.Text = Convert.ToString(Math.Round(Presentage03, 2));
                    label37.Text = "Rs." + ItmDisc03.Text;

                }

                else
                {
                    MessageBox.Show("Enterd amount is grater than the Selling price", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void ItmSellPrice_KeyUp(object sender, KeyEventArgs e)
        {
            label33.Text = "Rs."+ItmSellPrice.Text;
        }

        private void ItmStkUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItmOrderUnit.Text = ItmStkUnit.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            PnlItemSearch.Visible = false;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            PnlItemSearch.Visible = true;

            RbNew.Checked = false;
            try
            {

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string VenSelectAll = "SELECT ItmID,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmManufacturer,ItmModel,ItmWarrenty,ItmDescription,ItmVendor,ItmVenID ,ItmStkUnit,ItmReoderLvl,ItmTarQuntity,ItmOrderUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmMark01,ItmDisc02,ItmMark02,ItmDisc03,ItmMark03,ItemBarCode FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "'";
            SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            dataGridView2.Rows.Clear();

            while (dr.Read() == true)
            {
                dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22],dr[23]);

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

        public void lockItemBoxes()
        { 
        ItmID.Enabled=false;
     
      ItmName.Enabled=false;
      ItmCategory.Enabled=false;
      ItmStockType.Enabled=false;
      ItmLocation.Enabled=false;
      ItmManufacturer.Enabled=false;
      ItmModel.Enabled=false;
      ItmWarrenty.Enabled=false;
      ItmDescription.Enabled=false;
      ItmVendor.Enabled=false;
      ItmVenID.Enabled=false;
      ItmStkUnit.Enabled=false;
      ItmReoderLvl.Enabled=false;
      ItmTarQuntity.Enabled=false;
      ItmOrderUnit.Enabled=false;
      ItmOderCost.Enabled=false;
      ItmSellPrice.Enabled=false;
      ItmDisc01.Enabled=false;
      ItmMark01.Enabled=false;
      ItmDisc02.Enabled=false;
      ItmMark02.Enabled=false;
      ItmDisc03.Enabled=false;
      ItmMark03.Enabled=false;

      rbNoWarr.Enabled = false;
      rbYears.Enabled = false;
      rbMonths.Enabled = false;

      button1.Enabled = false;
      button2.Enabled = false;
      button6.Enabled = false;
      label45.Enabled = false;
      label47.Enabled = false;
      button4.Enabled = false;

      txtBarcode.Enabled = false;
      txtBarcodeConfirm.Enabled = false;
      ChkAddBarCode.Enabled = false;

      if (txtBarcode.Text == "")
      {
          ChkAddBarCode.Checked = false;
      }


        }

        public void UnlockItemBoxes()
        {
            
            ItmName.Enabled = true;
            ItmCategory.Enabled = true;
            ItmStockType.Enabled = true;
            ItmLocation.Enabled = true;
            ItmManufacturer.Enabled = true;
            ItmModel.Enabled = true;
            ItmWarrenty.Enabled = true;
            ItmDescription.Enabled = true;
            ItmVendor.Enabled = true;
            ItmVenID.Enabled = true;
            ItmStkUnit.Enabled = true;
            ItmReoderLvl.Enabled = true;
            ItmTarQuntity.Enabled = true;
            ItmOrderUnit.Enabled = true;
            ItmOderCost.Enabled = true;
            ItmSellPrice.Enabled = true;
            ItmDisc01.Enabled = true;
            ItmMark01.Enabled = true;
            ItmDisc02.Enabled = true;
            ItmMark02.Enabled = true;
            ItmDisc03.Enabled = true;
            ItmMark03.Enabled = true;

            rbNoWarr.Enabled = true;
            rbYears.Enabled = true;
            rbMonths.Enabled = true;

            button1.Enabled = true;
            button2.Enabled = true;
            button6.Enabled = true;
            label45.Enabled = true;
            label47.Enabled = true;
            button4.Enabled = true;

            if (ChkAddBarCode.Checked == true)
            {
                 txtBarcode.Enabled = true;
                txtBarcodeConfirm.Enabled = true;
            }

            ChkAddBarCode.Enabled = true;
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView2.SelectedRows[0];

        ItmID.Text = dr.Cells[0].Value.ToString();
      ItmName.Text = dr.Cells[1].Value.ToString();
      ItmCategory.Text = dr.Cells[2].Value.ToString();
      ItmStockType.Text = dr.Cells[3].Value.ToString();
      ItmLocation.Text = dr.Cells[4].Value.ToString();
      ItmManufacturer.Text = dr.Cells[5].Value.ToString();
      ItmModel.Text = dr.Cells[6].Value.ToString();   
      ItmDescription.Text = dr.Cells[8].Value.ToString();
      ItmVendor.Text = dr.Cells[9].Value.ToString();
      ItmVenID .Text = dr.Cells[10].Value.ToString();
      ItmStkUnit.Text = dr.Cells[11].Value.ToString();
      ItmReoderLvl.Text = dr.Cells[12].Value.ToString();
      ItmTarQuntity.Text = dr.Cells[13].Value.ToString();
      ItmOrderUnit.Text = dr.Cells[14].Value.ToString();
      ItmOderCost.Text = dr.Cells[15].Value.ToString();
      ItmSellPrice.Text = dr.Cells[16].Value.ToString();
      ItmDisc01.Text = dr.Cells[17].Value.ToString();
      ItmMark01.Text = dr.Cells[18].Value.ToString();
      ItmDisc02.Text = dr.Cells[19].Value.ToString();
      ItmMark02.Text = dr.Cells[20].Value.ToString();
      ItmDisc03.Text = dr.Cells[21].Value.ToString();
      ItmMark03.Text = dr.Cells[22].Value.ToString();
      String WrnNumber =dr.Cells[7].Value.ToString();

      txtBarcode.Text = dr.Cells[23].Value.ToString();
      txtBarcodeConfirm.Text = dr.Cells[23].Value.ToString();

      if (txtBarcode.Text != "")
      {   
          ChkAddBarCode.Checked = true;
      }

      string WarenyCat = WrnNumber.Substring(0, 1);
      string WWarntyTime = WrnNumber.Substring(1);

        // select the Radio Button
      if (WarenyCat == "0")
      {
          rbNoWarr.Checked = true;
      }

      if (WarenyCat == "1")
      {
          rbYears.Checked = true;
      }

      if (WarenyCat == "2")
      {
          rbMonths.Checked = true;
      }

      ItmWarrenty.Text = WWarntyTime;
        


      #region Select the Image-----------------------------------------------------------------------

      //===================================================================================================
      SqlConnection con1 = new SqlConnection(IMS);
      con1.Open();


      string ItemImage = "SELECT ItmImage FROM NewItemDetails WHERE ItmID='" + dr.Cells[0].Value.ToString() +"'";
      SqlCommand cmd1 = new SqlCommand(ItemImage, con1);
      SqlDataReader dr1 = cmd1.ExecuteReader();

      if (dr1.Read())
      {
      

          // select the Image____________________________________

          //If a pic is available in the Database
          if (dr1[0] != DBNull.Value)
          {

              byte[] img = (byte[])(dr1[0]);

              MemoryStream ms = new MemoryStream(img);
              ItmImage.Image = Image.FromStream(ms);
         
          }
              //If ther is no picture in the database......................
          else
          {
              ItmImage.Image = Inventory_Control_System.Properties.Resources.No_Image_01;
          }
          //______________________________________________________

      
      }

             if (con1.State == ConnectionState.Open)
                {
                con1.Close();
                }
      #endregion

             PnlItemSearch.Visible = false;

                lockItemBoxes();

                //Custermize radio button========================================================
                RbUp.Enabled = true;
                RbUp.Checked = false;

                RbNew.Checked = false;
                RbNew.Enabled = true;

                button4.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            
           
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            UnlockItemBoxes();

            RbUp.Enabled = false;

            getCreateItemCode();

            //clear txt boxes
            ClearItems();

            button4.Text = "&Save";

            CkDeactivated.Enabled = false;
        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {

            if (RbUp.Checked == true)
            {
                UnlockItemBoxes();
                button4.Enabled = true;
                button4.Text = "&Update";
                CkDeactivated.Enabled = true;
            }

            if (RbUp.Checked != true)
            {
                button4.Text = "&Save";
                CkDeactivated.Enabled = false;
            }

        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                textBox3.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmManufacturer,ItmModel,ItmWarrenty,ItmDescription,ItmVendor,ItmVenID ,ItmStkUnit,ItmReoderLvl,ItmTarQuntity,ItmOrderUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmMark01,ItmDisc02,ItmMark02,ItmDisc03,ItmMark03 FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "' AND ItmID LIKE '" + textBox4.Text+ "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22]);

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

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                textBox4.Text = "";

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT ItmID,ItmName,ItmCategory,ItmStockType,ItmLocation,ItmManufacturer,ItmModel,ItmWarrenty,ItmDescription,ItmVendor,ItmVenID ,ItmStkUnit,ItmReoderLvl,ItmTarQuntity,ItmOrderUnit,ItmOderCost,ItmSellPrice,ItmDisc01,ItmMark01,ItmDisc02,ItmMark02,ItmDisc03,ItmMark03 FROM NewItemDetails WHERE ItmActiveOrNot='" + 1 + "' AND ItmName LIKE '" + textBox3.Text + "%'";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22]);

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

        private void CkDeactivated_CheckedChanged(object sender, EventArgs e)
        {
            if (CkDeactivated.Checked == true && RbUp.Checked == true)
            {
                button4.Text = "Deactivate";
            }

            if (CkDeactivated.Checked == false && RbUp.Checked == true)
            {
                button4.Text = "Update";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ItmSellPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')   
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1) 
            {
                e.Handled = true;
            }
        }

        private void ItmDisc01_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ItmDisc02_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ItmDisc03_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ItmOderCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ItmReoderLvl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ItmTarQuntity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ItmWarrenty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            ItmSellPrice.Focus();
        }

        private void ChkAddBarCode_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAddBarCode.Checked == false)
            {
                txtBarcode.Enabled = false;
                txtBarcodeConfirm.Enabled= false;

                txtBarcodeConfirm.Text = "";
                txtBarcode.Text = "";
            }

            if (ChkAddBarCode.Checked == true)
            {
                txtBarcode.Enabled = true;
                txtBarcodeConfirm.Enabled = true;
            }
        }

        private void ItmVenID_Click(object sender, EventArgs e)
        {
            try
            {
                #region load vendors==========================================================================

                SqlConnection con = new SqlConnection(IMS);
                con.Open();

                string selectVen = "SELECT VenderID,VenderName,VenderPHAddress FROM VenderDetails WHERE ActiveDeactive='1' ORDER BY VenderID ASC";
                SqlCommand cmd = new SqlCommand(selectVen, con);
                SqlDataReader dr = cmd.ExecuteReader();

                ItmVenID.Items.Clear();

                while (dr.Read())
                {
                    ItmVenID.Items.Add(dr[0]);
                }
                cmd.Dispose();
                dr.Close();

                if (con.State == ConnectionState.Open)
                {

                    con.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void ItmManufacturer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmModel.Focus();
            }
        }

        private void ItmModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmWarrenty.Focus();
            }
        }

        private void ItmWarrenty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                rbNoWarr.Focus();
            }
        }

        private void rbNoWarr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ChkAddBarCode.Focus();
            }
        }

        private void rbYears_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ChkAddBarCode.Focus();
            }
        }

        private void rbMonths_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ChkAddBarCode.Focus();
            }
        }

        private void ChkAddBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ChkAddBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ChkAddBarCode.Checked == true)
            {
                if (e.KeyValue == 13)
                {
                    txtBarcode.Focus();
                }
            }

            if (ChkAddBarCode.Checked == false)
            {
                if (e.KeyValue == 13)
                {
                    ItmDescription.Focus();
                }
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBarcodeConfirm.Focus();
            }
        }

        private void txtBarcodeConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmDescription.Focus();
            }
        }

        private void ItmReoderLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmTarQuntity.Focus();
            }
        }

        private void ItmTarQuntity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmOrderUnit.DroppedDown = true;
                ItmOrderUnit.Focus();
            }
        }

        private void ItmOrderUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmOderCost.Focus();
            }
        }

        private void ItmSellPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmDisc01.Focus();
            }
        }

        private void ItmDisc01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmDisc02.Focus();
            }
        }

        private void ItmDisc02_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmDisc03.Focus();
            }
        }

        private void label61_Click(object sender, EventArgs e)
        {
            AddVendor adVe = new AddVendor();
            adVe.Visible=true;
        }

        private void NewItemLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13 )
            {
                button12.Focus();
            }
        }

        private void Stkcategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button11.Focus();
            }
        }

        private void ItmName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                ItmCategory.DroppedDown = true;
                ItmCategory.Focus();
            }
        }

        private void ItmCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmStockType.DroppedDown = true;
                ItmStockType.Focus();
            }
        }

        private void ItmLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ItmLocation.Text == "")
            //{
            //    PnlLocation.Visible = true;
            //}
        }

        private void ItmStockType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ItmStockType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (ItmLocation.Items.Count>0)
                {
                    ItmLocation.DroppedDown = true;
                    ItmLocation.Focus();
                }
                if (ItmLocation.Items.Count<=0)
                {
                    PnlLocation.Visible = true;
                    NewItemLocation.Focus();
                }
            }
        }

        private void ItmLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmManufacturer.Focus();
            }
        }

        private void ItmSellPrice_Leave(object sender, EventArgs e)
        {
            if (Double.Parse( ItmSellPrice.Text) <Double.Parse (ItmOderCost.Text))
            {
                MessageBox.Show("Selling price is Less than the order Price..");
                ItmSellPrice.Focus();
                return;
            }
        }

        private void ItmDisc01_Leave(object sender, EventArgs e)
        {
            if (ItmDisc01.Text == "" || Double.Parse(ItmDisc01.Text )==0)
            {
                ItmDisc01.Text = ItmOderCost.Text;
            }
        }

        private void ItmDisc02_Leave(object sender, EventArgs e)
        {
            if (ItmDisc02.Text == "" || Double.Parse(ItmDisc02.Text) == 0)
            {
                ItmDisc02.Text = ItmOderCost.Text;
            }
            if (Double.Parse(ItmDisc02.Text) > Double.Parse(ItmDisc01.Text))
            {
                MessageBox.Show(" Amount is Grater Than to Discount Price 1");
                ItmDisc02.Focus();
                return;
            }
        }

        private void ItmDisc03_Leave(object sender, EventArgs e)
        {
            if (ItmDisc03.Text == "" ||Double.Parse (ItmDisc03.Text) == 0)
            {
                ItmDisc03.Text = ItmOderCost.Text;
            }
            if (Double.Parse(ItmDisc03.Text) > Double.Parse(ItmDisc02.Text))
            {
                MessageBox.Show(" Amount is Grater Than to Discount Price 2");
                ItmDisc03.Focus();
                return;
            }
        }

        private void ItmDisc03_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                button4.Focus();
            }
        }

        private void ItmVenID_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                ItmStkUnit.DroppedDown = true;
                ItmStkUnit.Focus();
            }
        }

        private void ItmStkUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ItmReoderLvl.Focus();
            }
        }
    }
}
