using UnityEngine;
using UnityEngine.UI;

namespace SurvivalIsland.Common.Utils
{
    public static class ResourceLoader
    {
        public static Sprite Load(string imageName)
        {
            return Resources.Load<Sprite>(imageName);
        }

        public static Sprite LoadAll(string imageName, string spriteName)
        {
            Sprite[] all = Resources.LoadAll<Sprite>(imageName);

            foreach (var s in all)
            {
                if (s.name == spriteName)
                {
                    return s;
                }
            }
            return null;
        }
    }

}