using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Management;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.Campfire;
using SurvivalIsland.Components.Fishing;
using SurvivalIsland.Components.MainCharacter;
using SurvivalIsland.Components.Trees;
using SurvivalIsland.Gameplay.Management.UI;
using System;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class GameplaySceneManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private InputManager _inputManager;
        private GameObject _mainCharacter;
        private MainCharacterManager _mainCharacterManager;
        private MainCharacterActionsManager _mainCharacterActionsManager;
        private CameraManager _cameraManager;
        private DayNightCycle _dayNightCycle;
        private GameplayUIManager _uiManager;

        private ActionStateManagerBase[] _managers;

        public bool InputIsLocked;

        private void Awake()
        {
            _gameManager = GameObject.FindGameObjectWithTag(TagConstants.GAMECONTROLLER).GetComponent<GameManager>();
            _mainCharacter = GameObject.FindGameObjectWithTag(TagConstants.PLAYER);
            _mainCharacterManager = _mainCharacter.GetComponent<MainCharacterManager>();
            _mainCharacterActionsManager = _mainCharacter.GetComponent<MainCharacterActionsManager>();
            _inputManager = GetComponent<InputManager>();
            _cameraManager = GetComponent<CameraManager>();
            _dayNightCycle = GetComponentInChildren<DayNightCycle>();
            _uiManager = GetComponentInChildren<GameplayUIManager>();
        }

        private void Start()
        {
            _cameraManager.SetFollowingTarget(_mainCharacter.transform);
            _inputManager.Prepare(this, InputType.Virtual, ExecutePlayerAction);
            _mainCharacterManager.Prepare(_inputManager, _dayNightCycle);
            _uiManager.Prepare(this, _mainCharacterManager, _dayNightCycle);

            _dayNightCycle.SetCurrentTime(DateTime.Now);

            PrepareManagers();
        }

        private void Update()
        {
            _cameraManager.UpdateCamera();

            if (_uiManager.CurrentState is BasicUIState)
            {
                _dayNightCycle.UpdateDayNightCycle();
                _mainCharacterManager.UpdateMainCharacter();
            }

            _inputManager.UpdateInput();
            _uiManager.UpdateCurrentState();

            foreach (var manager in _managers)
            {
                manager.UpdateCurrentState();
            }
        }

        private void ExecutePlayerAction()
        {
            _mainCharacterActionsManager
                .ManagerInteracting
                ?.ExecuteAction(_mainCharacterActionsManager.ExecuteActionCallback);
        }

        private void PrepareManagers()
        {
            _managers = FindObjectsOfType<ActionStateManagerBase>();

            foreach (var manager in _managers)
            {
                if (manager is CampfireManager)
                {
                    manager.Prepare(_uiManager, _dayNightCycle);
                }
                else if (manager is FishingManager)
                {
                    manager.Prepare(_uiManager, _dayNightCycle);
                }
                else if (manager is TreeManager)
                {
                    manager.Prepare(_dayNightCycle);
                }
            }
        }

        public void BlockInput()
            => InputIsLocked = true;

        public void ReleaseInput()
            => InputIsLocked = false;
    }
}