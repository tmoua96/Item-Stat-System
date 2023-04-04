using UnityEngine;
using System.Collections.Generic;
using System;
using ScriptableObjectArchitecture;
using System.Linq;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Item System/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    private int inventorySize = 20;
    [SerializeField]
    private List<Item> items = new List<Item>();
    [SerializeField]
    private bool allowMultipleStacks = true;

    public Item[] Items => items.ToArray();

    public int RemainingInventorySpace { get { return inventorySize - items.Count; } }
    public bool IsFull { get { return items.Count >= inventorySize; } }

    [SerializeField]
    private InventoryEventDataGameEvent InventoryChangedEvent;

    private void OnEnable()
    {
        items.Clear();
    }

    /// <summary>
    /// Add an item to the inventory.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    /// <param name="amountToAdd">The amount of an item to be added.</param>
    /// <returns>The item that was added, else null.</returns>
    public Item AddItem(ItemData itemData, int amountToAdd = 1)
    {
        if (IsFull && GetItem(itemData).Amount >= itemData.MaxStackAmount)
            return null;
        amountToAdd = Mathf.Clamp(amountToAdd, 1, int.MaxValue);

        if (TryGetItem(itemData, out Item item))
        {
            if (itemData.Unique)
            {
                return null;
            }
            else if (itemData.Stackable && item.Amount + amountToAdd <= itemData.MaxStackAmount)
            {
                item.AddAmount(amountToAdd);
            }
            else if (itemData.Stackable && item.Amount + amountToAdd > itemData.MaxStackAmount)
            {
                // Original value minus new amount is added to current stack, new amountToAdd is added to the new stack after the previous one becomes full
                int newStackAmount = itemData.MaxStackAmount + amountToAdd - itemData.MaxStackAmount;
                item.AddAmount(amountToAdd - newStackAmount);
                item = new Item(itemData, newStackAmount);
                items.Add(item);
            }
            else
            {
                item = new Item(itemData, amountToAdd);
                items.Add(item);
            }
        }
        else
        {
            item = new Item(itemData, amountToAdd);
            items.Add(item);
        }

        InventoryChangedEvent?.Raise(new(item, amountToAdd, true, Items));

        return item;
    }

    /// <summary>
    /// Remove an item from the inventory.
    /// </summary>
    /// <param name="itemData">The item to be removed</param>
    /// <returns>The item that was removed.</returns>
    public Item RemoveItem(ItemData itemData, int amountToRemove = 1)
    {
        if (!HasItem(itemData))
            return null;

        if (TryGetItem(itemData, out Item i))
        {
            if (itemData.Unique || i.Amount <= amountToRemove)
                items.Remove(i);
            else
                i.RemoveAmount(amountToRemove);

            InventoryChangedEvent?.Raise(new(i, amountToRemove, false, Items));
        }

        return i;
    }

    private bool HasItem(ItemData itemData)
    {
        return items.Exists(item => item.Data == itemData);
    }

    private bool HasItem(Guid id)
    {
        return items.Exists(item => item.Data.ID.Equals(id.ToString()));
    }

    public bool TryGetItem(ItemData itemData, out Item item)
    {
        item = null;
        Item[] itemsArray = items?.FindAll(x => x.Data == itemData).ToArray();

        for (int i = 0; i < itemsArray.Length; i++)
        {
            Item tempItem = itemsArray[i];

            if (tempItem.Amount < itemData.MaxStackAmount || (tempItem.Amount >= itemData.MaxStackAmount && i + 1 >= itemsArray.Length))
            {
                item = tempItem;
                break;
            }
        }

        return item != null;
    }

    public bool TryGetItem(Guid id, out Item item)
    {
        item = null;
        Item[] itemsArray = items?.FindAll(x => x.Data.ID.Equals(id.ToString())).ToArray();

        for (int i = 0; i < itemsArray.Length; i++)
        {
            Item tempItem = itemsArray[i];

            if (tempItem.Amount < tempItem.Data.MaxStackAmount || (tempItem.Amount >= tempItem.Data.MaxStackAmount && i + 1 >= itemsArray.Length))
            {
                item = tempItem;
                break;
            }
        }

        return item != null;
    }

    public void Clear()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            RemoveItem(items[i].Data, items[i].Amount);
        }
    }

    public Item GetItem(ItemData item)
    {
        TryGetItem(item, out Item i);
        return i;
    }

    public Item GetItem(Guid id)
    {
        TryGetItem(id, out Item i);
        return i;
    }
}
