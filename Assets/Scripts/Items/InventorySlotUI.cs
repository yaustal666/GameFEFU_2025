using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    private int itemId;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private GameObject onSelectLighting;


    public void UpdateData(Sprite sprite, int amount)
    {
        itemIcon.sprite = sprite;
        amountText.text = amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelectLighting.SetActive(true);
    }
}