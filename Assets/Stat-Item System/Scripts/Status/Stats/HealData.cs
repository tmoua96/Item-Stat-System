using System;
using UnityEngine;

[Serializable]
public struct HealData
{
    public readonly MonoBehaviour healer;
    public readonly float amount;
    public readonly HealType type;
    public readonly HealthPercentType healthType;

    public HealData(MonoBehaviour healer, float amount, HealType type) : this(healer, amount, type, HealthPercentType.HealMaxHP) { }

    public HealData(MonoBehaviour healer, float amount, HealType type, HealthPercentType healthType)
    {
        this.healer = healer;
        this.amount = amount;
        this.type = type;
        this.healthType = healthType;
    }

    public enum HealType
    {
        Flat,
        Percent
    }

    public enum HealthPercentType
    {
        HealCurrentHP,
        HealMaxHP,
    }
}
