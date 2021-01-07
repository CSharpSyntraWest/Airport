using Airport_Common.Args;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic
{
    internal interface IRaiseChangeInStateEvent
    {
        void RaiseChangeInStateEvent(object sender, StationChangedEventArgs args);
    }
}