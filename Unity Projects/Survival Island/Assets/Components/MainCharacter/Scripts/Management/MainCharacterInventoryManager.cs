using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterInventoryManager : MonoBehaviour
    {
        public Inventory Inventory;
        public Inventory CharacterInventory;

        public void Prepare()
        {
            Inventory.Prepare(8, 25);
            CharacterInventory.Prepare(4, 999);
        }

        public void OnClick_OpenInventory()
        {
        }

        public bool TryAddItem(InventoryItemModel itemModel)
        {
            return Inventory.TryAddItem(itemModel);
        }

        public InventoryItemModel GetInventoryItem(int itemIndex)
        {
            if (itemIndex < Inventory.CurrentAmount)
                return Inventory.Items[itemIndex];

            return null;
        }

        public InventoryItemModel GetCharacterItem(int itemIndex)
        {
            if (itemIndex < CharacterInventory.CurrentAmount)
                return CharacterInventory.Items[itemIndex];

            return null;
        }
    }
}
