using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dotNet5781_03B_1853_9327
{
    /// <summary>
    /// Interaction logic for ShowBusDetailsWindow.xaml
    /// </summary>
    public partial class ShowBusDetailsWindow : Window
    {
        int currentIndex;
        public ShowBusDetailsWindow(int index)
        {
            InitializeComponent();
            currentIndex = index;
            Grid.DataContext = ((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread tidlukThread = new Thread(((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex].tidluk);
            tidlukThread.Start();
            
        }

        private void TreatmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread treatmentThread = new Thread(((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex].treatment);
            treatmentThread.Start();
        }
    }
}
