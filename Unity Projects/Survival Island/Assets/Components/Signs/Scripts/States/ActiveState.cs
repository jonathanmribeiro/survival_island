using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using System;

namespace SurvivalIsland.Components.Signs
{
    public class ActiveState : StateBase
    {
        private SignManager _manager;

        public ActiveState(SignManager manager)
        {
            _manager = manager;
        }

        public override void EnterState()
            => _manager.gameObject.SetActive(true);

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
            => _manager.Parent.ExecuteAction(playerActionCallback);

        public override void ExitState()
            => _manager.gameObject.SetActive(false);

        public override PlayerActionTypes GetAction()
            => PlayerActionTypes.OpenConstructionUI;
    }
}