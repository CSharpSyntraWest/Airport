using Airport_Logic;
using Airport_Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Services
{
    public class AirportManager : IAirportManager
    {
        private readonly List<IAirport> airports;

        public AirportManager()
        {
            this.airports = new List<IAirport>();
        }

        public void AddAirport(IAirport airport)
        {
            this.airports.Add(airport);
        }

        public IAirport GetAirport(int id)
        {
            return this.airports.FirstOrDefault(a => a.Id == id);
        }

        public IAirport GetAirport(string name)
        {
            return this.airports.FirstOrDefault(a => a.Name == name);
        }

        public IEnumerable<IAirport> GetAirports()
        {
            return airports;
        }

        public void AddAirports(IEnumerable<IAirport> airports)
        {
            foreach (var airport in airports)
            {
                AddAirport(airport);
            }
        }

    }


}
