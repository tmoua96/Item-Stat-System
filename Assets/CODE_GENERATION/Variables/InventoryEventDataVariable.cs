using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class InventoryEventDataEvent : UnityEvent<InventoryEventData> { }

	[CreateAssetMenu(
	    fileName = "InventoryEventDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Custom/Inventory Data",
	    order = 120)]
	public class InventoryEventDataVariable : BaseVariable<InventoryEventData, InventoryEventDataEvent>
	{
	}
}