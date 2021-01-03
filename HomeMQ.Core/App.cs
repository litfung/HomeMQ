using MvvmCross.ViewModels;
using MvvmCross.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using HomeMQ.Core.ViewModels;

namespace HomeMQ.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();
        }
    }
}
