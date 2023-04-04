using UnityEngine;

public abstract class DataHandler : ScriptableObject
{
    [SerializeField]
    protected Logging logger;

    private enum FilePath
    {
        PersistentDataPath,
        DataPath
    }

    [SerializeField]
    private FilePath directory = FilePath.PersistentDataPath;
    [SerializeField]
    protected string dataFilePath = "";
    [SerializeField]
    protected string fileExtension = "txt";
    protected string dataDirPath = "";

    private void OnValidate()
    {
        dataDirPath = directory == FilePath.PersistentDataPath ? Application.persistentDataPath : Application.dataPath;
    }

    public abstract bool Save(SaveData data, int saveSlot);
    public abstract SaveData Load(int saveSlot);
}
