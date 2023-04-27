using SurvivalIsland.Common.Constants;
using SurvivalIsland.Common.Management;
using UnityEngine;

namespace SurvivalIsland.MainMenu.Management
{
    internal class MainMenuUIManager : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GameObject
                .FindGameObjectWithTag(Tags.GAMECONTROLLER)
                .GetComponent<GameManager>();
        }

        internal void EnterGameplay()
        {
            _gameManager.EnterGameplay();
        }
    }
}