
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class InventoryUIState : IGameplayUIState
    {
        private readonly GameplayUIManager _uiManager;
        private readonly MainCharacterManager _mainCharacterManager;

        private GameObject _inventoryUI;

        private ChildTextUpdater _healthText;
        private ChildTextUpdater _hungerText;
        private ChildTextUpdater _thirstText;
        private ChildTextUpdater _energyText;

        private ChildButtonAction _closeInventoryButton;
        private ChildButtonAction _openJournalButton;

        public InventoryUIState(GameplayUIManager uiManager,
                                MainCharacterManager mainCharacterManager)
        {
            _uiManager = uiManager;
            _mainCharacterManager = mainCharacterManager;

            _inventoryUI = GameObject.Find("Canvas").FindChild("InventoryUI");
            _inventoryUI.SetActive(false);

            var health = _inventoryUI.FindChild("Health");
            _healthText = health.GetComponentInChildren<ChildTextUpdater>();

            var hunger = _inventoryUI.FindChild("Hunger");
            _hungerText = hunger.GetComponentInChildren<ChildTextUpdater>();

            var thirst = _inventoryUI.FindChild("Thirst");
            _thirstText = thirst.GetComponentInChildren<ChildTextUpdater>();

            var energy = _inventoryUI.FindChild("Energy");
            _energyText = energy.GetComponentInChildren<ChildTextUpdater>();

            var middlePanel = _inventoryUI.FindChild("MiddlePanel");
            _closeInventoryButton = middlePanel.FindChild("Close").GetComponent<ChildButtonAction>();
            _openJournalButton = middlePanel.FindChild("Journal").GetComponent<ChildButtonAction>();
        }

        public void EnterState()
        {
            _closeInventoryButton.Prepare(OnClick_CloseInventory);
            _openJournalButton.Prepare(OnClick_OpenJournal);

            _inventoryUI.SetActive(true);
            _inventoryUI.GetComponent<CanvasGroup>().alpha = 1.0f;
        }

        public void UpdateState()
        {
            var playerVitalitySystem = _mainCharacterManager.GetVitalitySystem();

            _healthText.UpdateUI(playerVitalitySystem.Health.ToString("0"));
            _hungerText.UpdateUI(playerVitalitySystem.Hunger.ToString("0"));
            _thirstText.UpdateUI(playerVitalitySystem.Thirst.ToString("0"));
            _energyText.UpdateUI(playerVitalitySystem.Energy.ToString("0"));
        }

        public void ExitState()
        {
            _inventoryUI.GetComponent<CanvasGroup>().alpha = 0.0f;
            _inventoryUI.SetActive(false);
        }

        public void OnClick_CloseInventory() => _uiManager.EnterBasicUIState();
        public void OnClick_OpenJournal() => _uiManager.EnterJournalState();
    }
}