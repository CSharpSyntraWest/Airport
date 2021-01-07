using Airport_Common.Args;
using System;
using System.ComponentModel.DataAnnotations;

namespace Airport_DAL.DatabaseModels
{
    public class PlaneLog
    {
        [Key]
        public int Id{ get; set; }
        public DbPlane Plane { get; set; }
        public DbStation Station { get; set; }
        public DateTime Time { get; set; }
        public PlaneAction PlaneAction{ get; set; }
    }
}
