using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, ISelectHandler
{
    private TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        text.color = Color.green;
        Debug.Log("Entered");
    }
}
