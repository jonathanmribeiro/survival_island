using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class InventoryUIState : IState
    {
        private readonly GameplayUIManager _uiManager;
        private readonly MainCharacterManager _mainCharacterManager;
        private readonly GameplaySceneManager _sceneManager;

        private readonly GameObject _inventoryUI;

        private readonly ChildTextUpdater _healthText;
        private readonly ChildTextUpdater _hungerText;
        private readonly ChildTextUpdater _thirstText;
        private readonly ChildTextUpdater _energyText;

        private readonly ChildButtonAction _closeInventoryButton;
        private readonly ChildButtonAction _openJournalButton;

        private readonly UIInventorySlot _characterSlot1;
        private readonly UIInventorySlot _characterSlot2;
        private readonly UIInventorySlot _characterSlot3;
        private readonly UIInventorySlot _characterSlot4;
        private readonly UIInventorySlot _uiInventorySlot1;
        private readonly UIInventorySlot _uiInventorySlot2;
        private readonly UIInventorySlot _uiInventorySlot3;
        private readonly UIInventorySlot _uiInventorySlot4;
        private readonly UIInventorySlot _uiInventorySlot5;
        private readonly UIInventorySlot _uiInventorySlot6;
        private readonly UIInventorySlot _uiInventorySlot7;
        private readonly UIInventorySlot _uiInventorySlot8;

        public InventoryUIState(GameplayUIManager uiManager,
                                GameplaySceneManager gameplaySceneManager,
                                MainCharacterManager mainCharacterManager)
        {
            _uiManager = uiManager;
            _mainCharacterManager = mainCharacterManager;
            _sceneManager = gameplaySceneManager;

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
            _characterSlot1 = new(gameplaySceneManager, characterPanel, "CharacterSlot1", () => { });
            _characterSlot2 = new(gameplaySceneManager, characterPanel, "CharacterSlot2", () => { });
            _characterSlot3 = new(gameplaySceneManager, characterPanel, "CharacterSlot3", () => { });
            _characterSlot4 = new(gameplaySceneManager, characterPanel, "CharacterSlot4", () => { });

            var inventoryPanel = _inventoryUI.FindChild("Inventory");
            _uiInventorySlot1 = new(gameplaySceneManager, inventoryPanel, "InventorySlot1", () => { });
            _uiInventorySlot2 = new(gameplaySceneManager, inventoryPanel, "InventorySlot2", () => { });
            _uiInventorySlot3 = new(gameplaySceneManager, inventoryPanel, "InventorySlot3", () => { });
            _uiInventorySlot4 = new(gameplaySceneManager, inventoryPanel, "InventorySlot4", () => { });
            _uiInventorySlot5 = new(gameplaySceneManager, inventoryPanel, "InventorySlot5", () => { });
            _uiInventorySlot6 = new(gameplaySceneManager, inventoryPanel, "InventorySlot6", () => { });
            _uiInventorySlot7 = new(gameplaySceneManager, inventoryPanel, "InventorySlot7", () => { });
            _uiInventorySlot8 = new(gameplaySceneManager, inventoryPanel, "InventorySlot8", () => { });
        }

        public void EnterState()
        {
            _closeInventoryButton.Prepare(_sceneManager, OnClick_CloseInventory);
            _openJournalButton.Prepare(_sceneManager, OnClick_OpenJournal);

            _characterSlot1.Prepare();
            _characterSlot2.Prepare();
            _characterSlot3.Prepare();
            _characterSlot4.Prepare();
            _uiInventorySlot1.Prepare();
            _uiInventorySlot2.Prepare();
            _uiInventorySlot3.Prepare();
            _uiInventorySlot4.Prepare();
            _uiInventorySlot5.Prepare();
            _uiInventorySlot6.Prepare();
            _uiInventorySlot7.Prepare();
            _uiInventorySlot8.Prepare();

            _inventoryUI.SetActive(true);
        }

        public void UpdateState()
        {
            var playerVitalitySystem = _mainCharacterManager.GetVitalitySystem();

            _healthText.UpdateUI(playerVitalitySystem.Health.ToString("0"));
            _hungerText.UpdateUI(playerVitalitySystem.Hunger.ToString("0"));
            _thirstText.UpdateUI(playerVitalitySystem.Thirst.ToString("0"));
            _energyText.UpdateUI(playerVitalitySystem.Energy.ToString("0"));

            _characterSlot1.Update(_mainCharacterManager.GetCharacterItem(0));
            _characterSlot2.Update(_mainCharacterManager.GetCharacterItem(1));
            _characterSlot3.Update(_mainCharacterManager.GetCharacterItem(2));
            _characterSlot4.Update(_mainCharacterManager.GetCharacterItem(3));
            _uiInventorySlot1.Update(_mainCharacterManager.GetInventorySlot(0));
            _uiInventorySlot2.Update(_mainCharacterManager.GetInventorySlot(1));
            _uiInventorySlot3.Update(_mainCharacterManager.GetInventorySlot(2));
            _uiInventorySlot4.Update(_mainCharacterManager.GetInventorySlot(3));
            _uiInventorySlot5.Update(_mainCharacterManager.GetInventorySlot(4));
            _uiInventorySlot6.Update(_mainCharacterManager.GetInventorySlot(5));
            _uiInventorySlot7.Update(_mainCharacterManager.GetInventorySlot(6));
            _uiInventorySlot8.Update(_mainCharacterManager.GetInventorySlot(7));
        }

        public void ExitState()
            => _inventoryUI.SetActive(false);

        public void OnClick_CloseInventory()
            => _uiManager.EnterBasicUIState();

        public void OnClick_OpenJournal()
            => _uiManager.EnterJournalState();
    }
}