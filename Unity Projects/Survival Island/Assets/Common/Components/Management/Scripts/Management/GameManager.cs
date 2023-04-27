using SurvivalIsland.Common.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SurvivalIsland.Common.Management
{
    internal class GameManager : MonoBehaviour
    {
        internal static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        internal void EnterGameplay()
        {
            SceneManager.LoadScene(Scenes.GAMEPLAY);
        }
    }
}