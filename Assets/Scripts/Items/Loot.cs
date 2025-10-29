using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItem
{
    public ItemData Data;
    public int Amount;
    public float DropChance;
}


public class Loot : MonoBehaviour
{
    [SerializeField] private List<DropItem> droppedItems = new List<DropItem>();

    public void DropLoot()
    {

        foreach (var item in droppedItems)
        {
            if (Chance.Roll(item.DropChance))
            {
                Inventory.Instance.AddItem(item.Data, item.Amount);
            }
        }
    }
}