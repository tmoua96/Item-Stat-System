using UnityEngine;
using System;

[Serializable]
public class EquipmentEventData
{
    [SerializeField]
    private EquipmentData[] equipment;
    public EquipmentData[] Equipment => equipment;
    [SerializeField]
    private EquipmentData modifiedEquipment;
    public EquipmentData ModifiedEquipment => modifiedEquipment;

    [SerializeField]
    private bool wasEquipmentAdded;
    public bool WasEquipmentAdded => wasEquipmentAdded;

    public EquipmentEventData(EquipmentData[] equipment, EquipmentData modifiedEquipment, bool wasEquipmentAdded)
    {
        this.equipment = equipment;
        this.modifiedEquipment = modifiedEquipment;
        this.wasEquipmentAdded = wasEquipmentAdded;
    }
}
