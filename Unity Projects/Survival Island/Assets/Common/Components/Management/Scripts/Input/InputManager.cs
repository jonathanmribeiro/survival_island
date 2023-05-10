using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Common.Management
{
    public class InputManager : MonoBehaviour
    {
        InputHandlerBase _inputHandler;

        public void Prepare(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Axis:
                    _inputHandler = new AxisInputHandler();
                    break;
                case InputType.Virtual:
                    _inputHandler = new VirtualInputHandler();
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