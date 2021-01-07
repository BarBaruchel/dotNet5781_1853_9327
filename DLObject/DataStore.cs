using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    internal static class DataStore
    {
        private static List<Bus> busses = new List<Bus>();
        private static List<Station> myStations = new List<Station>();
        private static List<User> users = new List<User>();


        public static List<Station> Stations { get => myStations; }
        public static List<Bus> Busses { get => busses; }
        public static List<User> Users { get => users; }
     
         public static User admin;
        private static Random rand = new Random();

        ////static DataStore()
        ////{
        ////    Init();
        ////    InitRepostory();
        ////}
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

            ///
            /// update the status of the new bus
            ///
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
            ///
            /// update the status of the bus
            ///
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
        }





        private static List<Line> myLine = new List<Line>();
        public static List<Line> Line { get; }

        private static List<LineStation> myLineStation = new List<LineStation>();
        public static List<LineStation> LineStation { get; }



    }
}
