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
    [SerializeField] private Button backgroundButton;

    private Button firstSelectedButton;

    public CraftingRecipe recipe;
    public CraftingRecipe recipe2;
    private CraftingRecipe selectedRecipe;

    private void Awake()
    {
        craftButton.onClick.AddListener(() => {
            CraftSelectedItem();
        });
        backgroundButton.onClick.AddListener(() =>
        {
            ToggleUI();
        });
        HideUI();
    }

    private void Start()
    {
        ScrollBarSetup();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.CraftingEvents.onCraftingMenuToggle += ToggleUI;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CraftingEvents.onCraftingMenuToggle -= ToggleUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleUI();
        }
    }

    private void CraftSelectedItem()
    {
        CraftingManager.Instance.CraftItem(selectedRecipe);
    }

    private void ScrollBarSetup()
    {
        foreach (CraftingRecipe recipe in CraftingManager.Instance.knownRecipesMap.Keys)
        {
            // Add the button to the scrolling list if not already added
            CraftingButton craftingButton = scrollingList.CreateButtonIfNotExists(recipe, () =>
            {
                SetCraftingInfo(recipe);
            });

            // Set questLogButton to first selected button if select button is null
            if (firstSelectedButton == null)
            {
                firstSelectedButton = craftingButton.Button;
                firstSelectedButton.Select();
            }
        }
    }

    private void SetCraftingInfo(CraftingRecipe recipe)
    {
        // If the recipe is already selected, don't redo everything
        //if (recipe == selectedRecipe) return;

        // Show proper amount of source slots as the recipe calls for
        foreach (Transform transform in sourceIngredientsGroup.transform)
        {
            Destroy(transform.gameObject);
        }
        foreach (GameObject itemPrefab in recipe.requiredIngredients)
        {
            Item item = itemPrefab.GetComponent<Item>();
            Instantiate(
                sourceIngredientSlotPrefab, 
                sourceIngredientsGroup.transform)
                .GetComponent<CraftingSlot>()
                .Initialize(item);
        }

        Item resultingItem = recipe.resultingItem.GetComponent<Item>();

        // Set resulting item text
        resultingItemText.text = resultingItem.ItemData.itemName;

        // Set resulting item slot image
        resultingItemSlot
            .GetComponentsInChildren<Image>()
            .First(c => c.gameObject != resultingItemSlot.gameObject)
            .sprite = resultingItem.ItemData.image;

        selectedRecipe = recipe;
        
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
        GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
        contentParent.SetActive(true);

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
