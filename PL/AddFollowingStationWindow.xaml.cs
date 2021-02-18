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
        TimeSpan time = new TimeSpan();
        List<BO.Station> stations = new List<BO.Station>();
        public AddFollowingStationWindow(IBl bl, int lineId, int station)  // the window get IBL ,the LineId of the line that user press on and station number
        { 
            this.bl = bl;
            this.lineId = lineId;
            this.station = station;
            stations = bl.getAllStations().Cast<BO.Station>().ToList();
            InitializeComponent();
            FollowingCb.ItemsSource = stations;
            FollowingCb.DisplayMemberPath = "Code";
            initComboBox();
        }
        private void initComboBox()
        {
            FollowingCb.SelectedIndex = 0; // choose by defult the first if the user still not press on any number in the comboBox 
        }

        private void ConfirmAddButton_Click(object sender, RoutedEventArgs e)  //the function check the input of the user and if he enter correctly it`s send it to BL layer to add the station
        {
            try
            {
                time = TimeSpan.Parse(TravelTimeTb.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Plesase enter in ths format : XX:XX:XX!");
                return;
            }
            string distanceCheck = DistanceTb.Text;
            for (int i = 0; i < distanceCheck.Length; i++)
            {
                if (!IsNumber(distanceCheck[i]))
                {
                    MessageBox.Show("please enter only numbers to the distance ");
                    return;
                }
            }
            double distance= Convert.ToInt32(DistanceTb.Text);
            if (distance<=0)
            {
                MessageBox.Show("please enter number tht bigger then a zero to the distance ");
                return;
            }
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
                if (lineStation.Station == codeF)
                {
                    MessageBox.Show(@"the chosen station is the station it`s selef,
                          please choose another station");
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
        private bool IsNumber(char c)  //the function check if the char that send it`s number
        {
            return Char.IsNumber(c);
        }
    }
}

 
