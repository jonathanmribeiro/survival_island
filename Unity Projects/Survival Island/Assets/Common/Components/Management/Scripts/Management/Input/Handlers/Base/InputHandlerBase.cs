using SurvivalIsland.Common.Models;

namespace SurvivalIsland.Common.Management
{
    internal abstract class InputHandlerBase
    {
        internal InputModel InputModel { get; set; }

        protected InputHandlerBase() {
            InputModel = new ();
        }

        internal abstract void UpdateInput();
    }
}
