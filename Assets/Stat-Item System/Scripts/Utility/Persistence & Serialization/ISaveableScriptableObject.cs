public interface ISaveableScriptableObject
{
    DataPersistenceManager PersistenceManager { get; }
    void RegisterSelfAsSaveable();
    void UnregisterSelfAsSaveable();
    void SaveData(SaveData saveData);
    void LoadData(SaveData saveData);
}
