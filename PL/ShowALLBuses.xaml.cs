using System;
using System.Collections.Generic;
using System.Data;
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
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for ShowALLBus.xaml
    /// </summary>
    public partial class ShowALLBuses : Window
    {
        IBl bl;

        public ShowALLBuses(IBl bl)  // the window get IBL
        {
            this.bl = bl;

            InitializeComponent();
            BusesDataGrid.ItemsSource = bl.getAllBusses();// עידכון הדאטה גריד
        }

        private void RefreshBuses()
        {
            BusesDataGrid.ItemsSource = bl.getAllBusses();
        }

        private void TreatButtonClick(object sender, RoutedEventArgs e) // press on the one of the treat button  in one of the bus in the table start processor

        {
            BO.Bus row = (BO.Bus)BusesDataGrid.SelectedItems[0];
            int licenseNum = row.LicenseNum;
            bl.treatBus(bl.getBusByLicense(licenseNum));
            RefreshBuses();
        }
        private void TidlukBtn_Click(object sender, RoutedEventArgs e) // press on the one of the tidluk button  in one of the bus in the table start processor
        {
           BO.Bus row = (BO.Bus)BusesDataGrid.SelectedItems[0];
            int licenseNum = row.LicenseNum;
            bl.fuelBus(bl.getBusByLicense(licenseNum));
            RefreshBuses();
        }
        private void BusesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)  // the function open the BusDetails window of the bus that the user press on 
        {
            BO.Bus row = (BO.Bus)BusesDataGrid.SelectedItems[0];
            int licenseNum = row.LicenseNum;
            BO.Bus BOBus = bl.getBusByLicense(licenseNum); 
            BusDetails busDetails = new BusDetails(BOBus);
            busDetails.Show();
        }
    }
}



