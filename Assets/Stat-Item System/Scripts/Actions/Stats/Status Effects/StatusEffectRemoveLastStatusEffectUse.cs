using UnityEngine;

[CreateAssetMenu(fileName = "Use_SERemoveLast_New", menuName = "Use Action/Status Effect/Remove Last Status Effect")]
public class StatusEffectRemoveLastStatusEffectUse : Use
{
    [SerializeField]
    private StatusEffectData.StatusEffectType type;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if(user.TryGetComponent<StatusEffectManager>(out var manager))
        {
            StatusEffect[] statuses = manager.CurrentStatusEffects;
            float latestTime = 0;
            StatusEffectData toRemove = null;

            foreach (var status in statuses)
            {
                if(status.Data.Type == type && status.TimeApplied > latestTime)
                {
                    latestTime = status.TimeApplied;
                    toRemove = status.Data;
                }
            }

            manager.RemoveStatusEffect(toRemove);
        }
    }
}
