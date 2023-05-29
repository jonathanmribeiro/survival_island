using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class TrunkState : ITreeState
    {
        private TreeManager _treeManager { get; }
        
        public TrunkState(TreeManager treeManager, TreeProps treeProps)
        {
            _treeManager = treeManager;
        }

        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            throw new NotImplementedException();
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            throw new NotImplementedException();
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback)
        {
            throw new NotImplementedException();
        }
    }
}