using SurvivalIsland.Common.Bases;

namespace SurvivalIsland.Components.Fishing
{
    public class FishShadowManager : ActionStateManagerBase
    {
        private FleeingState _fleeingState;
        private IdleState _idleState;
        private ReelingState _reelingState;
        private ReturningState _returningState;

        private void Awake()
        {
            _fleeingState = new(this);
            _idleState = new(this);
            _reelingState = new(this);
            _returningState = new(this);

            EnterIdleState();
        }

        public void EnterFleeingState() => SwitchState(_fleeingState);
        public void EnterIdleState() => SwitchState(_idleState);
        public void EnterReelingState() => SwitchState(_reelingState);
        public void EnterReturningState() => SwitchState(_returningState);
    }
}