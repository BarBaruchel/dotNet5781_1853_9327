using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1853_9327
{
    public class BusCompany : IEnumerable<BusLine>
    {
        private List<int> numbers = new List<int>();

        public List<BusLine> busses;

        public BusCompany()
        {
            busses = new List<BusLine>();
        }

        public BusLine search(int num)  // the funcation get bus number and search if it exsit and returns the position of the object
        {

            if (busses.Exists(x => x.Number == num))
                return busses.Find(x => x.Number == num);

            else
                throw new ArgumentNullException("This bus does not exist!"); 


        }

        public BusStationLine searchWithKey(int key) // the funcation get station number and search if it exsit and returns the position of the object
        {
            for (int i=0; i<busses.Count;i++)
            {
                if (busses[i].searchWithKey(key) != null)
                    return busses[i].searchWithKey(key);
            }
            throw new ArgumentNullException("This station does not exist!");
        }


        public void Add(BusLine bus)  // add new bus to company list
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
                    throw new ArgumentException("this number already exist in the company");
                }

            }
            busses.Add(bus);
            numbers.Add(bus.Number);

        }
        public void DeleteBus(BusLine bus)  // delete bus from the company list 
        {
            if (numbers.Contains(bus.Number))
            {
                busses.Remove(bus);
            }
            else
            {
                throw new ArgumentException("this number already exist in the company");
            }
        }

        public List<BusLine> BusesThatPassInStation(int key)  //  the funcation get station number and search all the busses that drive through
        {
            List<BusLine> existStation = new List<BusLine>();
            for (int i = 0; i < busses.Count(); i++)
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
      
       
        public BusLine this[int i]
        {
            get { return busses.Find(x => x.Number == i); }
            set { busses[busses.FindIndex(x => x.Number == i)] = value; }
        }




    }
}
