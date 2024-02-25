using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Image image;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;

    private void Awake()
    {
        image = GetComponent<Image>();
        Deselect();
    }

    public void Select()
    {
        image.sprite = selectedSprite;
    }

    public void Deselect()
    {
        image.sprite = notSelectedSprite;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;

        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        inventoryItem.parentAfterDrag = transform;
    }
}
