using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class HarvestingState : StateBase
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

        public override void EnterState()
        {
            _canopy.SetActive(true);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            if (!_treeProps.TimeEnteredHarvestingState.HasValue)
                _treeProps.TimeEnteredHarvestingState = _dayNightCycle.CurrentTime;

            _manager.TreeInventory.ForceAmount(InventoryItemType.Leaf, _treeProps.MaxLeavesAmount);
            _manager.TreeInventory.ForceAmount(InventoryItemType.Wood, _treeProps.MaxWoodAmount);
        }

        public override void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredHarvestingState.Value.Add(_treeProps.TimeNeededInHarvestingState);

            if (_dayNightCycle.CurrentTime >= nextStateTime)
            {
                _manager.EnterFruitfullState();
            }
        }

        public override void ExitState()
        {
            _treeProps.TimeEnteredHarvestingState = null;
        }

        public override PlayerActionTypes GetAction() => _playerActionToExecute;

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            InventoryItemModel randomItem = TryGetRandomItem();

            if (randomItem != null)
            {
                switch (randomItem.Type)
                {
                    case InventoryItemType.Leaf:
                        _playerActionToExecute = PlayerActionTypes.Collecting;
                        _leavesParticleSystem.TryPlay();
                        break;
                    case InventoryItemType.Wood:
                        _playerActionToExecute = PlayerActionTypes.Chopping;
                        _woodParticleSystem.TryPlay();
                        break;
                    default:
                        return;
                }

                var actionExecutedSuccessfully = playerActionCallback.Invoke(_playerActionToExecute, randomItem);

                if (actionExecutedSuccessfully)
                {
                    _manager.TreeInventory.Remove(randomItem);
                }
            }

            randomItem = TryGetRandomItem();

            if (randomItem == null)
                _manager.EnterTrunkState();
        }

        public InventoryItemModel TryGetRandomItem() => _manager.TreeInventory.ObtainRandom(InventoryItemType.Leaf) 
            ?? _manager.TreeInventory.ObtainRandom(InventoryItemType.Wood);
    }
}