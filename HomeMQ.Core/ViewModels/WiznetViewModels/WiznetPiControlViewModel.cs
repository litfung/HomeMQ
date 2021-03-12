using BaseClasses;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class WiznetPiControlViewModel : BaseWiznetViewModel
    {
        #region Fields
        private int piNumber;
        private IWiznetPiControl Wiznet;
        #endregion

        #region Properties
        //public string Title => $"Pi {piNumber} Control";

        private bool powerOn;
        public bool PowerOn
        {
            get { return powerOn; }
            set
            {
                if (powerOn != value)
                {
                    powerOn = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string piError;
        public string PiError
        {
            get { return piError; }
            set
            {
                if (piError != value)
                {
                    piError = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool piErrorLED;
        public bool PiErrorLED
        {
            get { return piErrorLED; }
            set
            {
                if (piErrorLED != value)
                {
                    piErrorLED = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string piIP;
        public string PiIP
        {
            get { return piIP; }
            set
            {
                if (piIP != value)
                {
                    piIP = value;
                    RaisePropertyChanged();
                }
            }
        }





        #endregion

        #region Commands
        public IMvxCommand RaspberryPowerCommand { get; set; }
        #endregion

        #region Constructors
        public WiznetPiControlViewModel(IBackgroundHandler backgroundHandler, IWiznetPiControl wiz, int pi) : base(backgroundHandler, wiz)
        {
            //Func<int, bool> CanPiPowerFunc = new Func<int, bool>(CanPiPower());
            piNumber = pi;
            Wiznet = wiz;
            //RaspberryErrorCommand = new MvxAsyncCommand(OnRaspberryError, CanSend, allowConcurrentExecutions: true);
            //RaspberryIPCommand = new MvxAsyncCommand(OnRaspberryIP, CanSend, allowConcurrentExecutions: true);
            RaspberryPowerCommand = new MvxAsyncCommand(OnRaspberryPower, CanSend, allowConcurrentExecutions: true);
        }
        #endregion

        #region Methods

        //public override async Task PollAllRegisters()
        //{
        //    var toLog = false;
        //    PowerOn = WiznetExtensions.CheckOnOff(await Wiznet.GetRaspberryPower(piNumber, toLog));
        //    PiError = await Wiznet.GetRaspberryError(piNumber, toLog: toLog);
        //    if (int.TryParse(PiError, out var data))
        //    {
        //        if ((data & 0x7fff) != 0)
        //        {
        //            PiErrorLED = true;
        //        }
        //        else
        //        {
        //            PiErrorLED = false;
        //        }
        //    }

        //    PiIP = await Wiznet.GetRaspberryIP(piNumber, toLog: toLog);
        //    await base.PollAllRegisters();
        //}

        private async Task OnRaspberryPower()
        {
            await Wiznet.SetRaspberryPower(piNumber, PowerOn);
            if (!PowerOn)
            {
                await Wiznet.SetRaspberryIP(piNumber, "0.0.0.0");
            }
        }

        #endregion

    }
}
