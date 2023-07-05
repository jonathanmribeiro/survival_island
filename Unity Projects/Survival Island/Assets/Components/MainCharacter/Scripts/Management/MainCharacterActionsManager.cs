using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.Selector;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterActionsManager : MonoBehaviour
    {
        public PlayerActionTypes ActionToExecute;
        public PlayerActionStateManagerBase ManagerInteracting;

        private MainCharacterInventoryManager _inventoryManager;

        private GameObject _actionBalloon;
        private Animator _actionBalloonAnimator;
        private SpriteRenderer _actionBalloonSpriteRenderer;
        private ChildTextUpdater _timeLabel;

        private SelectorManager _selectorManager;

        private void Awake()
        {
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
            _actionBalloon = gameObject.FindChild("MainCharacterBalloon");
            _actionBalloonAnimator = _actionBalloon.GetComponent<Animator>();
            _actionBalloonSpriteRenderer = _actionBalloon.GetComponent<SpriteRenderer>();
            _selectorManager = gameObject.GetComponentInChildren<SelectorManager>();
            _timeLabel = _actionBalloon.GetComponent<ChildTextUpdater>();
        }

        public void Prepare()
        {
            _selectorManager.Prepare();
            _timeLabel.Disable();
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
            collision.TryGetComponent(out ManagerInteracting);
        }

        private void OnTriggerExit2D()
        {
            ManagerInteracting = null;
        }

        private void UpdateActionBalloon()
        {
            ActionToExecute = ManagerInteracting != null ? ManagerInteracting.GetAction() : PlayerActionTypes.None;

            switch (ActionToExecute)
            {
                case PlayerActionTypes.Collecting:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), false);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.FeedCampfire.ToString(), false);
                    _actionBalloonSpriteRenderer.enabled = true;
                    break;
                case PlayerActionTypes.Chopping:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), false);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.FeedCampfire.ToString(), false);
                    _actionBalloonSpriteRenderer.enabled = true;
                    break;
                case PlayerActionTypes.FeedCampfire:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), false);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), false);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.FeedCampfire.ToString(), true);
                    _actionBalloonSpriteRenderer.enabled = true;
                    UpdateTimeLabel(ManagerInteracting?.TimeLeft);
                    break;
                default:
                    _actionBalloon.SetActive(false);
                    _actionBalloonSpriteRenderer.enabled = false;
                    UpdateTimeLabel();
                    break;
            }
        }

        private void UpdateSelector()
        {
            Transform location = transform;
            Vector2 size = Vector2.one;

            if (ManagerInteracting != null)
            {
                location = ManagerInteracting.SelectorLocation;
                size = ManagerInteracting.SelectorSize;
            }

            _selectorManager.UpdateSelector(location, size);
        }

        private void UpdateTimeLabel(TimeSpan? timeLeft = null)
        {
            if (timeLeft == null)
            {
                _timeLabel.Disable();
                return;
            }

            _timeLabel.Enable();
            _timeLabel.UpdateUI(timeLeft.Value.ToString(@"h\:mm"));
        }
    }
}