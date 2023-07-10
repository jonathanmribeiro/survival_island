using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.Signs;
using SurvivalIsland.Components.Trees;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalIsland.Components.Fishing
{
    public class WaitingState : StateBase
    {
        private readonly FishingManager _manager;
        private readonly DayNightCycle _dayNightCycle;
        private readonly FishingProps _fishingProps;
        private readonly SignManager _signAlert;

        private readonly GameObject _bucket;
        private readonly GameObject _rod;

        private PolygonCollider2D _fishArea;
        private readonly List<FishShadowManager> _fishShadowManagerList;

        public WaitingState(FishingManager manager, DayNightCycle dayNightCycle, FishingProps fishingProps)
        {
            _manager = manager;
            _dayNightCycle = dayNightCycle;
            _fishingProps = fishingProps;

            _bucket = _manager.gameObject.FindChild("Bucket");
            _rod = _manager.gameObject.FindChild("Rod");

            _signAlert = _manager.GetComponentInChildren<SignManager>();
            _fishShadowManagerList = new();
        }

        public override void EnterState()
        {
            _bucket.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            _rod.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            _bucket.GetComponent<CircleCollider2D>().enabled = false;
            _rod.GetComponent<CircleCollider2D>().enabled = true;

            _signAlert.Prepare(_manager, SignStates.InactiveState);

            _manager.SelectorLocation = _rod.transform;

            _fishArea = _manager.GetComponent<PolygonCollider2D>();
            _fishingProps.LastTimeFishSpawned = _dayNightCycle.CurrentDateTime;
            _dayNightCycle.MinuteByMinuteSubscribers.Add(PopulateArea);
        }

        public override void UpdateState()
        {
            foreach(FishShadowManager fishShadowManager in _fishShadowManagerList)
            {
                fishShadowManager.UpdateCurrentState();
            }
        }

        public override PlayerActionTypes GetAction()
            => PlayerActionTypes.StartFishing;

        private void PopulateArea()
        {
            int currentFishAmount = _manager.FishAreaInventory.CountItemsOfType(InventoryItemType.Clownfish);
            DateTime nextSpawnTime = _fishingProps.LastTimeFishSpawned.Add(_fishingProps.TimeNeededToSpawnFish);

            if (_dayNightCycle.CurrentDateTime > nextSpawnTime && currentFishAmount < _fishingProps.MaxFishWithinArea)
            {
                Transform newFish = AreaInstantiator
                    .Instantiate(_fishArea, _fishArea.transform, _fishingProps.FishShadowPrefab);
                _fishShadowManagerList.Add(newFish.GetComponent<FishShadowManager>());

                _fishingProps.LastTimeFishSpawned = _dayNightCycle.CurrentDateTime;
                _manager.FishAreaInventory.TryAddItem(InventoryItemType.Clownfish);
            }
        }
    }
}