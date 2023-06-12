using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Components.Signs;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Campfire
{
    public class UnlitState : PlayerDetectionBase, IPlayerActionState
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

        public void EnterState()
        {
            _pristineWood.SetActive(true);
            _burnedWood.SetActive(false);
            _pendingConstructionWood.SetActive(false);
            _flame.SetActive(false);

            _boundariesCollider.enabled = true;
            _activationTrigger.enabled = true;

            _signAlert.Prepare(SignStates.InactiveState);
        }

        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            throw new NotImplementedException();
        }

        public void ExitState()
        {
            throw new NotImplementedException();
        }

        public PlayerActionTypes GetAction()
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            throw new NotImplementedException();
        }
    }
}