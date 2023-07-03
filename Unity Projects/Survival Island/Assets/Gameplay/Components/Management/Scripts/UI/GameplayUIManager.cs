using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using SurvivalIsland.Gameplay.Management.UI;
using System;

namespace SurvivalIsland.Gameplay.Management
{
    public class GameplayUIManager : StateManagerBase
    {
        private BasicUIState _basicUIState;
        private CraftingUIState _craftingUIState;
        private InventoryUIState _inventoryUIState;
        private JournalUIState _journalUIState;

        public void Prepare
        (
            GameplaySceneManager gameplaySceneManager,
            MainCharacterManager mainCharacterManager,
            DayNightCycle dayNightCycle
        )
        {
            _basicUIState = new(this, gameplaySceneManager, dayNightCycle, mainCharacterManager);
            _craftingUIState = new(this, gameplaySceneManager, mainCharacterManager);
            _inventoryUIState = new(this, gameplaySceneManager, mainCharacterManager);
            _journalUIState = new(this, gameplaySceneManager);

            EnterBasicUIState();
        }

        public void UpdateUI() => CurrentState.UpdateState();
        public void EnterBasicUIState() => SwitchState(_basicUIState);
        public void EnterCraftingUIState(Inventory recipeInventory, Action afterCraftingCallback)
        {
            _craftingUIState.SetRecipe(recipeInventory);
            _craftingUIState.SetCraftingCallback(afterCraftingCallback);
            SwitchState(_craftingUIState);
        }
        public void EnterInventoryState() => SwitchState(_inventoryUIState);
        public void EnterJournalState() => SwitchState(_journalUIState);
    }
}