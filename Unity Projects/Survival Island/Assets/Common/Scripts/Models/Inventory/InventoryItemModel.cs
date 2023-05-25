using SurvivalIsland.Common.Enums;
using System;
using UnityEngine;

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
            (string name, string description, InventoryItemType type, float weight, Sprite icon)
        {
            Name = name;
            Description = description;
            Type = type;
            Weight = weight;
            Icon = icon;
        }
    }
}