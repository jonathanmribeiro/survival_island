using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Management;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterAnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private InputManager _inputManager;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        internal void Prepare(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        internal void UpdateMovement()
        {
            var currentInput = _inputManager.GetCurrentInput();

            _animator.SetBool(AnimatorVariablesConstants.MOVEEAST, currentInput.MovingEast);
            _animator.SetBool(AnimatorVariablesConstants.MOVENORTH, currentInput.MovingNorth);
            _animator.SetBool(AnimatorVariablesConstants.MOVESOUTH, currentInput.MovingSouth);
            _animator.SetBool(AnimatorVariablesConstants.MOVEWEST, currentInput.MovingWest);
        }

    }
}