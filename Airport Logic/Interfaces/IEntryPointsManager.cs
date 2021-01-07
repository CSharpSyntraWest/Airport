using Airport_Logic.Logic_Models;
using System.Collections.Generic;

namespace Airport_Logic.Services
{
    public interface IEntryPointsManager
    {
        LogicStation GetEntryStation(string entryName, int stationNumber);
        List<LogicStation> GetEntryStations(string entryName);
    }
}