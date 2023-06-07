using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Gameplay.Management.Enums;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class JournalUIState : IState
    {
        private readonly GameplayUIManager _uiManager;

        private GameObject _basicUI;
        private GameObject _inventoryUI;
        private GameObject _journalUI;
        private GameObject _craftingUI;

        public JournalUIState(GameplayUIManager uiManager)
        {
            _uiManager = uiManager;

            _basicUI = GameObject.Find("Canvas").FindChild("BasicUI");
            _craftingUI = GameObject.Find("Canvas").FindChild("CraftingUI");
            _inventoryUI = GameObject.Find("Canvas").FindChild("InventoryUI");
            _journalUI = GameObject.Find("Canvas").FindChild("JournalUI");
        }

        public void EnterState()
        {
            _basicUI.SetActive(false);
            _inventoryUI.SetActive(false);
            _journalUI.SetActive(true);
            _craftingUI.SetActive(false);
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