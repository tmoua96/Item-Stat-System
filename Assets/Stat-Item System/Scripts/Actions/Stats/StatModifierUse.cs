using UnityEngine;

[CreateAssetMenu(fileName = "StatMod_New", menuName = "Use Action/Stats/Stat Modifier")]
public class StatModifierUse : Use
{
    [SerializeField]
    private StatData data;
    [SerializeField]
    private float amount;
    [SerializeField]
    private StatModifierType modifierType;
    //[SerializeField]
    //private Object source;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if (!user.TryGetComponent<CharacterStats>(out var stats))
            return;

        stats.AddModifier(new StatModifier(data, amount, modifierType, this));
    }
}
