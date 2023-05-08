using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterVitalityManager : MonoBehaviour
    {
        private DayNightCycle _dayNightCycle;
        private DateTime _lastDateTime;
        internal VitalitySystemModel VitalitySystem { get; private set; }

        internal void Prepare(DayNightCycle dayNightCycle)
        {
            VitalitySystem = new();
            _dayNightCycle = dayNightCycle;
        }

        internal void UpdateVitality()
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