using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject weaponSection;
    [SerializeField] private GameObject weaponSectionContent;

    [SerializeField] private GameObject consumablesSection;
    [SerializeField] private GameObject consumablesSectionContent;

    [SerializeField] private GameObject resourcesSection;
    [SerializeField] private GameObject resourcesSectionContent;

    [SerializeField] private GameObject itemSlotPrefab;

    private void Start()
    {
        List<Item> weapons = Inventory.Instance.GetItemsOfType(ItemType.Weapon);
        if (weapons.Count != 0)
        {
            weaponSection.SetActive(true);
        }
        foreach (Item weapon in weapons)
        {
            var slot = Instantiate(itemSlotPrefab);
            slot.transform.SetParent(weaponSectionContent.transform, false);
            slot.GetComponent<InventorySlotUI>().UpdateData(weapon.ItemData.Icon, weapon.GetAmount());
        }

        List<Item> resources = Inventory.Instance.GetItemsOfType(ItemType.Resource);
        if (resources.Count != 0)
        {
            resourcesSection.SetActive(true);
        }
        foreach (Item resource in resources)
        {
            var slot = Instantiate(itemSlotPrefab);
            slot.transform.SetParent(resourcesSectionContent.transform, false);
            slot.GetComponent<InventorySlotUI>().UpdateData(resource.ItemData.Icon, resource.GetAmount());
        }

    }
}