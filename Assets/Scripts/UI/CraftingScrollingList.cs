using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Recipe Button")]
    [SerializeField] private GameObject craftingButtonPrefab;

    private Dictionary<string, CraftingButton> idToButtonMap = new Dictionary<string, CraftingButton>();

    public CraftingButton CreateButtonIfNotExists(CraftingRecipe recipe, UnityAction selectAction)
    {
        CraftingButton craftingButton = null;

        if (!idToButtonMap.ContainsKey(recipe.recipeName))
        {
            craftingButton = InstantiateCraftingButton(recipe, selectAction);
        }
        else
        {
            craftingButton = idToButtonMap[recipe.recipeName];
        }
        return craftingButton;
    }

    private CraftingButton InstantiateCraftingButton(CraftingRecipe recipe, UnityAction selectAction)
    {
        // Create button
        CraftingButton craftingButton = Instantiate(
            craftingButtonPrefab,
            contentParent.transform)
            .GetComponent<CraftingButton>();

        // Set name in scene
        craftingButton.gameObject.name = $"{recipe.recipeName}_button";

        // Init button
        craftingButton.Initialize(recipe.recipeName, selectAction);

        // Add to map to keep track of button
        idToButtonMap.Add(recipe.recipeName, craftingButton);

        return craftingButton;
    }
}
