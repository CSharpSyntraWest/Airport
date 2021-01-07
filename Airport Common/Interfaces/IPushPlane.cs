using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Interfaces
{
    public interface IPushPlane
    {
        /// <summary>
        /// Pushes the plane to the airport
        /// </summary>
        /// <param name="plane">plane to push</param>
        void PushPlane(Plane plane);
    }
}
