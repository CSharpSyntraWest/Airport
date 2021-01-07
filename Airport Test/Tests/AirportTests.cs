using Airport_Common.Routes;
using Airport_Logic;
using Airport_Logic.Logic_Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Airport_Test.Mock;
using System.Threading.Tasks;
using System.Threading;
using Airport_Common.Models;
using Airport_Simulator;
using Airport_Test.TestRoute;
using Airport_Test.Services;

namespace Airport_Test
{
    [TestClass]
    public class AirportTests
    {
        private readonly TestStationService StationService;
        public AirportTests()
        {
            StationService = TestStationService.GetInstance();
        }

        [TestMethod]
        public void InitializeAndConfigure_LandingRoute_Successfull()
        {
            var airport = new Airport(builder =>
            {
                //Add Stations
                builder.AddStation("Station1", TimeSpan.FromSeconds(15));
                builder.AddStation("Station2", TimeSpan.FromSeconds(10));
                builder.AddStation("Station3", TimeSpan.FromSeconds(30));
                builder.AddStation("Station4", TimeSpan.FromSeconds(5));
                builder.AddStation("Station5", TimeSpan.FromSeconds(10));
                builder.AddStation("Station6", TimeSpan.FromSeconds(15));
                builder.AddStation("Station7", TimeSpan.FromSeconds(15));
                builder.AddStation("Station8", TimeSpan.FromSeconds(40));

                //Add Routes
                builder.AddRoute(new LandingRoute());

            }, "Ben Gurion");

            // Arrange

            var entries = airport.EntryManager.GetEntryStations("Landing");
            var secondLayer = entries[0].ConnectedStations;
            var thirdLayer = secondLayer[0].ConnectedStations;
            var fouthLayer = thirdLayer[0].ConnectedStations;
            var fithLayer = fouthLayer[0].ConnectedStations;


            //Act

            //entry layer
            bool firstLayerContain8 = entries.Any(station => station.StationNumber == 8);
            bool firstLayerContainOnly8 = !(entries.Any(station => station.StationNumber != 8));

            //second layer
            bool secondLayerContain7 = secondLayer.Any(station => station.StationNumber == 7);
            bool secondLayerContain6 = secondLayer.Any(station => station.StationNumber == 6);
            bool secondLayerContain5 = secondLayer.Any(station => station.StationNumber == 5);
            bool secondLayerContainOnly = !(secondLayer.Any(station => station.StationNumber != 7
            && station.StationNumber != 6
            && station.StationNumber != 5));

            //third layer
            bool thirdLayerContain4 = thirdLayer.Any(station => station.StationNumber == 4);
            bool thirdLayerContain3 = thirdLayer.Any(station => station.StationNumber == 3);
            bool thirdLayerContainOnly = !(thirdLayer.Any(station => station.StationNumber != 4 && station.StationNumber != 3));

            //fourth layer
            bool fouthLayerContain2 = fouthLayer.Any(station => station.StationNumber == 2);
            bool fouthLayerContain1 = fouthLayer.Any(station => station.StationNumber == 1);
            bool fouthLayerContainOnly = !(fouthLayer.Any(station => station.StationNumber != 2 && station.StationNumber != 1));

            //fith layer
            bool fithLayerEmpty = !fithLayer.Any();

            //Assert

            Assert.IsTrue(firstLayerContain8);
            Assert.IsTrue(firstLayerContainOnly8);

            Assert.IsTrue(secondLayerContain7);
            Assert.IsTrue(secondLayerContain6);
            Assert.IsTrue(secondLayerContain5);
            Assert.IsTrue(secondLayerContainOnly);

            Assert.IsTrue(thirdLayerContain4);
            Assert.IsTrue(thirdLayerContain3);
            Assert.IsTrue(thirdLayerContainOnly);

            Assert.IsTrue(fouthLayerContain2);
            Assert.IsTrue(fouthLayerContain1);
            Assert.IsTrue(fouthLayerContainOnly);

            Assert.IsTrue(fithLayerEmpty);
        }

        [TestMethod]
        public void PushPlanes_DefualtAirport_1Plane()
        {
            //Arrange
            var airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            }, "Ben Gurion");

