using Airport_Common.Interfaces;

namespace Airport_Common.Models
{
    /// <summary>
    /// Interface for represent a object that has route
    /// </summary>
    public interface IRouteable
    {
        Route Route { get; }
    }
}