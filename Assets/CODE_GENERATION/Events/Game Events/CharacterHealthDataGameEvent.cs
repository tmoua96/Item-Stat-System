using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "CharacterHealthDataGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Custom/Character Health Data",
	    order = 120)]
	public sealed class CharacterHealthDataGameEvent : GameEventBase<CharacterHealthData>
	{
	}
}