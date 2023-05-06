using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    internal class GameplayUIManager : MonoBehaviour
    {
        private ChildTextUpdater _datetimeText;
        private ChildTextUpdater _healthText;
        private ChildTextUpdater _hungerText;
        private ChildTextUpdater _thirstText;
        private ChildTextUpdater _energyText;

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
        }

        public void UpdateUI(DateTime datetime, VitalitySystemModel currentPlayerVitality)
        {
            _datetimeText.UpdateUI(datetime.ToString("g"));
            
            _healthText.UpdateUI(currentPlayerVitality.Health.ToString("0"));
            _hungerText.UpdateUI(currentPlayerVitality.Hunger.ToString("0"));
            _thirstText.UpdateUI(currentPlayerVitality.Thirst.ToString("0"));
            _energyText.UpdateUI(currentPlayerVitality.Energy.ToString("0"));
        }
    }
}