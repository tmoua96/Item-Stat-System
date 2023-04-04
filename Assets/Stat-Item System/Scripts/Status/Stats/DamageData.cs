using System;
using UnityEngine;

[Serializable]
public struct DamageData
{
    /// <summary>
    /// The object that did the damage
    /// </summary>
    public readonly MonoBehaviour damager;
    /// <summary>
    /// The amount of damage to do.
    /// </summary>
    public readonly float amount;
    /// <summary>
    /// The type of damage to do(Flat or percent damage)
    /// </summary>
    public readonly DamageType type;
    /// <summary>
    /// Determines whether damage reduces from current HP or max HP.
    /// </summary>
    public readonly HealthPercentType healthType;
    /// <summary>
    /// The amount to knockback(in percentage)
    /// </summary>
    public readonly float knockbackAmount;
    /// <summary>
    /// The direction the attack is going towards.
    /// </summary>
    public readonly Vector3 direction;
    /// <summary>
    /// The stat that the damage is targeting.
    /// </summary>
    public readonly StatData targetStat;

    /// <summary>
    /// Data about an attack.
    /// </summary>
    /// <param name="damager">The stats of the attacker which contains their stats for damage and crit.</param>
    /// <param name="amount">The amount to deal.</param>
    /// <param name="type">The flat or percentage of damage to deal.</param>
    public DamageData(MonoBehaviour damager, float amount, DamageType type, StatData targetStat) : this(damager, amount, type, HealthPercentType.DamageMaxHP, 0, Vector3.zero, targetStat) { }

    /// <summary>
    /// Data about an attack.
    /// </summary>
    /// <param name="damager">The stats of the attacker which contains their stats for damage and crit.</param>
    /// <param name="amount">The amount to deal.</param>
    /// <param name="type">The flat or percentage of damage to deal.</param>
    /// <param name="healthType">Determines whether damage reduces from current HP or max HP.</param>
    public DamageData(MonoBehaviour damager, float amount, DamageType type, HealthPercentType healthType, StatData targetStat) : this(damager, amount, type, healthType, 0, Vector3.zero, targetStat) { }

    /// <summary>
    /// Data about an attack.
    /// </summary>
    /// <param name="damager">The object that attacked.</param>
    /// <param name="amount">The amount to deal.</param>
    /// <param name="type">The flat or percentage of damage to deal.</param>
    /// <param name="healthType">Determines whether damage reduces from current HP or max HP.</param>
    /// <param name="knockbackAmount">The percentage to knock back a target.</param>
    /// <param name="direction">The direction the attack is moving towards.</param>
    public DamageData(MonoBehaviour damager, float amount, DamageType type, HealthPercentType healthType, float knockbackAmount, Vector3 direction, StatData targetStat)
    {
        this.damager = damager;
        this.amount = amount;
        this.type = type;
        this.healthType = healthType;
        this.knockbackAmount = knockbackAmount;
        this.direction = Vector3.zero;
        this.targetStat = targetStat;
    }

    public enum DamageType
    {
        Flat,
        Percent
    }

    public enum DamageElement
    {
        Physical,
        Magical
    }

    public enum HealthPercentType
    {
        DamageCurrentHP,
        DamageMaxHP,
    }
}