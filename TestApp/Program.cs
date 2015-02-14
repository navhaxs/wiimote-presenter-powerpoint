using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            WiimoteAddin.AppWindow wnd = new WiimoteAddin.AppWindow();
            wnd.ShowDialog();

            //WiimoteAddin.PowerPointApi wnd2 = new WiimoteAddin.PowerPointApi();
        }
    }
}
