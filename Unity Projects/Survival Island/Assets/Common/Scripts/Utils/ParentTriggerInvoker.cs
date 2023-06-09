using SurvivalIsland.Common.Bases;
using UnityEngine;

namespace SurvivalIsland.Common.Utils
{
    public class ParentTriggerInvoker : MonoBehaviour
    {
        private ActionStateManagerBase _parentManager;

        private void Awake()
            => _parentManager = transform.parent.GetComponent<ActionStateManagerBase>();

        private void OnTriggerStay2D(Collider2D collision)
            => _parentManager.CurrentState.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision)
            => _parentManager.CurrentState.OnTriggerExit2D(collision);
    }
}