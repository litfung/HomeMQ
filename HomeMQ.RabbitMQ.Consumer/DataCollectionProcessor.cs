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
            switch (data.Header.Type)
            {
                case "trace":
                    //var dbData = data.RfPowerTrace();
                    Debug.WriteLine($"trace {data.Payload.Data[0]}");
                    break;
                case "pulse_data":
                    //var dbData = data.RfPowerTrace();
                    Debug.WriteLine($"pulse_data {data.Payload.Data[0]}");
                    break;
                default:
                    break;
            }


            foreach (var item in data.Payload.Data)
            {
                if (item > -30.0)
                {
                    Debug.WriteLine(item);
                }
                
            }
            //Debug.WriteLine("Data Collection Processor process");
        }
        #endregion

    }
}
