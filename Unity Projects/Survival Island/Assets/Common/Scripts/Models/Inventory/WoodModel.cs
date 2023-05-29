using SurvivalIsland.Common.Enums;
using System;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class WoodModel : InventoryItemModel
    {
        public WoodModel()
            : base("Wood", "Good for everything", InventoryItemType.Wood, UnityEngine.Random.Range(0.9f, 1.9f))
        { }
    }
}
