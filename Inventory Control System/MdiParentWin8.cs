using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Control_System
{
    public partial class MdiParentWin8 : Form
    {
        public MdiParentWin8()
        {
            InitializeComponent();
        }

        private void backstageViewControl1_Click(object sender, EventArgs e)
        {
           // wigt


        }

        private void MdiParentWin8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode.ToString() == "F")
            {
                MessageBox.Show("asdasd");
            }
        }
    }
}
