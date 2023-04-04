using UnityEngine;

[CreateAssetMenu(fileName = "Use_MaterialChange_New", menuName = "Use Action/Rendering/Material Change")]
public class MaterialChangeUse : Use
{
    [SerializeField]
    private Material material;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        if(user.TryGetComponent<MeshRenderer>(out var renderer))
            renderer.material = material;
    }
}
