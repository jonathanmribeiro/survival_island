using SurvivalIsland.Common.Interfaces;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class StateManagerBase : MonoBehaviour
    {
        public IState CurrentState;

        public void SwitchState(IState nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }

        public virtual void UpdateCurrentState()
            => CurrentState.UpdateState();
    }
}
