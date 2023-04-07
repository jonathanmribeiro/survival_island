using SurvivalIsland.GameState.Interfaces;
using SurvivalIsland.GameState.States;
using SurvivalIsland.Utils;

namespace SurvivalIsland.GameState.Managers
{
    public class GameStatesManager : Mediator<GameStatesManager>
    {
        public IGameState CurrentState;

        private GameplayState _gameplayState;
        private MainMenuState _mainMenuState;
        private PauseMenuState _pauseMenuState;

        private void Start()
        {
            _gameplayState = new();
            _mainMenuState = new();
            _pauseMenuState = new();

            EnterGameplayState();
        }

        public void EnterGameplayState() => SwitchState(_gameplayState);

        public void EnterMainMenuState() => SwitchState(_mainMenuState);

        public void EnterPauseMenuState() => SwitchState(_pauseMenuState);

        private void SwitchState(IGameState nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;

            Publish(this);
            CurrentState.Enter();
        }
    }
}
