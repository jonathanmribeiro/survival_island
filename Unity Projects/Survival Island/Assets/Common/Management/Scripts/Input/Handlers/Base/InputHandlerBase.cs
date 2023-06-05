using SurvivalIsland.Common.Models;
using System;

namespace SurvivalIsland.Common.Management
{
    public abstract class InputHandlerBase
    {
        public InputModel InputModel { get; set; }
        public Action ActionToExecute { get; set; }

        protected InputHandlerBase()
        {
            InputModel = new();
        }

        public abstract void UpdateInput();

        public void ActionCaptured()
        {
            ActionToExecute.Invoke();
        }
    }
}
