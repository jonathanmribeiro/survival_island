using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Management;
using UnityEngine;

namespace SurvivalIsland.MainMenu.Management
{
    public class MainMenuSceneManager : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameObject
                .FindGameObjectWithTag(TagConstants.GAMECONTROLLER)
                .GetComponent<GameManager>();
        }
    }
}