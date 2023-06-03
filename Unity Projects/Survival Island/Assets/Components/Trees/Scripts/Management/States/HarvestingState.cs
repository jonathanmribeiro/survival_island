using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using System;
using System.Linq;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class HarvestingState : PlayerDetectionBase, ITreeState
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly TreeManager _manager;

        private readonly TreeProps _treeProps;

        public HarvestingState(TreeManager manager, TreeProps treeProps)
        {
            _manager = manager;
            _treeProps = treeProps;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
        }

        public void EnterState()
        {
            _canopy.SetActive(true);
            _trunk.SetActive(true);
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
                    _manager.Remove(randomItem);
            }

            randomItem = _manager.ObtainRandom(InventoryItemType.Leaf)
                ?? _manager.ObtainRandom(InventoryItemType.Wood);
            
            if (randomItem == null)
                _manager.EnterTrunkState();
        }
    }
}