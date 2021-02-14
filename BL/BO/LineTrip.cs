using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class LineTrip
    {
        public int Id;

        public int LineId;

        public TimeSpan StartAt = new TimeSpan();

        public TimeSpan FinishAt = new TimeSpan();

        public TimeSpan Frequency = new TimeSpan();
    }
}
