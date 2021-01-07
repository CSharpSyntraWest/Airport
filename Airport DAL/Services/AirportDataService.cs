using Airport_Common.Models;
using Airport_DAL.Context;
using Airport_DAL.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_DAL.Services
{
    public class AirportDataService : IAirportDataService
    {
        private readonly object key = new object();
        public DataContext Context { get; set; }
        private readonly DataServiceHelper DataHelper;

        public AirportDataService()
        {
            this.Context = new DataContext();
            this.DataHelper = new DataServiceHelper(this.Context);
        }

        public async Task AddAirport(DbAirport airport)
        {
            airport.Stations.OrderBy(s => s.StationNumber);
            Context.Airports.Add(airport);
            await Context.SaveChangesAsync();
        }

        public IEnumerable<DbAirport> GetAirports()
        {
            //returns the full airport, with all the stations and routes and planes.
            return this.Context.Airports.Include(a => a.Routes)
                .Include(a => a.Stations.OrderBy(s => s.StationNumber)).ThenInclude(s => s.CurrentPlane).ThenInclude(p => p.Route)
                .Include(a => a.Stations.OrderBy(s => s.StationNumber)).ThenInclude(s => s.WaitingLine).ThenInclude(p => p.Route);
        }

        public void UpdateStation(DbStation stationDetails)
        {
            //code will reach when: 1. enter station, 2. exist station 3. add to waiting list
            Task.Run(() =>
            {
                lock (key)
                {
                    DbStation foundStation = GetStation(stationDetails.Id);
                    Context.Entry(foundStation).Property(s => s.WaitingTime).CurrentValue = stationDetails.WaitingTime;

                    ICollection<DbPlane> dbWaitingLine = new List<DbPlane>();
                    foreach (var plane in stationDetails.WaitingLine)
                    {
                        DbPlane dbPlane = this.DataHelper.GetOrCreatePlane(plane);
                        dbWaitingLine.Add(dbPlane);
                    }

                    DbPlane foundPlane = null;
                    if (stationDetails.CurrentPlane != null)
                    {
                        foundPlane = this.DataHelper.GetOrCreatePlane(stationDetails.CurrentPlane);
                    }

                    Context.Entry(foundStation).Collection(s => s.WaitingLine).CurrentValue = dbWaitingLine;
                    Context.Entry(foundStation).Reference(s => s.CurrentPlane).CurrentValue = foundPlane;

                    Context.SaveChanges();
                }
            });
        }

        public int GetIdCount()
        {
            return this.Context.Planes.Count();
        }

        public async void AddPlane(DbPlane plane)
        {
            this.Context.Planes.Add(plane);
            await this.Context.SaveChangesAsync();
        }

        public async Task<DbPlane> GetPlane(string flightNumber)
        {
            return await this.Context.Planes.SingleOrDefaultAsync(p => p.FlightNumber == flightNumber);
        }
        public DbStation GetStation(int id)
        {
            return this.Context.Stations.SingleOrDefault(s => s.Id == id);
        }
        public async Task<DbStation> GetStationAsync(int id)
        {
            return await this.Context.Stations.SingleOrDefaultAsync(s => s.Id == id);
        }

        public IEnumerable<PlaneLog> GetLogs()
        {
            using (var context = new DataContext())
            {
                return context.PlanesLog
                    .Include(log => log.Station)
                    .Include(log => log.Plane).ThenInclude(plane => plane.Route).ToList();
            }
        }
    }
}
