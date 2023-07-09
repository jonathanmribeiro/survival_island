using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalIsland.Gameplay.Management.UI
{
    public class CraftingUIState : IState
    {
        private readonly GameplayUIManager _uiManager;
        private readonly MainCharacterManager _mainCharacterManager;
        private readonly GameplaySceneManager _sceneManager;

        private readonly GameObject _craftingUI;

        private readonly UIInventorySlot _craftableSlot1;
        private readonly UIInventorySlot _craftableSlot2;
        private readonly UIInventorySlot _craftableSlot3;
        private readonly UIInventorySlot _craftableSlot4;
        private readonly UIInventorySlot _uiInventorySlot1;
        private readonly UIInventorySlot _uiInventorySlot2;
        private readonly UIInventorySlot _uiInventorySlot3;
        private readonly UIInventorySlot _uiInventorySlot4;
        private readonly UIInventorySlot _uiInventorySlot5;
        private readonly UIInventorySlot _uiInventorySlot6;
        private readonly UIInventorySlot _uiInventorySlot7;
        private readonly UIInventorySlot _uiInventorySlot8;

        private readonly ChildButtonAction _openJournal;
        private readonly ChildButtonAction _confirmCrafting;
        private readonly ChildButtonAction _closeCrafting;

        private readonly ChildTextUpdater _craftableItemNameUpdater;

        private readonly Button _confirmCraftingButton;
        private Inventory _recipeInventory;
        private Action _afterCraftingCallback;

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
            _craftableSlot1 = new(gameplaySceneManager, craftablePanel, "CraftableSlot1", () => { });
            _craftableSlot2 = new(gameplaySceneManager, craftablePanel, "CraftableSlot2", () => { });
            _craftableSlot3 = new(gameplaySceneManager, craftablePanel, "CraftableSlot3", () => { });
            _craftableSlot4 = new(gameplaySceneManager, craftablePanel, "CraftableSlot4", () => { });

            _craftableItemNameUpdater = craftablePanel.GetComponent<ChildTextUpdater>();

            var inventoryPanel = _craftingUI.FindChild("InventoryPanel");
            _uiInventorySlot1 = new(gameplaySceneManager, inventoryPanel, "InventorySlot1", () => HandleInventoryClick(0));
            _uiInventorySlot2 = new(gameplaySceneManager, inventoryPanel, "InventorySlot2", () => HandleInventoryClick(1));
            _uiInventorySlot3 = new(gameplaySceneManager, inventoryPanel, "InventorySlot3", () => HandleInventoryClick(2));
            _uiInventorySlot4 = new(gameplaySceneManager, inventoryPanel, "InventorySlot4", () => HandleInventoryClick(3));
            _uiInventorySlot5 = new(gameplaySceneManager, inventoryPanel, "InventorySlot5", () => HandleInventoryClick(4));
            _uiInventorySlot6 = new(gameplaySceneManager, inventoryPanel, "InventorySlot6", () => HandleInventoryClick(5));
            _uiInventorySlot7 = new(gameplaySceneManager, inventoryPanel, "InventorySlot7", () => HandleInventoryClick(6));
            _uiInventorySlot8 = new(gameplaySceneManager, inventoryPanel, "InventorySlot8", () => HandleInventoryClick(7));

            var middlePanel = _craftingUI.FindChild("MiddlePanel");
            _openJournal = middlePanel.FindChild("Journal").GetComponent<ChildButtonAction>();

            _confirmCrafting = middlePanel.FindChild("Craft").GetComponent<ChildButtonAction>();
            _confirmCraftingButton = _confirmCrafting.GetComponentInChildren<Button>();

            _closeCrafting = middlePanel.FindChild("Close").GetComponent<ChildButtonAction>();
        }

        public void EnterState()
        {
            _openJournal.Prepare(_sceneManager, () => { });
            _confirmCrafting.Prepare(_sceneManager, OnClick_ConfirmCrafting);
            _closeCrafting.Prepare(_sceneManager, OnClick_CloseCrafting);

            _craftableSlot1.Prepare();
            _craftableSlot2.Prepare();
            _craftableSlot3.Prepare();
            _craftableSlot4.Prepare();

            _craftableItemNameUpdater.Prepare("CraftableItemNameLabel");
            _craftableItemNameUpdater.UpdateUI(_recipeInventory.Name);

            _uiInventorySlot1.Prepare();
            _uiInventorySlot2.Prepare();
            _uiInventorySlot3.Prepare();
            _uiInventorySlot4.Prepare();
            _uiInventorySlot5.Prepare();
            _uiInventorySlot6.Prepare();
            _uiInventorySlot7.Prepare();
            _uiInventorySlot8.Prepare();

            UpdateCraftButton();

            _craftingUI.SetActive(true);
        }

        public void ExitState()
            => _craftingUI.SetActive(false);

        public void UpdateState()
        {
            _craftableSlot1.Update(_recipeInventory.ObtainSlot(0));
            _craftableSlot2.Update(_recipeInventory.ObtainSlot(1));
            _craftableSlot3.Update(_recipeInventory.ObtainSlot(2));
            _craftableSlot4.Update(_recipeInventory.ObtainSlot(3));

            _uiInventorySlot1.Update(_mainCharacterManager.GetInventorySlot(0));
            _uiInventorySlot2.Update(_mainCharacterManager.GetInventorySlot(1));
            _uiInventorySlot3.Update(_mainCharacterManager.GetInventorySlot(2));
            _uiInventorySlot4.Update(_mainCharacterManager.GetInventorySlot(3));
            _uiInventorySlot5.Update(_mainCharacterManager.GetInventorySlot(4));
            _uiInventorySlot6.Update(_mainCharacterManager.GetInventorySlot(5));
            _uiInventorySlot7.Update(_mainCharacterManager.GetInventorySlot(6));
            _uiInventorySlot8.Update(_mainCharacterManager.GetInventorySlot(7));

            UpdateCraftButton();
        }

        public void UpdateCraftButton()
        {
            _confirmCraftingButton.interactable = _recipeInventory.CurrentDifferentItemsAmount == 0;
        }

        public void SetRecipe(Inventory recipeInventory)
        {
            _recipeInventory = recipeInventory;
        }

        public void SetCraftingCallback(Action afterCraftingCallback)
        {
            _afterCraftingCallback = afterCraftingCallback;
        }

        public void OnClick_CloseCrafting()
            => _uiManager.EnterBasicUIState();

        public void OnClick_ConfirmCrafting()
        {
            _uiManager.EnterBasicUIState();
            _afterCraftingCallback.Invoke();
        }

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