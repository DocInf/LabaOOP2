using System;

public abstract class NaturalPhenomenon
{
    public TimeSpan Duration { get; set; }
    public string Scale { get; set; }

    public abstract void PerformDamageControl();
    public abstract void Forecast();
    public abstract void AssessDamage();
    public abstract void Evacuate();
}

