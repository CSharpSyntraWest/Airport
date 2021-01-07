using Airport_Common.Models;
using AirportClient.Views;
using ChatClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportClient.ViewModels
{
    public class PlaneViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private Plane plane;

        public Plane Plane { get => plane; set => Set(ref plane, value); }
        private Station station;
        public Station Station { get => station; set => Set(ref station, value); }
        public AirportStatus Airport { get; set; }
        public RelayCommand GoBackCommand { get; set; }

        public PlaneViewModel(NavigationService navigationService)
        {
            this.GoBackCommand = new RelayCommand(GoBack);
            this.navigationService = navigationService;
        }

        private void GoBack()
        {
            this.navigationService.Navigate(new StationView());
        }

        public void SetProperties(Plane plane, Station station, AirportStatus airport)
        {
            this.Plane = plane;
            this.Station = station;
            this.Airport = airport;
        }
    }
}
