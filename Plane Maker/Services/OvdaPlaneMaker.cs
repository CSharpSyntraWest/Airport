using Airport_Common.Interfaces;
using Airport_Common.Models;
using Airport_Common.Routes;
using Airport_Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Timers;

namespace Plane_Maker.Services
{
    public class OvdaPlaneMaker : IPlaneMaker
    {
        private readonly IPushPlane airPort;
        private Timer timer;
        private readonly string[] planeTypes = { "F-16 Fighting Falcon", "BOEING 787 DREAMLINER", "AIRBUS A350", "AIRBUS A380" };
        private readonly string[] countries = { "ISRAEL", "UNITED ARAB EMIRATES", "THAILAND", "UNITED STATES", "SPAIN", "ENGLAND" };


        private readonly Color[] colors = { Color.White, Color.Black, Color.Blue, Color.Yellow, Color.Green, Color.Red };
        private readonly Route[] routes;

        public OvdaPlaneMaker(IPushPlane plane, params Route[] routes)
        {
            this.airPort = plane;
            this.routes = routes;
        }

        public void ConfigureTimer(TimeSpan intervalTime)
        {
            ConfigureTimer(intervalTime.TotalMilliseconds);
        }

        public void ConfigureTimer(double intervalMills)
        {
            timer = new Timer
            {
                Interval = intervalMills
            };
            timer.Elapsed += Timer_Tick;
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            PushPlane();
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void PushPlane()
        {
            Random random = new Random();

            var planeType = planeTypes[random.Next(0, planeTypes.Length)];
            var county = countries[random.Next(0, countries.Length)];
            var color = colors[random.Next(0, colors.Length)];
            var planeRoute = routes[random.Next(0, routes.Length)];
            var participantes = random.Next(0, 200);

            Plane newPlane = new Plane()
            {
                AirplaneType = planeType,
                Country = county,
                Color = color,
                Route = planeRoute,
                FlightNumber = $"{county.Substring(0, 3)} {IdManager.Id}",
                PassangersCount = participantes
            };

            IdManager.Id++;

            airPort.PushPlane(newPlane);
        }

        public void PushPlane(Route route)
        {
            Random random = new Random();

            var planeType = planeTypes[random.Next(0, planeTypes.Length)];
            var county = countries[random.Next(0, countries.Length)];
            var color = colors[random.Next(0, colors.Length)];
            var planeRoute = route;
            var participantes = random.Next(0, 200);

            Plane newPlane = new Plane()
            {
                AirplaneType = planeType,
                Country = county,
                Color = color,
                Route = planeRoute,
                FlightNumber = "Test 775",
                PassangersCount = participantes
            };

            airPort.PushPlane(newPlane);
        }
    }
}
