using UnityEngine;

[CreateAssetMenu(fileName = "Use_ChangeMoveSpeed_New", menuName = "Use Action/Stats/Change Move Speed")]
public class ChangeMovementSpeedUse : Use
{
    [SerializeField]
    private float percentageChange = 1f;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;
        //if(user.TryGetComponent<CharacterActor>(out var actor))
        //{
        //    actor.Velocity *= percentageChange;
        //}
        // TODO: add some type of interface or osmething for movement scripts
    }
}
