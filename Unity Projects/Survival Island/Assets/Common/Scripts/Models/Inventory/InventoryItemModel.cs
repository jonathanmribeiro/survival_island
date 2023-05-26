using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class InventoryItemModel
    {
        public string Name;
        public string Description;
        public InventoryItemType Type;
        public float Weight;
        public Sprite Icon;

        public InventoryItemModel
            (string name, string description, InventoryItemType type, float weight)
        {
            Name = name;
            Description = description;
            Type = type;
            Weight = weight;

            switch (Type)
            {
                case InventoryItemType.Wood:
                    Icon = ResourceLoader.Load("UI/Icons/inventory_items");
                    break;
            }
        }
    }
}