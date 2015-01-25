using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using POWERPNT = Microsoft.Office.Interop.PowerPoint;
using System.Drawing;
using Microsoft.Office.Core;

namespace WiimoteAddin
{

    [GuidAttribute("3C891984-93C5-4F55-AF62-5237D3E472FD")]
    [ProgId("WiimoteAddin.Connect")]
    [ComVisible(true)]
    public class Connect : Extensibility.IDTExtensibility2, Microsoft.Office.Core.IRibbonExtensibility
    {
        public static POWERPNT.Application thisPowerPoint;

        void Extensibility.IDTExtensibility2.OnAddInsUpdate(ref Array custom)
        {
            //throw new NotImplementedException();
        }

        void Extensibility.IDTExtensibility2.OnBeginShutdown(ref Array custom)
        {
            
            // Turn off LEDs (neccessary??)
            //WiimoteManager.Wiimote_Collection.FindAllWiimotes();
            //foreach (WiimoteLib.Wiimote i in WiimoteManager.Wiimote_Collection)
            //{
            //    i.Connect();
            //    i.SetLEDs(false, false, false, false);
            //    i.Disconnect();
            //}
            //throw new NotImplementedException();
        }

        void Extensibility.IDTExtensibility2.OnConnection(object Application, Extensibility.ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
        {
            thisPowerPoint = (POWERPNT.Application) Application;
            //throw new NotImplementedException();
        }

        void Extensibility.IDTExtensibility2.OnDisconnection(Extensibility.ext_DisconnectMode RemoveMode, ref Array custom)
        {
            //throw new NotImplementedException();
        }

        void Extensibility.IDTExtensibility2.OnStartupComplete(ref Array custom)
        {
            //throw new NotImplementedException();
            Application.EnableVisualStyles();
        }

        string Microsoft.Office.Core.IRibbonExtensibility.GetCustomUI(string RibbonID)
        {
            return Properties.Resources.CustomUI; 
        }

        public Image ReturnImage(Microsoft.Office.Core.IRibbonControl Control)
        {
            // Since only ONE custom image is used for the addin, simply return that image.
            // Otherwise if multiple images were used, add some code here...
            return Properties.Resources.ribbonButtonLogo;
        }


        //[ComRegisterFunctionAttribute()]
        //public static void RegisterFunction(Type pType)
        //{

        //}

        //[ComUnregisterFunctionAttribute()]
        //public static void UnregisterFunction(Type pType)
        //{
           
        //}


        public void showWiimoteSetup(IRibbonControl control = null, bool wait = false)
        {
            // show if already created, otherwise if not created make new one
            
            // make static somehow
            if (App.wnd != null)
            {
                App.wnd.Show();
                App.wnd.Focus();
            }
            else
            {
                App.wnd = new AppWindow();
                App.wnd.Show(); 
            }
            
        }

        public void LaunchRemoveAllWiimotes(IRibbonControl control = null, bool wait = false)
        {
            
        }

        public void LaunchAddWiimotes(IRibbonControl control = null, bool wait = false)
        {

        }
    }
}
