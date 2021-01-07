using Airport_Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Airport_Common.Models
{
    public class Plane : IRouteable
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public Color Color { get; set; }
        public string AirplaneType { get; set; }
        public int PassangersCount { get; set; }
        public string Country { get; set; }

        public Route Route { get; set; }

        public override string ToString()
        {
            return $"{FlightNumber}, {Route.Name}";
        }
    }
}
