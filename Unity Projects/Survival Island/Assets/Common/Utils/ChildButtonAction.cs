using System;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public class ChildButtonAction : MonoBehaviour
    {
        private Action _action;

        public void Prepare(Action action)
        {
            _action = action;
        }

        public void OnClick()
        {
            _action.Invoke();
        }
    }
}