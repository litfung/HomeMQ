using HomeMQ.Models;
using NetworkDeviceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Core.ViewModels
{
    public interface IRabbitControlViewModel<T> where T : IRabbitControlled
    {
        T Device { get; }
    }
}
