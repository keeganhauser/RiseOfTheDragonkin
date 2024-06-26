using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public TMP_Text countText;
    private Image image;

    [HideInInspector] public Item item;
    [HideInInspector] private int count = 1;
    [HideInInspector]
    public int Count
    {
        get { return count; }
        set
        {
            count = value;

            if (count <= 0)
                Destroy(this.gameObject);

            RefreshCountText();
        }
    }

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int maxCount = 5;

    public void InitializeItem(Item item)
    {
        image = GetComponent<Image>();
        this.item = item;
        image.sprite = item.ItemData.image;
        RefreshCountText();
    }

    public void RefreshCountText()
    {
        if (item.ItemData.stackable && count > 1)
            countText.text = count.ToString();
        else
            countText.text = string.Empty;
    }

    // Drag and drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        countText.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        countText.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
