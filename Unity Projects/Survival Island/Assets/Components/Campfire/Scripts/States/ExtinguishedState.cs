using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Components.Signs;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Campfire
{
    public class ExtinguishedState : StateBase
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

        public ExtinguishedState(CampfireManager manager, CampfireProps campfireProps)
        {
            _manager = manager;
            _campfireProps = campfireProps;

            _pristineWood = _manager.gameObject.FindChild("PristineWood");
            _burnedWood = _manager.gameObject.FindChild("BurnedWood");
            _pendingConstructionWood = _manager.gameObject.FindChild("PendingConstructionWood");
            _flame = _manager.gameObject.FindChild("Flame");

            _boundariesCollider = _manager.gameObject.GetComponent<CapsuleCollider2D>();
            _activationTrigger = _manager.gameObject.GetComponent<CircleCollider2D>();

            _signAlert = _manager.GetComponentInChildren<SignManager>();
        }

        public override void EnterState()
        {
            _pristineWood.SetActive(false);
            _burnedWood.SetActive(true);
            _pendingConstructionWood.SetActive(false);
            _flame.SetActive(false);

            _boundariesCollider.enabled = true;
            _activationTrigger.enabled = true;

            _signAlert.Prepare(_manager, SignStates.InactiveState);
        }

        public override void UpdateState()
        {
            TimeSpan totalHoursLit = new(_manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood), 0, 0);
            _manager.TimeLeft = totalHoursLit;
        }

        public override PlayerActionTypes GetAction()
            => PlayerActionTypes.FeedCampfire;

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            if (_manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood) > 0)
            {
                _manager.EnterLitState();
            }
        }

        public override void ExecuteQuickAction(Action<InventoryItemModel> playerActionCallback, InventoryItemModel itemModel)
        {
            var woodAmount = _manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood);
            if (woodAmount >= _campfireProps.MaxWood)
                return;

            if (_manager.CampfireInventory.TryAddItem(itemModel))
            {
                playerActionCallback.Invoke(itemModel);
            }
        }
    }
}