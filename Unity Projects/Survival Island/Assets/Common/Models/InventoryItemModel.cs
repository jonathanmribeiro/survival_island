using System;
using UnityEngine;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class InventoryItemModel
    {
        public string Name;
        public string Description;
        public int Quantity;
        public Sprite Icon;
    }
}