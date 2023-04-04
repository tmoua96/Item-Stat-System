using UnityEngine;

[CreateAssetMenu(fileName = "Use_SERemove_New", menuName = "Use Action/Status Effect/Remove Status Effect")]
public class StatusEffectRemoveUse : Use
{
    [SerializeField]
    private StatusEffectData status;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if(user.TryGetComponent<StatusEffectManager>(out var manager))
            manager.RemoveStatusEffect(status);
    }
}
