using System;
using UnityEngine;

[Serializable]
public class StatEventData
{
    [SerializeField]
    private Stat[] stats;
    public Stat[] Stats => stats;

    public StatEventData(Stat[] stats)
    {
        this.stats = stats;
    }
}
