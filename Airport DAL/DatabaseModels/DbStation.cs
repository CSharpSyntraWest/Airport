using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class DbStation
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public int StationNumber{ get; set; }
        public string StationName { get; set; }
        public DbPlane CurrentPlane { get; set; }
        public ICollection<DbPlane> WaitingLine { get; set; }
        //public IEnumerable<DbStation> ConnectedStations { get; set; }
        public TimeSpan WaitingTime { get; set; }
        

        public DbAirport Airport { get; set; }
    }
}
