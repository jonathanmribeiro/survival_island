using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class GrowingState : StateBase
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

        private readonly TreeManager _manager;
        private readonly DayNightCycle _dayNightCycle;

        private readonly TreeProps _treeProps;

        private readonly ParticleSystem _woodParticleSystem;

        public GrowingState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;
            _treeProps = treeProps;
            _dayNightCycle = dayNightCycle;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
            _sapling = _manager.gameObject.FindChild("Sapling");

            _woodParticleSystem = _trunk.GetComponent<ParticleSystem>();
        }

        public override void EnterState()
        {
            _canopy.SetActive(false);
            _trunk.SetActive(false);
            _sapling.SetActive(true);

            _manager.TreeInventory.ForceAmount(InventoryItemType.Wood, _treeProps.MaxWoodAmount / 4);

            if (!_treeProps.TimeEnteredGrowingState.HasValue)
                _treeProps.TimeEnteredGrowingState = _dayNightCycle.CurrentTime;
        }

        public override void UpdateState()
        {
            DateTime nextStateTime = _treeProps.TimeEnteredGrowingState.Value.Add(_treeProps.TimeNeededInGoneState);

            if (_dayNightCycle.CurrentTime >= nextStateTime)
            {
                _manager.EnterHarvestingState();
            }
        }

        public override void ExitState()
        {
            _treeProps.TimeEnteredGrowingState = null;
        }

        public override PlayerActionTypes GetAction() => PlayerActionTypes.Chopping;

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = _manager.TreeInventory.ObtainRandom(InventoryItemType.Wood);
            _woodParticleSystem.TryPlay();

            if (randomItem != null)
            {
                var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.Collecting, randomItem);

                if (actionExecutedSuccessfully)
                {
                    _manager.TreeInventory.Remove(randomItem);
                }
            }

            randomItem = _manager.TreeInventory.ObtainRandom(InventoryItemType.Wood);

            if (randomItem == null)
                _manager.EnterGoneState();
        }
    }
}