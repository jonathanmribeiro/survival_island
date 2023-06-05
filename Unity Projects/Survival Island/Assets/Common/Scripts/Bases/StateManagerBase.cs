using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class StateManagerBase : MonoBehaviour
    {
        public IState CurrentState;
        public Transform SelectorLocation;
        public Vector2 SelectorSize;

        public void SwitchState(IState nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }

        private void OnTriggerStay2D(Collider2D collision) => CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision) => CurrentState.OnTriggerExit2D(collision);

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
            => CurrentState.ExecuteAction(playerActionCallback);

        public PlayerActionTypes GetAction() => CurrentState.GetAction();
    }
}
