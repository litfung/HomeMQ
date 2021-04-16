using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public interface IRabbitConsumerViewModel
    {
        Task PollUpdates();
        //void Start();
        //void Consume();
    }
}
