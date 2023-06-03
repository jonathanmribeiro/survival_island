using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using System;
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
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;
        }
    }
}