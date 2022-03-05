using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDesktopEnviourment.Core_Widgets
{
    public partial class CalenderView : Form
    {
        public CalenderView()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = string.Format("{0:hh:mm:ss tt}", DateTime.Now);
        }
    }
}
