using Acr.UserDialogs;
using BaseClasses;
using BaseViewModels;
using DeviceManagers;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.Core.ViewModels
{
    public class PrimaryOverviewViewModel : BaseViewModel
    {
        #region Fields
        bool isDisplayed = true;
        private IWiznetManager WiznetManager;
        private IUserDialogs dialogs;
        #endregion

        #region Properties
        private ObservableCollection<IWiznetStatusViewModel> wiznetStatusControls = new ObservableCollection<IWiznetStatusViewModel>();
        public ObservableCollection<IWiznetStatusViewModel> WiznetStatusControls
        {
            get { return wiznetStatusControls; }
            set
            {
                if (wiznetStatusControls != value)
                {
                    wiznetStatusControls = value;
                    RaisePropertyChanged();
                }
            }
        }

        //public ObservableCollection<WiznetControlsSCPI> Wiznet { get; set; }

        #endregion

        #region Constructors
        public PrimaryOverviewViewModel(IWiznetManager wizManager, IUserDialogs dialog)
        {
            WiznetManager = wizManager;
            dialogs = dialog;

            foreach (var item in WiznetManager.AllWiznets)
            {
                WiznetStatusControls.Add(new WiznetStatusViewModel((IWiznetPiControl)item));
            }
            //WiznetStatusControls.Add();
        }


        #endregion

        #region Methods
        //private Task OnUpdateView()
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

    }
}
