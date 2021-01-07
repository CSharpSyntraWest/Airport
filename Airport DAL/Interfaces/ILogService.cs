using Airport_Common.Args;
using Airport_Common.Models;
using System.Threading.Tasks;

namespace Airport_DAL.Services
{
    public interface ILogService
    {
        Task AddLog(Station commonStation, StationChangedEventArgs args);
    }
}