using Airport_Common.Models;
using Airport_Common.Routes;
using Airport_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Services
{
    public class AirportBuilder
    {
        public Route[] GetOvdaRoutes()
        {
            int[][] landArr = new int[][]
           {
                    new int[] { 10, 9, 8 },
                    new int[] { 0 },
                    new int[] { 0 },
                    new int[] { 0 },
                    new int[] { 3, 2, 1 },
                    new int[] { 3, 2, 1 },
                    new int[] { 5, 4 },
                    new int[] { 5, 4 },
                    new int[] { 7, 6 },
                    new int[] { 7, 6 },
                    new int[] { 7, 6 },
           };
            Route landing = new Route(landArr, "Landing");
            int[][] takeoffArr = new int[][]
            {
                    new int[] { 1, 2, 3 },
                    new int[] { 6, 7 },
                    new int[] { 6, 7 },
                    new int[] { 6, 7 },
                    new int[] { },
                    new int[] { },
                    new int[] { 8, 9, 10 },
                    new int[] { 8, 9, 10 },
                    new int[] { 0 },
                    new int[] { 0 },
                    new int[] { 0 },
            };
            Route takeoff = new Route(takeoffArr, "TakeOff");

            return new Route[] { landing, takeoff };
        }

        public Airport BuildBenGurionAirport()
        {
            const string imgUrl = "https://www.gannett-cdn.com/-mm-/9e1f6e2ee20f44aa1f3be4f71e9f3e52b6ae2c7e/c=0-110-2121-1303/local/-/media/2020/04/02/USATODAY/usatsports/airport-airplanes-source-getty.jpg";
            return new Airport(builder =>
            {
                builder.AddStation("Passangers Entrance 1", TimeSpan.FromSeconds(10));
                builder.AddStation("Passangers Entrance 2", TimeSpan.FromSeconds(10));
                builder.AddStation("Saftey Checks 1", TimeSpan.FromSeconds(10));
                builder.AddStation("Saftey Checks 2", TimeSpan.FromSeconds(10));
                builder.AddStation("Luggage 1", TimeSpan.FromSeconds(30));
                builder.AddStation("Luggage 2", TimeSpan.FromSeconds(30));
                builder.AddStation("Luggage 3", TimeSpan.FromSeconds(30));
                builder.AddStation("Runway", TimeSpan.FromSeconds(15));

                builder.AddRoute(new LandingRoute());
                builder.AddRoute(new TakeOffRoute());

            }, "Ben Gurion", imgUrl);
        }

        public Airport BuildOvdaAirport(Route[] routes)
        {
            const string imgUrl = "https://www.ynet.co.il/PicServer2/24012010/2547139/2_wa.jpg";
            return new Airport(builder =>
            {
                builder.AddStation("Passangers Entrance 1", TimeSpan.FromSeconds(20));
                builder.AddStation("Passangers Entrance 2", TimeSpan.FromSeconds(20));
                builder.AddStation("Passangers Entranc 3", TimeSpan.FromSeconds(20));
                builder.AddStation("Saftey Checks 1", TimeSpan.FromSeconds(20));
                builder.AddStation("Saftey Checks 2", TimeSpan.FromSeconds(20));
                builder.AddStation("Luggage 1", TimeSpan.FromSeconds(40));
                builder.AddStation("Luggage 2", TimeSpan.FromSeconds(40));
                builder.AddStation("Runway 1", TimeSpan.FromSeconds(30));
                builder.AddStation("Runway 2", TimeSpan.FromSeconds(30));
                builder.AddStation("Runway 3", TimeSpan.FromSeconds(30));

                foreach (var route in routes)
                {
                    builder.AddRoute(route);
                }

            }, "Ovda", imgUrl);
        }
        public Airport BuildOvdaAirport()
        {
            return BuildOvdaAirport(GetOvdaRoutes());
        }

    }
}
