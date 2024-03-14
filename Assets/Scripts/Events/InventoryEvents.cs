using System;

public class InventoryEvents
{
    public event Action onInventoryFinishLoad;
    public void InventoryFinishLoad()
    {
        onInventoryFinishLoad?.Invoke();
    }

    public event Action<Item, int> onInventoryAddItem;
    public void InventoryAddItem(Item item, int amount)
    {
        onInventoryAddItem?.Invoke(item, amount);
    }

    public event Action<Item, int> onInventoryRemoveItem;
    public void InventoryRemoveItem(Item item, int amount)
    {
        onInventoryRemoveItem?.Invoke(item, amount);
    }

    public event Action onInventorySlotSelect;
    public void InventorySlotSelect()
    {
        onInventorySlotSelect?.Invoke();
    }
}
