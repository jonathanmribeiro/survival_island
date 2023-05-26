using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using SurvivalIsland.Gameplay.Management.UI;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class GameplayUIManager : MonoBehaviour
    {
        public IGameplayUIState CurrentState;
        private BasicUIState _basicUIState;
        private InventoryUIState _inventoryUIState;
        private JournalUIState _journalUIState;

        public void Prepare
        (
            MainCharacterManager mainCharacterManager,
            DayNightCycle dayNightCycle
        )
        {
            _basicUIState = new(this, mainCharacterManager, dayNightCycle);
            _inventoryUIState = new(this, mainCharacterManager);
            _journalUIState = new(this);

            EnterBasicUIState();
        }

        public void UpdateUI()
        {
            CurrentState.UpdateState();
        }

        private void SwitchState(IGameplayUIState nextState)
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }

        public void EnterBasicUIState()
        {
            SwitchState(_basicUIState);
        }

        public void EnterInventoryState()
        {
            SwitchState(_inventoryUIState);
        }

        public void EnterJournalState()
        {
            SwitchState(_journalUIState);
        }
    }
}