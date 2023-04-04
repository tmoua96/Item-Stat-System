using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public bool PopulateSaveData(SaveData data, SaveableEntity saveable);
    public bool LoadSaveData(SaveData data, SaveableEntity saveable);
}
