using MvvmCross.ViewModels;
using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using HomeMQ.Core.ViewModels;
using MvvmCross;
using WiznetControllers;
using DeviceManagers;
using RabbitMqManagers;
using HomeMQ.RabbitMQ.Consumer;
using HomeMQ.RabbitMQ.Publishers;
using HomeMQ.Managers;
using BaseClasses;

namespace HomeMQ.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            //Optional Dependencies
            //Mvx.IoCProvider.RegisterSingleton(() => new LogManager());
            //Mvx.IoCProvider.RegisterSingleton(() => new Messenger());
            RegisterAppStart<MainViewModel>();
        }
    }
}
