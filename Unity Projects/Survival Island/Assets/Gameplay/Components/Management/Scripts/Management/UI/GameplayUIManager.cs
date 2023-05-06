using System;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    internal class GameplayUIManager : MonoBehaviour
    {
        private DateTimeUIHandler _dateTimeUIHandler;

        private void Awake()
        {
            var canvas = GameObject.Find("Canvas");
            _dateTimeUIHandler = canvas.GetComponentInChildren<DateTimeUIHandler>();
        }

        public void UpdateUI(DateTime datetime)
        {
            _dateTimeUIHandler.UpdateUI(datetime.ToString("g"));
        }
    }
}