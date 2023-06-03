using SurvivalIsland.Common.Enums;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    [Serializable]
    public class TreeProps
    {
        public InventoryItemType FruitType;

        public int MaxFruitAmount;
        public int CurrentWoodAmount;

        public int MaxLeavesAmount;
        public int CurrentFruitAmount;

        public int MaxWoodAmount;
        public int CurrentLeavesAmount;

        public TimeSpan TimeNeededInGoneState;
        public TimeSpan TimeNeededInGrowingState;
        public TimeSpan TimeNeededToSpawnFruit;
        public TimeSpan TimeNeededInHarvestingState;

        public DateTime? TimeEnteredFruitfullState;
        public DateTime? TimeEnteredGoneState;
        public DateTime? TimeEnteredGrowingState;
        public DateTime? TimeEnteredHarvestingState;

        public Transform FruitPrefab;

        public TreeProps()
        {
            System.Random random = new();

            int days = random.Next(10, 16);
            TimeNeededInGoneState = TimeSpan.FromDays(days);

            days = random.Next(5, 10);
            TimeNeededInGrowingState = TimeSpan.FromDays(days);

            days = random.Next(3, 5);
            TimeNeededToSpawnFruit = TimeSpan.FromDays(days);

            days = random.Next(3, 5);
            TimeNeededInHarvestingState = TimeSpan.FromDays(days);
        }

        public void ReduceCurrentAmount(InventoryItemType itemType)
        {
            if (itemType.Equals(FruitType))
            {
                CurrentFruitAmount--;
            }
            else if (itemType.Equals(InventoryItemType.Wood))
            {
                CurrentWoodAmount--;
            }
            else if (itemType.Equals(InventoryItemType.Leaf))
            {
                CurrentLeavesAmount--;
            }
        }
    }
}