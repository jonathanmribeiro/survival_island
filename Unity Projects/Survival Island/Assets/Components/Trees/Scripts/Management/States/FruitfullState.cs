using SurvivalIsland.Common.Bases;
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
        private readonly TreeManager _manager;

        private readonly PolygonCollider2D _fruitArea;
        private readonly TreeProps _treeProps;

        private readonly List<Transform> _fruitInstances;

        public FruitfullState(TreeManager manager, TreeProps treeProps)
        {
            _manager = manager;
            _treeProps = treeProps;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");

            _fruitArea = _canopy.GetComponent<PolygonCollider2D>();
            _fruitInstances = new();
        }

        public void EnterState()
        {
            _canopy.SetActive(true);
            _trunk.SetActive(true);

            PopulateFruitArea();
        }

        public void ExitState()
        {
        }

        public void UpdateState()
        {
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
            }

            if (!_fruitInstances.Any())
            {
                _manager.EnterHarvestingState();
            }
        }

        private void PopulateFruitArea()
        {
            foreach (var _ in _manager.ObtainAll(_treeProps.FruitType))
            {
                _fruitInstances.Add(AreaInstantiator.Instantiate(_fruitArea, _canopy.transform, _treeProps.FruitPrefab));
            }
        }
    }
}