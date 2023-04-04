using UnityEngine;

public class SaveableTransform : MonoBehaviour, ISaveable
{
    public bool LoadSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
            return false;

        transform.position = playerData.transformData.position.UnityVector;
        transform.rotation = playerData.transformData.rotation.UnityQuaternion;

        return true;
    }

    public bool PopulateSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
        {
            playerData = new();
            data.playerData[saveable.ID] = playerData;
        }

        playerData.transformData = new(transform.position, transform.rotation, Vector3.zero);

        return true;
    }
}
