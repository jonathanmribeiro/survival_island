using SurvivalIsland.Common.Utils;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class CameraManager : MonoBehaviour
    {
        private SmoothFollow _cameraSmoothFollow;

        private void Awake()
        {
            _cameraSmoothFollow = Camera.main.GetComponent<SmoothFollow>();
        }

        public void SetFollowingTarget(Transform target)
        {
            _cameraSmoothFollow.Target = target;
        }

        public void UpdateCamera()
        {
            _cameraSmoothFollow.UpdateSmoothFollow();
        }
    }
}