using Airport_Common.Args;
using Airport_Common.Models;
using Airport_DAL.Context;
using Airport_DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_DAL.Services
{
    public class LogService : ILogService
    {
        public async Task AddLog(Station commonStation, StationChangedEventArgs args)
        {
            using (var context = new DataContext())
            {
                DbStation station = context.Stations.First(station => station.Id == commonStation.Id);
                DbPlane plane = context.Planes.FirstOrDefault(plane => plane.FlightNumber == args.Plane.FlightNumber);
                if (plane != null)
                {
                    PlaneLog log = new PlaneLog()
                    {
                        Plane = plane,
                        Station = station,
                        Time = args.EventTime,
                        PlaneAction = args.PlaneAction
                    };

                    context.PlanesLog.Add(log);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
