using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BLAPI;
using DLAPI;

namespace BL
{
    public class BLimplementation : IBl
    {


        #region Singleton
        static readonly BLimplementation instance = new BLimplementation();

        private BLimplementation() { } // default => private
        static BLimplementation() { }
        public static IBl Instance
        {
            get => instance;
        }// The public Instance property to use
        #endregion Singleton

        IDL dal = DLFactory.GetDL();

        #region Bus
        public void addBus(Bus bus)
        {
            if (bus.Fuel > 1200)
            {
                throw new TooMuchFuelException("The maximom fuel is: 1200 ", bus.Fuel);
            }
            if (DateTime.Compare(bus.LastTreat, bus.StartPeilut) < 0)
            {
                throw new WrongInputException("The start peilut date is more earlier then last treatment date", bus.StartPeilut);
            }
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus); // Convert to DO type
            try
            {
                dal.addBus(newBus);
            }
            catch (DO.AlreadyExistsException ex)
            {
                throw new AlreadyExistsException("The bus already exists!", ex);
            }
        }
        public void deleteBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus); // Convert to DO type
            try
            {
                dal.deleteBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This bus does not exist!", ex);
            }
        }
        public void fuelBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus); // Convert to DO type
            try
            {
                newBus.Fuel = 1200;
                ///
                /// update the status of the new bus
                ///
                if (DateTime.Now.Subtract(newBus.LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
                {
                    newBus.Status = (DO.STATUS)STATUS.INTREATMENT;
                }
                else if (newBus.KiloFromLastTreat >= 20000) // if the kilometrage is more then 20000km the the bus can`t do the ride throw exception
                {
                    newBus.Status = (DO.STATUS)STATUS.INTREATMENT;
                }
                newBus.Status = (DO.STATUS)STATUS.READYFORRIDE;
                dal.updateBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This bus does not exist!", ex);
            }
        }
        public DO.Bus BOBusToDoBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus.LicenseNum = bus.LicenseNum;
            newBus.Fuel = bus.Fuel;
            newBus.KiloFromLastTreat = bus.KiloFromLastTreat;
            newBus.LastTreat = bus.LastTreat;
            newBus.StartPeilut = bus.StartPeilut;
            newBus.Status = (DO.STATUS)bus.Status;
            return newBus;
        }
        public Bus DOBusToBOBus(DO.Bus bus)
        {
            Bus newBus = new Bus();
            newBus.LicenseNum = bus.LicenseNum;
            newBus.Fuel = bus.Fuel;
            newBus.KiloFromLastTreat = bus.KiloFromLastTreat;
            newBus.Kilometrage = bus.Kilometrage;
            newBus.LastTreat = bus.LastTreat;
            newBus.StartPeilut = bus.StartPeilut;
            newBus.Status = (STATUS)bus.Status;
            return newBus;
        }
        public Bus getBusByLicense(int licenseNum)
        {
            DO.Bus DOBus = dal.getBusByLicenseNum(licenseNum);
            return DOBusToBOBus(DOBus);
        }
        public IEnumerable<object> getAllBusses()
        {    // LINQ that get bus from DO type and send it to function to convert to BO  type
            IEnumerable<Bus> busses = from bus in dal.getAllBusses()
                                      let BObus = DOBusToBOBus((DO.Bus)bus)
                                      select BObus;

            return busses;
        }

        public void treatBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus);
            try
            {
                newBus.LicenseNum = bus.LicenseNum;
                newBus.LastTreat = DateTime.Now;
                newBus.KiloFromLastTreat = 0;
                newBus.Fuel = 1200;
                newBus.Status = (DO.STATUS)STATUS.READYFORRIDE;
                dal.updateBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This bus does not exist!", ex);
            }
        }
        public void updateBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus);
            try
            {
                dal.updateBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This bus does not exist!", ex);
            }
        }
        #endregion Bus


        #region Line
        public void addLine(Line line)
        {
            if (line.FirstStation == line.LastStation)
            {
                throw new WrongInputException("entered only one station", line.LastStation);
            }
            DO.Line newLine = new DO.Line();
            newLine.Id = -1;
            newLine = BOLineToDOLine(line); // Convert to DO type
            dal.addLine(newLine);
        }
        public void deleteLine(Line line)
        {
            DO.Line newLine = new DO.Line();
            newLine = BOLineToDOLine(line); // Convert to DO type
            try
            {
                dal.deleteLine(newLine);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This line does not exist!", ex);
            }
        }
        public Line DOLineToBOLine(DO.Line line)
        {
            Line newLine = new Line();
            newLine.Id = line.Id;
            newLine.Code = line.Code;
            newLine.Area = (AREAS)line.Area;
            newLine.FirstStation = line.FirstStation;
            newLine.LastStation = line.LastStation;
            return newLine;
        }
        public DO.Line BOLineToDOLine(Line line)
        {
            DO.Line newLine = new DO.Line();
            newLine.Id = line.Id;
            newLine.Code = line.Code;
            newLine.Area = (DO.AREAS)line.Area;
            newLine.FirstStation = line.FirstStation;
            newLine.LastStation = line.LastStation;
            return newLine;
        }
        public Line getLineById(int id)
        {
            DO.Line DOLine = dal.getLineById(id);
            return DOLineToBOLine(DOLine);
        }
        public IEnumerable<object> getAllLines()
        {
            IEnumerable<Line> lines = from line in dal.getAllLines()
                                      let BOLine = DOLineToBOLine((DO.Line)line)
                                      select BOLine;
            return lines;
        }
        public void updateLine(Line line)
        {
            DO.Line newLine = new DO.Line();
            newLine = BOLineToDOLine(line); // Convert to DO type

            try
            {
                dal.updateLine(newLine);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This line does not exist!", ex);
            }
        }
        #endregion Line


        #region User
        public bool loginAdmin(string username, string password)
        {
            DO.User admin = dal.getAdmin();
            return username == admin.UserName && password == admin.Password;

            //return true;
        }
        #endregion User


        #region Station
        public IEnumerable<object> getAllStationCode()
        {
            IEnumerable<int> stationesCode = from station in dal.getAllStations()
                                             let BOStation = DOStationToBOStation((DO.Station)station)
                                             select BOStation.Code;
            return (IEnumerable<object>)stationesCode;
        }
        public void updateStation(Station station)
        {
            DO.Station newStation = new DO.Station();
            newStation = BOStationToDOStation(station); // Convert to DO type
            try
            {
                dal.updateStation(newStation);
            }
            catch (DO.NotExistStationException ex)
            {
                throw new NotExistStationException("This station does not exist!", ex);
            }
        }
        public void addStation(Station station)
        {
            DO.Station newStation = new DO.Station();
            newStation = BOStationToDOStation(station); // Convert to DO type
            try
            {
                dal.addStation(newStation);
            }
            catch (DO.AlreadyExistsException ex)
            {
                throw new AlreadyExistsException("This station already exist!", ex);
            }
        }
        public void deleteStation(Station station)
        {
            DO.Station newStation = new DO.Station();
            newStation = BOStationToDOStation(station); // Convert to DO type
            try
            {
                dal.deleteStation(newStation);

            }
            catch (DO.NotExistStationException ex)
            {
                throw new NotExistStationException("This station does not exist!", ex);
            }
        }
        public Station getStationByCode(int code)
        {
            List<DO.Station> stations = dal.getAllStations().Cast<DO.Station>().ToList();
            return DOStationToBOStation(stations.Find(x => x.Code == code));
        }
        public DO.Station BOStationToDOStation(Station station)
        {
            DO.Station newStation = new DO.Station();
            newStation.Location = station.Location;
            newStation.Address = station.Address;
            newStation.Name = station.Name;
            newStation.Code = station.Code;
            return newStation;
        }
        public Station DOStationToBOStation(DO.Station station)
        {
            Station newStation = new Station();
            newStation.Location = station.Location;
            newStation.Address = station.Address;
            newStation.Name = station.Name;
            newStation.Code = station.Code;
            return newStation;
        }
        public IEnumerable<object> getAllStations()
        {
            IEnumerable<Station> stationes = from station in dal.getAllStations()
                                             let BOStation = DOStationToBOStation((DO.Station)station)
                                             select BOStation;
            return stationes;
        }
        public double getDistanceBetweenTwoStations(LineStation from, LineStation to)
        {
            List<DO.LineStation> lineStations = dal.getAllLineStations().ToList();
            DO.LineStation from_ = lineStations.Find(x => x.LineId == from.LineId && x.Station == from.Station);
            DO.LineStation to_ = lineStations.Find(x => x.LineId == to.LineId && x.Station == to.Station);
            if (from_ == null)
                throw new NotExistStationException("this code is not a exist", from.LineId);
            if (to_ == null)
                throw new NotExistStationException("this code is not a exist", to.LineId);
            int indexOne = lineStations.FindIndex(x => x.LineId == from.LineId);
            int indexTwo = lineStations.FindIndex(x => x.LineId == to.LineId);
            double distanceF = 0; // the distance from the first station to station "from"
            double distanceT = 0; // the distance from the first station to station "to"
            for (int i = 1; i < indexOne; i++)
            {
                distanceF += lineStations[i].DistanceFromTheLastStat;
            }
            for (int i = 1; i < indexTwo; i++)
            {
                distanceT += lineStations[i].DistanceFromTheLastStat;
            }
            return Math.Abs(distanceF - distanceT);
        }
        #endregion Station


        #region LineStation
        public void AddFollowingStation(LineStation lineStation, double distanceFromThePrevToFollowing, TimeSpan timeFromThePrevToFollowing)
        {
            List<DO.LineStation> lineStations = dal.getAllLineStations().ToList();
            DO.LineStation lineStationToUpdate = lineStations.Find(x => (x.LineId == lineStation.LineId) && (x.Station == lineStation.Station));
            DO.LineStation nextStation = lineStations.Find(x => (x.LineId == lineStation.LineId) && (x.Station == lineStationToUpdate.NextStation));
            lineStationToUpdate.NextStation = lineStation.NextStation;
            try
            {
                dal.updateLineStation(lineStationToUpdate);
            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This line does not exist!", ex);
            }
            DO.LineStation newLineStation = new DO.LineStation();
            if ((nextStation == null) && (nextStation.NextStation == nextStation.Station)) // if lineStationToUpdate is the last station in the ride
            {
                newLineStation.LineId = lineStationToUpdate.LineId;
                newLineStation.LineStationIndex = lineStationToUpdate.LineStationIndex + 1;
                newLineStation.Station = lineStationToUpdate.NextStation;
                newLineStation.NextStation = newLineStation.Station;
                newLineStation.PrevStation = lineStationToUpdate.Station;
                newLineStation.DistanceFromTheLastStat = distanceFromThePrevToFollowing;
                newLineStation.TravelTimeFromTheLastStation = timeFromThePrevToFollowing;
                try
                {
                    dal.addLineStation(newLineStation);
                }
                catch (DO.NotExistException ex)
                {
                    throw new NotExistException("This line does not exist!", ex);
                }
                return;
            }
            newLineStation.LineId = lineStationToUpdate.LineId;
            newLineStation.LineStationIndex = lineStationToUpdate.LineStationIndex + 1;
            newLineStation.Station = lineStationToUpdate.NextStation;
            newLineStation.NextStation = nextStation.Station;
            newLineStation.PrevStation = lineStationToUpdate.Station;
            newLineStation.DistanceFromTheLastStat = distanceFromThePrevToFollowing;
            newLineStation.TravelTimeFromTheLastStation = timeFromThePrevToFollowing;
            try
            {
                dal.addLineStation(newLineStation);
            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This line does not exist!", ex);
            }
            ++nextStation.LineStationIndex;
            nextStation.PrevStation = newLineStation.Station;
            //prev
            DO.LineStation updateStations = lineStations.FirstOrDefault(x => (x.LineId == nextStation.LineId) && (x.Station == nextStation.NextStation));
            while (updateStations != null)                  // update the rest of LineStationIndex in the same LineId
            {
                updateStations.LineStationIndex++;
                try
                {
                    dal.updateLineStation(updateStations);
                }
                catch (DO.NotExistException ex)
                {
                    throw new NotExistException("This line does not exist!", ex);
                }
                if (updateStations.Station == updateStations.NextStation)
                    break;
                updateStations = lineStations.FirstOrDefault(x => (x.LineId == updateStations.LineId) && (x.Station == updateStations.NextStation));
            }
        }
        public IEnumerable<LineStation> getLineStationByCode(int code)
        {
            IEnumerable<LineStation> lineStations = from DOlineStat in dal.getAllLineStations()
                                                    let DOlineStation = (DO.LineStation)DOlineStat
                                                    where DOlineStation.Station == code
                                                    select DOLineStationToBOLineStation(DOlineStation);

            return lineStations.ToList();
        }
        public void UpdateDistanceLineStation(int lineId, int station, double distance)
        {

            List<DO.LineStation> lineStations = dal.getAllLineStations().Cast<DO.LineStation>().ToList();
            DO.LineStation lineStation = lineStations.Find(x => x.LineId == lineId && x.Station == station);
            lineStation.DistanceFromTheLastStat = distance;
            dal.updateLineStation(lineStation);
        }
        public void UpdateTimeLineStation(int lineId, int station, TimeSpan time)
        {
            List<DO.LineStation> lineStations = dal.getAllLineStations().Cast<DO.LineStation>().ToList();
            DO.LineStation lineStation = lineStations.Find(x => x.LineId == lineId && x.Station == station);
            lineStation.TravelTimeFromTheLastStation = time;
            dal.updateLineStation(lineStation);
        }
        public void updateLineStation(LineStation lineStation)
        {
            DO.LineStation newLineStation = new DO.LineStation();
            newLineStation = BOLineStationToDOLineStation(lineStation); // Convert to DO type
            try
            {
                dal.updateLineStation(newLineStation);

            }
            catch (DO.NotExistException ex)
            {
                throw new NotExistException("This line does not exist!", ex);
            }
        }
        public IEnumerable<LineStation> getLineStationsForLine(Line line)
        {
            IEnumerable<LineStation> lineStations = from DOline in dal.getAllLineStations()
                                                    let DOlineStation = (DO.LineStation)DOline
                                                    where DOlineStation.LineId == line.Id
                                                    select DOLineStationToBOLineStation(DOlineStation);

            return lineStations;

        }
        public DO.LineStation BOLineStationToDOLineStation(LineStation lineStation)
        {
            DO.LineStation newLineStation = new DO.LineStation();
            newLineStation.LineId = lineStation.LineId;
            newLineStation.Station = lineStation.Station;
            newLineStation.LineStationIndex = lineStation.LineStationIndex;
            newLineStation.PrevStation = lineStation.PrevStation;
            newLineStation.NextStation = lineStation.NextStation;
            newLineStation.DistanceFromTheLastStat = lineStation.DistanceFromTheLastStat;
            newLineStation.TravelTimeFromTheLastStation = lineStation.TravelTimeFromTheLastStation;
            return newLineStation;
        }
        public LineStation DOLineStationToBOLineStation(DO.LineStation lineStation)
        {
            LineStation newLineStation = new LineStation();
            List<DO.Station> stations = dal.getAllStations().Cast<DO.Station>().ToList();
            newLineStation.Name = stations.Find(x => x.Code == lineStation.Station).Name;
            newLineStation.LineId = lineStation.LineId;
            int lastStationId = dal.getAllLines().Cast<DO.Line>().ToList().Find(x => x.Id == lineStation.LineId).LastStation;
            newLineStation.LastStationName = dal.getAllStations().Cast<DO.Station>().ToList().Find(x => x.Code == lastStationId).Name;
            newLineStation.Station = lineStation.Station;
            newLineStation.LineStationIndex = lineStation.LineStationIndex;
            newLineStation.PrevStation = lineStation.PrevStation;
            newLineStation.NextStation = lineStation.NextStation;
            newLineStation.DistanceFromTheLastStat = lineStation.DistanceFromTheLastStat;
            newLineStation.TravelTimeFromTheLastStation = lineStation.TravelTimeFromTheLastStation;
            return newLineStation;
        }
        #endregion LineStation


        public List<LineTiming> startSimulator(List<LineStation> BOLineStations, TimeSpan startTime)
        {
            List<Line> lines = (from stations in BOLineStations
                                select getLineById(stations.LineId)).ToList();
            List<LineTiming> lineTimings = new List<LineTiming>();

            foreach (LineStation lineStation in BOLineStations)
            {
                int firstStation = getLineById(lineStation.LineId).FirstStation;
                int currentStation = lineStation.Station;
                List<LineStation> lineStationsFromLine = getLineStationsForLine(getLineById(lineStation.LineId)).ToList();
                TimeSpan totalTime = new TimeSpan();
                for (int i = 1; i < lineStation.LineStationIndex; i++)
                {
                    totalTime += lineStationsFromLine[i].TravelTimeFromTheLastStation;
                }
                DO.LineTrip trip = dal.getAllLineTrips().Where(x => x.LineId == lineStation.LineId).FirstOrDefault();
                for (int i = 0; i < 5; i++)
                {
                    LineTiming lineTiming = new LineTiming();
                    lineTiming.CurrentStation = currentStation;
                    lineTiming.LineId = lineStation.LineId;
                    if (i == 0)
                    {
                        lineTiming.StartTime = trip.StartAt;
                        lineTiming.TimeToArrive = lineTiming.StartTime + trip.FinishAt - trip.StartAt - startTime;
                        lineTiming.ArrivalTime = lineTiming.StartTime + trip.FinishAt - trip.StartAt;
                    }
                    else
                    {
                        lineTiming.StartTime = lineTimings[i - 1].ArrivalTime + trip.Frequency;
                        lineTiming.TimeToArrive = (lineTiming.StartTime + trip.FinishAt - trip.StartAt - startTime);
                        lineTiming.ArrivalTime = lineTiming.StartTime + trip.FinishAt - trip.StartAt;
                    }
                    lineTimings.Add(lineTiming);
                }

            }
            return lineTimings;
        }

        public TimeSpan updateTime(LineTiming lineTiming, int rate)
        {
            lineTiming.TimeToArrive -= new TimeSpan(0, 0, rate);
            return lineTiming.TimeToArrive;
        }
    }


}
