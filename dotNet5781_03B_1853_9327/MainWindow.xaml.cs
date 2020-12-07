using dotNet5781_01_1853_9327;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace dotNet5781_03B_1853_9327
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Bus> currentBuses = new ObservableCollection<Bus>();
        public MainWindow()
        {
            
            InitializeComponent();
            InitializeBuses();
           
                

            BusesDataGrid.ItemsSource = currentBuses;
            
        }



        public static DateTime randomDate()
        {
            
            Random randomDateNum = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(randomDateNum.Next(range));
        }
            
        public void InitializeBuses()
        {
            Random rand = new Random();
            Random randomDateN = new Random();
            List<Bus> buses = new List<Bus>();
            for (int i = 0; i < 9; i++)
            {
                Bus newBus = new Bus();
                newBus.Kilometrage = rand.Next();
                newBus.l_treatment.KilFromTreat = newBus.Kilometrage;
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                newBus.StartPeilut = start.AddDays(randomDateN.Next(range)).Date;
     
                if (newBus.StartPeilut.Year < 2018)
                {

                       string rishuyNum = rand.Next(1000000, 10000000).ToString();
                    newBus.Rishuy = rishuyNum[0].ToString() + rishuyNum[1].ToString() + "-" + rishuyNum[2].ToString() + rishuyNum[3].ToString() + rishuyNum[4].ToString() + "-" + rishuyNum[5].ToString() + rishuyNum[6].ToString();


                }
                else
                {
          
                    string rishuyNum = rand.Next(10000000, 100000000).ToString();
                    newBus.Rishuy = rishuyNum[0].ToString() + rishuyNum[1].ToString() + rishuyNum[2].ToString() + "-" + rishuyNum[3].ToString() + rishuyNum[4].ToString() + "-" + rishuyNum[5].ToString() + rishuyNum[6].ToString() + rishuyNum[7].ToString();

                }
                Random randomDateNum = new Random();
                 start = newBus.StartPeilut;
                 range = (DateTime.Today - start).Days;
                newBus.l_treatment.Date = start.AddDays(randomDateNum.Next(range));
                newBus.l_treatment.KilFromTreat = rand.Next(1, 40000);
                newBus.Delek = rand.Next(1, 1201);
                newBus.Bedika(0);
                buses.Add(newBus);

            }
           
            buses[1].l_treatment.KilFromTreat = 19000; // לפחות אוטובוס אחד יהיה קרוב נסועת הטיפול הבא
            buses[1].l_treatment.Date = DateTime.Now;
            buses[1].Bedika(0);
            Bus nBus = new Bus();
            nBus.Kilometrage = rand.Next();
            nBus.l_treatment.KilFromTreat = nBus.Kilometrage;
            nBus.StartPeilut = new DateTime(2019, 1, 3);
            nBus.l_treatment.Date = new DateTime(2019, 2, 3);
            rand = new Random();
            string rishuyN = rand.Next(10000000, 100000000).ToString();
            nBus.Bedika(0);
            nBus.Rishuy = rishuyN[0].ToString() + rishuyN[1].ToString() + rishuyN[2].ToString() + "-" + rishuyN[3].ToString() + rishuyN[4].ToString() + "-" + rishuyN[5].ToString() + rishuyN[6].ToString() + rishuyN[7].ToString();
            buses.Add(nBus);
            buses[3].Delek = 1;
            buses[3].l_treatment.Date = DateTime.Now;
            buses[3].Bedika(0);


            foreach (var bus in buses)
            {
                currentBuses.Add(bus);
            }
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow newWindow = new AddBusWindow();
            newWindow.Show();

        }

        private void BusesDataGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            BusesDataGrid.ItemsSource = currentBuses;
        }

        private void TravelBtnButtonClick(object sender, RoutedEventArgs e)
        {
            int index = BusesDataGrid.SelectedIndex;
            TravelWindow newWindow = new TravelWindow(index);
            newWindow.Show();
        }

        private void TidlukBtn_Click(object sender, RoutedEventArgs e)
        {
           Thread tidlukThread = new Thread(currentBuses[BusesDataGrid.SelectedIndex].tidluk);
            tidlukThread.Start();
        }

        private void BusesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowBusDetailsWindow newWindow = new ShowBusDetailsWindow(BusesDataGrid.SelectedIndex);
            newWindow.Show();
        }
    }
}
