using SurvivalIsland.Common.Constants;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SurvivalIsland.Common.Management
{
    public class VirtualInputHandler : InputHandlerBase
    {
        private Vector2 _pointA;
        private Vector2 _pointB;
        private bool _isMoving;

        public VirtualInputHandler(Action actionToExecute)
        {
            ActionToExecute = actionToExecute;
        }

        public override void UpdateInput()
        {
            VerifyAction();

            if (IsPointerOverUI() || Input.GetMouseButtonUp(0))
            {
                _pointA = Vector2.zero;
                _pointB = Vector2.zero;
            }
            else
            {
                CheckInput();
            }

            UpdateDirection();
        }

        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void VerifyAction()
        {
            if (!_isMoving && Input.GetMouseButtonUp(0) && !IsPointerOverUI())
            {
                ActionCaptured();
            }
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

            _isMoving = Vector2.Distance(_pointB, _pointA) >= InputConstants.AXIS_THRESHOLD;

            InputModel.Vertical = direction.y;
            InputModel.Horizontal = direction.x;
        }
    }
}
