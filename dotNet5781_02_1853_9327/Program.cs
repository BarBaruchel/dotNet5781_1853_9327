using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1853_9327
{
    class Program
    {
        static void Main(string[] args)
        {
            BusCompany company = new BusCompany();
            int key = 100000;
            Random r = new Random();

            List<BusStationLine> sharedStations = new List<BusStationLine>();  // list of stations through which several busses pass 
            List<BusStationLine> allStations = new List<BusStationLine>();  // list of all stations that all the busses pass through
            for (int i = 0; i < 10; i++)   // create 10 shared stations
            {
                double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                double distance = r.NextDouble() * (10 - 1) + 1;
                TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);
                sharedStations.Add(newStation);
                allStations.Add(newStation);
            }

            for (int i = 1; i <= 10; i++)  // create 10 busses line
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

                for (int j = 1; j <= 4; j++)  // enter to them 4 new stations
                {
                    double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                    double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                    double distance = r.NextDouble() * (10 - 1) + 1;
                    TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                    BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);

                    if (j == 1)
                    {
                        newBusLine.AddFirstStation(newStation);
                        allStations.Add(newStation);
                    }
                    else
                    {
                        newBusLine.AddLastStation(newStation);
                        allStations.Add(newStation);
                    }
                }
                for (int k = 0; k < 10; k++)  // add to them the 10 shared stations
                {
                    newBusLine.AddLastStation(sharedStations[k]);
                }
               
                company.Add(newBusLine);
            }


            Console.WriteLine(@"Please enter a number:
                                add a bus=>1,  add station for a bus=>2,  delete a bus=>3, delete station from a bus=>4,
                                search buses according to station number=>5, print path from one station to another=>6
                                print all the bus lines=>7,  print all the station and the buses that path throught=8 
                                exist=>9");




            int options = Int32.Parse(Console.ReadLine());
            BusLine bus = new BusLine();
            int num = 0;
            int keySattion;
            List<BusLine> buses = new List<BusLine>();
            while (options != 9)
            {
                switch (options)
                {
                    case 1:
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
                        for (int i = 0; i < 2; i++)
                        {
                            double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                            double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                            double distance = r.NextDouble() * (10 - 1) + 1;
                            TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                            BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);
                            if (i == 0)
                                newBusLine.AddFirstStation(newStation);
                            else
                                newBusLine.AddLastStation(newStation);
                            allStations.Add(newStation);

                        }

                        company.Add(newBusLine);
                        break;

                    case 2:
                        Console.WriteLine("Please enter a bus number ");
                        try
                        {
                            num = Int32.Parse(Console.ReadLine());
                            bus = company.search(num);
                            Console.WriteLine("Enter index of station");
                            int index = Int32.Parse(Console.ReadLine());

                            double longi = r.NextDouble() * (35.5 - 34.3 - 1) + 35.5;

                            double lat = r.NextDouble() * (33.3 - 31 - 1) + 31;

                            double distance = r.NextDouble() * (10 - 1) + 1;
                            TimeSpan travelTime = new TimeSpan((long)(distance * 4));


                            BusStationLine newStation = new BusStationLine(key++, lat, longi, distance, travelTime);
                            bus.AddStation(index, newStation);
                            allStations.Add(newStation);

                        }
                        catch (ArgumentNullException an)
                        {
                            Console.WriteLine(an);
                        }


                        break;


                    case 3:

                        try
                        {


                            Console.WriteLine("Please enter a bus number");
                            num = Int32.Parse(Console.ReadLine());
                            bus = company.search(num);
                            company.DeleteBus(bus);


                        }
                        catch (ArgumentNullException an)
                        {
                            Console.WriteLine(an);
                        }
                        break;

                    case 4:
                        Console.WriteLine("please  enter key of station");
                        keySattion = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Enter number of bus");
                        num = Int32.Parse(Console.ReadLine());
                        try
                        {
                            bus = company.search(num);
                            bool answer = bus.deleteStation(keySattion);
                            if (!answer)
                            {
                                Console.WriteLine("The key of station does not exsit!");
                            }
                        }
                        catch (ArgumentNullException an)
                        {
                            Console.WriteLine(an);
                        }
                        break;

                    case 5:
                        try
                        {
                            Console.WriteLine("please  enter key of station");
                            keySattion = Int32.Parse(Console.ReadLine());
                            buses = company.BusesThatPassInStation(keySattion);
                            for (int i = 0; i < buses.Count; i++)
                            {
                                Console.WriteLine(buses[i].Number);
                            }
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine(ex);
                        }


                        break;

                    case 6:
                        Console.WriteLine("Enter the first station key");
                        int firstKey = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the second station key");
                        int secondKey = Int32.Parse(Console.ReadLine());
                        try
                        {
                            BusStationLine firstStation = company.searchWithKey(firstKey);
                            BusStationLine secondStation = company.searchWithKey(secondKey);

                            var paths = company.busses.Where(x => x.subPath(firstStation, secondStation) != null).OrderBy(x => x.distanceBetween(firstStation, secondStation));
                            if (paths == null)
                                throw new ArgumentNullException("this path does not exist! ");
                            foreach (var path in paths)
                            {
                                List<BusStationLine> pathToGo = path.subPath(firstStation, secondStation).ToList();
                                for (int i=0; i< pathToGo.Count; i++)
                                {
                                    Console.WriteLine(pathToGo[i]);
                                    Console.WriteLine();
                                    Console.WriteLine();
                                }
                               
                            }
                        }
                        catch (ArgumentNullException an)
                        {
                            Console.WriteLine(an);
                        }

                        break;
                    case 7:
                        for (int i = 0; i < company.busses.Count; i++)
                        {

                            Console.WriteLine(company.busses[i]);
                        }

                        break;
                    case 8:
                        for (int i = 0; i < allStations.Count; i++)
                        {
                            Console.Write(allStations[i] + ":  ");
                            for (int k = 0; k < company.busses.Count; k++)
                            {
                                if (company.busses[k].existStations(allStations[i]))
                                    Console.Write(company.busses[k].Number + "    ");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case 9:
                        break;


                };
                 options = Int32.Parse(Console.ReadLine());
            }
            


        }
        
    }
}
