using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Gameplay.Management;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Management
{
    public class InputManager : MonoBehaviour
    {
        InputHandlerBase _inputHandler;

        public void Prepare(GameplaySceneManager gameplaySceneManager, InputType inputType, Action actionToExecute)
        {
            switch (inputType)
            {
                case InputType.Axis:
                    _inputHandler = new AxisInputHandler(actionToExecute);
                    break;
                case InputType.Virtual:
                    _inputHandler = new VirtualInputHandler(gameplaySceneManager, actionToExecute);
                    break;
            }
        }

        public void UpdateInput()
        {
            _inputHandler.UpdateInput();
        }

        public InputModel GetCurrentInput()
        {
            return _inputHandler.InputModel;
        }
    }
}