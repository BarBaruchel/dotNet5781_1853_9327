using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        public int Code { get; set; }
        public GeoCoordinate Location { get; set; }
        public string Address { get; set; }

        public double DistanceFromTheLastStat { get; set; } // distance from previous BusStation
        public TimeSpan TravelTimeFromTheLastStation { get; set; }  // Travel time from previous BusStation


    }
}

