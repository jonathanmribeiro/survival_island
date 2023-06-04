using SurvivalIsland.Common.Inventory;
using SurvivalIsland.Common.Models;
using System.Linq;
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

        public InventoryItemSlot GetInventorySlot(int itemIndex)
        {
            return Inventory.Slots.First(x => x.SlotNumber == itemIndex);
        }

        public InventoryItemSlot GetCharacterItem(int itemIndex)
        {
            return CharacterInventory.Slots.First(x => x.SlotNumber == itemIndex);
        }
    }
}
