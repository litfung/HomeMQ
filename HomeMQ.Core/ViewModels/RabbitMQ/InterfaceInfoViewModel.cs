using HomeMQ.Models;
using MvvmCross.ViewModels;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Core.ViewModels
{
    public class InterfaceInfoViewModel : MvxViewModel
    {
        #region Fields
        private IInterfaceData info;
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
        #endregion

        #region Methods

        #endregion

    }
}
