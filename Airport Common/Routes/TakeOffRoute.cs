using Airport_Common.Interfaces;
using Airport_Common.Models;
using System.Collections.Generic;

namespace Airport_Common.Routes
{
    public class TakeOffRoute : Route
    {
        public TakeOffRoute()
        {
            base.RouteArray = new int[][]
            {
                new int[] { 1,2 }, //0
                new int[] { 3, 4 }, //1
                new int[] { 3, 4 }, //2
                new int[] { 5, 6, 7 }, //3
                new int[] { 5, 6, 7 }, //4
                new int[] { 8 }, //5
                new int[] { 8 }, //6
                new int[] { 8 }, //7
                new int[] { 0 }, //8
            };

            base.Name = "TakeOff";
        }

        //public IEnumerable<int> GetNextAvailableRoute(int stationNumber)
        //{
        //    switch (stationNumber)
        //    {
        //        case 0:
        //            yield return 1;
        //            break;
        //        case 1:
        //            yield return 2;
        //            break;
        //        case 2:
        //            yield return 3;
        //            break;
        //        case 3:
        //            yield return 4;
        //            yield return 5;
        //            break;
        //        case 4:
        //        case 5:
        //            yield return 6;
        //            break;
        //        case 6:
        //            yield return 7;
        //            yield return 8;
        //            break;
        //        case 7:
        //        case 8:
        //            yield return 0;
        //            break;
        //        default:
        //            yield return -1;
        //            break;
        //    }
        //}
    }
}
