using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Item System/Item/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    protected string id;
    [SerializeField]
    protected new string name;
    [SerializeField, TextArea]
    protected string description;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected GameObject itemPrefab;
    [SerializeField]
    protected bool stackable = true;
    [SerializeField]
    protected int maxStackAmount = 99;
    [SerializeField, Tooltip("Whether there can only be 1 of the item in the inventory.")]
    protected bool unique = false;
    [SerializeField]
    protected int price;
    [SerializeField]
    protected Use[] uses;

    [SerializeField, Tooltip("This allows the ID to be changed even if it already has one assigned. " +
        "Avoid changing ID if not necessary. It can cause issues if save files exist because it assigns values by ID.")]
    protected bool generateIdWhenNotEmpty = false;  // TODO - see if there's a better way to keep IDs unchanged or if there's a better way to deal with save files
    
    public string ID { get { return id; } }
    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public GameObject ItemPrefab { get { return Instantiate(itemPrefab); } }
    public bool Stackable { get { return stackable; } }
    public int MaxStackAmount { get { return maxStackAmount; } }
    public bool Unique { get { return unique; } }
    public float Price { get { return price; } }
    public Use[] Use { get { return uses; } }

    public virtual void UseItem(MonoBehaviour user)
    {
        if (uses != null)
            for (int i = 0; i < uses.Length; i++)
                uses[i].UseEffect(user);
    }

    [ContextMenu("Set Stack Amount To Min")]
    protected void SetStackAmountToMin()
    {
        maxStackAmount = 1;
    }

    [ContextMenu("Set Stack Amount To Max")]
    protected void SetStackAmountToMax()
    {
        maxStackAmount = int.MaxValue;
    }

    [ContextMenu("Generate Id")]
    protected void GenerateId()
    {
        if(string.IsNullOrEmpty(id) || generateIdWhenNotEmpty)
            id = Guid.NewGuid().ToString();
    }

    protected void Reset()
    {
        GenerateId();
    }

    private void OnValidate()
    {
        if (!stackable || unique)
            maxStackAmount = 1;
    }
}