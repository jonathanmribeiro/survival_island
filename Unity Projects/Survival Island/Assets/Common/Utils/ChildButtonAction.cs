using System;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    internal class ChildButtonAction : MonoBehaviour
    {
        private Action _action;

        internal void Prepare(Action action)
        {
            _action = action;
        }

        public void OnClick()
        {
            _action.Invoke();
        }
    }
}