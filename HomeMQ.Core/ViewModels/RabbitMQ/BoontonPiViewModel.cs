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
    public abstract class BoontonPiViewModel : BaseViewModel, IBoontonPiControlViewModel
    {
        
        #region Fields
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

        private ObservableCollection<ISensorInfoViewModel> sensors = new ObservableCollection<ISensorInfoViewModel>();
        public ObservableCollection<ISensorInfoViewModel> Sensors
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
        protected BoontonPiViewModel(IBackgroundHandler backgroundHandler, IBoontonPi device) : base(backgroundHandler)
        {
            Device = device;
            
            foreach (var item in Device.Interfaces)
            {
                Interfaces.Add(new InterfaceInfoViewModel(item));
            }

            
        }
        #endregion

        #region Methods

        #endregion
    }
}