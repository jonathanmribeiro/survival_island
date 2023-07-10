using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class StateBase : IState
    {
        public bool PlayerInRange = false;

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                PlayerInRange = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                PlayerInRange = false;
        }

        public virtual PlayerActionTypes GetAction()
            => PlayerActionTypes.None;

        public virtual void EnterState() { }

        public virtual void UpdateState() { }

        public virtual void ExitState() { }

        public virtual void ExecuteAction
            (Func<PlayerActionTypes, object, bool> playerActionCallback)
        { }

        public virtual void ExecuteQuickAction
            (Action<InventoryItemModel> playerActionCallback, InventoryItemModel itemModel)
        { }
    }
}
