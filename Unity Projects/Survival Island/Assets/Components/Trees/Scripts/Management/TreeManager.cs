using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class TreeManager : MonoBehaviour
    {
        public ITreeState CurrentState;

        private FruitfullState _fruitfullState;
        private HarvestingState _harvestingState;
        private RevitalizationState _revitalizationState;
        private TrunkState _trunkState;

        public Inventory Inventory;

        public TreeProps TreeProps;

        private void Awake()
        {
            gameObject.name = $"{gameObject.name}_{transform.position}";

            _fruitfullState = new(this, TreeProps);
            _harvestingState = new(this, TreeProps);
            _revitalizationState = new(this, TreeProps);
            _trunkState = new(this, TreeProps);
        }

        public void EnterFruitfullState() => SwitchState(_fruitfullState);
        public void EnterHarvestingState() => SwitchState(_harvestingState);
        public void EnterRevitalizationState() => SwitchState(_revitalizationState);
        public void EnterTrunkState() => SwitchState(_trunkState);

        private void SwitchState(ITreeState nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }

        public void Prepare()
        {
            Inventory.Prepare
                (InventoryConstants.TREE_MAX_ITEMS, InventoryConstants.TREE_MAX_WEIGHT);

            //TODO receive the proper initial props for the tree
            Inventory.AddMultiple(InventoryItemType.Wood, 10);
            Inventory.AddMultiple(TreeProps.FruitType, TreeProps.MaxFruitAmount);
            EnterFruitfullState();
        }

        private void OnTriggerStay2D(Collider2D collision)
            => CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision)
            => CurrentState.OnTriggerExit2D(collision);

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
            => CurrentState.ExecuteAction(playerActionCallback);

        public bool TryAddItem(InventoryItemType itemType) => Inventory.TryAddItem(itemType);
        public InventoryItemModel ObtainRandom(InventoryItemType itemType) => Inventory.ObtainRandom(itemType);
        public List<InventoryItemModel> ObtainAll(InventoryItemType itemType) => Inventory.ObtainAll(itemType);
        public void Remove(InventoryItemModel item) => Inventory.Remove(item);
    }
}