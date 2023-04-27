using SurvivalIsland.Common.Models;
using UnityEngine;

namespace SurvivalIsland.Common.Management
{
    internal class InputManager : MonoBehaviour
    {
        [SerializeField]
        internal InputModel InputModel;

        private void Awake()
        {
            InputModel = new();
        }

        internal void UpdateInput()
        {
            InputModel.Vertical = Input.GetAxis(Constants.Input.VERTICAL);
            InputModel.Horizontal = Input.GetAxis(Constants.Input.HORIZONTAL);
        }
    }
}