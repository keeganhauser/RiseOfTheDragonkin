using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Inventory List Button")]
    [SerializeField] private GameObject inventoryButtonPrefab;

    private Dictionary<string, InventoryListButton> idToButtonMap = new Dictionary<string, InventoryListButton>();

    public InventoryListButton CreateButtonIfNotExists(Item item, UnityAction selectAction)
    {
        InventoryListButton button = null;

        if (!idToButtonMap.ContainsKey(item.ItemData.itemName))
        {
            button = InstantiateInventoryListButton(item, selectAction);
        }
        else
        {
            button = idToButtonMap[item.ItemData.itemName];
        }
        return button;
    }

    private InventoryListButton InstantiateInventoryListButton(Item item, UnityAction selectAction)
    {
        // Create button
        InventoryListButton button = Instantiate(
            inventoryButtonPrefab,
            contentParent.transform)
            .GetComponent<InventoryListButton>();

        // Set name in scene
        button.gameObject.name = $"{item.ItemData.itemName}_button";

        // Init button
        button.Initialize(item, selectAction);

        // Add to map
        idToButtonMap.Add(item.ItemData.itemName, button);

        return button;
    }

    public void Clear()
    {
        foreach (Transform childTransform in contentParent.transform)
        {
            Destroy(childTransform.gameObject);
        }
        idToButtonMap.Clear();
    }
}
