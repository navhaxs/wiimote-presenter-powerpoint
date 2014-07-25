using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiimoteAddin
{
    public partial class AppWindow : Form
    {

        System.Windows.UIElement hostedWPFcontent;
        public AppWindow(System.Windows.UIElement WPFcontent)
        {
            InitializeComponent();
            hostedWPFcontent = WPFcontent;
        }

        private void AppWindow_Load(object sender, EventArgs e)
        {
            elementHost1.Child = hostedWPFcontent;
            elementHost1.Visible = true;
        }

        private void AppWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hide window instead of destroying it
            this.Hide();
            e.Cancel = true;
        }
    }
}
