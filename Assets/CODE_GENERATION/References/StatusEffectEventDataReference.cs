using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class StatusEffectEventDataReference : BaseReference<StatusEffectEventData, StatusEffectEventDataVariable>
	{
	    public StatusEffectEventDataReference() : base() { }
	    public StatusEffectEventDataReference(StatusEffectEventData value) : base(value) { }
	}
}