using Airport_Logic.Logic_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Airport_Common.Models;
using Airport_Common_Interfaces;

namespace Airport_Test.Services
{
    internal class TestStationService
    {
        private static TestStationService singletonInstance;
        public static TestStationService GetInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new TestStationService();
            }
            return singletonInstance;
        }

        private TestStationService() { }

        internal bool IsCurrentPlane(ICurrentPlane logicStation, string flightNumber)
        {
            if (logicStation.CurrentPlane == null)
            {
                return false;
            }
            return logicStation.CurrentPlane.FlightNumber == flightNumber;
        }

        internal bool HasCurrentPlane(ICurrentPlane logicStation)
        {
            return logicStation.CurrentPlane != null;
        }

        internal bool IsExistInWaitingLine(IWaitingLine waitingLine, string flightNumber)
        {
            return waitingLine.WaitingLine.Any(plane => plane.FlightNumber == flightNumber);
        }

        internal bool IsWaitingLineEmpty(IWaitingLine waitingLine)
        {
            return waitingLine.WaitingLine.IsEmpty;
        }
    }
}
