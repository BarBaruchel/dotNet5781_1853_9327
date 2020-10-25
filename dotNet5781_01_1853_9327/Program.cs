using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_1853_9327
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus> chevra = new List<Bus>();
            OPTION option;
            do
            {
                Console.WriteLine(@" Make your choice:
                                     ADD, FIND, EXIT = -1"); ;
                string input = Console.ReadLine();
                bool pass = Enum.TryParse(input,out  option);

                switch (option)
                {
                    //TODO
                    case OPTION.ADD:
                        break;
                    case OPTION.FIND:
                        break;
                    case OPTION.EXIT:
                        break;
                    default:
                        break;
                }
            } while (true);
        }
    }
}
