using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class EquipmentEventDataReference : BaseReference<EquipmentEventData, EquipmentEventDataVariable>
	{
	    public EquipmentEventDataReference() : base() { }
	    public EquipmentEventDataReference(EquipmentEventData value) : base(value) { }
	}
}