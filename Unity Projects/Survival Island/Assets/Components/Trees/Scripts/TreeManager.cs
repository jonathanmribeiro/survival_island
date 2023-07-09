using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Utils;

namespace SurvivalIsland.Components.Trees
{
    public class TreeManager : PlayerActionStateManagerBase
    {
        private FruitfullState _fruitfullState;
        private HarvestingState _harvestingState;
        private GrowingState _growingState;
        private TrunkState _trunkState;
        private GoneState _goneState;

        public TreeProps TreeProps;
        public Inventory TreeInventory;

        private void Awake()
        {
            gameObject.name = $"{gameObject.name}_{transform.position}";
            SelectorLocation = gameObject.FindChild("SelectorLocation").transform;
        }

        public void EnterFruitfullState() 
            => SwitchState(_fruitfullState);

        public void EnterHarvestingState() 
            => SwitchState(_harvestingState);

        public void EnterGrowingState() 
            => SwitchState(_growingState);

        public void EnterTrunkState() 
            => SwitchState(_trunkState);

        public void EnterGoneState() 
            => SwitchState(_goneState);

        public override void Prepare(DayNightCycle dayNightCycle)
        {
            TreeInventory.Prepare("Tree", 3);

            //TODO receive the proper initial props for the tree
            TreeInventory.AddMultiple(InventoryItemType.Wood, TreeProps.MaxWoodAmount);
            TreeInventory.AddMultiple(TreeProps.FruitType, TreeProps.MaxFruitAmount);
            TreeInventory.AddMultiple(InventoryItemType.Leaf, TreeProps.MaxLeavesAmount);

            _fruitfullState = new(this, TreeProps, dayNightCycle);
            _goneState = new(this, TreeProps, dayNightCycle);
            _growingState = new(this, TreeProps, dayNightCycle);
            _harvestingState = new(this, TreeProps, dayNightCycle);
            _trunkState = new(this, TreeProps);

            EnterFruitfullState();
        }
    }
}