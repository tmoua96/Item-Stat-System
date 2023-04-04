//using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializableQuaternion
{
    public float w;
    public float x;
    public float y;
    public float z;

    //[JsonIgnore]
    public Quaternion UnityQuaternion
    {
        get
        {
            return new Quaternion(x, y, z, w);
        }
    }

    public SerializableQuaternion(Quaternion q)
    {
        w = q.w;
        x = q.x;
        y = q.y;
        z = q.z;
    }

    public static List<SerializableQuaternion> GetSerializableList(List<Quaternion> qList)
    {
        List<SerializableQuaternion> list = new List<SerializableQuaternion>(qList.Count);
        for (int i = 0; i < qList.Count; i++)
        {
            list.Add(new SerializableQuaternion(qList[i]));
        }
        return list;
    }

    public static List<Quaternion> GetSerializableList(List<SerializableQuaternion> qList)
    {
        List<Quaternion> list = new List<Quaternion>(qList.Count);
        for (int i = 0; i < qList.Count; i++)
        {
            list.Add(qList[i].UnityQuaternion);
        }
        return list;
    }
}
