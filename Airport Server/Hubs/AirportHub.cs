using Airport_Common.Models;
using Airport_DAL.Services;
using Airport_Server.Services;
using Airport_Simulator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Hubs
{
    public class AirportHub : Hub
    {
        private readonly UpdateClientService updateClientService;

        public AirportHub(UpdateClientService updateClientService)
        {
            this.updateClientService = updateClientService;
        }

        public override Task OnConnectedAsync()
        {
            this.updateClientService.SendAirports(null, null);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
