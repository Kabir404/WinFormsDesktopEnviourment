using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDesktopEnviourment
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles(); //Enable Windows Visual Style
            Application.SetCompatibleTextRenderingDefault(false); //Disable Compatible Text Rendering
            Application.Run(new SplashScreen());
        }
    }
}
