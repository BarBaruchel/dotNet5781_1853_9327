using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AlreadyExistsException : Exception
    {
        public int ID;
        public int CODE = 0;

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
            ID = ((DO.AlreadyExistsException)innerException).ID; //we did convert to innerException, therefore we have accsses to field 
            CODE = ((DO.AlreadyExistsException)innerException).CODE;
        }


        public override string ToString()
        {
            if (CODE != 0)
                return base.ToString() + "The id :  " + ID + "The code : " + CODE;
            return base.ToString() + "The id :  " + ID;


        }
    }
    public class TooMuchFuelException : Exception
    {
        public int FUEL;
        public TooMuchFuelException(string message, int fuel) : base(message) => FUEL = fuel;
        public override string ToString()
        {

            return base.ToString() + "The id :  " + FUEL;


        }
    }
    public class WrongInputException : Exception
    {
        public DateTime DATE;
        public int lastStation = 0;
        public WrongInputException(string message, DateTime date) : base(message) => DATE = date;
        public WrongInputException(string message, int ls) : base(message) => lastStation = ls;

        public override string ToString()
        {
            if (lastStation == 0)
                return base.ToString() + "The start peilut date is :  " + DATE;
            return base.ToString() + "The last station is: " + lastStation;

        }

    }
    public class NotExistException : Exception
    {
        public int ID;


        public NotExistException(string message, Exception innerException) : base(message, innerException)
        {
            ID = ((DO.NotExistException)innerException).ID; //we did convert to innerException, therefore we have accsses to field 

        }


        public override string ToString()
        {
            return base.ToString() + "The id :  " + ID;
        }
    }
    public class NotExistStationException : Exception
    {
        public int CODE;


        public NotExistStationException(string message, Exception innerException) : base(message, innerException)
        {
            CODE = ((DO.NotExistStationException)innerException).CODE; //we did convert to innerException, therefore we have accsses to field 

        }


        public override string ToString()
        {
            return base.ToString() + "The code :  " + CODE;
        }
    }

}
