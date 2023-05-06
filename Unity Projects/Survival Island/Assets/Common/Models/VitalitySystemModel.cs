namespace SurvivalIsland.Common.Models
{
    internal class VitalitySystemModel
    {
        internal float Health { get; set; }
        internal float Hunger { get; set; }
        internal float Thirst { get; set; }
        internal float Energy { get; set; }

        internal VitalitySystemModel()
        {
            Health = 100.0f;
            Hunger = 100.0f;
            Thirst = 100.0f;
            Energy = 100.0f;
        }

        internal void IncreasePhysiologicalNeeds()
        {
            Hunger -= (1.0f / (24 * 7)) * 100;
            if (Hunger < 0)
                Hunger = 0;

            Thirst -= (1.0f / (24 * 3)) * 100;
            if (Thirst < 0)
                Thirst = 0;

            Energy -= (1.0f / (24 * 11)) * 100;
            if (Energy < 0)
                Energy = 0;
        }
    }
}
