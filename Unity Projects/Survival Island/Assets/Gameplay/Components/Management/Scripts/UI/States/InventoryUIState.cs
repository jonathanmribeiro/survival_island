
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

        private ChildIconUpdater _characterIcon1;
        private ChildIconUpdater _characterIcon2;
        private ChildIconUpdater _characterIcon3;
        private ChildIconUpdater _characterIcon4;

        private ChildIconUpdater _inventoryIcon1;
        private ChildIconUpdater _inventoryIcon2;
        private ChildIconUpdater _inventoryIcon3;
        private ChildIconUpdater _inventoryIcon4;
        private ChildIconUpdater _inventoryIcon5;
        private ChildIconUpdater _inventoryIcon6;
        private ChildIconUpdater _inventoryIcon7;
        private ChildIconUpdater _inventoryIcon8;

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

            var characterPanel = _inventoryUI.FindChild("Character");
            _characterIcon1 = characterPanel.FindChild("CharacterSlot1").GetComponent<ChildIconUpdater>();
            _characterIcon2 = characterPanel.FindChild("CharacterSlot2").GetComponent<ChildIconUpdater>();
            _characterIcon3 = characterPanel.FindChild("CharacterSlot3").GetComponent<ChildIconUpdater>();
            _characterIcon4 = characterPanel.FindChild("CharacterSlot4").GetComponent<ChildIconUpdater>();

            var inventoryPanel = _inventoryUI.FindChild("Inventory");
            _inventoryIcon1 = inventoryPanel.FindChild("InventorySlot1").GetComponent<ChildIconUpdater>();
            _inventoryIcon2 = inventoryPanel.FindChild("InventorySlot2").GetComponent<ChildIconUpdater>();
            _inventoryIcon3 = inventoryPanel.FindChild("InventorySlot3").GetComponent<ChildIconUpdater>();
            _inventoryIcon4 = inventoryPanel.FindChild("InventorySlot4").GetComponent<ChildIconUpdater>();
            _inventoryIcon5 = inventoryPanel.FindChild("InventorySlot5").GetComponent<ChildIconUpdater>();
            _inventoryIcon6 = inventoryPanel.FindChild("InventorySlot6").GetComponent<ChildIconUpdater>();
            _inventoryIcon7 = inventoryPanel.FindChild("InventorySlot7").GetComponent<ChildIconUpdater>();
            _inventoryIcon8 = inventoryPanel.FindChild("InventorySlot8").GetComponent<ChildIconUpdater>();

        }

        public void EnterState()
        {
            _closeInventoryButton.Prepare(OnClick_CloseInventory);
            _openJournalButton.Prepare(OnClick_OpenJournal);

            _characterIcon1.Prepare("InventorySlotIcon");
            _characterIcon2.Prepare("InventorySlotIcon");
            _characterIcon3.Prepare("InventorySlotIcon");
            _characterIcon4.Prepare("InventorySlotIcon");
            _inventoryIcon1.Prepare("InventorySlotIcon");
            _inventoryIcon2.Prepare("InventorySlotIcon");
            _inventoryIcon3.Prepare("InventorySlotIcon");
            _inventoryIcon4.Prepare("InventorySlotIcon");
            _inventoryIcon5.Prepare("InventorySlotIcon");
            _inventoryIcon6.Prepare("InventorySlotIcon");
            _inventoryIcon7.Prepare("InventorySlotIcon");
            _inventoryIcon8.Prepare("InventorySlotIcon");

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

            _characterIcon1.UpdateUI(_mainCharacterManager.GetCharacterItem(0));
            _characterIcon2.UpdateUI(_mainCharacterManager.GetCharacterItem(1));
            _characterIcon3.UpdateUI(_mainCharacterManager.GetCharacterItem(2));
            _characterIcon4.UpdateUI(_mainCharacterManager.GetCharacterItem(3));

            _inventoryIcon1.UpdateUI(_mainCharacterManager.GetInventoryItem(0));
            _inventoryIcon2.UpdateUI(_mainCharacterManager.GetInventoryItem(1));
            _inventoryIcon3.UpdateUI(_mainCharacterManager.GetInventoryItem(2));
            _inventoryIcon4.UpdateUI(_mainCharacterManager.GetInventoryItem(3));
            _inventoryIcon5.UpdateUI(_mainCharacterManager.GetInventoryItem(4));
            _inventoryIcon6.UpdateUI(_mainCharacterManager.GetInventoryItem(5));
            _inventoryIcon7.UpdateUI(_mainCharacterManager.GetInventoryItem(6));
            _inventoryIcon8.UpdateUI(_mainCharacterManager.GetInventoryItem(7));
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