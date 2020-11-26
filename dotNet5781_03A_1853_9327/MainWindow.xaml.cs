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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_1853_9327;

namespace dotNet5781_03A_1853_9327
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusCompany busCompany;
        private BusLine currentDisplayBusLine;
        private static Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            lbBusLineStations.Foreground = Brushes.RoyalBlue;  // designing the window
            cbBusLines.Foreground = Brushes.BlueViolet;
            tbArea.Foreground = Brushes.BlueViolet;

            busCompany = new BusCompany();  // create variable from BusCompny type
            initializeBuses(busCompany);
            initComboBox();
        }



        private void initComboBox()
        {
            cbBusLines.ItemsSource = busCompany.busses;
            cbBusLines.DisplayMemberPath = "Number";
            cbBusLines.SelectedIndex = 0;  // choose by defult the first if the user still not press on any number in the comboBox 

        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).Number);

        }

        private void ShowBusLine(int busLineNum)
        {
            currentDisplayBusLine = busCompany[busLineNum];
            UpGrid.DataContext = currentDisplayBusLine; // upgade on the screen the bus`s number that the user choose
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }


        public void initializeBuses(BusCompany company)
        {

            int key = 100000;
            Random r = new Random();

            List<BusStationLine> sharedStations = new List<BusStationLine>();  // list of stations through which several busses pass 
            List<BusStationLine> allStations = new List<BusStationLine>();  // list of all stations that all the busses pass through
            for (int i = 0; i < 10; i++)   // create 10 shared stations
            {
                double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                double distance = r.NextDouble() * (10 - 1) + 1;
                TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);
                sharedStations.Add(newStation);
                allStations.Add(newStation);
            }

            for (int i = 1; i <= 10; i++)  // create 10 busses line
            {

                BusLine newBusLine = new BusLine();
                newBusLine.Number = r.Next(1, 1000);
                int randomNumber = r.Next(0, 4);
                switch (randomNumber)
                {
                    case 0:
                        newBusLine.Area = Area.GENERAL;
                        break;
                    case 1:
                        newBusLine.Area = Area.SOUTH;
                        break;
                    case 2:
                        newBusLine.Area = Area.CENTER;
                        break;
                    case 3:
                        newBusLine.Area = Area.JERUSALEM;
                        break;
                }

                for (int j = 1; j <= 4; j++)  // enter to them 4 new stations
                {
                    double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                    double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                    double distance = r.NextDouble() * (10 - 1) + 1;
                    TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                    BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);

                    if (j == 1)
                    {
                        newBusLine.AddFirstStation(newStation);
                        allStations.Add(newStation);
                    }
                    else
                    {
                        newBusLine.AddLastStation(newStation);
                        allStations.Add(newStation);
                    }
                }
                for (int k = 0; k < 10; k++)  // add to them the 10 shared stations
                {
                    newBusLine.AddLastStation(sharedStations[k]);
                }

                company.Add(newBusLine);
            }

        }


    }
}
