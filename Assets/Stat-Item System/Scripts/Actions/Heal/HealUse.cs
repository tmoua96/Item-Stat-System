using UnityEngine;

[CreateAssetMenu(fileName = "Use_Heal_New", menuName = "Use Action/Stats/Heal")]
public class HealUse : Use
{
    [SerializeField]
    [Tooltip("The amount of HP to heal. Any whole number for flat healing and between 0 and 1 for percent.")]
    private float amount;
    [SerializeField]
    private HealData.HealType healType;
    [SerializeField]
    private HealData.HealthPercentType healthPercentType = HealData.HealthPercentType.HealMaxHP;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if(user.TryGetComponent<IHealable>(out var healable))
            healable.HealDamage(new HealData(user, amount, healType, healthPercentType));
    }
}