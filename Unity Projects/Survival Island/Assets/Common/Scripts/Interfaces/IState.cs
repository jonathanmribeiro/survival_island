namespace SurvivalIsland.Common.Interfaces
{
    public interface IState
    {
        public void EnterState();
        public void UpdateState();
        public void ExitState();
    }
}