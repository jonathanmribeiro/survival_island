using SurvivalIsland.Common.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SurvivalIsland.Common.Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public void EnterGameplay()
        {
            SceneManager.LoadScene(SceneConstants.GAMEPLAY);
        }
    }
}