using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class BasicUIState : IState
    {
        private readonly GameplayUIManager _uiManager;
        private readonly DayNightCycle _dayNightCycle;
        private readonly MainCharacterManager _mainCharacterManager;
        private readonly GameplaySceneManager _sceneManager;

        private GameObject _basicUI;

        private ChildTextUpdater _datetimeText;

        private ChildTextUpdater _healthText;
        private ChildTextUpdater _hungerText;
        private ChildTextUpdater _thirstText;
        private ChildTextUpdater _energyText;

        private ChildIconUpdater _quickAction1Icon;
        private ChildButtonAction _quickAction1Button;
        private ChildTextUpdater _quickAction1Text;

        private ChildIconUpdater _quickAction2Icon;
        private ChildButtonAction _quickAction2Button;
        private ChildTextUpdater _quickAction2Text;

        private ChildIconUpdater _quickAction3Icon;
        private ChildButtonAction _quickAction3Button;
        private ChildTextUpdater _quickAction3Text;

        private ChildIconUpdater _quickAction4Icon;
        private ChildButtonAction _quickAction4Button;
        private ChildTextUpdater _quickAction4Text;

        private ChildButtonAction _openInventoryButton;

        public BasicUIState(GameplayUIManager uiManager,
                            GameplaySceneManager gameplaySceneManager,
                            DayNightCycle dayNightCycle,
                            MainCharacterManager mainCharacterManager)
        {
            _uiManager = uiManager;
            _dayNightCycle = dayNightCycle;
            _mainCharacterManager = mainCharacterManager;
            _sceneManager = gameplaySceneManager;

            _basicUI = GameObject.Find("Canvas").FindChild("BasicUI");
            _basicUI.SetActive(false);

            var dateTimePanel = _basicUI.FindChild("DateTimePanel");
            _datetimeText = dateTimePanel.GetComponentInChildren<ChildTextUpdater>();

            var health = _basicUI.FindChild("Health");
            _healthText = health.GetComponentInChildren<ChildTextUpdater>();

            var hunger = _basicUI.FindChild("Hunger");
            _hungerText = hunger.GetComponentInChildren<ChildTextUpdater>();

            var thirst = _basicUI.FindChild("Thirst");
            _thirstText = thirst.GetComponentInChildren<ChildTextUpdater>();

            var energy = _basicUI.FindChild("Energy");
            _energyText = energy.GetComponentInChildren<ChildTextUpdater>();

            var quickAction1 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot01");
            _quickAction1Button = quickAction1.GetComponent<ChildButtonAction>();
            _quickAction1Icon = quickAction1.GetComponent<ChildIconUpdater>();
            _quickAction1Text = quickAction1.GetComponent<ChildTextUpdater>();

            var quickAction2 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot02");
            _quickAction2Button = quickAction2.GetComponent<ChildButtonAction>();
            _quickAction2Icon = quickAction2.GetComponent<ChildIconUpdater>();
            _quickAction2Text = quickAction2.GetComponent<ChildTextUpdater>();

            var quickAction3 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot03");
            _quickAction3Button = quickAction3.GetComponent<ChildButtonAction>();
            _quickAction3Icon = quickAction3.GetComponent<ChildIconUpdater>();
            _quickAction3Text = quickAction3.GetComponent<ChildTextUpdater>();

            var quickAction4 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot04");
            _quickAction4Button = quickAction4.GetComponent<ChildButtonAction>();
            _quickAction4Icon = quickAction4.GetComponent<ChildIconUpdater>();
            _quickAction4Text = quickAction4.GetComponent<ChildTextUpdater>();

            var inventoryPanel = _basicUI.FindChild("InventoryPanel");
            _openInventoryButton = inventoryPanel.GetComponentInChildren<ChildButtonAction>();
        }

        public void EnterState()
        {
            _openInventoryButton.Prepare(_sceneManager, OnClick_OpenInventory);

            _quickAction1Button.Prepare(_sceneManager, OnClick_QuickAction1Button);
            _quickAction2Button.Prepare(_sceneManager, OnClick_QuickAction2Button);
            _quickAction3Button.Prepare(_sceneManager, OnClick_QuickAction3Button);
            _quickAction4Button.Prepare(_sceneManager, OnClick_QuickAction4Button);

            _quickAction1Icon.Prepare("QuickActionButton");
            _quickAction2Icon.Prepare("QuickActionButton");
            _quickAction3Icon.Prepare("QuickActionButton");
            _quickAction4Icon.Prepare("QuickActionButton");

            _basicUI.SetActive(true);
        }

        public void UpdateState()
        {
            var currentDateTime = _dayNightCycle.CurrentDateTime;
            var playerVitalitySystem = _mainCharacterManager.GetVitalitySystem();

            _datetimeText.UpdateUI(currentDateTime.ToString("g"));

            _healthText.UpdateUI(playerVitalitySystem.Health.ToString("0"));
            _hungerText.UpdateUI(playerVitalitySystem.Hunger.ToString("0"));
            _thirstText.UpdateUI(playerVitalitySystem.Thirst.ToString("0"));
            _energyText.UpdateUI(playerVitalitySystem.Energy.ToString("0"));

            _quickAction1Icon.UpdateUI(_mainCharacterManager.GetInventorySlot(0));
            _quickAction2Icon.UpdateUI(_mainCharacterManager.GetInventorySlot(1));
            _quickAction3Icon.UpdateUI(_mainCharacterManager.GetInventorySlot(2));
            _quickAction4Icon.UpdateUI(_mainCharacterManager.GetInventorySlot(3));

            _quickAction1Text.UpdateUI(_mainCharacterManager.GetInventorySlot(0).CurrentAmount.ToString());
            _quickAction2Text.UpdateUI(_mainCharacterManager.GetInventorySlot(1).CurrentAmount.ToString());
            _quickAction3Text.UpdateUI(_mainCharacterManager.GetInventorySlot(2).CurrentAmount.ToString());
            _quickAction4Text.UpdateUI(_mainCharacterManager.GetInventorySlot(3).CurrentAmount.ToString());
        }

        public void ExitState() 
            => _basicUI.SetActive(false);

        private void OnClick_OpenInventory() 
            => _uiManager.EnterInventoryState();

        private void OnClick_QuickAction1Button() 
            => _mainCharacterManager.OnClick_QuickActionButtonPressed(_mainCharacterManager.GetInventorySlot(0));

        private void OnClick_QuickAction2Button()
            => _mainCharacterManager.OnClick_QuickActionButtonPressed(_mainCharacterManager.GetInventorySlot(1));

        private void OnClick_QuickAction3Button()
            => _mainCharacterManager.OnClick_QuickActionButtonPressed(_mainCharacterManager.GetInventorySlot(2));

        private void OnClick_QuickAction4Button()
            => _mainCharacterManager.OnClick_QuickActionButtonPressed(_mainCharacterManager.GetInventorySlot(3));
    }
}