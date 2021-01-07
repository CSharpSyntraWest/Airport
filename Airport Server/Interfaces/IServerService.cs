using Airport_Common.Args;
using Airport_Logic;
using System.Collections.Generic;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Services
{
    public interface IServerService
    {
        void AssignPlaneMakers();
        void CreateAirports();
        IEnumerable<IAirport> GetAirports();
        void LoadFromDb();
        event StationEvent ChangeInStateEvent;
    }
}