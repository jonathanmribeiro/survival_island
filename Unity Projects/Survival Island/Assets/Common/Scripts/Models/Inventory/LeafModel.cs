using SurvivalIsland.Common.Enums;
using System;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class LeafModel : InventoryItemModel
    {
        public LeafModel()
            : base("Leaf", "A single leaf can't block the sun.", InventoryItemType.Leaf, UnityEngine.Random.Range(0.05f, 0.1f))
        { }
    }
}
