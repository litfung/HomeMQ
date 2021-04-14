using BaseClasses;
using BaseViewModels;
using HomeMQ.Models;
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

        private ObservableCollection<string> sensors = new ObservableCollection<string>();
        public ObservableCollection<string> Sensors
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
        public RabbitControlStatusViewModel(IBackgroundHandler backgroundHandler, IBoontonPi device) : base(backgroundHandler)
        {
            Device = device;
            var tmp = new List<InterfaceInfoViewModel>();
            var tmpSensors = new List<string>();
            foreach (var item in Device.Interfaces)
            {
                tmp.Add(new InterfaceInfoViewModel(item));
            }

            foreach (var sensor in Device.Sensors)
            {
                tmpSensors.Add(sensor.SerialNumber);
            }

            Interfaces = new ObservableCollection<InterfaceInfoViewModel>(tmp);
            Sensors = new ObservableCollection<string>(tmpSensors);
        }
        #endregion

        #region Methods

        #endregion

    }
}
