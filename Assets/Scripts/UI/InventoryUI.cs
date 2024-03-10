using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject mainInventoryGroup;
    [SerializeField] private Button showInventoryButton;

    private void Awake()
    {
        // Setup the button's onClick to trigger the onInventoryTogglePressed event
        showInventoryButton.onClick.AddListener(() => GameEventsManager.Instance.InputEvents.InventoryTogglePressed());
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.onInventoryTogglePressed += ToggleInventory;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onInventoryTogglePressed -= ToggleInventory;
    }

    private void ToggleInventory()
    {
        // If inventory is active in scene, hide it
        if (mainInventoryGroup.gameObject.activeInHierarchy)
        {
            mainInventoryGroup.SetActive(false);
            showInventoryButton.gameObject.SetActive(true);
        }

        // Otherwise show it
        else
        {
            mainInventoryGroup.SetActive(true);
            showInventoryButton.gameObject.SetActive(false);
        }
    }
}
