using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Management;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.Campfire;
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

        private TreeManager[] _treeManagers;
        private CampfireManager[] _campfireManagers;

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

            PrepareTrees();
            PrepareCampfires();
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

            _uiManager.UpdateUI();

            foreach (var treeManager in _treeManagers)
            {
                treeManager.UpdateTree();
            }
        }

        private void ExecutePlayerAction()
        {
            foreach (var manager in _treeManagers)
            {
                manager.ExecuteAction(_mainCharacterActionsManager.ExecuteAction);
            }

            foreach (var manager in _campfireManagers)
            {
                manager.ExecuteAction(_mainCharacterActionsManager.ExecuteAction);
            }
        }

        private void PrepareTrees()
        {
            _treeManagers = FindObjectsOfType<TreeManager>();

            foreach (var manager in _treeManagers)
            {
                manager.Prepare(_dayNightCycle);
            }
        }

        private void PrepareCampfires()
        {
            _campfireManagers = FindObjectsOfType<CampfireManager>();

            foreach (var manager in _campfireManagers)
            {
                manager.Prepare(_uiManager);
            }
        }

        public void BlockInput() => InputIsLocked = true;
        public void ReleaseInput() => InputIsLocked = false;
    }
}