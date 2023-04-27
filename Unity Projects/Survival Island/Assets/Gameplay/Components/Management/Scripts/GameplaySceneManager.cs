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
        private MainCharacterManager _mainCharacterManager;

        private void Awake()
        {
            _gameManager = GameObject
                .FindGameObjectWithTag(Tags.GAMECONTROLLER)
                .GetComponent<GameManager>();

            _mainCharacterManager = GameObject
                .FindGameObjectWithTag(Tags.PLAYER)
                .GetComponent<MainCharacterManager>();

            _inputManager = GetComponent<InputManager>();
        }

        private void Start()
        {
            Debug.Log("GameplayManager");
        }

        private void Update()
        {
            _inputManager.UpdateInput();

            _mainCharacterManager.UpdateInput(_inputManager.InputModel);
        }
    }
}