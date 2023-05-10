using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class GameplayUIManager : MonoBehaviour
    {
        private DayNightCycle _dayNightCycle;
        private MainCharacterManager _mainCharacterManager;

        private ChildTextUpdater _datetimeText;
        private ChildTextUpdater _healthText;
        private ChildTextUpdater _hungerText;
        private ChildTextUpdater _thirstText;
        private ChildTextUpdater _energyText;

        private ChildButtonAction _openInventoryButton;

        private void Awake()
        {
            var canvas = GameObject.Find("Canvas");
            
            _datetimeText = canvas
                .FindChild("DateTimePanel")
                .GetComponentInChildren<ChildTextUpdater>();

            _healthText = canvas
                .FindChild("Health")
                .GetComponentInChildren<ChildTextUpdater>();

            _hungerText = canvas
                .FindChild("Hunger")
                .GetComponentInChildren<ChildTextUpdater>();

            _thirstText = canvas
                .FindChild("Thirst")
                .GetComponentInChildren<ChildTextUpdater>();

            _energyText = canvas
                .FindChild("Energy")
                .GetComponentInChildren<ChildTextUpdater>();

            _openInventoryButton = canvas
                .FindChild("InventoryPanel")
                .GetComponentInChildren<ChildButtonAction>();
        }
        
        public void Prepare
        (
            MainCharacterManager mainCharacterManager, 
            DayNightCycle dayNightCycle,
            MainCharacterInventoryUIManager mainCharacterInventoryUIHandler
        )
        {
            _dayNightCycle = dayNightCycle;
            _mainCharacterManager = mainCharacterManager;

            _openInventoryButton.Prepare(mainCharacterInventoryUIHandler.OnClick_OpenInventory);
        }

        public void UpdateUI()
        {
            var currentDateTime = _dayNightCycle.GetCurrentTime();
            var playerVitalitySystem = _mainCharacterManager.VitalityManager.VitalitySystem;

            _datetimeText.UpdateUI(currentDateTime.ToString("g"));
            
            _healthText.UpdateUI(playerVitalitySystem.Health.ToString("0"));
            _hungerText.UpdateUI(playerVitalitySystem.Hunger.ToString("0"));
            _thirstText.UpdateUI(playerVitalitySystem.Thirst.ToString("0"));
            _energyText.UpdateUI(playerVitalitySystem.Energy.ToString("0"));
        }
    }
}