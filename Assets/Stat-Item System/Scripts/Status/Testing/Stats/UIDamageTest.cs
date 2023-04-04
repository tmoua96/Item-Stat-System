using TMPro;
using UnityEngine;

public class UIDamageTest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text damageText;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float damage = 5;
    [SerializeField, Range(0, 1)]
    private float damagePercent = 0;
    [SerializeField]
    private DamageData.DamageType damageType;
    [SerializeField]
    private DamageData.HealthPercentType healthPercentType = DamageData.HealthPercentType.DamageMaxHP;
    [SerializeField]
    private StatData targetStat;

    private void Update()
    {
        if(damageType == DamageData.DamageType.Flat)
        {
            damageText.text = $"Do {damage} damage";
        }
        else
        {
            string hpType = "";
            if (healthPercentType == DamageData.HealthPercentType.DamageCurrentHP)
                hpType = "current";
            else
                hpType = "max";
            damageText.text = $"Damage {damagePercent * 100}% of {hpType} health";
        }
    }

    public void DamageTarget()
    {
        if(target.TryGetComponent<IDamageable>(out var damageable))
        {
            float damage = damageType == DamageData.DamageType.Flat ? this.damage : damagePercent;
            damageable.TakeDamage(new DamageData(this, damage, damageType, healthPercentType, targetStat));
        }
    }
}
