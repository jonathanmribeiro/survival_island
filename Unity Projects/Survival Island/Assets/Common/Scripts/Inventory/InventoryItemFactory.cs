using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Models;

namespace Assets.Common.Inventory
{
    public static class InventoryItemFactory
    {
        public static InventoryItemModel Obtain(InventoryItemType type)
            => type switch
            {
                InventoryItemType.Wood => new WoodModel(),
                InventoryItemType.Apple => new AppleModel(),
                InventoryItemType.Leaf => new LeafModel(),
                _ => null,
            };
    }
}
