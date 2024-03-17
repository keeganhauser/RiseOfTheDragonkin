using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryListButton : MonoBehaviour, ISelectHandler
{
    public Button Button { get; private set; }
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonItemImage;
    private UnityAction onSelectAction;
    private Item item;

    public void Initialize(Item item, UnityAction selectAction)
    {
        this.Button = GetComponent<Button>();
        this.onSelectAction = selectAction;
        this.item = item;

        buttonText.text = item.ItemData.itemName;
        buttonItemImage.sprite = item.ItemData.image;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction.Invoke();
        GameEventsManager.Instance.InventoryEvents.InventoryListSelectItem(item);
    }
}
