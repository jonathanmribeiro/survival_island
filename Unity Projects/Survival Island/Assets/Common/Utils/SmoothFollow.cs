using SurvivalIsland.Common.Constants;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    internal class SmoothFollow : MonoBehaviour
    {
        public Transform Target;

        private Vector3 _velocity = Vector3.zero;

        public void UpdateSmoothFollow()
        {
            if (Target == null) return;

            var selfPositionZ = transform.position.z;
            var newPosition = new Vector3(Target.position.x, Target.position.y, selfPositionZ);

            transform.position = Vector3.SmoothDamp(
                transform.position,
                newPosition,
                ref _velocity,
                SceneConstants.CAMERA_FOLLOW_SMOOTH_TIME);
        }
    }
}