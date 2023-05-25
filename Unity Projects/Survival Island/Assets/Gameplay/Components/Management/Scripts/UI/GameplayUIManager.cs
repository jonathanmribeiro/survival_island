using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using SurvivalIsland.Gameplay.Management.UI;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class GameplayUIManager : MonoBehaviour
    {
        private IGameplayUIState _currentState;
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
            _inventoryUIState = new(this);
            _journalUIState = new(this);

            EnterBasicUIState();
        }

        public void UpdateUI()
        {
            _currentState.UpdateState();
        }

        private void SwitchState(IGameplayUIState nextState)
        {
            _currentState?.ExitState();
            _currentState = nextState;
            _currentState.EnterState();
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