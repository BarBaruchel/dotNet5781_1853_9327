using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        public int Code { get; set; }
        public GeoCoordinate Location { get; set; } = new GeoCoordinate();
        public string Address { get; set; }
        public string Name { get; set; }
    }
}

