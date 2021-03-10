using BaseClasses;
using BaseViewModels;
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
        private IRabbitControlled Device;
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


        #endregion

        #region Constructors
        public RabbitControlStatusViewModel(IMessenger iMessenger, IRabbitControlled device) : base(iMessenger)
        {
            Device = device;
            var tmp = new List<InterfaceInfoViewModel>();

            foreach (var item in Device.Interfaces)
            {
                tmp.Add(new InterfaceInfoViewModel(item));
            }

            Interfaces = new ObservableCollection<InterfaceInfoViewModel>(tmp);
        }
        #endregion

        #region Methods

        #endregion

    }
}
