using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Resource
}

[CreateAssetMenu(menuName = "ScriptableObjects/ItemData", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemId;
    [SerializeField] private int _itemHashId;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ItemType _itemType;

    public string ItemId => _itemId;
    public int ItemHashId => _itemHashId;
    public Sprite Icon => _icon;
    public ItemType Type => _itemType;
}