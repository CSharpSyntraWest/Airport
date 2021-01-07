using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_Common.Args;
using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Server.Converter;
using Airport_Server.Services.DALServices;
using Airport_Simulator;
using Plane_Maker.Services;

namespace Airport_Server.Services
{
    public enum AirportLoader
    {
        CreateAndLoad,
        Load
    }
    public class ServerService : IServerService
    {
        private readonly IAirportManager airportManager;
        private readonly IConverterProvider converterProvider;
        private readonly IDALService dalService;

        public event StationEvent ChangeInStateEvent;
        public ServerService(IConverterProvider converterProvider, IDALService dalService,
            AirportLoader airportLoader = AirportLoader.Load)
        {
            this.airportManager = new AirportManager();
            this.converterProvider = converterProvider;
            this.dalService = dalService;

            if (airportLoader == AirportLoader.CreateAndLoad)
            {
                CreateAirports();
            }

            ChangeInStateEvent += dalService.UpdateDatabase;
            ChangeInStateEvent += dalService.AddLogDatabase;
        }

        public virtual void AssignPlaneMakers()
        {
            IAirport benGurion = this.airportManager.GetAirport("Ben Gurion");
            benGurion.ChangeInState += RaiseChangeInStateEvent;
            IAirport ovda = this.airportManager.GetAirport("Ovda");
            ovda.ChangeInState += RaiseChangeInStateEvent;

            IdManager.Id = dalService.GetIdCount();

            IPlaneMaker planeMaker = new PlaneMaker(benGurion);
            planeMaker.ConfigureTimer(TimeSpan.FromSeconds(15));
            planeMaker.StartTimer();

            IPlaneMaker ovdaPlaneMaker = new OvdaPlaneMaker(ovda, ovda.Routes.ToArray());
            ovdaPlaneMaker.ConfigureTimer(TimeSpan.FromSeconds(25));
            ovdaPlaneMaker.StartTimer();
        }

        public void LoadFromDb()
        {
            IEnumerable<DbAirport> dbAirports = dalService.GetAirports();

            if (!dbAirports.Any() || dbAirports == null)
            {
                throw new Exception("You did not load any airport, if you meant that please catch this exception," +
                    " else change in the Startup the AirportLoader parameter(in the LogicService) to AirportLoader.CreateAndLoad instead of AirportLoader.Load");
            }

            IEnumerable<IAirport> airports = this.converterProvider.LogicDatabase.ConvertAirports(dbAirports);
            this.airportManager.AddAirports(airports);

            this.ChangeInStateEvent(null, null);
        }

        public virtual async void CreateAirports()
        {
            AirportBuilder builder = new AirportBuilder();

            Airport benGurion = builder.BuildBenGurionAirport();
            Airport ovda = builder.BuildOvdaAirport();

            await this.dalService.AddAiportToDatabaseAsync(benGurion);
            await this.dalService.AddAiportToDatabaseAsync(ovda);
        }

        public IEnumerable<IAirport> GetAirports()
        {
            return this.airportManager.GetAirports();
        }

        private void RaiseChangeInStateEvent(object sender, StationChangedEventArgs args)
        {
            ChangeInStateEvent?.Invoke(sender, args);
        }
    }
}
