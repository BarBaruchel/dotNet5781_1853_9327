using BLAPI;
using System;
using System.Collections.Generic;
using System.Device.Location;
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
    /// Interaction logic for UpdateStationWindow.xaml
    /// </summary>
    public partial class UpdateStationWindow : Window
    {
        IBl bl;
        BO.Station station;
        public UpdateStationWindow(IBl bl, BO.Station station)
        {
            this.bl = bl;
            this.station = station;
            InitializeComponent();

        }
        private void ConfirmUpdateButton_Click(object sender, RoutedEventArgs e)
        {

           
            double lati = Convert.ToDouble(LatitudeTb.Text);
            double longi = Convert.ToDouble(LongitudeTb.Text);
            GeoCoordinate location = new GeoCoordinate(lati, longi);
            string name = NameTb.Text;
            string address = AddressTb.Text;
            BO.Station newstation = new BO.Station();
            newstation.Code = station.Code;
            newstation.Location.Latitude = location.Latitude;
            newstation.Location.Longitude = location.Longitude;
            newstation.Name = name;
            newstation.Address = address;
            try
            {
                bl.updateStation(newstation);
            }
            catch (BO.NotExistStationException ms)
            {
                MessageBox.Show(ms.Message);
                return;
            }
            Close();
        }
    }
}


