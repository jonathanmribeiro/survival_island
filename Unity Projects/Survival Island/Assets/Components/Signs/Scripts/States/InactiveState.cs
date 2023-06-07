using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using System;

namespace SurvivalIsland.Components.Signs
{
    public class InactiveState : PlayerDetectionBase, IPlayerActionState
    {
        private SignManager _manager;

        public InactiveState(SignManager manager)
        {
            _manager = manager;
        }

        public void EnterState()
        {
            _manager.gameObject.SetActive(false);
        }

        public void ExecuteAction(Func<PlayerActionTypes, InventoryItemModel, bool> playerActionCallback) {/*Left empty on purpose*/}
        public void ExitState() {/*Left empty on purpose*/}
        public PlayerActionTypes GetAction() => PlayerActionTypes.None;
        public void UpdateState() {/*Left empty on purpose*/}
    }
}