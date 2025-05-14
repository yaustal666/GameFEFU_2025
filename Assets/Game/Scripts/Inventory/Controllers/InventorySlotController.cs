public class InventorySlotController
{
    private readonly InventorySlotView _view;

    public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view)
    {
        _view = view;
        slot.ItemIdChanged += OnSlotItemIdChanged;
        slot.ItemAmountChanged += OnSlotItemAmountChanged;

        view.Title = slot.ItemId;
        view.Amount = slot.Amount;
    }

    private void OnSlotItemIdChanged(string newItemid)
    {
        _view.Title = newItemid;
    }

    private void OnSlotItemAmountChanged(int newItemAmount)
    {
        _view.Amount = newItemAmount;
    }
}