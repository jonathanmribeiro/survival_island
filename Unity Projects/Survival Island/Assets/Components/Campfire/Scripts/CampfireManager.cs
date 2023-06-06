using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Extensions;

namespace SurvivalIsland.Components.Campfire
{
    public class CampfireManager : StateManagerBase
    {
        private ExtinguishedState _extinguishedState;
        private LitState _litState;
        private PendingConstructionState _pendingConstructionState;
        private UnlitState _unlitState;

        private void Awake()
        {
            _extinguishedState = new(this);
            _litState = new(this);
            _pendingConstructionState = new(this);
            _unlitState = new(this);

        }

        public void Prepare()
        {
            gameObject.name = $"{gameObject.name}_{transform.position}";
            SelectorLocation = gameObject.FindChild("SelectorLocation").transform;

            EnterPendingConstructionState();
        }

        public void EnterExtinguishedState() => SwitchState(_extinguishedState);
        public void EnterLitState() => SwitchState(_litState );
        public void EnterPendingConstructionState() => SwitchState(_pendingConstructionState );
        public void EnterUnlitState() => SwitchState(_unlitState );
    }
}