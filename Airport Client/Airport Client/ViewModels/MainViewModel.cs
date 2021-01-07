using AirportClient.ViewModels;
using AirportClient.Views;
using ChatClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using WpfNetCoreMvvm.Models;
using WpfNetCoreMvvm.Services;

namespace WpfNetCoreMvvm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AppSettings settings;

        public readonly NavigationService navigationService;

        readonly Dictionary<string, UserControl> Views = new Dictionary<string, UserControl>();

        public RelayCommand HomeNavCommand { get; }
        public RelayCommand DatabaseCommand { get; }
        public RelayCommand VisualAirportCommand { get; }

        private UserControl _control;
        public UserControl Control
        {
            get { return _control; }
            set { Set(ref _control, value); }
        }

        public MainViewModel(IOptions<AppSettings> options, NavigationService navigationService)
        {
            settings = options.Value;
            this.navigationService = navigationService;

            //commands
            HomeNavCommand = new RelayCommand(HomeNav);
            DatabaseCommand = new RelayCommand(DatabaseNav);
            VisualAirportCommand = new RelayCommand(VisualAirport);


            //views
            Views.Add("Home", new HomeView());
            Views.Add("Database", new DatabaseView());
            Views.Add("VisualAirport", new VisualAirportView());

            //set the home user control.
            this.Control = Views["Home"];

            //subscribe event
            navigationService.ContentChanged += ContentChangedEventHandler;
        }

        private void ContentChangedEventHandler(object sender, UserControl control)
        {
            this.Control = control;
        }

        private void DatabaseNav()
        {
            this.navigationService.Navigate(Views["Database"]);
        }

        private void VisualAirport()
        {
            this.navigationService.Navigate(Views["VisualAirport"]);
        }

        private void HomeNav()
        {
            this.navigationService.Navigate(Views["Home"]);
        }
    }
}
