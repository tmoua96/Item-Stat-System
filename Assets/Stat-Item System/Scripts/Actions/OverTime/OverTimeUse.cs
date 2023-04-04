using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Use_OverTime_New", menuName = "Use Action/Over Time Use")]
public class OverTimeUse : Use
{
    [SerializeField]
    private float interval = 3f;
    [SerializeField]
    private int totalTicks;

    [SerializeField]
    private Use[] useActions;

    public override void UseEffect(MonoBehaviour user)
    {
        if (user == null)
            return;

        user.StartCoroutine(DoOvertimeAction(user));        // TODO - come back to handle game pause
    }

    private IEnumerator DoOvertimeAction(MonoBehaviour user)
    {
        int remainingTicks = totalTicks;
        
        while (remainingTicks > 0)
        {
            remainingTicks--;

            yield return new WaitForSeconds(interval);
            
            for (int i = 0; i < useActions.Length; i++)
                useActions[i].UseEffect(user);
        }
    }
}
