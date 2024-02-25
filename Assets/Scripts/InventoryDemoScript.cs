using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDemoScript : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool res = InventoryManager.AddItem(itemsToPickup[id]);
        if (res)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Could not add item");
        }
    }

    public void GetSelectedItem()
    {
        Item receivedItem = InventoryManager.GetSelectedItem(false);

        if (receivedItem != null)
        {
            Debug.Log($"Received item: {receivedItem.name}");
        }
        else
        {
            Debug.Log("No item received");
        }
    }
}
