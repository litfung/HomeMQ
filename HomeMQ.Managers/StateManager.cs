using BaseClasses;
using HomeMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Managers
{
    public class StateManager
    {
        #region Singleton
        private static readonly Lazy<StateManager> instance = new Lazy<StateManager>();
        public static StateManager Instance => instance.Value;
        public StateManager()
        {
            //LoadDefaultConfig();
        }
        #endregion

        #region Fields
        public Messenger Messenger => Messenger.Instance;
        #endregion

        #region Properties
        public SavedStateModel State { get; private set; }
        
        #endregion
    }
}
