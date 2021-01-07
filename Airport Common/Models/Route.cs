using Airport_Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class Route : IRoute
    {
        public int Id { get; set; }
        public int[][] RouteArray { get; set; }
        public int RouteLength => RouteArray.Length - 1;

        public string Name { get; set; }

        public Route() { }
        public Route(int[][] routeArr, string name)
        {
            this.RouteArray = routeArr;
            Name = name;
        }

        public IEnumerable<int> GetNextAvailableRoute(int stationNumber)
        {
            if (stationNumber > this.RouteLength || stationNumber < 0)
            {
                return new int[] { -1 };
            }
            else
            {
                return this.RouteArray[stationNumber];
            }
        }

    }
}
