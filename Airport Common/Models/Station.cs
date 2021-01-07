using Airport_Common_Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class Station : ICurrentPlane
    {
        public int Id { get; set; }
        public int StationNumber { get; set; }
        public string StationName { get; set; }
        public Plane CurrentPlane { get; set; }
      
        public ConcurrentQueue<Plane> WaitingLine { get; set; }

        public List<Station> ConnectedStations { get; protected set; }

        public Station()
        {
            ConnectedStations = new List<Station>();
        }


        public TimeSpan WaitingTime { get; set; }

        public void AddStation(Station station)
        {
            if (!ConnectedStations.Contains(station))
            {
                ConnectedStations.Add(station);
            }
        }

        public override string ToString()
        {
            return $"{this.StationNumber}, {this.StationName}";
        }
    }
}
