using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781__02_1853_9327
{
    class Program
    {
        static void Main(string[] args)
        {
            BusCompany company = new BusCompany();
            int key = 100000;
            Random r = new Random();

            List<BusStationLine> sharedStations = new List<BusStationLine>();
            for (int i = 0; i < 10; i++)
            {
                double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                double distance = r.NextDouble() * (10 - 1) + 1;
                TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);
                sharedStations.Add(newStation);
            }

            for (int i = 1; i <= 10; i++)
            {

                BusLine newBusLine = new BusLine();
                newBusLine.Number = r.Next(1, 1000);
                int randomNumber = r.Next(0, 4);
                switch (randomNumber)
                {
                    case 0:
                        newBusLine.Area = Area.GENERAL;
                        break;
                    case 1:
                        newBusLine.Area = Area.SOUTH;
                        break;
                    case 2:
                        newBusLine.Area = Area.CENTER;
                        break;
                    case 3:
                        newBusLine.Area = Area.JERUSALEM;
                        break;
                }

                for (int j = 1; j <= 4; j++)
                {
                    double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                    double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                    double distance = r.NextDouble() * (10 - 1) + 1;
                    TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                    BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);

                    if (j == 1)
                    {
                        newBusLine.AddFirstStation(newStation);
                    }
                    else
                    {
                        newBusLine.AddLastStation(newStation);
                    }
                }
                for (int k = 0; k < 10; k++)
                {
                    newBusLine.AddLastStation(sharedStations[k]);
                }
                Console.WriteLine(newBusLine);
                company.Add(newBusLine);
            }


        }
    }
}
