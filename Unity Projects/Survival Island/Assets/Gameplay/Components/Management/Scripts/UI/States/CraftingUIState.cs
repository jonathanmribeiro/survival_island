using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class CraftingUIState : IState
    {
        private readonly GameplayUIManager _uiManager;

        private GameObject _basicUI;
        private GameObject _inventoryUI;
        private GameObject _journalUI;
        private GameObject _craftingUI;

        public CraftingUIState(GameplayUIManager uiManager)
        {
            _uiManager = uiManager;

            _basicUI = GameObject.Find("Canvas").FindChild("BasicUI");
            _craftingUI = GameObject.Find("Canvas").FindChild("CraftingUI");
            _inventoryUI = GameObject.Find("Canvas").FindChild("InventoryUI");
            _journalUI = GameObject.Find("Canvas").FindChild("JournalUI");

            _basicUI.SetActive(false);
            _inventoryUI.SetActive(false);
            _journalUI.SetActive(false);
            _craftingUI.SetActive(false);
        }

        public void EnterState()
        {
            _basicUI.SetActive(false);
            _inventoryUI.SetActive(false);
            _journalUI.SetActive(false);
            _craftingUI.SetActive(true);
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }
}