using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    public Button Button { get; private set; }
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;

    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.Button = GetComponent<Button>();
        this.buttonText = GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction.Invoke();
    }
}
