using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterManager : MonoBehaviour
    {
        private MainCharacterMovementManager _movementManager;
        private MainCharacterAnimationManager _animationManager;

        private void Awake()
        {
            _movementManager = GetComponent<MainCharacterMovementManager>();
            _animationManager = GetComponent<MainCharacterAnimationManager>();
        }

        internal void UpdateInput(InputModel inputModel)
        {
            _movementManager.UpdateMovement(inputModel);
            _animationManager.UpdateMovement(inputModel);
        }
    }
}