using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    private Image itemImage;
    private bool inInventory;

    public void Initialize(Item item)
    {
        itemImage = GetComponentsInChildren<Image>().First(c => c.gameObject != this.gameObject);
        itemImage.sprite = item.image;
        inInventory = InventoryManager.Instance.HasItem(item);

        if (!inInventory)
        {
            itemImage.color = new Color(0f, 0f, 0f, 0.6f);
        }
    }
}
