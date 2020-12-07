using dotNet5781_01_1853_9327;
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

namespace dotNet5781_03B_1853_9327
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        Bus newBus = new Bus();
      
        public AddBusWindow()
        {
            InitializeComponent();
            Calendar.SelectedDate = DateTime.Today;
            newBus.StartPeilut = Calendar.SelectedDate.Value;
            Calendar.DisplayDateEnd = DateTime.Now;
           
        }

        private void onSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            newBus.StartPeilut = Calendar.SelectedDate.Value;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newBus.StartPeilut = Calendar.SelectedDate.Value;
            try
            {
                newBus.Rishuy = RishuyTb.Text;
            }
            catch (Exception ex)
            {

                ErrorLabel.Content = ex.Message;
                ErrorLabel.Foreground = Brushes.Red;
                return;
            }
           ((MainWindow)Application.Current.MainWindow).currentBuses.Add(newBus);
       
                this.Close();








        }

       

        /* private void onTextChanged(object sender, TextChangedEventArgs e)
         {
             string Rishuy = RishuyTb.Text;
             if (Rishuy.Length == 9 || Rishuy.Length == 10)
             {
                 if (Calendar.SelectedDate.Value.Year < 2018)
                     if ((Rishuy[2] != '-') || (Rishuy[6] != '-'))
                     {
                         ErrorLabel = new Label();
                         ErrorLabel.Content = "מספר רישוי לא תקין";
                         ErrorLabel.Foreground = Brushes.Red;

                     }
                     else
                     {
                         ErrorLabel.Content = "";
                     }
                 else
                 {
                     if ((Rishuy[3] != '-') || (Rishuy[6] != '-'))
                     {
                         ErrorLabel.Content = "מספר רישוי לא תקין";
                         ErrorLabel.Foreground = Brushes.Red;

                     }
                     else
                     {
                         ErrorLabel.Content = "";
                     }
                 }
             }
         }*/
    }
}
