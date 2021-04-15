using BaseClasses;
using BaseViewModels;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HomeMQ.Core.ViewModels
{
    public class RabbitControlStatusViewModel : BaseViewModel, IRabbitControlViewModel
    {
        #region Fields
        private IBoontonPi Device;
        private IPiControlPublisher _commandPublisher;
        #endregion

        #region Properties

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
                Sensors.Add(new SensorInfoViewModel());// sensor, _commandPublisher));
            }
            //Interfaces.Clear();
            //Sensors.Clear();
            //Interfaces = new ObservableCollection<InterfaceInfoViewModel>(tmp);
            //Sensors = new ObservableCollection<SensorInfoViewModel>(tmpSensors);
            
        }

        
        #endregion

        #region Methods

        #endregion

    }
}
