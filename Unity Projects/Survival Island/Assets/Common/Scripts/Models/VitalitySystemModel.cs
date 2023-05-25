namespace SurvivalIsland.Common.Models
{
    public class VitalitySystemModel
    {
        public float Health { get; set; }
        public float Hunger { get; set; }
        public float Thirst { get; set; }
        public float Energy { get; set; }

        public VitalitySystemModel()
        {
            Health = 100.0f;
            Hunger = 100.0f;
            Thirst = 100.0f;
            Energy = 100.0f;
        }

        public void IncreasePhysiologicalNeeds()
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
