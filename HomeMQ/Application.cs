using BaseClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiznetControllers;
using HomeMQ.Managers;
using BaseClasses.StateManagers;

namespace HomeMQ.ConsoleApp
{
    public class Application : IApplication
    {
        IMessenger _messenger;
        ILogManager _logger;
        IStateManager _stateManager;
        IBackgroundHandler _backgroundHandler;
        IWiznetManager _wiznetManager;
        public Application(IBackgroundHandler backgroundHandler, IWiznetManager wiznetManager)
        {
            _backgroundHandler = backgroundHandler;
            _wiznetManager = wiznetManager;
        }

        public void Run()
        {
            var firstWiz = new WiznetControlSCPI("169.254.208.100", _backgroundHandler);
            _wiznetManager.AddWiznet(firstWiz);

            _ = FirstTest();
        }

        private async Task FirstTest()
        {
            try
            {
                var wiz = _wiznetManager.AllWiznets.First();
                wiz.Connect();
                await Task.Delay(1000);
                var resp = await wiz.SendAsync("*idn?", false);
                Debug.WriteLine(resp);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }  
            
        }
    }
}
