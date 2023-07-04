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

        private GameplayUIManager _gameplayUIManager;
        public Inventory CampfireInventory;
        public Inventory RecipeInventory;

        public CampfireProps CampfireProps;

        public void Prepare(GameplayUIManager gameplayUIManager, DayNightCycle dayNightCycle)
        {
            //TODO receive the proper initial props for the campfire

            gameObject.name = $"{gameObject.name}_{transform.position}";
            SelectorLocation = gameObject.FindChild("SelectorLocation").transform;

            _gameplayUIManager = gameplayUIManager;

            CampfireInventory.Prepare(1);
            RecipeInventory.Prepare(3);

            RecipeInventory.AddMultiple(InventoryItemType.Leaf, 5);
            RecipeInventory.AddMultiple(InventoryItemType.Wood, 4);

            _extinguishedState = new(this);
            _litState = new(this, CampfireProps, dayNightCycle);
            _pendingConstructionState = new(this);
            _unlitState = new(this);

            EnterPendingConstructionState();
        }

        private void ConfirmCrafting()
        {
            CampfireInventory.AddMultiple(InventoryItemType.Wood, 5);
            EnterUnlitState();
        }

        public void EnterExtinguishedState() => SwitchState(_extinguishedState);
        public void EnterLitState() => SwitchState(_litState);
        public void EnterPendingConstructionState() => SwitchState(_pendingConstructionState);
        public void EnterUnlitState() => SwitchState(_unlitState);
        
        public void OpenCraftingUI() => _gameplayUIManager.EnterCraftingUIState(RecipeInventory, ConfirmCrafting);
    }
}