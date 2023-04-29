using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Management;
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
        }

        private void Start()
        {
            _cameraManager.SetFollowingTarget(_mainCharacter.transform);
        }

        private void Update()
        {
            _inputManager.UpdateInput();

            _mainCharacterManager.UpdateInput(_inputManager.InputModel);
            _cameraManager.UpdateCamera();
        }
    }
}