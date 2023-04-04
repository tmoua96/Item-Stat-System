using System;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "SE_New", menuName = "Stats/Status Effect")]
public class StatusEffectData : ScriptableObject, IComparable<StatusEffectData>
{
    public enum StatusEffectType
    {
        Buff,
        Debuff
    }

    [SerializeField]
    private new string name;
    [SerializeField][Multiline]
    private string description;
    [SerializeField]
    private Sprite icon;

    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;

    [SerializeField]
    private bool persistent = false;
    [SerializeField]
    private float duration = 15;
    [SerializeField]
    private float tickInterval = 3;

    public bool Persistent => persistent;
    public float Duration => duration;
    public float TickInterval => tickInterval;

    [SerializeField]
    private StatusEffectType type = StatusEffectType.Buff;

    public StatusEffectType Type => type;

    [SerializeField]
    private Use[] onApplyUses;
    [SerializeField]
    private Use[] onUpdateUses;
    [SerializeField]
    private Use[] onRemoveUses;

    // Don't want any changes to the original references. The references are immutable but the reference to it can be changed so
    // must return different copy of uses. 
    public ReadOnlyCollection<Use> OnApplyUses 
    { 
        get
        {
            return Array.AsReadOnly(onApplyUses);
        }
    }
    public ReadOnlyCollection<Use> OnUpdateUses
    {
        get
        {
            return Array.AsReadOnly(onUpdateUses);
        }
    }
    public ReadOnlyCollection<Use> OnRemoveUses
    {
        get
        {
            return Array.AsReadOnly(onRemoveUses);
        }
    }

    private void OnValidate()
    {
        CalculateDurationDivisibility();
        ClampTimeVariables();
    }

    private void ClampTimeVariables()
    {
        tickInterval = Mathf.Clamp(tickInterval, 0.1f, float.MaxValue);
        duration = Mathf.Clamp(duration, 0, float.MaxValue);
    }

    private void CalculateDurationDivisibility()
    {
        if(onUpdateUses.Length > 0)
            duration = Mathf.Ceil(duration / tickInterval) * tickInterval;
    }

    public int CompareTo(StatusEffectData other)
    {
        // Check for null values and compare references
        if (other == null) 
            return 1;
        if (ReferenceEquals(this, other)) 
            return 0;
        
        return name.CompareTo(other.name);
    }
}