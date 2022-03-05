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
    public partial class ShutdownFade : Form
    {
        public bool IsShuttingDown = false;
        public ShutdownFade()
        {
            InitializeComponent();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.07;

            if(this.Opacity >= 1.00)
            {
                await Task.Delay(1000);
                Application.Exit();
            }
        }
    }
}
