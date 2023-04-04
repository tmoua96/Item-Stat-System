using UnityEngine;
using ScriptableObjectArchitecture;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;

public class CharacterStats : MonoBehaviour, IDamageable, IHealable, ISaveable
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private StatData maxHealthStat;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, this[maxHealthStat].Value);
            if (currentHealth < 0.5f)
                currentHealth = 0;
            HealthChangedEvent?.Raise(new CharacterHealthData(CurrentHealth, this[maxHealthStat].Value));
        }
    }
    public Stat MaxHealth { get { return new Stat(this[maxHealthStat]); } }

    [SerializeField]
    private BaseStatValues baseStats;
    [SerializeField]
    private Stat[] stats;

    public ReadOnlyCollection<Stat> Stats 
    { 
        get
        {
            return new ReadOnlyCollection<Stat>(stats);
        }
    }

    [SerializeField]
    private CharacterHealthDataGameEvent HealthChangedEvent;


    [SerializeField, Header("Debugging")]
    private Logging logger;

    private void Start()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        if (baseStats.BaseStats == null)
            return;

        KeyValuePair<StatData, float>[] statPairs = baseStats.BaseStats.ToArray();

        stats = new Stat[statPairs.Length];

        for (int i = 0; i < statPairs.Length; i++)
        {
            stats[i] = new Stat(statPairs[i].Key, statPairs[i].Value);
        }

        CurrentHealth = this[maxHealthStat].Value;
    }

    public Stat this[StatData data]
    {
        get
        {
            return Array.Find(stats, x => x.Data == data);
        }
    }

    public bool AddModifier(StatModifier modifier)
    {
        return this[modifier?.Data]?.AddModifier(modifier) ?? false;
    }

    public bool RemoveModifier(StatModifier modifier)
    {
        return this[modifier?.Data]?.RemoveModifier(modifier) ?? false;
    }

    public bool RemoveModifiersFromSource(StatData data, object source)
    {
        return this[data]?.RemoveAllModifiersFromSource(source) ?? false;
    }

    // TODO: add more types of damage such as physical, magical, etc. -> def, magic def, etc.
    public void TakeDamage(DamageData data)
    {
        if(stats == null || stats.Length == 0) 
            return;

        float effectiveDamage;

        if(data.type == DamageData.DamageType.Flat)
        {
            effectiveDamage = data.amount - this[data.targetStat].Value;

            if (effectiveDamage < 0)
            {
                HealDamage(new HealData(data.damager, -effectiveDamage, HealData.HealType.Flat));
                return;
            }
        }
        else
        {
            effectiveDamage = data.healthType == DamageData.HealthPercentType.DamageMaxHP ?
                this[maxHealthStat].Value * data.amount - this[data.targetStat].Value : 
                currentHealth * data.amount - this[data.targetStat].Value;
        }

        CurrentHealth -= effectiveDamage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }

        logger?.Log($"{transform.name} takes {effectiveDamage} damage.", this);
    }

    public virtual void Die()
    {
        GetComponent<StatusEffectManager>().ClearStatusEffects();

        // This method may be overwritten
        logger?.Log($"{transform.name} died.", this);
    }

    public void HealDamage(HealData data)
    {
        if (stats == null || stats.Length == 0)
            return;

        float effectiveHeal;

        if(data.type == HealData.HealType.Flat)
        {
            effectiveHeal = Mathf.Round(data.amount);
        }
        else
        {
            effectiveHeal = data.healthType == HealData.HealthPercentType.HealMaxHP ?
                this[maxHealthStat].Value * data.amount : currentHealth * data.amount;
        }

        CurrentHealth += effectiveHeal;

        logger?.Log($"{transform.name} heals {effectiveHeal} hp.", this);
    }

    public bool PopulateSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
        {
            playerData = new SaveData.PlayerData();
            data.playerData[saveable.ID] = playerData;
        }

        Dictionary<StatData, Stat> stats = new();

        foreach (var stat in this.stats)
        {
            stats[stat.Data] = stat;
        }

        playerData.statsData = new(currentHealth, this[maxHealthStat].Value, stats);

        return true;
    }

    public bool LoadSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
            return false;

        SaveData.StatsData statsData = playerData.statsData;

        if (statsData == null)
            return false;
        
        foreach (var stat in stats)
        {
            for (int i = stat.Modifiers.Count - 1; i >= 0; i--)
            {
                stat.RemoveAllModifiersFromSource(stat.Modifiers[i].Source);
            }
        }

        foreach (var stat in statsData.stats)
        {
            foreach (var modifier in stat.Value.Modifiers)
            {
                this[stat.Key].AddModifier(new StatModifier(modifier.Data, modifier.Value, modifier.Type, modifier.Source));
            }
        }

        CurrentHealth = statsData.currentHealth;

        return true;
    }
}