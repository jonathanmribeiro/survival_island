using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Interfaces;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class StateBase: IState
    {
        public bool _playerInRange = false;

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                _playerInRange = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                _playerInRange = false;
        }

        public virtual PlayerActionTypes GetAction() 
            => PlayerActionTypes.None;

        public virtual void EnterState() { }

        public virtual void UpdateState() { }

        public virtual void ExitState() { }

        public virtual void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback) { }
    }
}
