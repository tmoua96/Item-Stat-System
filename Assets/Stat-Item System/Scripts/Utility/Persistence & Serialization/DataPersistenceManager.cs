using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data Persistence/Manager")]
public class DataPersistenceManager : ScriptableObject
{
    [SerializeField]
    private int maxSaveFiles = 200;
    public int MaxSaveFiles => maxSaveFiles;

    private SaveData saveData;
    private List<SaveableEntity> saveableObjects = new List<SaveableEntity>();
    private List<ISaveableScriptableObject> saveableSOs = new List<ISaveableScriptableObject>();

    [SerializeField]
    private DataHandler dataHandler;

    [SerializeField]
    private Logging logger;

    private void OnEnable()
    {
        saveableObjects.Clear();
        saveableSOs.Clear();
    }

    public void NewGame()
    {
        saveData = new SaveData();
    }

    public void LoadGame(int saveSlot)
    {
        saveData = dataHandler.Load(saveSlot);

        if (saveData == null)
        {
            logger?.Log("Save data not found. Initializing data to defaults.", this);
            NewGame();
            return;
        }

        for (int i = 0; i < saveableObjects.Count; i++)
        {
            saveableObjects[i].RestoreState(saveData);
        }
    }

    public void SaveGame(int saveSlot)
    {
        if (saveSlot > maxSaveFiles)
            return;

        saveData = dataHandler.Load(saveSlot);

        if(saveData == null)
            NewGame();

        for(int i = 0; i < saveableObjects.Count; i++)
        {
            saveableObjects[i].CaptureState(saveData);
        }

        dataHandler.Save(saveData, saveSlot);
    }

    public void AddSaveable(SaveableEntity saveable)
    {
        if(!saveableObjects.Contains(saveable))
            saveableObjects.Add(saveable);
    }

    public void RemoveSaveable(SaveableEntity saveable)
    {
        if (saveableObjects.Contains(saveable))
            saveableObjects.Remove(saveable);
    }

    public void AddSaveableSO(ISaveableScriptableObject saveable)
    {  
        if(!saveableSOs.Contains(saveable))
            saveableSOs.Add(saveable);
    }

    public void RemoveSaveableSO(ISaveableScriptableObject saveable)
    {
        if(saveableSOs.Contains(saveable))
            saveableSOs.Remove(saveable);
    }
}