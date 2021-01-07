using Airport_Common.Models;
using AirportClient.ViewModels;
using AirportClient.Views;
using ChatClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.ViewModels
{
    public class AirportViewModel : ViewModelBase
    {
        private AirportStatus airport;
        private readonly NavigationService navigationService;
        private readonly StationViewModel stationViewModel;

        public AirportStatus Airport { get => airport; set { Set(ref airport, value); } }

        public RelayCommand<Station> ViewCommand { get; }

        public AirportViewModel(NavigationService navigationService, StationViewModel stationViewModel)
        {
            ViewCommand = new RelayCommand<Station>(station => View(station));
            this.navigationService = navigationService;
            this.stationViewModel = stationViewModel;
        }

        private void View(Station station)
        {
            stationViewModel.SetProperties(station, Airport);
            navigationService.Navigate(new StationView());
        }
    }
}
