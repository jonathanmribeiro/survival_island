using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Components.Signs;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Fishing
{
    public class PendingConstructionState : StateBase
    {
        private readonly FishingManager _manager;
        private readonly SignManager _signAlert;
        
        private readonly GameObject _bucket;
        private readonly GameObject _rod;

        public PendingConstructionState(FishingManager manager)
        {
            _manager = manager;

            _bucket = _manager.gameObject.FindChild("Bucket");
            _rod = _manager.gameObject.FindChild("Rod");
            
            _signAlert = _manager.GetComponentInChildren<SignManager>();
        }

        public override void EnterState()
        {
            _bucket.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
            _rod.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);

            _signAlert.Prepare(_manager, SignStates.ActiveState);
        }

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            if (!_signAlert.GetAction().Equals(PlayerActionTypes.OpenConstructionUI))
                return;

            _manager.OpenCraftingUI();
        }
    }
}