using SurvivalIsland.Common.Constants;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public class SmoothFollow : MonoBehaviour
    {
        public Transform Target;

        public Vector3 Velocity = Vector3.zero;
        private float SelfZPosition => transform.position.z;

        public void UpdateSmoothFollow()
        {
            if (Target == null) return;

            var newPosition = new Vector3(Target.position.x, Target.position.y, SelfZPosition);

            transform.position = Vector3.SmoothDamp(
                transform.position,
                newPosition,
                ref Velocity,
                SceneConstants.CAMERA_FOLLOW_SMOOTH_TIME);
        }
    }
}