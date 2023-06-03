using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using System.Linq;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class HarvestingState : PlayerDetectionBase, ITreeState
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

        private readonly TreeManager _manager;
        private readonly DayNightCycle _dayNightCycle;

        private readonly TreeProps _treeProps;

        public HarvestingState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
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
            _canopy.SetActive(true);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            if (!_treeProps.TimeEnteredHarvestingState.HasValue)
                _treeProps.TimeEnteredHarvestingState = _dayNightCycle.CurrentTime;
        }

        public void ExitState()
        {
            _treeProps.TimeEnteredHarvestingState = null;
        }

        public void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredHarvestingState.Value.Add(_treeProps.TimeNeededInHarvestingState);

            if (_dayNightCycle.CurrentTime >= nextStateTime)
            {
                _manager.EnterFruitfullState();
            }
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = _manager.ObtainRandom(InventoryItemType.Leaf)
                ?? _manager.ObtainRandom(InventoryItemType.Wood);

            if (randomItem != null)
            {
                var actionToExecute = randomItem.Type switch
                {
                    InventoryItemType.Leaf => PlayerActionTypes.CollectingLeaves,
                    InventoryItemType.Wood => PlayerActionTypes.CollectingWood,
                    _ => PlayerActionTypes.None
                };

                var actionExecutedSuccessfully = playerActionCallback.Invoke(actionToExecute, randomItem);

                if (actionExecutedSuccessfully)
                {
                    _manager.Remove(randomItem);
                    _treeProps.ReduceCurrentAmount(randomItem.Type);
                }
            }

            randomItem = _manager.ObtainRandom(InventoryItemType.Leaf)
                ?? _manager.ObtainRandom(InventoryItemType.Wood);
            
            if (randomItem == null)
                _manager.EnterTrunkState();
        }
    }
}