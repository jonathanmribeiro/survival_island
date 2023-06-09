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
        public string Name;
        public List<InventoryItemSlot> Slots;

        public int MaxItems;
        public float MaxWeight;

        public int CurrentDifferentItemsAmount
            => Slots?.Count(x => x.Type != InventoryItemType.None) ?? 0;
        private float CurrentTotalWeight
            => Slots?.Sum(x => x.CurrentWeight) ?? 0;

        public void Prepare(string name = "", int maxItems = 10, float maxWeight = 999)
        {
            Slots = new();
            Name = name;

            for (int i = 0; i < maxItems; i++)
            {
                Slots.Add(new(InventoryItemType.None, i));
            }

            MaxItems = maxItems;
            MaxWeight = maxWeight;
        }

        private void RearrangeInventory()
        {
            for (int i = 0; i < Slots.Count - 2; i++)
            {
                var currentSlot = Slots[i];
                var nextSlot = Slots[i + 1];

                if (currentSlot.Type == InventoryItemType.None)
                {
                    Slots[i].Items = nextSlot.Items;
                    Slots[i].Type = nextSlot.Type;

                    Slots[i + 1].Items = new();
                    Slots[i + 1].Type = InventoryItemType.None;
                }
            }
        }

        #region Adders
        public bool TryAddItem(InventoryItemType type)
            => TryAddItem(InventoryItemFactory.Obtain(type));

        public bool TryAddItem(InventoryItemModel item)
        {
            var alreadyHasSlotOfType = CountItemsOfType(item.Type) > 0;

            if (!alreadyHasSlotOfType && CurrentDifferentItemsAmount >= MaxItems)
                return false;

            var futureWeight = CurrentTotalWeight + item.Weight;

            if (futureWeight >= MaxWeight)
                return false;

            var slot = Slots.FirstOrDefault(x => x.Type == item.Type);

            if (slot != null)
            {
                slot.Items.Add(item);
            }
            else
            {
                var emptySlot = Slots
                    .OrderBy(x => x.SlotNumber)
                    .FirstOrDefault(x => x.Type == InventoryItemType.None);

                if (emptySlot == null)
                    return false;

                emptySlot.Type = item.Type;
                emptySlot.Items.Add(item);
            }

            return true;
        }

        public void AddMultiple(InventoryItemType type, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var continueToAdd = TryAddItem(InventoryItemFactory.Obtain(type));

                if (!continueToAdd)
                    break;
            }
        }
        #endregion

        #region Obtainers

        public InventoryItemModel ObtainRandom(InventoryItemType type)
            => Slots.FirstOrDefault(x => x.Type == type)?.Items.FirstOrDefault();

        public List<InventoryItemModel> ObtainAll(InventoryItemType type)
            => Slots.FirstOrDefault(x => x.Type == type)?.Items;

        public InventoryItemSlot ObtainSlot(int index)
            => index < Slots.Count - 1 ? Slots[index] : default;

        public int CountItemsOfType(InventoryItemType itemType)
            => ObtainAll(itemType)?.Count ?? 0;
        #endregion

        #region Removers
        public bool Remove(InventoryItemModel item)
        {
            var slot = Slots.FirstOrDefault(x => x.Type == item.Type);

            if (slot != null)
            {
                slot.Items.Remove(item);

                if (slot.Items.Count.Equals(0))
                {
                    slot.Type = InventoryItemType.None;
                    RearrangeInventory();
                    return true;
                }
            }

            return false;
        }

        public void Remove(InventoryItemType itemType)
        {
            var slot = Slots.FirstOrDefault(x => x.Type == itemType);

            if (slot != null)
                Remove(slot.Items.First());
        }

        public void RemoveAll(InventoryItemType type)
        {
            var slots = Slots.Where(x => x.Type == type);
            foreach (var slot in slots)
            {
                slot.Items.Clear();
                slot.Type = InventoryItemType.None;
                RearrangeInventory();
            }
        }

        public void ForceAmount(InventoryItemType itemType, int amount)
        {
            RemoveAll(itemType);
            AddMultiple(itemType, amount);
        }
        #endregion
    }
}