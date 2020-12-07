using dotNet5781_01_1853_9327;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace dotNet5781_03B_1853_9327
{
    /// <summary>
    /// Interaction logic for TravelWindow.xaml
    /// </summary>
    public partial class TravelWindow : Window
    {

        int currentIndex = 0;
        Bus currentBus = new Bus();
        public TravelWindow(int index)
        {
            currentIndex = index;
            currentBus = ((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex];
            InitializeComponent();
        }

        private void DistanceSlider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    Thread travelThread = new Thread(new ParameterizedThreadStart(currentBus.Travel));
                    travelThread.Start(Convert.ToInt32(DistanceSlider.Value));
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MessageBox.Show("Success!");
                   
                
               
            }
            Close();

        }
    }
}
