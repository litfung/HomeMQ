using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HomeMQ.RabbitMQ.Consumers
{
    public class DataCollectionProcessor : IDataCollectionProcessor
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public void Process(DataSaveMessage data)
        {
            Debug.WriteLine("Data Collection Processor process");
        }
        #endregion

    }
}
