using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    internal class MainCharacterAnimationManager : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        internal void UpdateMovement(InputModel inputModel)
        {
            _animator.SetBool(AnimatorVariables.MOVEEAST, inputModel.MovingEast);
            _animator.SetBool(AnimatorVariables.MOVENORTH, inputModel.MovingNorth);
            _animator.SetBool(AnimatorVariables.MOVESOUTH, inputModel.MovingSouth);
            _animator.SetBool(AnimatorVariables.MOVEWEST, inputModel.MovingWest);
        }
    }
}