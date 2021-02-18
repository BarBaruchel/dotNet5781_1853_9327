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
    /// Interaction logic for DistanceWindow.xaml
    /// </summary>
    public partial class DistanceWindow : Window
    {
        IBl bl;
        int lineId, station;
        public DistanceWindow(IBl bl, int lineId, int station)  // the window get IBL ,the LineId of the line that user press on and station number  
        {
            this.bl = bl;
            this.lineId = lineId;
            this.station = station;
            InitializeComponent();
        }

        private void DistanceSlider_KeyDown(object sender, KeyEventArgs e)  // the function send the distance that the user chosen and send it to the BL layer to update the distance between the station
        {
            if (e.Key == Key.Return)
            {
                int distance = (Convert.ToInt32(DistanceSlider.Value));
                bl.UpdateDistanceLineStation(lineId, station, distance);
                DialogResult = true;
                Close();

            }
        }
    }
}
