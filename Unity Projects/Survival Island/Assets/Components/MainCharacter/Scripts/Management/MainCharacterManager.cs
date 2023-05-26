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

        private void Awake()
        {
            _movementManager = GetComponent<MainCharacterMovementManager>();
            _animationManager = GetComponent<MainCharacterAnimationManager>();
            _inventoryManager = GetComponent<MainCharacterInventoryManager>();
            _vitalityManager = GetComponent<MainCharacterVitalityManager>();
        }

        public void Prepare(InputManager inputManager, DayNightCycle dayNightCycle)
        {
            _movementManager.Prepare(inputManager);
            _animationManager.Prepare(inputManager);
            _vitalityManager.Prepare(dayNightCycle);
            _inventoryManager.Prepare();
        }

        public void UpdateMainCharacter()
        {
            _movementManager.UpdateMovement();
            _animationManager.UpdateMovement();
            _vitalityManager.UpdateVitality();
        }

        public VitalitySystemModel GetVitalitySystem()
        {
            return _vitalityManager.VitalitySystem;
        }

        public InventoryItemModel GetInventoryItem(int inventoryItemIndex)
        {
            return _inventoryManager.GetInventoryItem(inventoryItemIndex);
        }

        public void OnClick_QuickAction1Button()
        {
            var inventoryItem = GetInventoryItem(0);

            if (inventoryItem == null)
                return;
        }

        public void OnClick_QuickAction2Button()
        {
            var inventoryItem = GetInventoryItem(1);

            if (inventoryItem == null)
                return;
        }

        public void OnClick_QuickAction3Button()
        {
            var inventoryItem = GetInventoryItem(2);

            if (inventoryItem == null)
                return;
        }

        public void OnClick_QuickAction4Button()
        {
            var inventoryItem = GetInventoryItem(3);

            if (inventoryItem == null)
                return;
        }
    }
}