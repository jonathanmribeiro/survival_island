using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class PlayerActionStateManagerBase : MonoBehaviour
    {
        public IPlayerActionState CurrentState;
        public string CurrentStateName;
        public Transform SelectorLocation;
        public Vector2 SelectorSize;
        public Inventory.Inventory Inventory;

        public void SwitchState(IPlayerActionState nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentStateName = CurrentState.GetType().Name;
            CurrentState.EnterState();
        }

        private void OnTriggerStay2D(Collider2D collision) => CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision) => CurrentState.OnTriggerExit2D(collision);

        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
            => CurrentState.ExecuteAction(playerActionCallback);

        public PlayerActionTypes GetAction() => CurrentState.GetAction();

        public int CountItemsOfType(InventoryItemType itemType)
            => Inventory.ObtainAll(itemType)?.Count ?? 0;

        public bool TryAddItem(InventoryItemType itemType)
            => Inventory.TryAddItem(itemType);

        public InventoryItemModel ObtainRandom(InventoryItemType itemType)
            => Inventory.ObtainRandom(itemType);

        public void Remove(InventoryItemModel item)
            => Inventory.Remove(item);

        public void Remove(InventoryItemType itemType)
            => Inventory.Remove(itemType);

        public void ForceAmount(InventoryItemType itemType, int amount)
            => Inventory.ForceAmount(itemType, amount);
    }
}
