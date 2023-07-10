using SurvivalIsland.Common.Bases;

namespace SurvivalIsland.Components.Fishing
{
    public class FleeingState : StateBase
    {
        private readonly FishShadowManager _manager;

        public FleeingState(FishShadowManager manager)
        {
            _manager = manager;
        }
    }
}