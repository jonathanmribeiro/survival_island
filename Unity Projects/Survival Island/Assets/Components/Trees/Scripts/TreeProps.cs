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
        public int MaxLeavesAmount;
        public int MaxWoodAmount;

        public TimeSpan TimeNeededInGoneState;
        public TimeSpan TimeNeededInGrowingState;
        public TimeSpan TimeNeededInHarvestingState;

        public TimeSpan TimeNeededToSpawnFruit;
        public TimeSpan TimeNeededToSpawnWood;

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
            TimeNeededInHarvestingState = TimeSpan.FromDays(days);

            days = random.Next(3, 5);
            TimeNeededToSpawnFruit = TimeSpan.FromDays(days);
        }
    }
}