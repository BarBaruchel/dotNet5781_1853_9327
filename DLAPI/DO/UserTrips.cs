using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserTrips
    {
        public int TripId { get; set; }
        public string Username { get; set; }
        public int BusCode { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public TimeSpan InAt { get; set; }
        public TimeSpan OutAt { get; set; }
    }
}
