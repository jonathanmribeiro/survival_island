using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Common.Management
{
    internal class AxisInputHandler : InputHandlerBase
    {
        internal override void UpdateInput()
        {
            InputModel.Vertical = Input.GetAxis(InputConstants.VERTICAL);
            InputModel.Horizontal = Input.GetAxis(InputConstants.HORIZONTAL);
        }
    }
}