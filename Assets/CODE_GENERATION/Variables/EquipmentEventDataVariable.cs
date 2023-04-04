using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class EquipmentEventDataEvent : UnityEvent<EquipmentEventData> { }

	[CreateAssetMenu(
	    fileName = "EquipmentEventDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Custom/Equipment Data",
	    order = 120)]
	public class EquipmentEventDataVariable : BaseVariable<EquipmentEventData, EquipmentEventDataEvent>
	{
	}
}