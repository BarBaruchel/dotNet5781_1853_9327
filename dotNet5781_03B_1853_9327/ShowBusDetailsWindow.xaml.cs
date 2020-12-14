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
using System.ComponentModel;
using System.Diagnostics;

namespace dotNet5781_03B_1853_9327
{
    /// <summary>
    /// Interaction logic for ShowBusDetailsWindow.xaml
    /// </summary>
    public partial class ShowBusDetailsWindow : Window
    {
        int currentIndex;
        private BackgroundWorker worker;
        private BackgroundWorker worker2;
        public ShowBusDetailsWindow(int index)
        {
            InitializeComponent();
            currentIndex = index;
            Grid.DataContext = ((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex];
        }

        private void Button_Click(object sender, RoutedEventArgs e) //definition of the theard when the user press on the tidluk button
        {
            worker = new BackgroundWorker();  //Initialize the name of the process
            worker.DoWork += Worker_DoWork; // go to the function that activate the clock
            worker.ProgressChanged += Worker_ProgressChanged;  //update changes
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted; // when the clock finish go to the finish of the theard function
            worker.WorkerReportsProgress = true;
            if (worker.IsBusy != true)
                worker.RunWorkerAsync(12);  //how many second the theard will be
            Thread tidlukThread = new Thread(((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex].tidluk);
            tidlukThread.Start();  // activate the theard

        }

        private void TreatmentBtn_Click(object sender, RoutedEventArgs e)
        {
            worker2 = new BackgroundWorker();  //Initialize the name of the process
            worker2.DoWork += Worker2_DoWork; // go to the function that activate the clock
            worker2.ProgressChanged += Worker1_ProgressChanged;//update changes
            worker2.RunWorkerCompleted += Worker2_RunWorkerCompleted; // when the clock finish go to the finish of the theard function
            worker2.WorkerReportsProgress = true;
            if (worker2.IsBusy != true)
                worker2.RunWorkerAsync(144); //how many second the theard will be
            Thread treatmentThread = new Thread(((MainWindow)Application.Current.MainWindow).currentBuses[currentIndex].treatment);
            treatmentThread.Start(); // activate the theard
        }



        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // BackgroundWorker worker = sender as BackgroundWorker;
           
            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++)
            {
               
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000);
                    worker.ReportProgress(length - i);
                
            }

            e.Result = stopwatch.ElapsedMilliseconds;
        }

        private void Worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();

            

            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++)
            {

                // Perform a time consuming operation and report progress.
                System.Threading.Thread.Sleep(1000);
                worker2.ReportProgress(length - i);

            }

            e.Result = stopwatch2.ElapsedMilliseconds;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) //With each change in the  tidluk`s clock in this function will be activated, setting the color and visibility of the clock on the screen
        {
            int progress = e.ProgressPercentage;
            RemainingTimeTidlukTb.Foreground = Brushes.DarkMagenta;
            RemainingTimeTidlukTb.Text = "00:00:" + progress.ToString();
        }

        private void Worker1_ProgressChanged(object sender, ProgressChangedEventArgs e) //With each change in the treatment`s clock this function will be activated, setting the color and visibility of the clock on the screen
        {
            int progress = e.ProgressPercentage;
            RemainingTimeTreatTb.Foreground = Brushes.DarkMagenta;
            int minutes = progress / 60;
            int seconds = progress % 60;
            RemainingTimeTreatTb.Text = "00:0" + minutes + ":" + seconds;
                
        }



        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) // When the watch has finished this function will be activated, the end message setting of visibility and color
        {
            RemainingTimeTidlukTb.Foreground = Brushes.Black;
            RemainingTimeTidlukTb.Text = "Completed!";
            
        }

        private void Worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RemainingTimeTreatTb.Foreground = Brushes.Black;
            RemainingTimeTreatTb.Text = "Completed!";

        }


    }
}
