using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System.Collections.ObjectModel;
using System.Linq;

public class EquipmentManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private List<EquipmentData> equipment = new();

    public EquipmentData[] Equipment 
    {
        get
        {
            return equipment.ToArray();
        }
    }

    [SerializeField]
    private EquipmentEventDataGameEvent EquipmentChangedEvent;

    [SerializeField]
    private Logging logger;

    public void Equip(EquipmentData data)
    {
        if (data == null)
            return;
        if (IsEquipmentTypeFilled(data.EquipmentType))
        {
            EquipmentData toRemove = equipment.Find(x => x.EquipmentType == data.EquipmentType);
            RemoveEquipment(toRemove);
        }

        AddEquipment(data);

        EquipmentChangedEvent?.Raise(new EquipmentEventData(Equipment, data, true));
    }

    public void Unequip(EquipmentData data)
    {
        if (data == null || !equipment.Contains(data))
            return;

        RemoveEquipment(data);

        EquipmentChangedEvent?.Raise(new EquipmentEventData(Equipment, data, false));
    } 

    private void AddEquipment(EquipmentData equipment)
    {
        if (equipment == null)
            return;

        this.equipment.Add(equipment);
        AddEquipmentStats(equipment);
    }

    private void RemoveEquipment(EquipmentData equipment)
    {
        if (equipment == null)
            return;

        this.equipment.Remove(equipment);
        RemoveEquipmentStats(equipment);
    }

    private bool IsEquipmentTypeFilled(EquipmentType equipmentType)
    {
        return equipment.Any(x => x.EquipmentType == equipmentType);
    }

    private void AddEquipmentStats(EquipmentData equipment)
    {
        if (!TryGetComponent<CharacterStats>(out var stats))
            return;

        ReadOnlyDictionary<StatData, float> statBonuses = equipment.StatBonuses;
        ReadOnlyDictionary<StatData, StatPercentData> statPercentBonuses = equipment.StatPercentBonuses;

        if (statBonuses != null)
        {
            foreach (var bonus in statBonuses)
            {
                bool success = stats[bonus.Key].AddModifier(new StatModifier(bonus.Key, bonus.Value, StatModifierType.Flat, equipment.Item));
                logger?.Log($"Addition Successful: {success}. Stat {bonus.Key.StatType} New Value: {stats[bonus.Key].Value}", this);
            }
        }
        if(statPercentBonuses != null)
        {
            foreach (var percentBonus in statPercentBonuses)
            {
                bool success = stats[percentBonus.Key].AddModifier(new StatModifier(percentBonus.Key, percentBonus.Value.value, percentBonus.Value.type, equipment.Item));
                logger?.Log($"Addition Successful: {success}. Stat {percentBonus.Key.StatType} New Value: {stats[percentBonus.Key].Value}", this);
            }
        }
    }

    private void RemoveEquipmentStats(EquipmentData equipment)
    {
        if (TryGetComponent<CharacterStats>(out var stats))
        {
            foreach(var stat in stats.Stats)
            {
                stat.RemoveAllModifiersFromSource(equipment.Item);
            }
        }
    }

    public bool PopulateSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
        {
            playerData = new SaveData.PlayerData();
            data.playerData[saveable.ID] = playerData;
        }

        playerData.equipmentData = new(equipment.ToArray());

        return true;
    }

    public bool LoadSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
            return false;
        if (playerData.equipmentData == null)
        {
            logger.LogWarning("Equipment data is null", this);
            return false;
        }

        EquipmentData[] equipmentArray = playerData.equipmentData.equipment;

        if (equipmentArray == null)
            return false;

        for (int i = equipment.Count - 1; i >= 0; i--)
        {
            RemoveEquipment(equipment[i]);
        }

        for (int i = 0; i < equipmentArray.Length; i++)
        {
            AddEquipment(equipmentArray[i]);
        }

        EquipmentChangedEvent?.Raise(new EquipmentEventData(Equipment, equipment[equipmentArray.Length - 1], true));

        return true;
    }
}