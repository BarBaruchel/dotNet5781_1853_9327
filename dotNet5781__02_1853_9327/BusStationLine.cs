using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781__02_1853_9327
{
    public class BusStationLine : BusStation
    {

        public BusStationLine(int key, double lat, double longi, double distance, TimeSpan travelTime) : base(key, lat, longi)
        {

            DistanceFromTheLastStation = distance;
            TravelTimeFromTheLastStation = travelTime;
        }



        /// distance from previous BusStation
        /// </summary>
        public double DistanceFromTheLastStation { get; set; }
        /// <summary>
        /// Travel time from previous BusStation
        /// </summary>
        public TimeSpan TravelTimeFromTheLastStation { get; set; }

        public override string ToString()
        {

            //TODO
            return base.ToString();
        }
    }
}
