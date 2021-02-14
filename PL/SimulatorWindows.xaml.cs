using BLAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindows.xaml
    /// </summary>
    public partial class SimulatorWindows : Window
    {
        IBl bl;
        bool isSimulatorActive = false;
        TimeSpan startTime = new TimeSpan();
        Stopwatch watch = new Stopwatch();
        int rate;
        List<BO.LineStation> BOLineStations;
        List<BO.LineTiming> LineTimings = new List<BO.LineTiming>();
        BackgroundWorker clockWorker = new BackgroundWorker();
        public SimulatorWindows(IBl bl, List<BO.LineStation> BOLineStations)
        {
            this.bl = bl;
            this.BOLineStations = BOLineStations;
            clockWorker.DoWork += clockWorker_DoWork;
            clockWorker.ProgressChanged += clockWorker_ProgressChanged;
            clockWorker.RunWorkerCompleted += clockWorker_RunWorkerCompleted;
            clockWorker.WorkerReportsProgress = true;
            clockWorker.WorkerSupportsCancellation = true;
            InitializeComponent();
        }


        private void RefreshLineTimings()
        {

            this.LinesDG.ItemsSource = this.LineTimings;
            this.LinesDG.Items.Refresh();
        }
        private void clockWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            startClock();
            while (watch.IsRunning)
            {
                Thread.Sleep(1000);
                clockWorker.ReportProgress(rate);
            }
        }
        private void clockWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            TimeSpan elapsed = new TimeSpan(0, 0, e.ProgressPercentage);
            startTime = (startTime + elapsed);
            TimeTB.Text = String.Format("{0}:{1}:{2}", startTime.Hours, startTime.Minutes, startTime.Seconds);
            List<BO.LineTiming> linesToRemove = new List<BO.LineTiming>();
            foreach (BO.LineTiming lt in this.LineTimings)
            {
                lt.TimeToArrive = bl.updateTime(lt, rate);
                if (lt.TimeToArrive.Hours < 1 && lt.TimeToArrive.Minutes < 1 && lt.TimeToArrive.Seconds < 1)
                {
                    linesToRemove.Add(lt);
                    this.LastStationTB.Text = lt.LineId.ToString();
                }

            }
            foreach (BO.LineTiming linetoRemove in linesToRemove)
            {
                this.LineTimings.Remove(linetoRemove);
            }
            RefreshLineTimings();
        }
        private void clockWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isSimulatorActive = !isSimulatorActive;  ///////////
            if (isSimulatorActive)
            {
                Btn.Content = "Stop";
                TimeTB.IsReadOnly = true;
                RateTB.IsReadOnly = true;
                if (TimeTB.GetType() != typeof(TimeSpan))
                {
                    MessageBox.Show("please enter a Time in this form 00:00:00");
                    Btn.Content = "Start";
                    watch.Stop();
                    TimeTB.IsReadOnly = false;
                    RateTB.IsReadOnly = false;
                    return;
                }
                startTime = TimeSpan.Parse(TimeTB.Text);
                if (RateTB.GetType() != typeof(int))  // check the input of Rate Text if it is`nt from int type
                {
                    MessageBox.Show("please enter a number (not string or below than zero)");
                    Btn.Content = "Start";
                    watch.Stop();
                    TimeTB.IsReadOnly = false;
                    RateTB.IsReadOnly = false;
                    return;
                }
                rate = Convert.ToInt32(RateTB.Text);
                clockWorker.RunWorkerAsync();
                this.LineTimings = bl.startSimulator(BOLineStations, startTime);
                RefreshLineTimings();
            }
            else
            {
                Btn.Content = "Start";
                watch.Stop();
                TimeTB.IsReadOnly = false;
                RateTB.IsReadOnly = false;
                clockWorker.CancelAsync();
            }

        }

        private void startClock()
        {
            //  DispatcherTimer timer = new DispatcherTimer();
            ///  timer.Interval = TimeSpan.FromSeconds(1);
            // timer.Tick += tickevent;
            // timer.Start();
            watch.Start();

        }

        private void tickevent(object sender, EventArgs e)
        {



        }
    }
}
