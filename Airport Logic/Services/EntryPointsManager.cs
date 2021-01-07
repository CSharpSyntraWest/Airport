using Airport_Common.Models;
using Airport_Logic.Logic_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airport_Logic.Services
{
    public class EntryPointsManager
    {
        private readonly Dictionary<string, List<LogicStation>> EntryPoints;

        public EntryPointsManager()
        {
            EntryPoints = new Dictionary<string, List<LogicStation>>();
        }

        internal void InitializeEntryPoint(string name)
        {
            EntryPoints.Add(name, new List<LogicStation>());
        }

        internal void AddStationToEntry(string entryName, LogicStation station)
        {
            EntryPoints[entryName].Add(station);
        }

        public List<LogicStation> GetEntryStations(string entryName)
        {
            return EntryPoints[entryName];
        }

        public LogicStation GetEntryStation(string entryName, int stationNumber)
        {
            return EntryPoints[entryName].First(station => station.StationNumber == stationNumber);
        }

        public List<LogicStation> this[string entry]
        {
            get
            {
                return GetEntryStations(entry);
            }
        }
    }
}
