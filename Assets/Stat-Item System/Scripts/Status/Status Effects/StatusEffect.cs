using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class StatusEffect : IComparable<StatusEffect>
{
    [SerializeField]
    private StatusEffectData data;
    public StatusEffectData Data => data;

    // TODO: idk if these can or even need to be serialized. SEs will typically only be set in combat and you can't save during combat anyway, but maybe persist outside combat?
    [SerializeField]
    private float timeRemaining;    
    [SerializeField]
    private float tickTimeRemaining;
    [SerializeField]
    private int ticksRemaining;

    public float TimeRemaining => timeRemaining;
    public float TimeApplied { get; private set; }
    public bool IsExpired { get { return !data.Persistent && timeRemaining <= 0 && ticksRemaining <= 0; } }

    // The update use effects to be acted on each tick interval. Cached here because 
    // creating new array each tick seems like a waste, unlike apply and remove which happen only once on creation and removal.
    private ReadOnlyCollection<Use> onUpdateUses;
    private readonly bool hasUpdateUses;

    private readonly Logging logger;

    public StatusEffect(StatusEffectData data) : this(data, null) { }

    public StatusEffect(StatusEffectData data, Logging logger)
    {
        this.data = data;
        onUpdateUses = data.OnUpdateUses;
        hasUpdateUses = onUpdateUses.Count > 0;
        timeRemaining = data.Persistent ? Mathf.Infinity : data.Duration;
        tickTimeRemaining = data.TickInterval;
        if(hasUpdateUses)
            ticksRemaining = Mathf.RoundToInt(data.Duration / data.TickInterval);
        this.logger = logger;
    }

    public void OnApply(StatusEffectManager manager)
    {
        TimeApplied = Time.time;
        
        ReadOnlyCollection<Use> onApplyUses = data.OnApplyUses;

        for (int i = 0; i < onApplyUses.Count; i++)
        {
            onApplyUses[i]?.UseEffect(manager);
        }
    }

    public void OnUpdate(StatusEffectManager manager)
    {
        if(timeRemaining > 0)
            timeRemaining -= Time.deltaTime;    // TODO: if tickInterval is smaller than Time.deltaTime, all ticks won't go off when duration is done. 
                                            // Maybe do something where you add up multiple uses for each frame missed and decrement the ticksRemaining based on how many were missed each frame.
        if (!hasUpdateUses)
            return;

        tickTimeRemaining -= Time.deltaTime;

        if (tickTimeRemaining <= 0)
        {
            for (int i = 0; i < onUpdateUses.Count; i++)
            {
                onUpdateUses[i]?.UseEffect(manager);
            }

            tickTimeRemaining = data.TickInterval;
            ticksRemaining--;
        }
    }

    public void OnRemove(StatusEffectManager manager)
    {
        ReadOnlyCollection<Use> onRemoveUses = data.OnRemoveUses;

        for (int i = 0; i < onRemoveUses.Count; i++)
        {
            onRemoveUses[i]?.UseEffect(manager);
        }
    }

    public int CompareTo(StatusEffect other)
    {
        return data.CompareTo(other.data);
    }

    public override bool Equals(object obj)
    {
        return obj is StatusEffect effect &&
               EqualityComparer<StatusEffectData>.Default.Equals(data, effect.data);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(data);
    }
}