            IPlaneMaker MockSimulator = new PlaneMakerMock(airport);

            var station8 = airport.EntryManager.GetEntryStation("Landing", 8);
            var station7 = station8.GetLogicStationByNumber(7);
            var station4 = station7.GetLogicStationByNumber(4);
            var station2 = station4.GetLogicStationByNumber(2);

            //Act 
            MockSimulator.PushPlane(new LandingRoute());

            Thread.Sleep(50);
            Assert.IsTrue(StationService.IsCurrentPlane(station8, "0"));

            Thread.Sleep(TimeSpan.FromSeconds(15.05)); // wait 15 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station8));
            Assert.IsTrue(StationService.IsCurrentPlane(station7, "0"));

            Thread.Sleep(TimeSpan.FromSeconds(20.05)); // Wait 20 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station7));
            Assert.IsTrue(StationService.IsCurrentPlane(station4, "0"));

            Thread.Sleep(TimeSpan.FromSeconds(5.05)); // Wait 5 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station4));
            Assert.IsTrue(StationService.IsCurrentPlane(station2, "0"));

            Thread.Sleep(TimeSpan.FromSeconds(10.05)); // Wait 10 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station2));
        }

        [TestMethod]
        public void PushPlanes_DefualtAirport_1Plane_CheckIfDisposePlaneBeforeTime()
        {
            //Arrange
            var airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            }, "Ben Gurion");

            IPlaneMaker MockSimulator = new PlaneMakerMock(airport);

            var station8 = airport.EntryManager.GetEntryStation("Landing", 8);
            var station7 = station8.GetLogicStationByNumber(7);
            var station4 = station7.GetLogicStationByNumber(4);
            var station2 = station4.GetLogicStationByNumber(2);


            //Act 
            MockSimulator.PushPlane(new LandingRoute());

            Thread.Sleep(100);
            Assert.IsTrue(StationService.IsCurrentPlane(station8, "0"));

            Thread.Sleep(15005); // wait 15 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station8));
            Assert.IsTrue(StationService.IsCurrentPlane(station7, "0"));

            Thread.Sleep(20005); // Wait 20 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station7));
            Assert.IsTrue(StationService.IsCurrentPlane(station4, "0"));

            Thread.Sleep(5005); // Wait 5 seconds
            Assert.IsFalse(StationService.HasCurrentPlane(station4));
            Assert.IsTrue(StationService.IsCurrentPlane(station2, "0"));

            Thread.Sleep(5000); // Wait 5 seconds **wrong
            Assert.IsTrue(StationService.HasCurrentPlane(station2));
        }

        [TestMethod]
        public void PushPlane_DefualtAirport_3Planes_3Layers()

        {
            //Arrange
            var airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            }, "Ben Gurion");

            IPlaneMaker MockSimulator = new PlaneMakerMock(airport);

            var station8 = airport.EntryManager["Landing"].First();
            var station7 = station8.GetLogicStationByNumber(7);
            var station6 = station8.GetLogicStationByNumber(6);
            var station4 = station7.GetLogicStationByNumber(4);

            var landingRoute = new LandingRoute();

            //Act 
            MockSimulator.PushPlane(landingRoute);

            Thread.Sleep(50);

            MockSimulator.PushPlane(landingRoute);

            Thread.Sleep(50);

            MockSimulator.PushPlane(landingRoute);

            Thread.Sleep(50);


            void FirstStage()
            {
                Assert.IsTrue(StationService.IsCurrentPlane(station8, "0"));
                int id = 1;
                foreach (var plane in station8.WaitingLine)
                {
                    Assert.IsTrue(plane.FlightNumber == id.ToString());
                    id++;
                }
            }
            FirstStage();

            Thread.Sleep(TimeSpan.FromSeconds(15.15));

            void SecondStage()
            {
                Assert.IsTrue(StationService.IsCurrentPlane(station8, "1"));

                station8.WaitingLine.TryPeek(out Plane waitngPlane8);
                Assert.IsTrue(waitngPlane8.FlightNumber == "2");
                Assert.IsTrue(StationService.IsCurrentPlane(station7, "0"));
            }
            SecondStage();

            Thread.Sleep(TimeSpan.FromSeconds(20.15));

            void ThirdStage()
            {
                Assert.IsTrue(StationService.IsCurrentPlane(station8, "2"));
                Assert.IsTrue(StationService.IsWaitingLineEmpty(station8));

                Assert.IsTrue(StationService.IsCurrentPlane(station6, "1"));
                Assert.IsTrue(StationService.IsWaitingLineEmpty(station6));

                Assert.IsTrue(StationService.IsCurrentPlane(station4, "0"));
                Assert.IsTrue(StationService.IsWaitingLineEmpty(station4));
            }
            ThirdStage();
        }

        [TestMethod]
        public void PushPlane_3StationAirport_1Plane()
        {
            //arrange
            var testRoute3Stations = new TestRoute3Stations();

            var airport = new Airport(builder =>
            {
                builder.AddStation("station1", TimeSpan.FromSeconds(1));
                builder.AddStation("station2", TimeSpan.FromSeconds(1));
                builder.AddStation("station3", TimeSpan.FromSeconds(1));

                builder.AddRoute(testRoute3Stations);
            }, "Ben Gurion");

            var planeMaker = new PlaneMakerMock(airport);

            //act 
            planeMaker.PushPlane(testRoute3Stations);

            Thread.Sleep(50);

            //assert
            var firstStation = airport.EntryManager.GetEntryStation(testRoute3Stations.Name, 1);
            Assert.IsTrue(StationService.IsCurrentPlane(firstStation, "0"));
            Thread.Sleep(TimeSpan.FromSeconds(1.05));

            var secondStation = firstStation.GetLogicStationByNumber(2);
            Assert.IsTrue(StationService.IsCurrentPlane(secondStation, "0"));
            Assert.IsFalse(StationService.HasCurrentPlane(firstStation));
            Thread.Sleep(TimeSpan.FromSeconds(1.05));

            var thirdStation = secondStation.GetLogicStationByNumber(3);
            Assert.IsTrue(StationService.IsCurrentPlane(thirdStation, "0"));
            Assert.IsFalse(StationService.HasCurrentPlane(secondStation));
            Thread.Sleep(TimeSpan.FromSeconds(1.05));

            Assert.IsFalse(StationService.HasCurrentPlane(thirdStation));
        }

        [TestMethod]
        public void PushPlane_3StationAirport_1Plane_DifferentTimes()
        {
            //arrange
            var testRoute3Stations = new TestRoute3Stations();

            var airport = new Airport(builder =>
            {
                builder.AddStation("station1", TimeSpan.FromSeconds(1));
                builder.AddStation("station2", TimeSpan.FromSeconds(3));
                builder.AddStation("station3", TimeSpan.FromSeconds(2));

                builder.AddRoute(testRoute3Stations);
            }, "Ben Gurion");

            var planeMaker = new PlaneMakerMock(airport);

            //act 
            planeMaker.PushPlane(testRoute3Stations);

            Thread.Sleep(50);

            //assert
            var firstStation = airport.EntryManager.GetEntryStation(testRoute3Stations.Name, 1);
            Assert.IsTrue(StationService.IsCurrentPlane(firstStation, "0"));
            Thread.Sleep(TimeSpan.FromSeconds(1.05));

            var secondStation = firstStation.GetLogicStationByNumber(2);
            Assert.IsTrue(StationService.IsCurrentPlane(secondStation, "0"));
            Assert.IsFalse(StationService.HasCurrentPlane(firstStation));
            Thread.Sleep(TimeSpan.FromSeconds(3.05));

            var thirdStation = secondStation.GetLogicStationByNumber(3);
            Assert.IsTrue(StationService.IsCurrentPlane(thirdStation, "0"));
            Assert.IsFalse(StationService.HasCurrentPlane(secondStation));
            Thread.Sleep(TimeSpan.FromSeconds(2.05));

            Assert.IsFalse(StationService.HasCurrentPlane(thirdStation));
        }
    }
}
