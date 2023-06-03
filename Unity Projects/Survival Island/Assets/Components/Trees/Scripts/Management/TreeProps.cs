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

        public Transform FruitPrefab;
	}
}