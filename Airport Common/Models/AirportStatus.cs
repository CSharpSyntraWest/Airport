using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class AirportStatus
    {
        public int Id { get; set; }
        public IEnumerable<Station> Stations { get; set; }
        public string Name { get; set; }
        public string ImageUrl{ get; set; }
        public List<Route> Routes{ get; set; }

        public AirportStatus() { }

        public AirportStatus(IEnumerable<Station> stations, string name)
        {
            Stations = stations;
            this.Name = name;
        }
        public AirportStatus(IEnumerable<Station> stations, string name, string imageUrl) 
            : this(stations,name)
        {
            this.ImageUrl = imageUrl;
        }

        public AirportStatus(IEnumerable<Station> stations, string name, string imageUrl, List<Route> routes)
            : this(stations, name, imageUrl)
        {
            this.Routes = routes;
        }
    }
}
