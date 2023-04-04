using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    private StatData data;
    public StatData Data => data;

    [SerializeField]
    protected float baseValue;
    public float BaseValue => baseValue;

    public virtual float Value 
    {
        get
        {
            if (isDirty || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                value = CalculateFinalValue();
                isDirty = false;
            }
            return value;
        } 
    }

    [SerializeField]
    protected float value;
    [SerializeField]
    protected float lastBaseValue = float.MinValue;
    [SerializeField]
    protected bool isDirty = false;

    [SerializeField]
    protected List<StatModifier> modifiers;
    public ReadOnlyCollection<StatModifier> Modifiers => modifiers.AsReadOnly();

    public Stat()
    {
        modifiers = new List<StatModifier>();
    }

    public Stat(StatData data, float baseValue) : this()
    {
        this.data = data;
        this.baseValue = baseValue;
        value = baseValue;
    }

    public Stat(Stat stat)
    {
        data = stat.data;
        baseValue = stat.baseValue;
        value = stat.value;
        lastBaseValue = stat.lastBaseValue;
        isDirty = stat.isDirty;

        foreach(var modifier in stat.modifiers)
        {
            modifiers.Add(modifier);
        }
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < modifiers.Count; i++)
        {
            StatModifier modifier = modifiers[i];

            switch (modifier.Type)
            {
                case StatModifierType.Flat:
                    finalValue += Mathf.Round(modifier.Value);

                    break;
                case StatModifierType.PercentAdditive:
                    sumPercentAdd += modifier.Value;

                    if (i + 1 >= modifiers.Count || modifiers[i + 1].Type != StatModifierType.PercentAdditive)
                    {
                        finalValue = Mathf.Round(finalValue * (1 + sumPercentAdd));
                        sumPercentAdd = 0;
                    }

                    break;
                case StatModifierType.PercentMultiplicative:
                    finalValue = Mathf.Round(finalValue * (1 + modifier.Value));

                    break;
            }
        }

        return finalValue;
    }

    public virtual bool AddModifier(StatModifier modifier)
    {
        if (modifier == null || data != modifier.Data)
            return false;
        if (modifiers.Any(x => x.Source == modifier.Source))
            return false;

        isDirty = true;
        modifiers.Add(modifier);
        modifiers.Sort(CompareModifierOrder);
        return true;
    }

    public virtual bool RemoveModifier(StatModifier modifier)
    {
        if (data == modifier.Data && modifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        }

        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool removed = false;

        for(int i = modifiers.Count - 1; i >= 0; i--)
        {
            if(modifiers[i].Source == source)
            {
                isDirty = true;
                removed = true;
                modifiers.RemoveAt(i);
            }
        }

        return removed;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Value < b.Value)
            return -1;
        else if (a.Value > b.Value)
            return 1;
        else
            return 0;
    }
}
