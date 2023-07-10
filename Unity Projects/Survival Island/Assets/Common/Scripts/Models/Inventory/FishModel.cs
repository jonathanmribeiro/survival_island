using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using System;

namespace SurvivalIsland.Common.Models
{
    [Serializable]
    public class FishModel : InventoryItemModel
    {
        public FishModel(InventoryItemType type) : base(GetName(type), GetDescription(type), type, GetWeight(type))
        {
        }

        public static string GetName(InventoryItemType type)
            => type switch
            {
                InventoryItemType.Clownfish => FishConstants.CLOWNFISH_NAME,
                _ => ""
            };
        
        public static string GetDescription(InventoryItemType type)
            => type switch
            {
                InventoryItemType.Clownfish => FishConstants.CLOWNFISH_DESCRIPTION,
                _ => ""
            };

        public static float GetWeight(InventoryItemType type)
            => type switch
            {
                InventoryItemType.Clownfish => UnityEngine.Random.Range(0.1f, 0.3f),
                _ => 0f
            };
    }
}