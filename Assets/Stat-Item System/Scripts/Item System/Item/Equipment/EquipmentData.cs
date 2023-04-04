using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData_New", menuName = "Item System/Item/Equipment/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    [SerializeField]
    private ItemData item;
    // TODO: make a serializable dictionary
    [SerializeField]
    private Dictionary<StatData, float> statBonuses = new();
    [SerializeField]
    private Dictionary<StatData, StatPercentData> statPercentBonuses = new();

    [SerializeField]
    private EquipmentType equipmentType;

    public ItemData Item => item;

    public ReadOnlyDictionary<StatData, float> StatBonuses => new(statBonuses);
    public ReadOnlyDictionary<StatData, StatPercentData> StatPercentBonuses => new(statPercentBonuses);

    public EquipmentType EquipmentType => equipmentType;

    public float GetStatBonus(StatData data)
    {
        return statBonuses[data];
    }

    public StatPercentData GetStatPercentBonus(StatData data)
    {
        return statPercentBonuses[data];
    }
}
