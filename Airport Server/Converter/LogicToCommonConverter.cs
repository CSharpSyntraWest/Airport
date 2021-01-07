using Airport_Common.Models;
using Airport_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Converter
{
    public class LogicToCommonConverter
    {
        public AirportStatus ConvertAirport(IAirport airport)
        {
            IEnumerable<Station> stations = airport.GetStations();
            string name = airport.Name;
            string url = airport.ImageUrl;
            List<Route> routes = airport.Routes;

            return new AirportStatus(stations, name, url, routes);
        }

        public Airport ConvertAirport(AirportStatus commonAirport)
        {
           return Airport.RestoreAirport(commonAirport);
        }

        public IEnumerable<AirportStatus> ConvertAirports(IEnumerable<IAirport> airports)
        {
            foreach (var airport in airports)
            {
                yield return ConvertAirport(airport);
            }
        }
        public IEnumerable<Airport> ConvertAirports(IEnumerable<AirportStatus> commonAirports)
        {
            foreach (var airport in commonAirports)
            {
                yield return ConvertAirport(airport);
            }
        }
    }
}
