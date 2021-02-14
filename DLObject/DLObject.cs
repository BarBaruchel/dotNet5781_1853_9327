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
        public IEnumerable<DO.Bus> getAllBusses()
        {
            /// LINQ
            IEnumerable<Bus> result =
            from bus in DS.DataStore.Busses
            select bus.Clone();

            ////// delete after
            //XMLTools.SaveListToXMLSerializer<DO.Bus>(result.ToList(), "Bus.xml");
            return result;
        }
        public DO.Bus getBusByLicenseNum(int licenseNum)
        {
            return DS.DataStore.Busses.Find(x => x.LicenseNum == licenseNum).Clone();
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
        public DO.Line getLineById(int id)
        {
            return DS.DataStore.Lines.Find(x => x.Id == id).Clone();
        }
        public IEnumerable<DO.Line> getAllLines()
        {
            /// LINQ
            IEnumerable<Line> result =
            from line in DS.DataStore.Lines
            select line.Clone();

            ////// delete after
            //XMLTools.SaveListToXMLSerializer<DO.Line>(result.ToList(), "Line.xml");
            return result;
        }
        public void addLine(DO.Line line)
        {
            line.Id = ++DS.DataStore.RunningNum;
            DS.DataStore.Lines.Add(line);
        }
        private Line ExistLine(int id)
        {
            return DS.DataStore.Lines.FirstOrDefault(item => item.Id == id).Clone();
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
            return DS.DataStore.admin.Clone();
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
        public IEnumerable<DO.Station> getAllStations()
        {
            /// LINQ
            IEnumerable<Station> result =
            from station in DS.DataStore.Stations
            select station.Clone();

            ////// delete after
            // XMLTools.SaveListToXMLSerializer<DO.Station>(result.ToList(), "Station.xml");

            return result;
        }
        public void updateStation(DO.Station station)
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
        public void deleteStation(DO.Station station)
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
        public void addLineStation(DO.LineStation lineStation)
        {
            LineStation lineS = ExistLineStation(lineStation.LineId, lineStation.Station);
            if (lineS == null)
            {
                DS.DataStore.LineStations.Add(lineStation);
            }
            throw new NotExistException("The Id station number not exist", lineS.LineId, lineS.Station);
        }
        public IEnumerable<DO.LineStation> getAllLineStations()
        {
            IEnumerable<LineStation> result =
          from station in DS.DataStore.LineStations
          select station.Clone();

            ////// delete after
            //XMLTools.SaveListToXMLSerializer<DO.LineStation>(result.ToList(), "LineStation.xml");

            return result;
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

        public IEnumerable<LineTrip> getAllLineTrips()
        {
            throw new NotImplementedException();
        }
        #endregion LineStation

















    }
}
