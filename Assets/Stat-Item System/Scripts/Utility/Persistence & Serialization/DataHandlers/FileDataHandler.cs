using UnityEngine;
using System.IO;
using System;
//using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Data Persistence/Data Handler/LocalFileHandler")]
public class FileDataHandler : DataHandler
{
    public override SaveData Load(int saveSlot)
    {
        SaveData data = null;
        string fullPath = Path.Combine(dataDirPath, $"{dataFilePath}-{saveSlot:D3}.{fileExtension}");

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = File.ReadAllText(fullPath);

                //data = JsonConvert.DeserializeObject<SaveData>(dataToLoad);
            }
            catch (Exception e)
            {
                logger.Log($"Error occured when trying to load data from file: {fullPath}\n{e}", this);
            }
        }

        return data;
    }

    public override bool Save(SaveData data, int saveSlot)
    {
        string fullPath = Path.Combine(dataDirPath, $"{dataFilePath}-{saveSlot:D3}.{fileExtension}");

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //JsonSerializerSettings settings = new JsonSerializerSettings()
            //{
            //    Formatting = Formatting.Indented,
            //};

            //string dataToStore = JsonConvert.SerializeObject(data, settings);

            //File.WriteAllText(fullPath, dataToStore);
            return true;
        }
        catch (Exception e)
        {
            logger.Log($"Error occured when trying to save data to file: {fullPath}\n{e}", this);
        }

        return false;
    }
}
