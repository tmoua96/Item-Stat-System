using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [SerializeField]
    private StatData data;
    [SerializeField]
    private float value;
    [SerializeField]
    private StatModifierType type;
    [SerializeField]
    private int order;
    [SerializeField]
    private object source;

    public StatData Data => data;
    public float Value => value;
    public StatModifierType Type => type;
    public int Order => order;
    public object Source => source;

    public StatModifier() { }

    /// <summary>
    /// A stat modifier for character stats.
    /// </summary>
    /// <param name="value">The amount to modify a stat by, percents based around 1 representing 100%(0.2 = 20%, 1.5 = 150%, etc.).</param>
    /// <param name="type">The type of stat modifier.</param>
    /// <param name="order">The order that a modifier should be applied to stats (ex. flat(index = 0) add before percent(index = 1))</param>
    public StatModifier(StatData data, float value, StatModifierType type, int order, object source)
    {
        this.data = data;
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    /// <summary>
    /// A stat modifier for character stats.
    /// </summary>
    /// <param name="value">The amount to modify a stat by, percents based around 1 representing 100%(0.2 = 20%, 1.5 = 150%, etc.).</param>
    /// <param name="type">The type of stat modifier.</param>
    public StatModifier(StatData data, float value, StatModifierType type) : this(data, value, type, (int)type, null) { }

    /// <summary>
    /// A stat modifier for character stats.
    /// </summary>
    /// <param name="value">The amount to modify a stat by, percents based around 1 representing 100%(0.2 = 20%, 1.5 = 150%, etc.).</param>
    /// <param name="type">The type of stat modifier.</param>
    /// <param name="order">The order that a modifier should be applied to stats (ex. flat(index = 0) add before percent(index = 1))</param>
    public StatModifier(StatData data, float value, StatModifierType type, int order) : this(data, value, type, order, null) { }

    /// <summary>
    /// A stat modifier for character stats.
    /// </summary>
    /// <param name="value">The amount to modify a stat by, percents based around 1 representing 100%(0.2 = 20%, 1.5 = 150%, etc.).</param>
    /// <param name="type">The type of stat modifier.</param>
    /// <param name="order">The order that a modifier should be applied to stats (ex. flat(index = 0) add before percent(index = 1))</param>
    public StatModifier(StatData data, float value, StatModifierType type, object source) : this(data, value, type, (int)type, source) { }

    public override bool Equals(object obj)
    {
        return obj is StatModifier modifier &&
               EqualityComparer<StatData>.Default.Equals(data, modifier.data) &&
               value == modifier.value &&
               type == modifier.type &&
               order == modifier.order &&
               EqualityComparer<object>.Default.Equals(source, modifier.source);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(data, value, type, order, source);
    }
}

public enum StatModifierType
{
    Flat = 100,
    PercentAdditive = 200,
    PercentMultiplicative = 300
}