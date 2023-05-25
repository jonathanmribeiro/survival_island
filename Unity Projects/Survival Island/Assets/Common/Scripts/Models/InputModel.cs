using SurvivalIsland.Common.Constants;

namespace SurvivalIsland.Common.Models
{
    public class InputModel
    {
        public float Vertical { get; set; }
        public float Horizontal { get; set; }

        public bool MovingNorth { get { return Vertical > InputConstants.AXIS_THRESHOLD; } }
        public bool MovingSouth { get { return Vertical < -InputConstants.AXIS_THRESHOLD; } }
        public bool MovingEast { get { return Horizontal > InputConstants.AXIS_THRESHOLD; } }
        public bool MovingWest { get { return Horizontal < -InputConstants.AXIS_THRESHOLD; } }
    }
}
