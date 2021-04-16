using HomeMQ.Models;
using MvvmCross.ViewModels;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Core.ViewModels
{
    public class InterfaceInfoViewModel : MvxViewModel, IDisposable
    {
        #region Fields
        private IInterfaceData info;
        private bool disposedValue;
        #endregion

        #region Properties

        public string Name
        {
            get { return info.Name; }
            set
            {
                if (info.Name != value)
                {
                    info.Name = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string MAC
        {
            get { return info.MAC; }
            set
            {
                if (info.MAC != value)
                {
                    info.MAC = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string IPAddress
        {
            get { return info.IPAddress; }
            set
            {
                if (info.IPAddress != value)
                {
                    info.IPAddress = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors
        public InterfaceInfoViewModel(IInterfaceData nInfo)
        {
            info = nInfo;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                info = null;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~InterfaceInfoViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Methods

        #endregion

    }
}
