using SurvivalIsland.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class InventoryItemSlot
    {
        public InventoryItemType Type;

        public int SlotNumber;

        public List<InventoryItemModel> Items;

        public int CurrentAmount 
            => Items.Count;
        public float CurrentWeight 
            => Items.Select(x => x.Weight).Sum();

        public InventoryItemSlot(InventoryItemType type, int slotNumber)
        {
            Type = type;
            SlotNumber = slotNumber;
            Items = new List<InventoryItemModel>();
        }
    }
}
