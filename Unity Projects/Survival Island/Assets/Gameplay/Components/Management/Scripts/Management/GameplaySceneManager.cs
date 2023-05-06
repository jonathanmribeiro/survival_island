using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Management;
using SurvivalIsland.Common.Utils;
using SurvivalIsland.Components.MainCharacter;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    internal class GameplaySceneManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private InputManager _inputManager;
        private GameObject _mainCharacter;
        private MainCharacterManager _mainCharacterManager;
        private CameraManager _cameraManager;
        private DayNightCycle _dayNightCycle;
        private GameplayUIManager _uiManager;

        private void Awake()
        {
            _gameManager = GameObject
                .FindGameObjectWithTag(TagConstants.GAMECONTROLLER)
                .GetComponent<GameManager>();

            _mainCharacter = GameObject
                .FindGameObjectWithTag(TagConstants.PLAYER);

            _mainCharacterManager = _mainCharacter
                .GetComponent<MainCharacterManager>();

            _inputManager = GetComponent<InputManager>();

            _cameraManager = GetComponent<CameraManager>();

            _dayNightCycle = GetComponentInChildren<DayNightCycle>();

            _uiManager = GetComponentInChildren<GameplayUIManager>();
        }

        private void Start()
        {
            _cameraManager.SetFollowingTarget(_mainCharacter.transform);

            Prepare();
        }

        private void Prepare()
        {
            _inputManager.Prepare(InputType.Virtual);
        }

        private void Update()
        {
            _inputManager.UpdateInput();
            
            var currentInput = _inputManager.GetCurrentInput();
            _mainCharacterManager.UpdateInput(currentInput);
            
            _cameraManager.UpdateCamera();
            
            _dayNightCycle.UpdateDayNightCycle();

            var currentDateTime = _dayNightCycle.GetCurrentTime();
            _uiManager.UpdateUI(currentDateTime);
        }
    }
}