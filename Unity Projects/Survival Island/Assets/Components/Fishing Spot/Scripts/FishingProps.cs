using System;
using UnityEngine;

namespace SurvivalIsland.Components.Fishing
{
    [Serializable]
    public class FishingProps
    {
        public Sprite BucketEmpty;
        public Sprite BucketClownfish;

        public int MaxFishWithinArea;

        public Transform FishShadowPrefab;
        public DateTime LastTimeFishSpawned;
        public TimeSpan TimeNeededToSpawnFish;

        public FishingProps()
        {
            System.Random random = new();

            int days = random.Next(1, 1);
            TimeNeededToSpawnFish = TimeSpan.FromDays(days);
        }
    }
}