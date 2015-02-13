using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NetOffice;
using System.Drawing;

using Office = NetOffice.OfficeApi;
using NetOffice.OfficeApi.Enums;
using POWERPNT = NetOffice.PowerPointApi;
using NetOffice.PowerPointApi.Enums;


// Connect.cs
// Implments the COM interface as a PowerPoint Addin
namespace WiimoteAddin
{
    [GuidAttribute("3C891984-93C5-4F55-AF62-5237D3E472FD")]
    [ProgId("WiimoteAddin.Connect")]
    [ComVisible(true)]
    public class Connect : Extensibility.IDTExtensibility2, Office.IRibbonExtensibility
    {
        public static POWERPNT.Application thisPowerPoint;

        void Extensibility.IDTExtensibility2.OnAddInsUpdate(ref Array custom) {
            //do nothing
        }

        void Extensibility.IDTExtensibility2.OnBeginShutdown(ref Array custom) {
            //do nothing
        }

        void Extensibility.IDTExtensibility2.OnConnection(object Application, Extensibility.ext_ConnectMode ConnectMode, object AddInInst, ref Array custom) {
            //thisPowerPoint = (POWERPNT.Application) Application;
            thisPowerPoint  = new POWERPNT.Application(null, Application);
        }

        void Extensibility.IDTExtensibility2.OnDisconnection(Extensibility.ext_DisconnectMode RemoveMode, ref Array custom) {
            //do nothing
        }

        void Extensibility.IDTExtensibility2.OnStartupComplete(ref Array custom) {
            //do nothing
        }

        // add the app to the Office Ribbon
        public string GetCustomUI(string RibbonID) {
            return Properties.Resources.CustomUI; 
        }

        public Image ReturnImage(Office.IRibbonControl Control) {
            // Since only ONE custom image is used for the addin, simply return that image.
            // Otherwise if multiple images were used, add some code here...
            return Properties.Resources.ribbonButtonLogo;
        }


        // Using the msi installer rather than relying on regsvr32

        //[ComRegisterFunctionAttribute()]
        //public static void RegisterFunction(Type pType) {

        //}

        //[ComUnregisterFunctionAttribute()]
        //public static void UnregisterFunction(Type pType) {
           
        //}


        // show the main screen
        public void showWiimoteSetup(Office.IRibbonControl control = null, bool wait = false)
        {    
            if (App.ui != null) {
                // if already created
                App.ui.Show();
                App.ui.Focus();
            } else {
                // otherwise create it
                App.ui = new AppWindow();
                App.ui.Show(); 
            }     
        }

        public void LaunchRemoveAllWiimotes(Office.IRibbonControl control = null, bool wait = false)
        {
            
        }

        public void LaunchAddWiimotes(Office.IRibbonControl control = null, bool wait = false)
        {

        }
    }
}
