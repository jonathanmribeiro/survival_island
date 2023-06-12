using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalIsland.Common.Utils
{
    public class ChildIconUpdater : MonoBehaviour
    {
        private Image _icon;

        public void Prepare(string childName)
        {
            _icon = gameObject.FindChild(childName).GetComponent<Image>();
            _icon.enabled = false;
        }

        public void UpdateUI(InventoryItemSlot inventorySlot)
        {
            _icon.enabled = inventorySlot != null && inventorySlot.Type != Enums.InventoryItemType.None;

            if (!_icon.enabled)
                return;

            _icon.sprite = inventorySlot?.Items.First().Icon;
        }
    }
}