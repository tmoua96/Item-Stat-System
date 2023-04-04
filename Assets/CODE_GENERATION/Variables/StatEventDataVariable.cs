using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class StatEventDataEvent : UnityEvent<StatEventData> { }

	[CreateAssetMenu(
	    fileName = "StatEventDataVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Custom/Stat Data",
	    order = 120)]
	public class StatEventDataVariable : BaseVariable<StatEventData, StatEventDataEvent>
	{
	}
}