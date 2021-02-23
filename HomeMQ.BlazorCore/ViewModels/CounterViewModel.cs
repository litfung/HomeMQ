using System;
using System.Collections.Generic;
using System.Text;
using WiznetControllers;

namespace HomeMQ.CoreBlazor.ViewModels
{
    public class CounterViewModel
    {
        #region Fields
        public ICounterService CounterService { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public CounterViewModel(ICounterService counterService)
        {
            CounterService = counterService;
        }
        #endregion

        #region Methods

        #endregion

    }
}
