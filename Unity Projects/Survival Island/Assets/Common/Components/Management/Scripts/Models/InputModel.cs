using SurvivalIsland.Common.Constants;

namespace SurvivalIsland.Common.Models
{
    internal class InputModel
    {
        internal float Vertical { get; set; }
        internal float Horizontal { get; set; }

        internal bool MovingNorth { get { return Vertical > Input.AXIS_TRESHOLD; } }
        internal bool MovingSouth { get { return Vertical < -Input.AXIS_TRESHOLD; } }
        internal bool MovingEast { get { return Horizontal > Input.AXIS_TRESHOLD; } }
        internal bool MovingWest { get { return Horizontal < -Input.AXIS_TRESHOLD; } }
    }
}
