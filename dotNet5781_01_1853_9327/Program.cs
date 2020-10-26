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

        static void Main(string[] args)
        {
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
                    case 1:
                        Bus newBus = new Bus();
                        try
                        {
                            Console.WriteLine("please enter peliut date like this : Jan 1, 2009 ");
                            newBus.Peilut = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("please enter rishuy number");
                            newBus.Rishuy = Console.ReadLine();
                            
                        }   catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        };
                        chevra.Add(newBus);
                        break;
                    case 2:
                        Console.WriteLine("please enter rishuy number");
                        string requestBus = Console.ReadLine();
                        var chosenBus = chevra.Find(bus => bus.Rishuy == requestBus);
                            if (chosenBus == null)
                        {
                            Console.WriteLine("There is no such bus!");
                            break;
                        }
                       int random = r.Next(1, 1200);
                        Console.WriteLine(random);
                        try
                        {
                            chosenBus.Bedika(random);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }



                        break;
                    case 3:
                        foreach (var bus in chevra)
                        {
                            Console.WriteLine(bus);
                        }
                        break;
                    case 4:
                        Console.WriteLine("please enter rishuy number");
                         requestBus = Console.ReadLine();
                         chosenBus = chevra.Find(bus => bus.Rishuy == requestBus);
                        if (chosenBus == null)
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
                        break;
                    default:
                        Console.WriteLine("you press a wrong number ");
                        //Console.WriteLine(@" Make your choice:
                          //    EXIT -> 0 ,ADD -> 1, FIND -> 2, print all the link buses-> 3, commit tidluk or treatment-> ");
                        exitFlag = true;
                        break;
                }
            } while (!exitFlag);
              Console.ReadKey();
        }

    }
}
