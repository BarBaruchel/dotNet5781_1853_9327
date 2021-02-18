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
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        IBl bl;
        List<BO.Station> stations = new List<BO.Station>();
      
        public AddLineWindow(IBl bl)  //the window get IBL
        {
            this.bl = bl;
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

        private void ConfirmAddButton_Click(object sender, RoutedEventArgs e) //the function check the input of the user and if is enter correctly it`s and it to BL layer to add the station
        {
            string codeCheck = CodeNumber.Text;
            for (int i = 0; i < codeCheck.Length; i++)
            {
                if (!IsNumber(codeCheck[i]))
                {
                    MessageBox.Show("please enter only numbers that up then a zero to The Code");
                    return;
                }
            }
            int code = Convert.ToInt32(CodeNumber.Text);
            if (code <= 0)
            {
                MessageBox.Show("please enter only numbers that up then a zero to The Code");
                return;
            }
            BO.AREAS area = (BO.AREAS)AreaCb.SelectedItem;
            int firstStation = int.Parse(FirstCb.Text);
            int lastStation = int.Parse(SecondCb.Text);
             BO.Line newline = new BO.Line();
            newline.Code = code;
            newline.Area = area;
            newline.FirstStation = firstStation;
            newline.LastStation = lastStation;
            try
            {
                bl.addLine(newline);
            }
            catch (BO.WrongInputException ms)
            {
                MessageBox.Show(ms.Message);
            }
            Close();
        }
        private bool IsNumber(char c)  //the function check if the char that send it`s number
        {
            return Char.IsNumber(c);
        }
    }

}
