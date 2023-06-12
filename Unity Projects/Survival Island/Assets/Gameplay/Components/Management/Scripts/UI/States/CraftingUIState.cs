using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class CraftingUIState : IState
    {
        private readonly GameplayUIManager _uiManager;
        private readonly MainCharacterManager _mainCharacterManager;

        private GameObject _basicUI;
        private GameObject _inventoryUI;
        private GameObject _journalUI;
        private GameObject _craftingUI;

        private ChildIconUpdater _craftableIcon1;
        private ChildIconUpdater _craftableIcon2;
        private ChildIconUpdater _craftableIcon3;
        private ChildIconUpdater _craftableIcon4;

        private ChildTextUpdater _craftableText1;
        private ChildTextUpdater _craftableText2;
        private ChildTextUpdater _craftableText3;
        private ChildTextUpdater _craftableText4;

        private ChildIconUpdater _inventoryIcon1;
        private ChildIconUpdater _inventoryIcon2;
        private ChildIconUpdater _inventoryIcon3;
        private ChildIconUpdater _inventoryIcon4;
        private ChildIconUpdater _inventoryIcon5;
        private ChildIconUpdater _inventoryIcon6;
        private ChildIconUpdater _inventoryIcon7;
        private ChildIconUpdater _inventoryIcon8;

        private ChildTextUpdater _inventoryText1;
        private ChildTextUpdater _inventoryText2;
        private ChildTextUpdater _inventoryText3;
        private ChildTextUpdater _inventoryText4;
        private ChildTextUpdater _inventoryText5;
        private ChildTextUpdater _inventoryText6;
        private ChildTextUpdater _inventoryText7;
        private ChildTextUpdater _inventoryText8;

        private Inventory _recipeInventory;

        public CraftingUIState(GameplayUIManager uiManager, MainCharacterManager mainCharacterManager)
        {
            _uiManager = uiManager;
            _mainCharacterManager = mainCharacterManager;

            _basicUI = GameObject.Find("Canvas").FindChild("BasicUI");
            _craftingUI = GameObject.Find("Canvas").FindChild("CraftingUI");
            _inventoryUI = GameObject.Find("Canvas").FindChild("InventoryUI");
            _journalUI = GameObject.Find("Canvas").FindChild("JournalUI");

            _basicUI.SetActive(false);
            _inventoryUI.SetActive(false);
            _journalUI.SetActive(false);
            _craftingUI.SetActive(false);

            var craftablePanel = _craftingUI.FindChild("CraftablePanel");
            _craftableIcon1 = craftablePanel.FindChild("CraftableSlot1").GetComponent<ChildIconUpdater>();
            _craftableIcon2 = craftablePanel.FindChild("CraftableSlot2").GetComponent<ChildIconUpdater>();
            _craftableIcon3 = craftablePanel.FindChild("CraftableSlot3").GetComponent<ChildIconUpdater>();
            _craftableIcon4 = craftablePanel.FindChild("CraftableSlot4").GetComponent<ChildIconUpdater>();

            _craftableText1 = craftablePanel.FindChild("CraftableSlot1").GetComponent<ChildTextUpdater>();
            _craftableText2 = craftablePanel.FindChild("CraftableSlot2").GetComponent<ChildTextUpdater>();
            _craftableText3 = craftablePanel.FindChild("CraftableSlot3").GetComponent<ChildTextUpdater>();
            _craftableText4 = craftablePanel.FindChild("CraftableSlot4").GetComponent<ChildTextUpdater>();

            var inventoryPanel = _craftingUI.FindChild("InventoryPanel");
            _inventoryIcon1 = inventoryPanel.FindChild("InventorySlot1").GetComponent<ChildIconUpdater>();
            _inventoryIcon2 = inventoryPanel.FindChild("InventorySlot2").GetComponent<ChildIconUpdater>();
            _inventoryIcon3 = inventoryPanel.FindChild("InventorySlot3").GetComponent<ChildIconUpdater>();
            _inventoryIcon4 = inventoryPanel.FindChild("InventorySlot4").GetComponent<ChildIconUpdater>();
            _inventoryIcon5 = inventoryPanel.FindChild("InventorySlot5").GetComponent<ChildIconUpdater>();
            _inventoryIcon6 = inventoryPanel.FindChild("InventorySlot6").GetComponent<ChildIconUpdater>();
            _inventoryIcon7 = inventoryPanel.FindChild("InventorySlot7").GetComponent<ChildIconUpdater>();
            _inventoryIcon8 = inventoryPanel.FindChild("InventorySlot8").GetComponent<ChildIconUpdater>();

            _inventoryText1 = inventoryPanel.FindChild("InventorySlot1").GetComponent<ChildTextUpdater>();
            _inventoryText2 = inventoryPanel.FindChild("InventorySlot2").GetComponent<ChildTextUpdater>();
            _inventoryText3 = inventoryPanel.FindChild("InventorySlot3").GetComponent<ChildTextUpdater>();
            _inventoryText4 = inventoryPanel.FindChild("InventorySlot4").GetComponent<ChildTextUpdater>();
            _inventoryText5 = inventoryPanel.FindChild("InventorySlot5").GetComponent<ChildTextUpdater>();
            _inventoryText6 = inventoryPanel.FindChild("InventorySlot6").GetComponent<ChildTextUpdater>();
            _inventoryText7 = inventoryPanel.FindChild("InventorySlot7").GetComponent<ChildTextUpdater>();
            _inventoryText8 = inventoryPanel.FindChild("InventorySlot8").GetComponent<ChildTextUpdater>();
        }

        public void EnterState()
        {
            _basicUI.SetActive(false);
            _inventoryUI.SetActive(false);
            _journalUI.SetActive(false);
            _craftingUI.SetActive(true);

            _craftableIcon1.Prepare("InventorySlotIcon");
            _craftableIcon2.Prepare("InventorySlotIcon");
            _craftableIcon3.Prepare("InventorySlotIcon");
            _craftableIcon4.Prepare("InventorySlotIcon");

            _inventoryIcon1.Prepare("InventorySlotIcon");
            _inventoryIcon2.Prepare("InventorySlotIcon");
            _inventoryIcon3.Prepare("InventorySlotIcon");
            _inventoryIcon4.Prepare("InventorySlotIcon");
            _inventoryIcon5.Prepare("InventorySlotIcon");
            _inventoryIcon6.Prepare("InventorySlotIcon");
            _inventoryIcon7.Prepare("InventorySlotIcon");
            _inventoryIcon8.Prepare("InventorySlotIcon");
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            UpdateIconAndText(_craftableIcon1, _craftableText1, _recipeInventory.ObtainSlot(0));
            UpdateIconAndText(_craftableIcon2, _craftableText2, _recipeInventory.ObtainSlot(1));
            UpdateIconAndText(_craftableIcon3, _craftableText3, _recipeInventory.ObtainSlot(2));
            UpdateIconAndText(_craftableIcon4, _craftableText4, _recipeInventory.ObtainSlot(3));

            UpdateIconAndText(_inventoryIcon1, _inventoryText1, _mainCharacterManager.GetInventorySlot(0));
            UpdateIconAndText(_inventoryIcon2, _inventoryText2, _mainCharacterManager.GetInventorySlot(1));
            UpdateIconAndText(_inventoryIcon3, _inventoryText3, _mainCharacterManager.GetInventorySlot(2));
            UpdateIconAndText(_inventoryIcon4, _inventoryText4, _mainCharacterManager.GetInventorySlot(3));
            UpdateIconAndText(_inventoryIcon5, _inventoryText5, _mainCharacterManager.GetInventorySlot(4));
            UpdateIconAndText(_inventoryIcon6, _inventoryText6, _mainCharacterManager.GetInventorySlot(5));
            UpdateIconAndText(_inventoryIcon7, _inventoryText7, _mainCharacterManager.GetInventorySlot(6));
            UpdateIconAndText(_inventoryIcon8, _inventoryText8, _mainCharacterManager.GetInventorySlot(7));
        }

        public void SetRecipe(Inventory recipeInventory)
        {
            _recipeInventory = recipeInventory;
        }

        private void UpdateIconAndText(ChildIconUpdater iconUpdater, ChildTextUpdater textUpdater, InventoryItemSlot slot)
        {
            if (slot != default)
            {
                iconUpdater.UpdateUI(slot);
                textUpdater.UpdateUI(slot.CurrentAmount.ToString());
            } else
            {
                textUpdater.UpdateUI("");
            }
        }
    }
}