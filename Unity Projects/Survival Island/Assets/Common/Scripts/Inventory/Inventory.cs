using Assets.Common.Inventory;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurvivalIsland.Common.Inventory
{
    [Serializable]
    public class Inventory
    {
        public List<InventoryItemModel> Items;

        public int MaxItems;
        public float MaxWeight;

        public int CurrentAmount => Items.Count;
        private float CurrentTotalWeight => Items.Sum(x => x.Weight);

        public void Prepare(int maxItems, float maxWeight)
        {
            Items = new();
            MaxItems = maxItems;
            MaxWeight = maxWeight;
        }

        public bool TryAddItem(InventoryItemType type) => TryAddItem(InventoryItemFactory.Obtain(type));
        public bool TryAddItem(InventoryItemModel item)
        {
            var futureWeight = CurrentTotalWeight + item.Weight;

            if (CurrentAmount >= MaxItems || futureWeight >= MaxWeight)
                return false;

            Items.Add(item);

            return true;
        }

        public void AddMultiple(InventoryItemType type, int amount)
        {
            for (int i = 0; i <= amount; i++)
            {
                var continueToAdd = TryAddItem(InventoryItemFactory.Obtain(type));

                if (!continueToAdd)
                    break;
            }
        }

        public InventoryItemModel ObtainRandom(InventoryItemType type) => Items.FirstOrDefault(x => x.Type == type);
        public List<InventoryItemModel> ObtainAll(InventoryItemType type) => Items.Where(x => x.Type == type).ToList();
        public void Remove(InventoryItemModel item) => Items.Remove(item);
    }
}