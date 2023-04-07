using SurvivalIsland.GameState.Interfaces;
using UnityEngine;

namespace SurvivalIsland.GameState.States
{
    public class PauseMenuState : IGameState
    {
        public string StateName => nameof(MainMenuState);

        public void Enter()
        {
            Debug.Log($"Entered: {StateName}");
        }

        public void Exit()
        {
            Debug.Log($"Exited: {StateName}");
        }
    }
}