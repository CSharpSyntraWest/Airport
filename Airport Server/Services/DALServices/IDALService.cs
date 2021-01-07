using Airport_Common.Args;
using Airport_DAL.DatabaseModels;
using Airport_Logic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport_Server.Services.DALServices
{
    public interface IDALService
    {
        void UpdateDatabase(object sender, StationChangedEventArgs args);
        void AddLogDatabase(object sender, StationChangedEventArgs args);
        Task AddAiportToDatabaseAsync(IAirport airport);
        IEnumerable<DbAirport> GetAirports();
        int GetIdCount();
    }
}