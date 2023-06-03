using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public interface ITreeState
    {
        public void EnterState();
        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback);
        public void ExitState();
        public void OnTriggerExit2D(Collider2D collision);
        public void OnTriggerStay2D(Collider2D collision);
        public void UpdateState();
    }
}