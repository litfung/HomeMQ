using HomeMQ.Models;
using RabbitMQ.Client;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeMQ.RabbitMQ.Publishers
{
    public class PiControlPublisher : TopicPublisher, IPiControlPublisher
    {

        #region Fields
        //private Queue<ControlMessage>
        private CancellationTokenSource statusToken = new CancellationTokenSource();
        #endregion

        #region Properties
        #endregion

        #region Constructors
        //public PiControlPublisher(IConnection conn, MQConnectionManager rabbitConnectionManager, string exchange) : base(conn, exchange)
        //{
        //    var homeFactory = rabbitConnectionManager.FactoriesByName["home"];
        //    var connection = homeFactory.CreateConnection(clientProvidedName: "Master Controller");
        //    var _ = StatusPoll();
        //}

        public PiControlPublisher(IConnectionFactory factory, string exchange, string connectionName = null) : base(factory, exchange, connectionName)
        {
            //var homeFactory = rabbitConnectionManager.FactoriesByName["home"];
            //var connection = homeFactory.CreateConnection(clientProvidedName: connectionName);
            _ = StatusPoll();
        }
        #endregion

        #region Methods
        private async Task StatusPoll()
        {
            while (!statusToken.IsCancellationRequested)
            {
                AddMessage(new RabbitControlMessage(new StatusCheck(), "rasp.control.all"));
                await Task.Delay(1000);
            }
        }

        //private async Task StatusPoll()
        //{
        //    //await Publish(new ControlMessage(new StatusCheck()), "rasp.control.all");
        //    AddMessage(new RabbitControlMessage(new StatusCheck(), "rasp.control.all"));
        //    await Task.Delay(1000);
        //    await StatusPoll();
        //}

        #endregion


    }
}
