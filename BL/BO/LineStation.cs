using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int LineId { get; set; }
        public int Station { get; set; }
        public string Name { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
        public double DistanceFromTheLastStat { get; set; } // distance from previous BusStation
        public TimeSpan TravelTimeFromTheLastStation { get; set; }  // Travel time from previous BusStation
        public string LastStationName { get; set; }
    }
}

