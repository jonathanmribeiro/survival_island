﻿using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class FruitfullState : PlayerDetectionBase, ITreeState
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

        public FruitfullState(TreeManager manager, TreeProps treeProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;
            _treeProps = treeProps;
            _dayNightCycle = dayNightCycle;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
            _sapling = _manager.gameObject.FindChild("Sapling");

            _fruitArea = _canopy.GetComponent<PolygonCollider2D>();
            _fruitInstances = new();
        }

        public void EnterState()
        {
            _canopy.SetActive(true);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            if (!_treeProps.TimeEnteredFruitfullState.HasValue)
                _treeProps.TimeEnteredFruitfullState = _dayNightCycle.CurrentTime;

            VerifyMaximumAmountOfFruit();
            PopulateFruitArea();
        }

        public void ExitState()
        {
            _treeProps.TimeEnteredFruitfullState = null;
        }

        public void UpdateState()
        {
            if (_hasMaximumAmountOfFruits)
                return;

            DateTime respawnFruitTime = _treeProps.TimeEnteredFruitfullState.Value.Add(_treeProps.TimeNeededToSpawnFruit);

            if (_dayNightCycle.CurrentTime >= respawnFruitTime)
            {
                _manager.TryAddItem(_treeProps.FruitType);
                VerifyMaximumAmountOfFruit();
                PopulateFruitArea();
                _treeProps.TimeEnteredFruitfullState = _dayNightCycle.CurrentTime;
            }
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = _manager.ObtainRandom(_treeProps.FruitType);

            if (randomItem == null)
                return;

            var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.CollectingWood, randomItem);

            if (actionExecutedSuccessfully)
            {
                _manager.Remove(randomItem);
                var instanceToRemove = _fruitInstances.First();
                UnityEngine.Object.Destroy(instanceToRemove.gameObject);
                _fruitInstances.Remove(instanceToRemove);
                _treeProps.ReduceCurrentAmount(_treeProps.FruitType);
                VerifyMaximumAmountOfFruit();
            }

            if (!_fruitInstances.Any())
            {
                _manager.EnterHarvestingState();
            }
        }

        private void PopulateFruitArea()
        {
            var totalFruitAmount = _manager.ObtainAll(_treeProps.FruitType).Count;
            var currentInstantiatedFruits = _fruitInstances.Count;

            for (int i = currentInstantiatedFruits; i < totalFruitAmount; i++)
            {
                _fruitInstances.Add(AreaInstantiator.Instantiate(_fruitArea, _canopy.transform, _treeProps.FruitPrefab));
            }
        }

        private void VerifyMaximumAmountOfFruit()
            => _hasMaximumAmountOfFruits = _manager.ObtainAll(_treeProps.FruitType).Count.Equals(_treeProps.MaxFruitAmount);
    }
}