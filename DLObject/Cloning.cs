using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DL
{
    internal static class Cloning
    {
        public static Bus Clone(this Bus source)  //The function get Bus variable and do deep copying and send the new variable
        {
            return new Bus
            {
                StartPeilut = source.StartPeilut,
                LastTreat = source.LastTreat,
                KiloFromLastTreat = source.KiloFromLastTreat,
                LicenseNum = source.LicenseNum,
                Fuel = source.Fuel,
                Kilometrage = source.Kilometrage,
                Status = source.Status
            };
        }

        public static Station Clone(this Station source)   //The function get Station variable and do deep copying and send the new variable
        {
            return new Station
            {
                Code = source.Code,
                Location = source.Location,
                Address = source.Address,
                Name = source.Name
            };
        }
        public static AdjacentStations Clone(this AdjacentStations source)   //The function get AdjacentStations variable and do deep copying and send the new variable
        {
            return new AdjacentStations
            {
                station1 = source.station1,
                station2 = source.station2,
                Distance = source.Distance,
                Time = source.Time

            };
        }

        public static BusOnTrip Clone(this BusOnTrip source)    //The function get BusOnTrip variable and do deep copying and send the new variable
        {
            return new BusOnTrip
            {
                Id = source.Id,
                LicenseNum = source.LicenseNum,
                LineId = source.LineId,
                PlannedTakeOff = source.PlannedTakeOff,
                ActualTakeOff = source.ActualTakeOff,
                PrevStation = source.PrevStation,
                PrevStationAt = source.PrevStationAt,
                NextStationAt = source.NextStationAt
            };
        }
        public static Line Clone(this Line source)   //The function get Line variable and do deep copying and send the new variable
        {
            return new Line
            {
                Id = source.Id,
                Code = source.Code,
                Area = source.Area,
                FirstStation = source.FirstStation,
                LastStation = source.LastStation
            };
        }
        public static LineStation Clone(this LineStation source)   //The function get LineStation variable and do deep copying and send the new variable
        {
            return new LineStation
            {
                //In the Dal layer there is no Name field
                LineId = source.LineId,
                Station = source.Station,
                LineStationIndex = source.LineStationIndex,
                PrevStation = source.PrevStation,
                NextStation = source.NextStation,
                TravelTimeFromTheLastStation = source.TravelTimeFromTheLastStation,
                DistanceFromTheLastStat = source.DistanceFromTheLastStat
            };
        }
        public static LineTrip Clone(this LineTrip source) //The function get LineTrip variable and do deep copying and send the new variable
        {
            return new LineTrip
            {
                Id = source.Id,
                LineId = source.LineId,
                StartAt = source.StartAt,
                FinishAt = source.FinishAt,
                Frequency = source.Frequency
            };
        }

        public static Trip Clone(this Trip source)  //The function get Trip variable and do deep copying and send the new variable
        {
            return new Trip
            {
                Id = source.Id,
                UserName = source.UserName,
                LineId = source.LineId,
                InStation = source.InStation,
                OutStation = source.OutStation,
                InAt = source.InAt,
                OutAt = source.OutAt

            };
        }
        public static UserTrips Clone(this UserTrips source)  //The function get UserTrips variable and do deep copying and send the new variable
        {
            return new UserTrips
            {
                TripId = source.TripId,
                Username = source.Username,
                BusCode = source.BusCode,
                FirstStation = source.FirstStation,
                LastStation = source.LastStation,
                InAt = source.InAt,
                OutAt = source.OutAt
            };
        }

        public static User Clone(this User source)  //The function get User variable and do deep copying and send the new variable
        {
            return new User
            {

                UserName = source.UserName,
                Password = source.Password,
                Admin = source.Admin
            };
        }








    }
}
