using Airport_Common.Models;
using ChatClient.Services;
using ChatClient.ViewModels;
using ChatClient.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfNetCoreMvvm.Services;

namespace AirportClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IConnectionService service;

        public NavigationService NavigationService { get; private set; }
        public AirportViewModel AirportViewModel { get; }
        public StationViewModel StationViewModel{ get; }
        public PlaneViewModel PlaneViewModel { get; }

        private ObservableCollection<AirportStatus> airports;

        public ObservableCollection<AirportStatus> Airports { get => airports; set { Set(ref airports, value); } }
        private Visibility loadingVisability;

        public Visibility LoadingVisability { get => loadingVisability; set => Set(ref loadingVisability, value); }
        public RelayCommand<AirportStatus> ViewDetailsCommand { get; }


        public HomeViewModel(IConnectionService connectionService, NavigationService navigationService, AirportViewModel airportViewModel, StationViewModel stationViewModel, PlaneViewModel planeViewModel)
        {
            this.service = connectionService;
            this.NavigationService = navigationService;
            this.AirportViewModel = airportViewModel;
            this.StationViewModel = stationViewModel;
            this.PlaneViewModel = planeViewModel;

            //assign events
            this.service.ReceiveAirports += ReceiveAirportsEventHandler;
            this.service.ErrorOccured += ErrorOccuredEventHandler;

            //assign commands.
            this.ViewDetailsCommand = new RelayCommand<AirportStatus>(airport => ViewDetails(airport));

            this.LoadingVisability = Visibility.Visible;
        }

        private void ErrorOccuredEventHandler(object sender, string error)
        {
            MessageBox.Show(error, $"Error Occured When Tried To Connect To Server!");
            if (this.LoadingVisability != Visibility.Collapsed)
            {
                this.LoadingVisability = Visibility.Visible;
            }

            RetryConnection();
        }

        private void RetryConnection()
        {
            Task.Run(() =>
            {
                Thread.Sleep(100);
                this.service.Connect();
            });
        }

        private void ViewDetails(AirportStatus selectedAirport)
        {
            NavigationService.Navigate(new AirportView());
            AirportViewModel.Airport = selectedAirport;
        }

        private void ReceiveAirportsEventHandler(object sender, IEnumerable<AirportStatus> AirportsArgs)
        {
            if(this.LoadingVisability != Visibility.Collapsed)
            {
                this.LoadingVisability = Visibility.Collapsed;
            }

            Airports = new ObservableCollection<AirportStatus>(AirportsArgs);

            AirportStatus airport = null;
            Station station = null;

            if (this.AirportViewModel.Airport != null)
            {
                airport = AirportsArgs.FirstOrDefault(airport => airport.Name == this.AirportViewModel.Airport.Name);
                this.AirportViewModel.Airport = airport;
            }

            if (this.StationViewModel.Station != null)
            {
                station = this.AirportViewModel.Airport.Stations
                   .FirstOrDefault(station => station.StationNumber == this.StationViewModel.Station.StationNumber);

                this.StationViewModel.Station = station;
                this.StationViewModel.Airport = airport;

            }

            //update Plane
            //if(this.PlaneViewModel.Plane != null)
            //{
            //    //find the plane and station to update the ui
            //    Station planeStation = airport.Stations.FirstOrDefault(s => s.CurrentPlane?.FlightNumber == this.PlaneViewModel.Plane.FlightNumber);
            //    this.PlaneViewModel.Plane = planeStation.CurrentPlane;
            //    this.PlaneViewModel.Station = planeStation;
            //};
        }
    }
}
