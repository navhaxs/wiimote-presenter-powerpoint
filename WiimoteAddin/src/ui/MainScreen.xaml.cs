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
    public partial class MainScreen : Page
    {

        public MainScreen()
        {
            InitializeComponent();

            if (App.wiimoteManager.WiimoteCount == 0)
            {
                App.MainScreenData.StatusLabel = "Wiimote not connected";
            }
            else if (App.wiimoteManager.WiimoteCount == 1)
            {
                App.MainScreenData.StatusLabel = "Wiimote connected";
            }
            else if (App.wiimoteManager.WiimoteCount > 0)
            {
                App.MainScreenData.StatusLabel = App.wiimoteManager.WiimoteCount + " Wiimotes connected";
            }
        }

        private void ButtonDetectWiimote_Click(object sender, RoutedEventArgs e)
        {
            if (!App.wiimoteManager.NoWiimotesConnected())
            {
                Connect.thisPowerPoint.Windows.Application.ActivePresentation.SlideShowSettings.Run();
            }
            else
            {
                //App.PairingWorker.startPairWorker(false);
            }
        }

        private void ButtonClearAllWiimotes_Click(object sender, RoutedEventArgs e)
        {
            App.wiimoteManager.startUnpairWorker();
        }

        private void PairAnotherWiimote_Click(object sender, RoutedEventArgs e)
        {
            App.wiimoteManager.startPairWorker(false);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //App.wnd.frame1.Content = new MappingScreen();
        }

    }

}
