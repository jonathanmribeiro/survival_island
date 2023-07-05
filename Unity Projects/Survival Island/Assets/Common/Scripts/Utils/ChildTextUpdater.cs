using TMPro;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public class ChildTextUpdater : MonoBehaviour
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

        public void Disable() 
            => _text.gameObject.SetActive(false);
        public void Enable() 
            => _text.gameObject.SetActive(true);
    }
}