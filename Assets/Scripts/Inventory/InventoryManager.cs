using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    public GameObject[] startingItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);

        foreach (GameObject itemPrefab in startingItems)
        {
            AddItem(itemPrefab.GetComponent<Item>());
        }
        GameEventsManager.Instance.InventoryEvents.InventoryFinishLoad();
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number >= 0 && number < 10)
            {
                if (number == 0)
                    number = 10;
                ChangeSelectedSlot(number - 1);
            }
        }
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.onToolbarScroll += ToolbarScroll;
        GameEventsManager.Instance.InventoryEvents.onInventoryListUseItem += UseItem;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onToolbarScroll -= ToolbarScroll;
        GameEventsManager.Instance.InventoryEvents.onInventoryListUseItem -= UseItem;
    }

    private void ToolbarScroll(float scroll)
    {
        if (scroll != 0)
        {
            int newValue = selectedSlot - (int)(scroll / Mathf.Abs(scroll));
            if (newValue < 0)
            {
                newValue = 9;
            }
            else if (newValue >= 10)
            {
                newValue = 0;
            }
            ChangeSelectedSlot(newValue);
        }
    }

    private void ChangeSelectedSlot(int newValue)
    {
        // Deselect current selection
        if (selectedSlot >= 0)
            inventorySlots[selectedSlot].Deselect();

        // Select new selection
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        InventorySlot slot;
        InventoryItem itemInSlot;

        // Find a spot to put item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            slot = inventorySlots[i];
            itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // Check if current inv slot already holds item of same type
            if (itemInSlot != null &&
                itemInSlot.item.ItemData.stackable &&
                itemInSlot.item == item &&
                itemInSlot.Count < itemInSlot.maxCount)
            {
                itemInSlot.Count++;
                return true;
            }
        }

        // If there was no previous stack to add to,
        // find empty slot to insert item.
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            slot = inventorySlots[i];
            itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public bool HasItem(Item item)
    {
        Debug.Log($"Checking to see if {item.ItemData.itemName} is in inventory...");
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                return true;
            }
        }
        return false;
    }



    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemObj = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObj.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use)
                UseItem(itemInSlot);

            return item;
        }
        return null;
    }

    public bool RemoveItem(Item item, int amount = 1)
    {
        // Find item
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                itemInSlot.Count -= amount;
                Debug.Log($"Items in slot: {itemInSlot.Count}");
                Debug.Log($"Removed {amount} {item.ItemData.itemName}");
                GameEventsManager.Instance.InventoryEvents.InventoryRemoveItem(item, amount);
                return true;
            }
        }
        return false;
    }

    private void UseItem(InventoryItem invItem)
    {
        invItem.Count--;
    }

    public int GetItemCount(Item item)
    {
        int count = 0;
        
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                count += itemInSlot.Count;        
            }
        }

        return count;
    }

    public Item[] GetUniqueItemList()
    {
        HashSet<Item> items = new HashSet<Item>();

        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                items.Add(itemInSlot.item);        
            }
        }
        
        return items.ToArray();
    }

    private void UseItem(GameObject target, Item item)
    {
        (item as ConsumableItem).Consume(target);
        RemoveItem(item);
    }
}
