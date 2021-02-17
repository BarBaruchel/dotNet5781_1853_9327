using BLAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : Window
    {
        IBl bl;
        BO.Station station;
        List<BO.LineStation> BOLineStations;
        public StationDetails(IBl bl, BO.Station station, List<BO.LineStation> BOLineStations)
        {
            InitializeComponent();
            this.bl = bl;
            this.station = station;
            this.BOLineStations = BOLineStations;
            NumberStationTBl.Text = station.Code.ToString();
            AddressTextBlock.Text = station.Address;
            LatTextBlock.Text = station.Location.Latitude.ToString();
            LonTextBlock.Text = station.Location.Longitude.ToString();
            StationDataGrid.ItemsSource = BOLineStations;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SimulatorWindows windows = new SimulatorWindows(bl, BOLineStations);
            windows.ShowDialog();
        }
    }
}
