using BaseClasses;
using BaseViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class BaseWiznetViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Properties
        public IWiznetControl Wiznet { get; private set; }
        #endregion

        #region Constructors
        public BaseWiznetViewModel(IMessenger iMessenger, IWiznetControl wiz) : base(iMessenger)
        {
            Wiznet = wiz;

            
        }
        #endregion

        #region Methods

        #endregion

        //public async Task PollAllRegisters()
        //{
        //    messenger.Send(new LogUpdateMessage());
        //    await UpdateUIControlAccess();
        //}

        public virtual async Task Started()
        {
            
        }

        public bool CanSend()
        {
            if (Wiznet == null) { return false; }
            return Wiznet.IsConnected;// Wiznet.ClientStatus == ClientStatus.Connected;
        }

        public bool CanSendHexInt(string checkString)
        {
            if (CanSend())
            {
                if (string.IsNullOrEmpty(checkString)) { return false; }

                if (Regex.IsMatch(checkString, @"\A\b[0-9]+\b\Z"))
                {
                    return true;
                }
                else if (Regex.IsMatch(checkString, @"\A\b(0?[xX])[0-9a-fA-F]+\b\Z"))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanSendHexInt(string[] checkStrings)
        {
            foreach (var item in checkStrings)
            {
                if (!CanSendHexInt(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
