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
    /// Interaction logic for AddFollowingStationWindow.xaml
    /// </summary>
    public partial class AddFollowingStationWindow : Window
    {
        IBl bl;
        int lineId;
        int station;
        List<BO.Station> stations = new List<BO.Station>();
        public AddFollowingStationWindow(IBl bl, int lineId, int station)
        {
            this.bl = bl;
            this.lineId = lineId;
            this.station = station;
            stations = bl.getAllStations().Cast<BO.Station>().ToList();
            InitializeComponent();
            FollowingCb.ItemsSource = stations;
            FollowingCb.DisplayMemberPath = "Code";
        }

        private void ConfirmAddButton_Click(object sender, RoutedEventArgs e)
        {
            double distance= Convert.ToInt32(DistanceTb.Text);
            string stringTime = TravelTimeTb.Text;
            string[] values = stringTime.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), 0);
            int codeF = Convert.ToInt32(FollowingCb.Text);
            List<BO.LineStation> stationsByCode = bl.getLineStationByCode(station).Cast<BO.LineStation>().ToList();
            BO.LineStation lineStation = stationsByCode.Find(x => x.LineId == lineId);
            if (lineStation.NextStation ==codeF)
            {
                MessageBox.Show("the chosen station is already the next station!");
            }
            else
            {
                BO.LineStation newline = new BO.LineStation();
                newline.Station = station;
                newline.LineId = lineId;
                newline.NextStation = codeF;
                try
                {
                    bl.AddFollowingStation(newline, distance, ts);
                }
                catch (BO.WrongInputException ms)
                {
                    MessageBox.Show(ms.Message);
                }
                Close();
            }
        }
    }
}

 
