using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{


    public class AlreadyExistsException : Exception
    {
        public int ID;
        public int CODE = 0;

        public AlreadyExistsException(string message, int id) : base(message) => ID = id;

        public AlreadyExistsException(string message, int id, int code) : base(message)
        {
            ID = id; CODE = code;
        }

        public override string ToString()
        {
            if (CODE != 0)
                return base.ToString() + "bad id :  " + ID + "bad code : " + CODE;
            return base.ToString() + "bad id :  " + ID;


        }
    }

    public class NotExistException : Exception
    {
        public int ID;
        public int CODE = 0;

        public NotExistException(string message, int id) : base(message) => ID = id;
        public NotExistException(string message, int id, int code) : base(message)
        {
            ID = id;
            CODE = code;
        }

        public override string ToString()
        {
            if (CODE == 0)
            {
                return base.ToString() + "bad id :  " + ID;
            }
            return base.ToString() + "bad id :  " + ID + "bad code :  " + CODE;



        }


    }
    public class NotExistStationException : Exception
    {

        public int CODE;

        public NotExistStationException(string message, int code) : base(message) => CODE = code;



        public override string ToString()
        {

            return base.ToString() + "bad code : " + CODE;



        }


    }
}
