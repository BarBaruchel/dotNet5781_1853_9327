using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class DataStore
    {
        private static List<Bus> busses = new List<Bus>();
        public static List<Bus> Busses { get => busses; }
        private static List<User> users = new List<User>();
        public static List<User> Users { get => users; }
        public static List<Station> stations = new List<Station>();
        public static List<Station> Stations { get => stations; }
        private static List<Line> lines = new List<Line>();
        public static List<Line> Lines { get=>lines; }
        private static List<LineStation> lineStations = new List<LineStation>();
        public static List<LineStation> LineStations { get=>lineStations; }
       
        public static int RunningNum = 1;
        public static User admin;
        private static Random rand = new Random();

        public static void Init()
        {
            admin = new User();
            admin.UserName = "Ranit";
            admin.Password = "Bar";
            admin.Admin = true;
            users.Add(admin);
  
        }

        public static void InitRepostory()
        {

            ///<summary>
            ///create 10 buses and enter them to the bus list 
            ///</summary>
            for (int i = 0; i < 9; i++)  // create the buses to the table in the mainWindow
            {
                Bus newBus = new Bus();
                newBus.Kilometrage = rand.Next();
                newBus.KiloFromLastTreat = newBus.Kilometrage;
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                newBus.StartPeilut = start.AddDays(rand.Next(range)).Date;
                if (newBus.StartPeilut.Year < 2018) //raffled rishuy nuber according to the start peilut
                {
                    int licenseNum = rand.Next(1000000, 10000000);
                    newBus.LicenseNum = licenseNum;
                }
                else
                {
                    int licenseNum = rand.Next(10000000, 100000000);
                    newBus.LicenseNum = licenseNum;
                }
                start = newBus.StartPeilut;
                range = (DateTime.Today - start).Days;
                newBus.LastTreat = start.AddDays(rand.Next(range));
                newBus.KiloFromLastTreat = rand.Next(1, 40000);
                newBus.Fuel = rand.Next(1, 1201);
                ///
                /// update the status of the new bus
                ///
                if (DateTime.Now.Subtract(newBus.LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
                {
                    newBus.Status = STATUS.INTREATMENT;
                }
                else if (newBus.KiloFromLastTreat >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
                {
                    newBus.Status = STATUS.INTREATMENT;
                }
                else if (!(newBus.Fuel < 0))  // if the ride is more then 1200km then the bus can`t do the ride throw exception
                {
                    newBus.Status = STATUS.READYFORRIDE;
                }
                busses.Add(newBus);

            }
            busses[1].KiloFromLastTreat = 19000; // לפחות אוטובוס אחד יהיה קרוב נסועת הטיפול הבא
            busses[1].LastTreat = DateTime.Now;
            ///
            /// update the status of the bus
            ///
            if (DateTime.Now.Subtract(busses[1].LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
            {
                busses[1].Status = STATUS.INTREATMENT;

            }
            else if (busses[1].KiloFromLastTreat >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
            {
                busses[1].Status = STATUS.INTREATMENT;

            }
            else if (!(busses[1].Fuel < 0))  // if the ride is more then 1200km then the bus can`t do the ride throw exception
            {

                busses[1].Status = STATUS.READYFORRIDE;
            }
            Bus nBus = new Bus();
            nBus.Kilometrage = rand.Next();
            nBus.KiloFromLastTreat = nBus.Kilometrage;
            nBus.StartPeilut = new DateTime(2019, 1, 3);  //לפחות אוטובוס אחד יהיה לאחר תאריך טיפול הבא
            nBus.LastTreat = new DateTime(2019, 2, 3);
            rand = new Random();
            int rishuyN = rand.Next(10000000, 100000000);
            nBus.LicenseNum = rishuyN;
            ///<summary>
            /// update the status of the new bus
            ///</summary>
            if (DateTime.Now.Subtract(nBus.LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
            {
                nBus.Status = STATUS.INTREATMENT;

            }
            else if (nBus.KiloFromLastTreat >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
            {
                nBus.Status = STATUS.INTREATMENT;

            }
            else if (!(nBus.Fuel < 0))  // if the ride is more then 1200km then the bus can`t do the ride throw exception
            {

                nBus.Status = STATUS.READYFORRIDE;
            }
            busses.Add(nBus);
            busses[3].StartPeilut = new DateTime(2021, 1, 3);
            busses[3].Fuel = 1;  // לפחות אוטובוס אחד יהיה עם מעט דלק
            busses[3].LastTreat = DateTime.Now;
            ///<summary>
            /// update the status of the bus
            ///</summary>
            if (DateTime.Now.Subtract(busses[3].LastTreat).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
            {
                busses[3].Status = STATUS.INTREATMENT;
            }
            else if (busses[3].KiloFromLastTreat >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
            {
                busses[3].Status = STATUS.INTREATMENT;
            }
            else if (!(busses[3].Fuel < 0))  // if the ride is more then 1200km then the bus can`t do the ride throw exception
            {
                busses[3].Status = STATUS.READYFORRIDE;
            }

            ///<summary>
            /// create 40 station and enter them to the station list
            ///</summary>
            Station newstation = new Station();
            newstation.Code = 38831;                               //1
            newstation.Location.Longitude = 34.917806;
            newstation.Location.Latitude = 32.183921;
            newstation.Name = "בי''ס בר לב/בן יהודה";
            newstation.Address = "רחוב:בן יהודה 76 עיר: כפר סבא ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38832;                               //2
            newstation.Location.Longitude = 34.819541;
            newstation.Location.Latitude = 31.870034;
            newstation.Name = "הרצל/צומת בילו";
            newstation.Address = " רחוב:הרצל  עיר: קרית עקרון : ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38833;                               //3
            newstation.Location.Longitude = 34.782828;
            newstation.Location.Latitude = 31.984553;
            newstation.Name = "הנחשול/הדייגים";
            newstation.Address = "  רחוב:הנחשול 30 עיר: ראשון לציון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38834;                               //4
            newstation.Location.Longitude = 34.790904;
            newstation.Location.Latitude = 31.88855;
            newstation.Name = "פריד/ששת הימים";
            newstation.Address = "  רחוב:משה פריד 9 עיר: רחובות ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38836;                               //5
            newstation.Location.Longitude = 34.898098;
            newstation.Location.Latitude = 31.956392;
            newstation.Name = "ת. מרכזית לוד/הורדה";
            newstation.Address = " רחוב:  עיר: לוד ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38837;                               //6
            newstation.Location.Longitude = 34.796071;
            newstation.Location.Latitude = 31.892166;
            newstation.Name = "חנה אברך/וולקני";
            newstation.Address = "  רחוב:חנה אברך 9 עיר: רחובות ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38838;                               //7
            newstation.Location.Longitude = 34.824106;
            newstation.Location.Latitude = 31.956392;
            newstation.Name = "הרצל/משה שרת";
            newstation.Address = " רחוב:הרצל 20 עיר: קרית עקרון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38839;                               //8
            newstation.Location.Longitude = 34.821857;
            newstation.Location.Latitude = 31.862305;
            newstation.Name = "הבנים/אלי כהן";
            newstation.Address = " רחוב:הבנים 4 עיר: קרית עקרון  ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38840;                               //9
            newstation.Location.Longitude = 34.822237;
            newstation.Location.Latitude = 31.865085;
            newstation.Address = " רחוב:וייצמן 11 עיר: קרית עקרון ";
            newstation.Name = " ויצמן/הבנים ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38841;                               //10
            newstation.Location.Longitude = 34.818957;
            newstation.Location.Latitude = 31.865222;
            newstation.Name = "האירוס/הכלנית";
            newstation.Address = " רחוב:האירוס 13 עיר: קרית עקרון";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38842;                               //11
            newstation.Location.Longitude = 34.818392;
            newstation.Location.Latitude = 31.867597;
            newstation.Name = "הכלנית/הנרקיס";
            newstation.Address = " רחוב:הכלנית  עיר: קרית עקרון";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38844;                               //12
            newstation.Location.Longitude = 34.827023;
            newstation.Location.Latitude = 31.86244;
            newstation.Name = "אלי כהן/לוחמי הגטאות";
            newstation.Address = " רחוב:אלי כהן 62 עיר: קרית עקרון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38845;                               //13
            newstation.Location.Longitude = 34.828702;
            newstation.Location.Latitude = 31.863501;
            newstation.Name = "שבזי/שבת אחים";
            newstation.Address = " רחוב:שבזי 51 עיר: קרית עקרון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38846;                               //14
            newstation.Location.Longitude = 34.827102;
            newstation.Location.Latitude = 31.865348;
            newstation.Name = "שבזי/ויצמן";
            newstation.Address = " רחוב:שבזי 31 עיר: קרית עקרון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38847;                               //15
            newstation.Location.Longitude = 34.763896;
            newstation.Location.Latitude = 31.977409;
            newstation.Name = "חיים בר לב/שדרות יצחק רבין";
            newstation.Address = " רחוב:חיים ברלב  עיר: ראשון לציון";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38848;                               //16
            newstation.Location.Longitude = 34.912708;
            newstation.Location.Latitude = 32.300345;
            newstation.Name = "מרכז לבריאות הנפש לב השרון";
            newstation.Address = " רחוב:  עיר: צור משה";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38852;                               //17
            newstation.Location.Longitude = 34.807944;
            newstation.Location.Latitude = 31.914255;
            newstation.Name = "הולצמן/המדע";
            newstation.Address = " רחוב:חיים הולצמן 2 עיר: רחובות  ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38854;                               //18
            newstation.Location.Longitude = 34.836363;
            newstation.Location.Latitude = 31.963668;
            newstation.Name = "מחנה צריפין/מועדון";
            newstation.Address = " רחוב:  עיר: צריפין ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38855;                               //19
            newstation.Location.Longitude = 34.825249;
            newstation.Location.Latitude = 31.856115;
            newstation.Address = "רחוב:הרצל 4 עיר: קרית עקרון ";
            newstation.Name = "הרצל/גולני ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38856;                               //20
            newstation.Location.Longitude = 34.81249;
            newstation.Location.Latitude = 31.874963;
            newstation.Name = "הרותם/הדגניות";
            newstation.Address = " רחוב:הרותם 3 עיר: רחובות ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38859;                               //21
            newstation.Location.Longitude = 34.910842;
            newstation.Location.Latitude = 32.300035;
            newstation.Name = "הערבה";
            newstation.Address = " רחוב:הערבה  עיר: צור משה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38860;                               //22
            newstation.Location.Longitude = 34.948647;
            newstation.Location.Latitude = 32.305234;
            newstation.Name = "מבוא הגפן/מורד התאנה";
            newstation.Address = " רחוב:מבוא הגפן  עיר: תל אביב ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38861;                               //23
            newstation.Location.Longitude = 34.943393;
            newstation.Location.Latitude = 32.304022;
            newstation.Name = "מבוא הגפן/ההרחבה";
            newstation.Address = "  רחוב:מבוא הגפן  עיר:תל אביב  ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38865;                               //24
            newstation.Location.Longitude = 34.8976;
            newstation.Location.Latitude = 31.990876;
            newstation.Address = " רחוב:שדרות העליה  עיר: נמל תעופה בן גוריון ";
            newstation.Name = " רשות שדות התעופה/העליה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38866;                               //25
            newstation.Location.Longitude = 34.879725;
            newstation.Location.Latitude = 31.998767;
            newstation.Name = "כנף/ברוש";
            newstation.Address = " רחוב:כנף  עיר: נמל תעופה בן גוריון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38867;                               //26
            newstation.Location.Longitude = 34.818708;
            newstation.Location.Latitude = 31.883019;
            newstation.Name = "החבורה/דב הוז";
            newstation.Address = " רחוב:החבורה 24 עיר: רחובות ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38877;                               //27
            newstation.Location.Longitude = 34.904907;
            newstation.Location.Latitude = 32.076535;
            newstation.Name = "דרך מנחם בגין/יעקב חזן";
            newstation.Address = " רחוב:דרך מנחם בגין 30 עיר: פתח תקווה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38878;                               //28
            newstation.Location.Longitude = 34.878765;
            newstation.Location.Latitude = 32.299994;
            newstation.Name = "דרך הפארק/הרב נריה";
            newstation.Address = " רחוב:דרך הפארק 20 עיר: נתניה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38881;                               //29
            newstation.Location.Longitude = 34.784347;
            newstation.Location.Latitude = 31.809325;
            newstation.Name = "דרך הפרחים/יסמין";
            newstation.Address = " רחוב:דרך הפרחים 46 עיר: גדרה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38883;                               //30
            newstation.Location.Longitude = 34.778239;
            newstation.Location.Latitude = 31.80037;
            newstation.Name = "יצחק רבין/פנחס ספיר";
            newstation.Address = " רחוב:דרך יצחק רבין  עיר: גדרה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38884;                               //31
            newstation.Location.Longitude = 34.782985;
            newstation.Location.Latitude = 31.799224;
            newstation.Address = " רחוב:שדרות מנחם בגין 4 עיר: גדרה";
            newstation.Name = " מנחם בגין/יצחק רבין ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38885;                               //32
            newstation.Location.Longitude = 34.785069;
            newstation.Location.Latitude = 31.800334;
            newstation.Name = "חיים הרצוג/דולב";
            newstation.Address = " רחוב:חיים הרצוג 12 עיר: גדרה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38904;                               //33
            newstation.Location.Longitude = 34.813355;
            newstation.Location.Latitude = 31.975479;
            newstation.Name = "יעקב פריימן/בנימין שמוטקין";
            newstation.Address = " רחוב:יעקב פריימן 9 עיר: ראשון לציון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38905;                               //34
            newstation.Location.Longitude = 34.789445;
            newstation.Location.Latitude = 31.982177;
            newstation.Name = "אנילביץ'/שלום אש";
            newstation.Address = " רחוב:אנילביץ' 13 עיר: ראשון לציון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38906;                               //35
            newstation.Location.Longitude = 34.822422;
            newstation.Location.Latitude = 31.948552;
            newstation.Name = "נחמיה/בית העלמין";
            newstation.Address = " רחוב:נחמיה 71 עיר: ראשון לציון";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38907;                               //36
            newstation.Location.Longitude = 34.816339;
            newstation.Location.Latitude = 31.967732;
            newstation.Name = "יהודה הלוי/יוחנן הסנדלר";
            newstation.Address = "  רחוב:יהודה הלוי 89 עיר: ראשון לציון ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 38908;                               //37
            newstation.Location.Longitude = 34.824617;
            newstation.Location.Latitude = 31.893823;
            newstation.Name = "ההגנה/חי''ש";
            newstation.Address = " רחוב:ההגנה 37 עיר: רחובות ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 39039;                               //38
            newstation.Location.Longitude = 34.884936;
            newstation.Location.Latitude = 31.931068;
            newstation.Name = "גן חק''ל/רבאט";
            newstation.Address = " רחוב:רבאט  עיר: רמלה ";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 39041;                               //39
            newstation.Location.Longitude = 34.887207;
            newstation.Location.Latitude = 31.933379;
            newstation.Name = "קניון צ. רמלה לוד/דוכיפת";
            newstation.Address = "  רחוב:דוכיפת  עיר: רמלה";
            stations.Add(newstation);
            newstation = new Station();
            newstation.Code = 39042;                               //40
            newstation.Location.Longitude = 34.880069;
            newstation.Location.Latitude = 31.929318;
            newstation.Name = "היצירה/התקווה";
            newstation.Address = " רחוב:היצירה 2 עיר: רמלה ";
            stations.Add(newstation);

            ///<summary>
            ///create 10 lines and enter them to the line list 
            ///</summary>
            Line newline = new Line();
            newline.Id = RunningNum;    //1
            newline.Code = rand.Next(1, 1000);
            newline.Area = AREAS.SOUTH;
            newline.FirstStation = 38838;
             newline.LastStation = 38846;
            lines.Add(newline);
            newline = new Line();
            ++RunningNum;
            newline.Id = RunningNum;    //2
                newline.Code = rand.Next(1, 1000);
            newline.Area = AREAS.GENERAL;
                newline.FirstStation = 38834;
                newline.LastStation = 38837;
                lines.Add(newline);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //3
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.CENTER;
                newline.FirstStation = 38861;
                newline.LastStation = 38866;
                lines.Add(newline);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //4
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.CENTER;
                newline.FirstStation = 38836;
                newline.LastStation = 38866;
                lines.Add(newline);
                ++RunningNum;
            Line newline1 = new Line();
            newline1.Id = RunningNum;    //5
                newline1.Code = rand.Next(1, 1000);
                newline1.Area = AREAS.SOUTH;
                newline1.FirstStation = 38881;
                newline1.LastStation = 38885;
                lines.Add(newline1);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //6
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.CENTER;
                newline.FirstStation = 38904;
                newline.LastStation = 38907;
                lines.Add(newline);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //7
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.GENERAL;
                newline.FirstStation = 39039;
                newline.LastStation = 39042;
                lines.Add(newline);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //8
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.GENERAL;
                newline.FirstStation = 38834;
                newline.LastStation = 38908;
                lines.Add(newline);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //9
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.CENTER;
                newline.FirstStation = 38861;
                newline.LastStation = 38885;
                lines.Add(newline);
                ++RunningNum;
            newline = new Line();
            newline.Id = RunningNum;    //10
                newline.Code = rand.Next(1, 1000);
                newline.Area = AREAS.GENERAL;
                newline.FirstStation = 38831;
                newline.LastStation = 38906;
                lines.Add(newline);
               

            ///<summary>
            ///create for the 10 lines track
            ///</summary>
            LineStation newLS = new LineStation();
            newLS.LineId = Lines[0].Id;                   // for  1 line  in the line list
            newLS.Station = 38838;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 38838;
            newLS.NextStation = 38839;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0) ;
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38839;
            newLS.LineStationIndex = 2;
            newLS.PrevStation = 38838;
            newLS.NextStation = 38840;
            var lat = stations.Find(x => x.Code == newLS.Station).Location.Latitude;
            var lon = stations.Find(x => x.Code == newLS.Station).Location.Longitude;
            GeoCoordinate cord1 = new GeoCoordinate(lat, lon);
            GeoCoordinate cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            double distance = cord1.GetDistanceTo(cord2) * 0.1;
            int dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38840;
            newLS.LineStationIndex = 3;
            newLS.PrevStation = 38839;
            newLS.NextStation = 38841;
            lat = stations.Find(x => x.Code == newLS.Station).Location.Latitude;
             lon = stations.Find(x => x.Code == newLS.Station).Location.Longitude;
            cord1 = new GeoCoordinate(lat, lon);
             cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
             distance = cord1.GetDistanceTo(cord2) * 0.1;
             dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38841;
            newLS.LineStationIndex =4;
            newLS.PrevStation = 38840;
            newLS.NextStation = 38842;
            lat = stations.Find(x => x.Code == newLS.Station).Location.Latitude;
            lon = stations.Find(x => x.Code == newLS.Station).Location.Longitude;
            cord1 =  new GeoCoordinate(lat, lon);
             cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
             distance = cord1.GetDistanceTo(cord2)*0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38842;
            newLS.LineStationIndex = 5;
            newLS.PrevStation = 38841;
            newLS.NextStation = 38844;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
             dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38844;
            newLS.LineStationIndex = 6;
            newLS.PrevStation = 38842;
            newLS.NextStation = 38845;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
             dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38845;
            newLS.LineStationIndex = 7;
            newLS.PrevStation = 38844;
            newLS.NextStation = 38846;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[0].Id;
            newLS.Station = 38846;
            newLS.LineStationIndex = 8;
            newLS.PrevStation = 38845;
            newLS.NextStation = 38846;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
             dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[1].Id;                   // for line 2 in the line list
            newLS.Station = 38834;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 38834;
            newLS.NextStation = 38836;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[1].Id;                  
            newLS.Station = 38836;
            newLS.LineStationIndex =2 ;
            newLS.PrevStation =38834;
            newLS.NextStation = 38852; 
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[1].Id;
            newLS.Station =38852 ;
            newLS.LineStationIndex =3 ;
            newLS.PrevStation =38836;
            newLS.NextStation = 38856;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[1].Id;
            newLS.Station = 38856;
            newLS.LineStationIndex = 4;
            newLS.PrevStation =38852;
            newLS.NextStation =38867 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[1].Id;
            newLS.Station = 38867;
            newLS.LineStationIndex = 5;
            newLS.PrevStation = 38856;
            newLS.NextStation = 38837;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[1].Id;
            newLS.Station = 38837;
            newLS.LineStationIndex =6 ;
            newLS.PrevStation =38867;
            newLS.NextStation = 38837;
          cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
             dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[2].Id;                   // for line 3 in the line list
            newLS.Station = 38861;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 38861;
            newLS.NextStation = 38860;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[2].Id;                   
            newLS.Station = 38860;
            newLS.LineStationIndex =2;
            newLS.PrevStation =38861 ;
            newLS.NextStation =38865 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[2].Id;
            newLS.Station = 38865;
            newLS.LineStationIndex =3;
            newLS.PrevStation = 38860;
            newLS.NextStation =38866 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[2].Id;
            newLS.Station = 38866;
            newLS.LineStationIndex =4;
            newLS.PrevStation = 38865;
            newLS.NextStation =38866 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
             newLS.LineId = Lines[3].Id;       // for line 4 in the line list
            newLS.Station = 38836;
             newLS.LineStationIndex =1;
             newLS.PrevStation =38836 ;
             newLS.NextStation = 38861;
             newLS.DistanceFromTheLastStat = 0;
             newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
             lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[3].Id;
            newLS.Station = 38861;
            newLS.LineStationIndex = 2;
            newLS.PrevStation = 38836;
            newLS.NextStation = 38866;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[3].Id;
            newLS.Station =38866 ;
            newLS.LineStationIndex = 3;
            newLS.PrevStation = 38861;
            newLS.NextStation =38866 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[4].Id;           // for line 5 in the line list
            newLS.Station =38881 ;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 38881;
            newLS.NextStation = 38883;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[4].Id;      
            newLS.Station = 38883;
            newLS.LineStationIndex = 2;
            newLS.PrevStation = 38881;
            newLS.NextStation =38884 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[4].Id;
            newLS.Station =38884 ;
            newLS.LineStationIndex = 3;
            newLS.PrevStation = 38883;
            newLS.NextStation =38885 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[4].Id;
            newLS.Station = 38885;
            newLS.LineStationIndex = 4;
            newLS.PrevStation = 38884;
            newLS.NextStation = 38885;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[5].Id;              // for line 6 in the line list
            newLS.Station = 38904 ;
            newLS.LineStationIndex = 1;
            newLS.PrevStation =38904 ;
            newLS.NextStation =38905 ;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[5].Id;              
            newLS.Station = 38905;
            newLS.LineStationIndex = 2;
            newLS.PrevStation = 38904;
            newLS.NextStation = 38906 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[5].Id;             
            newLS.Station = 38906;
            newLS.LineStationIndex =3 ;
            newLS.PrevStation = 38905;
            newLS.NextStation = 38907;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[5].Id;              
            newLS.Station = 38907;
            newLS.LineStationIndex = 4;
            newLS.PrevStation = 38906;
            newLS.NextStation =38907 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[6].Id;                  // for line 7 in the line list          
            newLS.Station =39039 ;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 39039;
            newLS.NextStation = 39041;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[6].Id;             
            newLS.Station = 39041;
            newLS.LineStationIndex = 2;
            newLS.PrevStation = 39039;
            newLS.NextStation =39042 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[6].Id;              
            newLS.Station = 39042;
            newLS.LineStationIndex = 3;
            newLS.PrevStation =39041 ;
            newLS.NextStation = 39042;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[7].Id;                  // for line 8 in the line list          
            newLS.Station = 38834;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 38834;
            newLS.NextStation =38837 ;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[7].Id;
            newLS.Station =38837 ;
            newLS.LineStationIndex =2;
            newLS.PrevStation = 38834;
            newLS.NextStation =38852 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[7].Id;
            newLS.Station =38852 ;
            newLS.LineStationIndex =3;
            newLS.PrevStation = 38837;
            newLS.NextStation = 38876;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[7].Id;
            newLS.Station =38867 ;
            newLS.LineStationIndex =4;
            newLS.PrevStation =38852 ;
            newLS.NextStation =38908 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[7].Id;
            newLS.Station = 38908;
            newLS.LineStationIndex =5;
            newLS.PrevStation = 38867;
            newLS.NextStation = 38908;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[8].Id;   // for line 9 in the line list  
            newLS.Station = 38861;
            newLS.LineStationIndex =1;
            newLS.PrevStation = 38861;
            newLS.NextStation = 38854;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[8].Id;
            newLS.Station = 38854;
            newLS.LineStationIndex =2;
            newLS.PrevStation = 38861;
            newLS.NextStation = 38883;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[8].Id;
            newLS.Station = 38883;
            newLS.LineStationIndex =3;
            newLS.PrevStation = 38854;
            newLS.NextStation = 38884;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[8].Id;
            newLS.Station = 38884;
            newLS.LineStationIndex = 4;
            newLS.PrevStation =38883 ;
            newLS.NextStation =38885 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[8].Id;
            newLS.Station = 38885;
            newLS.LineStationIndex = 5;
            newLS.PrevStation =38884 ;
            newLS.NextStation =38885 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[9].Id;   // for line 10 in the line list  
            newLS.Station = 38831;
            newLS.LineStationIndex = 1;
            newLS.PrevStation = 38831;
            newLS.NextStation =38904 ;
            newLS.DistanceFromTheLastStat = 0;
            newLS.TravelTimeFromTheLastStation = new TimeSpan(0);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[9].Id;
            newLS.Station = 38904;
            newLS.LineStationIndex = 2;
            newLS.PrevStation = 38831;
            newLS.NextStation = 38905 ;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[9].Id;
            newLS.Station = 38905;
            newLS.LineStationIndex = 3;
            newLS.PrevStation = 38904;
            newLS.NextStation = 38906;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);
            newLS = new LineStation();
            newLS.LineId = Lines[9].Id;
            newLS.Station = 38906;
            newLS.LineStationIndex = 4;
            newLS.PrevStation = 38905;
            newLS.NextStation = 38906;
            cord1 = new GeoCoordinate(stations.Find(x => x.Code == newLS.Station).Location.Latitude, stations.Find(x => x.Code == newLS.Station).Location.Longitude);
            cord2 = new GeoCoordinate(stations.Find(x => x.Code == newLS.PrevStation).Location.Latitude, stations.Find(x => x.Code == newLS.PrevStation).Location.Longitude);
            distance = cord1.GetDistanceTo(cord2) * 0.1;
            dist = (int)distance;
            newLS.DistanceFromTheLastStat = dist;
            newLS.TravelTimeFromTheLastStation = TimeSpan.FromMinutes(distance);
            lineStations.Add(newLS);

        }

    }
}
