using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterMovementManager : MonoBehaviour
    {
        internal void UpdateMovement(InputModel inputModel)
        {
            var direction = new Vector3(inputModel.Horizontal, inputModel.Vertical);
            direction *= MainCharacterConstants.SPEED;
            direction *= Time.deltaTime;

            transform.Translate(direction);
        }
    }
}