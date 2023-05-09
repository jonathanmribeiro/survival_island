using SurvivalIsland.Common.Models;
using System;
using System.Collections.Generic;

namespace SurvivalIsland.Common.Inventory
{
    [Serializable]
    internal class Inventory
    {
        public List<InventoryItemModel> Items;

        internal virtual void AddItem(InventoryItemModel item)
        {
            Items.Add(item);
        }

        internal virtual void RemoveItem(InventoryItemModel item)
        {
            Items.Remove(item);
        }

        internal virtual bool ContainsItem(InventoryItemModel item)
        {
            return Items.Contains(item);
        }
    }
}