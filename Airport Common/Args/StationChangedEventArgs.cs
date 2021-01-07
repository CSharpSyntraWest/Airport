using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Args
{
    public delegate void StationEvent(object sender, StationChangedEventArgs args);
    public class StationChangedEventArgs : EventArgs
    {
        public Queue<Plane> WaitingLine { get; }
        public Plane Plane { get; }
        public DateTime EventTime { get; }
        public PlaneAction PlaneAction { get; set; }

        public StationChangedEventArgs(IEnumerable<Plane> waitingLine, Plane plane, DateTime eventTime, PlaneAction planeAction)
        {
            this.WaitingLine = new Queue<Plane>(waitingLine);
            this.Plane = plane;
            this.EventTime = eventTime;
            this.PlaneAction = planeAction;
        }
    }

    public enum PlaneAction
    {
        EnterStation,
        LeaveStation,
        EnterAirport,
        LeaveAirport,
        EnterWaitingLine,
        LeaveWaitingLine,
        NoAction
    }
}
