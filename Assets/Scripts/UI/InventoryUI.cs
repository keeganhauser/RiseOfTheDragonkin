using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject mainInventoryGroup;
    [SerializeField] private Button darkBackgroundButton;
    [SerializeField] private Button showInventoryButton;

    private void Awake()
    {
        // Setup the button's onClick to trigger the onInventoryTogglePressed event
        showInventoryButton.onClick.AddListener(() => GameEventsManager.Instance.InputEvents.InventoryTogglePressed());
        darkBackgroundButton.onClick.AddListener(() => GameEventsManager.Instance.InputEvents.InventoryTogglePressed());
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.onInventoryTogglePressed += ToggleInventory;
        GameEventsManager.Instance.InputEvents.onQuestLogTogglePressed += ToggleButton;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.onInventoryTogglePressed -= ToggleInventory;
        GameEventsManager.Instance.InputEvents.onQuestLogTogglePressed -= ToggleButton;
    }

    private void ToggleInventory()
    {
        // If inventory is active in scene, hide it
        if (mainInventoryGroup.gameObject.activeInHierarchy)
        {
            mainInventoryGroup.SetActive(false);
            ToggleButton();
        }

        // Otherwise show it
        else
        {
            mainInventoryGroup.SetActive(true);
            ToggleButton();
        }
    }

    private void ToggleButton()
    {
        showInventoryButton.gameObject.SetActive(!showInventoryButton.gameObject.activeInHierarchy);
    }
}
