using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData_New", menuName = "Stats/Stat Data")]
public class StatData : ScriptableObject, IComparable<StatData>
{
    [SerializeField]
    private string statType;

    public string StatType => statType;

    /// <summary>
    /// Sets the names of the stat type only if it's null or empty.
    /// </summary>
    /// <param name="statType">The name of the Stat Type.</param>
    public void SetStatTypeIfNullOrEmpty(string statType)
    {
        if(string.IsNullOrEmpty(this.statType))
            this.statType = statType;
    }

    public int CompareTo(StatData other)
    {
        return statType.CompareTo(other.statType);
    }
}
