using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterActionsManager : MonoBehaviour
    {
        private MainCharacterInventoryManager _inventoryManager;

        private void Awake()
        {
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
        }

        public void Prepare()
        {
        }

        public bool ExecuteAction(PlayerActionTypes performedAction, InventoryItemModel itemModel = null)
            => performedAction switch
            {
                PlayerActionTypes.CollectingLeaves => _inventoryManager.TryAddItem(itemModel),
                PlayerActionTypes.CollectingWood => _inventoryManager.TryAddItem(itemModel),
                _ => false,
            };
    }
}