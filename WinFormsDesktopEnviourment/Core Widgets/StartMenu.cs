using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WinFormsDesktopEnviourment.Dialogs;
using System.Diagnostics;

namespace WinFormsDesktopEnviourment.Core_Widgets
{
    public partial class StartMenu : MetroFramework.Forms.MetroForm
    {
        string userName = Environment.UserName;
        private string StartMenuPath = "";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        private string tempvar = "";

        public StartMenu()
        {
            InitializeComponent();
        }

        private void StartMenu_Load(object sender, EventArgs e)
        {
            //StartMenuPath = $"C:/Users/{userName}/AppData/Roaming/Microsoft/Windows/Start Menu/";
            StartMenuPath = $"C:/ProgramData/Microsoft/Windows/Start Menu/";
            LoadStartMenuItems();
        }

        private void LoadStartMenuItems() 
        {
            DirectoryInfo filelist;

            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = StartMenuPath + "/" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(StartMenuPath);

                }

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(StartMenuPath);
                    FileInfo[] files = fileList.GetFiles(); // GET ALL THE FILES
                    DirectoryInfo[] dirs = fileList.GetDirectories(); // GET ALL THE DIRS
                    string fileExtension = "";
                    listView1.Items.Clear();

                    for (int i = 0; i < files.Length; i++)
                    {
                        fileExtension = files[i].Extension.ToUpper();
                        switch (fileExtension)
                        {
                            case ".EXE":
                            case ".COM":
                            case ".LNK":
                                listView1.Items.Add(files[i].Name, 1);
                                break;
                            default:
                                listView1.Items.Add(files[i].Name, 1);
                                break;
                        }

                    }

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 0);
                    }
                }
                else
                {
                    tempFilePath = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {

            }
        }

        public void loadButtonAction()
        {
            removeBackSlash();
            StartMenuPath = tempvar;
            LoadStartMenuItems();
            isFile = false;
        }

        public void removeBackSlash()
        {
            try
            {
                string path = tempvar;
                if (path.LastIndexOf("/") == path.Length - 1)
                {
                    tempvar = path.Substring(0, path.Length - 1);
                }
            }
            catch { }
        }

        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = tempvar;
                path = path.Substring(0, path.LastIndexOf("/"));
                this.isFile = false;
                tempvar = path;
                removeBackSlash();
            }
            catch (Exception e)
            {

            }
        }
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                currentlySelectedItemName = e.Item.Text;

                FileAttributes fileAttr = File.GetAttributes(StartMenuPath + "/" + currentlySelectedItemName);
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    isFile = false;
                    tempvar = StartMenuPath + "/" + currentlySelectedItemName;
                }
                else
                {
                    isFile = true;
                }
            }
            catch { }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }
        //Button Actions
        private void button1_Click(object sender, EventArgs e)
        {
            ShutdownDialog sd = new ShutdownDialog();
            sd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }
    }
}
