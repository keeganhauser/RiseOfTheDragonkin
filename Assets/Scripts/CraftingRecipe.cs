using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Scriptable object/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public string recipeName;
    public Item[] requiredIngredients;
    public Item resultingItem;
}
