using UnityEngine;

namespace SurvivalIsland.Common.Extensions
{
    public static class PolygonCollider2DExtensions
    {
        public static Vector3 GetPointWithinArea(this PolygonCollider2D self, Transform initialPoint)
        {
            var withinPolygon = false;
            var calculatedPoint = Vector2.zero;

            while (!withinPolygon)
            {
                var randomPoint = Random.insideUnitCircle * 10;

                calculatedPoint = new Vector2(initialPoint.position.x, initialPoint.position.y) + (randomPoint * 2);

                withinPolygon = self.OverlapPoint(calculatedPoint);
            }

            return new Vector3(calculatedPoint.x, calculatedPoint.y, 0);
        }
    }
}