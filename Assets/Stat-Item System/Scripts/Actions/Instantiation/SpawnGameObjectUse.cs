using UnityEngine;

[CreateAssetMenu(fileName = "Use_SpawnGO_New", menuName = "Use Action/Instantiation/Spawn Game Object")]
public class SpawnGameObjectUse : Use
{
    [SerializeField]
    private GameObject spawnGameObject;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private bool setUserAsParent = false;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if (spawnGameObject == null)
            return;
        Instantiate(spawnGameObject, user.transform.position + offset, Quaternion.identity, setUserAsParent ? user.transform : null);
    }
}
