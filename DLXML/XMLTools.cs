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
    public static class XMLTools
    {
        static string dir = @"xml\";
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        #region SaveLoadWithXElement
        public static void SaveListToXMLElement(XElement rootElem, string filePath)  //save XElement that the func get and enter it to the xml file (filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static XElement LoadListFromXMLElement(string filePath)  //load XElement from the xml file (filePath) that the func get and return it 
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath) //save list that the func get and enter it to the xml file (filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)  //load list from the xml file (filePath) that the func get and return it 
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
        public static XElement LineStationToXML(this DO.LineStation lineStation)  //the func get LineStation variable and create new XElement and enter it to the xml
        {
            return new XElement("LineStation",
                new XElement("LineId", lineStation.LineId),
                new XElement("Station", lineStation.Station),
                new XElement("LineStationIndex", lineStation.LineStationIndex),
                new XElement("NextStation", lineStation.NextStation),
                new XElement("PrevStation", lineStation.PrevStation),
                new XElement("DistanceFromTheLastStat", lineStation.DistanceFromTheLastStat),
                new XElement("TravelTimeFromTheLastStation", lineStation.TravelTimeFromTheLastStation.ToString()));
        }
        public static XElement BusToXML(this DO.Bus bus)    //the func get Bus variable and create new XElement and enter it to the xml
        {
            return new XElement("Bus",
                new XElement("StartPeilut", bus.StartPeilut.ToString()),
                    new XElement("LastTreat", bus.LastTreat.ToString()),
                new XElement("KiloFromLastTreat", bus.KiloFromLastTreat),
                new XElement("LicenseNum", bus.LicenseNum),
                new XElement("Kilometrage", bus.Kilometrage),
                new XElement("Fuel", bus.Fuel),
                new XElement("Status", bus.Status) );
        }
    }
}



