using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using System;

namespace SurvivalIsland.Components.Signs
{
    public class ActiveState : PlayerDetectionBase, IPlayerActionState
    {
        private SignManager _manager;

        public ActiveState(SignManager manager)
        {
            _manager = manager;
        }

        public void EnterState() => _manager.gameObject.SetActive(true);
        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback) {/*Left empty on purpose*/}
        public void ExitState() {/*Left empty on purpose*/}
        public PlayerActionTypes GetAction() => _playerInRange ? PlayerActionTypes.OpenConstructionUI : PlayerActionTypes.None;
        public void UpdateState() {/*Left empty on purpose*/}
    }
}