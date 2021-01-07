using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Interfaces
{
    public interface IRoute
    {
        /// <summary>
        /// Returns a IEnumarable of numbers which represent the next avaliable station.
        /// <list type="bullet">
        ///     <item>
        ///         <description>if you pass 0 you will get the entry points</description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///             if you receive 0, it means the station number you pass as parameter is the last station for the 
        ///             route(could be multiple stations with exist)
        ///          </description>
        ///     </item>
        ///     <item>
        ///         <description>if you receive -1 it means there is no such station in the route.</description>
        ///     </item>
        ///     <item>
        ///         <description>if you receive empty IEnumarable, it means the station does not participate in the route(but do exist)</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="stationNumber">The station we are checking whats the next avaliable stations</param>
        /// <returns>List of next avaliable station numbers.</returns>
        IEnumerable<int> GetNextAvailableRoute(int stationNumber);

        string Name { get; }

    }
}
