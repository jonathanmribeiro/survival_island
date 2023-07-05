using SurvivalIsland.Common.Enums;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class PlayerActionStateManagerBase : MonoBehaviour
    {
        public StateBase CurrentState;
        public string CurrentStateName;
        public Transform SelectorLocation;
        public Vector2 SelectorSize;

        public void SwitchState(StateBase nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentStateName = CurrentState.GetType().Name;
            CurrentState.EnterState();
        }

        private void OnTriggerStay2D(Collider2D collision) 
            => CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision) 
            => CurrentState.OnTriggerExit2D(collision);

        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
            => CurrentState.ExecuteAction(playerActionCallback);

        public PlayerActionTypes GetAction() 
            => CurrentState.GetAction();
    }
}
