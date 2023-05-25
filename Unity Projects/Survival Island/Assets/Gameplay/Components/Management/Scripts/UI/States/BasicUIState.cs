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

        private GameObject InventoryPanel;
        private ChildButtonAction _openInventoryButton;

        public BasicUIState(GameplayUIManager uiManager,
                            MainCharacterManager mainCharacterManager,
                            DayNightCycle dayNightCycle)
        {
            _mainCharacterManager = mainCharacterManager;
            _dayNightCycle = dayNightCycle;
            _uiManager = uiManager;

            var canvas = GameObject.Find("Canvas");

            DateTimePanel = canvas.FindChild("DateTimePanel");
            Health = canvas.FindChild("Health");
            Hunger = canvas.FindChild("Hunger");
            Thirst = canvas.FindChild("Thirst");
            Energy = canvas.FindChild("Energy");
            InventoryPanel = canvas.FindChild("InventoryPanel");

            _datetimeText = DateTimePanel.GetComponentInChildren<ChildTextUpdater>();
            _healthText = Health.GetComponentInChildren<ChildTextUpdater>();
            _hungerText = Hunger.GetComponentInChildren<ChildTextUpdater>();
            _thirstText = Thirst.GetComponentInChildren<ChildTextUpdater>();
            _energyText = Energy.GetComponentInChildren<ChildTextUpdater>();
            _openInventoryButton = InventoryPanel.GetComponentInChildren<ChildButtonAction>();

            _openInventoryButton.Prepare(OnClick_InventoryPanel);
        }

        public void EnterState()
        {
            DateTimePanel.GetComponent<CanvasGroup>().alpha = 1.0f;
            Health.GetComponent<CanvasGroup>().alpha = 1.0f;
            Hunger.GetComponent<CanvasGroup>().alpha = 1.0f;
            Thirst.GetComponent<CanvasGroup>().alpha = 1.0f;
            Energy.GetComponent<CanvasGroup>().alpha = 1.0f;
            InventoryPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
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
        }

        public void ExitState()
        {
            DateTimePanel.GetComponent<CanvasGroup>().alpha = 0.0f;
            Health.GetComponent<CanvasGroup>().alpha = 0.0f;
            Hunger.GetComponent<CanvasGroup>().alpha = 0.0f;
            Thirst.GetComponent<CanvasGroup>().alpha = 0.0f;
            Energy.GetComponent<CanvasGroup>().alpha = 0.0f;
            InventoryPanel.GetComponent<CanvasGroup>().alpha = 0.0f;
        }

        private void OnClick_InventoryPanel()
        {
            _uiManager.EnterInventoryState();
        }
    }
}