using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class TreeManager : MonoBehaviour
    {
        public Inventory Inventory;

        private bool _playerInRange;

        private void Awake()
        {
            gameObject.name = $"{gameObject.name}_{transform.position}";
        }

        public void Prepare()
        {
            Inventory.Prepare
                (InventoryConstants.TREE_MAX_ITEMS, InventoryConstants.TREE_MAX_WEIGHT);

            Inventory.AddMultiple
                (InventoryItemType.Wood, 10);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _playerInRange = collision.CompareTag(TagConstants.PLAYER);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                _playerInRange = false;
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = Inventory.ObtainRandom(InventoryItemType.Wood);
            
            if (randomItem == null)
                return;

            var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.CollectingWood, randomItem);

            if (actionExecutedSuccessfully)
                Inventory.Remove(randomItem);
        }
    }
}