using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryListUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private InventoryScrollingList scrollingList;
    [SerializeField] private Button useButton;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    private Image inventoryListPanel;
    private Button firstSelectedButton;
    private Item selectedItem;
    private Item[] itemList;

    private void Awake()
    {
        inventoryListPanel = GetComponent<Image>();
        HideUI();
        useButton.onClick.AddListener(() =>
        {
            // TODO: Setup use button
            GameEventsManager.Instance.InventoryEvents.InventoryListUseItem(
                Player.Instance.gameObject, // TODO: Refactor to not have this hardcoded 
                selectedItem);
        });
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.InventoryEvents.onInventoryFinishLoad += InventoryFinishLoad;
        GameEventsManager.Instance.InventoryEvents.onInventoryRemoveItem += InventoryRemoveItem;
        GameEventsManager.Instance.InventoryEvents.onInventoryListToggle += ToggleUI;
        GameEventsManager.Instance.InventoryEvents.onInventoryListSelectItem += InventoryListSelectItem;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InventoryEvents.onInventoryFinishLoad -= InventoryFinishLoad;
        GameEventsManager.Instance.InventoryEvents.onInventoryRemoveItem -= InventoryRemoveItem;
        GameEventsManager.Instance.InventoryEvents.onInventoryListToggle -= ToggleUI;
        GameEventsManager.Instance.InventoryEvents.onInventoryListSelectItem -= InventoryListSelectItem;
    }

    private void InventoryRemoveItem(Item item, int amount)
    {
        StartCoroutine(PopulateScrollBar());
    }

    private void InventoryFinishLoad()
    {
        StartCoroutine(PopulateScrollBar());
    }

    private IEnumerator PopulateScrollBar()
    {
        // Clear scroll bar
        scrollingList.Clear();

        yield return StartCoroutine(UpdateItemList());

        foreach (Item item in itemList)
        {
            InventoryListButton inventoryButton = scrollingList.CreateButtonIfNotExists(item, () =>
            {
                SetItemInfo(item);
            });

            if (firstSelectedButton == null)
            {
                firstSelectedButton = inventoryButton.Button;
                firstSelectedButton.Select();
            }
        }
    }

    private IEnumerator UpdateItemList()
    {
        yield return null; // Wait a frame
        itemList = InventoryManager.Instance.GetUniqueItemList();
    }

    private void SetItemInfo(Item item)
    {
        itemNameText.text = item.ItemData.itemName;
        itemCountText.text = $"x{InventoryManager.Instance.GetItemCount(item)}";
        itemDescriptionText.text = item.ItemData.description;
        // Only want to use consumable items
        // TODO: Revisit this if ItemType becomes obsolete or outdated
        useButton.interactable = item.ItemData.type == ItemType.Consumable; 
    }

    private void ToggleUI()
    {
        if (contentParent.activeInHierarchy)
            HideUI();
        else
            ShowUI();
    }

    private void HideUI()
    {
        inventoryListPanel.raycastTarget = false;
        contentParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        inventoryListPanel.raycastTarget = true;

        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    private void InventoryListSelectItem(Item item)
    {
        selectedItem = item;
    }
} 
