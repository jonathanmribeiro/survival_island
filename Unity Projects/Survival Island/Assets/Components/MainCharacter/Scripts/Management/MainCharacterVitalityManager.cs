using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterVitalityManager : MonoBehaviour
    {
        private DayNightCycle _dayNightCycle;
        private DateTime _lastDateTime;
        public VitalitySystemModel VitalitySystem { get; private set; }

        public void Prepare(DayNightCycle dayNightCycle)
        {
            VitalitySystem = new();
            _dayNightCycle = dayNightCycle;
        }

        public void UpdateVitality()
        {
            var currentDateTime = _dayNightCycle.GetCurrentTime();

            if ((currentDateTime - _lastDateTime).TotalMinutes > 60)
            {
                VitalitySystem.IncreasePhysiologicalNeeds();
                _lastDateTime = currentDateTime;
            }
        }
    }
}