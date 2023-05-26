
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Gameplay.Management.Enums;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class InventoryUIState : IGameplayUIState
    {
        private readonly GameplayUIManager _uiManager;

        public InventoryUIState(GameplayUIManager uiManager)
        {
            _uiManager = uiManager;

            var canvas = GameObject.Find("Canvas").FindChild("InventoryUI");
        }

        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void SwitchState(GameplayUIStates state)
        {
            throw new System.NotImplementedException();
        }
    }
}