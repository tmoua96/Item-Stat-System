using UnityEngine;

[CreateAssetMenu(fileName = "Use_Equip_New", menuName = "Use Action/Item/Equip")]
public class EquipUse : Use
{
    [SerializeField]
    private EquipmentData equipmentData;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if (user.TryGetComponent<EquipmentManager>(out var manager))
            manager.Equip(equipmentData);
    }
}
