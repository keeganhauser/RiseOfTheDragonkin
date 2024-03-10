using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    public CraftingRecipe[] recipes;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void CraftItem(CraftingRecipe recipe)
    {
        // Check if player has ingredients in inventory
        foreach (Item ingredient in recipe.requiredIngredients)
        {
            if (!InventoryManager.Instance.HasItem(ingredient))
            {
                Debug.Log($"Missing required ingredient: {ingredient.itemName}");
                return;
            }
        }

        // Remove ingredients from inventory
        foreach (Item ingredient in recipe.requiredIngredients)
        {
            bool removed = InventoryManager.Instance.RemoveItem(ingredient);
            if (!removed)
            {
                Debug.LogError("Error when removing items from inventory!");
                return;
            }
        }

        // Add resulting item to inventory
        InventoryManager.Instance.AddItem(recipe.resultingItem);
    }
}
