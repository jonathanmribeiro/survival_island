using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Components.Signs;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Campfire
{
    public class UnlitState : StateBase
    {
        private CampfireManager _manager;

        private GameObject _pristineWood;
        private GameObject _burnedWood;
        private GameObject _pendingConstructionWood;
        private GameObject _flame;

        private CapsuleCollider2D _boundariesCollider;
        private CircleCollider2D _activationTrigger;

        private SignManager _signAlert;

        public UnlitState(CampfireManager manager)
        {
            _manager = manager;

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
            _pristineWood.SetActive(true);
            _burnedWood.SetActive(false);
            _pendingConstructionWood.SetActive(false);
            _flame.SetActive(false);

            _boundariesCollider.enabled = true;
            _activationTrigger.enabled = true;

            _signAlert.Prepare(_manager, SignStates.InactiveState);
        }

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            var woodAmount = _manager.CampfireInventory.CountItemsOfType(InventoryItemType.Wood);

            if (woodAmount > 0)
                _manager.EnterLitState();
        }

        public override PlayerActionTypes GetAction() 
            => PlayerActionTypes.FeedCampfire;
    }
}