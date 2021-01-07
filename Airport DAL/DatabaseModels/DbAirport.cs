using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class DbAirport
    {
        [Key]
        public int Id{ get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<DbRoute> Routes { get; set; }
        public ICollection<DbStation> Stations{ get; set; }
    }
}
