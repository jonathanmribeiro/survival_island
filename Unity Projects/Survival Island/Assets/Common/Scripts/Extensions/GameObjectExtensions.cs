using SurvivalIsland.Common.Utils;
using UnityEngine;

namespace SurvivalIsland.Common.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject FindChild(this GameObject self, string name)
        {
            var childCound = self.transform.childCount;
            GameObject foundling = null;

            for (int i = 0; i < childCound; i++)
            {
                var currentChild = self.transform.GetChild(i);

                if (string.Equals(currentChild.name, name))
                {
                    foundling = currentChild.gameObject;
                }

                if (foundling != null)
                {
                    break;
                }

                if (currentChild.transform.childCount > 1)
                {
                    foundling = currentChild.gameObject.FindChild(name);
                }
            }

            return foundling;
        }

        public static bool TryGetParentComponent<T>(this GameObject self, out T component)
        {
            component = default;

            if (self.TryGetComponent<ParentTriggerInvoker>(out var parentTriggerInvoker))
            {
                return parentTriggerInvoker.transform.parent.TryGetComponent(out component);
            }

            return false;
        }
    }
}