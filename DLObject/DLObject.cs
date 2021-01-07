using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DS;
using DLAPI;

namespace DL
{
    public class DLObject : IDL
    {
        #region Singleton
        static readonly DLObject instance = new DLObject();
        static DLObject() 
        {
            DS.DataStore.Init();
            DS.DataStore.InitRepostory();

        }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static IDL Instance { get => instance; }// The public Instance property to use
        #endregion
        public void addBus(Bus bus)
        {
            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle == null)
            {
                DS.DataStore.Busses.Add(bus);

            }
            throw new AlreadyExistsException("The rishuy number already exist", vehicle.LicenseNum);
        }
        private Bus ExistBus(int license)
        {
            return DS.DataStore.Busses.FirstOrDefault(item => item.LicenseNum == license);
        }


        public IEnumerable<object> getAllBusses()
        {
            /// LINQ
            IEnumerable<Bus> result =
            from bus in DS.DataStore.Busses
            select bus;

            return result;


        }


        public void addStation(Station station)
        {
            Station stat = ExistStation(station.Code);
            if (stat == null)
            {
                DS.DataStore.Stations.Add(station.Clone());

            }
            throw new AlreadyExistsException("The code number already exist", station.Code);
        }

        private Station ExistStation(int code)
        {
            return DS.DataStore.Stations.FirstOrDefault(item => item.Code == code);
        }


        public Bus getBusByLicenseNum(int licenseNum)
        {
            return DS.DataStore.Busses.Find(x => x.LicenseNum == licenseNum);
        }
        public IEnumerable<object> getAllStations()
        {
            /// LINQ
            IEnumerable<Station> result =
            from station in DS.DataStore.Stations
            select station;

            return result;
        }

        public void Bedika(DO.Bus bus, int distance) //to check if the bus can do the track according to the conditions
        {

            
                if (DateTime.Now.Subtract(bus.LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
                {
                    bus.Status = STATUS.INTREATMENT;
                     return;
                }

                else if (bus.KiloFromLastTreat + distance >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
                {
                   bus.Status = STATUS.INTREATMENT;
                   return;
                }

                else if (bus.Fuel - distance < 0)  // if the ride is more then 1200km then the bus can`t do the ride throw exception
                {

                   return;
                }



                bus.Status = STATUS.READYFORRIDE;
                return;
            }



        public void treatBus(Bus bus)
        {
            // בשלב2 נבצע תהליכונים



            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle != null)
            {
                vehicle.LastTreat = DateTime.Now;
                vehicle.KiloFromLastTreat = 0;
                vehicle.Fuel = 1200;
                vehicle.Status = STATUS.READYFORRIDE;
                return;
            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }

        public void fuelBus(Bus bus)
        {
           
                Bus vehicle = ExistBus(bus.LicenseNum);
                if (vehicle != null)
                {
                    vehicle.Fuel = 1200;
                ///
                /// update the status of the new bus
                ///
                if (DateTime.Now.Subtract(bus.LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
                {
                    bus.Status = STATUS.INTREATMENT;

                }

                else if (bus.KiloFromLastTreat >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
                {
                    bus.Status = STATUS.INTREATMENT;

                }

                else if (!(bus.Fuel < 0))  // if the ride is more then 1200km then the bus can`t do the ride throw exception
                {

                    bus.Status = STATUS.READYFORRIDE;
                }

              

                return;

                }
            
            
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }



        public void updateBus(Bus bus)
        {
            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle != null)
            {
                vehicle = bus.Clone();

            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }

        public void deleteBus(Bus bus)
        {

            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle != null)
            {
                DS.DataStore.Busses.Remove(vehicle.Clone());
            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }
        public IEnumerable<object> getAllLines()
        {
            /// LINQ
            IEnumerable<Line> result =
            from line in DS.DataStore.Line
            select line;

            return result;
        }

        public void addLine(Line line)
        {
            Line lin = ExistLine(line.Id);
            if (lin == null)
            {
                DS.DataStore.Line.Add(lin);

            }
            throw new AlreadyExistsException("The Id & code number already exist", line.Id, line.Code);
        }

        private Line ExistLine(int id)
        {
            return DS.DataStore.Line.FirstOrDefault(item => item.Id == id);
        }


        public void updateLine(Line line)
        {
            Line lin = ExistLine(line.Id);
            if (lin != null)
            {
                lin = line.Clone();
            }
            throw new NotExistException("The Id  number not exist", line.Id);
        }


        public void deleteLine(Line line)
        {
            Line lin = ExistLine(line.Id);
            if (lin != null)
            {
                DS.DataStore.Line.Remove(lin.Clone());
            }
            throw new NotExistException("The Id number not exist", line.Id);
        }



        public double getDistanceBetweenTwoStations(Station from, Station to)
        {
            Station from_ = ExistStation(from.Code);
            Station to_ = ExistStation(to.Code);
            if (from_ == null)
                throw new NotExistStationException("this code is not a exist", from.Code);
            if (to_ == null)
                throw new NotExistStationException("this code is not a exist", to.Code);

            int indexOne = DS.DataStore.Stations.FindIndex(x => x.Code == from.Code);
            int indexTwo = DS.DataStore.Stations.FindIndex(x => x.Code == to.Code);
            double distanceF = 0; // the distance from the first station to station "from"
            double distanceT = 0; // the distance from the first station to station "to"
            for (int i = 1; i < indexOne; i++)
            {
                distanceF += DS.DataStore.Stations[i].DistanceFromTheLastStat;

            }
            for (int i = 1; i < indexTwo; i++)
            {
                distanceT += DS.DataStore.Stations[i].DistanceFromTheLastStat;

            }
            return Math.Abs(distanceF - distanceT);
        }



        public void updateStation(Station Station)
        {
            Station stat = ExistStation(Station.Code);
            if (stat != null)
            {
                stat = Station.Clone();
            }
            throw new NotExistException("The code number not exist", Station.Code);
        }

        public void deleteStation(Station Station)
        {
            Station stat = ExistStation(Station.Code);
            if (stat != null)
            {
                DS.DataStore.Stations.Remove(stat.Clone());
            }
            throw new NotExistException("The code number not exist", Station.Code);
        }

        public void updateLineStation(LineStation lineStation)
        {
            LineStation lineStat = ExistLineStation(lineStation.LineId);
            if (lineStat != null)
            {
                lineStat = lineStation.Clone();
            }
            throw new NotExistException("The Id station number not exist", lineStat.LineId, lineStat.Station);
        }

        public User getAdmin()
        {
            return DS.DataStore.admin;
        }
        private LineStation ExistLineStation(int lineId)
        {
            return DS.DataStore.LineStation.FirstOrDefault(item => item.LineId == lineId);
        }
    }
}
