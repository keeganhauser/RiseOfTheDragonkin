using UnityEngine;

public class InventoryManager : SingletonMonobehavior<InventoryManager>
{
    public Item[] startingItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);

        foreach (Item item in startingItems)
        {
            AddItem(item);
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
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onToolbarScroll -= ToolbarScroll;
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
                itemInSlot.item.stackable &&
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
                return true;
            }
        }
        return false;
    }

    private void UseItem(InventoryItem invItem)
    {
        invItem.Count--;
    }
}
