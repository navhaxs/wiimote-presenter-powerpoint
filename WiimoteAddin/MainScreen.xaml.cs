using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WiimoteAddin
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : UserControl
    {
        WiimotePairing m = new WiimotePairing();

        public MainScreen()
        {
            InitializeComponent();

            if (App.WiimoteCount == 0)
            {
                App.MainScreenData.StatusLabel = "Wiimote not connected";
            }
            else if (App.WiimoteCount == 1)
            {
                App.MainScreenData.StatusLabel = "Wiimote connected";
            }
            else if (App.WiimoteCount > 0)
            {
                App.MainScreenData.StatusLabel = App.WiimoteCount + " Wiimotes connected";
            }
        }

        private void ButtonDetectWiimote_Click(object sender, RoutedEventArgs e)
        {
            if (!App.NoWiimotesActive())
            {
                Connect.thisPowerPoint.Windows.Application.ActivePresentation.SlideShowSettings.Run();
            }
            else
            {
                m.startPairWorker(false);
            }
        }

        private void ButtonClearAllWiimotes_Click(object sender, RoutedEventArgs e)
        {
            m.startUnpairWorker();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            m.startPairWorker(true);
        }

    }

}
