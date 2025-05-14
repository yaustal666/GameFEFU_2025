using System.Collections.Generic;
using UnityEngine;

public class InventoryService
{
    private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new();


    public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
    {
        var inventory = new InventoryGrid(inventoryData);
        _inventoriesMap[inventory.OwnerId] = inventory;

        return inventory;
    }

    public AddItemsToInventoryGridResult AddItems(string ownerId, string itemId, int amount = 1)
    {
        var inventory = _inventoriesMap[ownerId];
        return inventory.AddItems(itemId, amount);
    }

    public AddItemsToInventoryGridResult AddItems(string ownerId, Vector2Int slotCoords, string itemId, int amount = 1)
    {
        var inventory = _inventoriesMap[ownerId];
        return inventory.AddItems(slotCoords, itemId, amount);
    }

    public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, string itemId, int amount = 1)
    {
        var inventory = _inventoriesMap[ownerId];
        return inventory.RemoveItems(itemId, amount);
    }
    public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, Vector2Int slotCoords, string itemId, int amount = 1)
    {
        var inventory = _inventoriesMap[ownerId];
        return inventory.RemoveItems(slotCoords, itemId, amount);
    }

    public bool Has(string ownerId, string itemId, int amount = 1)
    {
        var inventory = _inventoriesMap[ownerId];
        return inventory.Has(itemId, amount);
    }

    public IReadOnlyInventoryGrid GetInventory(string ownerId)
    {
        return _inventoriesMap[ownerId];
    }
}