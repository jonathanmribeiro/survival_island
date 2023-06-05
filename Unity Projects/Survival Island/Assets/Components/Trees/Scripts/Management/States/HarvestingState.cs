﻿using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class HarvestingState : PlayerDetectionBase, IState
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

        private readonly TreeManager _manager;
        private readonly DayNightCycle _dayNightCycle;

        private readonly TreeProps _treeProps;
        private readonly ParticleSystem _leavesParticleSystem;
        private readonly ParticleSystem _woodParticleSystem;

        private PlayerActionTypes _playerActionToExecute;

        public HarvestingState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;
            _treeProps = treeProps;
            _dayNightCycle = dayNightCycle;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
            _sapling = _manager.gameObject.FindChild("Sapling");

            _leavesParticleSystem = _canopy.GetComponent<ParticleSystem>();
            _woodParticleSystem = _trunk.GetComponent<ParticleSystem>();
        }

        public void EnterState()
        {
            _canopy.SetActive(true);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            if (!_treeProps.TimeEnteredHarvestingState.HasValue)
                _treeProps.TimeEnteredHarvestingState = _dayNightCycle.CurrentTime;
        }

        public void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredHarvestingState.Value.Add(_treeProps.TimeNeededInHarvestingState);

            if (_dayNightCycle.CurrentTime >= nextStateTime)
            {
                _manager.EnterFruitfullState();
            }
        }

        public void ExitState()
        {
            _treeProps.TimeEnteredHarvestingState = null;
        }

        public PlayerActionTypes GetAction() => _playerActionToExecute;

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = _manager.ObtainRandom(InventoryItemType.Leaf)
                ?? _manager.ObtainRandom(InventoryItemType.Wood);

            if (randomItem != null)
            {
                switch (randomItem.Type)
                {
                    case InventoryItemType.Leaf:
                        _playerActionToExecute = PlayerActionTypes.Collecting;
                        if (!_leavesParticleSystem.isPlaying)
                            _leavesParticleSystem.Play();
                        break;
                    case InventoryItemType.Wood:
                        _playerActionToExecute = PlayerActionTypes.Chopping;
                        if (!_woodParticleSystem.isPlaying)
                            _woodParticleSystem.Play();
                        break;
                }

                var actionExecutedSuccessfully = playerActionCallback.Invoke(_playerActionToExecute, randomItem);

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