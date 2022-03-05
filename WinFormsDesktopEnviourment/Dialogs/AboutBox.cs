using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDesktopEnviourment
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            label1.Text = Properties.Settings.Default.WindowsVersion;
            label2.Text = Properties.Settings.Default.ProcessorName;
            label3.Text = Properties.Settings.Default.RamAmount;
            label4.Text = Properties.Settings.Default.GraphicsName;
            label5.Text = Application.ProductName;
            label6.Text = "Version : " + Application.ProductVersion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
