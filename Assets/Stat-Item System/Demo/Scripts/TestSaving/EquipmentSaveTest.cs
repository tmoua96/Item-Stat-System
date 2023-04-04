using System.IO;
using UnityEngine;

public class EquipmentSaveTest : MonoBehaviour
{
    [SerializeField]
    private EquipmentData data;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            string path = Path.Combine(Application.dataPath, "Saves", "equipmentSave.txt");

            //string json = JsonUtility.ToJson(floatRef);
            //string json = JsonUtility.ToJson(floatVar);
            //Debug.Log(json);

            //File.WriteAllText(path, json);

            //ES3.Save("floatRef", floatRef, path);
            //ES3.Save("equipment", data, path);

            Debug.Log($"Saved {data.name}");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            string path = Path.Combine(Application.dataPath, "Saves", "equipmentSave.txt");

            //string json = File.ReadAllText(path);
            //Debug.Log(json);

            //floatRef = JsonUtility.FromJson<FloatReference>(json);
            //floatVar = JsonUtility.FromJson<FloatVariable>(json);
            //JsonUtility.FromJsonOverwrite(json, floatRef);
            //JsonUtility.FromJsonOverwrite(json, floatVar);

            //floatRef = ES3.Load<FloatReference>("floatRef", path);
            //data = ES3.Load<EquipmentData>("equipment", path);
            //ES3.LoadInto("floatRef", path, floatRef);
            //ES3.LoadInto("floatVar", path, floatVar);

            Debug.Log($"Loaded {data.name}");
        }
    }
}
