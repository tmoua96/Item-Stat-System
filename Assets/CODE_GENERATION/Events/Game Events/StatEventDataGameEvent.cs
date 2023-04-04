using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "StatEventDataGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Custom/Stat Data",
	    order = 120)]
	public sealed class StatEventDataGameEvent : GameEventBase<StatEventData>
	{
	}
}