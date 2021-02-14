using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        private int lineId;
        private int lineNumber;
        private int firstStation;
        private int currentStation;
        private string lastStation;
        private TimeSpan timeToArrive = new TimeSpan();
        private TimeSpan arrivalTime = new TimeSpan();
        private TimeSpan startTime = new TimeSpan();


        public int LineId
        {
            get { return lineId; }
            set { lineId = value; }
        }
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }
        public int FirstStation
        {
            get { return firstStation; }
            set { firstStation = value; }
        }
        public int CurrentStation
        {
            get { return currentStation; }
            set { currentStation = value; }
        }
        public string LastStation
        {
            get { return lastStation; }
            set { lastStation = value; }
        }
        public TimeSpan TimeToArrive
        {
            get
            {
                return timeToArrive;
            }
            set
            {
                timeToArrive = value;
            }
        }
        public TimeSpan ArrivalTime
        {
            get
            {
                return arrivalTime;
            }
            set

            {
                arrivalTime = value;
            }
        }

        public TimeSpan StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }
    }
}
