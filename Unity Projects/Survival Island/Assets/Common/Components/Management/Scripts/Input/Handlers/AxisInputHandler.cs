using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Management
{
    public class AxisInputHandler : InputHandlerBase
    {
        public AxisInputHandler(Action actionToExecute)
        {
            ActionToExecute = actionToExecute;
        }

        public override void UpdateInput()
        {
            InputModel.Vertical = Input.GetAxis(InputConstants.VERTICAL);
            InputModel.Horizontal = Input.GetAxis(InputConstants.HORIZONTAL);

            if (Input.GetButtonUp(InputConstants.FIRE1))
                ActionCaptured();
        }
    }
}