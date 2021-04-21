using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using HomeMQ.Models;
using HomeMQ.RabbitMQ.Publishers;
using MvvmCross.Commands;
using RabbitMQ.Control.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeMQ.Core.ViewModels
{
    public class RabbitControlOverviewViewModel : BaseViewModel
    {
        #region Fields
        private IRabbitControlledManager _deviceManager;
        private IPiControlPublisher _commandPublisher;
        private CancellationToken statusToken;
        #endregion

        #region Properties

        public ObservableCollection<IBoontonPiControlViewModel> BoontonPis { get; set; }
        #endregion

        #region Commands
        //public IMvxCommand StartPi1Command { get; }
        public IMvxCommand StartAllPisCommand { get; }
        //public IMvxCommand StopPi1Command { get; }
        public IMvxCommand StopAllPisCommand { get; }
        public IMvxCommand BoontonStartupCommand { get; }
        public IMvxCommand BoontonCloseSensorsCommand { get; }
        public IMvxCommand BoontonResetSensorsCommand { get; }
        #endregion

        #region Constructors
        public RabbitControlOverviewViewModel(IBackgroundHandler backgroundHandler, IRabbitControlledManager deviceManager, IPiControlPublisher commandPublisher) : base(backgroundHandler)
        {
            _deviceManager = deviceManager;
            _commandPublisher = commandPublisher;

            //StartPi1Command = new MvxAsyncCommand(OnStartPi1);
            StartAllPisCommand = new MvxAsyncCommand(OnStartAllPis);
            //StopPi1Command = new MvxAsyncCommand(OnStopPi1);
            StopAllPisCommand = new MvxAsyncCommand(OnStopAllPis);
            BoontonStartupCommand = new MvxAsyncCommand(OnBoontonStartup);
            BoontonCloseSensorsCommand = new MvxAsyncCommand(OnBoontonCloseSensors);
            BoontonResetSensorsCommand = new MvxAsyncCommand(OnBoontonResetSensors);
        }
        #endregion

        #region Methods
        private Task OnStartPi1()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StartPoll(), "rasp.control.pi1"));
            return Task.CompletedTask;
        }

        private Task OnStartAllPis()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StartPoll(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnStopPi1()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StopPoll(), "rasp.control.pi1"));
            return Task.CompletedTask;
        }

        private Task OnStopAllPis()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new StopPoll(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnBoontonStartup()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonStartup(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnBoontonCloseSensors()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonCloseSensors(), "rasp.control.all"));
            return Task.CompletedTask;
        }

        private Task OnBoontonResetSensors()
        {
            _commandPublisher.AddMessage(new RabbitControlMessage(new BoontonResetSensors(), "rasp.control.all"));
            return Task.CompletedTask;
        }
        #endregion

    }
}
