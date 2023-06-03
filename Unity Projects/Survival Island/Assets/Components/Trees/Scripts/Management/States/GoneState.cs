using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class GoneState : PlayerDetectionBase, ITreeState
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

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
        }

        public void EnterState()
        {
            _canopy.SetActive(false);
            _trunk.SetActive(false);
            _sapling.SetActive(false);

            if (!_treeProps.TimeEnteredGoneState.HasValue)
                _treeProps.TimeEnteredGoneState = _dayNightCycle.CurrentTime;
        }

        public void ExitState()
        {
            _treeProps.TimeEnteredGoneState = null;
        }

        public void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredGoneState.Value.Add(_treeProps.TimeNeededInGoneState);

            if (_dayNightCycle.CurrentTime >= nextStateTime)
            {
                _manager.EnterGrowingState();
            }
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback) {/*Left empty on purpose*/}
    }
}