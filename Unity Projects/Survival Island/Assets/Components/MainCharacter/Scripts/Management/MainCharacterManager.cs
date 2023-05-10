using SurvivalIsland.Common.Management;
using SurvivalIsland.Common.Utils;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterManager : MonoBehaviour
    {
        private MainCharacterMovementManager _movementManager;
        private MainCharacterAnimationManager _animationManager;
        public MainCharacterVitalityManager VitalityManager { get; private set; }

        private void Awake()
        {
            _movementManager = GetComponent<MainCharacterMovementManager>();
            _animationManager = GetComponent<MainCharacterAnimationManager>();
            VitalityManager = GetComponent<MainCharacterVitalityManager>();
        }

        public void Prepare(InputManager inputManager, DayNightCycle dayNightCycle)
        {
            _movementManager.Prepare(inputManager);
            _animationManager.Prepare(inputManager);

            VitalityManager.Prepare(dayNightCycle);
        }

        public void UpdateMainCharacter()
        {
            _movementManager.UpdateMovement();
            _animationManager.UpdateMovement();

            VitalityManager.UpdateVitality();
        }
    }
}