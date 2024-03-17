using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    private bool inInventory;
    private Item item;

    private void OnEnable()
    {
        GameEventsManager.Instance.CraftingEvents.onCraftItem += OnCraft;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.CraftingEvents.onCraftItem -= OnCraft;
    }

    public void Initialize(Item item)
    {
        Debug.Log("Initialized slot");
        this.item = item;
        itemImage.sprite = item.ItemData.image;
        StartCoroutine(UpdateSprite());
    }

    private void OnCraft(Item item)
    {
        StartCoroutine(UpdateSprite());
    }

    public IEnumerator UpdateSprite()
    {
        // Janky, but have to wait until next frame since inventory slots are not updated when Deleting until the next frame
        yield return null; 
        inInventory = InventoryManager.Instance.HasItem(item);
        if (!inInventory)
        {
            itemImage.color = new Color(0f, 0f, 0f, 0.6f);
        }
    }
}
