
public class Item
{
    public ItemData ItemData;
    private int count;

    public Item(ItemData data, int itemCount = 1)
    {
        ItemData = data;
        count = itemCount;
    }

    public void AddItem(int amount)
    {
        count += amount;
    }

    public void RemoveItem(int amount)
    {
        if (count >= amount)
        {
            count -= amount;
        }
    }

    public int GetAmount()
    {
        return count;
    }
}