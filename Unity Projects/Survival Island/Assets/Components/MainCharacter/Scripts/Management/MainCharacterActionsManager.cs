using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Components.Selector;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterActionsManager : MonoBehaviour
    {
        public PlayerActionTypes ActionToExecute;
        private MainCharacterInventoryManager _inventoryManager;
        private PlayerActionStateManagerBase _managerInteracting;

        private GameObject _actionBalloon;
        private Animator _actionBalloonAnimator;
        private SpriteRenderer _actionBalloonSpriteRenderer;

        private SelectorManager _selectorManager;

        private void Awake()
        {
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
            _actionBalloon = gameObject.FindChild("MainCharacterBalloon");
            _actionBalloonAnimator = _actionBalloon.GetComponent<Animator>();
            _actionBalloonSpriteRenderer = _actionBalloon.GetComponent<SpriteRenderer>();
            _selectorManager = gameObject.GetComponentInChildren<SelectorManager>();
        }

        public void Prepare()
        {
            _selectorManager.Prepare();
        }

        public bool ExecuteAction(PlayerActionTypes performedAction, object itemModel = null)
            => performedAction switch
            {
                PlayerActionTypes.Chopping => _inventoryManager.TryAddItem(itemModel as InventoryItemModel),
                PlayerActionTypes.Collecting => _inventoryManager.TryAddItem(itemModel as InventoryItemModel),
                _ => false,
            };

        public void UpdateActions()
        {
            UpdateActionBalloon();
            UpdateSelector();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            collision.TryGetComponent(out _managerInteracting);
        }

        private void OnTriggerExit2D()
        {
            _managerInteracting = null;
        }

        private void UpdateActionBalloon()
        {
            ActionToExecute = _managerInteracting != null ? _managerInteracting.GetAction() : PlayerActionTypes.None;

            switch (ActionToExecute)
            {
                case PlayerActionTypes.Collecting:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), false);
                    _actionBalloonSpriteRenderer.enabled = true;
                    break;
                case PlayerActionTypes.Chopping:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), false);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), true);
                    _actionBalloonSpriteRenderer.enabled = true;
                    break;
                default:
                    _actionBalloon.SetActive(false);
                    _actionBalloonSpriteRenderer.enabled = false;
                    break;
            }
        }

        private void UpdateSelector()
        {
            Transform location = transform;
            Vector2 size = Vector2.one;

            if (_managerInteracting != null)
            {
                location = _managerInteracting.SelectorLocation;
                size = _managerInteracting.SelectorSize;
            }

            _selectorManager.UpdateSelector(location, size);
        }
    }
}