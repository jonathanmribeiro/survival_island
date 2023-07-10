using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class GoneState : StateBase
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;
        private readonly CircleCollider2D _circleCollider;

        private readonly TreeManager _manager;
        private readonly DayNightCycle _dayNightCycle;

        private readonly TreeProps _treeProps;

        public GoneState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;
            _treeProps = treeProps;
            _dayNightCycle = dayNightCycle;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
            _sapling = _manager.gameObject.FindChild("Sapling");
            _circleCollider = _manager.GetComponent<CircleCollider2D>();
        }

        public override void EnterState()
        {
            _canopy.SetActive(false);
            _trunk.SetActive(false);
            _sapling.SetActive(false);
            _circleCollider.enabled = false;

            if (!_treeProps.TimeEnteredGoneState.HasValue)
                _treeProps.TimeEnteredGoneState = _dayNightCycle.CurrentDateTime;
        }

        public override void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredGoneState.Value.Add(_treeProps.TimeNeededInGoneState);

            if (_dayNightCycle.CurrentDateTime >= nextStateTime)
            {
                _manager.EnterGrowingState();
            }
        }

        public override void ExitState()
        {
            _treeProps.TimeEnteredGoneState = null;
            _circleCollider.enabled = true;
        }
    }
}