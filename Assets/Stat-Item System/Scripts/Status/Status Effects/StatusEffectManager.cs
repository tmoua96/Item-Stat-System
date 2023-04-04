using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System.Linq;

public class StatusEffectManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private List<StatusEffect> currentStatusEffects = new List<StatusEffect>();

    public StatusEffect[] CurrentStatusEffects
    {
        get
        {
            return currentStatusEffects.ToArray();
        }
    }

    public List<StatusEffectData> ImmuneDebuffs { get; private set; }   // TODO: have yet to implement

    // List used to stop the same status effects from being applied in the same frame.
    private List<StatusEffectData> currentlyApplyingStatuses = new List<StatusEffectData>();

    [SerializeField]
    private StatusEffectEventDataGameEvent StatusChangedEvent;

    [SerializeField]
    private Logging logger;

    private void Update()
    {
        for (int i = currentStatusEffects.Count - 1; i >= 0; i--)
        {
            currentStatusEffects[i].OnUpdate(this);

            if (currentStatusEffects[i].IsExpired)
                RemoveStatusEffect(currentStatusEffects[i].Data);
        }
    }

    /// <summary>
    /// Apply a status effect. Reapplies a status effect if it's already active.
    /// </summary>
    /// <param name="data">The status effect to be applied.</param>
    public void ApplyStatusEffect(StatusEffectData data)
    {
        if (data == null)
            return;

        if (currentlyApplyingStatuses.Contains(data))
        {
            logger?.Log($"Tried to apply {data.Name} but already applying the same status effect this frame.", this);
            return;
        }

        StatusEffect status = new StatusEffect(data);

        currentlyApplyingStatuses?.Add(status.Data);

        // If effect wants to be applied again, remove and apply a new application. Can change implementation if wanted but this is intended for now.
        if (currentStatusEffects.Contains(status))
            currentStatusEffects.Remove(status);

        status.OnApply(this);
        currentStatusEffects?.Add(status);

        StatusChangedEvent?.Raise(new StatusEffectEventData(CurrentStatusEffects));

        logger?.Log($"The status effect {status.Data.name} was applied at {status.TimeApplied}", this);

        currentlyApplyingStatuses.Remove(data);
    }

    public void RemoveStatusEffect(StatusEffectData data)
    {
        if (data == null)
            return;

        StatusEffect status = currentStatusEffects.Find(x => x.Data == data);

        if (status == null)
            return;

        status.OnRemove(this);
        currentStatusEffects.Remove(status);

        StatusChangedEvent?.Raise(new StatusEffectEventData(CurrentStatusEffects));

        logger?.Log($"The status effect {status.Data.name} was removed at {Time.time}. Applied at {status.TimeApplied}", this);
    }

    public bool HasStatusEffect(StatusEffectData data)
    {
        return currentStatusEffects.Any(x => x.Data == data);
    }

    public void ClearStatusEffects()
    {
        for(int i = currentStatusEffects.Count - 1; i >= 0; i--)
        {
            currentStatusEffects[i].OnRemove(this);
        }
        currentStatusEffects.Clear();

        StatusChangedEvent?.Raise(new StatusEffectEventData(CurrentStatusEffects));

        logger?.Log($"The status effects were all cleared.", this);
    }

    public bool IsStatusActive(StatusEffectData data)
    {
        return currentStatusEffects.Any(x => x.Data == data);
    }

    public bool PopulateSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
        {
            playerData = new SaveData.PlayerData();
            data.playerData[saveable.ID] = playerData;
        }

        playerData.statusEffectData = new(currentStatusEffects.ToArray());

        return true;
    }

    public bool LoadSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
            return false;

        //currentStatusEffects.Clear();
        
        //for (int i = 0; i < playerData.statusEffectData.statusEffects.Length; i++)
        //{
        //    ApplyStatusEffect(playerData.statusEffectData.statusEffects[i].Data);   // TODO: if i want to keep their duration, come back and finish implementing serialize/deserialize 
        //}

        return true;
    }
}
