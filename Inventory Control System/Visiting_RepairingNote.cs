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
    public partial class Visiting_RepairingNote : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        


        public Visiting_RepairingNote()
        {
            InitializeComponent();
            GenerateVisitNumbe();
            CustomerId();
            VisitPersonLoad();
            LoadPreviousNote();
            rbtNew.Checked = true;
        }

        public void GenerateVisitNumbe()
        {
            #region New User Number...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT Visit_ID FROM Repair_VisitNote";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    lblVisitingID.Text = "VST1001";
                    // PassInvoiceNumber.Text = "INV1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 Visit_ID FROM Repair_VisitNote order by Visit_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        lblVisitingID.Text = "VST" + no;
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
        }

        public void CustomerId()
        {
            #region Cus ID Load...................................................
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select CusID, CusFirstName, CusLastName,CusPersonalAddress, CusTelNUmber, CusMobileNumber from CustomerDetails";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    cmbCusName.Items.Add(dr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        public void VisitPersonLoad()
        {
            #region Visit Person Load....................................................
            try
            {
              //  cmbVisitedID.Items.Clear();
               // cmbVisitperson.Items.Clear();
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select UserCode, FirstName, LastName from UserProfile";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    cmbVisitperson.Items.Add(dr[0].ToString());
                    cmbVisitedID.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        public void LoadPreviousNote()
        {
            #region Load Previous Note.....................................
            try 
            {
                cmbPrevious.Items.Clear();
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "SELECT  Visit_ID, CusID, CusName, CusAddre, Custel, VisitedPerson, Solution, Conform_Discription FROM Repair_VisitNote";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
               
                while (dr.Read())
                {
                    
                    cmbPrevious.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }
    
    



        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbCusName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            CustomerId();
  
            
        }


        private void cmbVisitperson_SelectedIndexChanged(object sender, EventArgs e)
        {
           // VisitPersonLoad(); 

            #region change visit person wise.........................................
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitperson.Text + "'";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    txtVistPerNAme.Text = dr[1].ToString() + "  " + dr[2].ToString();
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
            #region load vendor details in gridView
            pnlPreviousNotSearch.Visible = true;


            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = @"SELECT  Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, Solution, ConformPerson,VisitedPerson,visited_Date, Visited_time, Date, addUser FROM Repair_VisitNote";
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

        private void button7_Click(object sender, EventArgs e)
        {
            #region load vendor details in gridView
            PnlCustomerSerch.Visible = true;


            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT CusID, CusFirstName, CusLastName,CusPersonalAddress, CusMobileNumber, CusTelNUmber FROM  CustomerDetails";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);

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

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            #region Serach vendor details in gridView
           


            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string VenSelectAll = "SELECT CusID, CusFirstName, CusLastName,CusPersonalAddress, CusMobileNumber, CusTelNUmber FROM  CustomerDetails where CusID like '%" + textBox1.Text + "%' or CusFirstName like '%" + textBox1.Text + "%' or CusLastName like '%" + textBox1.Text + "%' or CusPersonalAddress like '%" + textBox1.Text + "%' or CusMobileNumber like '%" + textBox1.Text + "%' or CusTelNUmber like '%" + textBox1.Text + "%' ";
                SqlCommand cmd1 = new SqlCommand(VenSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView3.Rows.Clear();

                while (dr.Read() == true)
                {
                    dataGridView3.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);

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

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region  value pass to form from customer search panel
            try
            {
                DataGridViewRow dr = dataGridView3.SelectedRows[0];

                cmbCusName.Text = dr.Cells[0].Value.ToString();

                PnlCustomerSerch.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = false;
        }

        string Solution = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Save @ Upadate........................................
            try
            {

                if (rbtPending.Checked == true)
                {
                    Solution = "pending";
                }
                if (rbtVisit.Checked == true)
                {
                    Solution = "Visited";
                }
                if (rbtRepairsucc.Checked == true)
                {
                    Solution = "Repaire Successful";
                }
                if (rbtBorrow.Checked == true)
                {
                    Solution = "Borrow Item";
                }


                //-
                if (cmbCusName.SelectedIndex < 0)
                {
                    MessageBox.Show("Please Fill Dedails....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCusName.Focus();
                    return;
                }
               

                if(cmbCusName.Text=="" || txtAddress.Text=="" || txtcusName.Text=="" ||txtCusTel.Text=="" )
                {
                    MessageBox.Show("Please Full All Details ............", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCusName.DroppedDown = true;
                    cmbCusName.Focus();
                    return;
                }


                if (rbtNew.Checked == true)
                {
                    #region Insert value..............................................

                    if (rbtPending.Checked == false)
                    {
                        MessageBox.Show("Please Select Solution............", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rbtPending.Focus();
                        return;
                    }

                    if (btnSave.Text == "Save")
                    {
                        if (rbtNew.Checked == true)
                        {
                            DialogResult sa = MessageBox.Show("Do You Save This Details ?", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (sa == DialogResult.Yes)
                            {

                                GenerateVisitNumbe();

                                rbtPending.Checked = true;

                                SqlConnection cnn2 = new SqlConnection(IMS);
                                cnn2.Open();
                                String InsertVisit = @"insert into Repair_VisitNote(Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, ConformDate, ConformTime, Conform_Discription, Solution, VisitedPerson,[Visited _Discription],Date, addUser, LastUpdateUser, AddedRepairNotes) values
                                       ('" + lblVisitingID.Text + "','" + cmbCusName.Text + "','" + txtcusName.Text + "','" + txtAddress.Text + "','" + txtCusTel.Text + "','" + cmbVisitperson.Text + "','" + Convert.ToDateTime(DateTimeConformDate.Text).ToShortDateString() + "','" + Convert.ToDateTime(DateTimeConformTime.Text).ToShortTimeString() + "',  '" + txtDiscrip.Text + "','" + Solution + "','" + "-" + "','" + "-" + "','" + DateTime.Now.ToString() + "','" + LgUser.Text + "','" + "-" + "','" + "-" + "')";
                                SqlCommand cmm2 = new SqlCommand(InsertVisit, cnn2);
                                cmm2.ExecuteNonQuery();


                                MessageBox.Show("Insert Successful.....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                               
                                frm_Visiting_Repairing_Report VisitingInsertReport = new frm_Visiting_Repairing_Report();
                                VisitingInsertReport.VisitIDPassToReport = lblVisitingID.Text;
                                VisitingInsertReport.NewInsert = rbtNew.Checked = true;
                                VisitingInsertReport.Show();
                                


                            }
                            if (sa == DialogResult.No)
                            {
                                return;
                            }
                            if (sa == DialogResult.Cancel)
                            {
                                #region All Clear...............................................

                                rbtNew.Checked = false;
                                rbtUpdate.Checked = false;
                                rbtUpdate.Enabled = false;
                                btnSerachCus.Enabled = false;
                                cmbCusName.Enabled = false;
                                txtcusName.Enabled = false;
                                txtCusTel.Enabled = false;
                                txtAddress.Enabled = false;
                                cmbVisitperson.Enabled = false;
                                txtVistPerNAme.Enabled = false;
                                DateTimeConformDate.Enabled = false;
                                DateTimeConformTime.Enabled = false;
                                txtDiscrip.Enabled = false;
                                rbtPending.Enabled = false;
                                cmbPrevious.SelectedIndex = -1;
                                txtDiscrip.Text = "";
                                rbtVisit.Enabled = false;
                                rbtRepairsucc.Enabled = false;
                                rbtBorrow.Enabled = false;
                                cmbVisitedID.Enabled = false;
                                cmbVisitedID.SelectedIndex = -1;
                                rbtVisit.Checked = false;
                                rbtRepairsucc.Checked = false;
                                rbtBorrow.Checked = false;
                                rbtPending.Checked = false;
                                cmbCusName.SelectedIndex = -1;
                                DateTimeConformTime.ResetText();
                                DateTimeConformDate.ResetText();
                                DateTimeVisited_Date.ResetText();
                                DateTimeVisitedTime.ResetText();
                                txtDiscrVisitedPer.Text = "";
                                rbtNew.Checked = true;
                                txtcusName.Text = "";
                                txtCusTel.Text = "";
                                txtAddress.Text = "";
                                cmbVisitperson.SelectedIndex = -1;
                                txtVistPerNAme.Text = "";
                                #endregion
                            }
                        }
                        


                    }
                    #endregion
                }

                if (rbtUpdate.Checked == true)
                {
                    #region Update Query............................................
                     DialogResult sa = MessageBox.Show("Do You UpDate This Details ?", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                     if (sa == DialogResult.Yes)
                     {


                         SqlConnection cnn3 = new SqlConnection(IMS);
                         cnn3.Open();
                         String UpdateVisitNote = @"update Repair_VisitNote set CusID='" + cmbCusName.Text + "', CusName='" + txtcusName.Text + "', CusAddre='" + txtAddress.Text + "', Custel='" + txtCusTel.Text + "', ConformPerson='" + cmbVisitperson.Text + "',  ConformDate='" + Convert.ToDateTime(DateTimeConformDate.Text).ToShortDateString() + "', ConformTime='" + Convert.ToDateTime(DateTimeConformTime.Text).ToShortTimeString() + "', Conform_Discription='" + txtDiscrip.Text + "', Solution='" + Solution + "', VisitedPerson='" + cmbVisitedID.Text + "', visited_Date='" + Convert.ToDateTime(DateTimeVisited_Date.Text).ToShortDateString() + "', Visited_time='" + Convert.ToDateTime(DateTimeVisitedTime.Text).ToShortTimeString() + "', [Visited _Discription]='" + txtDiscrVisitedPer.Text + "', LastUpdateUser='" + LgUser.Text + "', LastUpdate_date='" + DateTime.Now.ToShortDateString() + "' where Visit_ID='" + cmbPrevious.Text + "'  ";
                        // MessageBox.Show(UpdateVisitNote);
                         SqlCommand cmm3 = new SqlCommand(UpdateVisitNote, cnn3);
                         cmm3.ExecuteNonQuery();


                         MessageBox.Show("UpDate Successful.....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         frm_Visiting_Repairing_Report VisitingInsertReport = new frm_Visiting_Repairing_Report();
                         VisitingInsertReport.VisitIDPassToReport = cmbPrevious.Text;
                         VisitingInsertReport.NewInsert = rbtNew.Checked = false;
                         VisitingInsertReport.Show();

                     }
                     if (sa == DialogResult.No)
                     {
                         return;
                     }
                     if (sa == DialogResult.Cancel)
                     {
                         #region All Clear...............................................

                         rbtNew.Checked = false;
                         rbtUpdate.Checked = false;
                         rbtUpdate.Enabled = false;
                         btnSerachCus.Enabled = false;
                         cmbCusName.Enabled = false;
                         txtcusName.Enabled = false;
                         txtCusTel.Enabled = false;
                         txtAddress.Enabled = false;
                         cmbVisitperson.Enabled = false;
                         txtVistPerNAme.Enabled = false;
                         DateTimeConformDate.Enabled = false;
                         DateTimeConformTime.Enabled = false;
                         txtDiscrip.Enabled = false;
                         rbtPending.Enabled = false;
                         cmbPrevious.SelectedIndex = -1;
                         txtDiscrip.Text = "";
                         rbtVisit.Enabled = false;
                         rbtRepairsucc.Enabled = false;
                         rbtBorrow.Enabled = false;
                         cmbVisitedID.Enabled = false;
                         cmbVisitedID.SelectedIndex = -1;
                         rbtVisit.Checked = false;
                         rbtRepairsucc.Checked = false;
                         rbtBorrow.Checked = false;
                         rbtPending.Checked = false;
                         cmbCusName.SelectedIndex = -1;
                         DateTimeConformTime.ResetText();
                         DateTimeConformDate.ResetText();
                         DateTimeVisited_Date.ResetText();
                         DateTimeVisitedTime.ResetText();
                         txtDiscrVisitedPer.Text = "";
                         #endregion
                     }

                    

                    #endregion
                }
                #region All Clear...............................................

                rbtNew.Checked = false;
                rbtUpdate.Checked = false;
                rbtUpdate.Enabled = false;
                btnSerachCus.Enabled = false;
                cmbCusName.Enabled = false;
                txtcusName.Enabled = false;
                txtCusTel.Enabled = false;
                txtAddress.Enabled = false;
                cmbVisitperson.Enabled = false;
                txtVistPerNAme.Enabled = false;
                DateTimeConformDate.Enabled = false;
                DateTimeConformTime.Enabled = false;
                txtDiscrip.Enabled = false;
                rbtPending.Enabled = false;
                cmbPrevious.SelectedIndex = -1;
                txtDiscrip.Text = "";
                rbtVisit.Enabled = false;
                rbtRepairsucc.Enabled = false;
                rbtBorrow.Enabled = false;
                cmbVisitedID.Enabled = false;
                cmbVisitedID.SelectedIndex = -1;
                rbtVisit.Checked = false;
                rbtRepairsucc.Checked = false;
                rbtBorrow.Checked = false;
                rbtPending.Checked = false;
                cmbCusName.SelectedIndex = -1;
                DateTimeConformTime.ResetText();
                DateTimeConformDate.ResetText();
                DateTimeVisited_Date.ResetText();
                DateTimeVisitedTime.ResetText();
                txtDiscrVisitedPer.Text = "";
                rbtNew.Checked = true;
                txtcusName.Text = "";
                txtCusTel.Text = "";
                txtAddress.Text = "";
                cmbVisitperson.SelectedIndex = -1;
                txtVistPerNAme.Text = "";
                #endregion

                GenerateVisitNumbe();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

           
            #endregion

        }

        private void Visiting_RepairingNote_Load(object sender, EventArgs e)
        {

        }

        private void rbtNew_CheckedChanged(object sender, EventArgs e)
        {
            #region if  rbtnew checked OR unchecked chgange solution..........................
            try
            {

                GenerateVisitNumbe();
                if (rbtNew.Checked == true)
                {
                    btnSave.Enabled = true;
                    btnSave.Text = "Save";
                    btnSerachCus.Enabled = true;
                    cmbCusName.Enabled = true;
                    txtcusName.Enabled = true;
                    txtCusTel.Enabled = true;
                    txtAddress.Enabled = true;
                    cmbVisitperson.Enabled = true;
                    txtVistPerNAme.Enabled = true;
                    DateTimeConformDate.Enabled = true;
                    DateTimeConformTime.Enabled = true;
                    txtDiscrip.Enabled = true;
                    rbtPending.Enabled = true;
                    cmbPrevious.SelectedIndex = -1;
                    txtDiscrip.Text = "";
                    //dateTimePicker1.Refresh();
                    DateTimeConformTime.ResetText();
                    DateTimeConformDate.ResetText();
                    DateTimeVisited_Date.ResetText();
                    DateTimeVisitedTime.ResetText();
                    txtDiscrVisitedPer.Enabled = false; ;
                   
                    rbtVisit.Enabled = false;
                    rbtRepairsucc.Enabled = false;
                    rbtBorrow.Enabled = false;
                    cmbVisitedID.Enabled = false;
                    cmbVisitedID.SelectedIndex = -1;
                    rbtBorrow.Checked = false;
                    rbtRepairsucc.Checked = false;
                    rbtVisit.Checked = false;
                    rbtPending.Checked = true;
                    DateTimeVisited_Date.ResetText();
                    DateTimeVisitedTime.ResetText() ;
                    DateTimeVisited_Date.Enabled = false;
                    DateTimeVisitedTime.Enabled = false;
                }
                if (rbtNew.Checked == false)
                {
                    btnSerachCus.Enabled = false;
                    cmbCusName.Enabled = false;
                    txtcusName.Enabled = false;
                    txtCusTel.Enabled = false;
                    txtAddress.Enabled = false;
                    cmbVisitperson.Enabled = false;
                    txtVistPerNAme.Enabled = false;
                    DateTimeConformDate.Enabled = false;
                    DateTimeConformTime.Enabled = false;
                    txtDiscrip.Enabled = false;
                    rbtPending.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void cmbPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   LoadPreviousNote();

            #region Load Previous Note.....................................
            cmbCusName.Items.Clear();
            cmbVisitedID.Items.Clear();
            cmbVisitperson.Items.Clear();
            txtAddress.Text = "";
            txtcusName.Text = "";
            txtCusTel.Text = "";
            txtVisitedName.Text = "";
            txtVistPerNAme.Text = "";
           // txtAddress.Text = "";

            CustomerId();
            VisitPersonLoad();

            
            try
            {


                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = @"SELECT  Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, ConformDate, ConformTime,Conform_Discription, Solution, VisitedPerson,
                visited_Date, Visited_time, [Visited _Discription], Date, addUser, LastUpdateUser, LastUpdate_date, AddedRepairNotes FROM Repair_VisitNote where Visit_ID='" + cmbPrevious.Text + "' ";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                
                while (dr.Read())
                {
                   // #region
                    lblVisitingID.Text ="--";
                    cmbPrevious.Text = dr[0].ToString();
                    cmbCusName.Text = dr[1].ToString(); 
                    txtcusName.Text = dr[2].ToString();
                    txtAddress.Text = dr[3].ToString();
                    txtCusTel.Text = dr[4].ToString();
                    cmbVisitperson.Text = dr[5].ToString();
                    DateTimeConformDate.Text = dr[6].ToString();
                    DateTimeConformTime.Text = dr[7].ToString();
                    txtDiscrip.Text = dr[8].ToString();


                    if (dr[9].ToString() == "pending")
                    {
                        rbtPending.Checked = true;
                    }
                    if (dr[9].ToString() == "Visited")
                    {
                        rbtVisit.Checked = true;
                    }
                    if (dr[9].ToString() == "Repaire Successful")
                    {
                        rbtRepairsucc.Checked = true;
                    }
                    if (dr[9].ToString() == "Borrow Item")
                    {
                        rbtBorrow.Checked = true;
                    }
                 //   #endregion

                    cmbVisitperson.Text = dr[10].ToString();
                    DateTimeVisited_Date.Text = dr[11].ToString();
                    DateTimeVisitedTime.Text = dr[12].ToString();
                    txtDiscrVisitedPer.Text = dr[13].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion


            if (cmbPrevious.SelectedIndex >= 0)
            {
                #region if Previous loane id >=0...............................

                rbtNew.Checked = false;
                rbtUpdate.Checked = false;
                rbtUpdate.Enabled = true;
                btnSerachCus.Enabled = false;
                cmbCusName.Enabled = false;
                txtcusName.Enabled = false;
                txtCusTel.Enabled = false;
                txtAddress.Enabled = false;
                cmbVisitperson.Enabled = false;
                txtVistPerNAme.Enabled = false;
                DateTimeConformDate.Enabled = false;
                DateTimeConformTime.Enabled = false;
                txtDiscrip.Enabled = false;
                rbtPending.Enabled = false;
                DateTimeConformDate.Enabled = false;
                DateTimeConformTime.Enabled = false;
                cmbVisitedID.Enabled = false;
                txtVisitedName.Enabled = false;
                rbtVisit.Enabled = false;
                rbtRepairsucc.Enabled = false;
                rbtBorrow.Enabled = false;
            }


                #endregion
        }

        private void cmbVisitedID_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisitPersonLoad();
            
            #region select visited Person...........................................................
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitperson.Text + "'";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    txtVisitedName.Text = dr[1].ToString() + "  " + dr[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            #region Search Previous.........................................................

            try
            {
             SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string VenSelectAll = @"SELECT  Visit_ID, CusID, CusName, CusAddre, Custel, ConformPerson, Solution, ConformPerson,VisitedPerson,visited_Date, Visited_time, Date, addUser FROM Repair_VisitNote where Visit_ID like '%" + textBox2.Text + "%'OR CusID like '%" + textBox2.Text + "%' OR CusName like '%" + textBox2.Text + "%' OR CusAddre like '%" + textBox2.Text + "%' OR Custel like '%" + textBox2.Text + "%' OR ConformPerson like '%" + textBox2.Text + "%' OR Solution like '%" + textBox2.Text + "%' OR VisitedPerson like '%" + textBox2.Text + "%'";
                SqlCommand cmd1=new SqlCommand(VenSelectAll,con1);
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

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           # region from data gride view to Textbox...................................................

            DataGridViewRow dr = dtagridSearchprevious.SelectedRows[0];

            lblVisitingID.Text = "--";
            cmbPrevious.Text = dr.Cells[0].Value.ToString();
            cmbCusName.Text = dr.Cells[1].Value.ToString();
            txtcusName.Text = dr.Cells[2].Value.ToString();
            txtAddress.Text = dr.Cells[3].Value.ToString();
            txtCusTel.Text = dr.Cells[4].Value.ToString();
            cmbVisitperson.Text = dr.Cells[5].Value.ToString();




            if (dr.Cells[6].Value.ToString() == "pending")
            {
                rbtPending.Checked = true;
            }
            if (dr.Cells[6].Value.ToString() == "Visited")
            {
                rbtVisit.Checked = true;
            }
            if (dr.Cells[6].Value.ToString() == "Repaire Successful")
            {
                rbtRepairsucc.Checked = true;
            }
            if (dr.Cells[6].Value.ToString() == "Borrow Item")
            {
                rbtBorrow.Checked = true;
            }


            txtDiscrip.Text = dr.Cells[7].Value.ToString();
            cmbVisitedID.Text = dr.Cells[8].Value.ToString();
            DateTimeConformDate.Text = dr.Cells[9].Value.ToString();
            DateTimeConformTime.Text = dr.Cells[10].Value.ToString();
            // lblVisitingID.Text = dr.Cells[11].Value.ToString();
            // lblVisitingID.Text = dr.Cells[12].Value.ToString();

            pnlPreviousNotSearch.Visible = false;
           #endregion

        }

        private void cmbPrevious_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbCusName_TextChanged(object sender, EventArgs e)
        {
            #region Text change/........................................................
            try
            {
               
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                
                String Cusid = "select CusID, CusFirstName, CusLastName,CusPersonalAddress, CusTelNUmber, CusMobileNumber from CustomerDetails where CusID='" + cmbCusName.Text + "' ";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
               
                if (dr.Read())
                {
                    cmbCusName.Text = dr[0].ToString();
                    txtcusName.Text = dr[1].ToString() + "  " + dr[2].ToString();
                    // txtsecondName.Text = dr[2].ToString();
                    txtAddress.Text = dr[3].ToString();

                    if (dr[4].ToString() == "")
                    {
                        txtCusTel.Text = dr[5].ToString();
                    }
                    if (dr[4].ToString() != "")
                    {
                        txtCusTel.Text = dr[4].ToString();
                    }
                    if (dr[4].ToString() == "" && dr[4].ToString() == "")
                    {
                        txtCusTel.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void rbtUpdate_CheckedChanged(object sender, EventArgs e)
        {
            #region if Change OR not for update button
            if (rbtUpdate.Checked == true)
            {
               
                btnSave.Enabled = true;
               
                btnSerachCus.Enabled = true;
                cmbCusName.Enabled = true;
                txtcusName.Enabled = true;
                txtCusTel.Enabled = true;
                txtAddress.Enabled = true;
                cmbVisitperson.Enabled = true;
                txtVistPerNAme.Enabled = false;
                DateTimeConformDate.Enabled = true;
                DateTimeConformTime.Enabled = true;
                txtDiscrip.Enabled = true;
                rbtPending.Enabled = true;
                cmbVisitedID.Enabled = true;
                rbtUpdate.Enabled = true;
                rbtRepairsucc.Enabled = true;
                rbtVisit.Enabled = true;
                rbtBorrow.Enabled = true;
                cmbVisitedID.Enabled = true;
                DateTimeVisited_Date.Enabled = true;
                DateTimeVisitedTime.Enabled = true;
                txtDiscrVisitedPer.Enabled = true;

                btnSave.Text = "UpDate";


            }
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            #region All Clear...............................................

            rbtNew.Checked = false;
            rbtUpdate.Checked = false;
            rbtUpdate.Enabled = false;
            btnSerachCus.Enabled = false;
            cmbCusName.Enabled = false;
            txtcusName.Enabled = false;
            txtCusTel.Enabled = false;
            txtAddress.Enabled = false;
            cmbVisitperson.Enabled = false;
            txtVistPerNAme.Enabled = false;
            DateTimeConformDate.Enabled = false;
            DateTimeConformTime.Enabled = false;
            txtDiscrip.Enabled = false;
            rbtPending.Enabled = false;
            cmbPrevious.SelectedIndex = -1;
            txtDiscrip.Text = "";            
            rbtVisit.Enabled = false;
            rbtRepairsucc.Enabled = false;
            rbtBorrow.Enabled = false;
            cmbVisitedID.Enabled = false;
            cmbVisitedID.SelectedIndex = -1;
            rbtVisit.Checked = false;
            rbtRepairsucc.Checked = false;
            rbtBorrow.Checked = false;
            rbtPending.Checked = false;
            cmbCusName.SelectedIndex = -1;
            DateTimeConformTime.ResetText();
            DateTimeConformDate.ResetText();
            DateTimeVisited_Date.ResetText();
            DateTimeVisitedTime.ResetText();
            txtDiscrVisitedPer.Text = "";
            rbtNew.Checked = true;
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlPreviousNotSearch.Visible = false;
        }

        private void cmbPrevious_Click(object sender, EventArgs e)
        {
            LoadPreviousNote();
        }

        private void cmbVisitedID_Click(object sender, EventArgs e)
        {
          //  cmbVisitedID.Items.Clear();
           // VisitPersonLoad();
            //#region change visit person wise.........................................
            //try
            //{
            //    SqlConnection cnn1 = new SqlConnection(IMS);
            //    cnn1.Open();
            //    String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitperson.Text + "'";
            //    SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
            //    SqlDataReader dr = cmm1.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        txtVisitedName.Text = dr[1].ToString() + "  " + dr[2].ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //}
            //#endregion
        }

        private void cmbPrevious_Click_1(object sender, EventArgs e)
        {
           // cmbPrevious.Items.Clear();
            LoadPreviousNote();
            #region Load Previous Note.....................................
            cmbCusName.Items.Clear();
            cmbVisitedID.Items.Clear();
            cmbVisitperson.Items.Clear();
            txtAddress.Text = "";
            txtcusName.Text = "";
            txtCusTel.Text = "";
            txtVisitedName.Text = "";
            txtVistPerNAme.Text = "";
            // txtAddress.Text = "";

            CustomerId();
            VisitPersonLoad();


            try
            {


                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = @"select  Visit_ID,CusID,CusName, CusAddre, Custel, ConformPerson, ConformDate, ConformTime, Conform_Discription, Solution, VisitedPerson,visited_Date, Visited_time, [Visited _Discription], Date, addUser, LastUpdateUser, LastUpdate_date, AddedRepairNotes FROM Repair_VisitNote where Visit_ID='" + cmbPrevious.Text + "' ";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();

                while (dr.Read())
                {

                    lblVisitingID.Text = dr[0].ToString();
                    cmbPrevious.Text = dr[0].ToString();
                    cmbCusName.Text = dr[1].ToString();
                    txtcusName.Text = dr[2].ToString();
                    txtAddress.Text = dr[3].ToString();
                    txtCusTel.Text = dr[4].ToString();
                    cmbVisitperson.Text = dr[5].ToString();
                    DateTimeConformDate.Text = dr[6].ToString();
                    DateTimeConformTime.Text = dr[7].ToString();
                    txtDiscrip.Text = dr[8].ToString();


                    if (dr[9].ToString() == "pending")
                    {
                        rbtPending.Checked = true;
                    }
                    if (dr[9].ToString() == "Visited")
                    {
                        rbtVisit.Checked = true;
                    }
                    if (dr[9].ToString() == "Repaire Successful")
                    {
                        rbtRepairsucc.Checked = true;
                    }
                    if (dr[9].ToString() == "Borrow Item")
                    {
                        rbtBorrow.Checked = true;
                    }
                    //   #endregion

                    cmbVisitperson.Text = dr[10].ToString();
                    DateTimeVisited_Date.Text = dr[11].ToString();
                    DateTimeVisitedTime.Text = dr[12].ToString();
                    txtDiscrVisitedPer.Text = dr[13].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion


            if (cmbPrevious.SelectedIndex >= 0)
            {
                #region if Previous loane id >=0...............................

                rbtNew.Checked = false;
                rbtUpdate.Checked = false;
                rbtUpdate.Enabled = true;
                btnSerachCus.Enabled = false;
                cmbCusName.Enabled = false;
                txtcusName.Enabled = false;
                txtCusTel.Enabled = false;
                txtAddress.Enabled = false;
                cmbVisitperson.Enabled = false;
                txtVistPerNAme.Enabled = false;
                DateTimeConformDate.Enabled = false;
                DateTimeConformTime.Enabled = false;
                txtDiscrip.Enabled = false;
                rbtPending.Enabled = false;
                DateTimeConformDate.Enabled = false;
                DateTimeConformTime.Enabled = false;
                cmbVisitedID.Enabled = false;
                txtVisitedName.Enabled = false;
                rbtVisit.Enabled = false;
                rbtRepairsucc.Enabled = false;
                rbtBorrow.Enabled = false;
            }


                #endregion
        }

        private void cmbCusName_Click(object sender, EventArgs e)
        {
            cmbCusName.Items.Clear();
            CustomerId();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void cmbVisitperson_Click(object sender, EventArgs e)
        {
            cmbVisitperson.Items.Clear();
            VisitPersonLoad();
            #region change visit person wise.........................................
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitperson.Text + "'";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    txtVistPerNAme.Text = dr[1].ToString() + "  " + dr[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void cmbVisitedID_SelectedIndexChanged_1(object sender, EventArgs e)
        {

           // VisitPersonLoad();

            #region change visit person wise.........................................
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitedID.Text + "'";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    txtVisitedName.Text = dr[1].ToString() + "  " + dr[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void cmbVisitedID_Click_1(object sender, EventArgs e)
        {
            cmbVisitedID.Items.Clear();
            VisitPersonLoad();
             #region change visit person wise.........................................
             try
             {
                 SqlConnection cnn1 = new SqlConnection(IMS);
                 cnn1.Open();
                 String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitperson.Text + "'";
                 SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                 SqlDataReader dr = cmm1.ExecuteReader();
                 while (dr.Read())
                 {
                     txtVisitedName.Text = dr[1].ToString() + "  " + dr[2].ToString();
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
             }
             #endregion
        }

        private void cmbVisitedID_TextChanged(object sender, EventArgs e)
        {
            #region change visit person wise.........................................
            try
            {
                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String Cusid = "select UserCode, FirstName, LastName from UserProfile where UserCode='" + cmbVisitedID.Text + "'";
                SqlCommand cmm1 = new SqlCommand(Cusid, cnn1);
                SqlDataReader dr = cmm1.ExecuteReader();
                while (dr.Read())
                {
                    txtVisitedName.Text = dr[1].ToString() + "  " + dr[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void label6_Click(object sender, EventArgs e)
        {
            CustomerReg customerNew = new CustomerReg();
            customerNew.Visible = true;
        }
    }
}
