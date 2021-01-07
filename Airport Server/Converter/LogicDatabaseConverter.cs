using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using Airport_Logic;
using Airport_Server.Services;
using System.Collections.Generic;

namespace Airport_Server.Converter
{
    public class LogicDatabaseConverter
    {
        private readonly CommonToDbConverter commonToDbConvert;
        private readonly LogicToCommonConverter logicToCommon;

        public LogicDatabaseConverter(CommonToDbConverter commonToDbConvert, LogicToCommonConverter logicToCommon)
        {
            this.commonToDbConvert = commonToDbConvert;
            this.logicToCommon = logicToCommon;
        }

        public DbAirport ConvertAirport(IAirport airport)
        {
            AirportStatus commonAirport = this.logicToCommon.ConvertAirport(airport);
            return this.commonToDbConvert.ConvertAirport(commonAirport);
        }

        public IEnumerable<DbAirport> ConvertAirports(IEnumerable<IAirport> airports)
        {
            foreach (var airport in airports)
            {
                yield return ConvertAirport(airport);
            }
        }

        public Airport ConvertAirport(DbAirport dbAirport)
        {
            AirportStatus commonAirport = this.commonToDbConvert.ConvertAirport(dbAirport);
            return this.logicToCommon.ConvertAirport(commonAirport);
        }

        public IEnumerable<Airport> ConvertAirports(IEnumerable<DbAirport> dbAirports)
        {
            foreach (var airport in dbAirports)
            {
                yield return ConvertAirport(airport);
            }
        }
    }
}
