using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace dotNet5781__02_1853_9327
{

    public class BusLine : IComparable<BusLine>
    {
        private List<BusStationLine> stations = new List<BusStationLine>();
        public List<BusStationLine> Stations
        {
            get
            {
                List<BusStationLine> temp = new List<BusStationLine>(stations);
                return temp;
            }
        }

        public BusLine()
        {
            stations = new List<BusStationLine>();
        }

        //public readonly List<BusStation> busStations;
        public BusLine(BusStationLine bus)
        {
            AddFirstStation(bus);
        }
        public BusLine(BusStationLine first, BusStationLine last)

        {
            AddFirstStation(first);
            AddLastStation(last);
        }
        /// <summary>
        /// Line number
        /// </summary>
        public int Number { get; set; }
        public BusStationLine FirstStation { get; private set; }
        public BusStationLine LastStation { get; private set; }
        public Area Area { get; set; }

        public void AddLastStation(BusStationLine busStation)
        {
            stations.Add(busStation);
            LastStation = stations[stations.Count - 1];
        }
        public void AddFirstStation(BusStationLine busStation)
        {
            stations.Insert(0, busStation);
            FirstStation = stations[0];
        }
        public void AddStation(int index, BusStationLine busStation)
        {
            if (index == 0)
            {
                AddFirstStation(busStation);
            }
            else
            {
                if (index > stations.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + stations.Count);
                }
                if (index == stations.Count)
                {
                    stations.Insert(index, busStation);
                    LastStation = stations[stations.Count - 1];
                }
            }
        }
        public double timeBetween(BusStationLine one, BusStationLine two)
        {
            return one.TravelTimeFromTheLastStation.Subtract(two.TravelTimeFromTheLastStation).TotalMinutes;
        }

        public int CompareTo(BusLine other)
        {
            double mytotal = totalTime();
            double othertotal = other.totalTime();

            return mytotal.CompareTo(othertotal);
        }

        private double totalTime()
        {
            double total = 0;
            for (int i = 0; i < stations.Count - 1; i++)
            {
                total += timeBetween(stations[i], stations[i + 1]);
            }

            return total;
        }


        public bool existStations(BusStationLine station)
        {
            return stations.Contains(station);
        }

        public Double distanceBetween(BusStationLine one, BusStationLine two)
        {
            if (!existStations(one))
                throw new ArgumentException(
                      String.Format("{0} is not a exist", one));
            if (!existStations(two))
                throw new ArgumentException(
                      String.Format("{0} is not a exist", two));
            int indexOne = stations.FindIndex(x => x.BusStationKey == one.BusStationKey);
            int indexTwo = stations.FindIndex(x => x.BusStationKey == two.BusStationKey);
            double distanceOne = 0; // the distance from the first station to one
            double distanceTwo = 0; // the distance from the first station to two
            for (int i = 1; i < indexOne; i++)
            {
                distanceOne += stations[i].DistanceFromTheLastStation;

            }
            for (int i = 1; i < indexTwo; i++)
            {
                distanceTwo += stations[i].DistanceFromTheLastStation;

            }


            return Math.Abs(distanceOne - distanceTwo);
        }
        public List<BusStationLine> subPath(BusStationLine one, BusStationLine two)
        {

            List<BusStationLine> Newstations = new List<BusStationLine>();
            int indexOne = stations.FindIndex(x => x.BusStationKey == one.BusStationKey);
            int indexTwo = stations.FindIndex(x => x.BusStationKey == two.BusStationKey);
            if (indexOne < indexTwo)
            {
                for (int i = indexOne; i <= indexTwo; i++)
                {
                    Newstations.Add(stations[i]);
                }
            }
            else
            {
                for (int i = indexTwo; i <= indexOne; i++)
                {
                    Newstations.Add(stations[i]);
                }
            }

            return Newstations;

        }



        public override string ToString()
        {
            string result = "Number : " + Number + "  Area: " + Area + "  Stations : ";
            for (int i = 0; i < stations.Count - 1; i++)
            {
                result += stations[i] + " ";
            }
            result += '\n';
            return result;
        }

    }

}
