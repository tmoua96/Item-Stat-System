using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class CharacterHealthDataReference : BaseReference<CharacterHealthData, CharacterHealthDataVariable>
	{
	    public CharacterHealthDataReference() : base() { }
	    public CharacterHealthDataReference(CharacterHealthData value) : base(value) { }
	}
}