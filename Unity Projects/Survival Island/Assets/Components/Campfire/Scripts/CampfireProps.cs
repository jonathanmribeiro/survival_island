using System;

namespace SurvivalIsland.Components.Campfire
{
    [Serializable]
    public class CampfireProps
    {
        public DateTime? TimeEnteredLitState;
        public DateTime TimeBurnedWood;
        public TimeSpan TimeNeededToBurnWood;

        public CampfireProps()
        {
            TimeNeededToBurnWood = TimeSpan.FromHours(1);
        }
    }
}