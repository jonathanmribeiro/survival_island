using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.Signs;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Campfire
{
    public class LitState : PlayerDetectionBase, IPlayerActionState
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

        public LitState(CampfireManager manager, CampfireProps campfireProps, DayNightCycle dayNightCycle)
        {
            _manager = manager;

            _campfireProps = campfireProps;
            _dayNightCycle = dayNightCycle;

            _pristineWood = _manager.gameObject.FindChild("PristineWood");
            _burnedWood = _manager.gameObject.FindChild("BurnedWood");
            _pendingConstructionWood = _manager.gameObject.FindChild("PendingConstructionWood");
            _flame = _manager.gameObject.FindChild("Flame");

            _boundariesCollider = _manager.gameObject.GetComponent<CapsuleCollider2D>();
            _activationTrigger = _manager.gameObject.GetComponent<CircleCollider2D>();

            _signAlert = _manager.GetComponentInChildren<SignManager>();
        }

        public void EnterState()
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
                _campfireProps.TimeEnteredLitState = _dayNightCycle.CurrentTime;
                _campfireProps.TimeBurnedWood = _campfireProps.TimeEnteredLitState.Value;
            }
        }

        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            /*Left empty on purpose*/
        }

        public void ExitState()
        {
        }

        public PlayerActionTypes GetAction() => PlayerActionTypes.None;

        public void UpdateState()
        {
            if (_manager.CountItemsOfType(InventoryItemType.Wood) == 0)
            {
                _manager.EnterExtinguishedState();
                return;
            }

            DateTime timeToConsumeWood = _campfireProps.TimeBurnedWood.Add(_campfireProps.TimeNeededToBurnWood);

            if (_dayNightCycle.CurrentTime >= timeToConsumeWood)
            {
                _manager.Remove(InventoryItemType.Wood);
                _campfireProps.TimeBurnedWood = _dayNightCycle.CurrentTime;
            }
        }
    }
}