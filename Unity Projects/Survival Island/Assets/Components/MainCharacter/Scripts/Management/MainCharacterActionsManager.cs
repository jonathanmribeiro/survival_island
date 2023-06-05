using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterActionsManager : MonoBehaviour
    {
        private MainCharacterInventoryManager _inventoryManager;
        private PlayerActionTypes _actionToExecute;

        private GameObject _actionBalloon;
        private Animator _actionBalloonAnimator;

        private void Awake()
        {
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
            _actionBalloon = gameObject.FindChild("MainCharacterBalloon");
            _actionBalloonAnimator = _actionBalloon.GetComponent<Animator>();
        }

        public void Prepare()
        {
            UpdateActionBalloon();
        }

        public bool ExecuteAction(PlayerActionTypes performedAction, InventoryItemModel itemModel = null)
            => performedAction switch
            {
                PlayerActionTypes.Chopping => _inventoryManager.TryAddItem(itemModel),
                PlayerActionTypes.Collecting => _inventoryManager.TryAddItem(itemModel),
                _ => false,
            };

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent<StateManagerBase>(out var stateManager))
            {
                _actionToExecute = stateManager.GetAction();
            }
            else
            {
                _actionToExecute = PlayerActionTypes.None;
            }

            UpdateActionBalloon();
        }

        private void OnTriggerExit2D()
        {
            _actionToExecute = PlayerActionTypes.None;
            UpdateActionBalloon();
        }

        private void UpdateActionBalloon()
        {
            switch (_actionToExecute)
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
    }
}