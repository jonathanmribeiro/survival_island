using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class CraftingUIState : IState
    {
        private readonly GameplayUIManager _uiManager;
        private readonly MainCharacterManager _mainCharacterManager;
        private readonly GameplaySceneManager _sceneManager;

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

        private ChildButtonAction _inventoryButton1;
        private ChildButtonAction _inventoryButton2;
        private ChildButtonAction _inventoryButton3;
        private ChildButtonAction _inventoryButton4;
        private ChildButtonAction _inventoryButton5;
        private ChildButtonAction _inventoryButton6;
        private ChildButtonAction _inventoryButton7;
        private ChildButtonAction _inventoryButton8;

        private ChildButtonAction _openJournal;
        private Button _openJournalButton;

        private ChildButtonAction _confirmCrafting;
        private Button _confirmCraftingButton;

        private ChildButtonAction _closeCrafting;
        private Button _closeCraftingButton;

        private Inventory _recipeInventory;

        public CraftingUIState(GameplayUIManager uiManager,
                               GameplaySceneManager gameplaySceneManager,
                               MainCharacterManager mainCharacterManager)
        {
            _uiManager = uiManager;
            _mainCharacterManager = mainCharacterManager;
            _sceneManager = gameplaySceneManager;

            _craftingUI = GameObject.Find("Canvas").FindChild("CraftingUI");
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

            _inventoryButton1 = inventoryPanel.FindChild("InventorySlot1").GetComponent<ChildButtonAction>();
            _inventoryButton2 = inventoryPanel.FindChild("InventorySlot2").GetComponent<ChildButtonAction>();
            _inventoryButton3 = inventoryPanel.FindChild("InventorySlot3").GetComponent<ChildButtonAction>();
            _inventoryButton4 = inventoryPanel.FindChild("InventorySlot4").GetComponent<ChildButtonAction>();
            _inventoryButton5 = inventoryPanel.FindChild("InventorySlot5").GetComponent<ChildButtonAction>();
            _inventoryButton6 = inventoryPanel.FindChild("InventorySlot6").GetComponent<ChildButtonAction>();
            _inventoryButton7 = inventoryPanel.FindChild("InventorySlot7").GetComponent<ChildButtonAction>();
            _inventoryButton8 = inventoryPanel.FindChild("InventorySlot8").GetComponent<ChildButtonAction>();

            var middlePanel = _craftingUI.FindChild("MiddlePanel");
            _openJournal = middlePanel.FindChild("Journal").GetComponent<ChildButtonAction>();
            _openJournalButton = _openJournal.GetComponentInChildren<Button>();

            _confirmCrafting = middlePanel.FindChild("Craft").GetComponent<ChildButtonAction>();
            _confirmCraftingButton = _confirmCrafting.GetComponentInChildren<Button>();

            _closeCrafting = middlePanel.FindChild("Close").GetComponent<ChildButtonAction>();
            _closeCraftingButton = _closeCrafting.GetComponentInChildren<Button>();
        }

        public void EnterState()
        {
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

            _inventoryButton1.Prepare(_sceneManager, OnClick_InventoryButton1);
            _inventoryButton2.Prepare(_sceneManager, OnClick_InventoryButton2);
            _inventoryButton3.Prepare(_sceneManager, OnClick_InventoryButton3);
            _inventoryButton4.Prepare(_sceneManager, OnClick_InventoryButton4);
            _inventoryButton5.Prepare(_sceneManager, OnClick_InventoryButton5);
            _inventoryButton6.Prepare(_sceneManager, OnClick_InventoryButton6);
            _inventoryButton7.Prepare(_sceneManager, OnClick_InventoryButton7);
            _inventoryButton8.Prepare(_sceneManager, OnClick_InventoryButton8);

            _openJournal.Prepare(_sceneManager, () => { });
            _confirmCrafting.Prepare(_sceneManager, OnClick_ConfirmCrafting);
            _closeCrafting.Prepare(_sceneManager, OnClick_CloseCrafting);

            UpdateCraftButton();
        }

        public void ExitState() => _craftingUI.SetActive(false);

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

            UpdateCraftButton();
        }

        public void UpdateCraftButton()
        {
            _confirmCraftingButton.interactable = _recipeInventory.CurrentAmount == 0;
        }

        public void SetRecipe(Inventory recipeInventory)
        {
            _recipeInventory = recipeInventory;
        }

        private void UpdateIconAndText(ChildIconUpdater iconUpdater, ChildTextUpdater textUpdater, InventoryItemSlot slot)
        {
            if (slot != default && slot.CurrentAmount > 0)
            {
                iconUpdater.UpdateUI(slot);
                textUpdater.UpdateUI(slot.CurrentAmount.ToString());
            }
            else
            {
                iconUpdater.UpdateUI(null);
                textUpdater.UpdateUI("");
            }
        }

        public void OnClick_CloseCrafting() => _uiManager.EnterBasicUIState();
        public void OnClick_ConfirmCrafting() => _uiManager.EnterBasicUIState();

        private void OnClick_InventoryButton1() => HandleInventoryClick(0);
        private void OnClick_InventoryButton2() => HandleInventoryClick(1);
        private void OnClick_InventoryButton3() => HandleInventoryClick(2);
        private void OnClick_InventoryButton4() => HandleInventoryClick(3);
        private void OnClick_InventoryButton5() => HandleInventoryClick(4);
        private void OnClick_InventoryButton6() => HandleInventoryClick(5);
        private void OnClick_InventoryButton7() => HandleInventoryClick(6);
        private void OnClick_InventoryButton8() => HandleInventoryClick(7);

        private void HandleInventoryClick(int index)
        {
            InventoryItemSlot slot = _mainCharacterManager.GetInventorySlot(index);
            InventoryItemModel item = _recipeInventory.ObtainRandom(slot.Type);

            if (item == null)
                return;

            _recipeInventory.Remove(item);
            _mainCharacterManager.RemoveInventoryItem(slot.Type);
        }
    }
}