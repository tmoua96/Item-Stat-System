using UnityEngine;

[CreateAssetMenu(fileName = "Use_Damage_New", menuName = "Use Action/Stats/Damage")]
public class DamageUse : Use
{
    [SerializeField]
    [Tooltip("The amount of HP to heal. Any whole number for flat damage and between 0 and 1 for percent.")]
    private float amount;
    [SerializeField]
    private DamageData.DamageType damageType;
    [SerializeField]
    private DamageData.HealthPercentType healthPercentType = DamageData.HealthPercentType.DamageMaxHP;
    [SerializeField]
    private StatData targetStat;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if (user.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(new DamageData(user, amount, damageType, healthPercentType, targetStat));
    }
}
