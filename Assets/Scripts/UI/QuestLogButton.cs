using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
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

    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.RequirementsNotMet:
            case QuestState.CanStart:
                buttonText.color = Color.red;
                break;
            case QuestState.InProgress:
            case QuestState.CanFinish:
                buttonText.color = Color.yellow;
                break;
            case QuestState.Finished:
                buttonText.color = Color.green;
                break;
            default:
                break;
        }
    }
}
