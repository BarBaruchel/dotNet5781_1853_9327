using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLAPI
{
    public interface IBl
    {
        #region Bus
        void addBus(BO.Bus bus, int licenseNum);
        void treatBus(BO.Bus bus);
        void fuelBus(BO.Bus bus);

        DO.Bus BOBusToDoBus(BO.Bus bus);
        IEnumerable<object> getAllBusses();
        void updateBus(BO.Bus bus, int licenseNum);
        void deleteBus(BO.Bus bus, int licenseNum);

        Bus getBusByLicense(int licenseNum);

        BO.Bus DOBusToBOBus(DO.Bus bus);


        #endregion Bus



        #region Lines

        IEnumerable<object> getAllLines();
        void addLine(BO.Line line, int id, int code);
        void updateLine(BO.Line line, int id);
        void deleteLine(BO.Line line, int id);


        #endregion


        #region User

        bool loginAdmin(string username, string password);


        #endregion User




        #region Station

        double getDistanceBetweenTwoStations(BO.Station from, int codeF, BO.Station to, int codeT);
        void addStation(BO.Station station, int code);
        IEnumerable<object> getAllStations();
        void updateStation(BO.Station Station, int code);
        void deleteStation(BO.Station Station, int code);


        #endregion Station



        #region LineStation
        void updateLineStation(BO.LineStation lineStation, int lineId);

        #endregion LineStation

    }
}
