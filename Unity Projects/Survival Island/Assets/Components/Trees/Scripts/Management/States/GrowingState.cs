﻿using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class GrowingState : PlayerDetectionBase, ITreeState
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

        private readonly TreeManager _manager;
        private readonly DayNightCycle _dayNightCycle;

        private readonly TreeProps _treeProps;

        public GrowingState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
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
            _sapling.SetActive(true);

            _manager.AddMultiple(InventoryItemType.Wood, _treeProps.MaxWoodAmount / 4);

            if (!_treeProps.TimeEnteredGrowingState.HasValue)
                _treeProps.TimeEnteredGrowingState = _dayNightCycle.CurrentTime;
        }

        public void ExitState()
        {
            _treeProps.TimeEnteredGrowingState = null;
        }

        public void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredGrowingState.Value.Add(_treeProps.TimeNeededInGoneState);

            if (_dayNightCycle.CurrentTime >= nextStateTime)
            {
                _manager.EnterHarvestingState();
            }
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = _manager.ObtainRandom(InventoryItemType.Wood);

            if (randomItem != null)
            {
                var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.CollectingWood, randomItem);

                if (actionExecutedSuccessfully)
                {
                    _manager.Remove(randomItem);
                    _treeProps.ReduceCurrentAmount(InventoryItemType.Wood);
                }
            }

            randomItem = _manager.ObtainRandom(InventoryItemType.Wood);

            if (randomItem == null)
                _manager.EnterGoneState();
        }
    }
}