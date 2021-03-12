using Autofac;
using BaseClasses;
using DeviceManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WiznetControllers;

namespace HomeMQ.ConsoleApp
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            //Manual mode

            //builder.RegisterType<Application>(); //Whenever you ask for an Application it creates a new one
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<Messenger>().As<IMessenger>();     //Singleton??
            builder.RegisterType<WiznetManager>().As<IWiznetManager>();

            //Automated config
            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(BaseClasses))).As(t => t.GetInterfaces().FirstOrDefault())
            //    //.Where(t => t.Namespace.Contains())
            return builder.Build();
        }
    }
}
