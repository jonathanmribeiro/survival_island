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
        public List<InventoryItemModel> Items = new();

        public int MaxItems;
        public float MaxWeight;

        private int CurrentAmount => Items.Count;
        private float CurrentTotalWeight => Items.Sum(x => x.Weight);

        public void Prepare(int maxItems, float maxWeight)
        {
            MaxItems = maxItems;
            MaxWeight = maxWeight;
        }

        public virtual void AddItem(InventoryItemModel item)
        {
            var futureWeight = CurrentTotalWeight + item.Weight;

            if (CurrentAmount < MaxItems && futureWeight < MaxWeight)
                Items.Add(item);
        }

        public void AddMultiple(InventoryItemType type, int amount)
        {
            for (int i = 0; i <= amount; i++)
            {
                AddItem(InventoryItemFactory.Obtain(type));
            }
        }
    }
}