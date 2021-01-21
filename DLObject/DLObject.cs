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
        #endregion Singleton


        #region Bus
        public void addBus(DO.Bus bus)
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
        public DO.Bus getBusByLicenseNum(int licenseNum)
        {
            return DS.DataStore.Busses.Find(x => x.LicenseNum == licenseNum);
        }
        public DO.Line getLineById(int id)
        {
            return DS.DataStore.Lines.Find(x => x.Id == id);
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
        public void treatBus(DO.Bus bus)
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
        public void fuelBus(DO.Bus bus)
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
                else if (bus.KiloFromLastTreat >= 20000) // if the kilometrage is more then 20000km the the bus can`t do the ride throw exception
                {
                    bus.Status = STATUS.INTREATMENT;
                }
                bus.Status = STATUS.READYFORRIDE;
                return;
            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }
        public void updateBus(DO.Bus bus)
        {
            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle != null)
            {
                vehicle = bus.Clone();
                return;
            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }
        public void deleteBus(DO.Bus bus)
        {
            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle != null)
            {
                DS.DataStore.Busses.Remove(vehicle.Clone());
                return;
            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }
        #endregion Bus


        #region Lines
        public IEnumerable<object> getAllLines()
        {
            /// LINQ
            IEnumerable<Line> result =
            from line in DS.DataStore.Lines
            select line;

            return result;
        }
        public void addLine(DO.Line line)
        {
            line.Id = ++DS.DataStore.RunningNum;
             DS.DataStore.Lines.Add(line);
        }
        private Line ExistLine(int id)
        {
            return DS.DataStore.Lines.FirstOrDefault(item => item.Id == id);
        }
        public void updateLine(DO.Line line)
        {
            DO.Line lineToUpdate = DS.DataStore.Lines.Find(x => x.Id == line.Id);
           if (lineToUpdate != null)
             {
                lineToUpdate.Area = line.Area;
                lineToUpdate.Code = line.Code;
                lineToUpdate.FirstStation = line.FirstStation;
                lineToUpdate.LastStation = line.LastStation;
                return;
             }
             throw new NotExistException("The Id  number not exist", line.Id);
        }
        public void deleteLine(DO.Line line)
        {
            Line lin = ExistLine(line.Id);
            if (lin != null)
            {
                DS.DataStore.Lines.Remove(lin);
                return;
            }
            throw new NotExistException("The Id number not exist", line.Id);
        }
        #endregion Line


        #region User
        public User getAdmin()
        {
            return DS.DataStore.admin;
        }
        #endregion User


        #region Station
        public void addStation(DO.Station station)
        {
            Station stat = ExistStation(station.Code);
            if (stat == null)
            {
                DS.DataStore.Stations.Add(station);
                return;
            }
            throw new AlreadyExistsException("The code number already exist", station.Code);
        }
        private Station ExistStation(int code)
        {
            return DS.DataStore.Stations.FirstOrDefault(item => item.Code == code);
        }
        public IEnumerable<object> getAllStations()
        {
            /// LINQ
            IEnumerable<Station> result =
            from station in DS.DataStore.Stations
            select station;

            return result;
        }
        public void updateStation(DO.Station Station)
        {
            Station stat = ExistStation(Station.Code);
            if (stat != null)
            {
                stat = Station.Clone();
                List<DO.Station> stations = getAllStations().Cast<DO.Station>().ToList(); // DELET IN THE END
                
                return;
            }
            throw new NotExistException("The code number not exist", Station.Code);
        }
        public void deleteStation(DO.Station Station)
        {
            Station stat = ExistStation(Station.Code);
            if (stat != null)
            {
                DS.DataStore.Stations.Remove(stat.Clone());
                return;
            }
            throw new NotExistException("The code number not exist", Station.Code);
        }
        #endregion Station


        #region LineStation
       public void AddFollowingStation(DO.LineStation lineStation, double distanceFromThePrevToFollowing, TimeSpan timeFromThePrevToFollowing)
        {
            DO.LineStation lineStationToUpdate = DS.DataStore.LineStations.Find(x => (x.LineId == lineStation.LineId) && (x.Station==lineStation.Station));
            DO.LineStation nextStation= DS.DataStore.LineStations.Find(x => (x.LineId == lineStation.LineId)&&(x.Station == lineStationToUpdate.NextStation));
            lineStationToUpdate.NextStation = lineStation.NextStation;
            DO.LineStation newLineStation = new DO.LineStation();
            if ((nextStation==null)&&(nextStation.NextStation==nextStation.Station) ) // if lineStationToUpdate is the last station in the ride
            {
                newLineStation.LineId = lineStationToUpdate.LineId;
                newLineStation.LineStationIndex = lineStationToUpdate.LineStationIndex + 1;
                newLineStation.Station = lineStationToUpdate.NextStation;
                newLineStation.NextStation = newLineStation.Station;
                newLineStation.PrevStation = lineStationToUpdate.Station;
                newLineStation.DistanceFromTheLastStat = distanceFromThePrevToFollowing;
                newLineStation.TravelTimeFromTheLastStation = timeFromThePrevToFollowing;
                DS.DataStore.LineStations.Add(newLineStation);
                return;
            }
            newLineStation.LineId = lineStationToUpdate.LineId;
            newLineStation.LineStationIndex = lineStationToUpdate.LineStationIndex + 1;
            newLineStation.Station = lineStationToUpdate.NextStation;
            newLineStation.NextStation = nextStation.Station;
            newLineStation.PrevStation = lineStationToUpdate.Station;
            newLineStation.DistanceFromTheLastStat = distanceFromThePrevToFollowing;
            newLineStation.TravelTimeFromTheLastStation = timeFromThePrevToFollowing;
            DS.DataStore.LineStations.Add(newLineStation);
            ++nextStation.LineStationIndex;
            nextStation.PrevStation = newLineStation.Station;
            //prev
            DO.LineStation updateStations = DS.DataStore.LineStations.FirstOrDefault(x => (x.LineId == nextStation.LineId) && (x.Station == nextStation.NextStation));
            while (updateStations!= null)                  // update the rest of LineStationIndex in the same LineId
            {
                ++updateStations.LineStationIndex;
                if (updateStations.Station == updateStations.NextStation)
                    break;
                updateStations = DS.DataStore.LineStations.FirstOrDefault(x => (x.LineId == updateStations.LineId) && (x.Station == updateStations.NextStation));
            }
        }
        public IEnumerable<object> getAllLineStations()
        {
            IEnumerable<LineStation> result =
          from station in DS.DataStore.LineStations
          select station;

            return result;
        }
        public double getDistanceBetweenTwoStations(LineStation from, LineStation to)
        {
            LineStation from_ = ExistLineStation(from.LineId, from.Station);
            LineStation to_ = ExistLineStation(to.LineId, to.Station);
            if (from_ == null)
                throw new NotExistStationException("this code is not a exist", from.LineId);
            if (to_ == null)
                throw new NotExistStationException("this code is not a exist", to.LineId);
            int indexOne = DS.DataStore.LineStations.FindIndex(x => x.LineId == from.LineId);
            int indexTwo = DS.DataStore.LineStations.FindIndex(x => x.LineId == to.LineId);
            double distanceF = 0; // the distance from the first station to station "from"
            double distanceT = 0; // the distance from the first station to station "to"
            for (int i = 1; i < indexOne; i++)
            {
                distanceF += DS.DataStore.LineStations[i].DistanceFromTheLastStat;
            }
            for (int i = 1; i < indexTwo; i++)
            {
                distanceT += DS.DataStore.LineStations[i].DistanceFromTheLastStat;
            }
            return Math.Abs(distanceF - distanceT);
        }
        public void updateLineStation(DO.LineStation lineStation)
        {
            LineStation lineStat = ExistLineStation(lineStation.LineId, lineStation.Station);
            if (lineStat != null)
            {
                lineStat = lineStation.Clone();
                return;
            }
            throw new NotExistException("The Id station number not exist", lineStat.LineId, lineStat.Station);
        }
        private LineStation ExistLineStation(int lineId, int station)
        {
            return DS.DataStore.LineStations.FirstOrDefault(item => item.LineId == lineId && item.Station == station);
        }
        #endregion LineStation















      
      
    }
}
