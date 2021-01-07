using Airport_Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WpfNetCoreMvvm.Models;

namespace WpfNetCoreMvvm.Services
{
    public class ConnectionService : IConnectionService
    {
        private HubConnection connection;
        private readonly IOptions<AppSettings> settings;

        public event EventHandler<IEnumerable<AirportStatus>> ReceiveAirports;
        public event EventHandler<IEnumerable<CommonPlaneLog>> ReceiveLogs;

        public event EventHandler<string> ErrorOccured;

        public ConnectionService(IOptions<AppSettings> settings)
        {
            this.settings = settings;
            InitialConnection();
            Connect();
        }

        private void InitialConnection()
        {
            string hubUrl = this.settings.Value.HubUrl;
            connection = new HubConnectionBuilder()
               .WithUrl(hubUrl)
               .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        public async void Connect()
        {
            SetSingalRFuncs();
            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                ErrorOccured(this, ex.Message);
            }
        }

        private void SetSingalRFuncs()
        {
            connection.On<string>("ReceiveAirports", (jsonAirports) =>
            {
                IEnumerable<AirportStatus> airports = JsonConvert.DeserializeObject<IEnumerable<AirportStatus>>(jsonAirports,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                ReceiveAirports?.Invoke(this, airports);
            });

            connection.On<string>("ReceiveLogs", (jsonLogs) =>
            {
                IEnumerable<CommonPlaneLog> logs = JsonConvert.DeserializeObject<IEnumerable<CommonPlaneLog>>(jsonLogs,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                ReceiveLogs?.Invoke(this, logs);
            });
        }
    }
}
