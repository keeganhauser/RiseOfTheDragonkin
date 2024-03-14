using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private CraftingScrollingList scrollingList;
    [SerializeField] private TextMeshProUGUI resultingItemText;
    [SerializeField] private Button craftButton;
    [SerializeField] private GridLayoutGroup sourceIngredientsGroup;
    [SerializeField] private GameObject sourceIngredientSlotPrefab;
    [SerializeField] private Image resultingItemSlot;

    private Button firstSelectedButton;

    public CraftingRecipe recipe;
    private CraftingRecipe selectedRecipe;

    private void Awake()
    {
        craftButton.onClick.AddListener(() => CraftingManager.Instance.CraftItem(recipe));
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CraftingEvents.onCraftingMenuToggle += ToggleUI;
        GameEventsManager.Instance.InventoryEvents.onInventoryFinishLoad += TestRecipe;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CraftingEvents.onCraftingMenuToggle -= ToggleUI;
        GameEventsManager.Instance.InventoryEvents.onInventoryFinishLoad -= TestRecipe;
    }

    private void TestRecipe()
    {
        SetCraftingInfo(recipe);
    }

    private void SetCraftingInfo(CraftingRecipe recipe)
    {
        // Show proper amount of source slots as the recipe calls for
        // TODO: Show source items in slots
        foreach (Transform transform in sourceIngredientsGroup.transform)
        {
            Destroy(transform.gameObject);
        }
        foreach (Item item in recipe.requiredIngredients)
        {
            Instantiate(
                sourceIngredientSlotPrefab, 
                sourceIngredientsGroup.transform)
                .GetComponent<CraftingSlot>()
                .Initialize(item);
        }

        // Set resulting item text
        resultingItemText.text = recipe.resultingItem.itemName;

        // Set resulting item slot image
        resultingItemSlot
            .GetComponentsInChildren<Image>()
            .First(c => c.gameObject != resultingItemSlot.gameObject)
            .sprite = recipe.resultingItem.image;
    }

    private void ToggleUI()
    {
        if (contentParent.activeInHierarchy)
            HideUI();
        else
            ShowUI();
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();

        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
        EventSystem.current.SetSelectedGameObject(null);
    }
}
