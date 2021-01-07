using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using BO;
using DLAPI;

namespace BL
{
    public class BLimplementation : IBl
    {
       
        IDL dal = DLFactory.GetDL();
        public void addBus(BO.Bus bus, int licenseNum)
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
            newBus.LicenseNum = licenseNum;
            try
            {
                dal.addBus(newBus);
            }
            catch (DO.AlreadyExistsException ex)
            {
                throw new BO.AlreadyExistsException("The bus already exists!", ex);
            }
        }

        public void addLine(Line line, int id, int code)
        {
            if (line.FirstStation == line.LastStation)
            {
                throw new WrongInputException("entered only one station", line.LastStation);
            }
            DO.Line newLine = new DO.Line();
            newLine.Id = id;
            newLine.Code = code;
            try
            {
                dal.addLine(newLine);
            }
            catch (DO.AlreadyExistsException ex)
            {
                throw new BO.AlreadyExistsException("This line already exist!", ex);
            }
        }

        public void addStation(Station station, int code)
        {
            DO.Station newStation = new DO.Station();
            newStation.Code = code;
            try
            {
                dal.addStation(newStation);
            }
            catch (DO.AlreadyExistsException ex)
            {
                throw new BO.AlreadyExistsException("This station already exist!", ex);
            }
        }

        public void deleteBus(Bus bus, int licenseNum)
        {
            DO.Bus newBus = new DO.Bus();
            newBus.LicenseNum = licenseNum;
            try
            {
                dal.deleteBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This bus does not exist!", ex);
            }
        }

        public void deleteLine(Line line, int id)
        {
            DO.Line newLine = new DO.Line();
            newLine.Id = id;
            try
            {
                dal.deleteLine(newLine);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This line does not exist!", ex);
            }
        }

        public void deleteStation(Station Station, int code)
        {
            DO.Station newStation = new DO.Station();
            newStation.Code = code;
            try
            {
                dal.deleteStation(newStation);

            }
            catch (DO.NotExistStationException ex)
            {
                throw new BO.NotExistStationException("This station does not exist!", ex);
            }
        }

        public void fuelBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus); // Convert to DO type
            try
            {
                dal.fuelBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This bus does not exist!", ex);
            }
        }
        public DO.Bus BOBusToDoBus(BO.Bus bus)
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

        public BO.Bus DOBusToBOBus(DO.Bus bus)
        {
            BO.Bus newBus = new BO.Bus();
            newBus.LicenseNum = bus.LicenseNum;
            newBus.Fuel = bus.Fuel;
            newBus.KiloFromLastTreat = bus.KiloFromLastTreat;
            newBus.LastTreat = bus.LastTreat;
            newBus.StartPeilut = bus.StartPeilut;
            newBus.Status = (BO.STATUS)bus.Status;
            return newBus;
        }

        public IEnumerable<object> getAllBusses()
        {    // LINQ that get bus from DO type and send it to function to convert to BO  type
            IEnumerable<BO.Bus> busses = from bus in dal.getAllBusses()
                                         let BObus = DOBusToBOBus((DO.Bus)bus)
                                         select BObus;

            return busses;
        }
        public BO.Line DOLineToBOLine(DO.Line line)
        {
            BO.Line newLine = new BO.Line();
            newLine.Area = (BO.AREAS)line.Area;
            newLine.FirstStation = line.FirstStation;
            newLine.LastStation = line.LastStation;
            return newLine;
        }

        public IEnumerable<object> getAllLines()
        {
            IEnumerable<BO.Line> lines = from line in dal.getAllLines()
                                         let BOLine = DOLineToBOLine((DO.Line)line)
                                         select BOLine;
            return lines;
        }

        public BO.Station DOStationToBOStation(DO.Station station)
        {
            BO.Station newStation = new BO.Station();
            newStation.Location = station.Location;
            newStation.Address = station.Address;
            newStation.DistanceFromTheLastStat = station.DistanceFromTheLastStat;
            newStation.TravelTimeFromTheLastStation = station.TravelTimeFromTheLastStation;
            return newStation;
        }
        public IEnumerable<object> getAllStations()
        {
            IEnumerable<BO.Station> stationes = from station in dal.getAllStations()
                                                let BOStation = DOStationToBOStation((DO.Station)station)
                                                select BOStation;
            return stationes;
        }

        public double getDistanceBetweenTwoStations(Station from, int codeF, Station to, int codeT)
        {

            DO.Station newStationFrom = new DO.Station();
            newStationFrom.Code = codeF;
            DO.Station newStationTo = new DO.Station();
            newStationTo.Code = codeT;

            double distanc;
            try
            {
                distanc = dal.getDistanceBetweenTwoStations(newStationFrom, newStationTo);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistStationException("This station does not exist!", ex);
            }
            return distanc;
        }

        public Bus getBusByLicense(int licenseNum)
        {
            DO.Bus bus = dal.getBusByLicenseNum(licenseNum);
            return DOBusToBOBus(bus);

        }

        public void treatBus(Bus bus)
        {
            DO.Bus newBus = new DO.Bus();
            newBus = BOBusToDoBus(bus);
            try
            {
                dal.treatBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This bus does not exist!", ex);
            }
        }

        public void updateBus(Bus bus, int licenseNum)
        {
            DO.Bus newBus = new DO.Bus();
            newBus.LicenseNum = licenseNum;
            try
            {
                dal.updateBus(newBus);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This bus does not exist!", ex);
            }
        }

        public void updateLine(Line line, int id)
        {
            DO.Line newLine = new DO.Line();
            newLine.Id = id;
            try
            {
                dal.updateLine(newLine);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This line does not exist!", ex);
            }
        }

        public void updateLineStation(LineStation lineStation, int lineId)
        {
            DO.LineStation newLineStation = new DO.LineStation();
            newLineStation.LineId = lineId;
            try
            {
                dal.updateLineStation(newLineStation);

            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("This line does not exist!", ex);
            }
        }

        public void updateStation(Station Station, int code)
        {
            DO.Station newStation = new DO.Station();
            newStation.Code = code;
            try
            {
                dal.deleteStation(newStation);

            }
            catch (DO.NotExistStationException ex)
            {
                throw new BO.NotExistStationException("This station does not exist!", ex);
            }
        }

        public bool loginAdmin(string username, string password)
        {
            /* DO.User admin = dal.getAdmin();
             return username == admin.UserName && password == admin.Password; */
          
            return true;
        }
    }
}
