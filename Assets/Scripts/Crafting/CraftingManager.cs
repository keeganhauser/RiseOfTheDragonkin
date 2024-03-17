using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingManager : SingletonMonoBehavior<CraftingManager>
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
            Debug.Log($"Adding {recipe.recipeName} to recipe map");
            knownRecipesMap.Add(recipe, true);
        }

    }

    public bool CraftItem(CraftingRecipe recipe)
    {
        // Check if player has ingredients in inventory
        foreach (GameObject ingredientPrefab in recipe.requiredIngredients)
        {
            Item ingredient = ingredientPrefab.GetComponent<Item>();
            if (!InventoryManager.Instance.HasItem(ingredient))
            {
                Debug.Log($"Missing required ingredient: {ingredient.ItemData.itemName}");
                return false;
            }
        }

        // Remove ingredients from inventory
        foreach (GameObject ingredientPrefab in recipe.requiredIngredients)
        {
            Item ingredient = ingredientPrefab.GetComponent<Item>();
            bool removed = InventoryManager.Instance.RemoveItem(ingredient);
            if (!removed)
            {
                Debug.LogError("Error when removing items from inventory!");
                return false;
            }
        }

        // Add resulting item to inventory
        Item resultingItem = recipe.resultingItem.GetComponent<Item>();
        InventoryManager.Instance.AddItem(resultingItem);

        GameEventsManager.Instance.CraftingEvents.CraftItem(resultingItem);
        return true;
    }
}
