using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Station
    {
        private string name;
        private string address;
        public int Code { get; set; }
        public GeoCoordinate Location { get; set; } = new GeoCoordinate();
        public string Address 
        {
            get => address;
            set
            {
                if (value != address)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }
        public string Name 
        { get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    RaisePropertyChanged("Name");
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


