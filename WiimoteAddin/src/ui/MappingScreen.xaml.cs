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
    /// Interaction logic for Mapping.xaml
    /// </summary>
    public partial class MappingScreen : Page
    {
        public MappingScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
        	 {
	        this.NavigationService.GoBack();
	 }
        }

        private void newevent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hai");

        }
    }
}
