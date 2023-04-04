using UnityEngine;

[System.Serializable]
public class StatusEffectEventData
{
    [SerializeField]
    private StatusEffect[] statusEffects;
    public StatusEffect[] StatusEffects => statusEffects;

    public StatusEffectEventData(StatusEffect[] statusEffects)
    {
        this.statusEffects = statusEffects;
    }
}
