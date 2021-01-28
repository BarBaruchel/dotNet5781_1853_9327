using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    public class DLXML : IDL
    {
        #region singleton
        static readonly DLXML instance = new DLXML();
        public static IDL Instance { get => instance; }
     

        static DLXML()
        {
            /*
          
            DS.DataStore.Init();
            DS.DataStore.InitRepostory();

            //DS.DataStore.Init();
            //DS.DataStore.InitRepostory();

            List<DO.Bus> buses = DS.DataStore.Busses;
            XElement bussesRoot = XMLTools.LoadListFromXMLElement(dir + BusPath);
            foreach (Bus bus in buses)
            {
                bussesRoot.Add(XMLTools.BusToXML(bus));
            }
            bussesRoot.Save(dir + BusPath);

            */

        }
        #endregion singleton


        static XElement LineStationRoot = new XElement("LineStations");
        static XElement BusRoot = new XElement("Buses");
        static XElement StationRoot = new XElement("Stations");
        static XElement RunningNumbersRoot = new XElement("IDS");

        static string BusPath = @"Bus.xml";
        static string lineStationPath = @"LineStation.xml";
        static string LinePath = @"Line.xml";
        static string StationPath = @"Station.xml";
        static string RunningNumbersPath = @"RunningNumbers.xml";
        //List<DO.Bus> Buses = new List<DO.Bus>();
        //List<DO.LineStation> lineStations = new List<LineStation>();

   
        private DLXML()
        {
    
        }
        //public void AddPerson(DO.Person person)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    XElement per1 = (from p in personsRootElem.Elements()
        //                     where int.Parse(p.Element("ID").Value) == person.ID
        //                     select p).FirstOrDefault();

        //    if (per1 != null)
        //        throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");

        //    XElement personElem = new XElement("Person", new XElement("ID", person.ID),
        //                          new XElement("Name", person.Name),
        //                          new XElement("Street", person.Street),
        //                          new XElement("HouseNumber", person.HouseNumber.ToString()),
        //                          new XElement("City", person.City),
        //                          new XElement("BirthDate", person.BirthDate),
        //                          new XElement("PersonalStatus", person.PersonalStatus.ToString()),
        //                          new XElement("Duration", person.Duration.ToString()));

        //    personsRootElem.Add(personElem);

        //    XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //}
        public void addBus(Bus bus)
        {
            List<DO.Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);
            if (buses.FirstOrDefault(x => x.LicenseNum == bus.LicenseNum) != null)
                throw new AlreadyExistsException("This license num already exists!", bus.LicenseNum);

            buses.Add(bus);
            XMLTools.SaveListToXMLSerializer<Bus>(buses, BusPath);
        }
        DO.Bus ConvertBus(XElement element)  // convert xml object to Bus Type 
        {
            Bus bus = new Bus();
            var ser = new XmlSerializer(typeof(Bus));
            return (Bus)ser.Deserialize(element.CreateReader());
        }
        public IEnumerable<DO.Bus> getAllBusses()
        {
            List<DO.Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            return from bus in buses
                   select bus;
        }
        public void updateBus(Bus bus)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);
            Bus busToUpdate = buses.FirstOrDefault(x => x.LicenseNum == bus.LicenseNum);
            if (busToUpdate == null)
                throw new NotExistException("This license num doenst exist", bus.LicenseNum);
            buses.Remove(busToUpdate);
            buses.Add(bus);
            XMLTools.SaveListToXMLSerializer<Bus>(buses, BusPath);

        }
        public void deleteBus(Bus bus)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);
            Bus busToDelete = buses.FirstOrDefault(x => x.LicenseNum == bus.LicenseNum);
            if (busToDelete == null)
                throw new NotExistException("This license num doenst exist", bus.LicenseNum);
            buses.Remove(busToDelete);
            XMLTools.SaveListToXMLSerializer<Bus>(buses, BusPath);
        }
        public Bus getBusByLicenseNum(int licenseNum)
        {
            List<Bus> buses = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);
            Bus busToReturn = (from bus in buses
                               where bus.LicenseNum == licenseNum
                               select bus).FirstOrDefault();

            if (busToReturn == null)
                throw new NotExistException("License num doesnt exist!", licenseNum);
            return busToReturn;
        }

        public IEnumerable<DO.Line> getAllLines()
        {
            List<DO.Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            return from line in lines
                   select line;
        }
        public void addLine(Line line)
        {
            List<DO.Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            XElement RunningNumbersRoot = XMLTools.LoadListFromXMLElement(RunningNumbersPath);
            int id = Convert.ToInt32(RunningNumbersRoot.Element("IDS").Element("LineId").Value);
            RunningNumbersRoot.Element("IDS").Element("LineId").Value = (++id).ToString();
            line.Id = id;
            lines.Add(line);
            XMLTools.SaveListToXMLSerializer<Line>(lines, LinePath);
            XMLTools.SaveListToXMLElement(RunningNumbersRoot, RunningNumbersPath);
        }

        public void updateLine(Line line)
        {
            List<DO.Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            Line lineToUpdate = lines.FirstOrDefault(x => x.Id == line.Id);
            if (lineToUpdate == null)
                throw new NotExistException("The Id  number not exist", line.Id);
            lines.Remove(lineToUpdate);
            lines.Add(line);
            XMLTools.SaveListToXMLSerializer<Line>(lines, LinePath);
        }

        public void deleteLine(Line line)
        {
            List<DO.Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            Line lineToDelete = lines.FirstOrDefault(x => x.Id == line.Id);
            if (lineToDelete == null)
                throw new NotExistException("The Id  number not exist", line.Id);
            lines.Remove(lineToDelete);
            XMLTools.SaveListToXMLSerializer<Line>(lines, LinePath);
        }

        public Line getLineById(int id)
        {
            List<DO.Line> lines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            Line lineToReturn = (from line in lines
                                 where line.Id == id
                                 select line).FirstOrDefault();
            return lineToReturn;
        }

        public User getAdmin()
        {
            User admin = new User { UserName = "Ranit", Password = "Bar", Admin = true };
            return admin;
        }

        public void addStation(Station station)
        {
            List<DO.Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (stations.FirstOrDefault(x => x.Code == station.Code) != null)
            {
                XElement stationXml = new XElement("Station",
                new XElement("Code", station.Code),
                new XElement("Latitude", station.Location.Latitude),
                new XElement("Longitude", station.Location.Longitude),
                new XElement("Name", station.Name),
                new XElement("Address", station.Address)
                );
                StationRoot.Add(stationXml);
                StationRoot.Save(StationPath);
            }
            throw new AlreadyExistsException("The code number already exist", station.Code);

        }

        public void addLineStation(LineStation lineStation)
        {
            XElement lineStationRoot = XMLTools.LoadListFromXMLElement(lineStationPath);

            XElement line1 = (from p in lineStationRoot.Elements()
                              where int.Parse(p.Element("LineId").Value) == lineStation.LineId
                              select p).FirstOrDefault();
            if (line1 != null)
                throw new AlreadyExistsException("This is already exists!", lineStation.LineId);


            LineStationRoot.Add(lineStation.LineStationToXML());
            LineStationRoot.Save(lineStationPath);
        }
        public IEnumerable<DO.Station> getAllStations()
        {
            List<DO.Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            return from station in stations
                   select station;
        }

        public void updateStation(Station station)
        {
            List<DO.Station> stations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            Station stationToUpdate = stations.FirstOrDefault(x => x.Code == station.Code);
            if (stationToUpdate == null)
                throw new NotExistException("The code number not exist", station.Code);
            stations.Remove(stationToUpdate);
            stations.Add(station);
            XMLTools.SaveListToXMLSerializer<Station>(stations, StationPath);
        }

        public void deleteStation(Station station)
        {
            List<DO.Station> stations = XMLTools.LoadListFromXMLSerializer<DO.Station>(StationPath);
            Station stationToDelete = stations.FirstOrDefault(x => x.Code == station.Code);
            if (stationToDelete == null)
                throw new NotExistException("The code number not exist", station.Code);
            stations.Remove(stationToDelete);
            XMLTools.SaveListToXMLSerializer<Station>(stations, StationPath);

        }
        public void updateLineStation(LineStation lineStation)
        {
            XElement lineStationRootElem = XMLTools.LoadListFromXMLElement(lineStationPath);
            XElement line1 = (from p in lineStationRootElem.Elements()
                              where int.Parse(p.Element("LineId").Value) == lineStation.LineId
                              select p).FirstOrDefault();
            if (line1 == null)
                throw new NotExistException("This id doesnt  exist!", lineStation.LineId);

            line1.Element("LineId").Value = lineStation.LineId.ToString();
            line1.Element("Station").Value = lineStation.Station.ToString();
            line1.Element("LineStationIndex").Value = lineStation.LineStationIndex.ToString();
            line1.Element("NextStation").Value = lineStation.NextStation.ToString();
            line1.Element("PrevStation").Value = lineStation.PrevStation.ToString();
            line1.Element("DistanceFromTheLastStat").Value = lineStation.DistanceFromTheLastStat.ToString();
            line1.Element("TravelTimeFromTheLastStation").Value = lineStation.TravelTimeFromTheLastStation.ToString();

            XMLTools.SaveListToXMLElement(lineStationRootElem, lineStationPath);



        }

        /*public double getDistanceBetweenTwoStations(LineStation from, LineStation to)
        {
            throw new NotImplementedException();
        }*/

        public IEnumerable<DO.LineStation> getAllLineStations()
        {
            XElement lineStationRootElem = XMLTools.LoadListFromXMLElement(lineStationPath);
            List<LineStation> lineStations = (from p in lineStationRootElem.Elements()
                                              select new LineStation()
                                              {
                                                  LineId = Convert.ToInt32(p.Element("LineId").Value),
                                                  LineStationIndex = Convert.ToInt32(p.Element("LineStationIndex")),
                                                  Station = Convert.ToInt32(p.Element("Station")),
                                                  DistanceFromTheLastStat = Convert.ToDouble(p.Element("DistanceFromTheLastStat")),
                                                  NextStation = Convert.ToInt32(p.Element("NextStation")),
                                                  TravelTimeFromTheLastStation = TimeSpan.Parse(p.Element("TravelTimeFromTheLastStation").ToString()),
                                                  PrevStation = Convert.ToInt32(p.Element("PrevStation"))
                                              }).ToList();

            return lineStations;

        }
    }
}
