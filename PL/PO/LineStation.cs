using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class LineStation :INotifyPropertyChanged
    {
        private double distance;
        private TimeSpan travelTime;

        public string LastStationName { get; set; }
        public int LineId { get; set; }
        public int Station { get; set; }
        public string Name { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
        public double DistanceFromTheLastStat  // distance from previous BusStation
        {
            get => distance;
            set
            {
                if (value != distance)
                {
                    distance = value;
                    RaisePropertyChanged("DistanceFromTheLastStat");
                }
            }
        }       
        private void RaisePropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public TimeSpan TravelTimeFromTheLastStation  // Travel time from previous BusStation
        { get=> travelTime;
            set
            {
                if (value != travelTime)
                {
                    travelTime = value;
                    RaisePropertyChanged("TravelTimeFromTheLastStation");
                }
            }
        }  
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
