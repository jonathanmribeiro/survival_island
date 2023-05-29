using SurvivalIsland.Common.Enums;
using System;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class AppleModel : InventoryItemModel
    {
        public AppleModel()
            : base("Apple", "Delicious, juicy fruit", InventoryItemType.Apple, UnityEngine.Random.Range(0.1f, 0.3f))
        { }
    }
}
