using Airport_Common.Args;
using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Server.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Services.DALServices
{
    public class DALService : IDALService
    {
        private readonly IAirportDataService dataService;
        private readonly IConverterProvider converterProvider;
        private readonly ILogService logService;

        public DALService(IAirportDataService dataService, IConverterProvider converterProvider, ILogService logService)
        {
            this.dataService = dataService;
            this.converterProvider = converterProvider;
            this.logService = logService;
        }

        public void UpdateDatabase(object sender, StationChangedEventArgs args)
        {
            if (sender != null && args != null)
            {
                Station station = (Station)sender;
                DbStation dbStation = this.converterProvider.CommonToDb.ConvertStation(station);

                this.dataService.UpdateStation(dbStation);
            }
        }

        public async void AddLogDatabase(object sender, StationChangedEventArgs args)
        {
            Station station = (Station)sender;
            if (sender != null && args != null)
            {
                await this.logService.AddLog(station, args);
            }
        }

        public async Task AddAiportToDatabaseAsync(IAirport airport)
        {
            DbAirport dbAirport = this.converterProvider.LogicDatabase.ConvertAirport(airport);
            await this.dataService.AddAirport(dbAirport);
        }

        public IEnumerable<DbAirport> GetAirports()
        {
            return this.dataService.GetAirports();
        }

        public int GetIdCount()
        {
            return this.dataService.GetIdCount();
        }
    }
}
