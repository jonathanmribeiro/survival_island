using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Management;
using SurvivalIsland.Common.Models;
using SurvivalIsland.Common.Utils;
using UnityEngine;

namespace SurvivalIsland.Components.MainCharacter
{
    public class MainCharacterManager : MonoBehaviour
    {
        private MainCharacterMovementManager _movementManager;
        private MainCharacterAnimationManager _animationManager;
        private MainCharacterInventoryManager _inventoryManager;
        private MainCharacterVitalityManager _vitalityManager;
        private MainCharacterActionsManager _actionsManager;

        private void Awake()
        {
            _movementManager = GetComponent<MainCharacterMovementManager>();
            _animationManager = GetComponent<MainCharacterAnimationManager>();
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
            _vitalityManager = GetComponent<MainCharacterVitalityManager>();
            _actionsManager = GetComponent<MainCharacterActionsManager>();
        }

        public void Prepare(InputManager inputManager, DayNightCycle dayNightCycle)
        {
            _movementManager.Prepare(inputManager);
            _animationManager.Prepare(inputManager);
            _vitalityManager.Prepare(dayNightCycle);
            _inventoryManager.Prepare();
            _actionsManager.Prepare();
        }

        public void UpdateMainCharacter()
        {
            _movementManager.UpdateMovement();
            _animationManager.UpdateMovement();
            _vitalityManager.UpdateVitality();
            _actionsManager.UpdateActions();
        }

        public VitalitySystemModel GetVitalitySystem()
            => _vitalityManager.VitalitySystem;
        public InventoryItemSlot GetInventorySlot(int inventoryItemIndex)
            => _inventoryManager.GetInventorySlot(inventoryItemIndex);
        public InventoryItemSlot GetCharacterItem(int inventoryItemIndex)
            => _inventoryManager.GetCharacterItem(inventoryItemIndex);

        public void OnClick_QuickAction1Button()
        {
            var inventorySlot = GetInventorySlot(0);

            if (inventorySlot == null)
                return;
        }

        public void OnClick_QuickAction2Button()
        {
            var inventorySlot = GetInventorySlot(1);

            if (inventorySlot == null)
                return;
        }

        public void OnClick_QuickAction3Button()
        {
            var inventorySlot = GetInventorySlot(2);

            if (inventorySlot == null)
                return;
        }

        public void OnClick_QuickAction4Button()
        {
            var inventorySlot = GetInventorySlot(3);

            if (inventorySlot == null)
                return;
        }

        public void RemoveInventoryItem(InventoryItemType type)
        {
            _inventoryManager.RemoveInventoryItemByType(type);
        }
    }
}