using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class InventoryEventDataReference : BaseReference<InventoryEventData, InventoryEventDataVariable>
	{
	    public InventoryEventDataReference() : base() { }
	    public InventoryEventDataReference(InventoryEventData value) : base(value) { }
	}
}