using SurvivalIsland.Common.Bases;

namespace SurvivalIsland.Components.Fishing
{
    public class ReelingState : StateBase
    {
        private readonly FishShadowManager _manager;

        public ReelingState(FishShadowManager manager)
        {
            _manager = manager;
        }
    }
}