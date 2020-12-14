using dotNet5781_01_1853_9327;
using dotNet5781_03B_1853_9327;
using System;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_01_1853_9327
{
    public class Bus : INotifyPropertyChanged

    {
        private int kilometrage = 0;
        public int Kilometrage { get 
            {
                return kilometrage;
            }
            set {
                kilometrage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Kilometrage"));
            }
        } 
        private string rishuy;
     

        private int delek = 1200;
        public int Delek {
            get
            {
                return delek;
            }

            set {
                delek = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Delek"));
            } } 
        public lastTreatment l_treatment { get; } = new lastTreatment();

        private STATUS status;
        public STATUS Status {
            set
            {
                status = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
            get
            {
                return status;
            }
        }
        public class lastTreatment : INotifyPropertyChanged
        {

            private DateTime date;
            public event PropertyChangedEventHandler PropertyChanged;
            public DateTime Date
            {
                set
                {
                    date = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
                get
                {
                    return date;
                }
            }

            private int kilFromTreat;
            public int KilFromTreat
            {
                set
                {
                    kilFromTreat = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("KilFromTreat"));
                }
                get
                {
                    return kilFromTreat;
                }
            }

        
            public lastTreatment() { this.kilFromTreat = 0; }

            public override string ToString()
            {
                return (this.date.ToString() + kilFromTreat.ToString());
            }

        }
        private DateTime startPeilut = new DateTime();

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime StartPeilut{ get { return this.startPeilut.Date; } 
            set
            {
                this.startPeilut = value.Date;
            
                this.l_treatment.Date = startPeilut.Date;
            }
        }


        public string Rishuy
        {
            get { return rishuy; }
            set {
                rishuy = value;
                    try
                    {
                    if (StartPeilut.Year < 2018)
                    {
                        if ((Rishuy[2] != '-') || (Rishuy[6] != '-'))
                        {
                            throw new Exception("location of makaf not in place");

                        }
                        else
                        {
                            if (Rishuy.Length != 9)
                                throw new Exception("not the right length");
                        } 
                    }
                           

                            
                        
                    else
                    {
                        if ((Rishuy[3] != '-') || (Rishuy[6] != '-'))
                        {
                            throw new Exception("location of makaf not in place");

                        }
                        else
                        {
                            if (Rishuy.Length != 10)
                                throw new Exception("not the right length");
                        }
                    }

                    }
                    catch (Exception ex)
                    {
                    rishuy = null;
                        throw new Exception(ex.Message); // throw to the user exception message that the rishuy number is not invalid 
                    }
                
            }
        }

        public override string ToString()  // print all attributes of the bus
        {
            return string.Format("rishuy : {0}, \t peilut: {1}, KM: {2}", Rishuy, l_treatment.Date , Kilometrage);
        }
       
         
        
        
        
        
        public bool Bedika(int distance) //to check if the bus can do the track according to the conditions
        {
            if (DateTime.Now.Subtract(l_treatment.Date).TotalDays >= 365) // if the subtraction between the last time the bus does a treatment,then bus can`t do the ride throw exception
            {
                Status = STATUS.INTREATMENT;
                return false;
            }

            else if (l_treatment.KilFromTreat + distance >= 20000) // if the kilometrage plus the ride is more then 20000km the the bus can`t do the ride throw exception
            {
                Status = STATUS.INTREATMENT;
                return false;
            }

            else if (delek - distance < 0)  // if the ride is more then 1200km then the bus can`t do the ride throw exception
            {
               
                return false;
            }



            Status = STATUS.READYFORRIDE;
            return true;
            
        }
        public void Travel(object dist)
        {
            int distance = (int)dist;
            bool result = Bedika(distance);
            if (result== false)
            {
                if (Status == STATUS.INTREATMENT)
                    return;
                //throw new Exception("The bus needs a treatment!");
                else
                    return;   
                // throw new Exception("The bus needs a tidluk!");
               
            }
            if (Status == STATUS.MIDDLERIDE)
                return;
                //throw new Exception("The bus is in another ride!");
            Random rand = new Random();
            int velocity = rand.Next(20, 51);
            int TimeToTravel = (distance / velocity) * 6; // In seconds
            Status = STATUS.MIDDLERIDE;
            Thread.Sleep(TimeToTravel * 1000);
            Delek -= distance;
            l_treatment.KilFromTreat += distance;
            Kilometrage += distance;
            Status = STATUS.READYFORRIDE;





        }

        public void tidluk()
        {
            Status = STATUS.INTIDLUK;
            Thread.Sleep(12000);
            Delek = 1200;
            Bedika(0);
            
        }

        public void treatment()
        {
            Status = STATUS.INTREATMENT;
            Thread.Sleep(144000);
            l_treatment.Date = DateTime.Now;
            l_treatment.KilFromTreat = 0;
            Status = STATUS.READYFORRIDE;

        }
        
    }
   
}


