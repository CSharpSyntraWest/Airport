using Airport_Common.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class CommonPlaneLog
    {
        public int Id { get; set; }
        public Plane Plane { get; set; }
        public Station Station { get; set; }
        public DateTime Time { get; set; }
        public PlaneAction PlaneAction { get; set; }
    }
}
