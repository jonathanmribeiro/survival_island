using SurvivalIsland.Common.Utils;
using UnityEngine;

namespace SurvivalIsland.Components.Selector
{
    public class SelectorManager : MonoBehaviour
    {
        private SmoothFollow _smoothFollow;
        private SpriteRenderer _spriteRenderer;
        private float DistanceFromParent 
            => Vector3.Distance(transform.parent.position, transform.position);
        
        private void Awake()
        {
            _smoothFollow = GetComponent<SmoothFollow>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Prepare()
        {
            gameObject.SetActive(false);
            _smoothFollow.Target = transform.parent;
            _smoothFollow.Velocity = Vector3.one;
        }

        public void UpdateSelector(Transform parent, Vector2 size)
        {
            gameObject.SetActive(DistanceFromParent >= 0.5f);
            _spriteRenderer.size = size;
            _smoothFollow.Target = parent;
            _smoothFollow.UpdateSmoothFollow();
        }
    }
}