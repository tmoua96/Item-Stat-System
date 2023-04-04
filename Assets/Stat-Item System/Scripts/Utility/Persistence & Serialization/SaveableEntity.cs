using UnityEditor;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField]
    private System.Guid id;
    public System.Guid ID { get { return id; } }

    [SerializeField]
    [Tooltip("This allows a guid to be generated even if there already is one. Ideally this shouldn't change " +
        "once it has been set already so that serialization doesn't get messed up.")]
    private bool generateIdWhenNotEmpty = false;

    [SerializeField]
    private DataPersistenceManager persistenceManager;
    public DataPersistenceManager PersistenceManager { get { return persistenceManager; } }

    public Logging logger;

    private void Start()
    {
        persistenceManager.AddSaveable(this);
    }

    private void OnDestroy()
    {
        persistenceManager.RemoveSaveable(this);
    }

    private void Reset()
    {
        InitializeFields();
    }

    public void CaptureState(SaveData data)
    {
        if (data == null)
            return;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            bool success = saveable.PopulateSaveData(data, this);
            if(success)
                logger?.Log($"Saving state: {saveable}. Successful", this);
            else
                logger?.LogWarning($"Saving state: {saveable} - Unsuccessful", this);
        }
    }

    public void RestoreState(SaveData data)
    {
        if (data == null)
            return;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            bool success = saveable.LoadSaveData(data, this);
            if(success)
                logger?.Log($"Loading state: {saveable} - Successful", this);
            else
                logger?.LogWarning($"Loading state: {saveable} - Unsuccessful", this);
        }
    }

    [ContextMenu("Initialize Fields")]
    private void InitializeFields()
    {
        GenerateID();
#if UNITY_EDITOR
        FindPersistenceManager();
#endif
    }

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        if(id == null || id == System.Guid.Empty || generateIdWhenNotEmpty)
            id = System.Guid.NewGuid();
    }

#if UNITY_EDITOR
    [ContextMenu("Find Persistence Manager")]
    private void FindPersistenceManager()
    {
        string[] managerPaths = AssetDatabase.FindAssets("t:DataPersistenceManager");
        if (managerPaths == null || managerPaths.Length == 0)
            return;
        if (AssetDatabase.GUIDToAssetPath(managerPaths[0]) == null)
            return;

        string path = AssetDatabase.GUIDToAssetPath(managerPaths[0]);
        persistenceManager = AssetDatabase.LoadAssetAtPath<DataPersistenceManager>(path);
    }
#endif

    public override bool Equals(object obj)
    {
        return obj is SaveableEntity entity &&
               base.Equals(obj) &&
               id == entity.id;
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(base.GetHashCode(), id);
    }
}
