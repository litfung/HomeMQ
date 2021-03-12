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
            //Mvx.IoCProvider.RegisterSingleton<IMessenger>(new Messenger());
            //Mvx.IoCProvider.RegisterSingleton<ILogManager>(new LogManager());
            //Mvx.IoCProvider.RegisterSingleton<IStateManager>(new StateManager());
            //Mvx.IoCProvider.RegisterSingleton<IBackgroundHandler>(new SimpleBackgroundHandler());
            //Mvx.IoCProvider.RegisterSingleton<IMQConnectionManager>(new MQConnectionManager());
            //Mvx.IoCProvider.RegisterSingleton< IRabbitControlledManager>(new RabbitControlledDeviceManager());
            //Mvx.IoCProvider.RegisterType<IMasterControlProcessor, MasterControlProcessor>();
            //Mvx.IoCProvider.RegisterSingleton<IWiznetManager>(new WiznetManager());
            RegisterAppStart<MainViewModel>();
        }
    }
}
