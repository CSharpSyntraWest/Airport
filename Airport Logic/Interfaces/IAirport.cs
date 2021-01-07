using Airport_Common.Args;
using Airport_Common.Interfaces;
using Airport_Common.Models;
using System.Collections.Generic;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic
{
    public interface IAirport : IPushPlane
    {
        int Id { get; }
        string Name { get; }
        string ImageUrl { get; }
        List<Route> Routes { get; }
        IEnumerable<Station> GetStations();
        event StationEvent ChangeInState;
    }
}