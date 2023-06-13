using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class JournalUIState : IState
    {
        private readonly GameplayUIManager _uiManager;

        private GameObject _journalUI;

        public JournalUIState(GameplayUIManager uiManager)
        {
            _uiManager = uiManager;

            _journalUI = GameObject.Find("Canvas").FindChild("JournalUI");
            _journalUI.SetActive(false);
        }

        public void EnterState()
        {
            _journalUI.SetActive(true);
        }

        public void UpdateState()
        {

        }

        public void ExitState() => _journalUI.SetActive(false);
    }
}