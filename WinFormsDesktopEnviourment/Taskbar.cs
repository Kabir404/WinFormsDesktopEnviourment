using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsDesktopEnviourment.Core_Widgets;
using WinFormsDesktopEnviourment.Dialogs;

namespace WinFormsDesktopEnviourment
{
    public partial class Taskbar : Form
    {
        public int taskbarSize = Properties.Settings.Default.TaskbarSize;
        public bool isInternetOn = false;
        public Taskbar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, taskbarSize);
            //TaskManagerWorker.RunWorkerAsync();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsConnectedToInternet()) {
                NetStatIndicator.Image = Properties.Resources._123_cloud_computing;
                networkStatusToolStripMenuItem.Text = "Network Status : Connected";
            }
            else
            {
                NetStatIndicator.Image = Properties.Resources._127_cloud_computing;
                networkStatusToolStripMenuItem.Text = "Network Status : Disconnected";
            }
            toolStripDropDownButton2.Text = string.Format("{0:hh:mm tt}", DateTime.Now);
        }

        private CalenderView fm;
        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

            if (fm == null || fm.Text == "")
            {
                fm = new CalenderView();
                fm.Dock = DockStyle.Fill;
                
                fm.Show();
                fm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - fm.Width, taskbarSize);
            }
            else if (CheckOpened(fm.Text))
            {
                fm.Close();
            }

        }
        private StartMenu sm;
        private void metroTile1_Click(object sender, EventArgs e)
        {
            if (sm == null || sm.Text == "")
            {
                sm = new StartMenu();
                sm.Dock = DockStyle.Fill;

                sm.Show();
                sm.Location = new Point(0, taskbarSize);
            }
            else if (CheckOpened(sm.Text))
            {
                sm.Close();
            }
        }
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShutdownFade sf = new ShutdownFade();
            sf.IsShuttingDown = false;
            sf.ShowDialog();
        }
            
        //BackgroundWorkers - Network and Task Manager

        //Creating the extern function...  
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        //Creating a function that uses the API function...  
        public bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }


        private void TaskManagerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            Process[] procs = Process.GetProcesses(".");
            foreach (Process proc in procs)
            {
                if (proc.MainWindowTitle.Length > 0)
                {
                    metroTabControl1.TabPages.Clear();
                    TabPage appname = new TabPage(proc.MainWindowTitle);
                    metroTabControl1.TabPages.Add(appname);
                }
            }
        }

        private async void TaskManagerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            await Task.Delay(500);
            TaskManagerWorker.RunWorkerAsync(); //DO IT AGAIN!
        }


    }
    }
