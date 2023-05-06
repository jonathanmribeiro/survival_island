using SurvivalIsland.Common.Extensions;
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

        public void UpdateUI(DateTime datetime)
        {
            _datetimeText.UpdateUI(datetime.ToString("g"));
            
            _healthText.UpdateUI("99");
            _hungerText.UpdateUI("99");
            _thirstText.UpdateUI("99");
            _energyText.UpdateUI("99");
        }
    }
}