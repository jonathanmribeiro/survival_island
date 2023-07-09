using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Gameplay.Management;

namespace SurvivalIsland.Components.Fishing
{
    public class FishingManager : PlayerActionStateManagerBase
    {
        private PendingConstructionState _pendingConstructionState;
        private WaitingState _waitingState;

        private GameplayUIManager _uiManager;
        public FishingProps FishingProps;

        public Inventory RecipeInventory;

        public override void Prepare(GameplayUIManager uiManager)
        {
            _pendingConstructionState = new(this);
            _waitingState = new();
            _uiManager = uiManager;

            RecipeInventory.Prepare("Fishing Spot", 3);
            RecipeInventory.AddMultiple(InventoryItemType.Wood, 5);

            EnterPendingConstructionState();
        }

        public void EnterPendingConstructionState()
            => SwitchState(_pendingConstructionState);

        public void EnterWaitingState()
            => SwitchState(_waitingState);

        private void ConfirmCrafting()
            => EnterWaitingState();

        public void OpenCraftingUI()
            => _uiManager.EnterCraftingUIState(RecipeInventory, ConfirmCrafting);
    }
}