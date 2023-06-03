using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
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
        private GrowingState _growingState;
        private TrunkState _trunkState;
        private GoneState _goneState;

        public Inventory Inventory;

        public TreeProps TreeProps;


        private void Awake()
        {
            gameObject.name = $"{gameObject.name}_{transform.position}";
        }

        public void EnterFruitfullState() => SwitchState(_fruitfullState);
        public void EnterHarvestingState() => SwitchState(_harvestingState);
        public void EnterGrowingState() => SwitchState(_growingState);
        public void EnterTrunkState() => SwitchState(_trunkState);
        public void EnterGoneState() => SwitchState(_goneState);

        private void SwitchState(ITreeState nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }

        public void Prepare(DayNightCycle dayNightCycle)
        {
            Inventory.Prepare
                (InventoryConstants.TREE_MAX_ITEMS, InventoryConstants.TREE_MAX_WEIGHT);

            //TODO receive the proper initial props for the tree
            Inventory.AddMultiple(InventoryItemType.Wood, TreeProps.MaxWoodAmount);
            Inventory.AddMultiple(TreeProps.FruitType, TreeProps.MaxFruitAmount);
            Inventory.AddMultiple(InventoryItemType.Leaf, TreeProps.MaxLeavesAmount);

            _fruitfullState = new(this, TreeProps, dayNightCycle);
            _goneState = new(this, TreeProps, dayNightCycle);
            _growingState = new(this, TreeProps, dayNightCycle);
            _harvestingState = new(this, TreeProps, dayNightCycle);
            _trunkState = new(this, TreeProps);

            EnterFruitfullState();
        }

        public void UpdateTree()
        {
            CurrentState.UpdateState();
        }

        private void OnTriggerStay2D(Collider2D collision)
            => CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision)
            => CurrentState.OnTriggerExit2D(collision);

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
            => CurrentState.ExecuteAction(playerActionCallback);

        public bool TryAddItem(InventoryItemType itemType) => Inventory.TryAddItem(itemType);
        public void AddMultiple(InventoryItemType itemType, int amount) => Inventory.AddMultiple(itemType, amount);
        public InventoryItemModel ObtainRandom(InventoryItemType itemType) => Inventory.ObtainRandom(itemType);
        public List<InventoryItemModel> ObtainAll(InventoryItemType itemType) => Inventory.ObtainAll(itemType);
        public void Remove(InventoryItemModel item) => Inventory.Remove(item);
    }
}