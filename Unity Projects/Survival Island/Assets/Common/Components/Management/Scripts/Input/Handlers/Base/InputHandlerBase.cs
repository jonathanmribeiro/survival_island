using SurvivalIsland.Common.Models;

namespace SurvivalIsland.Common.Management
{
    public abstract class InputHandlerBase
    {
        public InputModel InputModel { get; set; }

        protected InputHandlerBase() {
            InputModel = new ();
        }

        public abstract void UpdateInput();
    }
}
