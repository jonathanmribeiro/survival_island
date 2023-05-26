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
    }

}