using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class StatEventDataReference : BaseReference<StatEventData, StatEventDataVariable>
	{
	    public StatEventDataReference() : base() { }
	    public StatEventDataReference(StatEventData value) : base(value) { }
	}
}