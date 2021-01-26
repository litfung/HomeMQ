using MvvmCross.ViewModels;
using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using HomeMQ.Core.ViewModels;
using MvvmCross;
using WiznetControllers;
using DeviceManagers;
using WPFMessageBox;
using RabbitMqManagers;

namespace HomeMQ.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton(() => WPFUserDialogs.Instance);
            Mvx.IoCProvider.RegisterSingleton(() => WiznetManager.Instance);
            Mvx.IoCProvider.RegisterSingleton(() => MQConnectionManager.Instance);
            RegisterAppStart<MainViewModel>();
        }
    }
}
