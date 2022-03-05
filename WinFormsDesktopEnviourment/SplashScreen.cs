using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;

namespace WinFormsDesktopEnviourment
{
    public partial class SplashScreen : Form
    {

        public SplashScreen()
        {
            InitializeComponent();
        }

       
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            label1.Text = Application.ProductName;
            label2.Text = "Version : " + Application.ProductVersion;
            this.Opacity = 0;
            FadeAnimator.Enabled = true;
            Wait();
        }
        private async void Wait() {
            await Task.Delay(1000);
            StartUPTask();
        }

        public async void StartUPTask()
        {


            //Search for an OS and set the variable accordingly
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                Properties.Settings.Default.WindowsVersion = ("" + obj["Caption"]);
            }


            //Search for an CPU(Name) and set the variable accordingly
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                Properties.Settings.Default.ProcessorName = ("CPU : " + obj["Name"]);
            }

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C taskkill /f /im explorer.exe";
            process.StartInfo = startInfo;
            process.Start();

            //Search for the Total Ram Amount and set the variables accordingly
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                Properties.Settings.Default.RamAmount = ("RAM : " + result["TotalVisibleMemorySize"]).ToString() + "KB";
            }

            //Search for a GPU(name) and set the variable accordingly
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in myVideoObject.Get())
            {
                Properties.Settings.Default.GraphicsName = ("GPU : " + obj["Name"]);
            }
            Taskbar tbar = new Taskbar(); // Create a taskbar form object
            tbar.Show(); //Show the taskbar
            DesktopInterface desktop = new DesktopInterface(); // Create a desktop form object
            desktop.Show(); //Show the desktop
            EndFadeAnimator.Start();


        }

        private void FadeAnimator_Tick(object sender, EventArgs e)
        {
            if(this.Opacity <= 1.00) 
            {
                this.Opacity += 0.07;
            }
            else if(this.Opacity == 1.00)
            {
                FadeAnimator.Stop();
            }

        }

        private void EndFadeAnimator_Tick(object sender, EventArgs e)
        {
            FadeAnimator.Enabled = false;
            if (this.Opacity != 0)
            {
                this.Opacity -= 0.07;

            }
            if (this.Opacity == 0) 
            {
                this.Hide();
                EndFadeAnimator.Stop();
            }
        }
        
    }
}
