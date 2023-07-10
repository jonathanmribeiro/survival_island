using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.Signs;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SurvivalIsland.Components.Campfire
{
    public class LitState : StateBase
    {
        private CampfireManager _manager;

        private GameObject _pristineWood;
        private GameObject _burnedWood;
        private GameObject _pendingConstructionWood;
        private GameObject _flame;

        private CapsuleCollider2D _boundariesCollider;
        private CircleCollider2D _activationTrigger;

        private SignManager _signAlert;
        private CampfireProps _campfireProps;
        private DayNightCycle _dayNightCycle;

        private Light2D _flameLight;

        public LitState(CampfireManager manager, CampfireProps campfireProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;

            _campfireProps = campfireProps;
            _dayNightCycle = dayNightCycle;

            _pristineWood = _manager.gameObject.FindChild("PristineWood");
            _burnedWood = _manager.gameObject.FindChild("BurnedWood");
            _pendingConstructionWood = _manager.gameObject.FindChild("PendingConstructionWood");
            _flame = _manager.gameObject.FindChild("Flame");
            _flameLight = _manager.GetComponentInChildren<Light2D>();
            _flameLight.enabled = false;

            _boundariesCollider = _manager.gameObject.GetComponent<CapsuleCollider2D>();
            _activationTrigger = _manager.gameObject.GetComponent<CircleCollider2D>();

            _signAlert = _manager.GetComponentInChildren<SignManager>();
        }

        public override void EnterState()
        {
            _pristineWood.SetActive(false);
            _burnedWood.SetActive(true);
            _pendingConstructionWood.SetActive(false);
            _flame.SetActive(true);

            _boundariesCollider.enabled = true;
            _activationTrigger.enabled = true;

            _signAlert.Prepare(_manager, SignStates.InactiveState);

            if (!_campfireProps.TimeEnteredLitState.HasValue)
            {
                _campfireProps.TimeEnteredLitState = _dayNightCycle.CurrentDateTime;
                _campfireProps.TimeBurnedWood = _campfireProps.TimeEnteredLitState.Value;
            }

            _dayNightCycle.MinuteByMinuteSubscribers.Add(UpdateTimeLeft);
            _flameLight.enabled = true;
        }

        public override void UpdateState()
        {
            if (_manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood) == 0)
            {
                _manager.EnterExtinguishedState();
                return;
            }

            UpdateWoodAmount();
            UpdateFlameLight();
        }

        public override void ExitState()
        {
            _dayNightCycle.MinuteByMinuteSubscribers.Remove(UpdateTimeLeft);
            _flameLight.enabled = false;
        }

        public override PlayerActionTypes GetAction()
            => PlayerActionTypes.FeedCampfire;

        public override void ExecuteQuickAction(Action<InventoryItemModel> playerActionCallback, InventoryItemModel itemModel)
        {
            var woodAmount = _manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood);
            if (woodAmount >= _manager.CampfireProps.MaxWood)
                return;

            if (_manager.CampfireInventory.TryAddItem(itemModel))
                playerActionCallback.Invoke(itemModel);

            _manager.TimeLeft = _manager.TimeLeft.Value.Add(_campfireProps.TimeNeededToBurnWood);
        }

        private void UpdateTimeLeft()
        {
            if (_manager.TimeLeft == null)
            {
                var woodAmount = _manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood);
                _manager.TimeLeft = new(woodAmount, 0, 0);
            }

            _manager.TimeLeft = _manager.TimeLeft.Value.Subtract(new(0, 1, 0));
        }

        private void UpdateWoodAmount()
        {
            DateTime timeToConsumeWood = _campfireProps.TimeBurnedWood.Add(_campfireProps.TimeNeededToBurnWood);

            if (_dayNightCycle.CurrentDateTime >= timeToConsumeWood)
            {
                _manager.CampfireInventory.Remove(InventoryItemType.Wood);
                _campfireProps.TimeBurnedWood = _dayNightCycle.CurrentDateTime;
            }
        }

        private void UpdateFlameLight()
        {
            if (UnityEngine.Random.Range(0f, 1f) > 0.7f)
            {
                _flameLight.color = new Color(0.74f, 0.62f, 0.48f);

                var firewoodAmount = _manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood);

                var minIntensity = Mathf.SmoothStep(0.1f, 0.8f, firewoodAmount / 10.0f);
                var maxIntensity = Mathf.SmoothStep(0.3f, 1.2f, firewoodAmount / 10.0f);
                _flameLight.intensity = UnityEngine.Random.Range(minIntensity, maxIntensity);

                var minOuterRadius = Mathf.SmoothStep(3.5f, 9.0f, firewoodAmount / 10.0f);
                var maxOuterRadius = Mathf.SmoothStep(3.8f, 9.3f, firewoodAmount / 10.0f);
                _flameLight.pointLightOuterRadius = UnityEngine.Random.Range(minOuterRadius, maxOuterRadius);

                var minInnerRadius = Mathf.SmoothStep(0.0f, 0.3f, firewoodAmount / 10.0f);
                var maxInnerRadius = Mathf.SmoothStep(0.1f, 0.5f, firewoodAmount / 10.0f);
                _flameLight.pointLightInnerRadius = UnityEngine.Random.Range(minInnerRadius, maxInnerRadius);
            }
        }

    }
}