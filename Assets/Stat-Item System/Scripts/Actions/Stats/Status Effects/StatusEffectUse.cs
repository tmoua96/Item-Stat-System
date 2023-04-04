using UnityEngine;

[CreateAssetMenu(fileName = "Use_SE_New", menuName = "Use Action/Status Effect/Apply Status Effect")]
public class StatusEffectUse : Use
{
    [SerializeField, Tooltip("The status effect to apply on use.")]
    private StatusEffectData statusEffect;

    public override void UseEffect(MonoBehaviour user)
    {
        if(user.TryGetComponent<StatusEffectManager>(out var manager))
        {
            manager.ApplyStatusEffect(statusEffect);
        }
    }
}
