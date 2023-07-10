using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Extensions;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SurvivalIsland.Components.Fishing
{
    public class IdleState : StateBase
    {
        private readonly FishShadowManager _manager;
        private Vector3 _movementTarget;
        private readonly PolygonCollider2D _fishArea;
        private readonly Color _randomColor;

        public IdleState(FishShadowManager manager)
        {
            _manager = manager;
            _fishArea = _manager.transform.parent.GetComponent<PolygonCollider2D>();
            PrepareNewMovementTarget();

            _randomColor = Random.ColorHSV();
        }

        public override void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent<FishShadowManager>(out _))
                PrepareNewMovementTarget();
        }

        public override void UpdateState()
        {
            var distance = Math.Round(Vector3.Distance(_movementTarget, _manager.transform.position), 2);

            if (distance > 0.01d)
            {
                var direction = (_movementTarget - _manager.transform.position).normalized;
                _manager.transform.Translate(Time.deltaTime * direction, Space.World);
                _manager.transform.up = direction;

                Debug.DrawLine(_manager.transform.position, _movementTarget, _randomColor, 5);

                return;
            }

            PrepareNewMovementTarget();
        }

        private void PrepareNewMovementTarget() 
            => _movementTarget = _fishArea.GetPointWithinArea(_manager.transform);
    }
}