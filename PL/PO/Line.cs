using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Line : INotifyPropertyChanged
    {
        private int code;
        private AREAS area;
        private int firstStation;
        private int lastStation;


        public int Id{ get; set; }
        public int Code
        {
            get => code;
            set
            {
                if (value != code)
                {
                    code = value;
                    RaisePropertyChanged("Code");
                }
            }
        }
        public AREAS Area
        {
            get => area;
            set
            {
                if (value != area)
                {
                    area = value;
                    RaisePropertyChanged("Area");
                }
            }
        }
        public int FirstStation
        {
            get => firstStation;
            set
            {
                if (value != firstStation)
                {
                    firstStation = value;
                    RaisePropertyChanged("FirstStation");
                }
            }
        }
        public int LastStation
        {
            get => lastStation;
            set
            {
                if (value != lastStation)
                {
                    firstStation = value;
                    RaisePropertyChanged("LastStation");
                }
            }
        }
        private void RaisePropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
