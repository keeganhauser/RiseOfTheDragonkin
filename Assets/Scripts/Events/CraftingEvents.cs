using System;

public class CraftingEvents
{
    public event Action onCraftingMenuToggle;
    public void CraftingMenuToggle()
    {
        onCraftingMenuToggle?.Invoke();
    }

    public event Action<Item> onCraftItem;
    public void CraftItem(Item craftedItem)
    {
        onCraftItem?.Invoke(craftedItem);
    }
}
