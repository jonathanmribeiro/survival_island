using SurvivalIsland.Gameplay.Management;
using System;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public class ChildButtonAction : MonoBehaviour
    {
        private Action _action;
        private GameplaySceneManager _sceneManager;

        public void Prepare(GameplaySceneManager gameplaySceneManager, Action action)
        {
            _action = action;
            _sceneManager = gameplaySceneManager;
        }

        public void OnClick()
        {
            _action.Invoke();
            _sceneManager.BlockInput();
        }
    }
}