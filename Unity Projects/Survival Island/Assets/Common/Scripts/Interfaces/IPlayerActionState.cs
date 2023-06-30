using SurvivalIsland.Common.Enums;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Interfaces
{
    public interface IPlayerActionState: IState
    {
        public PlayerActionTypes GetAction();
        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback);
        public void OnTriggerExit2D(Collider2D collision);
        public void OnTriggerStay2D(Collider2D collision);
    }
}