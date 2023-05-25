using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterInventoryManager : MonoBehaviour
    {
        public Inventory Inventory;

        public void Prepare()
        {
            Inventory.Prepare(InventoryConstants.MAIN_CHARACTER_MAX_ITEMS, InventoryConstants.MAIN_CHARACTER_MAX_ITEMS);
        }

        public void OnClick_OpenInventory()
        {
        }

        public bool TryAddItem(InventoryItemModel itemModel)
        {
            return Inventory.TryAddItem(itemModel);
        }
    }
}
