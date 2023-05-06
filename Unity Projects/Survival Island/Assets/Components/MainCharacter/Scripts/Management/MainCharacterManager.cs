using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterManager : MonoBehaviour
    {
        private MainCharacterMovementManager _movementManager;
        private MainCharacterAnimationManager _animationManager;
        private MainCharacterVitalityManager _vitalityManager;

        private void Awake()
        {
            _movementManager = GetComponent<MainCharacterMovementManager>();
            _animationManager = GetComponent<MainCharacterAnimationManager>();
            _vitalityManager = GetComponent<MainCharacterVitalityManager>();
        }

        internal void Prepare()
        {
            _vitalityManager.Prepare();
        }

        internal void UpdateMainCharacter(InputModel inputModel, DateTime currentDateTime)
        {
            _movementManager.UpdateMovement(inputModel);
            _animationManager.UpdateMovement(inputModel);

            _vitalityManager.UpdateVitality(currentDateTime);
        }

        internal VitalitySystemModel GetVitalitySystem()
        {
            return _vitalityManager.VitalitySystem;
        }
    }
}