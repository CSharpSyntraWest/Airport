using Airport_Common.Args;

namespace Airport_Server.Services
{
    public interface IUpdateClientService
    {
        void SendAirports(object sender, StationChangedEventArgs args);
    }
}