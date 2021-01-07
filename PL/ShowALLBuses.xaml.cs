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

        public ShowALLBuses(IBl bl)
        {
            this.bl = bl;

            InitializeComponent();
            BusesDataGrid.ItemsSource = bl.getAllBusses();// עידכון הדאטה גריד
        }

        private void RefreshBuses()
        {
            BusesDataGrid.ItemsSource = bl.getAllBusses();
        }

        private void BusesDataGrid_TargetUpdated(object sender, DataTransferEventArgs e) //double click whit the mouse on one of the buses in the table will show the details on the bus
        {
            // BusesDataGrid.ItemsSource = currentBuses;
        }
        private void TreatBtnButtonClick(object sender, RoutedEventArgs e) // press on the one of the travel button  in one of the bus in the table start processor

        {
            BO.Bus row = (BO.Bus)BusesDataGrid.SelectedItems[0];
            int licenseNum = row.LicenseNum;
            bl.treatBus(bl.getBusByLicense(licenseNum));
            RefreshBuses();
            /*
            int index = BusesDataGrid.SelectedIndex;
            TravelWindow newWindow = new TravelWindow(index);
            newWindow.Show();
            */
        }

        private void TidlukBtn_Click(object sender, RoutedEventArgs e) // press on the one of the tidluk button  in one of the bus in the table start processor
        {
           BO.Bus row = (BO.Bus)BusesDataGrid.SelectedItems[0];
            int licenseNum = row.LicenseNum;
            bl.fuelBus(bl.getBusByLicense(licenseNum));
            RefreshBuses();
            /*
            Thread tidlukThread = new Thread(currentBuses[BusesDataGrid.SelectedIndex].tidluk);
            tidlukThread.Start();
            */
        }

        private void BusesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Bus row = (BO.Bus)BusesDataGrid.SelectedItems[0];
            int licenseNum = row.LicenseNum;
            BO.Bus BOBus = bl.getBusByLicense(licenseNum); 
            BusDetails busDetails = new BusDetails(BOBus);
            busDetails.Show();


        }
    }
}



