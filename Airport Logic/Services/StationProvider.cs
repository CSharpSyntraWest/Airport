using Airport_Logic.Logic_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Airport_Common.Models;
using Airport_Logic.Interfaces;
using static Airport_Logic.Logic_Models.LogicStation;
using System.Threading.Tasks;
using System.Threading;

namespace Airport_Logic.Services
{
    internal class StationProvider : IStationProvider
    {
        private List<LogicStation> stations = new List<LogicStation>();
        private readonly IRaiseChangeInStateEvent changeInStateEvent;
        private int stationCount = 0;

        public StationProvider(IRaiseChangeInStateEvent changeInStateEvent)
        {
            this.changeInStateEvent = changeInStateEvent;
        }
        public void CreateStation(string stationName, TimeSpan timeSpan)
        {
            stationCount++;
            if (stationCount == 0)
            {
                throw new ArgumentException("Station number cannot be 0");
            }

            if (stations.Any(s => s.StationNumber == stationCount))
            {
                throw new ArgumentException("Station number must be unique");
            }

            LogicStation station = new LogicStation()
            {
                StationNumber = stationCount,
                StationName = stationName,
                WaitingTime = timeSpan,
            };

            AddStation(station);
        }

        private void AddStation(LogicStation station)
        {
            station.ChangeInState += this.changeInStateEvent.RaiseChangeInStateEvent;

            this.stations.Add(station);
        }
        public void RestoreStations(List<Station> stations)
        {
            foreach (var station in stations)
            {
                AddStation(RestoreStation(station));
            }
        }

        /// <summary>
        /// Returns a station without planes(waiting line and current plane)
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        private LogicStation RestoreStation(Station station)
        {
            LogicStation logicStation = new LogicStation()
            {
                Id = station.Id,
                StationName = station.StationName,
                StationNumber = station.StationNumber,
                WaitingTime = station.WaitingTime,
            };

            return logicStation;
        }
        public LogicStation GetStation(int stationNum)
        {
            if (stationNum == 0)
            {
                throw new ArgumentException("station Number cannot be 0");
            }
            return stations.First(s => s.StationNumber == stationNum);
        }

        public IEnumerable<LogicStation> GetStations()
        {
            return stations;
        }

        public void RestorePlanes(IEnumerable<Station> commonStations)
        {
            List<Station> listCommonStations = commonStations.ToList();
            //first restore the waiting lines, this will not trigger activity in the station yet.
            for (int i = 0; i < this.stations.Count; i++)
            {
                if (listCommonStations[i].WaitingLine != null && !listCommonStations[i].WaitingLine.Any())
                {
                    this.stations[i].WaitingLine = listCommonStations[i].WaitingLine;
                }
            }

            //now restore the current planes, this will trigget activity in the station.
            for (int i = 0; i < this.stations.Count; i++)
            {
                Plane currentPlane = listCommonStations[i].CurrentPlane;

                if (currentPlane != null)
                {
                    this.stations[i].EnterStationRestore(currentPlane);
                }
            }
        }
    }
}
