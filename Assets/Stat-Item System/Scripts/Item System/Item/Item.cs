using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField]
    private ItemData data;
    public ItemData Data { get { return data; } }

    [SerializeField]
    private int amount;
    public int Amount { get { return amount; } }

    public Item() { }

    public Item(ItemData itemSO, int amount = 1)
    {
        this.data = itemSO;
        this.amount = amount;
    }

    public void AddAmount(int amount)
    {
        this.amount += amount;
    }

    public void RemoveAmount(int amount)
    {
        this.amount -= amount;
    }

    public override bool Equals(object obj)
    {
        return obj is Item item &&
               EqualityComparer<ItemData>.Default.Equals(Data, item.Data);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Data);
    }
}
