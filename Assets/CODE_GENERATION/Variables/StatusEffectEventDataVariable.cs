using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class StatusEffectEventDataEvent : UnityEvent<StatusEffectEventData> { }

	[CreateAssetMenu(
	    fileName = "StatusEffectEventDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Custom/Status Effect Data",
	    order = 120)]
	public class StatusEffectEventDataVariable : BaseVariable<StatusEffectEventData, StatusEffectEventDataEvent>
	{
	}
}