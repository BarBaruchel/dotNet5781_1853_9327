using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
    public interface IDL
    {
        #region Bus
        void addBus(DO.Bus bus);
        void treatBus(DO.Bus bus);
        void fuelBus(DO.Bus bus);
        IEnumerable<object> getAllBusses();
        void updateBus(DO.Bus bus);
        void deleteBus(DO.Bus bus);
        void Bedika(DO.Bus bus, int distance);
        DO.Bus getBusByLicenseNum(int licenseNum);
        
        #endregion Bus

        #region Lines

        IEnumerable<object> getAllLines();
        void addLine(DO.Line line);
        void updateLine(DO.Line line);
        void deleteLine(DO.Line line);
        DO.Line getLineById(int id);

        #endregion Line

        #region User
        User getAdmin();

        #endregion User


        #region Station

        void addStation(DO.Station station);
        IEnumerable<object> getAllStations();
        void updateStation(DO.Station Station);
        void deleteStation(DO.Station Station);
       // Station ExistStation(int code);

        #endregion Station


        #region LineStation
        void AddFollowingStation(DO.LineStation lineStation, double distanceFromThePrevToFollowing, TimeSpan timeFromThePrevToFollowing);
        void updateLineStation(DO.LineStation lineStation);
        double getDistanceBetweenTwoStations(DO.LineStation from, DO.LineStation to);
        IEnumerable<object> getAllLineStations();

        #endregion LineStation




    }
}
