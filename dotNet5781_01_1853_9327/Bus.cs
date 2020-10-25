using System;

namespace dotNet5781_01_1853_9327
{
    public class Bus
    {
        public int Kilometrage { get; set; }
        private string rishuy;

        public DateTime  Kaballa { get; set; }
        public string Rishuy
        {
            get { return rishuy; }
            set { rishuy = value; }
        }

        public override string ToString()
        {
            return string.Format("rishuy : {0}, \t kabala: {1}, KM: {2}",Rishuy,Kaballa,Kilometrage) 
        }

    }
}