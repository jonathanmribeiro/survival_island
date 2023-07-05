using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class FruitfullState : StateBase
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

        private readonly TreeManager _manager;
        private readonly DayNightCycle _dayNightCycle;

        private readonly PolygonCollider2D _fruitArea;
        private readonly TreeProps _treeProps;

        private readonly List<Transform> _fruitInstances;
        private bool _hasMaximumAmountOfFruits;

        private readonly ParticleSystem _leavesParticleSystem;

        public FruitfullState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;
            _treeProps = treeProps;
            _dayNightCycle = dayNightCycle;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
            _sapling = _manager.gameObject.FindChild("Sapling");

            _leavesParticleSystem = _canopy.GetComponent<ParticleSystem>();

            _fruitArea = _canopy.GetComponent<PolygonCollider2D>();
            _fruitInstances = new();
        }

        public override void EnterState()
        {
            _canopy.SetActive(true);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            if (!_treeProps.TimeEnteredFruitfullState.HasValue)
                _treeProps.TimeEnteredFruitfullState = _dayNightCycle.CurrentTime;

            VerifyMaximumAmountOfFruit();
            PopulateFruitArea();
        }

        public override void UpdateState()
        {
            if (_hasMaximumAmountOfFruits)
                return;

            DateTime respawnFruitTime = _treeProps.TimeEnteredFruitfullState.Value.Add(_treeProps.TimeNeededToSpawnFruit);

            if (_dayNightCycle.CurrentTime >= respawnFruitTime)
            {
                _manager.TreeInventory.TryAddItem(_treeProps.FruitType);
                VerifyMaximumAmountOfFruit();
                PopulateFruitArea();
                _treeProps.TimeEnteredFruitfullState = _dayNightCycle.CurrentTime;
            }
        }

        public override void ExitState()
        {
            _treeProps.TimeEnteredFruitfullState = null;
        }

        public override PlayerActionTypes GetAction()
            => PlayerActionTypes.Collecting;

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            _leavesParticleSystem.TryPlay();

            var randomItem = _manager.TreeInventory.ObtainRandom(_treeProps.FruitType);

            if (randomItem == null)
                return;

            var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.Collecting, randomItem);

            if (actionExecutedSuccessfully)
            {
                _manager.TreeInventory.Remove(randomItem);
                var instanceToRemove = _fruitInstances.First();
                UnityEngine.Object.Destroy(instanceToRemove.gameObject);
                _fruitInstances.Remove(instanceToRemove);
                VerifyMaximumAmountOfFruit();
            }

            if (!_fruitInstances.Any())
            {
                _manager.EnterHarvestingState();
            }
        }

        private void PopulateFruitArea()
        {
            var totalFruitAmount = _manager.TreeInventory.CountItemsOfType(_treeProps.FruitType);
            var currentInstantiatedFruits = _fruitInstances.Count;

            for (int i = currentInstantiatedFruits; i < totalFruitAmount; i++)
            {
                _fruitInstances.Add(AreaInstantiator.Instantiate(_fruitArea, _canopy.transform, _treeProps.FruitPrefab));
            }
        }

        private void VerifyMaximumAmountOfFruit()
            => _hasMaximumAmountOfFruits = _manager.TreeInventory.CountItemsOfType(_treeProps.FruitType).Equals(_treeProps.MaxFruitAmount);
    }
}