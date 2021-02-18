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
        DLObject() { }// default => private 
        public static IDL Instance { get => instance; }// The public Instance property to use
        #endregion Singleton


        #region Bus
        public void addBus(DO.Bus bus) //the function get Bus variable and check if it`s already exist in the DS, if yes throw excepation, else add it
        {
            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle == null)
            {
                DS.DataStore.Busses.Add(bus);
            }
            throw new AlreadyExistsException("The rishuy number already exist", vehicle.LicenseNum);
        }
        private Bus ExistBus(int license)    //the function get Bus variable and check if it`s exist in the DS and return it (if not exist return null)
        {
            return DS.DataStore.Busses.FirstOrDefault(item => item.LicenseNum == license);
        }
        public IEnumerable<DO.Bus> getAllBusses()  //the funcation return all the list of all busses
        {
            /// LINQ
            IEnumerable<Bus> result =
            from bus in DS.DataStore.Busses
            select bus.Clone();

            return result;
        }
        public DO.Bus getBusByLicenseNum(int licenseNum) //the function get licenseNum and return the clone of the bus that found
        {
            return DS.DataStore.Busses.Find(x => x.LicenseNum == licenseNum).Clone();
        }
        public void updateBus(DO.Bus bus)    //the function get Bus variable and check if it`s  exist in the DS, if no throw excepation, else update it
        {
            Bus vehicle = ExistBus(bus.LicenseNum);
            if (vehicle != null)
            {
                vehicle = bus.Clone();
                return;
            }
            throw new NotExistException("The rishuy number not exist", vehicle.LicenseNum);
        }
        public void deleteBus(DO.Bus bus) //the function get Bus variable and check if it`s  exist in the DS, if no throw excepation, else delete it
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
        public DO.Line getLineById(int id)   //the function get id and return the clone of the line that found
        {
            return DS.DataStore.Lines.Find(x => x.Id == id).Clone();
        }
        public IEnumerable<DO.Line> getAllLines()  //the funcation return all the list of all Lines
        {
            /// LINQ
            IEnumerable<Line> result =
            from line in DS.DataStore.Lines
            select line.Clone();

            return result;
        }
        public void addLine(DO.Line line)  //the function get Line variable and check if it`s already exist in the DS, if yes throw excepation, else add it
        {
            line.Id = ++DS.DataStore.RunningNum;
            DS.DataStore.Lines.Add(line);
        }
        private Line ExistLine(int id)  //the function get Line variable and check if it`s exist in the DS and return it (if not exist return null)
        {
            return DS.DataStore.Lines.FirstOrDefault(item => item.Id == id).Clone();
        }
        public void updateLine(DO.Line line)  //the function get Line variable and check if it`s  exist in the DS, if no throw excepation, else update it
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
        public void deleteLine(DO.Line line)  //the function get Line variable and check if it`s  exist in the DS, if no throw excepation, else delete it
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
        public User getAdmin()  //the funcation return the clone admin user from the DS
        {
            return DS.DataStore.admin.Clone();
        }
        #endregion User


        #region Station
        public void addStation(DO.Station station)  //the function get Station variable and check if it`s already exist in the DS, if yes throw excepation, else add it
        {
            Station stat = ExistStation(station.Code);
            if (stat == null)
            {
                DS.DataStore.Stations.Add(station);
                return;
            }
            throw new AlreadyExistsException("The code number already exist", station.Code);
        }
        private Station ExistStation(int code)  //the function get Station variable and check if it`s exist in the DS and return it (if not exist return null)
        {
            return DS.DataStore.Stations.FirstOrDefault(item => item.Code == code);
        }
        public IEnumerable<DO.Station> getAllStations()   //the funcation return all the Station of all Lines
        {
            /// LINQ
            IEnumerable<Station> result =
            from station in DS.DataStore.Stations
            select station.Clone();

            return result;
        }
        public void updateStation(DO.Station station)  //the function get Station variable and check if it`s exist in the DS, if no throw excepation, else update it
        {
            Station stat = ExistStation(station.Code);
            if (stat != null)
            {
                stat = station.Clone();
                List<DO.Station> stations = getAllStations().Cast<DO.Station>().ToList(); // DELET IN THE END

                return;
            }
            throw new NotExistException("The code number not exist", station.Code);
        }
        public void deleteStation(DO.Station station)  //the function get Station variable and check if it`s  exist in the DS, if no throw excepation, else delete it
        {
            Station stat = ExistStation(station.Code);
            if (stat != null)
            {
                DS.DataStore.Stations.Remove(stat.Clone());
                return;
            }
            throw new NotExistException("The code number not exist", station.Code);
        }
        #endregion Station


        #region LineStation
        public void addLineStation(DO.LineStation lineStation)  //the function get LineStation variable and check if it`s already exist in the DS, if yes throw excepation, else add it
        {
            LineStation lineS = ExistLineStation(lineStation.LineId, lineStation.Station);
            if (lineS == null)
            {
                DS.DataStore.LineStations.Add(lineStation);
            }
            throw new NotExistException("The Id station number not exist", lineS.LineId, lineS.Station);
        }
        public IEnumerable<DO.LineStation> getAllLineStations()  //the funcation return all the LineStation of all Lines
        {
            IEnumerable<LineStation> result =
          from station in DS.DataStore.LineStations
          select station.Clone();

            return result;
        }
        public void updateLineStation(DO.LineStation lineStation) //the function get LineStation variable and check if it`s exist in the DS, if no throw excepation, else update it
        {
            LineStation lineStat = ExistLineStation(lineStation.LineId, lineStation.Station);
            if (lineStat != null)
            {
                lineStat = lineStation.Clone();
                return;
            }
            throw new NotExistException("The Id station number not exist", lineStat.LineId, lineStat.Station);
        }
        private LineStation ExistLineStation(int lineId, int station)  //the function get LineStation variable and check if it`s exist in the DS and return it (if not exist return null)
        {
            return DS.DataStore.LineStations.FirstOrDefault(item => item.LineId == lineId && item.Station == station);
        }

        #endregion LineStation


        #region LineTrip
        public IEnumerable<LineTrip> getAllLineTrips() //the funcation return all the LineTrip of all Lines
        {
            throw new NotImplementedException();
        }
        #endregion LineTrip

    }
}
