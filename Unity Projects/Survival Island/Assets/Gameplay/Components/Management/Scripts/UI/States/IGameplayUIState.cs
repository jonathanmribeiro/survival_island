using SurvivalIsland.Gameplay.Management.Enums;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public interface IGameplayUIState
    {
        public void EnterState();
        public void ExitState();
        public void UpdateState();
    }
}