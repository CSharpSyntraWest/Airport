using Airport_Common.Args;
using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using Airport_DAL.Services;
using Airport_Server.Converter;
using Airport_Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Server.Services
{
    public class UpdateClientService : IUpdateClientService
    {
        private readonly IServerService serverService;
        private readonly IHubContext<AirportHub> hubContext;
        private readonly IConverterProvider converterProvider;
        private readonly IAirportDataService airportDataService;

        public UpdateClientService(IServerService serverService, IHubContext<AirportHub> hubContext, IConverterProvider converterProvider, IAirportDataService airportDataService)
        {
            this.serverService = serverService;
            serverService.ChangeInStateEvent += SendAirports;
            serverService.ChangeInStateEvent += SendLog;

            this.hubContext = hubContext;
            this.converterProvider = converterProvider;
            this.airportDataService = airportDataService;
            this.serverService.LoadFromDb();
            this.serverService.AssignPlaneMakers();
        }


        public async void SendAirports(object sender, StationChangedEventArgs args)
        {
            IEnumerable<AirportStatus> airports = this.converterProvider.LogicCommon
                .ConvertAirports(this.serverService.GetAirports());

            string jsonAirports = JsonConvert.SerializeObject(airports, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            await hubContext.Clients.All.SendAsync("ReceiveAirports", jsonAirports);
        }

        public async void SendLog(object sender, StationChangedEventArgs args)
        {
            IEnumerable<PlaneLog> dbLogs = this.airportDataService.GetLogs();
            if(dbLogs.Any(l => l.Plane == null))
            {
                //
            }

            IEnumerable<CommonPlaneLog> logs = this.converterProvider.CommonToDb.ConvertPlaneLogs(dbLogs);

            string jsonAirports = JsonConvert.SerializeObject(logs, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });


            await hubContext.Clients.All.SendAsync("ReceiveLogs", jsonAirports);
        }
    }
}
