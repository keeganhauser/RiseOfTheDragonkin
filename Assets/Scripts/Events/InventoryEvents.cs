using System;
using UnityEngine;

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

    public event Action onInventoryListToggle;
    public void InventoryListToggle()
    {
        onInventoryListToggle?.Invoke();
    }

    public event Action<GameObject, Item> onInventoryListUseItem;
    public void InventoryListUseItem(GameObject target, Item item)
    {
        Debug.Log($"Used item: {item.ItemData.itemName}");
        onInventoryListUseItem?.Invoke(target, item);
    }

    public event Action<Item> onInventoryListSelectItem;
    public void InventoryListSelectItem(Item item)
    {
        onInventoryListSelectItem?.Invoke(item);
    }
}
