using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using SurvivalIsland.Gameplay.Management.UI;

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
            MainCharacterManager mainCharacterManager,
            DayNightCycle dayNightCycle
        )
        {
            _basicUIState = new(this, dayNightCycle, mainCharacterManager);
            _craftingUIState = new(this);
            _inventoryUIState = new(this, mainCharacterManager);
            _journalUIState = new(this);

            EnterBasicUIState();
        }

        public void UpdateUI() => CurrentState.UpdateState();
        public void EnterBasicUIState() => SwitchState(_basicUIState);
        public void EnterCraftingUIState() => SwitchState(_craftingUIState);
        public void EnterInventoryState() => SwitchState(_inventoryUIState);
        public void EnterJournalState() => SwitchState(_journalUIState);
    }
}