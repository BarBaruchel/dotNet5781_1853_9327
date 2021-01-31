using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime StartPeilut { get; set; }
        public DateTime LastTreat { get; set; }
        public int KiloFromLastTreat { get; set; }
        public int Kilometrage { get; set; }
        public int Fuel { get; set; }
        public STATUS Status { get; set; }
    }
}
