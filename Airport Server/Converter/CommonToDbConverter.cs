using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Services
{
    public class CommonToDbConverter
    {
        public DbAirport ConvertAirport(AirportStatus commonAirport)
        {
            return new DbAirport()
            {
                Id = commonAirport.Id,
                Name = commonAirport.Name,
                Stations = ConvertStations(commonAirport.Stations).ToList(),
                ImageUrl = commonAirport.ImageUrl,
                Routes = ConvertRoutes(commonAirport.Routes),
            };
        }

        public AirportStatus ConvertAirport(DbAirport dbAirport)
        {
            return new AirportStatus()
            {
                Id = dbAirport.Id,
                ImageUrl = dbAirport.ImageUrl,
                Stations = ConvertStations(dbAirport.Stations),
                Name = dbAirport.Name,
                Routes = ConvertRoutes(dbAirport.Routes)
            };
        }

        public IEnumerable<DbAirport> ConvertAirports(IEnumerable<AirportStatus> commonAirports)
        {
            foreach (var airport in commonAirports)
            {
                yield return ConvertAirport(airport);
            }
        }

        public IEnumerable<AirportStatus> ConvertAirports(IEnumerable<DbAirport> dbAirports)
        {
            foreach (var airport in dbAirports)
            {
                yield return ConvertAirport(airport);
            }
        }

        private IEnumerable<Station> ConvertStations(IEnumerable<DbStation> stations)
        {
            foreach (var station in stations)
            {
                yield return ConvertStation(station);
            }
        }

        public IEnumerable<DbStation> ConvertStations(IEnumerable<Station> commonStations)
        {
            foreach (var commonStation in commonStations)
            {
                yield return ConvertStation(commonStation);

            }
        }

        public DbStation ConvertStation(Station commonStation)
        {
            return new DbStation()
            {
                Id = commonStation.Id,
                StationName = commonStation.StationName,
                StationNumber = commonStation.StationNumber,
                WaitingTime = commonStation.WaitingTime,
                CurrentPlane = ConvertPlane(commonStation.CurrentPlane),
                WaitingLine = new List<DbPlane>(ConvertPlanes(commonStation.WaitingLine)),
            };
        }

        internal IEnumerable<CommonPlaneLog> ConvertPlaneLogs(IEnumerable<PlaneLog> logs)
        {
            foreach (var log in logs)
            {
                yield return ConvertPlaneLog(log);
            }
        }

        private Station ConvertStation(DbStation station)
        {
            var commonStation = new Station()
            {
                Id = station.Id,
                StationName = station.StationName,
                StationNumber = station.StationNumber,
                CurrentPlane = ConvertPlane(station.CurrentPlane),
                WaitingTime = station.WaitingTime,
            };

            if (station.WaitingLine != null)
            {
                commonStation.WaitingLine = new ConcurrentQueue<Plane>(ConvertPlanes(station.WaitingLine));
            }

            return commonStation;
        }

        public DbPlane ConvertPlane(Plane plane)
        {
            if (plane == null)
            {
                return null;
            }

            return new DbPlane()
            {
                Id = plane.Id,
                AirplaneType = plane.AirplaneType,
                ColorARGB = plane.Color.ToArgb(),
                Country = plane.Country,
                FlightNumber = plane.FlightNumber,
                PassangersCount = plane.PassangersCount,
                Route = ConvertRoute(plane.Route)
            };
        }

        public Plane ConvertPlane(DbPlane dbPlane)
        {
            if (dbPlane != null)
            {
                return new Plane()
                {
                    Id = dbPlane.Id,
                    AirplaneType = dbPlane.AirplaneType,
                    Color = Color.FromArgb(dbPlane.ColorARGB),
                    Country = dbPlane.Country,
                    FlightNumber = dbPlane.FlightNumber,
                    PassangersCount = dbPlane.PassangersCount,
                    Route = ConvertRoute(dbPlane.Route),
                };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DbPlane> ConvertPlanes(IEnumerable<Plane> planes)
        {
            foreach (var plane in planes)
            {
                yield return ConvertPlane(plane);
            }
        }

        public IEnumerable<Plane> ConvertPlanes(IEnumerable<DbPlane> dbPlanes)
        {

            foreach (var dbplane in dbPlanes)
            {
                yield return ConvertPlane(dbplane);
            }
        }

        private ICollection<DbRoute> ConvertRoutes(List<Route> routes)
        {
            List<DbRoute> dbRoutes = new List<DbRoute>();

            if (routes != null)
            {
                foreach (var route in routes)
                {
                    dbRoutes.Add(ConvertRoute(route));
                }
            }
            return dbRoutes;
        }
        private List<Route> ConvertRoutes(ICollection<DbRoute> dbRoutes)
        {
            List<Route> routes = new List<Route>();

            foreach (var route in dbRoutes)
            {
                routes.Add(ConvertRoute(route));
            }

            return routes;
        }
        private DbRoute ConvertRoute(Route route)
        {
            List<List<int>> dbRouteArray = new List<List<int>>();

            foreach (var routeIds in route.RouteArray)
            {
                dbRouteArray.Add(routeIds.ToList());
            }

            return new DbRoute()
            {
                //Id = route.Id,
                Name = route.Name,
                RouteArray = dbRouteArray
            };
        }
        private Route ConvertRoute(DbRoute dbRoute)
        {
            if (dbRoute != null)
            {
                int[][] routeArr = new int[dbRoute.RouteArray.Count][];

                for (int i = 0; i < dbRoute.RouteArray.Count; i++)
                {
                    routeArr[i] = dbRoute.RouteArray[i].ToArray();
                }

                return new Route()
                {
                    //Id = dbRoute.Id,
                    Name = dbRoute.Name,
                    RouteArray = routeArr
                };
            }
            else
            {
                return null;
            }

        }

        public PlaneLog ConvertPlaneLog(CommonPlaneLog commonPlaneLog)
        {
            return new PlaneLog()
            {
                Id = commonPlaneLog.Id,
                Plane = ConvertPlane(commonPlaneLog.Plane),
                Station = ConvertStation(commonPlaneLog.Station),
                PlaneAction = commonPlaneLog.PlaneAction,
                Time = commonPlaneLog.Time
            };
        }

        public CommonPlaneLog ConvertPlaneLog(PlaneLog PlaneLog)
        {
            Station station = null;
            if (PlaneLog.Station != null)
            {
                station = ConvertStation(PlaneLog.Station);
            }
            return new CommonPlaneLog()
            {
                Id = PlaneLog.Id,
                Plane = ConvertPlane(PlaneLog.Plane),
                Station = station,
                PlaneAction = PlaneLog.PlaneAction,
                Time = PlaneLog.Time
            };
        }
    }
}
