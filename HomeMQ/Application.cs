using BaseClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiznetControllers;

namespace HomeMQ.ConsoleApp
{
    public class Application : IApplication
    {
        IMessenger messenger;
        IWiznetManager wiznetManager;
        public Application(IMessenger blah, IWiznetManager wm)
        {
            messenger = blah;
            wiznetManager = wm;
        }

        public void Run()
        {
            var firstWiz = new WiznetControlSCPI("169.254.208.100", messenger);
            wiznetManager.AddWiznet(firstWiz);

            _ = FirstTest();
        }

        private async Task FirstTest()
        {
            try
            {
                var wiz = wiznetManager.AllWiznets.First();
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
