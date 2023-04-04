using System;
using UnityEngine;

[Serializable]
public class InventoryEventData
{
    [SerializeField]
    private Item modifiedItem;
    public Item ModifiedItem => modifiedItem;
    [SerializeField]
    private int modifiedAmount;
    public int ModifiedAmount => modifiedAmount;

    [SerializeField]
    private bool wasItemAdded;
    public bool WasItemAdded => wasItemAdded;

    [SerializeField]
    private Item[] items;
    public Item[] Items => items;

    public InventoryEventData(Item modifiedItem, int modifiedAmount, bool wasItemAdded, Item[] items)
    {
        this.modifiedItem = modifiedItem;
        this.modifiedAmount = modifiedAmount;
        this.wasItemAdded = wasItemAdded;
        this.items = items;
    }
}
