using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Components.Signs;
using UnityEngine;

namespace SurvivalIsland.Components.Fishing
{
    public class WaitingState : StateBase
    {
        private readonly FishingManager _manager;
        private readonly SignManager _signAlert;

        private readonly GameObject _bucket;
        private readonly GameObject _rod;

        public WaitingState(FishingManager manager)
        {
            _manager = manager;

            _bucket = _manager.gameObject.FindChild("Bucket");
            _rod = _manager.gameObject.FindChild("Rod");

            _signAlert = _manager.GetComponentInChildren<SignManager>();
        }

        public override void EnterState()
        {
            _bucket.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            _rod.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            _bucket.GetComponent<CircleCollider2D>().enabled = false;
            _rod.GetComponent<CircleCollider2D>().enabled = true;

            _signAlert.Prepare(_manager, SignStates.InactiveState);

            _manager.SelectorLocation = _rod.transform;
        }

        public override PlayerActionTypes GetAction()
            => PlayerActionTypes.StartFishing;
    }
}