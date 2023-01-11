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
using System.Data.Sql;

namespace Inventory_Control_System
{
    public partial class change_Discount_Price : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string dis1, dis2, dis3;

        public change_Discount_Price()
        {
            InitializeComponent();
        }

        private void change_Discount_Price_Load(object sender, EventArgs e)
        {
           selectlistveiw();

           Txtserch.Focus();
        }

        public void selectlistveiw()
        {
            #region insert data in to listview
            try{
            SqlConnection con = new SqlConnection(IMS);
            con.Open();
            string selectlist = @"SELECT GRNWholesaleItems.ItemID, NewItemDetails.ItmName, GRNWholesaleItems.BarCodeID,GRNWholesaleItems.BatchNumber,GRNWholesaleItems.PerchPrice,GRNWholesaleItems.SellingPrice,GRNWholesaleItems.ItmDisc01,GRNWholesaleItems.ItmDisc02,GRNWholesaleItems.ItmDisc03 
            from NewItemDetails INNER JOIN
            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID where AvailbleItemCount>0 ";
            SqlCommand cmd = new SqlCommand(selectlist, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            listView1.Items.Clear();
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
                li.SubItems.Add(dr[8].ToString());



                listView1.Items.Add(li);

                Txtserch.Focus();
            }
            }
               catch (Exception ex)
                 {
                MessageBox.Show(ex.Message,"System Error", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                }
            

            #endregion

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
           
           

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            #region pass value in to textbox

            try
            {
                ListViewItem item = listView1.SelectedItems[0];

                TxtItemID.Text = item.SubItems[0].Text;
                txtItemName.Text = item.SubItems[1].Text;
                txtBarcode.Text = item.SubItems[2].Text;
                txtBatchNu.Text = item.SubItems[3].Text;
                txtpurches.Text = item.SubItems[4].Text;
                txtSelling.Text = item.SubItems[5].Text;
                txtDisc1.Text = dis1 = item.SubItems[6].Text;

                txtDisc2.Text = dis2 = item.SubItems[7].Text;
                txtDisc3.Text = dis3 = item.SubItems[8].Text;

            }
             
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            btnUpdate.Enabled = true;

            txtDisc1.Focus();
            #endregion
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Double a = Convert.ToDouble(txtSelling.Text);
                Double b = Convert.ToDouble(txtDisc1.Text);
                Double c = Convert.ToDouble(txtDisc2.Text);
                Double d = Convert.ToDouble(txtDisc3.Text);
                //string msg = "";

                //ceck Selling price and Order Price_________________________________________________________________________________________
                if (b > a || c > a || d > a || c > b || d > b || d > c)
                {
                    MessageBox.Show("please compair the price list that you enter the textboxes. The Order must be Selling pirce > discount 01>discount 02>discount 03");
                    txtDisc1.Focus();
                    return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

                #region update price & discount
                try
                {
                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();
                    string updateChangeDisc = "UPDATE GRNWholesaleItems SET SellingPrice='" + txtSelling.Text + "', ItmDisc01='" + txtDisc1.Text + "',ItmDisc02='" + txtDisc2.Text + "',ItmDisc03='" + txtDisc3.Text + "' where ItemID='" + TxtItemID.Text + "'";
                    SqlCommand cmd = new SqlCommand(updateChangeDisc, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtItemID.Text = "";
                    txtItemName.Text = "";
                    txtBarcode.Text = "";
                    txtBatchNu.Text = "";
                    txtpurches.Text = "0.00";
                    txtSelling.Text = "0.00";
                    txtDisc1.Text = "0.00";
                    txtDisc2.Text = "0.00";
                    txtDisc3.Text = "0.00";
                    Txtserch.Text = "";
                    btnUpdate.Enabled = false;
                    Txtserch.Focus();

                    selectlistveiw();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                #endregion
           
        }

        private void TxtItemID_KeyUp(object sender, KeyEventArgs e)
        {
           
           
           
        }

        private void Txtserch_KeyUp(object sender, KeyEventArgs e)
        {
           
                #region search itemid,batchid
                try
                {
                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();
                    string selectlist = @"SELECT GRNWholesaleItems.ItemID, NewItemDetails.ItmName, GRNWholesaleItems.BarCodeID,GRNWholesaleItems.BatchNumber,GRNWholesaleItems.PerchPrice,GRNWholesaleItems.SellingPrice,GRNWholesaleItems.ItmDisc01,GRNWholesaleItems.ItmDisc02,GRNWholesaleItems.ItmDisc03 
            from NewItemDetails INNER JOIN
            GRNWholesaleItems ON NewItemDetails.ItmID = GRNWholesaleItems.ItemID where AvailbleItemCount>0 AND (GRNWholesaleItems.ItemID Like'%" + Txtserch.Text + "%' OR GRNWholesaleItems.BarCodeID Like '%" + Txtserch.Text + "%'OR GRNWholesaleItems.BatchNumber like '%" + Txtserch.Text + "%' )";
                    SqlCommand cmd = new SqlCommand(selectlist, con);
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    listView1.Items.Clear();
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
                        li.SubItems.Add(dr[8].ToString());



                        listView1.Items.Add(li);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }

                #endregion
          
        }

        private void change_Discount_Price_Activated(object sender, EventArgs e)
        {
            Txtserch.Focus();
        }

        private void txtDisc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one dash point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void txtDisc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one dash point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDisc3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one dash point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDisc1_Leave(object sender, EventArgs e)
        {
            if (txtDisc1.Text == "")
            {
                txtDisc1.Text = dis1;
            }
        }

        private void txtDisc2_Leave(object sender, EventArgs e)
        {
            if (txtDisc2.Text == "")
            {
                txtDisc2.Text = dis2;
            }
        }

        private void txtDisc3_Layout(object sender, LayoutEventArgs e)
        {
            if (txtDisc3.Text == "")
            {
                txtDisc3.Text = dis3;
            }
        }

        private void txtDisc1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtDisc2.Focus();
            }
        }

        private void txtDisc2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtDisc3.Focus();
            }
        }

        private void txtDisc3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnUpdate.Focus();
            }
        }
    }
}
