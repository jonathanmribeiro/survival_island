using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System.Collections.Generic;

namespace SurvivalIsland.Components.Trees
{
    public class TreeManager : PlayerActionStateManagerBase
    {
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
            SelectorLocation = gameObject.FindChild("SelectorLocation").transform;
        }

        public void EnterFruitfullState() => SwitchState(_fruitfullState);
        public void EnterHarvestingState() => SwitchState(_harvestingState);
        public void EnterGrowingState() => SwitchState(_growingState);
        public void EnterTrunkState() => SwitchState(_trunkState);
        public void EnterGoneState() => SwitchState(_goneState);

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

        public bool TryAddItem(InventoryItemType itemType) => Inventory.TryAddItem(itemType);
        public void AddMultiple(InventoryItemType itemType, int amount) => Inventory.AddMultiple(itemType, amount);
        public InventoryItemModel ObtainRandom(InventoryItemType itemType) => Inventory.ObtainRandom(itemType);
        public List<InventoryItemModel> ObtainAll(InventoryItemType itemType) => Inventory.ObtainAll(itemType);
        public void Remove(InventoryItemModel item) => Inventory.Remove(item);
    }
}