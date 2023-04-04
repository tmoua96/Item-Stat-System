using System;

public interface IInventory
{
    void AddItem(ItemData item, int amount = 1);
    void RemoveItem(ItemData item, int amount = 1);
    Item GetItem(Guid id);
    bool TryGetItem(Guid id, out Item item);
}
