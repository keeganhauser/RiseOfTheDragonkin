using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : SingletonMonobehavior<CraftingManager>
{
    public CraftingRecipe[] recipes;

    // TODO: Make private
    public Dictionary<CraftingRecipe, bool> knownRecipesMap;

    protected override void Awake()
    {
        base.Awake();
        GenerateRecipesMap();
    }

    private void GenerateRecipesMap()
    {
        CraftingRecipe[] recipes = Resources.LoadAll<CraftingRecipe>("Recipes");
        knownRecipesMap = new Dictionary<CraftingRecipe, bool>();

        foreach (CraftingRecipe recipe in recipes)
        {
            if (knownRecipesMap.ContainsKey(recipe))
            {
                Debug.LogWarning($"Duplicate recipe found when generating map: {recipe.recipeName}");
            }
            // TODO: Redo this to load persistent data
            knownRecipesMap.Add(recipe, true);
        }

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
