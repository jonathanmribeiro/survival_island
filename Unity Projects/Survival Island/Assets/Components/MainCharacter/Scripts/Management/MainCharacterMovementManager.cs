using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Management;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterMovementManager : MonoBehaviour
    {
        private InputManager _inputManager;

        public void Prepare(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public void UpdateMovement()
        {
            var currentInput = _inputManager.GetCurrentInput();

            var direction = new Vector3(currentInput.Horizontal, currentInput.Vertical);
            direction *= MainCharacterConstants.SPEED;
            direction *= Time.deltaTime;

            transform.Translate(direction);
        }
    }
}