using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Extensions;

namespace SurvivalIsland.Components.Signs
{
    public class SignManager : ActionStateManagerBase
    {
        public ActionStateManagerBase Parent;

        private ActiveState _activeState;
        private InactiveState _inactiveState;

        private void Awake()
        {
            _activeState = new(this);
            _inactiveState = new(this);
        }

        public void Prepare(ActionStateManagerBase parent, SignStates state)
        {
            SelectorLocation = gameObject.FindChild("SelectorLocation").transform;
            Parent = parent;

            switch (state)
            {
                case SignStates.ActiveState: EnterActiveState(); break;
                default: EnterInactiveState(); break;
            }
        }

        public void EnterActiveState() 
            => SwitchState(_activeState);

        public void EnterInactiveState() 
            => SwitchState(_inactiveState);
    }
}