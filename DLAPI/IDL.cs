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
        IEnumerable<DO.Bus> getAllBusses();
        void updateBus(DO.Bus bus);
        void deleteBus(DO.Bus bus);
        DO.Bus getBusByLicenseNum(int licenseNum);
        
        #endregion Bus

        #region Lines

        IEnumerable<DO.Line> getAllLines();
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
        IEnumerable<DO.Station> getAllStations();
        void updateStation(DO.Station station);
        void deleteStation(DO.Station station);
       
        #endregion Station


        #region LineStation
        void addLineStation(DO.LineStation lineStation);
        void updateLineStation(DO.LineStation lineStation);
        IEnumerable<DO.LineStation> getAllLineStations();

        #endregion LineStation

        #region LineTrip
        IEnumerable<DO.LineTrip> getAllLineTrips();
        #endregion LineTrip



    }
}
