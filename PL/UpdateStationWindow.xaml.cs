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
            string longiCheck = LongitudeTb.Text;
            for (int i = 0; i < longiCheck.Length; i++)
            {
                if (!IsNumber(longiCheck[i]))
                {
                    MessageBox.Show("please enter only numbers to the Longitude ");
                    return;
                }
            }
            double longi = Convert.ToDouble(LongitudeTb.Text);
                if ((longi > 90.00) || (longi < -90.00))
                {
                   MessageBox.Show("please enter Longitude between -90.00 to 90.00 ");
                    return;
                }
            string latiCheck = LatitudeTb.Text;
            for (int i = 0; i < latiCheck.Length; i++)
            {
                if (!IsNumber(latiCheck[i]))
                {
                    MessageBox.Show("please enter only numbers to the Latitude");
                    return;
                }
            }
            double lati = Convert.ToDouble(LatitudeTb.Text);
            if ((lati > 90.00) || (lati < -90.00))
            {
                MessageBox.Show("please enter Latitude between -90.00 to 90.00 ");
                return;
            }
            GeoCoordinate location = new GeoCoordinate(lati, longi);
            if(NameTb.GetType()!= typeof(string))
            {
                MessageBox.Show("please enter only letters to the Name");
                return;
            }
                string name = NameTb.Text;
            if (AddressTb.GetType() != typeof(string))
            {
                MessageBox.Show("please enter only letters to the Address ");
                return;
            }
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
        private bool IsNumber(char c)
        {
            return Char.IsNumber(c);
        }
    }
}


