using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1853_9327
{
    public class BusStationLine : BusStation
    {

        public BusStationLine(int key, double lat, double longi, double distance, TimeSpan travelTime) : base(key, lat, longi)
        {

            DistanceFromTheLastStation = distance;
            TravelTimeFromTheLastStation = travelTime;
        }



       
       
        public double DistanceFromTheLastStation { get; set; }  // distance from previous BusStation
        /// <summary>
       
        public TimeSpan TravelTimeFromTheLastStation { get; set; }  // Travel time from previous BusStation

        public override string ToString()
        {   
            return base.ToString()+"  "+ TravelTimeFromTheLastStation;
        }
    }
}
