using UnityEngine;
using System.IO;  

public class TestSaveable : MonoBehaviour
{
    [SerializeField]
    private Logging logger;

    //[SerializeField]
    //private float speed = 10;

    public ItemData testItem;
    public ItemData testItemToSee;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            string savePath = Path.Combine(Application.dataPath, "Saves/otherTestSave.txt");
            Item item = new(testItem);
            string itemStr = JsonUtility.ToJson(item);
            System.IO.File.WriteAllText(savePath, itemStr);

            logger.Log(itemStr, this);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            string savePath = Path.Combine(Application.dataPath, "Saves/otherTestSave.txt");
            string itemStr = File.ReadAllText(savePath);
            Item item = JsonUtility.FromJson<Item>(itemStr);

            testItemToSee = item.Data;

            logger.Log($"item string: {itemStr}", this);
            logger.Log($"ItemSO name: {item.Data.name}", this);
            logger.Log("Item: " + item, this);
        }
    }
}
