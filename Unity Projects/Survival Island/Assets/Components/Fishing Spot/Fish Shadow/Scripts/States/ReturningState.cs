using SurvivalIsland.Common.Bases;

namespace SurvivalIsland.Components.Fishing
{
    public class ReturningState : StateBase
    {
        private readonly FishShadowManager _manager;

        public ReturningState(FishShadowManager manager)
        {
            _manager = manager;
        }
    }
}