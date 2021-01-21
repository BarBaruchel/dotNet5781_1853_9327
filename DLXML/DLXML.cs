using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    public class DLXML : IDL
    {
        #region singleton
        static readonly DLXML instance = new DLXML();
        public static DLXML Instance { get => instance; }
        static string dir = @"xml\";

        static DLXML()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

        }
        #endregion
        //List<DO.Bus> Buses = new List<DO.Bus>();
        //List<DO.LineStation> lineStations = new List<LineStation>();

        XElement LineStationRoot = new XElement("LineStations");
        XElement BusRoot = new XElement("Buses");

        string BusPath = @"Bus.xml";
        string lineStationPath = @"LineStation.xml";
        string LinePath = @"Line.xml";
        string StationPath = @"Station.xml";
        private DLXML()
        {

            // BusRoot.Save(BusPath);
            // SaveToXML(Buses, BusPath);
        }

        public void addBus(Bus bus)
        {
            //bus.id = 
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

        public void treatBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void fuelBus(Bus bus)
        {
            throw new NotImplementedException();
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
            Bus busToDelete = buses.FirstOrDefault(x => x.LicenseNum == bus.LicenseNum);
            if (busToDelete == null)
                throw new NotExistException("This license num doenst exist", bus.LicenseNum);
            buses.Remove(busToDelete);
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
            throw new NotImplementedException();
        }

        public void updateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void deleteLine(Line line)
        {
            throw new NotImplementedException();
        }

        public Line getLineById(int id)
        {
            throw new NotImplementedException();
        }

        public User getAdmin()
        {
            throw new NotImplementedException();
        }

        public void addStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void AddLineStation(LineStation lineStation)
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

        public void updateStation(Station Station)
        {
            throw new NotImplementedException();
        }

        public void deleteStation(Station Station)
        {
            throw new NotImplementedException();
        }

        public void AddFollowingStation(LineStation lineStation, double distanceFromTheFollowingToTheNext, TimeSpan timeFromTheFollowingToTheNext)
        {
            throw new NotImplementedException();
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

        public double getDistanceBetweenTwoStations(LineStation from, LineStation to)
        {
            throw new NotImplementedException();
        }

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

        public void Bedika(Bus bus, int distance)
        {
            throw new NotImplementedException();
        }

 
    }
}
