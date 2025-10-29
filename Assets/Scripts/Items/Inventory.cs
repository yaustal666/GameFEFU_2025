
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private Dictionary<int, Item> inventory = new Dictionary<int, Item>();

    public event Action<Item> ItemAdded;
    public event Action<Item> ItemRemoved;

    public ItemData item1;
    public ItemData item2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AddItem(item1, 10);
        AddItem(item2, 10);
    }

    public void AddItem(ItemData itemData, int amount = 1)
    {
        if (HasItem(itemData.ItemHashId)) { 
            inventory[itemData.ItemHashId].AddItem(amount);
        }
        else
        {
            inventory[itemData.ItemHashId] = new Item(itemData, amount);
        }
    }

    public void RemoveItem(ItemData itemData, int amount = 1)
    {
        if (HasItem(itemData.ItemHashId)) 
        {
            inventory[itemData.ItemHashId].RemoveItem(amount);
        }
    }

    public bool HasItem(int itemId, int amount = 1)
    {
        if (!inventory.ContainsKey(itemId)) return false;
        else if (inventory[itemId].GetAmount() < amount) return false;

        return true;
    }

    public int GetItemAmount(int itemId)
    {
        if (HasItem(itemId)) {
            return inventory[itemId].GetAmount();
        }

        return 0;
    }

    public Item GetItem(int itemId)
    {
        if (HasItem(itemId))
        {
            return inventory[itemId];
        }

        return null;
    }

    public List<Item> GetAllItems()
    {
        return new List<Item>(inventory.Values);
    }

    public List<Item> GetItemsOfType(ItemType type)
    {
        List<Item> result = new List<Item>();
        foreach (var item in inventory.Values)
        {
            if (item.ItemData.Type == type)
            {
                result.Add(item);
            }
        }
        return result;
    }

}