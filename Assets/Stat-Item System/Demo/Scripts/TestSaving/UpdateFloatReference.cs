using UnityEngine;
//using ScriptableObjectArchitecture;
using System.IO;

public class UpdateFloatReference : MonoBehaviour, ISaveable
{
    public TMPro.TextMeshProUGUI floatText;
    [SerializeField]
    //public FloatReference floatRef;
    //public FloatVariable floatVar;

    void Update()
    {
        //floatText.text = floatRef.Value.ToString();
        //floatText.text = floatVar.Value.ToString();

        //if (Input.GetKeyDown(KeyCode.A))
        //    floatRef = new FloatReference(floatRef.Value + 5);
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            string path = Path.Combine(Application.dataPath, "Saves", "floatRefSave.txt");

            //string json = JsonUtility.ToJson(floatRef);
            //string json = JsonUtility.ToJson(floatVar);
            //Debug.Log(json);

            //File.WriteAllText(path, json);

            //ES3.Save("floatRef", floatRef, path);
            //ES3.Save("floatVar", floatVar, path);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            string path = Path.Combine(Application.dataPath, "Saves", "floatRefSave.txt");

            string json = File.ReadAllText(path);
            Debug.Log(json);

            //floatRef = JsonUtility.FromJson<FloatReference>(json);
            //floatVar = JsonUtility.FromJson<FloatVariable>(json);
            //JsonUtility.FromJsonOverwrite(json, floatRef);
            //JsonUtility.FromJsonOverwrite(json, floatVar);

            //floatRef = ES3.Load<FloatReference>("floatRef", path);
            //floatVar = ES3.Load<FloatVariable>("floatVar", path);
            //ES3.LoadInto("floatRef", path, floatRef);
            //ES3.LoadInto("floatVar", path, floatVar);
        }
    }

    public bool PopulateSaveData(SaveData data, SaveableEntity saveable)
    {
        //data.floatRefs[saveable.ID] = floatRef;
        // TODO: if gonna use this again, add FloatRef or FloatVar dictionary to savedata class
        return false;
    }

    public bool LoadSaveData(SaveData data, SaveableEntity saveable)
    {
        //if (!data.floatRefs.TryGetValue(saveable.ID, out var floatData))
        //    return false;

        //floatRef = floatData;

        return false;
    }
}
