using Airport_Common.Interfaces;
using Airport_Common.Models;
using Airport_Logic.Logic_Models;
using Airport_Test.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Airport_Test
{
    [TestClass]
    public class LogicStationTests
    {
        private readonly TestStationService testStationService;
        public LogicStationTests()
        {
            testStationService = TestStationService.GetInstance();
        }

        [TestMethod]
        public void InitializeLogicStation()
        {
            var station = new LogicStation();
        }

        [TestMethod]
        public void AddStation()
        {
            //Arrange
            var station1 = new LogicStation();
            var station2 = new LogicStation();

            //Act
            station1.AddStation(station2);

            //Assert
            Assert.IsTrue(station1.ConnectedStations[0].Equals(station2));
        }

        [TestMethod]
        public void GetStation()
        {
            //Arrange
            var station1 = new LogicStation();
            var station2 = new LogicStation()
            {
                StationNumber = 2
            };

            //Act
            station1.AddStation(station2);

            //Assert
            Assert.IsTrue(station1.GetLogicStationByNumber(2).Equals(station2));
        }

        [TestMethod]
        public void EnterStation_1Plane()
        {
            //Arrange
            var station1 = new LogicStation
            {
                WaitingTime = TimeSpan.FromSeconds(2)
            };

            var plane = new Plane()
            {
                Route = new MockRoute()
            };

            //Act
            station1.EnterStation(plane);

            //Assert
            Thread.Sleep(10);
            Assert.IsTrue(station1.CurrentPlane.Equals(plane));

            Thread.Sleep(TimeSpan.FromSeconds(2.01));
            Assert.IsTrue(station1.CurrentPlane == null);
        }

        [TestMethod]
        public void EnterStation_2Planes()
        {
            //Arrange

            var s1 = new LogicStation()
            {
                WaitingTime = TimeSpan.FromSeconds(4),
                StationNumber = 1
            };

            var plane1 = new Plane()
            {
                Route = new MockRoute(),
                FlightNumber = "0"
            };

            var plane2 = new Plane()
            {
                Route = new MockRoute(),
                FlightNumber = "1"
            };

            //Act

            s1.EnterStation(plane1);
            Thread.Sleep(50);

            s1.EnterStation(plane2);
            Thread.Sleep(50);

            //Assert


            testStationService.IsCurrentPlane(s1, "0");
            testStationService.IsExistInWaitingLine(s1, "1");

            Thread.Sleep(TimeSpan.FromSeconds(4.1));
            testStationService.IsCurrentPlane(s1, "1");
            testStationService.IsWaitingLineEmpty(s1);

            Thread.Sleep(TimeSpan.FromSeconds(4.1));
            Assert.IsFalse(testStationService.HasCurrentPlane(s1));
        }

        public class MockRoute : Route
        {
            public new string Name => "Test1";

            public new IEnumerable<int> GetNextAvailableRoute(int stationNumber)
            {
                switch (stationNumber)
                {
                    case 1:
                        yield return 0;
                        break;
                }
            }
        }
    }
}
