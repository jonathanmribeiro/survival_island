using SurvivalIsland.Common.Enums;
using System.Collections.Generic;

namespace SurvivalIsland.Common.Constants
{
    public static class CraftingRecipesConstants
    {
        public static Dictionary<InventoryItemType, int> Campfire
            => new() {
                    { InventoryItemType.Wood, 4 },
                    { InventoryItemType.Leaf, 5}
                };
    }
}