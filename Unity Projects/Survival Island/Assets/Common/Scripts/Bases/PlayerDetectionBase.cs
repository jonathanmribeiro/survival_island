using SurvivalIsland.Common.Constants;
using UnityEngine;

namespace SurvivalIsland.Common.Bases
{
    public class PlayerDetectionBase
    {
        public bool _playerInRange = false;

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                _playerInRange = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagConstants.PLAYER))
                _playerInRange = false;
        }
    }
}
