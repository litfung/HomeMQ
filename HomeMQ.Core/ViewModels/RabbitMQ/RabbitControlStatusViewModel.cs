using BaseClasses;
using BaseViewModels;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class RabbitControlStatusViewModel : BaseViewModel, IRabbitControlViewModel<IBoontonPi>
    {
        #region Fields
        private IPiControlPublisher _commandPublisher;
        #endregion

        #region Properties

        public IBoontonPi Device { get; private set; }

        public string Hostname
        {
            get { return Device.Hostname; }
            set
            {
                if (Device.Hostname != value)
                {
                    Device.Hostname = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string Status
        {
            get { return Device.Status; }
            set
            {
                if (Device.Status != value)
                {
                    Device.Status = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Timestamp
        {
            get { return Device.LastUpdateTime.ToString(); }
            set
            {
                if (Device.Status != value)
                {
                    Device.Status = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<InterfaceInfoViewModel> interfaces = new ObservableCollection<InterfaceInfoViewModel>();
        public ObservableCollection<InterfaceInfoViewModel> Interfaces
        {
            get { return interfaces; }
            set
            {
                if (interfaces != value)
                {
                    interfaces = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<SensorInfoViewModel> sensors = new ObservableCollection<SensorInfoViewModel>();
        private bool disposedValue;

        public ObservableCollection<SensorInfoViewModel> Sensors
        {
            get { return sensors; }
            set
            {
                if (sensors != value)
                {
                    sensors = value;
                    RaisePropertyChanged();
                }
            }
        }

        




        #endregion

        #region Constructors
        public RabbitControlStatusViewModel(IBackgroundHandler backgroundHandler, IBoontonPi device, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            Device = device;
            _commandPublisher = commandPublisher;
            //var tmp = new List<InterfaceInfoViewModel>();
            //var tmpSensors = new List<SensorInfoViewModel>();
            foreach (var item in Device.Interfaces)
            {
                //tmp.Add(new InterfaceInfoViewModel(item));
                Interfaces.Add(new InterfaceInfoViewModel(item));
            }

            foreach (var sensor in Device.Sensors)
            {
                //tmpSensors.Add(new SensorInfoViewModel());// sensor, _commandPublisher));
                Sensors.Add(new SensorInfoViewModel(sensor, _commandPublisher));
            }
            //Interfaces.Clear();
            //Sensors.Clear();
            //Interfaces = new ObservableCollection<InterfaceInfoViewModel>(tmp);
            //Sensors = new ObservableCollection<SensorInfoViewModel>(tmpSensors);
            
        }

        public override async Task OnUpdateView()
        {
            await RaiseAllPropertiesChanged();
            await base.OnUpdateView();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var item in Interfaces)
                    {
                        item.Dispose();
                    }
                    Interfaces.Clear();
                    Interfaces = null;

                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                Device = null;
                _commandPublisher = null;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RabbitControlStatusViewModel()
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
