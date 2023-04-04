using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "InventoryEventDataGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Custom/Inventory Data",
	    order = 120)]
	public sealed class InventoryEventDataGameEvent : GameEventBase<InventoryEventData>
	{
	}
}