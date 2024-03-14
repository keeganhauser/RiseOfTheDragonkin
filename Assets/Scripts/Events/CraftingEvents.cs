using System;

public class CraftingEvents
{
    public event Action onCraftingMenuToggle;
    public void CraftingMenuToggle()
    {
        onCraftingMenuToggle?.Invoke();
    }

    public event Action<Item> onCraftingItemCraft;
    public void CraftingItemCraft(Item craftedItem)
    {
        onCraftingItemCraft?.Invoke(craftedItem);
    }
}
