using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Gameplay.Management;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class ActionStateManagerBase : MonoBehaviour
    {
        public StateBase CurrentState;
        public string CurrentStateName;
        public Transform SelectorLocation;
        public Vector2 SelectorSize;
        public TimeSpan? TimeLeft;

        public void SwitchState(StateBase nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentStateName = CurrentState.GetType().Name;
            CurrentState.EnterState();
        }

        public virtual void Prepare(GameplayUIManager uiManager) { }
        public virtual void Prepare(GameplayUIManager uiManager, DayNightCycle dayNightCycle) { }
        public virtual void Prepare(DayNightCycle dayNightCycle) { }

        public void UpdateCurrentState()
            => CurrentState.UpdateState();

        private void OnTriggerStay2D(Collider2D collision) 
            => CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision) 
            => CurrentState.OnTriggerExit2D(collision);

        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
            => CurrentState.ExecuteAction(playerActionCallback);

        public void ExecuteQuickAction(Action<InventoryItemModel> playerActionCallback, InventoryItemModel itemModel)
            => CurrentState.ExecuteQuickAction(playerActionCallback, itemModel);

        public PlayerActionTypes GetAction() 
            => CurrentState.GetAction();
    }
}
