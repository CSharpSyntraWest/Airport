using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class DbPlane
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FlightNumber { get; set; }
        public int ColorARGB { get; set; }
        public string AirplaneType { get; set; }
        public int PassangersCount { get; set; }
        public string Country { get; set; }
        public DbRoute Route{ get; set; } 
    }
}
