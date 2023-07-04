using SurvivalIsland.Common.Bases;

namespace SurvivalIsland.Components.Signs
{
    public class InactiveState : StateBase
    {
        private SignManager _manager;

        public InactiveState(SignManager manager)
        {
            _manager = manager;
        }

        public override void EnterState()
        {
            _manager.gameObject.SetActive(false);
        }
    }
}