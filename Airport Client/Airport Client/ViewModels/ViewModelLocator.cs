using AirportClient.ViewModels;
using ChatClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace WpfNetCoreMvvm.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();

        public HomeViewModel HomeViewModel => App.ServiceProvider.GetRequiredService<HomeViewModel>();
        public DatabaseViewModel DatabaseViewModel => App.ServiceProvider.GetRequiredService<DatabaseViewModel>();
        public VisualAirportViewModel VisualAirportViewModel => App.ServiceProvider.GetRequiredService<VisualAirportViewModel>();
        public AirportViewModel AirportViewModel => App.ServiceProvider.GetRequiredService<AirportViewModel>();
        public StationViewModel StationViewModel => App.ServiceProvider.GetRequiredService<StationViewModel>();
        public PlaneViewModel PlaneViewModel => App.ServiceProvider.GetRequiredService<PlaneViewModel>();

    }
}
