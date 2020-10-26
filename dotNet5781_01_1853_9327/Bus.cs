using dotNet5781_01_1853_9327;
using System;

namespace dotNet5781_01_1853_9327
{
    public class Bus
    {
        public int Kilometrage { get; set; } = 0;
        private string rishuy;
        private int delek = 1200;

        public DateTime  Peilut { get; set; }
        public string Rishuy
        {
            get { return rishuy; }
            set {
                rishuy = value;
                    try
                    {
                        if (Peilut.Year < 2018)
                        {
                            if ((Rishuy[2] != '-') || (Rishuy[6] != '-'))
                            {
                            throw new Exception("location of makaf not in place");

                            }
                        }
                    else
                    {
                        if ((Rishuy[3] != '-') || (Rishuy[6] != '-'))
                        {
                            throw new Exception("location of makaf not in place");

                        }
                    }

                    }
                    catch (Exception ex)
                    {
                    rishuy = null;
                        throw new Exception(ex.Message);
                    }
                
            }
        }

        public override string ToString()
        {
            return string.Format("rishuy : {0}, \t peilut: {1}, KM: {2}", Rishuy, Peilut, Kilometrage);
        }
        public void Bedika(int distance)
        {
           
                if (delek - distance <= 0)
                {
                   throw new Exception("no enough fuel!");
                }
             if (Kilometrage+distance>=20000)
            {
                throw new Exception("need a treatment");
            }
             if (DateTime.Now.Subtract(Peilut).TotalDays>=365)
            {
                throw new Exception("need a treatment");
            }
            delek -= distance;
            Kilometrage += distance;
            
        }

        public void tidluk()
        {
            delek = 0;
        }

        public void treatment()
        {
            Peilut = DateTime.Now;
            Kilometrage = 0;
        }
        
    }
   
}


