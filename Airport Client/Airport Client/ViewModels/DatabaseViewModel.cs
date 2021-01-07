using Airport_Common.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfNetCoreMvvm.Services;

namespace AirportClient.ViewModels
{
    public class DatabaseViewModel : ViewModelBase
    {
        private ObservableCollection<CommonPlaneLog> logs;
        public ObservableCollection<CommonPlaneLog> Logs { get => logs; set => Set(ref logs, value); }

        private readonly IConnectionService connectionService;

        public DatabaseViewModel(IConnectionService connectionService)
        {
            this.connectionService = connectionService;
            this.connectionService.ReceiveLogs += ReceiveLogsEventArgs;
        }

        private void ReceiveLogsEventArgs(object sender, IEnumerable<CommonPlaneLog> e)
        {
            Logs = new ObservableCollection<CommonPlaneLog>(e);
        }
    }
}
