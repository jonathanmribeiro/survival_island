using System;

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
