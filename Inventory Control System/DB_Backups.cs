using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Inventory_Control_System
{
    public partial class DataBase_Backups : Form
    {
      
        public DataBase_Backups()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader reader;
        string sql = "";
        


        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_Backp_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con3 = new SqlConnection(IMS);
                con3.Open();

                sql = "BACKUP DATABASE " + txt_BataBase.Text + " TO DISK='" + txt_Backup_Location.Text + "\\" + txt_BataBase.Text + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                command = new SqlCommand(sql, con3);
                command.ExecuteNonQuery();

                MessageBox.Show("DataBase Backup Successfull");

                txt_Backup_Location.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a folder or check whether the database is correct or not.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_Backup_Brwuse_Click(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txt_Backup_Location.Text = dlg.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void DataBase_Backups_Load(object sender, EventArgs e)
        {

        }
    }
}
