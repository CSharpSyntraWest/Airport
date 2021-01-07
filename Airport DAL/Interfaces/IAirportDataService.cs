using Airport_DAL.Context;
using Airport_DAL.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport_DAL.Services
{
    public interface IAirportDataService
    {
        DataContext Context { get; set; }

        Task AddAirport(DbAirport airport);
        void AddPlane(DbPlane plane);
        IEnumerable<DbAirport> GetAirports();
        int GetIdCount();
        Task<DbPlane> GetPlane(string flightNumber);
        DbStation GetStation(int id);
        Task<DbStation> GetStationAsync(int id);
        void UpdateStation(DbStation stationDetails);
        IEnumerable<PlaneLog> GetLogs();
    }
}