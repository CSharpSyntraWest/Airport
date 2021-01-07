using Airport_Common.Models;

namespace Airport_Common_Interfaces
{
    public interface ICurrentPlane
    {
        Plane CurrentPlane { get; }
    }
}