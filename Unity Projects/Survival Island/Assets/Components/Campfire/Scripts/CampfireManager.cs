using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Gameplay.Management;

namespace SurvivalIsland.Components.Campfire
{
    public class CampfireManager : PlayerActionStateManagerBase
    {
        private ExtinguishedState _extinguishedState;
        private LitState _litState;
        private PendingConstructionState _pendingConstructionState;
        private UnlitState _unlitState;

        private GameplayUIManager _uiManager;
        
        public CampfireProps CampfireProps;

        public Inventory CampfireInventory;
        public Inventory RecipeInventory;

        public override void Prepare(GameplayUIManager uiManager, DayNightCycle dayNightCycle)
        {
            //TODO receive the proper initial props for the campfire
            gameObject.name = $"{gameObject.name}_{transform.position}";
            SelectorLocation = gameObject.FindChild("SelectorLocation").transform;

            _uiManager = uiManager;

            CampfireInventory.Prepare(maxItems: 1);
            RecipeInventory.Prepare("Campfire", 3);

            RecipeInventory.AddMultiple(InventoryItemType.Leaf, 5);
            RecipeInventory.AddMultiple(InventoryItemType.Wood, 4);

            _extinguishedState = new(this, CampfireProps);
            _litState = new(this, CampfireProps, dayNightCycle);
            _pendingConstructionState = new(this);
            _unlitState = new(this, CampfireProps);

            EnterPendingConstructionState();
        }

        private void ConfirmCrafting()
        {
            CampfireInventory.AddMultiple(InventoryItemType.Wood, 5);
            EnterUnlitState();
        }

        public void EnterExtinguishedState()
            => SwitchState(_extinguishedState);

        public void EnterLitState()
            => SwitchState(_litState);

        public void EnterPendingConstructionState()
            => SwitchState(_pendingConstructionState);

        public void EnterUnlitState()
            => SwitchState(_unlitState);

        public void OpenCraftingUI()
            => _uiManager.EnterCraftingUIState(RecipeInventory, ConfirmCrafting);
    }
}