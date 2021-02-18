using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        public int Id { get; set; }            //ID for the LineTrip
        public int LineId { get; set; }        //Bus line ID
        public TimeSpan StartAt { get; set; }  // exit time
        public TimeSpan FinishAt { get; set; }  //finish time
        public TimeSpan Frequency { get; set; }  //Frequency (if 0 - Single exit) how often the bus exit for the line
    }
}
