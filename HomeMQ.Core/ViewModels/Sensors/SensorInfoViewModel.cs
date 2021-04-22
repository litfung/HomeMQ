using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class SensorInfoViewModel : MvxViewModel, ISensorInfoViewModel
    {
        #region Fields
        private ISensorData sensor;
        private IPiControlPublisher _commandPublisher;
        #endregion

        #region Properties

        public string SerialNumber
        {
            get { return sensor.SerialNumber; }
            set
            {
                if (sensor.SerialNumber != value)
                {
                    sensor.SerialNumber = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string Status
        {
            get { return sensor.Status; }
            set
            {
                if (sensor.Status != value)
                {
                    sensor.Status = value;
                    RaisePropertyChanged();
                }
            }
        }


        #endregion

        #region Commands
        //public IMvxCommand LoadFromConfigCommand { get; }
        #endregion

        #region Constructors
        public SensorInfoViewModel(ISensorData nSensor)//, IPiControlPublisher commandPublisher)
        {
            sensor = nSensor;
            //_commandPublisher = commandPublisher;
            //LoadFromConfigCommand = new MvxAsyncCommand(OnLoadFromConfig);
        }


        #endregion

        #region Methods
        //private Task OnLoadFromConfig()
        //{
        //    _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonLoadFromConfig(sensor.SerialNumber, "help.json"), "rasp.control.all"));
        //    return Task.CompletedTask;
        //}
        #endregion

    }
}
