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
        private MainCharacterInventoryManager _inventoryManager;
        private StateManagerBase _managerInteracting;

        private GameObject _actionBalloon;
        private Animator _actionBalloonAnimator;

        private SelectorManager _selectorManager;


        private void Awake()
        {
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
            _actionBalloon = gameObject.FindChild("MainCharacterBalloon");
            _actionBalloonAnimator = _actionBalloon.GetComponent<Animator>();
            _selectorManager = gameObject.GetComponentInChildren<SelectorManager>();
        }

        public void Prepare()
        {
            _selectorManager.Prepare();
        }

        public bool ExecuteAction(PlayerActionTypes performedAction, InventoryItemModel itemModel = null)
            => performedAction switch
            {
                PlayerActionTypes.Chopping => _inventoryManager.TryAddItem(itemModel),
                PlayerActionTypes.Collecting => _inventoryManager.TryAddItem(itemModel),
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
            switch (_managerInteracting?.GetAction())
            {
                case PlayerActionTypes.Collecting:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), false);
                    break;
                case PlayerActionTypes.Chopping:
                    _actionBalloon.SetActive(true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Chopping.ToString(), true);
                    _actionBalloonAnimator.SetBool(PlayerActionTypes.Collecting.ToString(), false);
                    break;
                default:
                    _actionBalloon.SetActive(false);
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