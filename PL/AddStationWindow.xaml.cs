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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBl bl;
        public AddStationWindow(IBl bl)
        {
            this.bl = bl;
            InitializeComponent();
        }

        private void ConfirmAddButton_Click(object sender, RoutedEventArgs e)
        {
            int code = Convert.ToInt32(CodeTb.Text);
            double lati = Convert.ToDouble(LatitudeTb.Text);
            double longi = Convert.ToDouble(LongitudeTb.Text);
            GeoCoordinate location= new GeoCoordinate(lati, longi);
            string name = NameTb.Text;
            string address = AddressTb.Text;
            BO.Station newstation = new BO.Station();
            newstation.Code = code;
            newstation.Location.Latitude = location.Latitude;
            newstation.Location.Longitude = location.Longitude;
            newstation.Name = name;
            newstation.Address = address;
            try
            {
                bl.addStation(newstation);
            }
            catch (BO.AlreadyExistsException ms)
            {
                MessageBox.Show(ms.Message);
                return;
            }
            Close();
        }
    }
}
