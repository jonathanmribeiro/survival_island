using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Gameplay.Management;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Inventory
{
    public class UIInventorySlot
    {
        private readonly ChildIconUpdater _iconUpdater;
        private readonly ChildTextUpdater _textUpdater;
        private readonly ChildButtonAction _buttonAction;
        private readonly GameplaySceneManager _gameplaySceneManager;
        private readonly Action _onButtonClickCallback;

        public UIInventorySlot(
            GameplaySceneManager gameplaySceneManager, 
            GameObject parent, 
            string slotName, 
            Action onButtonClickCallback)
        {
            _gameplaySceneManager = gameplaySceneManager;
            _onButtonClickCallback = onButtonClickCallback;

            _iconUpdater = parent.FindChild(slotName).GetComponent<ChildIconUpdater>();
            _textUpdater = parent.FindChild(slotName).GetComponent<ChildTextUpdater>();
            _buttonAction = parent.FindChild(slotName).GetComponent<ChildButtonAction>();
        }

        public void Prepare()
        {
            string slotIconName = "InventorySlotIcon";
            _iconUpdater.Prepare(slotIconName);
            _buttonAction.Prepare(_gameplaySceneManager, _onButtonClickCallback);
        }

        public void Update(InventoryItemSlot slot)
        {
            if (slot != default && slot.CurrentAmount > 0)
            {
                _iconUpdater.UpdateUI(slot);
                _textUpdater.UpdateUI(slot.CurrentAmount.ToString());
            }
            else
            {
                _iconUpdater.UpdateUI(null);
                _textUpdater.UpdateUI("");
            }
        }
    }
}