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
    public class DataServiceHelper
    {
        private readonly DataContext dataContext;

        public DataServiceHelper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<DbPlane> GetOrCreatePlaneAsync(DbPlane planeDetails)
        {
            DbPlane databasePlane = await GetPlaneAsync(planeDetails.FlightNumber);

            if (databasePlane == null) //if plane does not exist in database, create a new one and return it.
            {
                dataContext.Planes.Add(planeDetails);
                await dataContext.SaveChangesAsync();

                databasePlane = await GetPlaneAsync(databasePlane.FlightNumber);
            }

            return databasePlane;
        }

        public DbPlane GetOrCreatePlane(DbPlane planeDetails)
        {
            DbPlane databasePlane = GetPlane(planeDetails.FlightNumber);

            if (databasePlane == null) //if plane does not exist in database, create a new one and return it.
            {
                dataContext.Planes.Add(planeDetails);
                dataContext.SaveChanges();

                databasePlane = GetPlane(planeDetails.FlightNumber);
            }

            return databasePlane;
        }

        public async Task<DbPlane> GetPlaneAsync(string flightNumber)
        {
            return await dataContext.Planes.SingleOrDefaultAsync(p => p.FlightNumber == flightNumber);
        }

        public DbPlane GetPlane(string flightNumber)
        {
            return dataContext.Planes.SingleOrDefault(p => p.FlightNumber == flightNumber);
        }
    }
}
