using SurvivalIsland.Common.Extensions;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public static class AreaInstantiator
    {
        public static Transform Instantiate(PolygonCollider2D area, Transform initialPoint, Transform prefab)
        {
            var position = area.GetPointWithinArea(initialPoint);

            var rotation = Quaternion.Euler(Vector3.zero);

            var newInstance = UnityEngine.Object.Instantiate(prefab, position, rotation, initialPoint);

            return newInstance;
        }
    }
}