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
        private int currentStation;  //the current station that we press on in the window  
        private string lastStation;
        private TimeSpan timeToArrive = new TimeSpan();  // How much longer will we have to wait for the bus to arrive
        private TimeSpan arrivalTime = new TimeSpan(); //What time will the bus arrive?
        private TimeSpan startTime = new TimeSpan();  //when the bus start to drive from the first station

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
