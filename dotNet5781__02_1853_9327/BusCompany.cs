using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781__02_1853_9327
{

    public class BusCompany : IEnumerable<BusLine>
    {
        private List<int> numbers = new List<int>();

        private List<BusLine> busses;

        public BusCompany()
        {
            busses = new List<BusLine>();
        }
        public void Add(BusLine bus)
        {
            if (numbers.Contains(bus.Number))
            {
                int indexBusExist = busses.FindIndex(x => x.Number == bus.Number);
                if ((busses[indexBusExist].FirstStation == bus.LastStation) && (busses[indexBusExist].LastStation == bus.FirstStation))
                {
                    busses.Add(bus);
                    return;
                }
                else
                {
                    throw new ArgumentException("mispar kvar kayam bachevra");
                }

            }
            busses.Add(bus);
            numbers.Add(bus.Number);

        }
        public void DeleteBus(BusLine bus)
        {
            if (numbers.Contains(bus.Number))
            {
                busses.Remove(bus);
            }
            else
            {
                throw new ArgumentException("mispar lo kayam bachevra");
            }
        }

        public List<BusLine> BusesThatPassInStation(int key)
        {
            List<BusLine> existStation = new List<BusLine>();
            for (int i = 0; i <= busses.Count(); i++)
            {
                if (busses[i].Stations.Find(x => x.BusStationKey == key) != null)
                {
                    existStation.Add(busses[i]);
                }

            }
            if (existStation.Count == 0)
            {
                throw new KeyNotFoundException("There are no buses that pass by this station");
            }
            return existStation;
        }


        public IEnumerator<BusLine> GetEnumerator()
        {
            return busses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //private List<BusLine> buses = new List<BusLine>();

        //public List<BusLine> Busses
        //{
        //    get { return buses; }
        //}
        public BusLine this[int i]
        {
            get { return busses.Find(x => x.Number == i); }
            set { busses[busses.FindIndex(x => x.Number == i)] = value; }
        }


    }

}
