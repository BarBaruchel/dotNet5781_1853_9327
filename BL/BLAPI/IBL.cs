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
        void addBus(BO.Bus bus);
        void treatBus(BO.Bus bus);
        void fuelBus(BO.Bus bus);
        IEnumerable<object> getAllBusses();
        void updateBus(BO.Bus bus);
        void deleteBus(BO.Bus bus);
        DO.Bus BOBusToDoBus(BO.Bus bus);
        BO.Bus DOBusToBOBus(DO.Bus bus);
        BO.Bus getBusByLicense(int licenseNum);



        #endregion Bus



        #region Line

        IEnumerable<object> getAllLines();
        void addLine(BO.Line line);
        void updateLine(BO.Line line);
        void deleteLine(BO.Line line);
        BO.Line DOLineToBOLine(DO.Line line);
        DO.Line BOLineToDOLine(BO.Line line);
        BO.Line getLineById(int id);

        #endregion Line


        #region User

        bool loginAdmin(string username, string password);

        #endregion User


        #region Station

        BO.Station getStationByCode(int code);
        void addStation(BO.Station station);
        IEnumerable<object> getAllStations();
        void updateStation(BO.Station Station);
        void deleteStation(BO.Station Station);
        DO.Station BOStationToDOStation(BO.Station station);
        BO.Station DOStationToBOStation(DO.Station station);
        IEnumerable<object> getAllStationCode();
        #endregion Station


        #region LineStation

        void AddFollowingStation(BO.LineStation lineStation, double distanceFromThePrevToFollowing, TimeSpan timeFromThePrevToFollowing);
        IEnumerable<LineStation> getLineStationByCode(int code);
        void UpdateDistanceLineStation(int lineId, int station, double distance);
        void UpdateTimeLineStation(int lineId, int station, TimeSpan time);
        void updateLineStation(BO.LineStation lineStation);
        double getDistanceBetweenTwoStations(BO.LineStation from, BO.LineStation to);
        DO.LineStation BOLineStationToDOLineStation(BO.LineStation lineStation);
        IEnumerable<LineStation> getLineStationsForLine(BO.Line line);
        BO.LineStation DOLineStationToBOLineStation(DO.LineStation lineStation);

        #endregion LineStation


        List<LineTiming> startSimulator(List<BO.LineStation> BOLineStations, TimeSpan startTime);
        TimeSpan updateTime(LineTiming lineTiming, int rate);

    }
}
