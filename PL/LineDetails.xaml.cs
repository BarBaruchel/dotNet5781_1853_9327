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
    /// Interaction logic for LineDetails.xaml
    /// </summary>
    public partial class LineDetails : Window
    {
        IBl bl;
        BO.Line line = new BO.Line();
        public LineDetails(IBl bl ,BO.Line line) // the window get IBL ,the line that user press on to update  
        {
            InitializeComponent();
            this.bl = bl;
            LineDetailsDataGrid.ItemsSource = bl.getLineStationsForLine(line);
            this.line = line;
            IdTextBlock.Text = line.Id.ToString();
        }

        private void RefreshLines()// בגלל שעשינו שינויים בכפתורי התידלוק והטיפול נקרא לריפרש שיחפש שוב את האוטובוס ע"י מספר רישוי ויעדכן את הנתונים לתןך 
        {  // המיין גריד נקודה דאטה קונטקס ויציג את הנתונים בחלון
            LineDetailsDataGrid.ItemsSource = bl.getLineStationsForLine(line);
            LineDetailsDataGrid.Items.Refresh();
        }
        private void UpdateTimeBtn_Click(object sender, RoutedEventArgs e) // press on the one of the Update Time button  in one of the station in the table start processor
        {
            BO.LineStation row = (BO.LineStation)LineDetailsDataGrid.SelectedItems[0];
            int lineId = this.line.Id;
            int station = row.Station;
            int index= LineDetailsDataGrid.SelectedIndex;
            TimeWindow newWindow = new TimeWindow(bl, lineId, station);
            newWindow.ShowDialog();
            RefreshLines();
        }
        private void UpdateDisBtn_Click(object sender, RoutedEventArgs e) // press on the one of the Update Distance button  in one of the stations in the table start processor
        {
            BO.LineStation row = (BO.LineStation)LineDetailsDataGrid.SelectedItems[0];
            int lineId = row.LineId;
            int station = row.Station;
            int index = LineDetailsDataGrid.SelectedIndex;
            DistanceWindow newWindow = new DistanceWindow(bl,lineId, station);
            newWindow.ShowDialog();
            RefreshLines();
        }
        private void AddFollowingStationBtn_Click(object sender, RoutedEventArgs e)  //the function read the input that the user enter and send it to the BL layer to enter a new following station 
        {
            BO.LineStation row = (BO.LineStation)LineDetailsDataGrid.SelectedItems[0];
            int lineId = row.LineId;
            int station = row.Station;
            int index = LineDetailsDataGrid.SelectedIndex;
            AddFollowingStationWindow newWindow = new AddFollowingStationWindow(bl, lineId, station);
            newWindow.ShowDialog();
            RefreshLines();
        }
    }
}
