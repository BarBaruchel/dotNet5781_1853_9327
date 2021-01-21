using BLAPI;
using PO;
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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        IBl bl;
        BO.Line line = new BO.Line();
        List<BO.Station> stations = new List<BO.Station>();
        public UpdateLineWindow(IBl bl, BO.Line line)
        {
            this.bl = bl;
            this.line = line;
            stations = bl.getAllStations().Cast<BO.Station>().ToList();
            InitializeComponent();
            FirstCb.ItemsSource = stations;
            FirstCb.DisplayMemberPath = "Code";
            SecondCb.ItemsSource = stations;
            SecondCb.DisplayMemberPath = "Code";
            AreaCb.ItemsSource = Enum.GetValues(typeof(AREAS));
            initComboBox();
        }

        private void initComboBox()
        {
            FirstCb.SelectedIndex = 0; // choose by defult the first if the user still not press on any number in the comboBox 
            AreaCb.SelectedIndex = 0;
            SecondCb.SelectedIndex = 1;
        }
        private void ConfirmUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int id = line.Id;
            int code = Convert.ToInt32(CodeNumber.Text);
            BO.AREAS area = (BO.AREAS)AreaCb.SelectedItem;
            int firstStation = int.Parse(FirstCb.Text);
            int lastStation = int.Parse(SecondCb.Text);
            BO.Line newline = new BO.Line();
            newline.Id = id;
            newline.Code = code;
            newline.Area = area;
            newline.FirstStation = firstStation;
            newline.LastStation = lastStation;
            try
            { 
                bl.updateLine(newline); 
            }
            catch (BO.NotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
