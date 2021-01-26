using HomeMQ.RabbitMQ.Consumer;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Core.ViewModels
{
    public class RabbitConsumerViewModel : IRabbitConsumerViewModel
    {
        #region Fields
        private TopicConsumer consumer;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public RabbitConsumerViewModel(TopicConsumer topicConsumer)
        {
            consumer = topicConsumer;
        }
        #endregion

        #region Methods

        #endregion


    }
}
