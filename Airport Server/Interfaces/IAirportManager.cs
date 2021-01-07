using Airport_Logic;
using System.Collections.Generic;

namespace Airport_Server.Services
{
    public interface IAirportManager
    {
        void AddAirport(IAirport airport);
        void AddAirports(IEnumerable<IAirport> airports);
        IAirport GetAirport(int id);
        IAirport GetAirport(string name);
        IEnumerable<IAirport> GetAirports();
    }
}