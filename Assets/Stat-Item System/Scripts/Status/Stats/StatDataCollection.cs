using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "StatDataCollection_New", menuName = "Stats/Stat Data Collection")]
public class StatDataCollection : ScriptableObject
{
    [SerializeField]
    private List<StatData> data;

    public ReadOnlyCollection<StatData> Data => data.AsReadOnly();

#if UNITY_EDITOR
    private bool showEditButtons = false;

    // TODO: add buttons or method to call in inspector
    public void CreateStatData(string statName)
    {
        StatData newStatData = CreateInstance<StatData>();
        newStatData.name = $"StatData_{statName.Replace(" ", "")}";
        newStatData.SetStatTypeIfNullOrEmpty(statName);

        string savePath = Path.Combine("Assets", "ScriptableObjects", "Stats", "Stats", "Types", newStatData.name + ".asset");

        AssetDatabase.CreateAsset(newStatData, savePath);
        data.Add(newStatData);

        RemoveNullValuesAndSort();

        AssetDatabase.SaveAssets();
    }

    // buttons
    public void DeleteStatData(params StatData[] statTypes)
    {
        if (statTypes == null)
            return;

        for (int i = statTypes.Length - 1; i >= 0; i--)
        {
            if (statTypes[i] == null)
                continue;

            data.Remove(statTypes[i]);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(statTypes[i]));
        }

        RemoveNullValuesAndSort();

        AssetDatabase.SaveAssets();
    }

    // buttons
    private void RemoveNullValuesAndSort()
    {
        data.RemoveAll(x => x == null);
        data.Sort();
    }

    public void ToggleStatDataFoldoutGroup()
    {
        showEditButtons = !showEditButtons;
    }
#endif
}
