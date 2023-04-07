using SurvivalIsland.GameState.Interfaces;
using UnityEngine;

namespace SurvivalIsland.GameState.States
{
    public class GameplayState : IGameState
    {
        public string StateName => nameof(GameplayState);

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