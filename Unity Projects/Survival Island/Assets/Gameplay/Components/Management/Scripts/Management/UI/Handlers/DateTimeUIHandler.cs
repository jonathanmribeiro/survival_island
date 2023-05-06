using TMPro;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    internal class DateTimeUIHandler : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();

        }

        public void UpdateUI(string datetime)
        {
            _text.text = datetime;
        }
    }
}