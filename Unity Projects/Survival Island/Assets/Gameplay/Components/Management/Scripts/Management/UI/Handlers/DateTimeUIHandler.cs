using TMPro;
using UnityEngine;

namespace SurvivalIsland.Gameplay.Management
{
    public class DateTimeUIHandler : MonoBehaviour
    {
        private GameObject _canvas;
        private GameObject _dateTimePanel;
        private GameObject _dateTimeLabel;

        private void Awake()
        {
            _canvas = GameObject.Find("Canvas");
        }

        public void UpdateUI(string dateTime)
        {
            _dateTimeLabel.GetComponent<TextMeshPro>().text = dateTime;
        }
    }
}