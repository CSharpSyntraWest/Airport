

using Airport_Common.Models;
using System;
using System.Collections.Generic;

namespace WpfNetCoreMvvm.Services
{
    public interface IConnectionService
    {
        event EventHandler<IEnumerable<AirportStatus>> ReceiveAirports;
        event EventHandler<IEnumerable<CommonPlaneLog>> ReceiveLogs;
        event EventHandler<string> ErrorOccured;
        void Connect();
    }
}