using BLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        IBl bl;
        public ManagerWindow()
        {
            InitializeComponent();
            bl = this.bl = BLAPI.BLFactory.getBL(); 
        }

        private void ShowBuses_Click(object sender, RoutedEventArgs e)
        {
            ShowALLBuses window = new ShowALLBuses(bl);
            window.Show();
        }
    }
}
