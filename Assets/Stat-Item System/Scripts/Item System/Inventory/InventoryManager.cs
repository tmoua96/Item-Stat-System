using UnityEngine;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour, IInventory, ISaveable
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Logging logger;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out ItemPickup pickup))
        //{
        //    pickup.Pickup(this);
        //}
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void AddItem(ItemData item)
    {
        AddItem(item, 1);
    }

    public void AddItem(ItemData item, int amount)
    {
        inventory.AddItem(item, amount);
    }

    public void RemoveItem(ItemData item)
    {
        RemoveItem(item, 1);
    }

    public void RemoveItem(ItemData item, int amount)
    {
        inventory.RemoveItem(item, amount);
    }

    public Item GetItem(Guid id)
    {
        return inventory.GetItem(id);
    }

    public bool TryGetItem(Guid id, out Item item)
    {
        bool success = inventory.TryGetItem(id, out Item i);
        item = i;
        return success;
    }

    public bool PopulateSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
        {
            playerData = new SaveData.PlayerData();
            data.playerData[saveable.ID] = playerData;
        }

        playerData.inventoryData = new(inventory.Items.ToArray());

        return true;
    }

    public bool LoadSaveData(SaveData data, SaveableEntity saveable)
    {
        if (!data.playerData.TryGetValue(saveable.ID, out var playerData))
            return false;

        inventory.Clear();

        SaveData.InventoryData inventoryData = playerData.inventoryData;

        foreach (var item in inventoryData.items)
        {
            inventory.AddItem(item.Data, item.Amount);
        }

        return true;
    }
}
