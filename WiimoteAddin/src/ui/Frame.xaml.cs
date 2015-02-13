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

namespace WiimoteAddin
{
    /// <summary>
    /// Interaction logic for Frame.xaml
    /// </summary>
    public partial class Frame : UserControl
    {

        MainScreen pageMainScreen = new MainScreen();
        MappingScreen pageMappingScreen = new MappingScreen();

        
        public Frame()
        {
            InitializeComponent();
            pageMainScreen.DataContext = App.MainScreenData;
            frame.Content = pageMainScreen;
        }
    }
}
