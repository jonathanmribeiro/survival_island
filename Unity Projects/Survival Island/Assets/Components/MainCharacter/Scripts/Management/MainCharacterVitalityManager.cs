using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterVitalityManager : MonoBehaviour
    {
        public VitalitySystemModel VitalitySystem { internal get; set; }

        private DateTime _lastDateTime;

        internal void Prepare()
        {
            VitalitySystem = new();
        }

        internal void UpdateVitality(DateTime currentDateTime)
        {
            if ((currentDateTime - _lastDateTime).TotalMinutes > 60)
            {
                VitalitySystem.IncreasePhysiologicalNeeds();
                _lastDateTime = currentDateTime;
            }
        }
    }
}