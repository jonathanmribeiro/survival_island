using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class BasicUIState : IGameplayUIState
    {
        private readonly DayNightCycle _dayNightCycle;
        private readonly MainCharacterManager _mainCharacterManager;
        private readonly GameplayUIManager _uiManager;

        private GameObject _basicUI;

        private GameObject DateTimePanel;
        private ChildTextUpdater _datetimeText;

        private GameObject Health;
        private ChildTextUpdater _healthText;

        private GameObject Hunger;
        private ChildTextUpdater _hungerText;

        private GameObject Thirst;
        private ChildTextUpdater _thirstText;

        private GameObject Energy;
        private ChildTextUpdater _energyText;

        private GameObject QuickAction1;
        private ChildIconUpdater _quickAction1Icon;
        private ChildButtonAction _quickAction1Button;

        private GameObject QuickAction2;
        private ChildIconUpdater _quickAction2Icon;
        private ChildButtonAction _quickAction2Button;

        private GameObject QuickAction3;
        private ChildIconUpdater _quickAction3Icon;
        private ChildButtonAction _quickAction3Button;

        private GameObject QuickAction4;
        private ChildIconUpdater _quickAction4Icon;
        private ChildButtonAction _quickAction4Button;

        private GameObject InventoryPanel;
        private ChildButtonAction _openInventoryButton;

        public BasicUIState(GameplayUIManager uiManager,
                            MainCharacterManager mainCharacterManager,
                            DayNightCycle dayNightCycle)
        {
            _mainCharacterManager = mainCharacterManager;
            _dayNightCycle = dayNightCycle;
            _uiManager = uiManager;

            _basicUI = GameObject.Find("Canvas").FindChild("BasicUI");

            DateTimePanel = _basicUI.FindChild("DateTimePanel");
            Health = _basicUI.FindChild("Health");
            Hunger = _basicUI.FindChild("Hunger");
            Thirst = _basicUI.FindChild("Thirst");
            Energy = _basicUI.FindChild("Energy");

            QuickAction1 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot01");
            QuickAction2 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot02");
            QuickAction3 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot03");
            QuickAction4 = _basicUI.FindChild("QuickUsePanel").FindChild("QuickUseSlot04");

            InventoryPanel = _basicUI.FindChild("InventoryPanel");

            _datetimeText = DateTimePanel.GetComponentInChildren<ChildTextUpdater>();

            _healthText = Health.GetComponentInChildren<ChildTextUpdater>();
            _hungerText = Hunger.GetComponentInChildren<ChildTextUpdater>();
            _thirstText = Thirst.GetComponentInChildren<ChildTextUpdater>();
            _energyText = Energy.GetComponentInChildren<ChildTextUpdater>();

            _quickAction1Button = QuickAction1.GetComponentInChildren<ChildButtonAction>();
            _quickAction2Button = QuickAction2.GetComponentInChildren<ChildButtonAction>();
            _quickAction3Button = QuickAction3.GetComponentInChildren<ChildButtonAction>();
            _quickAction4Button = QuickAction4.GetComponentInChildren<ChildButtonAction>();

            _quickAction1Icon = QuickAction1.GetComponentInChildren<ChildIconUpdater>();
            _quickAction2Icon = QuickAction2.GetComponentInChildren<ChildIconUpdater>();
            _quickAction3Icon = QuickAction3.GetComponentInChildren<ChildIconUpdater>();
            _quickAction4Icon = QuickAction4.GetComponentInChildren<ChildIconUpdater>();

            _openInventoryButton = InventoryPanel.GetComponentInChildren<ChildButtonAction>();

            _basicUI.GetComponent<CanvasGroup>().alpha = 1.0f;
        }

        public void EnterState()
        {
            _openInventoryButton.Prepare(OnClick_InventoryPanel);

            _quickAction1Button.Prepare(OnClick_QuickAction1Button);
            _quickAction2Button.Prepare(OnClick_QuickAction2Button);
            _quickAction3Button.Prepare(OnClick_QuickAction3Button);
            _quickAction4Button.Prepare(OnClick_QuickAction4Button);

            _quickAction1Icon.Prepare("QuickActionButton");
            _quickAction2Icon.Prepare("QuickActionButton");
            _quickAction3Icon.Prepare("QuickActionButton");
            _quickAction4Icon.Prepare("QuickActionButton");
        }

        public void UpdateState()
        {
            var currentDateTime = _dayNightCycle.GetCurrentTime();
            var playerVitalitySystem = _mainCharacterManager.GetVitalitySystem();

            _datetimeText.UpdateUI(currentDateTime.ToString("g"));

            _healthText.UpdateUI(playerVitalitySystem.Health.ToString("0"));
            _hungerText.UpdateUI(playerVitalitySystem.Hunger.ToString("0"));
            _thirstText.UpdateUI(playerVitalitySystem.Thirst.ToString("0"));
            _energyText.UpdateUI(playerVitalitySystem.Energy.ToString("0"));

            _quickAction1Icon.UpdateUI(_mainCharacterManager.GetInventoryItem(0));
            _quickAction2Icon.UpdateUI(_mainCharacterManager.GetInventoryItem(1));
            _quickAction3Icon.UpdateUI(_mainCharacterManager.GetInventoryItem(2));
            _quickAction4Icon.UpdateUI(_mainCharacterManager.GetInventoryItem(3));
        }

        public void ExitState()
        {
            _basicUI.GetComponent<CanvasGroup>().alpha = 0.0f;
        }

        private void OnClick_InventoryPanel()
        {
            _uiManager.EnterInventoryState();
        }

        private void OnClick_QuickAction1Button()
        {
            _mainCharacterManager.OnClick_QuickAction1Button();
        }

        private void OnClick_QuickAction2Button()
        {
            _mainCharacterManager.OnClick_QuickAction2Button();
        }

        private void OnClick_QuickAction3Button()
        {
            _mainCharacterManager.OnClick_QuickAction3Button();
        }

        private void OnClick_QuickAction4Button()
        {
            _mainCharacterManager.OnClick_QuickAction4Button();
        }

    }
}