using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
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

        public void UpdateUI(InventoryItemModel inventoryItem)
        {
            _icon.enabled = inventoryItem != null;

            if (inventoryItem == null)
                return;

            _icon.sprite = inventoryItem.Icon;
        }
    }
}