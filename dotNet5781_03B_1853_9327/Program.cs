/*using dotNet5781_03B_1853_9327;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_1853_9327
{
    class Program
    {
        
        public static DateTime randomDate()
        {
            Random randomDateNum = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(randomDateNum.Next(range));
        }
        static void Main(string[] args)
        {

Random rand = new Random();
            List<Bus> buses = new List<Bus>();
            for (int i=0; i<9; i++)
            {            
                Bus newBus = new Bus();
                newBus.Kilometrage = rand.Next();
                newBus.l_treatment.kilFromTreat = newBus.Kilometrage;
               
                newBus.StartPeilut = randomDate();
                if (newBus.StartPeilut.Year<2018)
                {

                     rand = new Random();
                   string rishuyNum =  rand.Next(1000000, 10000000).ToString();
                   string rishuy;
                   rishuy = rishuyNum[0].ToString() + rishuyNum[1].ToString() + "-" + rishuyNum[2].ToString() + rishuyNum[3].ToString() + rishuyNum[4].ToString() + "-" + rishuyNum[5].ToString()+ rishuyNum[6].ToString();

                    
                }
                else
                {
                    rand = new Random();
                    string rishuyNum = rand.Next(10000000, 100000000).ToString();
                    newBus.Rishuy = rishuyNum[0].ToString() + rishuyNum[1].ToString() + rishuyNum[2].ToString() + "-" + rishuyNum[3].ToString() + rishuyNum[4].ToString() + "-" + rishuyNum[5].ToString() + rishuyNum[6].ToString()+ rishuyNum[7].ToString();

                }
                Random randomDateNum = new Random();
                DateTime start = newBus.StartPeilut;
                int range = (DateTime.Today - start).Days;
                newBus.l_treatment.date =  start.AddDays(randomDateNum.Next(range));
                newBus.l_treatment.kilFromTreat = rand.Next();
                newBus.Bedika(0);
                buses.Add(newBus);

            }
            buses[1].l_treatment.kilFromTreat = 19000; // לפחות אוטובוס אחד יהיה קרוב נסועת הטיפול הבא
           Bus nBus = new Bus();
            nBus.Kilometrage = rand.Next();
            nBus.l_treatment.kilFromTreat = nBus.Kilometrage;

            nBus.StartPeilut = new DateTime(2019, 1, 3);
            nBus.l_treatment.date = new DateTime(2019, 2, 3);
            rand = new Random();
            string rishuyN = rand.Next(10000000, 100000000).ToString();
            nBus.Rishuy = rishuyN[0].ToString() + rishuyN[1].ToString() + rishuyN[2].ToString() + "-" + rishuyN[3].ToString() + rishuyN[4].ToString() + "-" + rishuyN[5].ToString() + rishuyN[6].ToString() + rishuyN[7].ToString();
            buses.Add(nBus);
            buses[3].delek = 1;











            /*
           Random r = new Random(DateTime.Now.Millisecond);

            bool exitFlag = false;
            List<Bus> chevra = new List<Bus>();
            do
            {
                Console.WriteLine(@" Make your choice:
                     
EXIT -> 0 ,ADD -> 1, FIND -> 2, print all the link buses-> 3, commit tidluk or treatment-> 4") ;
                int option = Int32.Parse(Console.ReadLine());
                
                switch (option)
                {
                    //TODO
                    case 1: //add a bus 
                        Bus newBus = new Bus();
                        try
                        {
                            Console.WriteLine("please enter peliut date like this : Jan 1, 2009 ");
                            newBus.StartPeilut = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("please enter rishuy number");
                            newBus.Rishuy = Console.ReadLine();
                            
                        }   catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        };
                        chevra.Add(newBus); // the new bus is valid, add it to the list
                        break;
                    case 2:  // to chek if the user can make the ride 
                        Console.WriteLine("please enter rishuy number");
                        string requestBus = Console.ReadLine();
                        var chosenBus = chevra.Find(bus => bus.Rishuy == requestBus);
                            if (chosenBus == null) // check if the bus exist on the list 
                        {
                            Console.WriteLine("There is no such bus!");
                            break;
                        }
                       int random = r.Next(1, 1200);
                        Console.WriteLine(random);
                        try
                        {
                            chosenBus.Bedika(random); // check if the bus can do the ride 
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                        break;

                    case 3: //print all the link buses
                        foreach (var bus in chevra)
                        {
                            Console.WriteLine(bus);
                        }
                        break;

                    case 4: //commit tidluk or treatment
                        Console.WriteLine("please enter rishuy number");
                        requestBus = Console.ReadLine();
                         chosenBus = chevra.Find(bus => bus.Rishuy == requestBus);
                        if (chosenBus == null)// check if the bus exist on the list
                        {
                            Console.WriteLine("There is no such bus!");
                            break;
                        }
                        Console.WriteLine("please enter 1 for tidluk or 2 for treatment");
                        int input = Int32.Parse(Console.ReadLine());
                        if (input==1)
                        {
                            chosenBus.tidluk();
                        }
                        else if (input == 2)
                        {
                            chosenBus.treatment();
                        }
                        break;
                    case 0:
                        exitFlag = true;
                        break;
                    default: // if the user press a wrong number 
                        Console.WriteLine("you press a wrong number ");
             
                        break;
                }
            } while (!exitFlag);
              Console.ReadKe
        }

    }
}
*/