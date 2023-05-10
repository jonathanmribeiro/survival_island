using UnityEngine;

namespace SurvivalIsland.Common.Extensions
{
    internal static class GameObjectExtensions
    {
        internal static GameObject FindChild(this GameObject self, string name)
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
    }
}