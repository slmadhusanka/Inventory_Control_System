using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Inventory_Control_System
{
    public partial class Barcode_builder_For_GRNs : Form
    {
        public Barcode_builder_For_GRNs()
        {
            InitializeComponent();
        }

        BarcodeLib.Barcode b = new BarcodeLib.Barcode();

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        private void Barcode_builder_For_GRNs_Load(object sender, EventArgs e)
        {
            try
            {
                #region load all wholesale items ..............................................................................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string SelectAvailableBArcode = @"SELECT GRNWholesaleItems.ItemID, NewItemDetails.ItmName, GRNWholesaleItems.BatchNumber, GRNWholesaleItems.BarCodeID, GRNWholesaleItems.ItemAdded, GRNWholesaleItems.SellingPrice,GRNWholesaleItems.GrnAutiID,GRNWholesaleItems.GRNNumber
                                                    FROM GRNWholesaleItems INNER JOIN
                                                    NewItemDetails ON GRNWholesaleItems.ItemID = NewItemDetails.ItmID
                                                    WHERE (GRNWholesaleItems.AvailbleItemCount <> 0) ORDER BY GRNWholesaleItems.ItemID";

                SqlCommand cmd1 = new SqlCommand(SelectAvailableBArcode, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                LstBatchList.Items.Clear();

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

                    LstBatchList.Items.Add(li);

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

        private void Txtserch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                #region load all wholesale items in serch txtbox..............................................................................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string SelectAvailableBArcode = @"SELECT GRNWholesaleItems.ItemID, NewItemDetails.ItmName, GRNWholesaleItems.BatchNumber, GRNWholesaleItems.BarCodeID, GRNWholesaleItems.ItemAdded, GRNWholesaleItems.SellingPrice,GRNWholesaleItems.GrnAutiID,GRNWholesaleItems.GRNNumber
                                                    FROM GRNWholesaleItems INNER JOIN
                                                    NewItemDetails ON GRNWholesaleItems.ItemID = NewItemDetails.ItmID
                                                    WHERE (GRNWholesaleItems.AvailbleItemCount <> 0) AND (GRNWholesaleItems.ItemID Like'%" + Txtserch.Text + "%' OR GRNWholesaleItems.BarCodeID Like '%" + Txtserch.Text + "%'OR GRNWholesaleItems.BatchNumber like '%" + Txtserch.Text + "%' OR GRNWholesaleItems.GRNNumber like '%" + Txtserch.Text + "%' ) ORDER BY GRNWholesaleItems.ItemID";

                SqlCommand cmd1 = new SqlCommand(SelectAvailableBArcode, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                LstBatchList.Items.Clear();

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

                    LstBatchList.Items.Add(li);

                }//close while

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

        private void LstBatchList_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                string ITmBatch= LstBatchList.SelectedItems[0].SubItems[6].Text;
                string GRNNUm = LstBatchList.SelectedItems[0].SubItems[6].Text;

                for (int i = 0; i <= LstBarcodeToPrint.Items.Count - 1; i++)
                {
                   
                    if (LstBarcodeToPrint.Items[i].SubItems[6].Text == ITmBatch)
                    {
                        MessageBox.Show("This Item is Also in the list. please try another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }//close if

                }//close for

                ListViewItem li;
                li = new ListViewItem(LstBatchList.SelectedItems[0].SubItems[0].Text);

                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[1].Text);
                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[2].Text);
                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[3].Text);
                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[4].Text);
                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[5].Text);
                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[6].Text);
                li.SubItems.Add(LstBatchList.SelectedItems[0].SubItems[7].Text);

                LstBarcodeToPrint.Items.Add(li);

            }//close try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void LstBarcodeToPrint_DoubleClick(object sender, EventArgs e)
        {
            LstBarcodeToPrint.SelectedItems[0].Remove();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                IMSDataSET barcodeDetails = new IMSDataSET();

                DataTable dataTable = barcodeDetails.Barcodes;

                MemoryStream ms;

                BarcodeReport Report = new BarcodeReport();

                int blank_labels = 0;

                for (int x = 0; x <= LstBarcodeToPrint.Items.Count - 1; x++)
                {

                    for (int i = 0; i < Convert.ToDouble(LstBarcodeToPrint.Items[x].SubItems[4].Text); i++)
                    {

                        int W = Convert.ToInt32(230);
                        int H = Convert.ToInt32(40);
                        b.Alignment = BarcodeLib.AlignmentPositions.CENTER;

                        b.Alignment = BarcodeLib.AlignmentPositions.LEFT;

                        DataRow drow = dataTable.NewRow();

                        BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
                        type = BarcodeLib.TYPE.CODE128;


                        if (type != BarcodeLib.TYPE.UNSPECIFIED)
                        {
                            b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), "rotatenoneflipnone", true);

                            b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMLEFT;
                        }


                        if (blank_labels <= i)
                        {

                            drow["ItemID"] = LstBarcodeToPrint.Items[x].SubItems[0].Text;

                            drow["BarCodeID"] = BarCode.BarcodeConverter128.StringToBarcode(LstBarcodeToPrint.Items[x].SubItems[3].Text);

                            drow["BarcodeText"] = LstBarcodeToPrint.Items[x].SubItems[3].Text;
                            drow["BatchNumber"] = LstBarcodeToPrint.Items[x].SubItems[2].Text;
                            drow["SellingPrice"] = LstBarcodeToPrint.Items[x].SubItems[5].Text;
                            drow["ItmName"] = LstBarcodeToPrint.Items[x].SubItems[1].Text;

                        }
                        dataTable.Rows.Add(drow);
                    }

                }

                Report.Database.Tables["Barcodes"].SetDataSource((DataTable)dataTable);


                RptBarcodeViewer.ReportSource = Report;
                RptBarcodeViewer.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

    }
}
