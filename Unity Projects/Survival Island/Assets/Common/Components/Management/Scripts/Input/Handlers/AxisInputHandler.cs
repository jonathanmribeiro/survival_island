using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Common.Management
{
    public class AxisInputHandler : InputHandlerBase
    {
        public override void UpdateInput()
        {
            InputModel.Vertical = Input.GetAxis(InputConstants.VERTICAL);
            InputModel.Horizontal = Input.GetAxis(InputConstants.HORIZONTAL);
        }
    }
}