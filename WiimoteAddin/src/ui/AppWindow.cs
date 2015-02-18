using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiimoteAddin
{
    public partial class AppWindow : Form
    {

        public AppWindow()
        {
            InitializeComponent();
        }

        // begin pairing when(ever) the window is shown
        private void AppWindow_Shown(object sender, EventArgs e)
        {
            if (App.wiimoteManager.NoWiimotesConnected())
            {
                App.wiimoteManager.startPairWorker(true);
            }
        }

        private void AppWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hide window instead of destroying it
            this.Hide();
            e.Cancel = true;

            // TODO: ask pairing to stop
        }

        private void AppWindow_Load(object sender, EventArgs e)
        {
        }
        
        // expose SendKeys to slideshow controller
        public void DoSendKey(string s)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { DoSendKey(s); }));
            }
            else
            {
                SendKeys.Send(s);
            }
        }
    }
}
