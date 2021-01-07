using Airport_Common.Args;
using Airport_Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Airport_Logic.Logic_Models
{
    public class LogicStation : Station, IEnterStation, IWaitingLine
    {

        internal event StationEvent ChangeInState;
        private bool isStationOccupied;
        private readonly object waitingLineLock = new object();

        public LogicStation()
        {
            WaitingLine = new ConcurrentQueue<Plane>();
            ChangeInState += PushToWait;
        }

        private bool IsWaitinglineEmpty
        {
            get
            {
                return WaitingLine.IsEmpty;
            }
        }

        private void PushToWait(object sender, StationChangedEventArgs args)
        {
            Task.Run(() =>
            {
                if (!IsWaitinglineEmpty && !isStationOccupied)
                {
                    WaitingLine.TryDequeue(out Plane plane);

                    Wait(plane);
                }
            });
        }

        public void EnterStationRestore(Plane plane)
        {
            Task.Run(() =>
            {
                Wait(plane);
            });
        }

        public void EnterStation(Plane plane)
        {
            if (IsWaitinglineEmpty && !isStationOccupied)
            {
                Task.Run(() =>
                {
                    Wait(plane);
                });
            }
            else
            {
                WaitingLine.Enqueue(plane);
                ChangeInState?.Invoke(this, new StationChangedEventArgs(this.WaitingLine, plane, DateTime.Now, PlaneAction.EnterWaitingLine));
            }
        }

        private void Wait(Plane plane)
        {
            lock (waitingLineLock)
            {
                isStationOccupied = true;
                base.CurrentPlane = plane;
                ChangeInState?.Invoke(this, new StationChangedEventArgs(this.WaitingLine, plane, DateTime.Now, PlaneAction.EnterStation));
                Thread.Sleep(base.WaitingTime);
                base.CurrentPlane = null;
            }

            isStationOccupied = false;
            ChangeInState?.Invoke(this, new StationChangedEventArgs(this.WaitingLine, plane, DateTime.Now, PlaneAction.LeaveStation));

            MoveToNextStation(plane);
        }


        private void MoveToNextStation(Plane plane)
        {
            LogicStation nextStation = GetBestStation(plane);
            if (nextStation != null)
            {
                nextStation.EnterStation(plane);
            }
        }

        /// <summary>
        /// Get the least busy station for a plane(depends on the route)
        /// </summary>
        /// <param name="plane">plane to choose the next route for</param>
        /// <returns>least busy station for the plane</returns>
        private LogicStation GetBestStation(IRouteable plane)
        {
            var nextStationNumbers = plane.Route.GetNextAvailableRoute(this.StationNumber);

            if (!nextStationNumbers.Any())
            {
                throw new Exception("Could not be empty, if we reach the end we receive 0");
            }
            else if (nextStationNumbers.Any(staionNum => staionNum == 0)) //if it reached the end
            {
                return null;
            }
            else
            {
                var nextStations = GetStations(nextStationNumbers);
                return LogicStation.GetBestStation(nextStations);
            }
        }


        private List<LogicStation> GetStations(IEnumerable<int> stationNumbers)
        {
            List<LogicStation> NextAvaliableStations = new List<LogicStation>();
            foreach (var num in stationNumbers)
            {
                var station = GetLogicStationByNumber(num);
                NextAvaliableStations.Add(station);
            }
            return NextAvaliableStations;
        }

        /// <summary>
        /// Returns the least busy station
        /// </summary>
        /// <param name="stations">stations to pick from</param>
        /// <returns>least busy station</returns>
        internal static LogicStation GetBestStation(IEnumerable<LogicStation> stations)
        {
            return stations.OrderBy(station =>
             {
                 int count = 0;
                 if (station.isStationOccupied)
                 {
                     count++;
                 }
                 count += station.WaitingLine.Count;
                 return count;
             }).First();
        }

        /// <summary>
        /// Get a LogicStation Instance from the connected stations.
        /// </summary>
        /// <param name="stationNumber">station number</param>
        /// <returns>Instance of LogicStation</returns>
        /// <exception cref="Exception">If station number does not exist in the station connections</exception>
        public LogicStation GetLogicStationByNumber(int stationNumber)
        {
            return (LogicStation)base.ConnectedStations.First(s => s.StationNumber == stationNumber);
        }
        
    }

}
