using UnityEngine;
using UnityEngine.EventSystems;

namespace SurvivalIsland.Common.Management
{
    internal class VirtualInputHandler : InputHandlerBase
    {
        private Vector2 _pointA;
        private Vector2 _pointB;


        internal override void UpdateInput()
        {
            if (!ShouldCheckInput())
            {
                _pointA = Vector2.zero;
                _pointB = Vector2.zero;

                return;
            }

            CheckInput();
            UpdateDirection();
        }

        private bool ShouldCheckInput()
        {
            if (Input.GetMouseButtonUp(0))
            {
                return false;
            }
            
            return !EventSystem.current.IsPointerOverGameObject();
        }

        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _pointA = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButton(0))
            {
                _pointB = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        private void UpdateDirection()
        {
            Vector2 offset = _pointB - _pointA;
            var direction = Vector2.ClampMagnitude(offset, 1.0f);

            InputModel.Vertical = direction.y;
            InputModel.Horizontal = direction.x;
        }
    }
}
