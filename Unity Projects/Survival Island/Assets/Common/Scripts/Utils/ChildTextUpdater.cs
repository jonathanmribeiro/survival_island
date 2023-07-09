using SurvivalIsland.Common.Extensions;
using TMPro;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public class ChildTextUpdater : MonoBehaviour
    {
        private TMP_Text _text;

        public void Prepare(string childName)
        {
            _text = gameObject.FindChild(childName).GetComponent<TMP_Text>();
        }

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();

        }

        public void UpdateUI(string text)
        {
            _text.text = text;
        }

        public void Disable()
            => _text.gameObject.SetActive(false);
        public void Enable()
            => _text.gameObject.SetActive(true);
    }
}