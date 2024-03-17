using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Scriptable object/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public string recipeName;
    public GameObject[] requiredIngredients;
    public GameObject resultingItem;
}
