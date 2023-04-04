using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Base Stats")]
public class BaseStatValues : ScriptableObject
{
    [SerializeField]
    private StatDataCollection statData;
    [SerializeField, Tooltip("This is meant to align with the float values(baseValues) as if it were a dictionary.")]
    private List<StatData> statDataList;
    [SerializeField, Tooltip("This is meant to align with the StatData values(statDataList) as if it were a dictionary.")]
    private List<float> baseValues;

    public ReadOnlyDictionary<StatData, float> BaseStats
    {
        get
        {
            Dictionary<StatData, float> baseStats = new();

            for (int i = 0; i < statDataList.Count; i++)
            {
                baseStats[statDataList[i]] = baseValues[i];
            }

            return new ReadOnlyDictionary<StatData, float>(baseStats);
        }
    }

#if UNITY_EDITOR
    private void Reset()
    {
        LoadStatCollection();

        statDataList = new();
        baseValues = new();

        if (statData == null || statData.Data == null)
            return;

        for (int i = 0; i < statData.Data.Count; i++)
        {
            statDataList.Add(statData.Data[i]);
            baseValues.Add(0);
        }
    }

    private void LoadStatCollection()
    {
        string[] statsPath = AssetDatabase.FindAssets("t:StatDataCollection");
        if (statsPath == null || statsPath.Length == 0)
            return;
        if (AssetDatabase.GUIDToAssetPath(statsPath[0]) == null)
            return;

        string path = AssetDatabase.GUIDToAssetPath(statsPath[0]);
        statData = AssetDatabase.LoadAssetAtPath<StatDataCollection>(path);
    }
#endif
}
