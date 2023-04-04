using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Dictionary<Guid, PlayerData> playerData;

    public SaveData()
    {
        playerData = new();
    }

    [Serializable]
    public class PlayerData
    {
        public int playerIndex;
        public TransformData transformData;
        public StatsData statsData;
        public InventoryData inventoryData;
        public EquipmentSaveData equipmentData;
        public StatusEffectData statusEffectData;
    }

    [Serializable]
    public struct TransformData
    {
        public SerializableVector3 position;
        public SerializableQuaternion rotation;
        public SerializableVector3 velocity;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            this.position = new(position);
            this.rotation = new(rotation);
            this.velocity = new(velocity);
        }
    }

    [Serializable]
    public class StatsData
    {
        public float currentHealth;
        public float maxHealth;
        public Dictionary<StatData, Stat> stats;

        public StatsData() 
        {
            stats = new Dictionary<StatData, Stat>();
        }

        public StatsData(float currentHealth, float maxHealth, Dictionary<StatData, Stat> stats)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
            this.stats = stats;
        }
    }

    [Serializable]
    public class InventoryData
    {
        public Item[] items;

        public InventoryData()
        {
            items = new Item[20];
        }

        public InventoryData(Item[] items)
        {
            this.items = items;
        }   
    }

    [Serializable]
    public class EquipmentSaveData
    {
        public EquipmentData[] equipment;

        public EquipmentSaveData()
        {
            equipment = new EquipmentData[10];
        }

        public EquipmentSaveData(EquipmentData[] equipment)
        {
            this.equipment = equipment;
        }
    }

    [Serializable]
    public class StatusEffectData
    {
        public StatusEffect[] statusEffects;

        public StatusEffectData()
        {
            statusEffects = new StatusEffect[15];
        }

        public StatusEffectData(StatusEffect[] statusEffects)
        {
            this.statusEffects = statusEffects;
        }
    }
}