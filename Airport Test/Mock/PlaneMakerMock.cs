using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Timers;
using Airport_Common.Interfaces;
using Airport_Common.Models;
using Airport_Common.Routes;
using Airport_Simulator;

namespace Airport_Test.Mock
{
    public class PlaneMakerMock : IPlaneMaker
    {
        private readonly IPushPlane pushPlane;
        private int id = 0;
        private Timer timer;

        public PlaneMakerMock(IPushPlane pushPlane)
        {
            this.pushPlane = pushPlane;
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
            pushPlane.PushPlane(new Plane()
            {
                AirplaneType = "",
                Color = Color.White,
                Country = "israel",
                PassangersCount = 100,
                Route = new LandingRoute(),
                FlightNumber = id.ToString()
            });

            id++;
        }

        public void PushPlane(Route route)
        {
            pushPlane.PushPlane(new Plane()
            {
                AirplaneType = "",
                Color = Color.White,
                Country = "israel",
                PassangersCount = 100,
                Route = route,
                FlightNumber = id.ToString()
            });
            id++;
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
            throw new NotImplementedException();
        }
    }
}
