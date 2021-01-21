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
    /// Interaction logic for TimeWindow.xaml
    /// </summary>
    public partial class TimeWindow : Window
    {
        IBl bl;
        int lineId, station;
        public TimeWindow(IBl bl, int lineId, int station)
        {
            this.bl = bl;
            this.lineId = lineId;
            this.station = station;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // DateTime time = (Convert.ToDateTime(EnterTimeTb.Text));
            string stringTime = EnterTimeTb.Text;
            string[] values = stringTime.Split(':');
            TimeSpan ts = new TimeSpan(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]),0);
            bl.UpdateTimeLineStation(lineId, station, ts);
            Close();
        }
        
    }
}
