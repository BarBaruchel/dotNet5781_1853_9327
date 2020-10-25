using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_1853_9327
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome1853();
            Welcome9327();
            Console.ReadKey();
        }
        static partial void Welcome9327();
        static void Welcome1853()
        {
            Console.Write("Enter your name: ");
            string name =Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);

        }
    }
}
