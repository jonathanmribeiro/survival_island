using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Management;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using SurvivalIsland.Components.Trees;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class GameplaySceneManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private InputManager _inputManager;
        private GameObject _mainCharacter;
        private MainCharacterManager _mainCharacterManager;
        private CameraManager _cameraManager;
        private DayNightCycle _dayNightCycle;
        private GameplayUIManager _uiManager;
        private MainCharacterInventoryUIManager _mainCharacterInventoryUIHandler;
        
        private TreeManager[] _treeManagers;

        private void Awake()
        {
            _gameManager = GameObject.FindGameObjectWithTag(TagConstants.GAMECONTROLLER).GetComponent<GameManager>();
            _mainCharacter = GameObject.FindGameObjectWithTag(TagConstants.PLAYER);
            _mainCharacterManager = _mainCharacter.GetComponent<MainCharacterManager>();
            _mainCharacterInventoryUIHandler = _mainCharacter.GetComponent<MainCharacterInventoryUIManager>();
            _inputManager = GetComponent<InputManager>();
            _cameraManager = GetComponent<CameraManager>();
            _dayNightCycle = GetComponentInChildren<DayNightCycle>();
            _uiManager = GetComponentInChildren<GameplayUIManager>();
        }

        private void Start()
        {
            _cameraManager.SetFollowingTarget(_mainCharacter.transform);
            _inputManager.Prepare(InputType.Virtual, PlayerHasFiredEvent);
            _mainCharacterManager.Prepare(_inputManager, _dayNightCycle);
            _uiManager.Prepare(_mainCharacterManager, _dayNightCycle, _mainCharacterInventoryUIHandler);

            PrepareTrees();
        }

        private void Update()
        {
            _inputManager.UpdateInput();

            Debug.Log(_inputManager.GetCurrentInput().Vertical);

            _cameraManager.UpdateCamera();
            _dayNightCycle.UpdateDayNightCycle();
            _mainCharacterManager.UpdateMainCharacter();
            _uiManager.UpdateUI();
        }

        private void PlayerHasFiredEvent()
        {
            foreach (var treeManager in _treeManagers)
            {
                treeManager.ExecuteAction();
            }
        }

        private void PrepareTrees()
        {
            _treeManagers = FindObjectsOfType<TreeManager>();

            foreach(var treeManager in _treeManagers)
            {
                treeManager.Prepare();
            }
        }
    }
}